'****************************************************************
' Table:OrgWorkType
' Created Date: 2016.10.26
'****************************************************************/
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports Microsoft.Practices.EnterpriseLibrary.Common
Imports Microsoft.Practices.EnterpriseLibrary.Data
Imports System.Data.Common
Imports System.Data

Namespace beOrgWorkType
    Public Class Table
        Private m_Rows As Rows = New Rows()
        Private m_Fields As String() = { "CompID", "OrganID", "WorkTypeID", "PrincipalFlag", "LastChgComp", "LastChgID", "LastChgDate" }
        Private m_Types As System.Type() = { GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(Date) }
        Private m_PrimaryFields As String() = { "CompID", "OrganID", "WorkTypeID" }

        Public ReadOnly Property Rows() As beOrgWorkType.Rows 
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
        Public Sub Transfer2Row(OrgWorkTypeTable As DataTable)
            For Each dr As DataRow In OrgWorkTypeTable.Rows
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
                dr(m_Rows(i).OrganID.FieldName) = m_Rows(i).OrganID.Value
                dr(m_Rows(i).WorkTypeID.FieldName) = m_Rows(i).WorkTypeID.Value
                dr(m_Rows(i).PrincipalFlag.FieldName) = m_Rows(i).PrincipalFlag.Value
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

        Public Sub Add(OrgWorkTypeRow As Row)
            m_Rows.Add(OrgWorkTypeRow)
        End Sub

        Public Sub Remove(OrgWorkTypeRow As Row)
            If m_Rows.IndexOf(OrgWorkTypeRow) >= 0 Then
                m_Rows.Remove(OrgWorkTypeRow)
            End If
        End Sub

        Public Sub Dispose()
            m_Rows.Clear()
        End Sub

    End Class

    Public Class Row

        Private FI_CompID As Field(Of String) = new Field(Of String)("CompID", true)
        Private FI_OrganID As Field(Of String) = new Field(Of String)("OrganID", true)
        Private FI_WorkTypeID As Field(Of String) = new Field(Of String)("WorkTypeID", true)
        Private FI_PrincipalFlag As Field(Of String) = new Field(Of String)("PrincipalFlag", true)
        Private FI_LastChgComp As Field(Of String) = new Field(Of String)("LastChgComp", true)
        Private FI_LastChgID As Field(Of String) = new Field(Of String)("LastChgID", true)
        Private FI_LastChgDate As Field(Of Date) = new Field(Of Date)("LastChgDate", true)
        Private m_FieldNames As String() = { "CompID", "OrganID", "WorkTypeID", "PrincipalFlag", "LastChgComp", "LastChgID", "LastChgDate" }
        Private m_PrimaryFields As String() = { "CompID", "OrganID", "WorkTypeID" }
        Private m_IdentityFields As String() = {  }
        Private m_LoadFromDataRow As Boolean = False

        Private Function GetFieldValue(ByVal fieldName As String) As Object
            Select Case fieldName
                Case "CompID"
                    Return FI_CompID.Value
                Case "OrganID"
                    Return FI_OrganID.Value
                Case "WorkTypeID"
                    Return FI_WorkTypeID.Value
                Case "PrincipalFlag"
                    Return FI_PrincipalFlag.Value
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
                Case "OrganID"
                    FI_OrganID.SetValue(value)
                Case "WorkTypeID"
                    FI_WorkTypeID.SetValue(value)
                Case "PrincipalFlag"
                    FI_PrincipalFlag.SetValue(value)
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
                Case "OrganID"
                    return FI_OrganID.Updated
                Case "WorkTypeID"
                    return FI_WorkTypeID.Updated
                Case "PrincipalFlag"
                    return FI_PrincipalFlag.Updated
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
                Case "OrganID"
                    return FI_OrganID.CreateUpdateSQL
                Case "WorkTypeID"
                    return FI_WorkTypeID.CreateUpdateSQL
                Case "PrincipalFlag"
                    return FI_PrincipalFlag.CreateUpdateSQL
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
            FI_OrganID.SetInitValue("")
            FI_WorkTypeID.SetInitValue("")
            FI_PrincipalFlag.SetInitValue("0")
            FI_LastChgComp.SetInitValue("")
            FI_LastChgID.SetInitValue("")
            FI_LastChgDate.SetInitValue(Convert.ToDateTime("1900/01/01"))
        End Sub

        Public Sub New(ByVal dr As System.Data.DataRow)
            FI_CompID.SetInitValue(dr("CompID"))
            FI_OrganID.SetInitValue(dr("OrganID"))
            FI_WorkTypeID.SetInitValue(dr("WorkTypeID"))
            FI_PrincipalFlag.SetInitValue(dr("PrincipalFlag"))
            FI_LastChgComp.SetInitValue(dr("LastChgComp"))
            FI_LastChgID.SetInitValue(dr("LastChgID"))
            FI_LastChgDate.SetInitValue(dr("LastChgDate"))

            m_LoadFromDataRow = True
        End Sub

        Private Sub ClearUpdated()
            FI_CompID.Updated = False
            FI_OrganID.Updated = False
            FI_WorkTypeID.Updated = False
            FI_PrincipalFlag.Updated = False
            FI_LastChgComp.Updated = False
            FI_LastChgID.Updated = False
            FI_LastChgDate.Updated = False
        End Sub

        Public ReadOnly Property CompID As Field(Of String) 
            Get
                Return FI_CompID
            End Get
        End Property

        Public ReadOnly Property OrganID As Field(Of String) 
            Get
                Return FI_OrganID
            End Get
        End Property

        Public ReadOnly Property WorkTypeID As Field(Of String) 
            Get
                Return FI_WorkTypeID
            End Get
        End Property

        Public ReadOnly Property PrincipalFlag As Field(Of String) 
            Get
                Return FI_PrincipalFlag
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
        Public Function DeleteRowByPrimaryKey(ByVal OrgWorkTypeRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Delete From OrgWorkType")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And OrganID = @OrganID")
            strSQL.AppendLine("And WorkTypeID = @WorkTypeID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, OrgWorkTypeRow.CompID.Value)
            db.AddInParameter(dbcmd, "@OrganID", DbType.String, OrgWorkTypeRow.OrganID.Value)
            db.AddInParameter(dbcmd, "@WorkTypeID", DbType.String, OrgWorkTypeRow.WorkTypeID.Value)

            return db.ExecuteNonQuery(dbcmd)
        End Function

        public Function DeleteRowByPrimaryKey(ByVal OrgWorkTypeRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Delete From OrgWorkType")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And OrganID = @OrganID")
            strSQL.AppendLine("And WorkTypeID = @WorkTypeID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, OrgWorkTypeRow.CompID.Value)
            db.AddInParameter(dbcmd, "@OrganID", DbType.String, OrgWorkTypeRow.OrganID.Value)
            db.AddInParameter(dbcmd, "@WorkTypeID", DbType.String, OrgWorkTypeRow.WorkTypeID.Value)

            return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function DeleteRowByPrimaryKey(ByVal OrgWorkTypeRow As Row()) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Delete From OrgWorkType")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And OrganID = @OrganID")
            strSQL.AppendLine("And WorkTypeID = @WorkTypeID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            using cn As DbConnection = db.CreateConnection()
                cn.Open()
                Dim tran As DbTransaction = cn.BeginTransaction()
                Dim inTrans As Boolean = True

                Try
                    For Each r As Row In OrgWorkTypeRow
                        dbcmd.Parameters.Clear()
                        db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                        db.AddInParameter(dbcmd, "@OrganID", DbType.String, r.OrganID.Value)
                        db.AddInParameter(dbcmd, "@WorkTypeID", DbType.String, r.WorkTypeID.Value)

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

        Public Function DeleteRowByPrimaryKey(ByVal OrgWorkTypeRow As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Delete From OrgWorkType")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And OrganID = @OrganID")
            strSQL.AppendLine("And WorkTypeID = @WorkTypeID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())

            For Each r As Row In OrgWorkTypeRow
                dbcmd.Parameters.Clear()
                db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                db.AddInParameter(dbcmd, "@OrganID", DbType.String, r.OrganID.Value)
                db.AddInParameter(dbcmd, "@WorkTypeID", DbType.String, r.WorkTypeID.Value)

                intRowsAffected += db.ExecuteNonQuery(dbcmd, tran)
            Next
            Return intRowsAffected
        End Function

        Public Function QueryByKey(ByVal OrgWorkTypeRow As Row) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From OrgWorkType")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And OrganID = @OrganID")
            strSQL.AppendLine("And WorkTypeID = @WorkTypeID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, OrgWorkTypeRow.CompID.Value)
            db.AddInParameter(dbcmd, "@OrganID", DbType.String, OrgWorkTypeRow.OrganID.Value)
            db.AddInParameter(dbcmd, "@WorkTypeID", DbType.String, OrgWorkTypeRow.WorkTypeID.Value)

            Return db.ExecuteDataSet(dbcmd)
        End Function

        Public Function QueryByKey(OrgWorkTypeRow As Row, tran As DbTransaction) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From OrgWorkType")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And OrganID = @OrganID")
            strSQL.AppendLine("And WorkTypeID = @WorkTypeID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, OrgWorkTypeRow.CompID.Value)
            db.AddInParameter(dbcmd, "@OrganID", DbType.String, OrgWorkTypeRow.OrganID.Value)
            db.AddInParameter(dbcmd, "@WorkTypeID", DbType.String, OrgWorkTypeRow.WorkTypeID.Value)

            Return db.ExecuteDataSet(dbcmd, tran)
        End Function

        Public Function Update(ByVal OrgWorkTypeRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim strDot As String = String.Empty

            strSQL.AppendLine("Update OrgWorkType Set")
            For i As Integer = 0 To OrgWorkTypeRow.FieldNames.Length - 1
                If Not OrgWorkTypeRow.IsIdentityField(OrgWorkTypeRow.FieldNames(i)) AndAlso OrgWorkTypeRow.IsUpdated(OrgWorkTypeRow.FieldNames(i)) AndAlso OrgWorkTypeRow.CreateUpdateSQL(OrgWorkTypeRow.FieldNames(i)) Then
                    strSQL.AppendLine(string.Format("{0}{1} = @{1}", strDot, OrgWorkTypeRow.FieldNames(i)))
                    strDot = ","
                End If
            Next
            If strDot = String.Empty Then Throw New Exception("未異動資料欄位！")
            strSQL.AppendLine("Where CompID = @PKCompID")
            strSQL.AppendLine("And OrganID = @PKOrganID")
            strSQL.AppendLine("And WorkTypeID = @PKWorkTypeID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            If OrgWorkTypeRow.CompID.Updated Then db.AddInParameter(dbcmd, "@CompID", DbType.String, OrgWorkTypeRow.CompID.Value)
            If OrgWorkTypeRow.OrganID.Updated Then db.AddInParameter(dbcmd, "@OrganID", DbType.String, OrgWorkTypeRow.OrganID.Value)
            If OrgWorkTypeRow.WorkTypeID.Updated Then db.AddInParameter(dbcmd, "@WorkTypeID", DbType.String, OrgWorkTypeRow.WorkTypeID.Value)
            If OrgWorkTypeRow.PrincipalFlag.Updated Then db.AddInParameter(dbcmd, "@PrincipalFlag", DbType.String, OrgWorkTypeRow.PrincipalFlag.Value)
            If OrgWorkTypeRow.LastChgComp.Updated Then db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, OrgWorkTypeRow.LastChgComp.Value)
            If OrgWorkTypeRow.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, OrgWorkTypeRow.LastChgID.Value)
            If OrgWorkTypeRow.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(OrgWorkTypeRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), OrgWorkTypeRow.LastChgDate.Value))
            db.AddInParameter(dbcmd, "@PKCompID", DbType.String, IIf(OrgWorkTypeRow.LoadFromDataRow, OrgWorkTypeRow.CompID.OldValue, OrgWorkTypeRow.CompID.Value))
            db.AddInParameter(dbcmd, "@PKOrganID", DbType.String, IIf(OrgWorkTypeRow.LoadFromDataRow, OrgWorkTypeRow.OrganID.OldValue, OrgWorkTypeRow.OrganID.Value))
            db.AddInParameter(dbcmd, "@PKWorkTypeID", DbType.String, IIf(OrgWorkTypeRow.LoadFromDataRow, OrgWorkTypeRow.WorkTypeID.OldValue, OrgWorkTypeRow.WorkTypeID.Value))

            Return db.ExecuteNonQuery(dbcmd)
        End Function

        Public Function Update(ByVal OrgWorkTypeRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim strDot As String = String.Empty

            strSQL.AppendLine("Update OrgWorkType Set")
            For i As Integer = 0 To OrgWorkTypeRow.FieldNames.Length - 1
                If Not OrgWorkTypeRow.IsIdentityField(OrgWorkTypeRow.FieldNames(i)) AndAlso OrgWorkTypeRow.IsUpdated(OrgWorkTypeRow.FieldNames(i)) AndAlso OrgWorkTypeRow.CreateUpdateSQL(OrgWorkTypeRow.FieldNames(i)) Then
                    strSQL.AppendLine(string.Format("{0}{1} = @{1}", strDot, OrgWorkTypeRow.FieldNames(i)))
                    strDot = ","
                End If
            Next 
            If strDot = String.Empty Then Throw New Exception("未異動資料欄位！")
            strSQL.AppendLine("Where CompID = @PKCompID")
            strSQL.AppendLine("And OrganID = @PKOrganID")
            strSQL.AppendLine("And WorkTypeID = @PKWorkTypeID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            If OrgWorkTypeRow.CompID.Updated Then db.AddInParameter(dbcmd, "@CompID", DbType.String, OrgWorkTypeRow.CompID.Value)
            If OrgWorkTypeRow.OrganID.Updated Then db.AddInParameter(dbcmd, "@OrganID", DbType.String, OrgWorkTypeRow.OrganID.Value)
            If OrgWorkTypeRow.WorkTypeID.Updated Then db.AddInParameter(dbcmd, "@WorkTypeID", DbType.String, OrgWorkTypeRow.WorkTypeID.Value)
            If OrgWorkTypeRow.PrincipalFlag.Updated Then db.AddInParameter(dbcmd, "@PrincipalFlag", DbType.String, OrgWorkTypeRow.PrincipalFlag.Value)
            If OrgWorkTypeRow.LastChgComp.Updated Then db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, OrgWorkTypeRow.LastChgComp.Value)
            If OrgWorkTypeRow.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, OrgWorkTypeRow.LastChgID.Value)
            If OrgWorkTypeRow.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(OrgWorkTypeRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), OrgWorkTypeRow.LastChgDate.Value))
            db.AddInParameter(dbcmd, "@PKCompID", DbType.String, IIf(OrgWorkTypeRow.LoadFromDataRow, OrgWorkTypeRow.CompID.OldValue, OrgWorkTypeRow.CompID.Value))
            db.AddInParameter(dbcmd, "@PKOrganID", DbType.String, IIf(OrgWorkTypeRow.LoadFromDataRow, OrgWorkTypeRow.OrganID.OldValue, OrgWorkTypeRow.OrganID.Value))
            db.AddInParameter(dbcmd, "@PKWorkTypeID", DbType.String, IIf(OrgWorkTypeRow.LoadFromDataRow, OrgWorkTypeRow.WorkTypeID.OldValue, OrgWorkTypeRow.WorkTypeID.Value))

            Return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function Update(ByVal OrgWorkTypeRow As Row()) As Integer
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
                    For Each r As Row In OrgWorkTypeRow
                        strDot = String.Empty
                        If strSQL.Length > 0 Then strSQL.Remove(0, strSQL.Length)
                        strSQL.AppendLine("Update OrgWorkType Set")
                        For i As Integer = 0 To r.FieldNames.Length - 1
                            If Not r.IsIdentityField(r.FieldNames(i)) AndAlso r.IsUpdated(r.FieldNames(i)) AndAlso r.CreateUpdateSQL(r.FieldNames(i)) Then
                                strSQL.AppendLine(String.Format("{0}{1} = @{1}", strDot, r.FieldNames(i)))
                                strDot = ","
                            End If
                        Next
                        If strDot = String.Empty Then Continue For
                        strSQL.AppendLine("Where CompID = @PKCompID")
                        strSQL.AppendLine("And OrganID = @PKOrganID")
                        strSQL.AppendLine("And WorkTypeID = @PKWorkTypeID")

                        dbcmd = db.GetSqlStringCommand(strSQL.ToString())
                        If r.CompID.Updated Then db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                        If r.OrganID.Updated Then db.AddInParameter(dbcmd, "@OrganID", DbType.String, r.OrganID.Value)
                        If r.WorkTypeID.Updated Then db.AddInParameter(dbcmd, "@WorkTypeID", DbType.String, r.WorkTypeID.Value)
                        If r.PrincipalFlag.Updated Then db.AddInParameter(dbcmd, "@PrincipalFlag", DbType.String, r.PrincipalFlag.Value)
                        If r.LastChgComp.Updated Then db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, r.LastChgComp.Value)
                        If r.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, r.LastChgID.Value)
                        If r.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(r.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), r.LastChgDate.Value))
                        db.AddInParameter(dbcmd, "@PKCompID", DbType.String, IIf(r.LoadFromDataRow, r.CompID.OldValue, r.CompID.Value))
                        db.AddInParameter(dbcmd, "@PKOrganID", DbType.String, IIf(r.LoadFromDataRow, r.OrganID.OldValue, r.OrganID.Value))
                        db.AddInParameter(dbcmd, "@PKWorkTypeID", DbType.String, IIf(r.LoadFromDataRow, r.WorkTypeID.OldValue, r.WorkTypeID.Value))

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

        Public Function Update(ByVal OrgWorkTypeRow As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0
            Dim strDot As String

            For Each r As Row In OrgWorkTypeRow
                strDot = String.Empty
                If strSQL.Length > 0 Then strSQL.Remove(0, strSQL.Length)
                strSQL.AppendLine("Update OrgWorkType Set")
                For i As Integer = 0 To r.FieldNames.Length - 1
                    If Not r.IsIdentityField(r.FieldNames(i)) AndAlso r.IsUpdated(r.FieldNames(i)) AndAlso r.CreateUpdateSQL(r.FieldNames(i))
                        strSQL.AppendLine(String.Format("{0}{1} = @{1}", strDot, r.FieldNames(i)))
                        strDot = ","
                    End If
                Next
                If strDot = String.Empty Then Continue For
                strSQL.AppendLine("Where CompID = @PKCompID")
                strSQL.AppendLine("And OrganID = @PKOrganID")
                strSQL.AppendLine("And WorkTypeID = @PKWorkTypeID")

                dbcmd = db.GetSqlStringCommand(strSQL.ToString())
                If r.CompID.Updated Then db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                If r.OrganID.Updated Then db.AddInParameter(dbcmd, "@OrganID", DbType.String, r.OrganID.Value)
                If r.WorkTypeID.Updated Then db.AddInParameter(dbcmd, "@WorkTypeID", DbType.String, r.WorkTypeID.Value)
                If r.PrincipalFlag.Updated Then db.AddInParameter(dbcmd, "@PrincipalFlag", DbType.String, r.PrincipalFlag.Value)
                If r.LastChgComp.Updated Then db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, r.LastChgComp.Value)
                If r.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, r.LastChgID.Value)
                If r.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(r.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), r.LastChgDate.Value))
                db.AddInParameter(dbcmd, "@PKCompID", DbType.String, IIf(r.LoadFromDataRow, r.CompID.OldValue, r.CompID.Value))
                db.AddInParameter(dbcmd, "@PKOrganID", DbType.String, IIf(r.LoadFromDataRow, r.OrganID.OldValue, r.OrganID.Value))
                db.AddInParameter(dbcmd, "@PKWorkTypeID", DbType.String, IIf(r.LoadFromDataRow, r.WorkTypeID.OldValue, r.WorkTypeID.Value))

                intRowsAffected += db.ExecuteNonQuery(dbcmd, tran)
            Next
            Return intRowsAffected
        End Function

        Public Function IsDataExists(ByVal OrgWorkTypeRow As Row) As Boolean
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intCount As Integer = 0

            strSQL.AppendLine("Select Count(*) Cnt From OrgWorkType")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And OrganID = @OrganID")
            strSQL.AppendLine("And WorkTypeID = @WorkTypeID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, OrgWorkTypeRow.CompID.Value)
            db.AddInParameter(dbcmd, "@OrganID", DbType.String, OrgWorkTypeRow.OrganID.Value)
            db.AddInParameter(dbcmd, "@WorkTypeID", DbType.String, OrgWorkTypeRow.WorkTypeID.Value)

            intCount = Convert.ToInt32(db.ExecuteScalar(dbcmd))
            Return IIf(intCount > 0, True, False)
        End Function

        Public Function IsDataExists(ByVal OrgWorkTypeRow As Row, ByVal tran As DbTransaction) As Boolean
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intCount As Integer = 0

            strSQL.AppendLine("Select Count(*) Cnt From OrgWorkType")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And OrganID = @OrganID")
            strSQL.AppendLine("And WorkTypeID = @WorkTypeID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, OrgWorkTypeRow.CompID.Value)
            db.AddInParameter(dbcmd, "@OrganID", DbType.String, OrgWorkTypeRow.OrganID.Value)
            db.AddInParameter(dbcmd, "@WorkTypeID", DbType.String, OrgWorkTypeRow.WorkTypeID.Value)

            intCount = Convert.ToInt32(db.ExecuteScalar(dbcmd, tran))
            Return IIf(intCount > 0, True, False)
        End Function

        Public Function QuerybyWhere(WhereCondition As String) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From OrgWorkType")
            If WhereCondition <> String.Empty Then strSQL.AppendLine(WhereCondition)

            Return db.ExecuteDataSet(CommandType.Text, strSQL.ToString())
        End Function

        Public Function Insert(ByVal OrgWorkTypeRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Insert into OrgWorkType")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    CompID, OrganID, WorkTypeID, PrincipalFlag, LastChgComp, LastChgID, LastChgDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @CompID, @OrganID, @WorkTypeID, @PrincipalFlag, @LastChgComp, @LastChgID, @LastChgDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, OrgWorkTypeRow.CompID.Value)
            db.AddInParameter(dbcmd, "@OrganID", DbType.String, OrgWorkTypeRow.OrganID.Value)
            db.AddInParameter(dbcmd, "@WorkTypeID", DbType.String, OrgWorkTypeRow.WorkTypeID.Value)
            db.AddInParameter(dbcmd, "@PrincipalFlag", DbType.String, OrgWorkTypeRow.PrincipalFlag.Value)
            db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, OrgWorkTypeRow.LastChgComp.Value)
            db.AddInParameter(dbcmd, "@LastChgID", DbType.String, OrgWorkTypeRow.LastChgID.Value)
            db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(OrgWorkTypeRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), OrgWorkTypeRow.LastChgDate.Value))

            Return db.ExecuteNonQuery(dbcmd)
        End Function

        Public Function Insert(ByVal OrgWorkTypeRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Insert into OrgWorkType")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    CompID, OrganID, WorkTypeID, PrincipalFlag, LastChgComp, LastChgID, LastChgDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @CompID, @OrganID, @WorkTypeID, @PrincipalFlag, @LastChgComp, @LastChgID, @LastChgDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, OrgWorkTypeRow.CompID.Value)
            db.AddInParameter(dbcmd, "@OrganID", DbType.String, OrgWorkTypeRow.OrganID.Value)
            db.AddInParameter(dbcmd, "@WorkTypeID", DbType.String, OrgWorkTypeRow.WorkTypeID.Value)
            db.AddInParameter(dbcmd, "@PrincipalFlag", DbType.String, OrgWorkTypeRow.PrincipalFlag.Value)
            db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, OrgWorkTypeRow.LastChgComp.Value)
            db.AddInParameter(dbcmd, "@LastChgID", DbType.String, OrgWorkTypeRow.LastChgID.Value)
            db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(OrgWorkTypeRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), OrgWorkTypeRow.LastChgDate.Value))

            Return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function Insert(ByVal OrgWorkTypeRow As Row()) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Insert into OrgWorkType")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    CompID, OrganID, WorkTypeID, PrincipalFlag, LastChgComp, LastChgID, LastChgDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @CompID, @OrganID, @WorkTypeID, @PrincipalFlag, @LastChgComp, @LastChgID, @LastChgDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            Using cn As DbConnection = db.CreateConnection()
                cn.Open()
                Dim tran As DbTransaction = cn.BeginTransaction()
                Dim inTrans As Boolean = True

                Try
                    For Each r As Row In OrgWorkTypeRow
                        dbcmd.Parameters.Clear()
                        db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                        db.AddInParameter(dbcmd, "@OrganID", DbType.String, r.OrganID.Value)
                        db.AddInParameter(dbcmd, "@WorkTypeID", DbType.String, r.WorkTypeID.Value)
                        db.AddInParameter(dbcmd, "@PrincipalFlag", DbType.String, r.PrincipalFlag.Value)
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

        Public Function Insert(ByVal OrgWorkTypeRow As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Insert into OrgWorkType")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    CompID, OrganID, WorkTypeID, PrincipalFlag, LastChgComp, LastChgID, LastChgDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @CompID, @OrganID, @WorkTypeID, @PrincipalFlag, @LastChgComp, @LastChgID, @LastChgDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())

            For Each r As Row In OrgWorkTypeRow
                dbcmd.Parameters.Clear()
                db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                db.AddInParameter(dbcmd, "@OrganID", DbType.String, r.OrganID.Value)
                db.AddInParameter(dbcmd, "@WorkTypeID", DbType.String, r.WorkTypeID.Value)
                db.AddInParameter(dbcmd, "@PrincipalFlag", DbType.String, r.PrincipalFlag.Value)
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

