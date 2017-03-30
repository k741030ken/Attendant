'****************************************************************
' Table:WorkType
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

Namespace beWorkType
    Public Class Table
        Private m_Rows As Rows = New Rows()
        Private m_Fields As String() = { "CompID", "WorkTypeID", "Remark", "OrganPrintFlag", "AOFlag", "PBFlag", "Class", "InValidFlag", "CategoryI", "CategoryII" _
                                    , "CategoryIII", "SortOrder", "LastChgComp", "LastChgID", "LastChgDate" }
        Private m_Types As System.Type() = { GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String) _
                                    , GetType(String), GetType(String), GetType(String), GetType(String), GetType(Date) }
        Private m_PrimaryFields As String() = { "CompID", "WorkTypeID" }

        Public ReadOnly Property Rows() As beWorkType.Rows 
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
        Public Sub Transfer2Row(WorkTypeTable As DataTable)
            For Each dr As DataRow In WorkTypeTable.Rows
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
                dr(m_Rows(i).WorkTypeID.FieldName) = m_Rows(i).WorkTypeID.Value
                dr(m_Rows(i).Remark.FieldName) = m_Rows(i).Remark.Value
                dr(m_Rows(i).OrganPrintFlag.FieldName) = m_Rows(i).OrganPrintFlag.Value
                dr(m_Rows(i).AOFlag.FieldName) = m_Rows(i).AOFlag.Value
                dr(m_Rows(i).PBFlag.FieldName) = m_Rows(i).PBFlag.Value
                dr(m_Rows(i).Class.FieldName) = m_Rows(i).Class.Value
                dr(m_Rows(i).InValidFlag.FieldName) = m_Rows(i).InValidFlag.Value
                dr(m_Rows(i).CategoryI.FieldName) = m_Rows(i).CategoryI.Value
                dr(m_Rows(i).CategoryII.FieldName) = m_Rows(i).CategoryII.Value
                dr(m_Rows(i).CategoryIII.FieldName) = m_Rows(i).CategoryIII.Value
                dr(m_Rows(i).SortOrder.FieldName) = m_Rows(i).SortOrder.Value
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

        Public Sub Add(WorkTypeRow As Row)
            m_Rows.Add(WorkTypeRow)
        End Sub

        Public Sub Remove(WorkTypeRow As Row)
            If m_Rows.IndexOf(WorkTypeRow) >= 0 Then
                m_Rows.Remove(WorkTypeRow)
            End If
        End Sub

        Public Sub Dispose()
            m_Rows.Clear()
        End Sub

    End Class

    Public Class Row

        Private FI_CompID As Field(Of String) = new Field(Of String)("CompID", true)
        Private FI_WorkTypeID As Field(Of String) = new Field(Of String)("WorkTypeID", true)
        Private FI_Remark As Field(Of String) = new Field(Of String)("Remark", true)
        Private FI_OrganPrintFlag As Field(Of String) = new Field(Of String)("OrganPrintFlag", true)
        Private FI_AOFlag As Field(Of String) = new Field(Of String)("AOFlag", true)
        Private FI_PBFlag As Field(Of String) = new Field(Of String)("PBFlag", true)
        Private FI_Class As Field(Of String) = new Field(Of String)("Class", true)
        Private FI_InValidFlag As Field(Of String) = new Field(Of String)("InValidFlag", true)
        Private FI_CategoryI As Field(Of String) = new Field(Of String)("CategoryI", true)
        Private FI_CategoryII As Field(Of String) = new Field(Of String)("CategoryII", true)
        Private FI_CategoryIII As Field(Of String) = new Field(Of String)("CategoryIII", true)
        Private FI_SortOrder As Field(Of String) = new Field(Of String)("SortOrder", true)
        Private FI_LastChgComp As Field(Of String) = new Field(Of String)("LastChgComp", true)
        Private FI_LastChgID As Field(Of String) = new Field(Of String)("LastChgID", true)
        Private FI_LastChgDate As Field(Of Date) = new Field(Of Date)("LastChgDate", true)
        Private m_FieldNames As String() = { "CompID", "WorkTypeID", "Remark", "OrganPrintFlag", "AOFlag", "PBFlag", "Class", "InValidFlag", "CategoryI", "CategoryII" _
                                    , "CategoryIII", "SortOrder", "LastChgComp", "LastChgID", "LastChgDate" }
        Private m_PrimaryFields As String() = { "CompID", "WorkTypeID" }
        Private m_IdentityFields As String() = {  }
        Private m_LoadFromDataRow As Boolean = False

        Private Function GetFieldValue(ByVal fieldName As String) As Object
            Select Case fieldName
                Case "CompID"
                    Return FI_CompID.Value
                Case "WorkTypeID"
                    Return FI_WorkTypeID.Value
                Case "Remark"
                    Return FI_Remark.Value
                Case "OrganPrintFlag"
                    Return FI_OrganPrintFlag.Value
                Case "AOFlag"
                    Return FI_AOFlag.Value
                Case "PBFlag"
                    Return FI_PBFlag.Value
                Case "Class"
                    Return FI_Class.Value
                Case "InValidFlag"
                    Return FI_InValidFlag.Value
                Case "CategoryI"
                    Return FI_CategoryI.Value
                Case "CategoryII"
                    Return FI_CategoryII.Value
                Case "CategoryIII"
                    Return FI_CategoryIII.Value
                Case "SortOrder"
                    Return FI_SortOrder.Value
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
                Case "WorkTypeID"
                    FI_WorkTypeID.SetValue(value)
                Case "Remark"
                    FI_Remark.SetValue(value)
                Case "OrganPrintFlag"
                    FI_OrganPrintFlag.SetValue(value)
                Case "AOFlag"
                    FI_AOFlag.SetValue(value)
                Case "PBFlag"
                    FI_PBFlag.SetValue(value)
                Case "Class"
                    FI_Class.SetValue(value)
                Case "InValidFlag"
                    FI_InValidFlag.SetValue(value)
                Case "CategoryI"
                    FI_CategoryI.SetValue(value)
                Case "CategoryII"
                    FI_CategoryII.SetValue(value)
                Case "CategoryIII"
                    FI_CategoryIII.SetValue(value)
                Case "SortOrder"
                    FI_SortOrder.SetValue(value)
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
                Case "WorkTypeID"
                    return FI_WorkTypeID.Updated
                Case "Remark"
                    return FI_Remark.Updated
                Case "OrganPrintFlag"
                    return FI_OrganPrintFlag.Updated
                Case "AOFlag"
                    return FI_AOFlag.Updated
                Case "PBFlag"
                    return FI_PBFlag.Updated
                Case "Class"
                    return FI_Class.Updated
                Case "InValidFlag"
                    return FI_InValidFlag.Updated
                Case "CategoryI"
                    return FI_CategoryI.Updated
                Case "CategoryII"
                    return FI_CategoryII.Updated
                Case "CategoryIII"
                    return FI_CategoryIII.Updated
                Case "SortOrder"
                    return FI_SortOrder.Updated
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
                Case "WorkTypeID"
                    return FI_WorkTypeID.CreateUpdateSQL
                Case "Remark"
                    return FI_Remark.CreateUpdateSQL
                Case "OrganPrintFlag"
                    return FI_OrganPrintFlag.CreateUpdateSQL
                Case "AOFlag"
                    return FI_AOFlag.CreateUpdateSQL
                Case "PBFlag"
                    return FI_PBFlag.CreateUpdateSQL
                Case "Class"
                    return FI_Class.CreateUpdateSQL
                Case "InValidFlag"
                    return FI_InValidFlag.CreateUpdateSQL
                Case "CategoryI"
                    return FI_CategoryI.CreateUpdateSQL
                Case "CategoryII"
                    return FI_CategoryII.CreateUpdateSQL
                Case "CategoryIII"
                    return FI_CategoryIII.CreateUpdateSQL
                Case "SortOrder"
                    return FI_SortOrder.CreateUpdateSQL
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
            FI_WorkTypeID.SetInitValue("")
            FI_Remark.SetInitValue("")
            FI_OrganPrintFlag.SetInitValue("0")
            FI_AOFlag.SetInitValue("")
            FI_PBFlag.SetInitValue("0")
            FI_Class.SetInitValue("")
            FI_InValidFlag.SetInitValue("0")
            FI_CategoryI.SetInitValue("")
            FI_CategoryII.SetInitValue("")
            FI_CategoryIII.SetInitValue("")
            FI_SortOrder.SetInitValue("")
            FI_LastChgComp.SetInitValue("")
            FI_LastChgID.SetInitValue("")
            FI_LastChgDate.SetInitValue(Convert.ToDateTime("1900/01/01"))
        End Sub

        Public Sub New(ByVal dr As System.Data.DataRow)
            FI_CompID.SetInitValue(dr("CompID"))
            FI_WorkTypeID.SetInitValue(dr("WorkTypeID"))
            FI_Remark.SetInitValue(dr("Remark"))
            FI_OrganPrintFlag.SetInitValue(dr("OrganPrintFlag"))
            FI_AOFlag.SetInitValue(dr("AOFlag"))
            FI_PBFlag.SetInitValue(dr("PBFlag"))
            FI_Class.SetInitValue(dr("Class"))
            FI_InValidFlag.SetInitValue(dr("InValidFlag"))
            FI_CategoryI.SetInitValue(dr("CategoryI"))
            FI_CategoryII.SetInitValue(dr("CategoryII"))
            FI_CategoryIII.SetInitValue(dr("CategoryIII"))
            FI_SortOrder.SetInitValue(dr("SortOrder"))
            FI_LastChgComp.SetInitValue(dr("LastChgComp"))
            FI_LastChgID.SetInitValue(dr("LastChgID"))
            FI_LastChgDate.SetInitValue(dr("LastChgDate"))

            m_LoadFromDataRow = True
        End Sub

        Private Sub ClearUpdated()
            FI_CompID.Updated = False
            FI_WorkTypeID.Updated = False
            FI_Remark.Updated = False
            FI_OrganPrintFlag.Updated = False
            FI_AOFlag.Updated = False
            FI_PBFlag.Updated = False
            FI_Class.Updated = False
            FI_InValidFlag.Updated = False
            FI_CategoryI.Updated = False
            FI_CategoryII.Updated = False
            FI_CategoryIII.Updated = False
            FI_SortOrder.Updated = False
            FI_LastChgComp.Updated = False
            FI_LastChgID.Updated = False
            FI_LastChgDate.Updated = False
        End Sub

        Public ReadOnly Property CompID As Field(Of String) 
            Get
                Return FI_CompID
            End Get
        End Property

        Public ReadOnly Property WorkTypeID As Field(Of String) 
            Get
                Return FI_WorkTypeID
            End Get
        End Property

        Public ReadOnly Property Remark As Field(Of String) 
            Get
                Return FI_Remark
            End Get
        End Property

        Public ReadOnly Property OrganPrintFlag As Field(Of String) 
            Get
                Return FI_OrganPrintFlag
            End Get
        End Property

        Public ReadOnly Property AOFlag As Field(Of String) 
            Get
                Return FI_AOFlag
            End Get
        End Property

        Public ReadOnly Property PBFlag As Field(Of String) 
            Get
                Return FI_PBFlag
            End Get
        End Property

        Public ReadOnly Property [Class] As Field(Of String) 
            Get
                Return FI_Class
            End Get
        End Property

        Public ReadOnly Property InValidFlag As Field(Of String) 
            Get
                Return FI_InValidFlag
            End Get
        End Property

        Public ReadOnly Property CategoryI As Field(Of String) 
            Get
                Return FI_CategoryI
            End Get
        End Property

        Public ReadOnly Property CategoryII As Field(Of String) 
            Get
                Return FI_CategoryII
            End Get
        End Property

        Public ReadOnly Property CategoryIII As Field(Of String) 
            Get
                Return FI_CategoryIII
            End Get
        End Property

        Public ReadOnly Property SortOrder As Field(Of String) 
            Get
                Return FI_SortOrder
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
        Public Function DeleteRowByPrimaryKey(ByVal WorkTypeRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Delete From WorkType")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And WorkTypeID = @WorkTypeID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, WorkTypeRow.CompID.Value)
            db.AddInParameter(dbcmd, "@WorkTypeID", DbType.String, WorkTypeRow.WorkTypeID.Value)

            return db.ExecuteNonQuery(dbcmd)
        End Function

        public Function DeleteRowByPrimaryKey(ByVal WorkTypeRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Delete From WorkType")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And WorkTypeID = @WorkTypeID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, WorkTypeRow.CompID.Value)
            db.AddInParameter(dbcmd, "@WorkTypeID", DbType.String, WorkTypeRow.WorkTypeID.Value)

            return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function DeleteRowByPrimaryKey(ByVal WorkTypeRow As Row()) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Delete From WorkType")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And WorkTypeID = @WorkTypeID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            using cn As DbConnection = db.CreateConnection()
                cn.Open()
                Dim tran As DbTransaction = cn.BeginTransaction()
                Dim inTrans As Boolean = True

                Try
                    For Each r As Row In WorkTypeRow
                        dbcmd.Parameters.Clear()
                        db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
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

        Public Function DeleteRowByPrimaryKey(ByVal WorkTypeRow As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Delete From WorkType")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And WorkTypeID = @WorkTypeID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())

            For Each r As Row In WorkTypeRow
                dbcmd.Parameters.Clear()
                db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                db.AddInParameter(dbcmd, "@WorkTypeID", DbType.String, r.WorkTypeID.Value)

                intRowsAffected += db.ExecuteNonQuery(dbcmd, tran)
            Next
            Return intRowsAffected
        End Function

        Public Function QueryByKey(ByVal WorkTypeRow As Row) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From WorkType")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And WorkTypeID = @WorkTypeID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, WorkTypeRow.CompID.Value)
            db.AddInParameter(dbcmd, "@WorkTypeID", DbType.String, WorkTypeRow.WorkTypeID.Value)

            Return db.ExecuteDataSet(dbcmd)
        End Function

        Public Function QueryByKey(WorkTypeRow As Row, tran As DbTransaction) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From WorkType")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And WorkTypeID = @WorkTypeID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, WorkTypeRow.CompID.Value)
            db.AddInParameter(dbcmd, "@WorkTypeID", DbType.String, WorkTypeRow.WorkTypeID.Value)

            Return db.ExecuteDataSet(dbcmd, tran)
        End Function

        Public Function Update(ByVal WorkTypeRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim strDot As String = String.Empty

            strSQL.AppendLine("Update WorkType Set")
            For i As Integer = 0 To WorkTypeRow.FieldNames.Length - 1
                If Not WorkTypeRow.IsIdentityField(WorkTypeRow.FieldNames(i)) AndAlso WorkTypeRow.IsUpdated(WorkTypeRow.FieldNames(i)) AndAlso WorkTypeRow.CreateUpdateSQL(WorkTypeRow.FieldNames(i)) Then
                    strSQL.AppendLine(string.Format("{0}{1} = @{1}", strDot, WorkTypeRow.FieldNames(i)))
                    strDot = ","
                End If
            Next
            If strDot = String.Empty Then Throw New Exception("未異動資料欄位！")
            strSQL.AppendLine("Where CompID = @PKCompID")
            strSQL.AppendLine("And WorkTypeID = @PKWorkTypeID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            If WorkTypeRow.CompID.Updated Then db.AddInParameter(dbcmd, "@CompID", DbType.String, WorkTypeRow.CompID.Value)
            If WorkTypeRow.WorkTypeID.Updated Then db.AddInParameter(dbcmd, "@WorkTypeID", DbType.String, WorkTypeRow.WorkTypeID.Value)
            If WorkTypeRow.Remark.Updated Then db.AddInParameter(dbcmd, "@Remark", DbType.String, WorkTypeRow.Remark.Value)
            If WorkTypeRow.OrganPrintFlag.Updated Then db.AddInParameter(dbcmd, "@OrganPrintFlag", DbType.String, WorkTypeRow.OrganPrintFlag.Value)
            If WorkTypeRow.AOFlag.Updated Then db.AddInParameter(dbcmd, "@AOFlag", DbType.String, WorkTypeRow.AOFlag.Value)
            If WorkTypeRow.PBFlag.Updated Then db.AddInParameter(dbcmd, "@PBFlag", DbType.String, WorkTypeRow.PBFlag.Value)
            If WorkTypeRow.Class.Updated Then db.AddInParameter(dbcmd, "@Class", DbType.String, WorkTypeRow.Class.Value)
            If WorkTypeRow.InValidFlag.Updated Then db.AddInParameter(dbcmd, "@InValidFlag", DbType.String, WorkTypeRow.InValidFlag.Value)
            If WorkTypeRow.CategoryI.Updated Then db.AddInParameter(dbcmd, "@CategoryI", DbType.String, WorkTypeRow.CategoryI.Value)
            If WorkTypeRow.CategoryII.Updated Then db.AddInParameter(dbcmd, "@CategoryII", DbType.String, WorkTypeRow.CategoryII.Value)
            If WorkTypeRow.CategoryIII.Updated Then db.AddInParameter(dbcmd, "@CategoryIII", DbType.String, WorkTypeRow.CategoryIII.Value)
            If WorkTypeRow.SortOrder.Updated Then db.AddInParameter(dbcmd, "@SortOrder", DbType.String, WorkTypeRow.SortOrder.Value)
            If WorkTypeRow.LastChgComp.Updated Then db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, WorkTypeRow.LastChgComp.Value)
            If WorkTypeRow.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, WorkTypeRow.LastChgID.Value)
            If WorkTypeRow.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(WorkTypeRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), WorkTypeRow.LastChgDate.Value))
            db.AddInParameter(dbcmd, "@PKCompID", DbType.String, IIf(WorkTypeRow.LoadFromDataRow, WorkTypeRow.CompID.OldValue, WorkTypeRow.CompID.Value))
            db.AddInParameter(dbcmd, "@PKWorkTypeID", DbType.String, IIf(WorkTypeRow.LoadFromDataRow, WorkTypeRow.WorkTypeID.OldValue, WorkTypeRow.WorkTypeID.Value))

            Return db.ExecuteNonQuery(dbcmd)
        End Function

        Public Function Update(ByVal WorkTypeRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim strDot As String = String.Empty

            strSQL.AppendLine("Update WorkType Set")
            For i As Integer = 0 To WorkTypeRow.FieldNames.Length - 1
                If Not WorkTypeRow.IsIdentityField(WorkTypeRow.FieldNames(i)) AndAlso WorkTypeRow.IsUpdated(WorkTypeRow.FieldNames(i)) AndAlso WorkTypeRow.CreateUpdateSQL(WorkTypeRow.FieldNames(i)) Then
                    strSQL.AppendLine(string.Format("{0}{1} = @{1}", strDot, WorkTypeRow.FieldNames(i)))
                    strDot = ","
                End If
            Next 
            If strDot = String.Empty Then Throw New Exception("未異動資料欄位！")
            strSQL.AppendLine("Where CompID = @PKCompID")
            strSQL.AppendLine("And WorkTypeID = @PKWorkTypeID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            If WorkTypeRow.CompID.Updated Then db.AddInParameter(dbcmd, "@CompID", DbType.String, WorkTypeRow.CompID.Value)
            If WorkTypeRow.WorkTypeID.Updated Then db.AddInParameter(dbcmd, "@WorkTypeID", DbType.String, WorkTypeRow.WorkTypeID.Value)
            If WorkTypeRow.Remark.Updated Then db.AddInParameter(dbcmd, "@Remark", DbType.String, WorkTypeRow.Remark.Value)
            If WorkTypeRow.OrganPrintFlag.Updated Then db.AddInParameter(dbcmd, "@OrganPrintFlag", DbType.String, WorkTypeRow.OrganPrintFlag.Value)
            If WorkTypeRow.AOFlag.Updated Then db.AddInParameter(dbcmd, "@AOFlag", DbType.String, WorkTypeRow.AOFlag.Value)
            If WorkTypeRow.PBFlag.Updated Then db.AddInParameter(dbcmd, "@PBFlag", DbType.String, WorkTypeRow.PBFlag.Value)
            If WorkTypeRow.Class.Updated Then db.AddInParameter(dbcmd, "@Class", DbType.String, WorkTypeRow.Class.Value)
            If WorkTypeRow.InValidFlag.Updated Then db.AddInParameter(dbcmd, "@InValidFlag", DbType.String, WorkTypeRow.InValidFlag.Value)
            If WorkTypeRow.CategoryI.Updated Then db.AddInParameter(dbcmd, "@CategoryI", DbType.String, WorkTypeRow.CategoryI.Value)
            If WorkTypeRow.CategoryII.Updated Then db.AddInParameter(dbcmd, "@CategoryII", DbType.String, WorkTypeRow.CategoryII.Value)
            If WorkTypeRow.CategoryIII.Updated Then db.AddInParameter(dbcmd, "@CategoryIII", DbType.String, WorkTypeRow.CategoryIII.Value)
            If WorkTypeRow.SortOrder.Updated Then db.AddInParameter(dbcmd, "@SortOrder", DbType.String, WorkTypeRow.SortOrder.Value)
            If WorkTypeRow.LastChgComp.Updated Then db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, WorkTypeRow.LastChgComp.Value)
            If WorkTypeRow.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, WorkTypeRow.LastChgID.Value)
            If WorkTypeRow.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(WorkTypeRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), WorkTypeRow.LastChgDate.Value))
            db.AddInParameter(dbcmd, "@PKCompID", DbType.String, IIf(WorkTypeRow.LoadFromDataRow, WorkTypeRow.CompID.OldValue, WorkTypeRow.CompID.Value))
            db.AddInParameter(dbcmd, "@PKWorkTypeID", DbType.String, IIf(WorkTypeRow.LoadFromDataRow, WorkTypeRow.WorkTypeID.OldValue, WorkTypeRow.WorkTypeID.Value))

            Return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function Update(ByVal WorkTypeRow As Row()) As Integer
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
                    For Each r As Row In WorkTypeRow
                        strDot = String.Empty
                        If strSQL.Length > 0 Then strSQL.Remove(0, strSQL.Length)
                        strSQL.AppendLine("Update WorkType Set")
                        For i As Integer = 0 To r.FieldNames.Length - 1
                            If Not r.IsIdentityField(r.FieldNames(i)) AndAlso r.IsUpdated(r.FieldNames(i)) AndAlso r.CreateUpdateSQL(r.FieldNames(i)) Then
                                strSQL.AppendLine(String.Format("{0}{1} = @{1}", strDot, r.FieldNames(i)))
                                strDot = ","
                            End If
                        Next
                        If strDot = String.Empty Then Continue For
                        strSQL.AppendLine("Where CompID = @PKCompID")
                        strSQL.AppendLine("And WorkTypeID = @PKWorkTypeID")

                        dbcmd = db.GetSqlStringCommand(strSQL.ToString())
                        If r.CompID.Updated Then db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                        If r.WorkTypeID.Updated Then db.AddInParameter(dbcmd, "@WorkTypeID", DbType.String, r.WorkTypeID.Value)
                        If r.Remark.Updated Then db.AddInParameter(dbcmd, "@Remark", DbType.String, r.Remark.Value)
                        If r.OrganPrintFlag.Updated Then db.AddInParameter(dbcmd, "@OrganPrintFlag", DbType.String, r.OrganPrintFlag.Value)
                        If r.AOFlag.Updated Then db.AddInParameter(dbcmd, "@AOFlag", DbType.String, r.AOFlag.Value)
                        If r.PBFlag.Updated Then db.AddInParameter(dbcmd, "@PBFlag", DbType.String, r.PBFlag.Value)
                        If r.Class.Updated Then db.AddInParameter(dbcmd, "@Class", DbType.String, r.Class.Value)
                        If r.InValidFlag.Updated Then db.AddInParameter(dbcmd, "@InValidFlag", DbType.String, r.InValidFlag.Value)
                        If r.CategoryI.Updated Then db.AddInParameter(dbcmd, "@CategoryI", DbType.String, r.CategoryI.Value)
                        If r.CategoryII.Updated Then db.AddInParameter(dbcmd, "@CategoryII", DbType.String, r.CategoryII.Value)
                        If r.CategoryIII.Updated Then db.AddInParameter(dbcmd, "@CategoryIII", DbType.String, r.CategoryIII.Value)
                        If r.SortOrder.Updated Then db.AddInParameter(dbcmd, "@SortOrder", DbType.String, r.SortOrder.Value)
                        If r.LastChgComp.Updated Then db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, r.LastChgComp.Value)
                        If r.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, r.LastChgID.Value)
                        If r.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(r.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), r.LastChgDate.Value))
                        db.AddInParameter(dbcmd, "@PKCompID", DbType.String, IIf(r.LoadFromDataRow, r.CompID.OldValue, r.CompID.Value))
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

        Public Function Update(ByVal WorkTypeRow As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0
            Dim strDot As String

            For Each r As Row In WorkTypeRow
                strDot = String.Empty
                If strSQL.Length > 0 Then strSQL.Remove(0, strSQL.Length)
                strSQL.AppendLine("Update WorkType Set")
                For i As Integer = 0 To r.FieldNames.Length - 1
                    If Not r.IsIdentityField(r.FieldNames(i)) AndAlso r.IsUpdated(r.FieldNames(i)) AndAlso r.CreateUpdateSQL(r.FieldNames(i))
                        strSQL.AppendLine(String.Format("{0}{1} = @{1}", strDot, r.FieldNames(i)))
                        strDot = ","
                    End If
                Next
                If strDot = String.Empty Then Continue For
                strSQL.AppendLine("Where CompID = @PKCompID")
                strSQL.AppendLine("And WorkTypeID = @PKWorkTypeID")

                dbcmd = db.GetSqlStringCommand(strSQL.ToString())
                If r.CompID.Updated Then db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                If r.WorkTypeID.Updated Then db.AddInParameter(dbcmd, "@WorkTypeID", DbType.String, r.WorkTypeID.Value)
                If r.Remark.Updated Then db.AddInParameter(dbcmd, "@Remark", DbType.String, r.Remark.Value)
                If r.OrganPrintFlag.Updated Then db.AddInParameter(dbcmd, "@OrganPrintFlag", DbType.String, r.OrganPrintFlag.Value)
                If r.AOFlag.Updated Then db.AddInParameter(dbcmd, "@AOFlag", DbType.String, r.AOFlag.Value)
                If r.PBFlag.Updated Then db.AddInParameter(dbcmd, "@PBFlag", DbType.String, r.PBFlag.Value)
                If r.Class.Updated Then db.AddInParameter(dbcmd, "@Class", DbType.String, r.Class.Value)
                If r.InValidFlag.Updated Then db.AddInParameter(dbcmd, "@InValidFlag", DbType.String, r.InValidFlag.Value)
                If r.CategoryI.Updated Then db.AddInParameter(dbcmd, "@CategoryI", DbType.String, r.CategoryI.Value)
                If r.CategoryII.Updated Then db.AddInParameter(dbcmd, "@CategoryII", DbType.String, r.CategoryII.Value)
                If r.CategoryIII.Updated Then db.AddInParameter(dbcmd, "@CategoryIII", DbType.String, r.CategoryIII.Value)
                If r.SortOrder.Updated Then db.AddInParameter(dbcmd, "@SortOrder", DbType.String, r.SortOrder.Value)
                If r.LastChgComp.Updated Then db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, r.LastChgComp.Value)
                If r.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, r.LastChgID.Value)
                If r.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(r.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), r.LastChgDate.Value))
                db.AddInParameter(dbcmd, "@PKCompID", DbType.String, IIf(r.LoadFromDataRow, r.CompID.OldValue, r.CompID.Value))
                db.AddInParameter(dbcmd, "@PKWorkTypeID", DbType.String, IIf(r.LoadFromDataRow, r.WorkTypeID.OldValue, r.WorkTypeID.Value))

                intRowsAffected += db.ExecuteNonQuery(dbcmd, tran)
            Next
            Return intRowsAffected
        End Function

        Public Function IsDataExists(ByVal WorkTypeRow As Row) As Boolean
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intCount As Integer = 0

            strSQL.AppendLine("Select Count(*) Cnt From WorkType")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And WorkTypeID = @WorkTypeID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, WorkTypeRow.CompID.Value)
            db.AddInParameter(dbcmd, "@WorkTypeID", DbType.String, WorkTypeRow.WorkTypeID.Value)

            intCount = Convert.ToInt32(db.ExecuteScalar(dbcmd))
            Return IIf(intCount > 0, True, False)
        End Function

        Public Function IsDataExists(ByVal WorkTypeRow As Row, ByVal tran As DbTransaction) As Boolean
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intCount As Integer = 0

            strSQL.AppendLine("Select Count(*) Cnt From WorkType")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And WorkTypeID = @WorkTypeID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, WorkTypeRow.CompID.Value)
            db.AddInParameter(dbcmd, "@WorkTypeID", DbType.String, WorkTypeRow.WorkTypeID.Value)

            intCount = Convert.ToInt32(db.ExecuteScalar(dbcmd, tran))
            Return IIf(intCount > 0, True, False)
        End Function

        Public Function QuerybyWhere(WhereCondition As String) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From WorkType")
            If WhereCondition <> String.Empty Then strSQL.AppendLine(WhereCondition)

            Return db.ExecuteDataSet(CommandType.Text, strSQL.ToString())
        End Function

        Public Function Insert(ByVal WorkTypeRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Insert into WorkType")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    CompID, WorkTypeID, Remark, OrganPrintFlag, AOFlag, PBFlag, Class, InValidFlag, CategoryI,")
            strSQL.AppendLine("    CategoryII, CategoryIII, SortOrder, LastChgComp, LastChgID, LastChgDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @CompID, @WorkTypeID, @Remark, @OrganPrintFlag, @AOFlag, @PBFlag, @Class, @InValidFlag, @CategoryI,")
            strSQL.AppendLine("    @CategoryII, @CategoryIII, @SortOrder, @LastChgComp, @LastChgID, @LastChgDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, WorkTypeRow.CompID.Value)
            db.AddInParameter(dbcmd, "@WorkTypeID", DbType.String, WorkTypeRow.WorkTypeID.Value)
            db.AddInParameter(dbcmd, "@Remark", DbType.String, WorkTypeRow.Remark.Value)
            db.AddInParameter(dbcmd, "@OrganPrintFlag", DbType.String, WorkTypeRow.OrganPrintFlag.Value)
            db.AddInParameter(dbcmd, "@AOFlag", DbType.String, WorkTypeRow.AOFlag.Value)
            db.AddInParameter(dbcmd, "@PBFlag", DbType.String, WorkTypeRow.PBFlag.Value)
            db.AddInParameter(dbcmd, "@Class", DbType.String, WorkTypeRow.Class.Value)
            db.AddInParameter(dbcmd, "@InValidFlag", DbType.String, WorkTypeRow.InValidFlag.Value)
            db.AddInParameter(dbcmd, "@CategoryI", DbType.String, WorkTypeRow.CategoryI.Value)
            db.AddInParameter(dbcmd, "@CategoryII", DbType.String, WorkTypeRow.CategoryII.Value)
            db.AddInParameter(dbcmd, "@CategoryIII", DbType.String, WorkTypeRow.CategoryIII.Value)
            db.AddInParameter(dbcmd, "@SortOrder", DbType.String, WorkTypeRow.SortOrder.Value)
            db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, WorkTypeRow.LastChgComp.Value)
            db.AddInParameter(dbcmd, "@LastChgID", DbType.String, WorkTypeRow.LastChgID.Value)
            db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(WorkTypeRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), WorkTypeRow.LastChgDate.Value))

            Return db.ExecuteNonQuery(dbcmd)
        End Function

        Public Function Insert(ByVal WorkTypeRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Insert into WorkType")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    CompID, WorkTypeID, Remark, OrganPrintFlag, AOFlag, PBFlag, Class, InValidFlag, CategoryI,")
            strSQL.AppendLine("    CategoryII, CategoryIII, SortOrder, LastChgComp, LastChgID, LastChgDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @CompID, @WorkTypeID, @Remark, @OrganPrintFlag, @AOFlag, @PBFlag, @Class, @InValidFlag, @CategoryI,")
            strSQL.AppendLine("    @CategoryII, @CategoryIII, @SortOrder, @LastChgComp, @LastChgID, @LastChgDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, WorkTypeRow.CompID.Value)
            db.AddInParameter(dbcmd, "@WorkTypeID", DbType.String, WorkTypeRow.WorkTypeID.Value)
            db.AddInParameter(dbcmd, "@Remark", DbType.String, WorkTypeRow.Remark.Value)
            db.AddInParameter(dbcmd, "@OrganPrintFlag", DbType.String, WorkTypeRow.OrganPrintFlag.Value)
            db.AddInParameter(dbcmd, "@AOFlag", DbType.String, WorkTypeRow.AOFlag.Value)
            db.AddInParameter(dbcmd, "@PBFlag", DbType.String, WorkTypeRow.PBFlag.Value)
            db.AddInParameter(dbcmd, "@Class", DbType.String, WorkTypeRow.Class.Value)
            db.AddInParameter(dbcmd, "@InValidFlag", DbType.String, WorkTypeRow.InValidFlag.Value)
            db.AddInParameter(dbcmd, "@CategoryI", DbType.String, WorkTypeRow.CategoryI.Value)
            db.AddInParameter(dbcmd, "@CategoryII", DbType.String, WorkTypeRow.CategoryII.Value)
            db.AddInParameter(dbcmd, "@CategoryIII", DbType.String, WorkTypeRow.CategoryIII.Value)
            db.AddInParameter(dbcmd, "@SortOrder", DbType.String, WorkTypeRow.SortOrder.Value)
            db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, WorkTypeRow.LastChgComp.Value)
            db.AddInParameter(dbcmd, "@LastChgID", DbType.String, WorkTypeRow.LastChgID.Value)
            db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(WorkTypeRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), WorkTypeRow.LastChgDate.Value))

            Return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function Insert(ByVal WorkTypeRow As Row()) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Insert into WorkType")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    CompID, WorkTypeID, Remark, OrganPrintFlag, AOFlag, PBFlag, Class, InValidFlag, CategoryI,")
            strSQL.AppendLine("    CategoryII, CategoryIII, SortOrder, LastChgComp, LastChgID, LastChgDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @CompID, @WorkTypeID, @Remark, @OrganPrintFlag, @AOFlag, @PBFlag, @Class, @InValidFlag, @CategoryI,")
            strSQL.AppendLine("    @CategoryII, @CategoryIII, @SortOrder, @LastChgComp, @LastChgID, @LastChgDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            Using cn As DbConnection = db.CreateConnection()
                cn.Open()
                Dim tran As DbTransaction = cn.BeginTransaction()
                Dim inTrans As Boolean = True

                Try
                    For Each r As Row In WorkTypeRow
                        dbcmd.Parameters.Clear()
                        db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                        db.AddInParameter(dbcmd, "@WorkTypeID", DbType.String, r.WorkTypeID.Value)
                        db.AddInParameter(dbcmd, "@Remark", DbType.String, r.Remark.Value)
                        db.AddInParameter(dbcmd, "@OrganPrintFlag", DbType.String, r.OrganPrintFlag.Value)
                        db.AddInParameter(dbcmd, "@AOFlag", DbType.String, r.AOFlag.Value)
                        db.AddInParameter(dbcmd, "@PBFlag", DbType.String, r.PBFlag.Value)
                        db.AddInParameter(dbcmd, "@Class", DbType.String, r.Class.Value)
                        db.AddInParameter(dbcmd, "@InValidFlag", DbType.String, r.InValidFlag.Value)
                        db.AddInParameter(dbcmd, "@CategoryI", DbType.String, r.CategoryI.Value)
                        db.AddInParameter(dbcmd, "@CategoryII", DbType.String, r.CategoryII.Value)
                        db.AddInParameter(dbcmd, "@CategoryIII", DbType.String, r.CategoryIII.Value)
                        db.AddInParameter(dbcmd, "@SortOrder", DbType.String, r.SortOrder.Value)
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

        Public Function Insert(ByVal WorkTypeRow As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Insert into WorkType")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    CompID, WorkTypeID, Remark, OrganPrintFlag, AOFlag, PBFlag, Class, InValidFlag, CategoryI,")
            strSQL.AppendLine("    CategoryII, CategoryIII, SortOrder, LastChgComp, LastChgID, LastChgDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @CompID, @WorkTypeID, @Remark, @OrganPrintFlag, @AOFlag, @PBFlag, @Class, @InValidFlag, @CategoryI,")
            strSQL.AppendLine("    @CategoryII, @CategoryIII, @SortOrder, @LastChgComp, @LastChgID, @LastChgDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())

            For Each r As Row In WorkTypeRow
                dbcmd.Parameters.Clear()
                db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                db.AddInParameter(dbcmd, "@WorkTypeID", DbType.String, r.WorkTypeID.Value)
                db.AddInParameter(dbcmd, "@Remark", DbType.String, r.Remark.Value)
                db.AddInParameter(dbcmd, "@OrganPrintFlag", DbType.String, r.OrganPrintFlag.Value)
                db.AddInParameter(dbcmd, "@AOFlag", DbType.String, r.AOFlag.Value)
                db.AddInParameter(dbcmd, "@PBFlag", DbType.String, r.PBFlag.Value)
                db.AddInParameter(dbcmd, "@Class", DbType.String, r.Class.Value)
                db.AddInParameter(dbcmd, "@InValidFlag", DbType.String, r.InValidFlag.Value)
                db.AddInParameter(dbcmd, "@CategoryI", DbType.String, r.CategoryI.Value)
                db.AddInParameter(dbcmd, "@CategoryII", DbType.String, r.CategoryII.Value)
                db.AddInParameter(dbcmd, "@CategoryIII", DbType.String, r.CategoryIII.Value)
                db.AddInParameter(dbcmd, "@SortOrder", DbType.String, r.SortOrder.Value)
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

