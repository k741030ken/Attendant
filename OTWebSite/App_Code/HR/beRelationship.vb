'****************************************************************
' Table:Relationship
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

Namespace beRelationship
    Public Class Table
        Private m_Rows As Rows = New Rows()
        Private m_Fields As String() = { "RelativeID", "HeaRelID", "CompRelID", "DeathPayID", "TaxFamilyID", "RelTypeID", "RelClassID", "Remark", "RemarkCN", "LastChgComp" _
                                    , "LastChgID", "LastChgDate" }
        Private m_Types As System.Type() = { GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String) _
                                    , GetType(String), GetType(Date) }
        Private m_PrimaryFields As String() = { "RelativeID" }

        Public ReadOnly Property Rows() As beRelationship.Rows 
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
        Public Sub Transfer2Row(RelationshipTable As DataTable)
            For Each dr As DataRow In RelationshipTable.Rows
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

                dr(m_Rows(i).RelativeID.FieldName) = m_Rows(i).RelativeID.Value
                dr(m_Rows(i).HeaRelID.FieldName) = m_Rows(i).HeaRelID.Value
                dr(m_Rows(i).CompRelID.FieldName) = m_Rows(i).CompRelID.Value
                dr(m_Rows(i).DeathPayID.FieldName) = m_Rows(i).DeathPayID.Value
                dr(m_Rows(i).TaxFamilyID.FieldName) = m_Rows(i).TaxFamilyID.Value
                dr(m_Rows(i).RelTypeID.FieldName) = m_Rows(i).RelTypeID.Value
                dr(m_Rows(i).RelClassID.FieldName) = m_Rows(i).RelClassID.Value
                dr(m_Rows(i).Remark.FieldName) = m_Rows(i).Remark.Value
                dr(m_Rows(i).RemarkCN.FieldName) = m_Rows(i).RemarkCN.Value
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

        Public Sub Add(RelationshipRow As Row)
            m_Rows.Add(RelationshipRow)
        End Sub

        Public Sub Remove(RelationshipRow As Row)
            If m_Rows.IndexOf(RelationshipRow) >= 0 Then
                m_Rows.Remove(RelationshipRow)
            End If
        End Sub

        Public Sub Dispose()
            m_Rows.Clear()
        End Sub

    End Class

    Public Class Row

        Private FI_RelativeID As Field(Of String) = new Field(Of String)("RelativeID", true)
        Private FI_HeaRelID As Field(Of String) = new Field(Of String)("HeaRelID", true)
        Private FI_CompRelID As Field(Of String) = new Field(Of String)("CompRelID", true)
        Private FI_DeathPayID As Field(Of String) = new Field(Of String)("DeathPayID", true)
        Private FI_TaxFamilyID As Field(Of String) = new Field(Of String)("TaxFamilyID", true)
        Private FI_RelTypeID As Field(Of String) = new Field(Of String)("RelTypeID", true)
        Private FI_RelClassID As Field(Of String) = new Field(Of String)("RelClassID", true)
        Private FI_Remark As Field(Of String) = new Field(Of String)("Remark", true)
        Private FI_RemarkCN As Field(Of String) = new Field(Of String)("RemarkCN", true)
        Private FI_LastChgComp As Field(Of String) = new Field(Of String)("LastChgComp", true)
        Private FI_LastChgID As Field(Of String) = new Field(Of String)("LastChgID", true)
        Private FI_LastChgDate As Field(Of Date) = new Field(Of Date)("LastChgDate", true)
        Private m_FieldNames As String() = { "RelativeID", "HeaRelID", "CompRelID", "DeathPayID", "TaxFamilyID", "RelTypeID", "RelClassID", "Remark", "RemarkCN", "LastChgComp" _
                                    , "LastChgID", "LastChgDate" }
        Private m_PrimaryFields As String() = { "RelativeID" }
        Private m_IdentityFields As String() = {  }
        Private m_LoadFromDataRow As Boolean = False

        Private Function GetFieldValue(ByVal fieldName As String) As Object
            Select Case fieldName
                Case "RelativeID"
                    Return FI_RelativeID.Value
                Case "HeaRelID"
                    Return FI_HeaRelID.Value
                Case "CompRelID"
                    Return FI_CompRelID.Value
                Case "DeathPayID"
                    Return FI_DeathPayID.Value
                Case "TaxFamilyID"
                    Return FI_TaxFamilyID.Value
                Case "RelTypeID"
                    Return FI_RelTypeID.Value
                Case "RelClassID"
                    Return FI_RelClassID.Value
                Case "Remark"
                    Return FI_Remark.Value
                Case "RemarkCN"
                    Return FI_RemarkCN.Value
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
                Case "RelativeID"
                    FI_RelativeID.SetValue(value)
                Case "HeaRelID"
                    FI_HeaRelID.SetValue(value)
                Case "CompRelID"
                    FI_CompRelID.SetValue(value)
                Case "DeathPayID"
                    FI_DeathPayID.SetValue(value)
                Case "TaxFamilyID"
                    FI_TaxFamilyID.SetValue(value)
                Case "RelTypeID"
                    FI_RelTypeID.SetValue(value)
                Case "RelClassID"
                    FI_RelClassID.SetValue(value)
                Case "Remark"
                    FI_Remark.SetValue(value)
                Case "RemarkCN"
                    FI_RemarkCN.SetValue(value)
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
                Case "RelativeID"
                    return FI_RelativeID.Updated
                Case "HeaRelID"
                    return FI_HeaRelID.Updated
                Case "CompRelID"
                    return FI_CompRelID.Updated
                Case "DeathPayID"
                    return FI_DeathPayID.Updated
                Case "TaxFamilyID"
                    return FI_TaxFamilyID.Updated
                Case "RelTypeID"
                    return FI_RelTypeID.Updated
                Case "RelClassID"
                    return FI_RelClassID.Updated
                Case "Remark"
                    return FI_Remark.Updated
                Case "RemarkCN"
                    return FI_RemarkCN.Updated
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
                Case "RelativeID"
                    return FI_RelativeID.CreateUpdateSQL
                Case "HeaRelID"
                    return FI_HeaRelID.CreateUpdateSQL
                Case "CompRelID"
                    return FI_CompRelID.CreateUpdateSQL
                Case "DeathPayID"
                    return FI_DeathPayID.CreateUpdateSQL
                Case "TaxFamilyID"
                    return FI_TaxFamilyID.CreateUpdateSQL
                Case "RelTypeID"
                    return FI_RelTypeID.CreateUpdateSQL
                Case "RelClassID"
                    return FI_RelClassID.CreateUpdateSQL
                Case "Remark"
                    return FI_Remark.CreateUpdateSQL
                Case "RemarkCN"
                    return FI_RemarkCN.CreateUpdateSQL
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
            FI_RelativeID.SetInitValue("")
            FI_HeaRelID.SetInitValue("")
            FI_CompRelID.SetInitValue("")
            FI_DeathPayID.SetInitValue("")
            FI_TaxFamilyID.SetInitValue("")
            FI_RelTypeID.SetInitValue("")
            FI_RelClassID.SetInitValue("")
            FI_Remark.SetInitValue("")
            FI_RemarkCN.SetInitValue("")
            FI_LastChgComp.SetInitValue("")
            FI_LastChgID.SetInitValue("")
            FI_LastChgDate.SetInitValue(Convert.ToDateTime("1900/01/01"))
        End Sub

        Public Sub New(ByVal dr As System.Data.DataRow)
            FI_RelativeID.SetInitValue(dr("RelativeID"))
            FI_HeaRelID.SetInitValue(dr("HeaRelID"))
            FI_CompRelID.SetInitValue(dr("CompRelID"))
            FI_DeathPayID.SetInitValue(dr("DeathPayID"))
            FI_TaxFamilyID.SetInitValue(dr("TaxFamilyID"))
            FI_RelTypeID.SetInitValue(dr("RelTypeID"))
            FI_RelClassID.SetInitValue(dr("RelClassID"))
            FI_Remark.SetInitValue(dr("Remark"))
            FI_RemarkCN.SetInitValue(dr("RemarkCN"))
            FI_LastChgComp.SetInitValue(dr("LastChgComp"))
            FI_LastChgID.SetInitValue(dr("LastChgID"))
            FI_LastChgDate.SetInitValue(dr("LastChgDate"))

            m_LoadFromDataRow = True
        End Sub

        Private Sub ClearUpdated()
            FI_RelativeID.Updated = False
            FI_HeaRelID.Updated = False
            FI_CompRelID.Updated = False
            FI_DeathPayID.Updated = False
            FI_TaxFamilyID.Updated = False
            FI_RelTypeID.Updated = False
            FI_RelClassID.Updated = False
            FI_Remark.Updated = False
            FI_RemarkCN.Updated = False
            FI_LastChgComp.Updated = False
            FI_LastChgID.Updated = False
            FI_LastChgDate.Updated = False
        End Sub

        Public ReadOnly Property RelativeID As Field(Of String) 
            Get
                Return FI_RelativeID
            End Get
        End Property

        Public ReadOnly Property HeaRelID As Field(Of String) 
            Get
                Return FI_HeaRelID
            End Get
        End Property

        Public ReadOnly Property CompRelID As Field(Of String) 
            Get
                Return FI_CompRelID
            End Get
        End Property

        Public ReadOnly Property DeathPayID As Field(Of String) 
            Get
                Return FI_DeathPayID
            End Get
        End Property

        Public ReadOnly Property TaxFamilyID As Field(Of String) 
            Get
                Return FI_TaxFamilyID
            End Get
        End Property

        Public ReadOnly Property RelTypeID As Field(Of String) 
            Get
                Return FI_RelTypeID
            End Get
        End Property

        Public ReadOnly Property RelClassID As Field(Of String) 
            Get
                Return FI_RelClassID
            End Get
        End Property

        Public ReadOnly Property Remark As Field(Of String) 
            Get
                Return FI_Remark
            End Get
        End Property

        Public ReadOnly Property RemarkCN As Field(Of String) 
            Get
                Return FI_RemarkCN
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
        Public Function DeleteRowByPrimaryKey(ByVal RelationshipRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Delete From Relationship")
            strSQL.AppendLine("Where RelativeID = @RelativeID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@RelativeID", DbType.String, RelationshipRow.RelativeID.Value)

            return db.ExecuteNonQuery(dbcmd)
        End Function

        public Function DeleteRowByPrimaryKey(ByVal RelationshipRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Delete From Relationship")
            strSQL.AppendLine("Where RelativeID = @RelativeID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@RelativeID", DbType.String, RelationshipRow.RelativeID.Value)

            return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function DeleteRowByPrimaryKey(ByVal RelationshipRow As Row()) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Delete From Relationship")
            strSQL.AppendLine("Where RelativeID = @RelativeID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            using cn As DbConnection = db.CreateConnection()
                cn.Open()
                Dim tran As DbTransaction = cn.BeginTransaction()
                Dim inTrans As Boolean = True

                Try
                    For Each r As Row In RelationshipRow
                        dbcmd.Parameters.Clear()
                        db.AddInParameter(dbcmd, "@RelativeID", DbType.String, r.RelativeID.Value)

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

        Public Function DeleteRowByPrimaryKey(ByVal RelationshipRow As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Delete From Relationship")
            strSQL.AppendLine("Where RelativeID = @RelativeID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())

            For Each r As Row In RelationshipRow
                dbcmd.Parameters.Clear()
                db.AddInParameter(dbcmd, "@RelativeID", DbType.String, r.RelativeID.Value)

                intRowsAffected += db.ExecuteNonQuery(dbcmd, tran)
            Next
            Return intRowsAffected
        End Function

        Public Function QueryByKey(ByVal RelationshipRow As Row) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From Relationship")
            strSQL.AppendLine("Where RelativeID = @RelativeID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@RelativeID", DbType.String, RelationshipRow.RelativeID.Value)

            Return db.ExecuteDataSet(dbcmd)
        End Function

        Public Function QueryByKey(RelationshipRow As Row, tran As DbTransaction) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From Relationship")
            strSQL.AppendLine("Where RelativeID = @RelativeID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@RelativeID", DbType.String, RelationshipRow.RelativeID.Value)

            Return db.ExecuteDataSet(dbcmd, tran)
        End Function

        Public Function Update(ByVal RelationshipRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim strDot As String = String.Empty

            strSQL.AppendLine("Update Relationship Set")
            For i As Integer = 0 To RelationshipRow.FieldNames.Length - 1
                If Not RelationshipRow.IsIdentityField(RelationshipRow.FieldNames(i)) AndAlso RelationshipRow.IsUpdated(RelationshipRow.FieldNames(i)) AndAlso RelationshipRow.CreateUpdateSQL(RelationshipRow.FieldNames(i)) Then
                    strSQL.AppendLine(string.Format("{0}{1} = @{1}", strDot, RelationshipRow.FieldNames(i)))
                    strDot = ","
                End If
            Next
            If strDot = String.Empty Then Throw New Exception("未異動資料欄位！")
            strSQL.AppendLine("Where RelativeID = @PKRelativeID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            If RelationshipRow.RelativeID.Updated Then db.AddInParameter(dbcmd, "@RelativeID", DbType.String, RelationshipRow.RelativeID.Value)
            If RelationshipRow.HeaRelID.Updated Then db.AddInParameter(dbcmd, "@HeaRelID", DbType.String, RelationshipRow.HeaRelID.Value)
            If RelationshipRow.CompRelID.Updated Then db.AddInParameter(dbcmd, "@CompRelID", DbType.String, RelationshipRow.CompRelID.Value)
            If RelationshipRow.DeathPayID.Updated Then db.AddInParameter(dbcmd, "@DeathPayID", DbType.String, RelationshipRow.DeathPayID.Value)
            If RelationshipRow.TaxFamilyID.Updated Then db.AddInParameter(dbcmd, "@TaxFamilyID", DbType.String, RelationshipRow.TaxFamilyID.Value)
            If RelationshipRow.RelTypeID.Updated Then db.AddInParameter(dbcmd, "@RelTypeID", DbType.String, RelationshipRow.RelTypeID.Value)
            If RelationshipRow.RelClassID.Updated Then db.AddInParameter(dbcmd, "@RelClassID", DbType.String, RelationshipRow.RelClassID.Value)
            If RelationshipRow.Remark.Updated Then db.AddInParameter(dbcmd, "@Remark", DbType.String, RelationshipRow.Remark.Value)
            If RelationshipRow.RemarkCN.Updated Then db.AddInParameter(dbcmd, "@RemarkCN", DbType.String, RelationshipRow.RemarkCN.Value)
            If RelationshipRow.LastChgComp.Updated Then db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, RelationshipRow.LastChgComp.Value)
            If RelationshipRow.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, RelationshipRow.LastChgID.Value)
            If RelationshipRow.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(RelationshipRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), RelationshipRow.LastChgDate.Value))
            db.AddInParameter(dbcmd, "@PKRelativeID", DbType.String, IIf(RelationshipRow.LoadFromDataRow, RelationshipRow.RelativeID.OldValue, RelationshipRow.RelativeID.Value))

            Return db.ExecuteNonQuery(dbcmd)
        End Function

        Public Function Update(ByVal RelationshipRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim strDot As String = String.Empty

            strSQL.AppendLine("Update Relationship Set")
            For i As Integer = 0 To RelationshipRow.FieldNames.Length - 1
                If Not RelationshipRow.IsIdentityField(RelationshipRow.FieldNames(i)) AndAlso RelationshipRow.IsUpdated(RelationshipRow.FieldNames(i)) AndAlso RelationshipRow.CreateUpdateSQL(RelationshipRow.FieldNames(i)) Then
                    strSQL.AppendLine(string.Format("{0}{1} = @{1}", strDot, RelationshipRow.FieldNames(i)))
                    strDot = ","
                End If
            Next 
            If strDot = String.Empty Then Throw New Exception("未異動資料欄位！")
            strSQL.AppendLine("Where RelativeID = @PKRelativeID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            If RelationshipRow.RelativeID.Updated Then db.AddInParameter(dbcmd, "@RelativeID", DbType.String, RelationshipRow.RelativeID.Value)
            If RelationshipRow.HeaRelID.Updated Then db.AddInParameter(dbcmd, "@HeaRelID", DbType.String, RelationshipRow.HeaRelID.Value)
            If RelationshipRow.CompRelID.Updated Then db.AddInParameter(dbcmd, "@CompRelID", DbType.String, RelationshipRow.CompRelID.Value)
            If RelationshipRow.DeathPayID.Updated Then db.AddInParameter(dbcmd, "@DeathPayID", DbType.String, RelationshipRow.DeathPayID.Value)
            If RelationshipRow.TaxFamilyID.Updated Then db.AddInParameter(dbcmd, "@TaxFamilyID", DbType.String, RelationshipRow.TaxFamilyID.Value)
            If RelationshipRow.RelTypeID.Updated Then db.AddInParameter(dbcmd, "@RelTypeID", DbType.String, RelationshipRow.RelTypeID.Value)
            If RelationshipRow.RelClassID.Updated Then db.AddInParameter(dbcmd, "@RelClassID", DbType.String, RelationshipRow.RelClassID.Value)
            If RelationshipRow.Remark.Updated Then db.AddInParameter(dbcmd, "@Remark", DbType.String, RelationshipRow.Remark.Value)
            If RelationshipRow.RemarkCN.Updated Then db.AddInParameter(dbcmd, "@RemarkCN", DbType.String, RelationshipRow.RemarkCN.Value)
            If RelationshipRow.LastChgComp.Updated Then db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, RelationshipRow.LastChgComp.Value)
            If RelationshipRow.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, RelationshipRow.LastChgID.Value)
            If RelationshipRow.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(RelationshipRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), RelationshipRow.LastChgDate.Value))
            db.AddInParameter(dbcmd, "@PKRelativeID", DbType.String, IIf(RelationshipRow.LoadFromDataRow, RelationshipRow.RelativeID.OldValue, RelationshipRow.RelativeID.Value))

            Return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function Update(ByVal RelationshipRow As Row()) As Integer
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
                    For Each r As Row In RelationshipRow
                        strDot = String.Empty
                        If strSQL.Length > 0 Then strSQL.Remove(0, strSQL.Length)
                        strSQL.AppendLine("Update Relationship Set")
                        For i As Integer = 0 To r.FieldNames.Length - 1
                            If Not r.IsIdentityField(r.FieldNames(i)) AndAlso r.IsUpdated(r.FieldNames(i)) AndAlso r.CreateUpdateSQL(r.FieldNames(i)) Then
                                strSQL.AppendLine(String.Format("{0}{1} = @{1}", strDot, r.FieldNames(i)))
                                strDot = ","
                            End If
                        Next
                        If strDot = String.Empty Then Continue For
                        strSQL.AppendLine("Where RelativeID = @PKRelativeID")

                        dbcmd = db.GetSqlStringCommand(strSQL.ToString())
                        If r.RelativeID.Updated Then db.AddInParameter(dbcmd, "@RelativeID", DbType.String, r.RelativeID.Value)
                        If r.HeaRelID.Updated Then db.AddInParameter(dbcmd, "@HeaRelID", DbType.String, r.HeaRelID.Value)
                        If r.CompRelID.Updated Then db.AddInParameter(dbcmd, "@CompRelID", DbType.String, r.CompRelID.Value)
                        If r.DeathPayID.Updated Then db.AddInParameter(dbcmd, "@DeathPayID", DbType.String, r.DeathPayID.Value)
                        If r.TaxFamilyID.Updated Then db.AddInParameter(dbcmd, "@TaxFamilyID", DbType.String, r.TaxFamilyID.Value)
                        If r.RelTypeID.Updated Then db.AddInParameter(dbcmd, "@RelTypeID", DbType.String, r.RelTypeID.Value)
                        If r.RelClassID.Updated Then db.AddInParameter(dbcmd, "@RelClassID", DbType.String, r.RelClassID.Value)
                        If r.Remark.Updated Then db.AddInParameter(dbcmd, "@Remark", DbType.String, r.Remark.Value)
                        If r.RemarkCN.Updated Then db.AddInParameter(dbcmd, "@RemarkCN", DbType.String, r.RemarkCN.Value)
                        If r.LastChgComp.Updated Then db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, r.LastChgComp.Value)
                        If r.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, r.LastChgID.Value)
                        If r.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(r.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), r.LastChgDate.Value))
                        db.AddInParameter(dbcmd, "@PKRelativeID", DbType.String, IIf(r.LoadFromDataRow, r.RelativeID.OldValue, r.RelativeID.Value))

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

        Public Function Update(ByVal RelationshipRow As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0
            Dim strDot As String

            For Each r As Row In RelationshipRow
                strDot = String.Empty
                If strSQL.Length > 0 Then strSQL.Remove(0, strSQL.Length)
                strSQL.AppendLine("Update Relationship Set")
                For i As Integer = 0 To r.FieldNames.Length - 1
                    If Not r.IsIdentityField(r.FieldNames(i)) AndAlso r.IsUpdated(r.FieldNames(i)) AndAlso r.CreateUpdateSQL(r.FieldNames(i))
                        strSQL.AppendLine(String.Format("{0}{1} = @{1}", strDot, r.FieldNames(i)))
                        strDot = ","
                    End If
                Next
                If strDot = String.Empty Then Continue For
                strSQL.AppendLine("Where RelativeID = @PKRelativeID")

                dbcmd = db.GetSqlStringCommand(strSQL.ToString())
                If r.RelativeID.Updated Then db.AddInParameter(dbcmd, "@RelativeID", DbType.String, r.RelativeID.Value)
                If r.HeaRelID.Updated Then db.AddInParameter(dbcmd, "@HeaRelID", DbType.String, r.HeaRelID.Value)
                If r.CompRelID.Updated Then db.AddInParameter(dbcmd, "@CompRelID", DbType.String, r.CompRelID.Value)
                If r.DeathPayID.Updated Then db.AddInParameter(dbcmd, "@DeathPayID", DbType.String, r.DeathPayID.Value)
                If r.TaxFamilyID.Updated Then db.AddInParameter(dbcmd, "@TaxFamilyID", DbType.String, r.TaxFamilyID.Value)
                If r.RelTypeID.Updated Then db.AddInParameter(dbcmd, "@RelTypeID", DbType.String, r.RelTypeID.Value)
                If r.RelClassID.Updated Then db.AddInParameter(dbcmd, "@RelClassID", DbType.String, r.RelClassID.Value)
                If r.Remark.Updated Then db.AddInParameter(dbcmd, "@Remark", DbType.String, r.Remark.Value)
                If r.RemarkCN.Updated Then db.AddInParameter(dbcmd, "@RemarkCN", DbType.String, r.RemarkCN.Value)
                If r.LastChgComp.Updated Then db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, r.LastChgComp.Value)
                If r.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, r.LastChgID.Value)
                If r.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(r.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), r.LastChgDate.Value))
                db.AddInParameter(dbcmd, "@PKRelativeID", DbType.String, IIf(r.LoadFromDataRow, r.RelativeID.OldValue, r.RelativeID.Value))

                intRowsAffected += db.ExecuteNonQuery(dbcmd, tran)
            Next
            Return intRowsAffected
        End Function

        Public Function IsDataExists(ByVal RelationshipRow As Row) As Boolean
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intCount As Integer = 0

            strSQL.AppendLine("Select Count(*) Cnt From Relationship")
            strSQL.AppendLine("Where RelativeID = @RelativeID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@RelativeID", DbType.String, RelationshipRow.RelativeID.Value)

            intCount = Convert.ToInt32(db.ExecuteScalar(dbcmd))
            Return IIf(intCount > 0, True, False)
        End Function

        Public Function IsDataExists(ByVal RelationshipRow As Row, ByVal tran As DbTransaction) As Boolean
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intCount As Integer = 0

            strSQL.AppendLine("Select Count(*) Cnt From Relationship")
            strSQL.AppendLine("Where RelativeID = @RelativeID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@RelativeID", DbType.String, RelationshipRow.RelativeID.Value)

            intCount = Convert.ToInt32(db.ExecuteScalar(dbcmd, tran))
            Return IIf(intCount > 0, True, False)
        End Function

        Public Function QuerybyWhere(WhereCondition As String) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From Relationship")
            If WhereCondition <> String.Empty Then strSQL.AppendLine(WhereCondition)

            Return db.ExecuteDataSet(CommandType.Text, strSQL.ToString())
        End Function

        Public Function Insert(ByVal RelationshipRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Insert into Relationship")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    RelativeID, HeaRelID, CompRelID, DeathPayID, TaxFamilyID, RelTypeID, RelClassID, Remark,")
            strSQL.AppendLine("    RemarkCN, LastChgComp, LastChgID, LastChgDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @RelativeID, @HeaRelID, @CompRelID, @DeathPayID, @TaxFamilyID, @RelTypeID, @RelClassID, @Remark,")
            strSQL.AppendLine("    @RemarkCN, @LastChgComp, @LastChgID, @LastChgDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@RelativeID", DbType.String, RelationshipRow.RelativeID.Value)
            db.AddInParameter(dbcmd, "@HeaRelID", DbType.String, RelationshipRow.HeaRelID.Value)
            db.AddInParameter(dbcmd, "@CompRelID", DbType.String, RelationshipRow.CompRelID.Value)
            db.AddInParameter(dbcmd, "@DeathPayID", DbType.String, RelationshipRow.DeathPayID.Value)
            db.AddInParameter(dbcmd, "@TaxFamilyID", DbType.String, RelationshipRow.TaxFamilyID.Value)
            db.AddInParameter(dbcmd, "@RelTypeID", DbType.String, RelationshipRow.RelTypeID.Value)
            db.AddInParameter(dbcmd, "@RelClassID", DbType.String, RelationshipRow.RelClassID.Value)
            db.AddInParameter(dbcmd, "@Remark", DbType.String, RelationshipRow.Remark.Value)
            db.AddInParameter(dbcmd, "@RemarkCN", DbType.String, RelationshipRow.RemarkCN.Value)
            db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, RelationshipRow.LastChgComp.Value)
            db.AddInParameter(dbcmd, "@LastChgID", DbType.String, RelationshipRow.LastChgID.Value)
            db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(RelationshipRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), RelationshipRow.LastChgDate.Value))

            Return db.ExecuteNonQuery(dbcmd)
        End Function

        Public Function Insert(ByVal RelationshipRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Insert into Relationship")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    RelativeID, HeaRelID, CompRelID, DeathPayID, TaxFamilyID, RelTypeID, RelClassID, Remark,")
            strSQL.AppendLine("    RemarkCN, LastChgComp, LastChgID, LastChgDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @RelativeID, @HeaRelID, @CompRelID, @DeathPayID, @TaxFamilyID, @RelTypeID, @RelClassID, @Remark,")
            strSQL.AppendLine("    @RemarkCN, @LastChgComp, @LastChgID, @LastChgDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@RelativeID", DbType.String, RelationshipRow.RelativeID.Value)
            db.AddInParameter(dbcmd, "@HeaRelID", DbType.String, RelationshipRow.HeaRelID.Value)
            db.AddInParameter(dbcmd, "@CompRelID", DbType.String, RelationshipRow.CompRelID.Value)
            db.AddInParameter(dbcmd, "@DeathPayID", DbType.String, RelationshipRow.DeathPayID.Value)
            db.AddInParameter(dbcmd, "@TaxFamilyID", DbType.String, RelationshipRow.TaxFamilyID.Value)
            db.AddInParameter(dbcmd, "@RelTypeID", DbType.String, RelationshipRow.RelTypeID.Value)
            db.AddInParameter(dbcmd, "@RelClassID", DbType.String, RelationshipRow.RelClassID.Value)
            db.AddInParameter(dbcmd, "@Remark", DbType.String, RelationshipRow.Remark.Value)
            db.AddInParameter(dbcmd, "@RemarkCN", DbType.String, RelationshipRow.RemarkCN.Value)
            db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, RelationshipRow.LastChgComp.Value)
            db.AddInParameter(dbcmd, "@LastChgID", DbType.String, RelationshipRow.LastChgID.Value)
            db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(RelationshipRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), RelationshipRow.LastChgDate.Value))

            Return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function Insert(ByVal RelationshipRow As Row()) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Insert into Relationship")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    RelativeID, HeaRelID, CompRelID, DeathPayID, TaxFamilyID, RelTypeID, RelClassID, Remark,")
            strSQL.AppendLine("    RemarkCN, LastChgComp, LastChgID, LastChgDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @RelativeID, @HeaRelID, @CompRelID, @DeathPayID, @TaxFamilyID, @RelTypeID, @RelClassID, @Remark,")
            strSQL.AppendLine("    @RemarkCN, @LastChgComp, @LastChgID, @LastChgDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            Using cn As DbConnection = db.CreateConnection()
                cn.Open()
                Dim tran As DbTransaction = cn.BeginTransaction()
                Dim inTrans As Boolean = True

                Try
                    For Each r As Row In RelationshipRow
                        dbcmd.Parameters.Clear()
                        db.AddInParameter(dbcmd, "@RelativeID", DbType.String, r.RelativeID.Value)
                        db.AddInParameter(dbcmd, "@HeaRelID", DbType.String, r.HeaRelID.Value)
                        db.AddInParameter(dbcmd, "@CompRelID", DbType.String, r.CompRelID.Value)
                        db.AddInParameter(dbcmd, "@DeathPayID", DbType.String, r.DeathPayID.Value)
                        db.AddInParameter(dbcmd, "@TaxFamilyID", DbType.String, r.TaxFamilyID.Value)
                        db.AddInParameter(dbcmd, "@RelTypeID", DbType.String, r.RelTypeID.Value)
                        db.AddInParameter(dbcmd, "@RelClassID", DbType.String, r.RelClassID.Value)
                        db.AddInParameter(dbcmd, "@Remark", DbType.String, r.Remark.Value)
                        db.AddInParameter(dbcmd, "@RemarkCN", DbType.String, r.RemarkCN.Value)
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

        Public Function Insert(ByVal RelationshipRow As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Insert into Relationship")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    RelativeID, HeaRelID, CompRelID, DeathPayID, TaxFamilyID, RelTypeID, RelClassID, Remark,")
            strSQL.AppendLine("    RemarkCN, LastChgComp, LastChgID, LastChgDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @RelativeID, @HeaRelID, @CompRelID, @DeathPayID, @TaxFamilyID, @RelTypeID, @RelClassID, @Remark,")
            strSQL.AppendLine("    @RemarkCN, @LastChgComp, @LastChgID, @LastChgDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())

            For Each r As Row In RelationshipRow
                dbcmd.Parameters.Clear()
                db.AddInParameter(dbcmd, "@RelativeID", DbType.String, r.RelativeID.Value)
                db.AddInParameter(dbcmd, "@HeaRelID", DbType.String, r.HeaRelID.Value)
                db.AddInParameter(dbcmd, "@CompRelID", DbType.String, r.CompRelID.Value)
                db.AddInParameter(dbcmd, "@DeathPayID", DbType.String, r.DeathPayID.Value)
                db.AddInParameter(dbcmd, "@TaxFamilyID", DbType.String, r.TaxFamilyID.Value)
                db.AddInParameter(dbcmd, "@RelTypeID", DbType.String, r.RelTypeID.Value)
                db.AddInParameter(dbcmd, "@RelClassID", DbType.String, r.RelClassID.Value)
                db.AddInParameter(dbcmd, "@Remark", DbType.String, r.Remark.Value)
                db.AddInParameter(dbcmd, "@RemarkCN", DbType.String, r.RemarkCN.Value)
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

