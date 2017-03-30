'****************************************************************
' Table:EmpSenRank
' Created Date: 2015.08.18
'****************************************************************/
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports Microsoft.Practices.EnterpriseLibrary.Common
Imports Microsoft.Practices.EnterpriseLibrary.Data
Imports System.Data.Common
Imports System.Data

Namespace beEmpSenRank
    Public Class Table
        Private m_Rows As Rows = New Rows()
        Private m_Fields As String() = { "CompID", "EmpID", "RankID", "ValidDateB", "ValidDateE", "Days", "CurrentSen", "TotSen", "LastChgComp", "LastChgID" _
                                    , "LastChgDate" }
        Private m_Types As System.Type() = { GetType(String), GetType(String), GetType(String), GetType(Date), GetType(Date), GetType(Decimal), GetType(Decimal), GetType(Decimal), GetType(String), GetType(String) _
                                    , GetType(Date) }
        Private m_PrimaryFields As String() = { "CompID", "EmpID", "RankID", "ValidDateB" }

        Public ReadOnly Property Rows() As beEmpSenRank.Rows 
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
        Public Sub Transfer2Row(EmpSenRankTable As DataTable)
            For Each dr As DataRow In EmpSenRankTable.Rows
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
                dr(m_Rows(i).RankID.FieldName) = m_Rows(i).RankID.Value
                dr(m_Rows(i).ValidDateB.FieldName) = m_Rows(i).ValidDateB.Value
                dr(m_Rows(i).ValidDateE.FieldName) = m_Rows(i).ValidDateE.Value
                dr(m_Rows(i).Days.FieldName) = m_Rows(i).Days.Value
                dr(m_Rows(i).CurrentSen.FieldName) = m_Rows(i).CurrentSen.Value
                dr(m_Rows(i).TotSen.FieldName) = m_Rows(i).TotSen.Value
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

        Public Sub Add(EmpSenRankRow As Row)
            m_Rows.Add(EmpSenRankRow)
        End Sub

        Public Sub Remove(EmpSenRankRow As Row)
            If m_Rows.IndexOf(EmpSenRankRow) >= 0 Then
                m_Rows.Remove(EmpSenRankRow)
            End If
        End Sub

        Public Sub Dispose()
            m_Rows.Clear()
        End Sub

    End Class

    Public Class Row

        Private FI_CompID As Field(Of String) = new Field(Of String)("CompID", true)
        Private FI_EmpID As Field(Of String) = new Field(Of String)("EmpID", true)
        Private FI_RankID As Field(Of String) = new Field(Of String)("RankID", true)
        Private FI_ValidDateB As Field(Of Date) = new Field(Of Date)("ValidDateB", true)
        Private FI_ValidDateE As Field(Of Date) = new Field(Of Date)("ValidDateE", true)
        Private FI_Days As Field(Of Decimal) = new Field(Of Decimal)("Days", true)
        Private FI_CurrentSen As Field(Of Decimal) = new Field(Of Decimal)("CurrentSen", true)
        Private FI_TotSen As Field(Of Decimal) = new Field(Of Decimal)("TotSen", true)
        Private FI_LastChgComp As Field(Of String) = new Field(Of String)("LastChgComp", true)
        Private FI_LastChgID As Field(Of String) = new Field(Of String)("LastChgID", true)
        Private FI_LastChgDate As Field(Of Date) = new Field(Of Date)("LastChgDate", true)
        Private m_FieldNames As String() = { "CompID", "EmpID", "RankID", "ValidDateB", "ValidDateE", "Days", "CurrentSen", "TotSen", "LastChgComp", "LastChgID" _
                                    , "LastChgDate" }
        Private m_PrimaryFields As String() = { "CompID", "EmpID", "RankID", "ValidDateB" }
        Private m_IdentityFields As String() = {  }
        Private m_LoadFromDataRow As Boolean = False

        Private Function GetFieldValue(ByVal fieldName As String) As Object
            Select Case fieldName
                Case "CompID"
                    Return FI_CompID.Value
                Case "EmpID"
                    Return FI_EmpID.Value
                Case "RankID"
                    Return FI_RankID.Value
                Case "ValidDateB"
                    Return FI_ValidDateB.Value
                Case "ValidDateE"
                    Return FI_ValidDateE.Value
                Case "Days"
                    Return FI_Days.Value
                Case "CurrentSen"
                    Return FI_CurrentSen.Value
                Case "TotSen"
                    Return FI_TotSen.Value
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
                Case "RankID"
                    FI_RankID.SetValue(value)
                Case "ValidDateB"
                    FI_ValidDateB.SetValue(value)
                Case "ValidDateE"
                    FI_ValidDateE.SetValue(value)
                Case "Days"
                    FI_Days.SetValue(value)
                Case "CurrentSen"
                    FI_CurrentSen.SetValue(value)
                Case "TotSen"
                    FI_TotSen.SetValue(value)
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
                Case "RankID"
                    return FI_RankID.Updated
                Case "ValidDateB"
                    return FI_ValidDateB.Updated
                Case "ValidDateE"
                    return FI_ValidDateE.Updated
                Case "Days"
                    return FI_Days.Updated
                Case "CurrentSen"
                    return FI_CurrentSen.Updated
                Case "TotSen"
                    return FI_TotSen.Updated
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
                Case "RankID"
                    return FI_RankID.CreateUpdateSQL
                Case "ValidDateB"
                    return FI_ValidDateB.CreateUpdateSQL
                Case "ValidDateE"
                    return FI_ValidDateE.CreateUpdateSQL
                Case "Days"
                    return FI_Days.CreateUpdateSQL
                Case "CurrentSen"
                    return FI_CurrentSen.CreateUpdateSQL
                Case "TotSen"
                    return FI_TotSen.CreateUpdateSQL
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
            FI_RankID.SetInitValue("")
            FI_ValidDateB.SetInitValue(Convert.ToDateTime("1900/01/01"))
            FI_ValidDateE.SetInitValue(Convert.ToDateTime("1900/01/01"))
            FI_Days.SetInitValue(0)
            FI_CurrentSen.SetInitValue(0)
            FI_TotSen.SetInitValue(0)
            FI_LastChgComp.SetInitValue("")
            FI_LastChgID.SetInitValue("")
            FI_LastChgDate.SetInitValue(Convert.ToDateTime("1900/01/01"))
        End Sub

        Public Sub New(ByVal dr As System.Data.DataRow)
            FI_CompID.SetInitValue(dr("CompID"))
            FI_EmpID.SetInitValue(dr("EmpID"))
            FI_RankID.SetInitValue(dr("RankID"))
            FI_ValidDateB.SetInitValue(dr("ValidDateB"))
            FI_ValidDateE.SetInitValue(dr("ValidDateE"))
            FI_Days.SetInitValue(dr("Days"))
            FI_CurrentSen.SetInitValue(dr("CurrentSen"))
            FI_TotSen.SetInitValue(dr("TotSen"))
            FI_LastChgComp.SetInitValue(dr("LastChgComp"))
            FI_LastChgID.SetInitValue(dr("LastChgID"))
            FI_LastChgDate.SetInitValue(dr("LastChgDate"))

            m_LoadFromDataRow = True
        End Sub

        Private Sub ClearUpdated()
            FI_CompID.Updated = False
            FI_EmpID.Updated = False
            FI_RankID.Updated = False
            FI_ValidDateB.Updated = False
            FI_ValidDateE.Updated = False
            FI_Days.Updated = False
            FI_CurrentSen.Updated = False
            FI_TotSen.Updated = False
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

        Public ReadOnly Property RankID As Field(Of String) 
            Get
                Return FI_RankID
            End Get
        End Property

        Public ReadOnly Property ValidDateB As Field(Of Date) 
            Get
                Return FI_ValidDateB
            End Get
        End Property

        Public ReadOnly Property ValidDateE As Field(Of Date) 
            Get
                Return FI_ValidDateE
            End Get
        End Property

        Public ReadOnly Property Days As Field(Of Decimal) 
            Get
                Return FI_Days
            End Get
        End Property

        Public ReadOnly Property CurrentSen As Field(Of Decimal) 
            Get
                Return FI_CurrentSen
            End Get
        End Property

        Public ReadOnly Property TotSen As Field(Of Decimal) 
            Get
                Return FI_TotSen
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
        Public Function DeleteRowByPrimaryKey(ByVal EmpSenRankRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Delete From EmpSenRank")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And EmpID = @EmpID")
            strSQL.AppendLine("And RankID = @RankID")
            strSQL.AppendLine("And ValidDateB = @ValidDateB")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, EmpSenRankRow.CompID.Value)
            db.AddInParameter(dbcmd, "@EmpID", DbType.String, EmpSenRankRow.EmpID.Value)
            db.AddInParameter(dbcmd, "@RankID", DbType.String, EmpSenRankRow.RankID.Value)
            db.AddInParameter(dbcmd, "@ValidDateB", DbType.Date, EmpSenRankRow.ValidDateB.Value)

            return db.ExecuteNonQuery(dbcmd)
        End Function

        public Function DeleteRowByPrimaryKey(ByVal EmpSenRankRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Delete From EmpSenRank")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And EmpID = @EmpID")
            strSQL.AppendLine("And RankID = @RankID")
            strSQL.AppendLine("And ValidDateB = @ValidDateB")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, EmpSenRankRow.CompID.Value)
            db.AddInParameter(dbcmd, "@EmpID", DbType.String, EmpSenRankRow.EmpID.Value)
            db.AddInParameter(dbcmd, "@RankID", DbType.String, EmpSenRankRow.RankID.Value)
            db.AddInParameter(dbcmd, "@ValidDateB", DbType.Date, EmpSenRankRow.ValidDateB.Value)

            return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function DeleteRowByPrimaryKey(ByVal EmpSenRankRow As Row()) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Delete From EmpSenRank")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And EmpID = @EmpID")
            strSQL.AppendLine("And RankID = @RankID")
            strSQL.AppendLine("And ValidDateB = @ValidDateB")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            using cn As DbConnection = db.CreateConnection()
                cn.Open()
                Dim tran As DbTransaction = cn.BeginTransaction()
                Dim inTrans As Boolean = True

                Try
                    For Each r As Row In EmpSenRankRow
                        dbcmd.Parameters.Clear()
                        db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                        db.AddInParameter(dbcmd, "@EmpID", DbType.String, r.EmpID.Value)
                        db.AddInParameter(dbcmd, "@RankID", DbType.String, r.RankID.Value)
                        db.AddInParameter(dbcmd, "@ValidDateB", DbType.Date, r.ValidDateB.Value)

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

        Public Function DeleteRowByPrimaryKey(ByVal EmpSenRankRow As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Delete From EmpSenRank")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And EmpID = @EmpID")
            strSQL.AppendLine("And RankID = @RankID")
            strSQL.AppendLine("And ValidDateB = @ValidDateB")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())

            For Each r As Row In EmpSenRankRow
                dbcmd.Parameters.Clear()
                db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                db.AddInParameter(dbcmd, "@EmpID", DbType.String, r.EmpID.Value)
                db.AddInParameter(dbcmd, "@RankID", DbType.String, r.RankID.Value)
                db.AddInParameter(dbcmd, "@ValidDateB", DbType.Date, r.ValidDateB.Value)

                intRowsAffected += db.ExecuteNonQuery(dbcmd, tran)
            Next
            Return intRowsAffected
        End Function

        Public Function QueryByKey(ByVal EmpSenRankRow As Row) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From EmpSenRank")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And EmpID = @EmpID")
            strSQL.AppendLine("And RankID = @RankID")
            strSQL.AppendLine("And ValidDateB = @ValidDateB")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, EmpSenRankRow.CompID.Value)
            db.AddInParameter(dbcmd, "@EmpID", DbType.String, EmpSenRankRow.EmpID.Value)
            db.AddInParameter(dbcmd, "@RankID", DbType.String, EmpSenRankRow.RankID.Value)
            db.AddInParameter(dbcmd, "@ValidDateB", DbType.Date, EmpSenRankRow.ValidDateB.Value)

            Return db.ExecuteDataSet(dbcmd)
        End Function

        Public Function QueryByKey(EmpSenRankRow As Row, tran As DbTransaction) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From EmpSenRank")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And EmpID = @EmpID")
            strSQL.AppendLine("And RankID = @RankID")
            strSQL.AppendLine("And ValidDateB = @ValidDateB")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, EmpSenRankRow.CompID.Value)
            db.AddInParameter(dbcmd, "@EmpID", DbType.String, EmpSenRankRow.EmpID.Value)
            db.AddInParameter(dbcmd, "@RankID", DbType.String, EmpSenRankRow.RankID.Value)
            db.AddInParameter(dbcmd, "@ValidDateB", DbType.Date, EmpSenRankRow.ValidDateB.Value)

            Return db.ExecuteDataSet(dbcmd, tran)
        End Function

        Public Function Update(ByVal EmpSenRankRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim strDot As String = String.Empty

            strSQL.AppendLine("Update EmpSenRank Set")
            For i As Integer = 0 To EmpSenRankRow.FieldNames.Length - 1
                If Not EmpSenRankRow.IsIdentityField(EmpSenRankRow.FieldNames(i)) AndAlso EmpSenRankRow.IsUpdated(EmpSenRankRow.FieldNames(i)) AndAlso EmpSenRankRow.CreateUpdateSQL(EmpSenRankRow.FieldNames(i)) Then
                    strSQL.AppendLine(string.Format("{0}{1} = @{1}", strDot, EmpSenRankRow.FieldNames(i)))
                    strDot = ","
                End If
            Next
            If strDot = String.Empty Then Throw New Exception("未異動資料欄位！")
            strSQL.AppendLine("Where CompID = @PKCompID")
            strSQL.AppendLine("And EmpID = @PKEmpID")
            strSQL.AppendLine("And RankID = @PKRankID")
            strSQL.AppendLine("And ValidDateB = @PKValidDateB")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            If EmpSenRankRow.CompID.Updated Then db.AddInParameter(dbcmd, "@CompID", DbType.String, EmpSenRankRow.CompID.Value)
            If EmpSenRankRow.EmpID.Updated Then db.AddInParameter(dbcmd, "@EmpID", DbType.String, EmpSenRankRow.EmpID.Value)
            If EmpSenRankRow.RankID.Updated Then db.AddInParameter(dbcmd, "@RankID", DbType.String, EmpSenRankRow.RankID.Value)
            If EmpSenRankRow.ValidDateB.Updated Then db.AddInParameter(dbcmd, "@ValidDateB", DbType.Date, IIf(IsDateTimeNull(EmpSenRankRow.ValidDateB.Value), Convert.ToDateTime("1900/1/1"), EmpSenRankRow.ValidDateB.Value))
            If EmpSenRankRow.ValidDateE.Updated Then db.AddInParameter(dbcmd, "@ValidDateE", DbType.Date, IIf(IsDateTimeNull(EmpSenRankRow.ValidDateE.Value), Convert.ToDateTime("1900/1/1"), EmpSenRankRow.ValidDateE.Value))
            If EmpSenRankRow.Days.Updated Then db.AddInParameter(dbcmd, "@Days", DbType.Decimal, EmpSenRankRow.Days.Value)
            If EmpSenRankRow.CurrentSen.Updated Then db.AddInParameter(dbcmd, "@CurrentSen", DbType.Decimal, EmpSenRankRow.CurrentSen.Value)
            If EmpSenRankRow.TotSen.Updated Then db.AddInParameter(dbcmd, "@TotSen", DbType.Decimal, EmpSenRankRow.TotSen.Value)
            If EmpSenRankRow.LastChgComp.Updated Then db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, EmpSenRankRow.LastChgComp.Value)
            If EmpSenRankRow.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, EmpSenRankRow.LastChgID.Value)
            If EmpSenRankRow.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(EmpSenRankRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), EmpSenRankRow.LastChgDate.Value))
            db.AddInParameter(dbcmd, "@PKCompID", DbType.String, IIf(EmpSenRankRow.LoadFromDataRow, EmpSenRankRow.CompID.OldValue, EmpSenRankRow.CompID.Value))
            db.AddInParameter(dbcmd, "@PKEmpID", DbType.String, IIf(EmpSenRankRow.LoadFromDataRow, EmpSenRankRow.EmpID.OldValue, EmpSenRankRow.EmpID.Value))
            db.AddInParameter(dbcmd, "@PKRankID", DbType.String, IIf(EmpSenRankRow.LoadFromDataRow, EmpSenRankRow.RankID.OldValue, EmpSenRankRow.RankID.Value))
            db.AddInParameter(dbcmd, "@PKValidDateB", DbType.Date, IIf(EmpSenRankRow.LoadFromDataRow, EmpSenRankRow.ValidDateB.OldValue, EmpSenRankRow.ValidDateB.Value))

            Return db.ExecuteNonQuery(dbcmd)
        End Function

        Public Function Update(ByVal EmpSenRankRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim strDot As String = String.Empty

            strSQL.AppendLine("Update EmpSenRank Set")
            For i As Integer = 0 To EmpSenRankRow.FieldNames.Length - 1
                If Not EmpSenRankRow.IsIdentityField(EmpSenRankRow.FieldNames(i)) AndAlso EmpSenRankRow.IsUpdated(EmpSenRankRow.FieldNames(i)) AndAlso EmpSenRankRow.CreateUpdateSQL(EmpSenRankRow.FieldNames(i)) Then
                    strSQL.AppendLine(string.Format("{0}{1} = @{1}", strDot, EmpSenRankRow.FieldNames(i)))
                    strDot = ","
                End If
            Next 
            If strDot = String.Empty Then Throw New Exception("未異動資料欄位！")
            strSQL.AppendLine("Where CompID = @PKCompID")
            strSQL.AppendLine("And EmpID = @PKEmpID")
            strSQL.AppendLine("And RankID = @PKRankID")
            strSQL.AppendLine("And ValidDateB = @PKValidDateB")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            If EmpSenRankRow.CompID.Updated Then db.AddInParameter(dbcmd, "@CompID", DbType.String, EmpSenRankRow.CompID.Value)
            If EmpSenRankRow.EmpID.Updated Then db.AddInParameter(dbcmd, "@EmpID", DbType.String, EmpSenRankRow.EmpID.Value)
            If EmpSenRankRow.RankID.Updated Then db.AddInParameter(dbcmd, "@RankID", DbType.String, EmpSenRankRow.RankID.Value)
            If EmpSenRankRow.ValidDateB.Updated Then db.AddInParameter(dbcmd, "@ValidDateB", DbType.Date, IIf(IsDateTimeNull(EmpSenRankRow.ValidDateB.Value), Convert.ToDateTime("1900/1/1"), EmpSenRankRow.ValidDateB.Value))
            If EmpSenRankRow.ValidDateE.Updated Then db.AddInParameter(dbcmd, "@ValidDateE", DbType.Date, IIf(IsDateTimeNull(EmpSenRankRow.ValidDateE.Value), Convert.ToDateTime("1900/1/1"), EmpSenRankRow.ValidDateE.Value))
            If EmpSenRankRow.Days.Updated Then db.AddInParameter(dbcmd, "@Days", DbType.Decimal, EmpSenRankRow.Days.Value)
            If EmpSenRankRow.CurrentSen.Updated Then db.AddInParameter(dbcmd, "@CurrentSen", DbType.Decimal, EmpSenRankRow.CurrentSen.Value)
            If EmpSenRankRow.TotSen.Updated Then db.AddInParameter(dbcmd, "@TotSen", DbType.Decimal, EmpSenRankRow.TotSen.Value)
            If EmpSenRankRow.LastChgComp.Updated Then db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, EmpSenRankRow.LastChgComp.Value)
            If EmpSenRankRow.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, EmpSenRankRow.LastChgID.Value)
            If EmpSenRankRow.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(EmpSenRankRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), EmpSenRankRow.LastChgDate.Value))
            db.AddInParameter(dbcmd, "@PKCompID", DbType.String, IIf(EmpSenRankRow.LoadFromDataRow, EmpSenRankRow.CompID.OldValue, EmpSenRankRow.CompID.Value))
            db.AddInParameter(dbcmd, "@PKEmpID", DbType.String, IIf(EmpSenRankRow.LoadFromDataRow, EmpSenRankRow.EmpID.OldValue, EmpSenRankRow.EmpID.Value))
            db.AddInParameter(dbcmd, "@PKRankID", DbType.String, IIf(EmpSenRankRow.LoadFromDataRow, EmpSenRankRow.RankID.OldValue, EmpSenRankRow.RankID.Value))
            db.AddInParameter(dbcmd, "@PKValidDateB", DbType.Date, IIf(EmpSenRankRow.LoadFromDataRow, EmpSenRankRow.ValidDateB.OldValue, EmpSenRankRow.ValidDateB.Value))

            Return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function Update(ByVal EmpSenRankRow As Row()) As Integer
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
                    For Each r As Row In EmpSenRankRow
                        strDot = String.Empty
                        If strSQL.Length > 0 Then strSQL.Remove(0, strSQL.Length)
                        strSQL.AppendLine("Update EmpSenRank Set")
                        For i As Integer = 0 To r.FieldNames.Length - 1
                            If Not r.IsIdentityField(r.FieldNames(i)) AndAlso r.IsUpdated(r.FieldNames(i)) AndAlso r.CreateUpdateSQL(r.FieldNames(i)) Then
                                strSQL.AppendLine(String.Format("{0}{1} = @{1}", strDot, r.FieldNames(i)))
                                strDot = ","
                            End If
                        Next
                        If strDot = String.Empty Then Continue For
                        strSQL.AppendLine("Where CompID = @PKCompID")
                        strSQL.AppendLine("And EmpID = @PKEmpID")
                        strSQL.AppendLine("And RankID = @PKRankID")
                        strSQL.AppendLine("And ValidDateB = @PKValidDateB")

                        dbcmd = db.GetSqlStringCommand(strSQL.ToString())
                        If r.CompID.Updated Then db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                        If r.EmpID.Updated Then db.AddInParameter(dbcmd, "@EmpID", DbType.String, r.EmpID.Value)
                        If r.RankID.Updated Then db.AddInParameter(dbcmd, "@RankID", DbType.String, r.RankID.Value)
                        If r.ValidDateB.Updated Then db.AddInParameter(dbcmd, "@ValidDateB", DbType.Date, IIf(IsDateTimeNull(r.ValidDateB.Value), Convert.ToDateTime("1900/1/1"), r.ValidDateB.Value))
                        If r.ValidDateE.Updated Then db.AddInParameter(dbcmd, "@ValidDateE", DbType.Date, IIf(IsDateTimeNull(r.ValidDateE.Value), Convert.ToDateTime("1900/1/1"), r.ValidDateE.Value))
                        If r.Days.Updated Then db.AddInParameter(dbcmd, "@Days", DbType.Decimal, r.Days.Value)
                        If r.CurrentSen.Updated Then db.AddInParameter(dbcmd, "@CurrentSen", DbType.Decimal, r.CurrentSen.Value)
                        If r.TotSen.Updated Then db.AddInParameter(dbcmd, "@TotSen", DbType.Decimal, r.TotSen.Value)
                        If r.LastChgComp.Updated Then db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, r.LastChgComp.Value)
                        If r.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, r.LastChgID.Value)
                        If r.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(r.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), r.LastChgDate.Value))
                        db.AddInParameter(dbcmd, "@PKCompID", DbType.String, IIf(r.LoadFromDataRow, r.CompID.OldValue, r.CompID.Value))
                        db.AddInParameter(dbcmd, "@PKEmpID", DbType.String, IIf(r.LoadFromDataRow, r.EmpID.OldValue, r.EmpID.Value))
                        db.AddInParameter(dbcmd, "@PKRankID", DbType.String, IIf(r.LoadFromDataRow, r.RankID.OldValue, r.RankID.Value))
                        db.AddInParameter(dbcmd, "@PKValidDateB", DbType.Date, IIf(r.LoadFromDataRow, r.ValidDateB.OldValue, r.ValidDateB.Value))

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

        Public Function Update(ByVal EmpSenRankRow As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0
            Dim strDot As String

            For Each r As Row In EmpSenRankRow
                strDot = String.Empty
                If strSQL.Length > 0 Then strSQL.Remove(0, strSQL.Length)
                strSQL.AppendLine("Update EmpSenRank Set")
                For i As Integer = 0 To r.FieldNames.Length - 1
                    If Not r.IsIdentityField(r.FieldNames(i)) AndAlso r.IsUpdated(r.FieldNames(i)) AndAlso r.CreateUpdateSQL(r.FieldNames(i))
                        strSQL.AppendLine(String.Format("{0}{1} = @{1}", strDot, r.FieldNames(i)))
                        strDot = ","
                    End If
                Next
                If strDot = String.Empty Then Continue For
                strSQL.AppendLine("Where CompID = @PKCompID")
                strSQL.AppendLine("And EmpID = @PKEmpID")
                strSQL.AppendLine("And RankID = @PKRankID")
                strSQL.AppendLine("And ValidDateB = @PKValidDateB")

                dbcmd = db.GetSqlStringCommand(strSQL.ToString())
                If r.CompID.Updated Then db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                If r.EmpID.Updated Then db.AddInParameter(dbcmd, "@EmpID", DbType.String, r.EmpID.Value)
                If r.RankID.Updated Then db.AddInParameter(dbcmd, "@RankID", DbType.String, r.RankID.Value)
                If r.ValidDateB.Updated Then db.AddInParameter(dbcmd, "@ValidDateB", DbType.Date, IIf(IsDateTimeNull(r.ValidDateB.Value), Convert.ToDateTime("1900/1/1"), r.ValidDateB.Value))
                If r.ValidDateE.Updated Then db.AddInParameter(dbcmd, "@ValidDateE", DbType.Date, IIf(IsDateTimeNull(r.ValidDateE.Value), Convert.ToDateTime("1900/1/1"), r.ValidDateE.Value))
                If r.Days.Updated Then db.AddInParameter(dbcmd, "@Days", DbType.Decimal, r.Days.Value)
                If r.CurrentSen.Updated Then db.AddInParameter(dbcmd, "@CurrentSen", DbType.Decimal, r.CurrentSen.Value)
                If r.TotSen.Updated Then db.AddInParameter(dbcmd, "@TotSen", DbType.Decimal, r.TotSen.Value)
                If r.LastChgComp.Updated Then db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, r.LastChgComp.Value)
                If r.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, r.LastChgID.Value)
                If r.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(r.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), r.LastChgDate.Value))
                db.AddInParameter(dbcmd, "@PKCompID", DbType.String, IIf(r.LoadFromDataRow, r.CompID.OldValue, r.CompID.Value))
                db.AddInParameter(dbcmd, "@PKEmpID", DbType.String, IIf(r.LoadFromDataRow, r.EmpID.OldValue, r.EmpID.Value))
                db.AddInParameter(dbcmd, "@PKRankID", DbType.String, IIf(r.LoadFromDataRow, r.RankID.OldValue, r.RankID.Value))
                db.AddInParameter(dbcmd, "@PKValidDateB", DbType.Date, IIf(r.LoadFromDataRow, r.ValidDateB.OldValue, r.ValidDateB.Value))

                intRowsAffected += db.ExecuteNonQuery(dbcmd, tran)
            Next
            Return intRowsAffected
        End Function

        Public Function IsDataExists(ByVal EmpSenRankRow As Row) As Boolean
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intCount As Integer = 0

            strSQL.AppendLine("Select Count(*) Cnt From EmpSenRank")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And EmpID = @EmpID")
            strSQL.AppendLine("And RankID = @RankID")
            strSQL.AppendLine("And ValidDateB = @ValidDateB")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, EmpSenRankRow.CompID.Value)
            db.AddInParameter(dbcmd, "@EmpID", DbType.String, EmpSenRankRow.EmpID.Value)
            db.AddInParameter(dbcmd, "@RankID", DbType.String, EmpSenRankRow.RankID.Value)
            db.AddInParameter(dbcmd, "@ValidDateB", DbType.Date, EmpSenRankRow.ValidDateB.Value)

            intCount = Convert.ToInt32(db.ExecuteScalar(dbcmd))
            Return IIf(intCount > 0, True, False)
        End Function

        Public Function IsDataExists(ByVal EmpSenRankRow As Row, ByVal tran As DbTransaction) As Boolean
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intCount As Integer = 0

            strSQL.AppendLine("Select Count(*) Cnt From EmpSenRank")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And EmpID = @EmpID")
            strSQL.AppendLine("And RankID = @RankID")
            strSQL.AppendLine("And ValidDateB = @ValidDateB")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, EmpSenRankRow.CompID.Value)
            db.AddInParameter(dbcmd, "@EmpID", DbType.String, EmpSenRankRow.EmpID.Value)
            db.AddInParameter(dbcmd, "@RankID", DbType.String, EmpSenRankRow.RankID.Value)
            db.AddInParameter(dbcmd, "@ValidDateB", DbType.Date, EmpSenRankRow.ValidDateB.Value)

            intCount = Convert.ToInt32(db.ExecuteScalar(dbcmd, tran))
            Return IIf(intCount > 0, True, False)
        End Function

        Public Function QuerybyWhere(WhereCondition As String) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From EmpSenRank")
            If WhereCondition <> String.Empty Then strSQL.AppendLine(WhereCondition)

            Return db.ExecuteDataSet(CommandType.Text, strSQL.ToString())
        End Function

        Public Function Insert(ByVal EmpSenRankRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Insert into EmpSenRank")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    CompID, EmpID, RankID, ValidDateB, ValidDateE, Days, CurrentSen, TotSen, LastChgComp,")
            strSQL.AppendLine("    LastChgID, LastChgDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @CompID, @EmpID, @RankID, @ValidDateB, @ValidDateE, @Days, @CurrentSen, @TotSen, @LastChgComp,")
            strSQL.AppendLine("    @LastChgID, @LastChgDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, EmpSenRankRow.CompID.Value)
            db.AddInParameter(dbcmd, "@EmpID", DbType.String, EmpSenRankRow.EmpID.Value)
            db.AddInParameter(dbcmd, "@RankID", DbType.String, EmpSenRankRow.RankID.Value)
            db.AddInParameter(dbcmd, "@ValidDateB", DbType.Date, IIf(IsDateTimeNull(EmpSenRankRow.ValidDateB.Value), Convert.ToDateTime("1900/1/1"), EmpSenRankRow.ValidDateB.Value))
            db.AddInParameter(dbcmd, "@ValidDateE", DbType.Date, IIf(IsDateTimeNull(EmpSenRankRow.ValidDateE.Value), Convert.ToDateTime("1900/1/1"), EmpSenRankRow.ValidDateE.Value))
            db.AddInParameter(dbcmd, "@Days", DbType.Decimal, EmpSenRankRow.Days.Value)
            db.AddInParameter(dbcmd, "@CurrentSen", DbType.Decimal, EmpSenRankRow.CurrentSen.Value)
            db.AddInParameter(dbcmd, "@TotSen", DbType.Decimal, EmpSenRankRow.TotSen.Value)
            db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, EmpSenRankRow.LastChgComp.Value)
            db.AddInParameter(dbcmd, "@LastChgID", DbType.String, EmpSenRankRow.LastChgID.Value)
            db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(EmpSenRankRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), EmpSenRankRow.LastChgDate.Value))

            Return db.ExecuteNonQuery(dbcmd)
        End Function

        Public Function Insert(ByVal EmpSenRankRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Insert into EmpSenRank")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    CompID, EmpID, RankID, ValidDateB, ValidDateE, Days, CurrentSen, TotSen, LastChgComp,")
            strSQL.AppendLine("    LastChgID, LastChgDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @CompID, @EmpID, @RankID, @ValidDateB, @ValidDateE, @Days, @CurrentSen, @TotSen, @LastChgComp,")
            strSQL.AppendLine("    @LastChgID, @LastChgDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, EmpSenRankRow.CompID.Value)
            db.AddInParameter(dbcmd, "@EmpID", DbType.String, EmpSenRankRow.EmpID.Value)
            db.AddInParameter(dbcmd, "@RankID", DbType.String, EmpSenRankRow.RankID.Value)
            db.AddInParameter(dbcmd, "@ValidDateB", DbType.Date, IIf(IsDateTimeNull(EmpSenRankRow.ValidDateB.Value), Convert.ToDateTime("1900/1/1"), EmpSenRankRow.ValidDateB.Value))
            db.AddInParameter(dbcmd, "@ValidDateE", DbType.Date, IIf(IsDateTimeNull(EmpSenRankRow.ValidDateE.Value), Convert.ToDateTime("1900/1/1"), EmpSenRankRow.ValidDateE.Value))
            db.AddInParameter(dbcmd, "@Days", DbType.Decimal, EmpSenRankRow.Days.Value)
            db.AddInParameter(dbcmd, "@CurrentSen", DbType.Decimal, EmpSenRankRow.CurrentSen.Value)
            db.AddInParameter(dbcmd, "@TotSen", DbType.Decimal, EmpSenRankRow.TotSen.Value)
            db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, EmpSenRankRow.LastChgComp.Value)
            db.AddInParameter(dbcmd, "@LastChgID", DbType.String, EmpSenRankRow.LastChgID.Value)
            db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(EmpSenRankRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), EmpSenRankRow.LastChgDate.Value))

            Return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function Insert(ByVal EmpSenRankRow As Row()) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Insert into EmpSenRank")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    CompID, EmpID, RankID, ValidDateB, ValidDateE, Days, CurrentSen, TotSen, LastChgComp,")
            strSQL.AppendLine("    LastChgID, LastChgDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @CompID, @EmpID, @RankID, @ValidDateB, @ValidDateE, @Days, @CurrentSen, @TotSen, @LastChgComp,")
            strSQL.AppendLine("    @LastChgID, @LastChgDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            Using cn As DbConnection = db.CreateConnection()
                cn.Open()
                Dim tran As DbTransaction = cn.BeginTransaction()
                Dim inTrans As Boolean = True

                Try
                    For Each r As Row In EmpSenRankRow
                        dbcmd.Parameters.Clear()
                        db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                        db.AddInParameter(dbcmd, "@EmpID", DbType.String, r.EmpID.Value)
                        db.AddInParameter(dbcmd, "@RankID", DbType.String, r.RankID.Value)
                        db.AddInParameter(dbcmd, "@ValidDateB", DbType.Date, IIf(IsDateTimeNull(r.ValidDateB.Value), Convert.ToDateTime("1900/1/1"), r.ValidDateB.Value))
                        db.AddInParameter(dbcmd, "@ValidDateE", DbType.Date, IIf(IsDateTimeNull(r.ValidDateE.Value), Convert.ToDateTime("1900/1/1"), r.ValidDateE.Value))
                        db.AddInParameter(dbcmd, "@Days", DbType.Decimal, r.Days.Value)
                        db.AddInParameter(dbcmd, "@CurrentSen", DbType.Decimal, r.CurrentSen.Value)
                        db.AddInParameter(dbcmd, "@TotSen", DbType.Decimal, r.TotSen.Value)
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

        Public Function Insert(ByVal EmpSenRankRow As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Insert into EmpSenRank")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    CompID, EmpID, RankID, ValidDateB, ValidDateE, Days, CurrentSen, TotSen, LastChgComp,")
            strSQL.AppendLine("    LastChgID, LastChgDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @CompID, @EmpID, @RankID, @ValidDateB, @ValidDateE, @Days, @CurrentSen, @TotSen, @LastChgComp,")
            strSQL.AppendLine("    @LastChgID, @LastChgDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())

            For Each r As Row In EmpSenRankRow
                dbcmd.Parameters.Clear()
                db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                db.AddInParameter(dbcmd, "@EmpID", DbType.String, r.EmpID.Value)
                db.AddInParameter(dbcmd, "@RankID", DbType.String, r.RankID.Value)
                db.AddInParameter(dbcmd, "@ValidDateB", DbType.Date, IIf(IsDateTimeNull(r.ValidDateB.Value), Convert.ToDateTime("1900/1/1"), r.ValidDateB.Value))
                db.AddInParameter(dbcmd, "@ValidDateE", DbType.Date, IIf(IsDateTimeNull(r.ValidDateE.Value), Convert.ToDateTime("1900/1/1"), r.ValidDateE.Value))
                db.AddInParameter(dbcmd, "@Days", DbType.Decimal, r.Days.Value)
                db.AddInParameter(dbcmd, "@CurrentSen", DbType.Decimal, r.CurrentSen.Value)
                db.AddInParameter(dbcmd, "@TotSen", DbType.Decimal, r.TotSen.Value)
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

