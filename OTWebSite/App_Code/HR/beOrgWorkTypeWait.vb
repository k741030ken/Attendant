'****************************************************************
' Table:OrgWorkTypeWait
' Created Date: 2016.12.21
'****************************************************************/
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports Microsoft.Practices.EnterpriseLibrary.Common
Imports Microsoft.Practices.EnterpriseLibrary.Data
Imports System.Data.Common
Imports System.Data

Namespace beOrgWorkTypeWait
    Public Class Table
        Private m_Rows As Rows = New Rows()
        Private m_Fields As String() = { "CompID", "OrganReason", "OrganType", "ValidDate", "Seq", "OrganID", "WorkTypeID", "PrincipalFlag", "WaitStatus", "LastChgComp" _
                                    , "LastChgID", "LastChgDate" }
        Private m_Types As System.Type() = { GetType(String), GetType(String), GetType(String), GetType(Date), GetType(Integer), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String) _
                                    , GetType(String), GetType(Date) }
        Private m_PrimaryFields As String() = { "CompID", "OrganID", "OrganReason", "OrganType", "WorkTypeID", "ValidDate", "Seq" }

        Public ReadOnly Property Rows() As beOrgWorkTypeWait.Rows 
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
        Public Sub Transfer2Row(OrgWorkTypeWaitTable As DataTable)
            For Each dr As DataRow In OrgWorkTypeWaitTable.Rows
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
                dr(m_Rows(i).OrganReason.FieldName) = m_Rows(i).OrganReason.Value
                dr(m_Rows(i).OrganType.FieldName) = m_Rows(i).OrganType.Value
                dr(m_Rows(i).ValidDate.FieldName) = m_Rows(i).ValidDate.Value
                dr(m_Rows(i).Seq.FieldName) = m_Rows(i).Seq.Value
                dr(m_Rows(i).OrganID.FieldName) = m_Rows(i).OrganID.Value
                dr(m_Rows(i).WorkTypeID.FieldName) = m_Rows(i).WorkTypeID.Value
                dr(m_Rows(i).PrincipalFlag.FieldName) = m_Rows(i).PrincipalFlag.Value
                dr(m_Rows(i).WaitStatus.FieldName) = m_Rows(i).WaitStatus.Value
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

        Public Sub Add(OrgWorkTypeWaitRow As Row)
            m_Rows.Add(OrgWorkTypeWaitRow)
        End Sub

        Public Sub Remove(OrgWorkTypeWaitRow As Row)
            If m_Rows.IndexOf(OrgWorkTypeWaitRow) >= 0 Then
                m_Rows.Remove(OrgWorkTypeWaitRow)
            End If
        End Sub

        Public Sub Dispose()
            m_Rows.Clear()
        End Sub

    End Class

    Public Class Row

        Private FI_CompID As Field(Of String) = new Field(Of String)("CompID", true)
        Private FI_OrganReason As Field(Of String) = new Field(Of String)("OrganReason", true)
        Private FI_OrganType As Field(Of String) = new Field(Of String)("OrganType", true)
        Private FI_ValidDate As Field(Of Date) = new Field(Of Date)("ValidDate", true)
        Private FI_Seq As Field(Of Integer) = new Field(Of Integer)("Seq", true)
        Private FI_OrganID As Field(Of String) = new Field(Of String)("OrganID", true)
        Private FI_WorkTypeID As Field(Of String) = new Field(Of String)("WorkTypeID", true)
        Private FI_PrincipalFlag As Field(Of String) = new Field(Of String)("PrincipalFlag", true)
        Private FI_WaitStatus As Field(Of String) = new Field(Of String)("WaitStatus", true)
        Private FI_LastChgComp As Field(Of String) = new Field(Of String)("LastChgComp", true)
        Private FI_LastChgID As Field(Of String) = new Field(Of String)("LastChgID", true)
        Private FI_LastChgDate As Field(Of Date) = new Field(Of Date)("LastChgDate", true)
        Private m_FieldNames As String() = { "CompID", "OrganReason", "OrganType", "ValidDate", "Seq", "OrganID", "WorkTypeID", "PrincipalFlag", "WaitStatus", "LastChgComp" _
                                    , "LastChgID", "LastChgDate" }
        Private m_PrimaryFields As String() = { "CompID", "OrganID", "OrganReason", "OrganType", "WorkTypeID", "ValidDate", "Seq" }
        Private m_IdentityFields As String() = {  }
        Private m_LoadFromDataRow As Boolean = False

        Private Function GetFieldValue(ByVal fieldName As String) As Object
            Select Case fieldName
                Case "CompID"
                    Return FI_CompID.Value
                Case "OrganReason"
                    Return FI_OrganReason.Value
                Case "OrganType"
                    Return FI_OrganType.Value
                Case "ValidDate"
                    Return FI_ValidDate.Value
                Case "Seq"
                    Return FI_Seq.Value
                Case "OrganID"
                    Return FI_OrganID.Value
                Case "WorkTypeID"
                    Return FI_WorkTypeID.Value
                Case "PrincipalFlag"
                    Return FI_PrincipalFlag.Value
                Case "WaitStatus"
                    Return FI_WaitStatus.Value
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
                Case "OrganReason"
                    FI_OrganReason.SetValue(value)
                Case "OrganType"
                    FI_OrganType.SetValue(value)
                Case "ValidDate"
                    FI_ValidDate.SetValue(value)
                Case "Seq"
                    FI_Seq.SetValue(value)
                Case "OrganID"
                    FI_OrganID.SetValue(value)
                Case "WorkTypeID"
                    FI_WorkTypeID.SetValue(value)
                Case "PrincipalFlag"
                    FI_PrincipalFlag.SetValue(value)
                Case "WaitStatus"
                    FI_WaitStatus.SetValue(value)
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
                Case "OrganReason"
                    return FI_OrganReason.Updated
                Case "OrganType"
                    return FI_OrganType.Updated
                Case "ValidDate"
                    return FI_ValidDate.Updated
                Case "Seq"
                    return FI_Seq.Updated
                Case "OrganID"
                    return FI_OrganID.Updated
                Case "WorkTypeID"
                    return FI_WorkTypeID.Updated
                Case "PrincipalFlag"
                    return FI_PrincipalFlag.Updated
                Case "WaitStatus"
                    return FI_WaitStatus.Updated
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
                Case "OrganReason"
                    return FI_OrganReason.CreateUpdateSQL
                Case "OrganType"
                    return FI_OrganType.CreateUpdateSQL
                Case "ValidDate"
                    return FI_ValidDate.CreateUpdateSQL
                Case "Seq"
                    return FI_Seq.CreateUpdateSQL
                Case "OrganID"
                    return FI_OrganID.CreateUpdateSQL
                Case "WorkTypeID"
                    return FI_WorkTypeID.CreateUpdateSQL
                Case "PrincipalFlag"
                    return FI_PrincipalFlag.CreateUpdateSQL
                Case "WaitStatus"
                    return FI_WaitStatus.CreateUpdateSQL
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
            FI_OrganReason.SetInitValue("")
            FI_OrganType.SetInitValue("")
            FI_ValidDate.SetInitValue(Convert.ToDateTime("1900/01/01"))
            FI_Seq.SetInitValue(0)
            FI_OrganID.SetInitValue("")
            FI_WorkTypeID.SetInitValue("")
            FI_PrincipalFlag.SetInitValue("")
            FI_WaitStatus.SetInitValue("")
            FI_LastChgComp.SetInitValue("")
            FI_LastChgID.SetInitValue("")
            FI_LastChgDate.SetInitValue(Convert.ToDateTime("1900/01/01"))
        End Sub

        Public Sub New(ByVal dr As System.Data.DataRow)
            FI_CompID.SetInitValue(dr("CompID"))
            FI_OrganReason.SetInitValue(dr("OrganReason"))
            FI_OrganType.SetInitValue(dr("OrganType"))
            FI_ValidDate.SetInitValue(dr("ValidDate"))
            FI_Seq.SetInitValue(dr("Seq"))
            FI_OrganID.SetInitValue(dr("OrganID"))
            FI_WorkTypeID.SetInitValue(dr("WorkTypeID"))
            FI_PrincipalFlag.SetInitValue(dr("PrincipalFlag"))
            FI_WaitStatus.SetInitValue(dr("WaitStatus"))
            FI_LastChgComp.SetInitValue(dr("LastChgComp"))
            FI_LastChgID.SetInitValue(dr("LastChgID"))
            FI_LastChgDate.SetInitValue(dr("LastChgDate"))

            m_LoadFromDataRow = True
        End Sub

        Private Sub ClearUpdated()
            FI_CompID.Updated = False
            FI_OrganReason.Updated = False
            FI_OrganType.Updated = False
            FI_ValidDate.Updated = False
            FI_Seq.Updated = False
            FI_OrganID.Updated = False
            FI_WorkTypeID.Updated = False
            FI_PrincipalFlag.Updated = False
            FI_WaitStatus.Updated = False
            FI_LastChgComp.Updated = False
            FI_LastChgID.Updated = False
            FI_LastChgDate.Updated = False
        End Sub

        Public ReadOnly Property CompID As Field(Of String) 
            Get
                Return FI_CompID
            End Get
        End Property

        Public ReadOnly Property OrganReason As Field(Of String) 
            Get
                Return FI_OrganReason
            End Get
        End Property

        Public ReadOnly Property OrganType As Field(Of String) 
            Get
                Return FI_OrganType
            End Get
        End Property

        Public ReadOnly Property ValidDate As Field(Of Date) 
            Get
                Return FI_ValidDate
            End Get
        End Property

        Public ReadOnly Property Seq As Field(Of Integer) 
            Get
                Return FI_Seq
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

        Public ReadOnly Property WaitStatus As Field(Of String) 
            Get
                Return FI_WaitStatus
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
        Public Function DeleteRowByPrimaryKey(ByVal OrgWorkTypeWaitRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Delete From OrgWorkTypeWait")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And OrganID = @OrganID")
            strSQL.AppendLine("And OrganReason = @OrganReason")
            strSQL.AppendLine("And OrganType = @OrganType")
            strSQL.AppendLine("And WorkTypeID = @WorkTypeID")
            strSQL.AppendLine("And ValidDate = @ValidDate")
            strSQL.AppendLine("And Seq = @Seq")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, OrgWorkTypeWaitRow.CompID.Value)
            db.AddInParameter(dbcmd, "@OrganID", DbType.String, OrgWorkTypeWaitRow.OrganID.Value)
            db.AddInParameter(dbcmd, "@OrganReason", DbType.String, OrgWorkTypeWaitRow.OrganReason.Value)
            db.AddInParameter(dbcmd, "@OrganType", DbType.String, OrgWorkTypeWaitRow.OrganType.Value)
            db.AddInParameter(dbcmd, "@WorkTypeID", DbType.String, OrgWorkTypeWaitRow.WorkTypeID.Value)
            db.AddInParameter(dbcmd, "@ValidDate", DbType.Date, OrgWorkTypeWaitRow.ValidDate.Value)
            db.AddInParameter(dbcmd, "@Seq", DbType.Int32, OrgWorkTypeWaitRow.Seq.Value)

            return db.ExecuteNonQuery(dbcmd)
        End Function

        public Function DeleteRowByPrimaryKey(ByVal OrgWorkTypeWaitRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Delete From OrgWorkTypeWait")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And OrganID = @OrganID")
            strSQL.AppendLine("And OrganReason = @OrganReason")
            strSQL.AppendLine("And OrganType = @OrganType")
            strSQL.AppendLine("And WorkTypeID = @WorkTypeID")
            strSQL.AppendLine("And ValidDate = @ValidDate")
            strSQL.AppendLine("And Seq = @Seq")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, OrgWorkTypeWaitRow.CompID.Value)
            db.AddInParameter(dbcmd, "@OrganID", DbType.String, OrgWorkTypeWaitRow.OrganID.Value)
            db.AddInParameter(dbcmd, "@OrganReason", DbType.String, OrgWorkTypeWaitRow.OrganReason.Value)
            db.AddInParameter(dbcmd, "@OrganType", DbType.String, OrgWorkTypeWaitRow.OrganType.Value)
            db.AddInParameter(dbcmd, "@WorkTypeID", DbType.String, OrgWorkTypeWaitRow.WorkTypeID.Value)
            db.AddInParameter(dbcmd, "@ValidDate", DbType.Date, OrgWorkTypeWaitRow.ValidDate.Value)
            db.AddInParameter(dbcmd, "@Seq", DbType.Int32, OrgWorkTypeWaitRow.Seq.Value)

            return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function DeleteRowByPrimaryKey(ByVal OrgWorkTypeWaitRow As Row()) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Delete From OrgWorkTypeWait")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And OrganID = @OrganID")
            strSQL.AppendLine("And OrganReason = @OrganReason")
            strSQL.AppendLine("And OrganType = @OrganType")
            strSQL.AppendLine("And WorkTypeID = @WorkTypeID")
            strSQL.AppendLine("And ValidDate = @ValidDate")
            strSQL.AppendLine("And Seq = @Seq")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            using cn As DbConnection = db.CreateConnection()
                cn.Open()
                Dim tran As DbTransaction = cn.BeginTransaction()
                Dim inTrans As Boolean = True

                Try
                    For Each r As Row In OrgWorkTypeWaitRow
                        dbcmd.Parameters.Clear()
                        db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                        db.AddInParameter(dbcmd, "@OrganID", DbType.String, r.OrganID.Value)
                        db.AddInParameter(dbcmd, "@OrganReason", DbType.String, r.OrganReason.Value)
                        db.AddInParameter(dbcmd, "@OrganType", DbType.String, r.OrganType.Value)
                        db.AddInParameter(dbcmd, "@WorkTypeID", DbType.String, r.WorkTypeID.Value)
                        db.AddInParameter(dbcmd, "@ValidDate", DbType.Date, r.ValidDate.Value)
                        db.AddInParameter(dbcmd, "@Seq", DbType.Int32, r.Seq.Value)

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

        Public Function DeleteRowByPrimaryKey(ByVal OrgWorkTypeWaitRow As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Delete From OrgWorkTypeWait")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And OrganID = @OrganID")
            strSQL.AppendLine("And OrganReason = @OrganReason")
            strSQL.AppendLine("And OrganType = @OrganType")
            strSQL.AppendLine("And WorkTypeID = @WorkTypeID")
            strSQL.AppendLine("And ValidDate = @ValidDate")
            strSQL.AppendLine("And Seq = @Seq")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())

            For Each r As Row In OrgWorkTypeWaitRow
                dbcmd.Parameters.Clear()
                db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                db.AddInParameter(dbcmd, "@OrganID", DbType.String, r.OrganID.Value)
                db.AddInParameter(dbcmd, "@OrganReason", DbType.String, r.OrganReason.Value)
                db.AddInParameter(dbcmd, "@OrganType", DbType.String, r.OrganType.Value)
                db.AddInParameter(dbcmd, "@WorkTypeID", DbType.String, r.WorkTypeID.Value)
                db.AddInParameter(dbcmd, "@ValidDate", DbType.Date, r.ValidDate.Value)
                db.AddInParameter(dbcmd, "@Seq", DbType.Int32, r.Seq.Value)

                intRowsAffected += db.ExecuteNonQuery(dbcmd, tran)
            Next
            Return intRowsAffected
        End Function

        Public Function QueryByKey(ByVal OrgWorkTypeWaitRow As Row) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From OrgWorkTypeWait")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And OrganID = @OrganID")
            strSQL.AppendLine("And OrganReason = @OrganReason")
            strSQL.AppendLine("And OrganType = @OrganType")
            strSQL.AppendLine("And WorkTypeID = @WorkTypeID")
            strSQL.AppendLine("And ValidDate = @ValidDate")
            strSQL.AppendLine("And Seq = @Seq")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, OrgWorkTypeWaitRow.CompID.Value)
            db.AddInParameter(dbcmd, "@OrganID", DbType.String, OrgWorkTypeWaitRow.OrganID.Value)
            db.AddInParameter(dbcmd, "@OrganReason", DbType.String, OrgWorkTypeWaitRow.OrganReason.Value)
            db.AddInParameter(dbcmd, "@OrganType", DbType.String, OrgWorkTypeWaitRow.OrganType.Value)
            db.AddInParameter(dbcmd, "@WorkTypeID", DbType.String, OrgWorkTypeWaitRow.WorkTypeID.Value)
            db.AddInParameter(dbcmd, "@ValidDate", DbType.Date, OrgWorkTypeWaitRow.ValidDate.Value)
            db.AddInParameter(dbcmd, "@Seq", DbType.Int32, OrgWorkTypeWaitRow.Seq.Value)

            Return db.ExecuteDataSet(dbcmd)
        End Function

        Public Function QueryByKey(OrgWorkTypeWaitRow As Row, tran As DbTransaction) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From OrgWorkTypeWait")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And OrganID = @OrganID")
            strSQL.AppendLine("And OrganReason = @OrganReason")
            strSQL.AppendLine("And OrganType = @OrganType")
            strSQL.AppendLine("And WorkTypeID = @WorkTypeID")
            strSQL.AppendLine("And ValidDate = @ValidDate")
            strSQL.AppendLine("And Seq = @Seq")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, OrgWorkTypeWaitRow.CompID.Value)
            db.AddInParameter(dbcmd, "@OrganID", DbType.String, OrgWorkTypeWaitRow.OrganID.Value)
            db.AddInParameter(dbcmd, "@OrganReason", DbType.String, OrgWorkTypeWaitRow.OrganReason.Value)
            db.AddInParameter(dbcmd, "@OrganType", DbType.String, OrgWorkTypeWaitRow.OrganType.Value)
            db.AddInParameter(dbcmd, "@WorkTypeID", DbType.String, OrgWorkTypeWaitRow.WorkTypeID.Value)
            db.AddInParameter(dbcmd, "@ValidDate", DbType.Date, OrgWorkTypeWaitRow.ValidDate.Value)
            db.AddInParameter(dbcmd, "@Seq", DbType.Int32, OrgWorkTypeWaitRow.Seq.Value)

            Return db.ExecuteDataSet(dbcmd, tran)
        End Function

        Public Function Update(ByVal OrgWorkTypeWaitRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim strDot As String = String.Empty

            strSQL.AppendLine("Update OrgWorkTypeWait Set")
            For i As Integer = 0 To OrgWorkTypeWaitRow.FieldNames.Length - 1
                If Not OrgWorkTypeWaitRow.IsIdentityField(OrgWorkTypeWaitRow.FieldNames(i)) AndAlso OrgWorkTypeWaitRow.IsUpdated(OrgWorkTypeWaitRow.FieldNames(i)) AndAlso OrgWorkTypeWaitRow.CreateUpdateSQL(OrgWorkTypeWaitRow.FieldNames(i)) Then
                    strSQL.AppendLine(string.Format("{0}{1} = @{1}", strDot, OrgWorkTypeWaitRow.FieldNames(i)))
                    strDot = ","
                End If
            Next
            If strDot = String.Empty Then Throw New Exception("未異動資料欄位！")
            strSQL.AppendLine("Where CompID = @PKCompID")
            strSQL.AppendLine("And OrganID = @PKOrganID")
            strSQL.AppendLine("And OrganReason = @PKOrganReason")
            strSQL.AppendLine("And OrganType = @PKOrganType")
            strSQL.AppendLine("And WorkTypeID = @PKWorkTypeID")
            strSQL.AppendLine("And ValidDate = @PKValidDate")
            strSQL.AppendLine("And Seq = @PKSeq")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            If OrgWorkTypeWaitRow.CompID.Updated Then db.AddInParameter(dbcmd, "@CompID", DbType.String, OrgWorkTypeWaitRow.CompID.Value)
            If OrgWorkTypeWaitRow.OrganReason.Updated Then db.AddInParameter(dbcmd, "@OrganReason", DbType.String, OrgWorkTypeWaitRow.OrganReason.Value)
            If OrgWorkTypeWaitRow.OrganType.Updated Then db.AddInParameter(dbcmd, "@OrganType", DbType.String, OrgWorkTypeWaitRow.OrganType.Value)
            If OrgWorkTypeWaitRow.ValidDate.Updated Then db.AddInParameter(dbcmd, "@ValidDate", DbType.Date, IIf(IsDateTimeNull(OrgWorkTypeWaitRow.ValidDate.Value), Convert.ToDateTime("1900/1/1"), OrgWorkTypeWaitRow.ValidDate.Value))
            If OrgWorkTypeWaitRow.Seq.Updated Then db.AddInParameter(dbcmd, "@Seq", DbType.Int32, OrgWorkTypeWaitRow.Seq.Value)
            If OrgWorkTypeWaitRow.OrganID.Updated Then db.AddInParameter(dbcmd, "@OrganID", DbType.String, OrgWorkTypeWaitRow.OrganID.Value)
            If OrgWorkTypeWaitRow.WorkTypeID.Updated Then db.AddInParameter(dbcmd, "@WorkTypeID", DbType.String, OrgWorkTypeWaitRow.WorkTypeID.Value)
            If OrgWorkTypeWaitRow.PrincipalFlag.Updated Then db.AddInParameter(dbcmd, "@PrincipalFlag", DbType.String, OrgWorkTypeWaitRow.PrincipalFlag.Value)
            If OrgWorkTypeWaitRow.WaitStatus.Updated Then db.AddInParameter(dbcmd, "@WaitStatus", DbType.String, OrgWorkTypeWaitRow.WaitStatus.Value)
            If OrgWorkTypeWaitRow.LastChgComp.Updated Then db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, OrgWorkTypeWaitRow.LastChgComp.Value)
            If OrgWorkTypeWaitRow.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, OrgWorkTypeWaitRow.LastChgID.Value)
            If OrgWorkTypeWaitRow.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(OrgWorkTypeWaitRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), OrgWorkTypeWaitRow.LastChgDate.Value))
            db.AddInParameter(dbcmd, "@PKCompID", DbType.String, IIf(OrgWorkTypeWaitRow.LoadFromDataRow, OrgWorkTypeWaitRow.CompID.OldValue, OrgWorkTypeWaitRow.CompID.Value))
            db.AddInParameter(dbcmd, "@PKOrganID", DbType.String, IIf(OrgWorkTypeWaitRow.LoadFromDataRow, OrgWorkTypeWaitRow.OrganID.OldValue, OrgWorkTypeWaitRow.OrganID.Value))
            db.AddInParameter(dbcmd, "@PKOrganReason", DbType.String, IIf(OrgWorkTypeWaitRow.LoadFromDataRow, OrgWorkTypeWaitRow.OrganReason.OldValue, OrgWorkTypeWaitRow.OrganReason.Value))
            db.AddInParameter(dbcmd, "@PKOrganType", DbType.String, IIf(OrgWorkTypeWaitRow.LoadFromDataRow, OrgWorkTypeWaitRow.OrganType.OldValue, OrgWorkTypeWaitRow.OrganType.Value))
            db.AddInParameter(dbcmd, "@PKWorkTypeID", DbType.String, IIf(OrgWorkTypeWaitRow.LoadFromDataRow, OrgWorkTypeWaitRow.WorkTypeID.OldValue, OrgWorkTypeWaitRow.WorkTypeID.Value))
            db.AddInParameter(dbcmd, "@PKValidDate", DbType.Date, IIf(OrgWorkTypeWaitRow.LoadFromDataRow, OrgWorkTypeWaitRow.ValidDate.OldValue, OrgWorkTypeWaitRow.ValidDate.Value))
            db.AddInParameter(dbcmd, "@PKSeq", DbType.Int32, IIf(OrgWorkTypeWaitRow.LoadFromDataRow, OrgWorkTypeWaitRow.Seq.OldValue, OrgWorkTypeWaitRow.Seq.Value))

            Return db.ExecuteNonQuery(dbcmd)
        End Function

        Public Function Update(ByVal OrgWorkTypeWaitRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim strDot As String = String.Empty

            strSQL.AppendLine("Update OrgWorkTypeWait Set")
            For i As Integer = 0 To OrgWorkTypeWaitRow.FieldNames.Length - 1
                If Not OrgWorkTypeWaitRow.IsIdentityField(OrgWorkTypeWaitRow.FieldNames(i)) AndAlso OrgWorkTypeWaitRow.IsUpdated(OrgWorkTypeWaitRow.FieldNames(i)) AndAlso OrgWorkTypeWaitRow.CreateUpdateSQL(OrgWorkTypeWaitRow.FieldNames(i)) Then
                    strSQL.AppendLine(string.Format("{0}{1} = @{1}", strDot, OrgWorkTypeWaitRow.FieldNames(i)))
                    strDot = ","
                End If
            Next 
            If strDot = String.Empty Then Throw New Exception("未異動資料欄位！")
            strSQL.AppendLine("Where CompID = @PKCompID")
            strSQL.AppendLine("And OrganID = @PKOrganID")
            strSQL.AppendLine("And OrganReason = @PKOrganReason")
            strSQL.AppendLine("And OrganType = @PKOrganType")
            strSQL.AppendLine("And WorkTypeID = @PKWorkTypeID")
            strSQL.AppendLine("And ValidDate = @PKValidDate")
            strSQL.AppendLine("And Seq = @PKSeq")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            If OrgWorkTypeWaitRow.CompID.Updated Then db.AddInParameter(dbcmd, "@CompID", DbType.String, OrgWorkTypeWaitRow.CompID.Value)
            If OrgWorkTypeWaitRow.OrganReason.Updated Then db.AddInParameter(dbcmd, "@OrganReason", DbType.String, OrgWorkTypeWaitRow.OrganReason.Value)
            If OrgWorkTypeWaitRow.OrganType.Updated Then db.AddInParameter(dbcmd, "@OrganType", DbType.String, OrgWorkTypeWaitRow.OrganType.Value)
            If OrgWorkTypeWaitRow.ValidDate.Updated Then db.AddInParameter(dbcmd, "@ValidDate", DbType.Date, IIf(IsDateTimeNull(OrgWorkTypeWaitRow.ValidDate.Value), Convert.ToDateTime("1900/1/1"), OrgWorkTypeWaitRow.ValidDate.Value))
            If OrgWorkTypeWaitRow.Seq.Updated Then db.AddInParameter(dbcmd, "@Seq", DbType.Int32, OrgWorkTypeWaitRow.Seq.Value)
            If OrgWorkTypeWaitRow.OrganID.Updated Then db.AddInParameter(dbcmd, "@OrganID", DbType.String, OrgWorkTypeWaitRow.OrganID.Value)
            If OrgWorkTypeWaitRow.WorkTypeID.Updated Then db.AddInParameter(dbcmd, "@WorkTypeID", DbType.String, OrgWorkTypeWaitRow.WorkTypeID.Value)
            If OrgWorkTypeWaitRow.PrincipalFlag.Updated Then db.AddInParameter(dbcmd, "@PrincipalFlag", DbType.String, OrgWorkTypeWaitRow.PrincipalFlag.Value)
            If OrgWorkTypeWaitRow.WaitStatus.Updated Then db.AddInParameter(dbcmd, "@WaitStatus", DbType.String, OrgWorkTypeWaitRow.WaitStatus.Value)
            If OrgWorkTypeWaitRow.LastChgComp.Updated Then db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, OrgWorkTypeWaitRow.LastChgComp.Value)
            If OrgWorkTypeWaitRow.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, OrgWorkTypeWaitRow.LastChgID.Value)
            If OrgWorkTypeWaitRow.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(OrgWorkTypeWaitRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), OrgWorkTypeWaitRow.LastChgDate.Value))
            db.AddInParameter(dbcmd, "@PKCompID", DbType.String, IIf(OrgWorkTypeWaitRow.LoadFromDataRow, OrgWorkTypeWaitRow.CompID.OldValue, OrgWorkTypeWaitRow.CompID.Value))
            db.AddInParameter(dbcmd, "@PKOrganID", DbType.String, IIf(OrgWorkTypeWaitRow.LoadFromDataRow, OrgWorkTypeWaitRow.OrganID.OldValue, OrgWorkTypeWaitRow.OrganID.Value))
            db.AddInParameter(dbcmd, "@PKOrganReason", DbType.String, IIf(OrgWorkTypeWaitRow.LoadFromDataRow, OrgWorkTypeWaitRow.OrganReason.OldValue, OrgWorkTypeWaitRow.OrganReason.Value))
            db.AddInParameter(dbcmd, "@PKOrganType", DbType.String, IIf(OrgWorkTypeWaitRow.LoadFromDataRow, OrgWorkTypeWaitRow.OrganType.OldValue, OrgWorkTypeWaitRow.OrganType.Value))
            db.AddInParameter(dbcmd, "@PKWorkTypeID", DbType.String, IIf(OrgWorkTypeWaitRow.LoadFromDataRow, OrgWorkTypeWaitRow.WorkTypeID.OldValue, OrgWorkTypeWaitRow.WorkTypeID.Value))
            db.AddInParameter(dbcmd, "@PKValidDate", DbType.Date, IIf(OrgWorkTypeWaitRow.LoadFromDataRow, OrgWorkTypeWaitRow.ValidDate.OldValue, OrgWorkTypeWaitRow.ValidDate.Value))
            db.AddInParameter(dbcmd, "@PKSeq", DbType.Int32, IIf(OrgWorkTypeWaitRow.LoadFromDataRow, OrgWorkTypeWaitRow.Seq.OldValue, OrgWorkTypeWaitRow.Seq.Value))

            Return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function Update(ByVal OrgWorkTypeWaitRow As Row()) As Integer
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
                    For Each r As Row In OrgWorkTypeWaitRow
                        strDot = String.Empty
                        If strSQL.Length > 0 Then strSQL.Remove(0, strSQL.Length)
                        strSQL.AppendLine("Update OrgWorkTypeWait Set")
                        For i As Integer = 0 To r.FieldNames.Length - 1
                            If Not r.IsIdentityField(r.FieldNames(i)) AndAlso r.IsUpdated(r.FieldNames(i)) AndAlso r.CreateUpdateSQL(r.FieldNames(i)) Then
                                strSQL.AppendLine(String.Format("{0}{1} = @{1}", strDot, r.FieldNames(i)))
                                strDot = ","
                            End If
                        Next
                        If strDot = String.Empty Then Continue For
                        strSQL.AppendLine("Where CompID = @PKCompID")
                        strSQL.AppendLine("And OrganID = @PKOrganID")
                        strSQL.AppendLine("And OrganReason = @PKOrganReason")
                        strSQL.AppendLine("And OrganType = @PKOrganType")
                        strSQL.AppendLine("And WorkTypeID = @PKWorkTypeID")
                        strSQL.AppendLine("And ValidDate = @PKValidDate")
                        strSQL.AppendLine("And Seq = @PKSeq")

                        dbcmd = db.GetSqlStringCommand(strSQL.ToString())
                        If r.CompID.Updated Then db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                        If r.OrganReason.Updated Then db.AddInParameter(dbcmd, "@OrganReason", DbType.String, r.OrganReason.Value)
                        If r.OrganType.Updated Then db.AddInParameter(dbcmd, "@OrganType", DbType.String, r.OrganType.Value)
                        If r.ValidDate.Updated Then db.AddInParameter(dbcmd, "@ValidDate", DbType.Date, IIf(IsDateTimeNull(r.ValidDate.Value), Convert.ToDateTime("1900/1/1"), r.ValidDate.Value))
                        If r.Seq.Updated Then db.AddInParameter(dbcmd, "@Seq", DbType.Int32, r.Seq.Value)
                        If r.OrganID.Updated Then db.AddInParameter(dbcmd, "@OrganID", DbType.String, r.OrganID.Value)
                        If r.WorkTypeID.Updated Then db.AddInParameter(dbcmd, "@WorkTypeID", DbType.String, r.WorkTypeID.Value)
                        If r.PrincipalFlag.Updated Then db.AddInParameter(dbcmd, "@PrincipalFlag", DbType.String, r.PrincipalFlag.Value)
                        If r.WaitStatus.Updated Then db.AddInParameter(dbcmd, "@WaitStatus", DbType.String, r.WaitStatus.Value)
                        If r.LastChgComp.Updated Then db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, r.LastChgComp.Value)
                        If r.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, r.LastChgID.Value)
                        If r.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(r.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), r.LastChgDate.Value))
                        db.AddInParameter(dbcmd, "@PKCompID", DbType.String, IIf(r.LoadFromDataRow, r.CompID.OldValue, r.CompID.Value))
                        db.AddInParameter(dbcmd, "@PKOrganID", DbType.String, IIf(r.LoadFromDataRow, r.OrganID.OldValue, r.OrganID.Value))
                        db.AddInParameter(dbcmd, "@PKOrganReason", DbType.String, IIf(r.LoadFromDataRow, r.OrganReason.OldValue, r.OrganReason.Value))
                        db.AddInParameter(dbcmd, "@PKOrganType", DbType.String, IIf(r.LoadFromDataRow, r.OrganType.OldValue, r.OrganType.Value))
                        db.AddInParameter(dbcmd, "@PKWorkTypeID", DbType.String, IIf(r.LoadFromDataRow, r.WorkTypeID.OldValue, r.WorkTypeID.Value))
                        db.AddInParameter(dbcmd, "@PKValidDate", DbType.Date, IIf(r.LoadFromDataRow, r.ValidDate.OldValue, r.ValidDate.Value))
                        db.AddInParameter(dbcmd, "@PKSeq", DbType.Int32, IIf(r.LoadFromDataRow, r.Seq.OldValue, r.Seq.Value))

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

        Public Function Update(ByVal OrgWorkTypeWaitRow As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0
            Dim strDot As String

            For Each r As Row In OrgWorkTypeWaitRow
                strDot = String.Empty
                If strSQL.Length > 0 Then strSQL.Remove(0, strSQL.Length)
                strSQL.AppendLine("Update OrgWorkTypeWait Set")
                For i As Integer = 0 To r.FieldNames.Length - 1
                    If Not r.IsIdentityField(r.FieldNames(i)) AndAlso r.IsUpdated(r.FieldNames(i)) AndAlso r.CreateUpdateSQL(r.FieldNames(i))
                        strSQL.AppendLine(String.Format("{0}{1} = @{1}", strDot, r.FieldNames(i)))
                        strDot = ","
                    End If
                Next
                If strDot = String.Empty Then Continue For
                strSQL.AppendLine("Where CompID = @PKCompID")
                strSQL.AppendLine("And OrganID = @PKOrganID")
                strSQL.AppendLine("And OrganReason = @PKOrganReason")
                strSQL.AppendLine("And OrganType = @PKOrganType")
                strSQL.AppendLine("And WorkTypeID = @PKWorkTypeID")
                strSQL.AppendLine("And ValidDate = @PKValidDate")
                strSQL.AppendLine("And Seq = @PKSeq")

                dbcmd = db.GetSqlStringCommand(strSQL.ToString())
                If r.CompID.Updated Then db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                If r.OrganReason.Updated Then db.AddInParameter(dbcmd, "@OrganReason", DbType.String, r.OrganReason.Value)
                If r.OrganType.Updated Then db.AddInParameter(dbcmd, "@OrganType", DbType.String, r.OrganType.Value)
                If r.ValidDate.Updated Then db.AddInParameter(dbcmd, "@ValidDate", DbType.Date, IIf(IsDateTimeNull(r.ValidDate.Value), Convert.ToDateTime("1900/1/1"), r.ValidDate.Value))
                If r.Seq.Updated Then db.AddInParameter(dbcmd, "@Seq", DbType.Int32, r.Seq.Value)
                If r.OrganID.Updated Then db.AddInParameter(dbcmd, "@OrganID", DbType.String, r.OrganID.Value)
                If r.WorkTypeID.Updated Then db.AddInParameter(dbcmd, "@WorkTypeID", DbType.String, r.WorkTypeID.Value)
                If r.PrincipalFlag.Updated Then db.AddInParameter(dbcmd, "@PrincipalFlag", DbType.String, r.PrincipalFlag.Value)
                If r.WaitStatus.Updated Then db.AddInParameter(dbcmd, "@WaitStatus", DbType.String, r.WaitStatus.Value)
                If r.LastChgComp.Updated Then db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, r.LastChgComp.Value)
                If r.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, r.LastChgID.Value)
                If r.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(r.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), r.LastChgDate.Value))
                db.AddInParameter(dbcmd, "@PKCompID", DbType.String, IIf(r.LoadFromDataRow, r.CompID.OldValue, r.CompID.Value))
                db.AddInParameter(dbcmd, "@PKOrganID", DbType.String, IIf(r.LoadFromDataRow, r.OrganID.OldValue, r.OrganID.Value))
                db.AddInParameter(dbcmd, "@PKOrganReason", DbType.String, IIf(r.LoadFromDataRow, r.OrganReason.OldValue, r.OrganReason.Value))
                db.AddInParameter(dbcmd, "@PKOrganType", DbType.String, IIf(r.LoadFromDataRow, r.OrganType.OldValue, r.OrganType.Value))
                db.AddInParameter(dbcmd, "@PKWorkTypeID", DbType.String, IIf(r.LoadFromDataRow, r.WorkTypeID.OldValue, r.WorkTypeID.Value))
                db.AddInParameter(dbcmd, "@PKValidDate", DbType.Date, IIf(r.LoadFromDataRow, r.ValidDate.OldValue, r.ValidDate.Value))
                db.AddInParameter(dbcmd, "@PKSeq", DbType.Int32, IIf(r.LoadFromDataRow, r.Seq.OldValue, r.Seq.Value))

                intRowsAffected += db.ExecuteNonQuery(dbcmd, tran)
            Next
            Return intRowsAffected
        End Function

        Public Function IsDataExists(ByVal OrgWorkTypeWaitRow As Row) As Boolean
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intCount As Integer = 0

            strSQL.AppendLine("Select Count(*) Cnt From OrgWorkTypeWait")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And OrganID = @OrganID")
            strSQL.AppendLine("And OrganReason = @OrganReason")
            strSQL.AppendLine("And OrganType = @OrganType")
            strSQL.AppendLine("And WorkTypeID = @WorkTypeID")
            strSQL.AppendLine("And ValidDate = @ValidDate")
            strSQL.AppendLine("And Seq = @Seq")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, OrgWorkTypeWaitRow.CompID.Value)
            db.AddInParameter(dbcmd, "@OrganID", DbType.String, OrgWorkTypeWaitRow.OrganID.Value)
            db.AddInParameter(dbcmd, "@OrganReason", DbType.String, OrgWorkTypeWaitRow.OrganReason.Value)
            db.AddInParameter(dbcmd, "@OrganType", DbType.String, OrgWorkTypeWaitRow.OrganType.Value)
            db.AddInParameter(dbcmd, "@WorkTypeID", DbType.String, OrgWorkTypeWaitRow.WorkTypeID.Value)
            db.AddInParameter(dbcmd, "@ValidDate", DbType.Date, OrgWorkTypeWaitRow.ValidDate.Value)
            db.AddInParameter(dbcmd, "@Seq", DbType.Int32, OrgWorkTypeWaitRow.Seq.Value)

            intCount = Convert.ToInt32(db.ExecuteScalar(dbcmd))
            Return IIf(intCount > 0, True, False)
        End Function

        Public Function IsDataExists(ByVal OrgWorkTypeWaitRow As Row, ByVal tran As DbTransaction) As Boolean
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intCount As Integer = 0

            strSQL.AppendLine("Select Count(*) Cnt From OrgWorkTypeWait")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And OrganID = @OrganID")
            strSQL.AppendLine("And OrganReason = @OrganReason")
            strSQL.AppendLine("And OrganType = @OrganType")
            strSQL.AppendLine("And WorkTypeID = @WorkTypeID")
            strSQL.AppendLine("And ValidDate = @ValidDate")
            strSQL.AppendLine("And Seq = @Seq")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, OrgWorkTypeWaitRow.CompID.Value)
            db.AddInParameter(dbcmd, "@OrganID", DbType.String, OrgWorkTypeWaitRow.OrganID.Value)
            db.AddInParameter(dbcmd, "@OrganReason", DbType.String, OrgWorkTypeWaitRow.OrganReason.Value)
            db.AddInParameter(dbcmd, "@OrganType", DbType.String, OrgWorkTypeWaitRow.OrganType.Value)
            db.AddInParameter(dbcmd, "@WorkTypeID", DbType.String, OrgWorkTypeWaitRow.WorkTypeID.Value)
            db.AddInParameter(dbcmd, "@ValidDate", DbType.Date, OrgWorkTypeWaitRow.ValidDate.Value)
            db.AddInParameter(dbcmd, "@Seq", DbType.Int32, OrgWorkTypeWaitRow.Seq.Value)

            intCount = Convert.ToInt32(db.ExecuteScalar(dbcmd, tran))
            Return IIf(intCount > 0, True, False)
        End Function

        Public Function QuerybyWhere(WhereCondition As String) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From OrgWorkTypeWait")
            If WhereCondition <> String.Empty Then strSQL.AppendLine(WhereCondition)

            Return db.ExecuteDataSet(CommandType.Text, strSQL.ToString())
        End Function

        Public Function Insert(ByVal OrgWorkTypeWaitRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Insert into OrgWorkTypeWait")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    CompID, OrganReason, OrganType, ValidDate, Seq, OrganID, WorkTypeID, PrincipalFlag,")
            strSQL.AppendLine("    WaitStatus, LastChgComp, LastChgID, LastChgDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @CompID, @OrganReason, @OrganType, @ValidDate, @Seq, @OrganID, @WorkTypeID, @PrincipalFlag,")
            strSQL.AppendLine("    @WaitStatus, @LastChgComp, @LastChgID, @LastChgDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, OrgWorkTypeWaitRow.CompID.Value)
            db.AddInParameter(dbcmd, "@OrganReason", DbType.String, OrgWorkTypeWaitRow.OrganReason.Value)
            db.AddInParameter(dbcmd, "@OrganType", DbType.String, OrgWorkTypeWaitRow.OrganType.Value)
            db.AddInParameter(dbcmd, "@ValidDate", DbType.Date, IIf(IsDateTimeNull(OrgWorkTypeWaitRow.ValidDate.Value), Convert.ToDateTime("1900/1/1"), OrgWorkTypeWaitRow.ValidDate.Value))
            db.AddInParameter(dbcmd, "@Seq", DbType.Int32, OrgWorkTypeWaitRow.Seq.Value)
            db.AddInParameter(dbcmd, "@OrganID", DbType.String, OrgWorkTypeWaitRow.OrganID.Value)
            db.AddInParameter(dbcmd, "@WorkTypeID", DbType.String, OrgWorkTypeWaitRow.WorkTypeID.Value)
            db.AddInParameter(dbcmd, "@PrincipalFlag", DbType.String, OrgWorkTypeWaitRow.PrincipalFlag.Value)
            db.AddInParameter(dbcmd, "@WaitStatus", DbType.String, OrgWorkTypeWaitRow.WaitStatus.Value)
            db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, OrgWorkTypeWaitRow.LastChgComp.Value)
            db.AddInParameter(dbcmd, "@LastChgID", DbType.String, OrgWorkTypeWaitRow.LastChgID.Value)
            db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(OrgWorkTypeWaitRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), OrgWorkTypeWaitRow.LastChgDate.Value))

            Return db.ExecuteNonQuery(dbcmd)
        End Function

        Public Function Insert(ByVal OrgWorkTypeWaitRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Insert into OrgWorkTypeWait")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    CompID, OrganReason, OrganType, ValidDate, Seq, OrganID, WorkTypeID, PrincipalFlag,")
            strSQL.AppendLine("    WaitStatus, LastChgComp, LastChgID, LastChgDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @CompID, @OrganReason, @OrganType, @ValidDate, @Seq, @OrganID, @WorkTypeID, @PrincipalFlag,")
            strSQL.AppendLine("    @WaitStatus, @LastChgComp, @LastChgID, @LastChgDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, OrgWorkTypeWaitRow.CompID.Value)
            db.AddInParameter(dbcmd, "@OrganReason", DbType.String, OrgWorkTypeWaitRow.OrganReason.Value)
            db.AddInParameter(dbcmd, "@OrganType", DbType.String, OrgWorkTypeWaitRow.OrganType.Value)
            db.AddInParameter(dbcmd, "@ValidDate", DbType.Date, IIf(IsDateTimeNull(OrgWorkTypeWaitRow.ValidDate.Value), Convert.ToDateTime("1900/1/1"), OrgWorkTypeWaitRow.ValidDate.Value))
            db.AddInParameter(dbcmd, "@Seq", DbType.Int32, OrgWorkTypeWaitRow.Seq.Value)
            db.AddInParameter(dbcmd, "@OrganID", DbType.String, OrgWorkTypeWaitRow.OrganID.Value)
            db.AddInParameter(dbcmd, "@WorkTypeID", DbType.String, OrgWorkTypeWaitRow.WorkTypeID.Value)
            db.AddInParameter(dbcmd, "@PrincipalFlag", DbType.String, OrgWorkTypeWaitRow.PrincipalFlag.Value)
            db.AddInParameter(dbcmd, "@WaitStatus", DbType.String, OrgWorkTypeWaitRow.WaitStatus.Value)
            db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, OrgWorkTypeWaitRow.LastChgComp.Value)
            db.AddInParameter(dbcmd, "@LastChgID", DbType.String, OrgWorkTypeWaitRow.LastChgID.Value)
            db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(OrgWorkTypeWaitRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), OrgWorkTypeWaitRow.LastChgDate.Value))

            Return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function Insert(ByVal OrgWorkTypeWaitRow As Row()) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Insert into OrgWorkTypeWait")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    CompID, OrganReason, OrganType, ValidDate, Seq, OrganID, WorkTypeID, PrincipalFlag,")
            strSQL.AppendLine("    WaitStatus, LastChgComp, LastChgID, LastChgDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @CompID, @OrganReason, @OrganType, @ValidDate, @Seq, @OrganID, @WorkTypeID, @PrincipalFlag,")
            strSQL.AppendLine("    @WaitStatus, @LastChgComp, @LastChgID, @LastChgDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            Using cn As DbConnection = db.CreateConnection()
                cn.Open()
                Dim tran As DbTransaction = cn.BeginTransaction()
                Dim inTrans As Boolean = True

                Try
                    For Each r As Row In OrgWorkTypeWaitRow
                        dbcmd.Parameters.Clear()
                        db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                        db.AddInParameter(dbcmd, "@OrganReason", DbType.String, r.OrganReason.Value)
                        db.AddInParameter(dbcmd, "@OrganType", DbType.String, r.OrganType.Value)
                        db.AddInParameter(dbcmd, "@ValidDate", DbType.Date, IIf(IsDateTimeNull(r.ValidDate.Value), Convert.ToDateTime("1900/1/1"), r.ValidDate.Value))
                        db.AddInParameter(dbcmd, "@Seq", DbType.Int32, r.Seq.Value)
                        db.AddInParameter(dbcmd, "@OrganID", DbType.String, r.OrganID.Value)
                        db.AddInParameter(dbcmd, "@WorkTypeID", DbType.String, r.WorkTypeID.Value)
                        db.AddInParameter(dbcmd, "@PrincipalFlag", DbType.String, r.PrincipalFlag.Value)
                        db.AddInParameter(dbcmd, "@WaitStatus", DbType.String, r.WaitStatus.Value)
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

        Public Function Insert(ByVal OrgWorkTypeWaitRow As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Insert into OrgWorkTypeWait")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    CompID, OrganReason, OrganType, ValidDate, Seq, OrganID, WorkTypeID, PrincipalFlag,")
            strSQL.AppendLine("    WaitStatus, LastChgComp, LastChgID, LastChgDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @CompID, @OrganReason, @OrganType, @ValidDate, @Seq, @OrganID, @WorkTypeID, @PrincipalFlag,")
            strSQL.AppendLine("    @WaitStatus, @LastChgComp, @LastChgID, @LastChgDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())

            For Each r As Row In OrgWorkTypeWaitRow
                dbcmd.Parameters.Clear()
                db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                db.AddInParameter(dbcmd, "@OrganReason", DbType.String, r.OrganReason.Value)
                db.AddInParameter(dbcmd, "@OrganType", DbType.String, r.OrganType.Value)
                db.AddInParameter(dbcmd, "@ValidDate", DbType.Date, IIf(IsDateTimeNull(r.ValidDate.Value), Convert.ToDateTime("1900/1/1"), r.ValidDate.Value))
                db.AddInParameter(dbcmd, "@Seq", DbType.Int32, r.Seq.Value)
                db.AddInParameter(dbcmd, "@OrganID", DbType.String, r.OrganID.Value)
                db.AddInParameter(dbcmd, "@WorkTypeID", DbType.String, r.WorkTypeID.Value)
                db.AddInParameter(dbcmd, "@PrincipalFlag", DbType.String, r.PrincipalFlag.Value)
                db.AddInParameter(dbcmd, "@WaitStatus", DbType.String, r.WaitStatus.Value)
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

