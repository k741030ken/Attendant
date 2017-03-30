'****************************************************************
' Table:Calendar
' Created Date: 2015.04.29
'****************************************************************/
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports Microsoft.Practices.EnterpriseLibrary.Common
Imports Microsoft.Practices.EnterpriseLibrary.Data
Imports System.Data.Common
Imports System.Data

Namespace beCalendar
    Public Class Table
        Private m_Rows As Rows = New Rows()
        Private m_Fields As String() = { "CompID", "SysDate", "HolidayOrNot", "Week", "NextBusDate", "LastBusDate", "NeNeBusDate", "LastEndDate", "ThisEndDate", "MonEndDate" _
                                    , "NextDateDiff", "NeNeDateDiff", "LastDateDiff", "JulianDate", "LastChgComp", "LastChgID", "LastChgDate" }
        Private m_Types As System.Type() = { GetType(String), GetType(Date), GetType(String), GetType(String), GetType(Date), GetType(Date), GetType(Date), GetType(Date), GetType(Date), GetType(Date) _
                                    , GetType(Integer), GetType(Integer), GetType(Integer), GetType(Integer), GetType(String), GetType(String), GetType(Date) }
        Private m_PrimaryFields As String() = { "CompID", "SysDate" }

        Public ReadOnly Property Rows() As beCalendar.Rows 
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
        Public Sub Transfer2Row(CalendarTable As DataTable)
            For Each dr As DataRow In CalendarTable.Rows
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
                dr(m_Rows(i).SysDate.FieldName) = m_Rows(i).SysDate.Value
                dr(m_Rows(i).HolidayOrNot.FieldName) = m_Rows(i).HolidayOrNot.Value
                dr(m_Rows(i).Week.FieldName) = m_Rows(i).Week.Value
                dr(m_Rows(i).NextBusDate.FieldName) = m_Rows(i).NextBusDate.Value
                dr(m_Rows(i).LastBusDate.FieldName) = m_Rows(i).LastBusDate.Value
                dr(m_Rows(i).NeNeBusDate.FieldName) = m_Rows(i).NeNeBusDate.Value
                dr(m_Rows(i).LastEndDate.FieldName) = m_Rows(i).LastEndDate.Value
                dr(m_Rows(i).ThisEndDate.FieldName) = m_Rows(i).ThisEndDate.Value
                dr(m_Rows(i).MonEndDate.FieldName) = m_Rows(i).MonEndDate.Value
                dr(m_Rows(i).NextDateDiff.FieldName) = m_Rows(i).NextDateDiff.Value
                dr(m_Rows(i).NeNeDateDiff.FieldName) = m_Rows(i).NeNeDateDiff.Value
                dr(m_Rows(i).LastDateDiff.FieldName) = m_Rows(i).LastDateDiff.Value
                dr(m_Rows(i).JulianDate.FieldName) = m_Rows(i).JulianDate.Value
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

        Public Sub Add(CalendarRow As Row)
            m_Rows.Add(CalendarRow)
        End Sub

        Public Sub Remove(CalendarRow As Row)
            If m_Rows.IndexOf(CalendarRow) >= 0 Then
                m_Rows.Remove(CalendarRow)
            End If
        End Sub

        Public Sub Dispose()
            m_Rows.Clear()
        End Sub

    End Class

    Public Class Row

        Private FI_CompID As Field(Of String) = new Field(Of String)("CompID", true)
        Private FI_SysDate As Field(Of Date) = new Field(Of Date)("SysDate", true)
        Private FI_HolidayOrNot As Field(Of String) = new Field(Of String)("HolidayOrNot", true)
        Private FI_Week As Field(Of String) = new Field(Of String)("Week", true)
        Private FI_NextBusDate As Field(Of Date) = new Field(Of Date)("NextBusDate", true)
        Private FI_LastBusDate As Field(Of Date) = new Field(Of Date)("LastBusDate", true)
        Private FI_NeNeBusDate As Field(Of Date) = new Field(Of Date)("NeNeBusDate", true)
        Private FI_LastEndDate As Field(Of Date) = new Field(Of Date)("LastEndDate", true)
        Private FI_ThisEndDate As Field(Of Date) = new Field(Of Date)("ThisEndDate", true)
        Private FI_MonEndDate As Field(Of Date) = new Field(Of Date)("MonEndDate", true)
        Private FI_NextDateDiff As Field(Of Integer) = new Field(Of Integer)("NextDateDiff", true)
        Private FI_NeNeDateDiff As Field(Of Integer) = new Field(Of Integer)("NeNeDateDiff", true)
        Private FI_LastDateDiff As Field(Of Integer) = new Field(Of Integer)("LastDateDiff", true)
        Private FI_JulianDate As Field(Of Integer) = new Field(Of Integer)("JulianDate", true)
        Private FI_LastChgComp As Field(Of String) = new Field(Of String)("LastChgComp", true)
        Private FI_LastChgID As Field(Of String) = new Field(Of String)("LastChgID", true)
        Private FI_LastChgDate As Field(Of Date) = new Field(Of Date)("LastChgDate", true)
        Private m_FieldNames As String() = { "CompID", "SysDate", "HolidayOrNot", "Week", "NextBusDate", "LastBusDate", "NeNeBusDate", "LastEndDate", "ThisEndDate", "MonEndDate" _
                                    , "NextDateDiff", "NeNeDateDiff", "LastDateDiff", "JulianDate", "LastChgComp", "LastChgID", "LastChgDate" }
        Private m_PrimaryFields As String() = { "CompID", "SysDate" }
        Private m_IdentityFields As String() = {  }
        Private m_LoadFromDataRow As Boolean = False

        Private Function GetFieldValue(ByVal fieldName As String) As Object
            Select Case fieldName
                Case "CompID"
                    Return FI_CompID.Value
                Case "SysDate"
                    Return FI_SysDate.Value
                Case "HolidayOrNot"
                    Return FI_HolidayOrNot.Value
                Case "Week"
                    Return FI_Week.Value
                Case "NextBusDate"
                    Return FI_NextBusDate.Value
                Case "LastBusDate"
                    Return FI_LastBusDate.Value
                Case "NeNeBusDate"
                    Return FI_NeNeBusDate.Value
                Case "LastEndDate"
                    Return FI_LastEndDate.Value
                Case "ThisEndDate"
                    Return FI_ThisEndDate.Value
                Case "MonEndDate"
                    Return FI_MonEndDate.Value
                Case "NextDateDiff"
                    Return FI_NextDateDiff.Value
                Case "NeNeDateDiff"
                    Return FI_NeNeDateDiff.Value
                Case "LastDateDiff"
                    Return FI_LastDateDiff.Value
                Case "JulianDate"
                    Return FI_JulianDate.Value
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
                Case "SysDate"
                    FI_SysDate.SetValue(value)
                Case "HolidayOrNot"
                    FI_HolidayOrNot.SetValue(value)
                Case "Week"
                    FI_Week.SetValue(value)
                Case "NextBusDate"
                    FI_NextBusDate.SetValue(value)
                Case "LastBusDate"
                    FI_LastBusDate.SetValue(value)
                Case "NeNeBusDate"
                    FI_NeNeBusDate.SetValue(value)
                Case "LastEndDate"
                    FI_LastEndDate.SetValue(value)
                Case "ThisEndDate"
                    FI_ThisEndDate.SetValue(value)
                Case "MonEndDate"
                    FI_MonEndDate.SetValue(value)
                Case "NextDateDiff"
                    FI_NextDateDiff.SetValue(value)
                Case "NeNeDateDiff"
                    FI_NeNeDateDiff.SetValue(value)
                Case "LastDateDiff"
                    FI_LastDateDiff.SetValue(value)
                Case "JulianDate"
                    FI_JulianDate.SetValue(value)
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
                Case "SysDate"
                    return FI_SysDate.Updated
                Case "HolidayOrNot"
                    return FI_HolidayOrNot.Updated
                Case "Week"
                    return FI_Week.Updated
                Case "NextBusDate"
                    return FI_NextBusDate.Updated
                Case "LastBusDate"
                    return FI_LastBusDate.Updated
                Case "NeNeBusDate"
                    return FI_NeNeBusDate.Updated
                Case "LastEndDate"
                    return FI_LastEndDate.Updated
                Case "ThisEndDate"
                    return FI_ThisEndDate.Updated
                Case "MonEndDate"
                    return FI_MonEndDate.Updated
                Case "NextDateDiff"
                    return FI_NextDateDiff.Updated
                Case "NeNeDateDiff"
                    return FI_NeNeDateDiff.Updated
                Case "LastDateDiff"
                    return FI_LastDateDiff.Updated
                Case "JulianDate"
                    return FI_JulianDate.Updated
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
                Case "SysDate"
                    return FI_SysDate.CreateUpdateSQL
                Case "HolidayOrNot"
                    return FI_HolidayOrNot.CreateUpdateSQL
                Case "Week"
                    return FI_Week.CreateUpdateSQL
                Case "NextBusDate"
                    return FI_NextBusDate.CreateUpdateSQL
                Case "LastBusDate"
                    return FI_LastBusDate.CreateUpdateSQL
                Case "NeNeBusDate"
                    return FI_NeNeBusDate.CreateUpdateSQL
                Case "LastEndDate"
                    return FI_LastEndDate.CreateUpdateSQL
                Case "ThisEndDate"
                    return FI_ThisEndDate.CreateUpdateSQL
                Case "MonEndDate"
                    return FI_MonEndDate.CreateUpdateSQL
                Case "NextDateDiff"
                    return FI_NextDateDiff.CreateUpdateSQL
                Case "NeNeDateDiff"
                    return FI_NeNeDateDiff.CreateUpdateSQL
                Case "LastDateDiff"
                    return FI_LastDateDiff.CreateUpdateSQL
                Case "JulianDate"
                    return FI_JulianDate.CreateUpdateSQL
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
            FI_SysDate.SetInitValue(Convert.ToDateTime("1900/01/01"))
            FI_HolidayOrNot.SetInitValue("0")
            FI_Week.SetInitValue("")
            FI_NextBusDate.SetInitValue(Convert.ToDateTime("1900/01/01"))
            FI_LastBusDate.SetInitValue(Convert.ToDateTime("1900/01/01"))
            FI_NeNeBusDate.SetInitValue(Convert.ToDateTime("1900/01/01"))
            FI_LastEndDate.SetInitValue(Convert.ToDateTime("1900/01/01"))
            FI_ThisEndDate.SetInitValue(Convert.ToDateTime("1900/01/01"))
            FI_MonEndDate.SetInitValue(Convert.ToDateTime("1900/01/01"))
            FI_NextDateDiff.SetInitValue(0)
            FI_NeNeDateDiff.SetInitValue(0)
            FI_LastDateDiff.SetInitValue(0)
            FI_JulianDate.SetInitValue(0)
            FI_LastChgComp.SetInitValue("")
            FI_LastChgID.SetInitValue("")
            FI_LastChgDate.SetInitValue(Convert.ToDateTime("1900/01/01"))
        End Sub

        Public Sub New(ByVal dr As System.Data.DataRow)
            FI_CompID.SetInitValue(dr("CompID"))
            FI_SysDate.SetInitValue(dr("SysDate"))
            FI_HolidayOrNot.SetInitValue(dr("HolidayOrNot"))
            FI_Week.SetInitValue(dr("Week"))
            FI_NextBusDate.SetInitValue(dr("NextBusDate"))
            FI_LastBusDate.SetInitValue(dr("LastBusDate"))
            FI_NeNeBusDate.SetInitValue(dr("NeNeBusDate"))
            FI_LastEndDate.SetInitValue(dr("LastEndDate"))
            FI_ThisEndDate.SetInitValue(dr("ThisEndDate"))
            FI_MonEndDate.SetInitValue(dr("MonEndDate"))
            FI_NextDateDiff.SetInitValue(dr("NextDateDiff"))
            FI_NeNeDateDiff.SetInitValue(dr("NeNeDateDiff"))
            FI_LastDateDiff.SetInitValue(dr("LastDateDiff"))
            FI_JulianDate.SetInitValue(dr("JulianDate"))
            FI_LastChgComp.SetInitValue(dr("LastChgComp"))
            FI_LastChgID.SetInitValue(dr("LastChgID"))
            FI_LastChgDate.SetInitValue(dr("LastChgDate"))

            m_LoadFromDataRow = True
        End Sub

        Private Sub ClearUpdated()
            FI_CompID.Updated = False
            FI_SysDate.Updated = False
            FI_HolidayOrNot.Updated = False
            FI_Week.Updated = False
            FI_NextBusDate.Updated = False
            FI_LastBusDate.Updated = False
            FI_NeNeBusDate.Updated = False
            FI_LastEndDate.Updated = False
            FI_ThisEndDate.Updated = False
            FI_MonEndDate.Updated = False
            FI_NextDateDiff.Updated = False
            FI_NeNeDateDiff.Updated = False
            FI_LastDateDiff.Updated = False
            FI_JulianDate.Updated = False
            FI_LastChgComp.Updated = False
            FI_LastChgID.Updated = False
            FI_LastChgDate.Updated = False
        End Sub

        Public ReadOnly Property CompID As Field(Of String) 
            Get
                Return FI_CompID
            End Get
        End Property

        Public ReadOnly Property SysDate As Field(Of Date) 
            Get
                Return FI_SysDate
            End Get
        End Property

        Public ReadOnly Property HolidayOrNot As Field(Of String) 
            Get
                Return FI_HolidayOrNot
            End Get
        End Property

        Public ReadOnly Property Week As Field(Of String) 
            Get
                Return FI_Week
            End Get
        End Property

        Public ReadOnly Property NextBusDate As Field(Of Date) 
            Get
                Return FI_NextBusDate
            End Get
        End Property

        Public ReadOnly Property LastBusDate As Field(Of Date) 
            Get
                Return FI_LastBusDate
            End Get
        End Property

        Public ReadOnly Property NeNeBusDate As Field(Of Date) 
            Get
                Return FI_NeNeBusDate
            End Get
        End Property

        Public ReadOnly Property LastEndDate As Field(Of Date) 
            Get
                Return FI_LastEndDate
            End Get
        End Property

        Public ReadOnly Property ThisEndDate As Field(Of Date) 
            Get
                Return FI_ThisEndDate
            End Get
        End Property

        Public ReadOnly Property MonEndDate As Field(Of Date) 
            Get
                Return FI_MonEndDate
            End Get
        End Property

        Public ReadOnly Property NextDateDiff As Field(Of Integer) 
            Get
                Return FI_NextDateDiff
            End Get
        End Property

        Public ReadOnly Property NeNeDateDiff As Field(Of Integer) 
            Get
                Return FI_NeNeDateDiff
            End Get
        End Property

        Public ReadOnly Property LastDateDiff As Field(Of Integer) 
            Get
                Return FI_LastDateDiff
            End Get
        End Property

        Public ReadOnly Property JulianDate As Field(Of Integer) 
            Get
                Return FI_JulianDate
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
        Public Function DeleteRowByPrimaryKey(ByVal CalendarRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Delete From Calendar")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And SysDate = @SysDate")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, CalendarRow.CompID.Value)
            db.AddInParameter(dbcmd, "@SysDate", DbType.Date, CalendarRow.SysDate.Value)

            return db.ExecuteNonQuery(dbcmd)
        End Function

        public Function DeleteRowByPrimaryKey(ByVal CalendarRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Delete From Calendar")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And SysDate = @SysDate")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, CalendarRow.CompID.Value)
            db.AddInParameter(dbcmd, "@SysDate", DbType.Date, CalendarRow.SysDate.Value)

            return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function DeleteRowByPrimaryKey(ByVal CalendarRow As Row()) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Delete From Calendar")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And SysDate = @SysDate")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            using cn As DbConnection = db.CreateConnection()
                cn.Open()
                Dim tran As DbTransaction = cn.BeginTransaction()
                Dim inTrans As Boolean = True

                Try
                    For Each r As Row In CalendarRow
                        dbcmd.Parameters.Clear()
                        db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                        db.AddInParameter(dbcmd, "@SysDate", DbType.Date, r.SysDate.Value)

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

        Public Function DeleteRowByPrimaryKey(ByVal CalendarRow As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Delete From Calendar")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And SysDate = @SysDate")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())

            For Each r As Row In CalendarRow
                dbcmd.Parameters.Clear()
                db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                db.AddInParameter(dbcmd, "@SysDate", DbType.Date, r.SysDate.Value)

                intRowsAffected += db.ExecuteNonQuery(dbcmd, tran)
            Next
            Return intRowsAffected
        End Function

        Public Function QueryByKey(ByVal CalendarRow As Row) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From Calendar")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And SysDate = @SysDate")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, CalendarRow.CompID.Value)
            db.AddInParameter(dbcmd, "@SysDate", DbType.Date, CalendarRow.SysDate.Value)

            Return db.ExecuteDataSet(dbcmd)
        End Function

        Public Function QueryByKey(CalendarRow As Row, tran As DbTransaction) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From Calendar")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And SysDate = @SysDate")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, CalendarRow.CompID.Value)
            db.AddInParameter(dbcmd, "@SysDate", DbType.Date, CalendarRow.SysDate.Value)

            Return db.ExecuteDataSet(dbcmd, tran)
        End Function

        Public Function Update(ByVal CalendarRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim strDot As String = String.Empty

            strSQL.AppendLine("Update Calendar Set")
            For i As Integer = 0 To CalendarRow.FieldNames.Length - 1
                If Not CalendarRow.IsIdentityField(CalendarRow.FieldNames(i)) AndAlso CalendarRow.IsUpdated(CalendarRow.FieldNames(i)) AndAlso CalendarRow.CreateUpdateSQL(CalendarRow.FieldNames(i)) Then
                    strSQL.AppendLine(string.Format("{0}{1} = @{1}", strDot, CalendarRow.FieldNames(i)))
                    strDot = ","
                End If
            Next
            If strDot = String.Empty Then Throw New Exception("未異動資料欄位！")
            strSQL.AppendLine("Where CompID = @PKCompID")
            strSQL.AppendLine("And SysDate = @PKSysDate")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            If CalendarRow.CompID.Updated Then db.AddInParameter(dbcmd, "@CompID", DbType.String, CalendarRow.CompID.Value)
            If CalendarRow.SysDate.Updated Then db.AddInParameter(dbcmd, "@SysDate", DbType.Date, IIf(IsDateTimeNull(CalendarRow.SysDate.Value), Convert.ToDateTime("1900/1/1"), CalendarRow.SysDate.Value))
            If CalendarRow.HolidayOrNot.Updated Then db.AddInParameter(dbcmd, "@HolidayOrNot", DbType.String, CalendarRow.HolidayOrNot.Value)
            If CalendarRow.Week.Updated Then db.AddInParameter(dbcmd, "@Week", DbType.String, CalendarRow.Week.Value)
            If CalendarRow.NextBusDate.Updated Then db.AddInParameter(dbcmd, "@NextBusDate", DbType.Date, IIf(IsDateTimeNull(CalendarRow.NextBusDate.Value), Convert.ToDateTime("1900/1/1"), CalendarRow.NextBusDate.Value))
            If CalendarRow.LastBusDate.Updated Then db.AddInParameter(dbcmd, "@LastBusDate", DbType.Date, IIf(IsDateTimeNull(CalendarRow.LastBusDate.Value), Convert.ToDateTime("1900/1/1"), CalendarRow.LastBusDate.Value))
            If CalendarRow.NeNeBusDate.Updated Then db.AddInParameter(dbcmd, "@NeNeBusDate", DbType.Date, IIf(IsDateTimeNull(CalendarRow.NeNeBusDate.Value), Convert.ToDateTime("1900/1/1"), CalendarRow.NeNeBusDate.Value))
            If CalendarRow.LastEndDate.Updated Then db.AddInParameter(dbcmd, "@LastEndDate", DbType.Date, IIf(IsDateTimeNull(CalendarRow.LastEndDate.Value), Convert.ToDateTime("1900/1/1"), CalendarRow.LastEndDate.Value))
            If CalendarRow.ThisEndDate.Updated Then db.AddInParameter(dbcmd, "@ThisEndDate", DbType.Date, IIf(IsDateTimeNull(CalendarRow.ThisEndDate.Value), Convert.ToDateTime("1900/1/1"), CalendarRow.ThisEndDate.Value))
            If CalendarRow.MonEndDate.Updated Then db.AddInParameter(dbcmd, "@MonEndDate", DbType.Date, IIf(IsDateTimeNull(CalendarRow.MonEndDate.Value), Convert.ToDateTime("1900/1/1"), CalendarRow.MonEndDate.Value))
            If CalendarRow.NextDateDiff.Updated Then db.AddInParameter(dbcmd, "@NextDateDiff", DbType.Int32, CalendarRow.NextDateDiff.Value)
            If CalendarRow.NeNeDateDiff.Updated Then db.AddInParameter(dbcmd, "@NeNeDateDiff", DbType.Int32, CalendarRow.NeNeDateDiff.Value)
            If CalendarRow.LastDateDiff.Updated Then db.AddInParameter(dbcmd, "@LastDateDiff", DbType.Int32, CalendarRow.LastDateDiff.Value)
            If CalendarRow.JulianDate.Updated Then db.AddInParameter(dbcmd, "@JulianDate", DbType.Int32, CalendarRow.JulianDate.Value)
            If CalendarRow.LastChgComp.Updated Then db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, CalendarRow.LastChgComp.Value)
            If CalendarRow.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, CalendarRow.LastChgID.Value)
            If CalendarRow.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(CalendarRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), CalendarRow.LastChgDate.Value))
            db.AddInParameter(dbcmd, "@PKCompID", DbType.String, IIf(CalendarRow.LoadFromDataRow, CalendarRow.CompID.OldValue, CalendarRow.CompID.Value))
            db.AddInParameter(dbcmd, "@PKSysDate", DbType.Date, IIf(CalendarRow.LoadFromDataRow, CalendarRow.SysDate.OldValue, CalendarRow.SysDate.Value))

            Return db.ExecuteNonQuery(dbcmd)
        End Function

        Public Function Update(ByVal CalendarRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim strDot As String = String.Empty

            strSQL.AppendLine("Update Calendar Set")
            For i As Integer = 0 To CalendarRow.FieldNames.Length - 1
                If Not CalendarRow.IsIdentityField(CalendarRow.FieldNames(i)) AndAlso CalendarRow.IsUpdated(CalendarRow.FieldNames(i)) AndAlso CalendarRow.CreateUpdateSQL(CalendarRow.FieldNames(i)) Then
                    strSQL.AppendLine(string.Format("{0}{1} = @{1}", strDot, CalendarRow.FieldNames(i)))
                    strDot = ","
                End If
            Next 
            If strDot = String.Empty Then Throw New Exception("未異動資料欄位！")
            strSQL.AppendLine("Where CompID = @PKCompID")
            strSQL.AppendLine("And SysDate = @PKSysDate")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            If CalendarRow.CompID.Updated Then db.AddInParameter(dbcmd, "@CompID", DbType.String, CalendarRow.CompID.Value)
            If CalendarRow.SysDate.Updated Then db.AddInParameter(dbcmd, "@SysDate", DbType.Date, IIf(IsDateTimeNull(CalendarRow.SysDate.Value), Convert.ToDateTime("1900/1/1"), CalendarRow.SysDate.Value))
            If CalendarRow.HolidayOrNot.Updated Then db.AddInParameter(dbcmd, "@HolidayOrNot", DbType.String, CalendarRow.HolidayOrNot.Value)
            If CalendarRow.Week.Updated Then db.AddInParameter(dbcmd, "@Week", DbType.String, CalendarRow.Week.Value)
            If CalendarRow.NextBusDate.Updated Then db.AddInParameter(dbcmd, "@NextBusDate", DbType.Date, IIf(IsDateTimeNull(CalendarRow.NextBusDate.Value), Convert.ToDateTime("1900/1/1"), CalendarRow.NextBusDate.Value))
            If CalendarRow.LastBusDate.Updated Then db.AddInParameter(dbcmd, "@LastBusDate", DbType.Date, IIf(IsDateTimeNull(CalendarRow.LastBusDate.Value), Convert.ToDateTime("1900/1/1"), CalendarRow.LastBusDate.Value))
            If CalendarRow.NeNeBusDate.Updated Then db.AddInParameter(dbcmd, "@NeNeBusDate", DbType.Date, IIf(IsDateTimeNull(CalendarRow.NeNeBusDate.Value), Convert.ToDateTime("1900/1/1"), CalendarRow.NeNeBusDate.Value))
            If CalendarRow.LastEndDate.Updated Then db.AddInParameter(dbcmd, "@LastEndDate", DbType.Date, IIf(IsDateTimeNull(CalendarRow.LastEndDate.Value), Convert.ToDateTime("1900/1/1"), CalendarRow.LastEndDate.Value))
            If CalendarRow.ThisEndDate.Updated Then db.AddInParameter(dbcmd, "@ThisEndDate", DbType.Date, IIf(IsDateTimeNull(CalendarRow.ThisEndDate.Value), Convert.ToDateTime("1900/1/1"), CalendarRow.ThisEndDate.Value))
            If CalendarRow.MonEndDate.Updated Then db.AddInParameter(dbcmd, "@MonEndDate", DbType.Date, IIf(IsDateTimeNull(CalendarRow.MonEndDate.Value), Convert.ToDateTime("1900/1/1"), CalendarRow.MonEndDate.Value))
            If CalendarRow.NextDateDiff.Updated Then db.AddInParameter(dbcmd, "@NextDateDiff", DbType.Int32, CalendarRow.NextDateDiff.Value)
            If CalendarRow.NeNeDateDiff.Updated Then db.AddInParameter(dbcmd, "@NeNeDateDiff", DbType.Int32, CalendarRow.NeNeDateDiff.Value)
            If CalendarRow.LastDateDiff.Updated Then db.AddInParameter(dbcmd, "@LastDateDiff", DbType.Int32, CalendarRow.LastDateDiff.Value)
            If CalendarRow.JulianDate.Updated Then db.AddInParameter(dbcmd, "@JulianDate", DbType.Int32, CalendarRow.JulianDate.Value)
            If CalendarRow.LastChgComp.Updated Then db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, CalendarRow.LastChgComp.Value)
            If CalendarRow.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, CalendarRow.LastChgID.Value)
            If CalendarRow.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(CalendarRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), CalendarRow.LastChgDate.Value))
            db.AddInParameter(dbcmd, "@PKCompID", DbType.String, IIf(CalendarRow.LoadFromDataRow, CalendarRow.CompID.OldValue, CalendarRow.CompID.Value))
            db.AddInParameter(dbcmd, "@PKSysDate", DbType.Date, IIf(CalendarRow.LoadFromDataRow, CalendarRow.SysDate.OldValue, CalendarRow.SysDate.Value))

            Return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function Update(ByVal CalendarRow As Row()) As Integer
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
                    For Each r As Row In CalendarRow
                        strDot = String.Empty
                        If strSQL.Length > 0 Then strSQL.Remove(0, strSQL.Length)
                        strSQL.AppendLine("Update Calendar Set")
                        For i As Integer = 0 To r.FieldNames.Length - 1
                            If Not r.IsIdentityField(r.FieldNames(i)) AndAlso r.IsUpdated(r.FieldNames(i)) AndAlso r.CreateUpdateSQL(r.FieldNames(i)) Then
                                strSQL.AppendLine(String.Format("{0}{1} = @{1}", strDot, r.FieldNames(i)))
                                strDot = ","
                            End If
                        Next
                        If strDot = String.Empty Then Continue For
                        strSQL.AppendLine("Where CompID = @PKCompID")
                        strSQL.AppendLine("And SysDate = @PKSysDate")

                        dbcmd = db.GetSqlStringCommand(strSQL.ToString())
                        If r.CompID.Updated Then db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                        If r.SysDate.Updated Then db.AddInParameter(dbcmd, "@SysDate", DbType.Date, IIf(IsDateTimeNull(r.SysDate.Value), Convert.ToDateTime("1900/1/1"), r.SysDate.Value))
                        If r.HolidayOrNot.Updated Then db.AddInParameter(dbcmd, "@HolidayOrNot", DbType.String, r.HolidayOrNot.Value)
                        If r.Week.Updated Then db.AddInParameter(dbcmd, "@Week", DbType.String, r.Week.Value)
                        If r.NextBusDate.Updated Then db.AddInParameter(dbcmd, "@NextBusDate", DbType.Date, IIf(IsDateTimeNull(r.NextBusDate.Value), Convert.ToDateTime("1900/1/1"), r.NextBusDate.Value))
                        If r.LastBusDate.Updated Then db.AddInParameter(dbcmd, "@LastBusDate", DbType.Date, IIf(IsDateTimeNull(r.LastBusDate.Value), Convert.ToDateTime("1900/1/1"), r.LastBusDate.Value))
                        If r.NeNeBusDate.Updated Then db.AddInParameter(dbcmd, "@NeNeBusDate", DbType.Date, IIf(IsDateTimeNull(r.NeNeBusDate.Value), Convert.ToDateTime("1900/1/1"), r.NeNeBusDate.Value))
                        If r.LastEndDate.Updated Then db.AddInParameter(dbcmd, "@LastEndDate", DbType.Date, IIf(IsDateTimeNull(r.LastEndDate.Value), Convert.ToDateTime("1900/1/1"), r.LastEndDate.Value))
                        If r.ThisEndDate.Updated Then db.AddInParameter(dbcmd, "@ThisEndDate", DbType.Date, IIf(IsDateTimeNull(r.ThisEndDate.Value), Convert.ToDateTime("1900/1/1"), r.ThisEndDate.Value))
                        If r.MonEndDate.Updated Then db.AddInParameter(dbcmd, "@MonEndDate", DbType.Date, IIf(IsDateTimeNull(r.MonEndDate.Value), Convert.ToDateTime("1900/1/1"), r.MonEndDate.Value))
                        If r.NextDateDiff.Updated Then db.AddInParameter(dbcmd, "@NextDateDiff", DbType.Int32, r.NextDateDiff.Value)
                        If r.NeNeDateDiff.Updated Then db.AddInParameter(dbcmd, "@NeNeDateDiff", DbType.Int32, r.NeNeDateDiff.Value)
                        If r.LastDateDiff.Updated Then db.AddInParameter(dbcmd, "@LastDateDiff", DbType.Int32, r.LastDateDiff.Value)
                        If r.JulianDate.Updated Then db.AddInParameter(dbcmd, "@JulianDate", DbType.Int32, r.JulianDate.Value)
                        If r.LastChgComp.Updated Then db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, r.LastChgComp.Value)
                        If r.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, r.LastChgID.Value)
                        If r.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(r.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), r.LastChgDate.Value))
                        db.AddInParameter(dbcmd, "@PKCompID", DbType.String, IIf(r.LoadFromDataRow, r.CompID.OldValue, r.CompID.Value))
                        db.AddInParameter(dbcmd, "@PKSysDate", DbType.Date, IIf(r.LoadFromDataRow, r.SysDate.OldValue, r.SysDate.Value))

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

        Public Function Update(ByVal CalendarRow As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0
            Dim strDot As String

            For Each r As Row In CalendarRow
                strDot = String.Empty
                If strSQL.Length > 0 Then strSQL.Remove(0, strSQL.Length)
                strSQL.AppendLine("Update Calendar Set")
                For i As Integer = 0 To r.FieldNames.Length - 1
                    If Not r.IsIdentityField(r.FieldNames(i)) AndAlso r.IsUpdated(r.FieldNames(i)) AndAlso r.CreateUpdateSQL(r.FieldNames(i))
                        strSQL.AppendLine(String.Format("{0}{1} = @{1}", strDot, r.FieldNames(i)))
                        strDot = ","
                    End If
                Next
                If strDot = String.Empty Then Continue For
                strSQL.AppendLine("Where CompID = @PKCompID")
                strSQL.AppendLine("And SysDate = @PKSysDate")

                dbcmd = db.GetSqlStringCommand(strSQL.ToString())
                If r.CompID.Updated Then db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                If r.SysDate.Updated Then db.AddInParameter(dbcmd, "@SysDate", DbType.Date, IIf(IsDateTimeNull(r.SysDate.Value), Convert.ToDateTime("1900/1/1"), r.SysDate.Value))
                If r.HolidayOrNot.Updated Then db.AddInParameter(dbcmd, "@HolidayOrNot", DbType.String, r.HolidayOrNot.Value)
                If r.Week.Updated Then db.AddInParameter(dbcmd, "@Week", DbType.String, r.Week.Value)
                If r.NextBusDate.Updated Then db.AddInParameter(dbcmd, "@NextBusDate", DbType.Date, IIf(IsDateTimeNull(r.NextBusDate.Value), Convert.ToDateTime("1900/1/1"), r.NextBusDate.Value))
                If r.LastBusDate.Updated Then db.AddInParameter(dbcmd, "@LastBusDate", DbType.Date, IIf(IsDateTimeNull(r.LastBusDate.Value), Convert.ToDateTime("1900/1/1"), r.LastBusDate.Value))
                If r.NeNeBusDate.Updated Then db.AddInParameter(dbcmd, "@NeNeBusDate", DbType.Date, IIf(IsDateTimeNull(r.NeNeBusDate.Value), Convert.ToDateTime("1900/1/1"), r.NeNeBusDate.Value))
                If r.LastEndDate.Updated Then db.AddInParameter(dbcmd, "@LastEndDate", DbType.Date, IIf(IsDateTimeNull(r.LastEndDate.Value), Convert.ToDateTime("1900/1/1"), r.LastEndDate.Value))
                If r.ThisEndDate.Updated Then db.AddInParameter(dbcmd, "@ThisEndDate", DbType.Date, IIf(IsDateTimeNull(r.ThisEndDate.Value), Convert.ToDateTime("1900/1/1"), r.ThisEndDate.Value))
                If r.MonEndDate.Updated Then db.AddInParameter(dbcmd, "@MonEndDate", DbType.Date, IIf(IsDateTimeNull(r.MonEndDate.Value), Convert.ToDateTime("1900/1/1"), r.MonEndDate.Value))
                If r.NextDateDiff.Updated Then db.AddInParameter(dbcmd, "@NextDateDiff", DbType.Int32, r.NextDateDiff.Value)
                If r.NeNeDateDiff.Updated Then db.AddInParameter(dbcmd, "@NeNeDateDiff", DbType.Int32, r.NeNeDateDiff.Value)
                If r.LastDateDiff.Updated Then db.AddInParameter(dbcmd, "@LastDateDiff", DbType.Int32, r.LastDateDiff.Value)
                If r.JulianDate.Updated Then db.AddInParameter(dbcmd, "@JulianDate", DbType.Int32, r.JulianDate.Value)
                If r.LastChgComp.Updated Then db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, r.LastChgComp.Value)
                If r.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, r.LastChgID.Value)
                If r.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(r.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), r.LastChgDate.Value))
                db.AddInParameter(dbcmd, "@PKCompID", DbType.String, IIf(r.LoadFromDataRow, r.CompID.OldValue, r.CompID.Value))
                db.AddInParameter(dbcmd, "@PKSysDate", DbType.Date, IIf(r.LoadFromDataRow, r.SysDate.OldValue, r.SysDate.Value))

                intRowsAffected += db.ExecuteNonQuery(dbcmd, tran)
            Next
            Return intRowsAffected
        End Function

        Public Function IsDataExists(ByVal CalendarRow As Row) As Boolean
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intCount As Integer = 0

            strSQL.AppendLine("Select Count(*) Cnt From Calendar")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And SysDate = @SysDate")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, CalendarRow.CompID.Value)
            db.AddInParameter(dbcmd, "@SysDate", DbType.Date, CalendarRow.SysDate.Value)

            intCount = Convert.ToInt32(db.ExecuteScalar(dbcmd))
            Return IIf(intCount > 0, True, False)
        End Function

        Public Function IsDataExists(ByVal CalendarRow As Row, ByVal tran As DbTransaction) As Boolean
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intCount As Integer = 0

            strSQL.AppendLine("Select Count(*) Cnt From Calendar")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And SysDate = @SysDate")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, CalendarRow.CompID.Value)
            db.AddInParameter(dbcmd, "@SysDate", DbType.Date, CalendarRow.SysDate.Value)

            intCount = Convert.ToInt32(db.ExecuteScalar(dbcmd, tran))
            Return IIf(intCount > 0, True, False)
        End Function

        Public Function QuerybyWhere(WhereCondition As String) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From Calendar")
            If WhereCondition <> String.Empty Then strSQL.AppendLine(WhereCondition)

            Return db.ExecuteDataSet(CommandType.Text, strSQL.ToString())
        End Function

        Public Function Insert(ByVal CalendarRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Insert into Calendar")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    CompID, SysDate, HolidayOrNot, Week, NextBusDate, LastBusDate, NeNeBusDate, LastEndDate,")
            strSQL.AppendLine("    ThisEndDate, MonEndDate, NextDateDiff, NeNeDateDiff, LastDateDiff, JulianDate, LastChgComp,")
            strSQL.AppendLine("    LastChgID, LastChgDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @CompID, @SysDate, @HolidayOrNot, @Week, @NextBusDate, @LastBusDate, @NeNeBusDate, @LastEndDate,")
            strSQL.AppendLine("    @ThisEndDate, @MonEndDate, @NextDateDiff, @NeNeDateDiff, @LastDateDiff, @JulianDate, @LastChgComp,")
            strSQL.AppendLine("    @LastChgID, @LastChgDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, CalendarRow.CompID.Value)
            db.AddInParameter(dbcmd, "@SysDate", DbType.Date, IIf(IsDateTimeNull(CalendarRow.SysDate.Value), Convert.ToDateTime("1900/1/1"), CalendarRow.SysDate.Value))
            db.AddInParameter(dbcmd, "@HolidayOrNot", DbType.String, CalendarRow.HolidayOrNot.Value)
            db.AddInParameter(dbcmd, "@Week", DbType.String, CalendarRow.Week.Value)
            db.AddInParameter(dbcmd, "@NextBusDate", DbType.Date, IIf(IsDateTimeNull(CalendarRow.NextBusDate.Value), Convert.ToDateTime("1900/1/1"), CalendarRow.NextBusDate.Value))
            db.AddInParameter(dbcmd, "@LastBusDate", DbType.Date, IIf(IsDateTimeNull(CalendarRow.LastBusDate.Value), Convert.ToDateTime("1900/1/1"), CalendarRow.LastBusDate.Value))
            db.AddInParameter(dbcmd, "@NeNeBusDate", DbType.Date, IIf(IsDateTimeNull(CalendarRow.NeNeBusDate.Value), Convert.ToDateTime("1900/1/1"), CalendarRow.NeNeBusDate.Value))
            db.AddInParameter(dbcmd, "@LastEndDate", DbType.Date, IIf(IsDateTimeNull(CalendarRow.LastEndDate.Value), Convert.ToDateTime("1900/1/1"), CalendarRow.LastEndDate.Value))
            db.AddInParameter(dbcmd, "@ThisEndDate", DbType.Date, IIf(IsDateTimeNull(CalendarRow.ThisEndDate.Value), Convert.ToDateTime("1900/1/1"), CalendarRow.ThisEndDate.Value))
            db.AddInParameter(dbcmd, "@MonEndDate", DbType.Date, IIf(IsDateTimeNull(CalendarRow.MonEndDate.Value), Convert.ToDateTime("1900/1/1"), CalendarRow.MonEndDate.Value))
            db.AddInParameter(dbcmd, "@NextDateDiff", DbType.Int32, CalendarRow.NextDateDiff.Value)
            db.AddInParameter(dbcmd, "@NeNeDateDiff", DbType.Int32, CalendarRow.NeNeDateDiff.Value)
            db.AddInParameter(dbcmd, "@LastDateDiff", DbType.Int32, CalendarRow.LastDateDiff.Value)
            db.AddInParameter(dbcmd, "@JulianDate", DbType.Int32, CalendarRow.JulianDate.Value)
            db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, CalendarRow.LastChgComp.Value)
            db.AddInParameter(dbcmd, "@LastChgID", DbType.String, CalendarRow.LastChgID.Value)
            db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(CalendarRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), CalendarRow.LastChgDate.Value))

            Return db.ExecuteNonQuery(dbcmd)
        End Function

        Public Function Insert(ByVal CalendarRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Insert into Calendar")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    CompID, SysDate, HolidayOrNot, Week, NextBusDate, LastBusDate, NeNeBusDate, LastEndDate,")
            strSQL.AppendLine("    ThisEndDate, MonEndDate, NextDateDiff, NeNeDateDiff, LastDateDiff, JulianDate, LastChgComp,")
            strSQL.AppendLine("    LastChgID, LastChgDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @CompID, @SysDate, @HolidayOrNot, @Week, @NextBusDate, @LastBusDate, @NeNeBusDate, @LastEndDate,")
            strSQL.AppendLine("    @ThisEndDate, @MonEndDate, @NextDateDiff, @NeNeDateDiff, @LastDateDiff, @JulianDate, @LastChgComp,")
            strSQL.AppendLine("    @LastChgID, @LastChgDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, CalendarRow.CompID.Value)
            db.AddInParameter(dbcmd, "@SysDate", DbType.Date, IIf(IsDateTimeNull(CalendarRow.SysDate.Value), Convert.ToDateTime("1900/1/1"), CalendarRow.SysDate.Value))
            db.AddInParameter(dbcmd, "@HolidayOrNot", DbType.String, CalendarRow.HolidayOrNot.Value)
            db.AddInParameter(dbcmd, "@Week", DbType.String, CalendarRow.Week.Value)
            db.AddInParameter(dbcmd, "@NextBusDate", DbType.Date, IIf(IsDateTimeNull(CalendarRow.NextBusDate.Value), Convert.ToDateTime("1900/1/1"), CalendarRow.NextBusDate.Value))
            db.AddInParameter(dbcmd, "@LastBusDate", DbType.Date, IIf(IsDateTimeNull(CalendarRow.LastBusDate.Value), Convert.ToDateTime("1900/1/1"), CalendarRow.LastBusDate.Value))
            db.AddInParameter(dbcmd, "@NeNeBusDate", DbType.Date, IIf(IsDateTimeNull(CalendarRow.NeNeBusDate.Value), Convert.ToDateTime("1900/1/1"), CalendarRow.NeNeBusDate.Value))
            db.AddInParameter(dbcmd, "@LastEndDate", DbType.Date, IIf(IsDateTimeNull(CalendarRow.LastEndDate.Value), Convert.ToDateTime("1900/1/1"), CalendarRow.LastEndDate.Value))
            db.AddInParameter(dbcmd, "@ThisEndDate", DbType.Date, IIf(IsDateTimeNull(CalendarRow.ThisEndDate.Value), Convert.ToDateTime("1900/1/1"), CalendarRow.ThisEndDate.Value))
            db.AddInParameter(dbcmd, "@MonEndDate", DbType.Date, IIf(IsDateTimeNull(CalendarRow.MonEndDate.Value), Convert.ToDateTime("1900/1/1"), CalendarRow.MonEndDate.Value))
            db.AddInParameter(dbcmd, "@NextDateDiff", DbType.Int32, CalendarRow.NextDateDiff.Value)
            db.AddInParameter(dbcmd, "@NeNeDateDiff", DbType.Int32, CalendarRow.NeNeDateDiff.Value)
            db.AddInParameter(dbcmd, "@LastDateDiff", DbType.Int32, CalendarRow.LastDateDiff.Value)
            db.AddInParameter(dbcmd, "@JulianDate", DbType.Int32, CalendarRow.JulianDate.Value)
            db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, CalendarRow.LastChgComp.Value)
            db.AddInParameter(dbcmd, "@LastChgID", DbType.String, CalendarRow.LastChgID.Value)
            db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(CalendarRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), CalendarRow.LastChgDate.Value))

            Return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function Insert(ByVal CalendarRow As Row()) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Insert into Calendar")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    CompID, SysDate, HolidayOrNot, Week, NextBusDate, LastBusDate, NeNeBusDate, LastEndDate,")
            strSQL.AppendLine("    ThisEndDate, MonEndDate, NextDateDiff, NeNeDateDiff, LastDateDiff, JulianDate, LastChgComp,")
            strSQL.AppendLine("    LastChgID, LastChgDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @CompID, @SysDate, @HolidayOrNot, @Week, @NextBusDate, @LastBusDate, @NeNeBusDate, @LastEndDate,")
            strSQL.AppendLine("    @ThisEndDate, @MonEndDate, @NextDateDiff, @NeNeDateDiff, @LastDateDiff, @JulianDate, @LastChgComp,")
            strSQL.AppendLine("    @LastChgID, @LastChgDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            Using cn As DbConnection = db.CreateConnection()
                cn.Open()
                Dim tran As DbTransaction = cn.BeginTransaction()
                Dim inTrans As Boolean = True

                Try
                    For Each r As Row In CalendarRow
                        dbcmd.Parameters.Clear()
                        db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                        db.AddInParameter(dbcmd, "@SysDate", DbType.Date, IIf(IsDateTimeNull(r.SysDate.Value), Convert.ToDateTime("1900/1/1"), r.SysDate.Value))
                        db.AddInParameter(dbcmd, "@HolidayOrNot", DbType.String, r.HolidayOrNot.Value)
                        db.AddInParameter(dbcmd, "@Week", DbType.String, r.Week.Value)
                        db.AddInParameter(dbcmd, "@NextBusDate", DbType.Date, IIf(IsDateTimeNull(r.NextBusDate.Value), Convert.ToDateTime("1900/1/1"), r.NextBusDate.Value))
                        db.AddInParameter(dbcmd, "@LastBusDate", DbType.Date, IIf(IsDateTimeNull(r.LastBusDate.Value), Convert.ToDateTime("1900/1/1"), r.LastBusDate.Value))
                        db.AddInParameter(dbcmd, "@NeNeBusDate", DbType.Date, IIf(IsDateTimeNull(r.NeNeBusDate.Value), Convert.ToDateTime("1900/1/1"), r.NeNeBusDate.Value))
                        db.AddInParameter(dbcmd, "@LastEndDate", DbType.Date, IIf(IsDateTimeNull(r.LastEndDate.Value), Convert.ToDateTime("1900/1/1"), r.LastEndDate.Value))
                        db.AddInParameter(dbcmd, "@ThisEndDate", DbType.Date, IIf(IsDateTimeNull(r.ThisEndDate.Value), Convert.ToDateTime("1900/1/1"), r.ThisEndDate.Value))
                        db.AddInParameter(dbcmd, "@MonEndDate", DbType.Date, IIf(IsDateTimeNull(r.MonEndDate.Value), Convert.ToDateTime("1900/1/1"), r.MonEndDate.Value))
                        db.AddInParameter(dbcmd, "@NextDateDiff", DbType.Int32, r.NextDateDiff.Value)
                        db.AddInParameter(dbcmd, "@NeNeDateDiff", DbType.Int32, r.NeNeDateDiff.Value)
                        db.AddInParameter(dbcmd, "@LastDateDiff", DbType.Int32, r.LastDateDiff.Value)
                        db.AddInParameter(dbcmd, "@JulianDate", DbType.Int32, r.JulianDate.Value)
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

        Public Function Insert(ByVal CalendarRow As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Insert into Calendar")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    CompID, SysDate, HolidayOrNot, Week, NextBusDate, LastBusDate, NeNeBusDate, LastEndDate,")
            strSQL.AppendLine("    ThisEndDate, MonEndDate, NextDateDiff, NeNeDateDiff, LastDateDiff, JulianDate, LastChgComp,")
            strSQL.AppendLine("    LastChgID, LastChgDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @CompID, @SysDate, @HolidayOrNot, @Week, @NextBusDate, @LastBusDate, @NeNeBusDate, @LastEndDate,")
            strSQL.AppendLine("    @ThisEndDate, @MonEndDate, @NextDateDiff, @NeNeDateDiff, @LastDateDiff, @JulianDate, @LastChgComp,")
            strSQL.AppendLine("    @LastChgID, @LastChgDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())

            For Each r As Row In CalendarRow
                dbcmd.Parameters.Clear()
                db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                db.AddInParameter(dbcmd, "@SysDate", DbType.Date, IIf(IsDateTimeNull(r.SysDate.Value), Convert.ToDateTime("1900/1/1"), r.SysDate.Value))
                db.AddInParameter(dbcmd, "@HolidayOrNot", DbType.String, r.HolidayOrNot.Value)
                db.AddInParameter(dbcmd, "@Week", DbType.String, r.Week.Value)
                db.AddInParameter(dbcmd, "@NextBusDate", DbType.Date, IIf(IsDateTimeNull(r.NextBusDate.Value), Convert.ToDateTime("1900/1/1"), r.NextBusDate.Value))
                db.AddInParameter(dbcmd, "@LastBusDate", DbType.Date, IIf(IsDateTimeNull(r.LastBusDate.Value), Convert.ToDateTime("1900/1/1"), r.LastBusDate.Value))
                db.AddInParameter(dbcmd, "@NeNeBusDate", DbType.Date, IIf(IsDateTimeNull(r.NeNeBusDate.Value), Convert.ToDateTime("1900/1/1"), r.NeNeBusDate.Value))
                db.AddInParameter(dbcmd, "@LastEndDate", DbType.Date, IIf(IsDateTimeNull(r.LastEndDate.Value), Convert.ToDateTime("1900/1/1"), r.LastEndDate.Value))
                db.AddInParameter(dbcmd, "@ThisEndDate", DbType.Date, IIf(IsDateTimeNull(r.ThisEndDate.Value), Convert.ToDateTime("1900/1/1"), r.ThisEndDate.Value))
                db.AddInParameter(dbcmd, "@MonEndDate", DbType.Date, IIf(IsDateTimeNull(r.MonEndDate.Value), Convert.ToDateTime("1900/1/1"), r.MonEndDate.Value))
                db.AddInParameter(dbcmd, "@NextDateDiff", DbType.Int32, r.NextDateDiff.Value)
                db.AddInParameter(dbcmd, "@NeNeDateDiff", DbType.Int32, r.NeNeDateDiff.Value)
                db.AddInParameter(dbcmd, "@LastDateDiff", DbType.Int32, r.LastDateDiff.Value)
                db.AddInParameter(dbcmd, "@JulianDate", DbType.Int32, r.JulianDate.Value)
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

