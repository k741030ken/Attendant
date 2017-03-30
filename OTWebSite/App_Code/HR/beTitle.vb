'****************************************************************
' Table:Title
' Created Date: 2015.04.27
'****************************************************************/
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports Microsoft.Practices.EnterpriseLibrary.Common
Imports Microsoft.Practices.EnterpriseLibrary.Data
Imports System.Data.Common
Imports System.Data

Namespace beTitle
    Public Class Table
        Private m_Rows As Rows = New Rows()
        Private m_Fields As String() = { "CompID", "RankID", "TitleID", "TitleName", "TitleEngName", "LastChgComp", "LastChgID", "LastChgDate" }
        Private m_Types As System.Type() = { GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(Date) }
        Private m_PrimaryFields As String() = { "CompID", "RankID", "TitleID" }

        Public ReadOnly Property Rows() As beTitle.Rows 
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
        Public Sub Transfer2Row(TitleTable As DataTable)
            For Each dr As DataRow In TitleTable.Rows
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
                dr(m_Rows(i).TitleID.FieldName) = m_Rows(i).TitleID.Value
                dr(m_Rows(i).TitleName.FieldName) = m_Rows(i).TitleName.Value
                dr(m_Rows(i).TitleEngName.FieldName) = m_Rows(i).TitleEngName.Value
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

        Public Sub Add(TitleRow As Row)
            m_Rows.Add(TitleRow)
        End Sub

        Public Sub Remove(TitleRow As Row)
            If m_Rows.IndexOf(TitleRow) >= 0 Then
                m_Rows.Remove(TitleRow)
            End If
        End Sub

        Public Sub Dispose()
            m_Rows.Clear()
        End Sub

    End Class

    Public Class Row

        Private FI_CompID As Field(Of String) = new Field(Of String)("CompID", true)
        Private FI_RankID As Field(Of String) = new Field(Of String)("RankID", true)
        Private FI_TitleID As Field(Of String) = new Field(Of String)("TitleID", true)
        Private FI_TitleName As Field(Of String) = new Field(Of String)("TitleName", true)
        Private FI_TitleEngName As Field(Of String) = new Field(Of String)("TitleEngName", true)
        Private FI_LastChgComp As Field(Of String) = new Field(Of String)("LastChgComp", true)
        Private FI_LastChgID As Field(Of String) = new Field(Of String)("LastChgID", true)
        Private FI_LastChgDate As Field(Of Date) = new Field(Of Date)("LastChgDate", true)
        Private m_FieldNames As String() = { "CompID", "RankID", "TitleID", "TitleName", "TitleEngName", "LastChgComp", "LastChgID", "LastChgDate" }
        Private m_PrimaryFields As String() = { "CompID", "RankID", "TitleID" }
        Private m_IdentityFields As String() = {  }
        Private m_LoadFromDataRow As Boolean = False

        Private Function GetFieldValue(ByVal fieldName As String) As Object
            Select Case fieldName
                Case "CompID"
                    Return FI_CompID.Value
                Case "RankID"
                    Return FI_RankID.Value
                Case "TitleID"
                    Return FI_TitleID.Value
                Case "TitleName"
                    Return FI_TitleName.Value
                Case "TitleEngName"
                    Return FI_TitleEngName.Value
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
                Case "RankID"
                    FI_RankID.SetValue(value)
                Case "TitleID"
                    FI_TitleID.SetValue(value)
                Case "TitleName"
                    FI_TitleName.SetValue(value)
                Case "TitleEngName"
                    FI_TitleEngName.SetValue(value)
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
                Case "RankID"
                    return FI_RankID.Updated
                Case "TitleID"
                    return FI_TitleID.Updated
                Case "TitleName"
                    return FI_TitleName.Updated
                Case "TitleEngName"
                    return FI_TitleEngName.Updated
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
                Case "RankID"
                    return FI_RankID.CreateUpdateSQL
                Case "TitleID"
                    return FI_TitleID.CreateUpdateSQL
                Case "TitleName"
                    return FI_TitleName.CreateUpdateSQL
                Case "TitleEngName"
                    return FI_TitleEngName.CreateUpdateSQL
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
            FI_RankID.SetInitValue("")
            FI_TitleID.SetInitValue("")
            FI_TitleName.SetInitValue("")
            FI_TitleEngName.SetInitValue("")
            FI_LastChgComp.SetInitValue("")
            FI_LastChgID.SetInitValue("")
            FI_LastChgDate.SetInitValue(Convert.ToDateTime("1900/01/01"))
        End Sub

        Public Sub New(ByVal dr As System.Data.DataRow)
            FI_CompID.SetInitValue(dr("CompID"))
            FI_RankID.SetInitValue(dr("RankID"))
            FI_TitleID.SetInitValue(dr("TitleID"))
            FI_TitleName.SetInitValue(dr("TitleName"))
            FI_TitleEngName.SetInitValue(dr("TitleEngName"))
            FI_LastChgComp.SetInitValue(dr("LastChgComp"))
            FI_LastChgID.SetInitValue(dr("LastChgID"))
            FI_LastChgDate.SetInitValue(dr("LastChgDate"))

            m_LoadFromDataRow = True
        End Sub

        Private Sub ClearUpdated()
            FI_CompID.Updated = False
            FI_RankID.Updated = False
            FI_TitleID.Updated = False
            FI_TitleName.Updated = False
            FI_TitleEngName.Updated = False
            FI_LastChgComp.Updated = False
            FI_LastChgID.Updated = False
            FI_LastChgDate.Updated = False
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

        Public ReadOnly Property TitleID As Field(Of String) 
            Get
                Return FI_TitleID
            End Get
        End Property

        Public ReadOnly Property TitleName As Field(Of String) 
            Get
                Return FI_TitleName
            End Get
        End Property

        Public ReadOnly Property TitleEngName As Field(Of String) 
            Get
                Return FI_TitleEngName
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
        Public Function DeleteRowByPrimaryKey(ByVal TitleRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Delete From Title")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And RankID = @RankID")
            strSQL.AppendLine("And TitleID = @TitleID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, TitleRow.CompID.Value)
            db.AddInParameter(dbcmd, "@RankID", DbType.String, TitleRow.RankID.Value)
            db.AddInParameter(dbcmd, "@TitleID", DbType.String, TitleRow.TitleID.Value)

            return db.ExecuteNonQuery(dbcmd)
        End Function

        public Function DeleteRowByPrimaryKey(ByVal TitleRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Delete From Title")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And RankID = @RankID")
            strSQL.AppendLine("And TitleID = @TitleID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, TitleRow.CompID.Value)
            db.AddInParameter(dbcmd, "@RankID", DbType.String, TitleRow.RankID.Value)
            db.AddInParameter(dbcmd, "@TitleID", DbType.String, TitleRow.TitleID.Value)

            return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function DeleteRowByPrimaryKey(ByVal TitleRow As Row()) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Delete From Title")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And RankID = @RankID")
            strSQL.AppendLine("And TitleID = @TitleID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            using cn As DbConnection = db.CreateConnection()
                cn.Open()
                Dim tran As DbTransaction = cn.BeginTransaction()
                Dim inTrans As Boolean = True

                Try
                    For Each r As Row In TitleRow
                        dbcmd.Parameters.Clear()
                        db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                        db.AddInParameter(dbcmd, "@RankID", DbType.String, r.RankID.Value)
                        db.AddInParameter(dbcmd, "@TitleID", DbType.String, r.TitleID.Value)

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

        Public Function DeleteRowByPrimaryKey(ByVal TitleRow As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Delete From Title")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And RankID = @RankID")
            strSQL.AppendLine("And TitleID = @TitleID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())

            For Each r As Row In TitleRow
                dbcmd.Parameters.Clear()
                db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                db.AddInParameter(dbcmd, "@RankID", DbType.String, r.RankID.Value)
                db.AddInParameter(dbcmd, "@TitleID", DbType.String, r.TitleID.Value)

                intRowsAffected += db.ExecuteNonQuery(dbcmd, tran)
            Next
            Return intRowsAffected
        End Function

        Public Function QueryByKey(ByVal TitleRow As Row) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From Title")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And RankID = @RankID")
            strSQL.AppendLine("And TitleID = @TitleID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, TitleRow.CompID.Value)
            db.AddInParameter(dbcmd, "@RankID", DbType.String, TitleRow.RankID.Value)
            db.AddInParameter(dbcmd, "@TitleID", DbType.String, TitleRow.TitleID.Value)

            Return db.ExecuteDataSet(dbcmd)
        End Function

        Public Function QueryByKey(TitleRow As Row, tran As DbTransaction) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From Title")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And RankID = @RankID")
            strSQL.AppendLine("And TitleID = @TitleID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, TitleRow.CompID.Value)
            db.AddInParameter(dbcmd, "@RankID", DbType.String, TitleRow.RankID.Value)
            db.AddInParameter(dbcmd, "@TitleID", DbType.String, TitleRow.TitleID.Value)

            Return db.ExecuteDataSet(dbcmd, tran)
        End Function

        Public Function Update(ByVal TitleRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim strDot As String = String.Empty

            strSQL.AppendLine("Update Title Set")
            For i As Integer = 0 To TitleRow.FieldNames.Length - 1
                If Not TitleRow.IsIdentityField(TitleRow.FieldNames(i)) AndAlso TitleRow.IsUpdated(TitleRow.FieldNames(i)) AndAlso TitleRow.CreateUpdateSQL(TitleRow.FieldNames(i)) Then
                    strSQL.AppendLine(string.Format("{0}{1} = @{1}", strDot, TitleRow.FieldNames(i)))
                    strDot = ","
                End If
            Next
            If strDot = String.Empty Then Throw New Exception("未異動資料欄位！")
            strSQL.AppendLine("Where CompID = @PKCompID")
            strSQL.AppendLine("And RankID = @PKRankID")
            strSQL.AppendLine("And TitleID = @PKTitleID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            If TitleRow.CompID.Updated Then db.AddInParameter(dbcmd, "@CompID", DbType.String, TitleRow.CompID.Value)
            If TitleRow.RankID.Updated Then db.AddInParameter(dbcmd, "@RankID", DbType.String, TitleRow.RankID.Value)
            If TitleRow.TitleID.Updated Then db.AddInParameter(dbcmd, "@TitleID", DbType.String, TitleRow.TitleID.Value)
            If TitleRow.TitleName.Updated Then db.AddInParameter(dbcmd, "@TitleName", DbType.String, TitleRow.TitleName.Value)
            If TitleRow.TitleEngName.Updated Then db.AddInParameter(dbcmd, "@TitleEngName", DbType.String, TitleRow.TitleEngName.Value)
            If TitleRow.LastChgComp.Updated Then db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, TitleRow.LastChgComp.Value)
            If TitleRow.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, TitleRow.LastChgID.Value)
            If TitleRow.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(TitleRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), TitleRow.LastChgDate.Value))
            db.AddInParameter(dbcmd, "@PKCompID", DbType.String, IIf(TitleRow.LoadFromDataRow, TitleRow.CompID.OldValue, TitleRow.CompID.Value))
            db.AddInParameter(dbcmd, "@PKRankID", DbType.String, IIf(TitleRow.LoadFromDataRow, TitleRow.RankID.OldValue, TitleRow.RankID.Value))
            db.AddInParameter(dbcmd, "@PKTitleID", DbType.String, IIf(TitleRow.LoadFromDataRow, TitleRow.TitleID.OldValue, TitleRow.TitleID.Value))

            Return db.ExecuteNonQuery(dbcmd)
        End Function

        Public Function Update(ByVal TitleRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim strDot As String = String.Empty

            strSQL.AppendLine("Update Title Set")
            For i As Integer = 0 To TitleRow.FieldNames.Length - 1
                If Not TitleRow.IsIdentityField(TitleRow.FieldNames(i)) AndAlso TitleRow.IsUpdated(TitleRow.FieldNames(i)) AndAlso TitleRow.CreateUpdateSQL(TitleRow.FieldNames(i)) Then
                    strSQL.AppendLine(string.Format("{0}{1} = @{1}", strDot, TitleRow.FieldNames(i)))
                    strDot = ","
                End If
            Next 
            If strDot = String.Empty Then Throw New Exception("未異動資料欄位！")
            strSQL.AppendLine("Where CompID = @PKCompID")
            strSQL.AppendLine("And RankID = @PKRankID")
            strSQL.AppendLine("And TitleID = @PKTitleID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            If TitleRow.CompID.Updated Then db.AddInParameter(dbcmd, "@CompID", DbType.String, TitleRow.CompID.Value)
            If TitleRow.RankID.Updated Then db.AddInParameter(dbcmd, "@RankID", DbType.String, TitleRow.RankID.Value)
            If TitleRow.TitleID.Updated Then db.AddInParameter(dbcmd, "@TitleID", DbType.String, TitleRow.TitleID.Value)
            If TitleRow.TitleName.Updated Then db.AddInParameter(dbcmd, "@TitleName", DbType.String, TitleRow.TitleName.Value)
            If TitleRow.TitleEngName.Updated Then db.AddInParameter(dbcmd, "@TitleEngName", DbType.String, TitleRow.TitleEngName.Value)
            If TitleRow.LastChgComp.Updated Then db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, TitleRow.LastChgComp.Value)
            If TitleRow.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, TitleRow.LastChgID.Value)
            If TitleRow.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(TitleRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), TitleRow.LastChgDate.Value))
            db.AddInParameter(dbcmd, "@PKCompID", DbType.String, IIf(TitleRow.LoadFromDataRow, TitleRow.CompID.OldValue, TitleRow.CompID.Value))
            db.AddInParameter(dbcmd, "@PKRankID", DbType.String, IIf(TitleRow.LoadFromDataRow, TitleRow.RankID.OldValue, TitleRow.RankID.Value))
            db.AddInParameter(dbcmd, "@PKTitleID", DbType.String, IIf(TitleRow.LoadFromDataRow, TitleRow.TitleID.OldValue, TitleRow.TitleID.Value))

            Return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function Update(ByVal TitleRow As Row()) As Integer
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
                    For Each r As Row In TitleRow
                        strDot = String.Empty
                        If strSQL.Length > 0 Then strSQL.Remove(0, strSQL.Length)
                        strSQL.AppendLine("Update Title Set")
                        For i As Integer = 0 To r.FieldNames.Length - 1
                            If Not r.IsIdentityField(r.FieldNames(i)) AndAlso r.IsUpdated(r.FieldNames(i)) AndAlso r.CreateUpdateSQL(r.FieldNames(i)) Then
                                strSQL.AppendLine(String.Format("{0}{1} = @{1}", strDot, r.FieldNames(i)))
                                strDot = ","
                            End If
                        Next
                        If strDot = String.Empty Then Continue For
                        strSQL.AppendLine("Where CompID = @PKCompID")
                        strSQL.AppendLine("And RankID = @PKRankID")
                        strSQL.AppendLine("And TitleID = @PKTitleID")

                        dbcmd = db.GetSqlStringCommand(strSQL.ToString())
                        If r.CompID.Updated Then db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                        If r.RankID.Updated Then db.AddInParameter(dbcmd, "@RankID", DbType.String, r.RankID.Value)
                        If r.TitleID.Updated Then db.AddInParameter(dbcmd, "@TitleID", DbType.String, r.TitleID.Value)
                        If r.TitleName.Updated Then db.AddInParameter(dbcmd, "@TitleName", DbType.String, r.TitleName.Value)
                        If r.TitleEngName.Updated Then db.AddInParameter(dbcmd, "@TitleEngName", DbType.String, r.TitleEngName.Value)
                        If r.LastChgComp.Updated Then db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, r.LastChgComp.Value)
                        If r.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, r.LastChgID.Value)
                        If r.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(r.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), r.LastChgDate.Value))
                        db.AddInParameter(dbcmd, "@PKCompID", DbType.String, IIf(r.LoadFromDataRow, r.CompID.OldValue, r.CompID.Value))
                        db.AddInParameter(dbcmd, "@PKRankID", DbType.String, IIf(r.LoadFromDataRow, r.RankID.OldValue, r.RankID.Value))
                        db.AddInParameter(dbcmd, "@PKTitleID", DbType.String, IIf(r.LoadFromDataRow, r.TitleID.OldValue, r.TitleID.Value))

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

        Public Function Update(ByVal TitleRow As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0
            Dim strDot As String

            For Each r As Row In TitleRow
                strDot = String.Empty
                If strSQL.Length > 0 Then strSQL.Remove(0, strSQL.Length)
                strSQL.AppendLine("Update Title Set")
                For i As Integer = 0 To r.FieldNames.Length - 1
                    If Not r.IsIdentityField(r.FieldNames(i)) AndAlso r.IsUpdated(r.FieldNames(i)) AndAlso r.CreateUpdateSQL(r.FieldNames(i))
                        strSQL.AppendLine(String.Format("{0}{1} = @{1}", strDot, r.FieldNames(i)))
                        strDot = ","
                    End If
                Next
                If strDot = String.Empty Then Continue For
                strSQL.AppendLine("Where CompID = @PKCompID")
                strSQL.AppendLine("And RankID = @PKRankID")
                strSQL.AppendLine("And TitleID = @PKTitleID")

                dbcmd = db.GetSqlStringCommand(strSQL.ToString())
                If r.CompID.Updated Then db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                If r.RankID.Updated Then db.AddInParameter(dbcmd, "@RankID", DbType.String, r.RankID.Value)
                If r.TitleID.Updated Then db.AddInParameter(dbcmd, "@TitleID", DbType.String, r.TitleID.Value)
                If r.TitleName.Updated Then db.AddInParameter(dbcmd, "@TitleName", DbType.String, r.TitleName.Value)
                If r.TitleEngName.Updated Then db.AddInParameter(dbcmd, "@TitleEngName", DbType.String, r.TitleEngName.Value)
                If r.LastChgComp.Updated Then db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, r.LastChgComp.Value)
                If r.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, r.LastChgID.Value)
                If r.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(r.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), r.LastChgDate.Value))
                db.AddInParameter(dbcmd, "@PKCompID", DbType.String, IIf(r.LoadFromDataRow, r.CompID.OldValue, r.CompID.Value))
                db.AddInParameter(dbcmd, "@PKRankID", DbType.String, IIf(r.LoadFromDataRow, r.RankID.OldValue, r.RankID.Value))
                db.AddInParameter(dbcmd, "@PKTitleID", DbType.String, IIf(r.LoadFromDataRow, r.TitleID.OldValue, r.TitleID.Value))

                intRowsAffected += db.ExecuteNonQuery(dbcmd, tran)
            Next
            Return intRowsAffected
        End Function

        Public Function IsDataExists(ByVal TitleRow As Row) As Boolean
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intCount As Integer = 0

            strSQL.AppendLine("Select Count(*) Cnt From Title")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And RankID = @RankID")
            strSQL.AppendLine("And TitleID = @TitleID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, TitleRow.CompID.Value)
            db.AddInParameter(dbcmd, "@RankID", DbType.String, TitleRow.RankID.Value)
            db.AddInParameter(dbcmd, "@TitleID", DbType.String, TitleRow.TitleID.Value)

            intCount = Convert.ToInt32(db.ExecuteScalar(dbcmd))
            Return IIf(intCount > 0, True, False)
        End Function

        Public Function IsDataExists(ByVal TitleRow As Row, ByVal tran As DbTransaction) As Boolean
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intCount As Integer = 0

            strSQL.AppendLine("Select Count(*) Cnt From Title")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And RankID = @RankID")
            strSQL.AppendLine("And TitleID = @TitleID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, TitleRow.CompID.Value)
            db.AddInParameter(dbcmd, "@RankID", DbType.String, TitleRow.RankID.Value)
            db.AddInParameter(dbcmd, "@TitleID", DbType.String, TitleRow.TitleID.Value)

            intCount = Convert.ToInt32(db.ExecuteScalar(dbcmd, tran))
            Return IIf(intCount > 0, True, False)
        End Function

        Public Function QuerybyWhere(WhereCondition As String) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From Title")
            If WhereCondition <> String.Empty Then strSQL.AppendLine(WhereCondition)

            Return db.ExecuteDataSet(CommandType.Text, strSQL.ToString())
        End Function

        Public Function Insert(ByVal TitleRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Insert into Title")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    CompID, RankID, TitleID, TitleName, TitleEngName, LastChgComp, LastChgID, LastChgDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @CompID, @RankID, @TitleID, @TitleName, @TitleEngName, @LastChgComp, @LastChgID, @LastChgDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, TitleRow.CompID.Value)
            db.AddInParameter(dbcmd, "@RankID", DbType.String, TitleRow.RankID.Value)
            db.AddInParameter(dbcmd, "@TitleID", DbType.String, TitleRow.TitleID.Value)
            db.AddInParameter(dbcmd, "@TitleName", DbType.String, TitleRow.TitleName.Value)
            db.AddInParameter(dbcmd, "@TitleEngName", DbType.String, TitleRow.TitleEngName.Value)
            db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, TitleRow.LastChgComp.Value)
            db.AddInParameter(dbcmd, "@LastChgID", DbType.String, TitleRow.LastChgID.Value)
            db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(TitleRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), TitleRow.LastChgDate.Value))

            Return db.ExecuteNonQuery(dbcmd)
        End Function

        Public Function Insert(ByVal TitleRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Insert into Title")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    CompID, RankID, TitleID, TitleName, TitleEngName, LastChgComp, LastChgID, LastChgDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @CompID, @RankID, @TitleID, @TitleName, @TitleEngName, @LastChgComp, @LastChgID, @LastChgDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, TitleRow.CompID.Value)
            db.AddInParameter(dbcmd, "@RankID", DbType.String, TitleRow.RankID.Value)
            db.AddInParameter(dbcmd, "@TitleID", DbType.String, TitleRow.TitleID.Value)
            db.AddInParameter(dbcmd, "@TitleName", DbType.String, TitleRow.TitleName.Value)
            db.AddInParameter(dbcmd, "@TitleEngName", DbType.String, TitleRow.TitleEngName.Value)
            db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, TitleRow.LastChgComp.Value)
            db.AddInParameter(dbcmd, "@LastChgID", DbType.String, TitleRow.LastChgID.Value)
            db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(TitleRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), TitleRow.LastChgDate.Value))

            Return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function Insert(ByVal TitleRow As Row()) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Insert into Title")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    CompID, RankID, TitleID, TitleName, TitleEngName, LastChgComp, LastChgID, LastChgDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @CompID, @RankID, @TitleID, @TitleName, @TitleEngName, @LastChgComp, @LastChgID, @LastChgDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            Using cn As DbConnection = db.CreateConnection()
                cn.Open()
                Dim tran As DbTransaction = cn.BeginTransaction()
                Dim inTrans As Boolean = True

                Try
                    For Each r As Row In TitleRow
                        dbcmd.Parameters.Clear()
                        db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                        db.AddInParameter(dbcmd, "@RankID", DbType.String, r.RankID.Value)
                        db.AddInParameter(dbcmd, "@TitleID", DbType.String, r.TitleID.Value)
                        db.AddInParameter(dbcmd, "@TitleName", DbType.String, r.TitleName.Value)
                        db.AddInParameter(dbcmd, "@TitleEngName", DbType.String, r.TitleEngName.Value)
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

        Public Function Insert(ByVal TitleRow As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Insert into Title")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    CompID, RankID, TitleID, TitleName, TitleEngName, LastChgComp, LastChgID, LastChgDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @CompID, @RankID, @TitleID, @TitleName, @TitleEngName, @LastChgComp, @LastChgID, @LastChgDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())

            For Each r As Row In TitleRow
                dbcmd.Parameters.Clear()
                db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                db.AddInParameter(dbcmd, "@RankID", DbType.String, r.RankID.Value)
                db.AddInParameter(dbcmd, "@TitleID", DbType.String, r.TitleID.Value)
                db.AddInParameter(dbcmd, "@TitleName", DbType.String, r.TitleName.Value)
                db.AddInParameter(dbcmd, "@TitleEngName", DbType.String, r.TitleEngName.Value)
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

