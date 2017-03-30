'****************************************************************
' Table:Position
' Created Date: 2015.04.28
'****************************************************************/
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports Microsoft.Practices.EnterpriseLibrary.Common
Imports Microsoft.Practices.EnterpriseLibrary.Data
Imports System.Data.Common
Imports System.Data

Namespace bePosition
    Public Class Table
        Private m_Rows As Rows = New Rows()
        Private m_Fields As String() = { "CompID", "PositionID", "Remark", "OrganPrintFlag", "InValidFlag", "CategoryI", "CategoryII", "CategoryIII", "SortOrder", "LastChgComp" _
                                    , "LastChgID", "LastChgDate", "IsEVManager" }
        Private m_Types As System.Type() = { GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String) _
                                    , GetType(String), GetType(Date), GetType(String) }
        Private m_PrimaryFields As String() = { "CompID", "PositionID" }

        Public ReadOnly Property Rows() As bePosition.Rows 
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
        Public Sub Transfer2Row(PositionTable As DataTable)
            For Each dr As DataRow In PositionTable.Rows
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
                dr(m_Rows(i).PositionID.FieldName) = m_Rows(i).PositionID.Value
                dr(m_Rows(i).Remark.FieldName) = m_Rows(i).Remark.Value
                dr(m_Rows(i).OrganPrintFlag.FieldName) = m_Rows(i).OrganPrintFlag.Value
                dr(m_Rows(i).InValidFlag.FieldName) = m_Rows(i).InValidFlag.Value
                dr(m_Rows(i).CategoryI.FieldName) = m_Rows(i).CategoryI.Value
                dr(m_Rows(i).CategoryII.FieldName) = m_Rows(i).CategoryII.Value
                dr(m_Rows(i).CategoryIII.FieldName) = m_Rows(i).CategoryIII.Value
                dr(m_Rows(i).SortOrder.FieldName) = m_Rows(i).SortOrder.Value
                dr(m_Rows(i).LastChgComp.FieldName) = m_Rows(i).LastChgComp.Value
                dr(m_Rows(i).LastChgID.FieldName) = m_Rows(i).LastChgID.Value
                dr(m_Rows(i).LastChgDate.FieldName) = m_Rows(i).LastChgDate.Value
                dr(m_Rows(i).IsEVManager.FieldName) = m_Rows(i).IsEVManager.Value

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

        Public Sub Add(PositionRow As Row)
            m_Rows.Add(PositionRow)
        End Sub

        Public Sub Remove(PositionRow As Row)
            If m_Rows.IndexOf(PositionRow) >= 0 Then
                m_Rows.Remove(PositionRow)
            End If
        End Sub

        Public Sub Dispose()
            m_Rows.Clear()
        End Sub

    End Class

    Public Class Row

        Private FI_CompID As Field(Of String) = new Field(Of String)("CompID", true)
        Private FI_PositionID As Field(Of String) = new Field(Of String)("PositionID", true)
        Private FI_Remark As Field(Of String) = new Field(Of String)("Remark", true)
        Private FI_OrganPrintFlag As Field(Of String) = new Field(Of String)("OrganPrintFlag", true)
        Private FI_InValidFlag As Field(Of String) = new Field(Of String)("InValidFlag", true)
        Private FI_CategoryI As Field(Of String) = new Field(Of String)("CategoryI", true)
        Private FI_CategoryII As Field(Of String) = new Field(Of String)("CategoryII", true)
        Private FI_CategoryIII As Field(Of String) = new Field(Of String)("CategoryIII", true)
        Private FI_SortOrder As Field(Of String) = new Field(Of String)("SortOrder", true)
        Private FI_LastChgComp As Field(Of String) = new Field(Of String)("LastChgComp", true)
        Private FI_LastChgID As Field(Of String) = new Field(Of String)("LastChgID", true)
        Private FI_LastChgDate As Field(Of Date) = new Field(Of Date)("LastChgDate", true)
        Private FI_IsEVManager As Field(Of String) = new Field(Of String)("IsEVManager", true)
        Private m_FieldNames As String() = { "CompID", "PositionID", "Remark", "OrganPrintFlag", "InValidFlag", "CategoryI", "CategoryII", "CategoryIII", "SortOrder", "LastChgComp" _
                                    , "LastChgID", "LastChgDate", "IsEVManager" }
        Private m_PrimaryFields As String() = { "CompID", "PositionID" }
        Private m_IdentityFields As String() = {  }
        Private m_LoadFromDataRow As Boolean = False

        Private Function GetFieldValue(ByVal fieldName As String) As Object
            Select Case fieldName
                Case "CompID"
                    Return FI_CompID.Value
                Case "PositionID"
                    Return FI_PositionID.Value
                Case "Remark"
                    Return FI_Remark.Value
                Case "OrganPrintFlag"
                    Return FI_OrganPrintFlag.Value
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
                Case "IsEVManager"
                    Return FI_IsEVManager.Value
                Case Else
                    Return Nothing
            End Select
        End Function

        Private Sub SetFieldValue(ByVal fieldName As String, ByVal value As Object)
            Select Case fieldName
                Case "CompID"
                    FI_CompID.SetValue(value)
                Case "PositionID"
                    FI_PositionID.SetValue(value)
                Case "Remark"
                    FI_Remark.SetValue(value)
                Case "OrganPrintFlag"
                    FI_OrganPrintFlag.SetValue(value)
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
                Case "IsEVManager"
                    FI_IsEVManager.SetValue(value)
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
                Case "PositionID"
                    return FI_PositionID.Updated
                Case "Remark"
                    return FI_Remark.Updated
                Case "OrganPrintFlag"
                    return FI_OrganPrintFlag.Updated
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
                Case "IsEVManager"
                    return FI_IsEVManager.Updated
                Case Else
                    Throw New Exception("無此欄位！")
            End Select
        End Function

        Public Function CreateUpdateSQL(ByVal fieldName As String) As Boolean
            Select Case fieldName
                Case "CompID"
                    return FI_CompID.CreateUpdateSQL
                Case "PositionID"
                    return FI_PositionID.CreateUpdateSQL
                Case "Remark"
                    return FI_Remark.CreateUpdateSQL
                Case "OrganPrintFlag"
                    return FI_OrganPrintFlag.CreateUpdateSQL
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
                Case "IsEVManager"
                    return FI_IsEVManager.CreateUpdateSQL
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
            FI_PositionID.SetInitValue("")
            FI_Remark.SetInitValue("")
            FI_OrganPrintFlag.SetInitValue("0")
            FI_InValidFlag.SetInitValue("0")
            FI_CategoryI.SetInitValue("")
            FI_CategoryII.SetInitValue("")
            FI_CategoryIII.SetInitValue("")
            FI_SortOrder.SetInitValue("")
            FI_LastChgComp.SetInitValue("")
            FI_LastChgID.SetInitValue("")
            FI_LastChgDate.SetInitValue(Convert.ToDateTime("1900/01/01"))
            FI_IsEVManager.SetInitValue("0")
        End Sub

        Public Sub New(ByVal dr As System.Data.DataRow)
            FI_CompID.SetInitValue(dr("CompID"))
            FI_PositionID.SetInitValue(dr("PositionID"))
            FI_Remark.SetInitValue(dr("Remark"))
            FI_OrganPrintFlag.SetInitValue(dr("OrganPrintFlag"))
            FI_InValidFlag.SetInitValue(dr("InValidFlag"))
            FI_CategoryI.SetInitValue(dr("CategoryI"))
            FI_CategoryII.SetInitValue(dr("CategoryII"))
            FI_CategoryIII.SetInitValue(dr("CategoryIII"))
            FI_SortOrder.SetInitValue(dr("SortOrder"))
            FI_LastChgComp.SetInitValue(dr("LastChgComp"))
            FI_LastChgID.SetInitValue(dr("LastChgID"))
            FI_LastChgDate.SetInitValue(dr("LastChgDate"))
            FI_IsEVManager.SetInitValue(dr("IsEVManager"))

            m_LoadFromDataRow = True
        End Sub

        Private Sub ClearUpdated()
            FI_CompID.Updated = False
            FI_PositionID.Updated = False
            FI_Remark.Updated = False
            FI_OrganPrintFlag.Updated = False
            FI_InValidFlag.Updated = False
            FI_CategoryI.Updated = False
            FI_CategoryII.Updated = False
            FI_CategoryIII.Updated = False
            FI_SortOrder.Updated = False
            FI_LastChgComp.Updated = False
            FI_LastChgID.Updated = False
            FI_LastChgDate.Updated = False
            FI_IsEVManager.Updated = False
        End Sub

        Public ReadOnly Property CompID As Field(Of String) 
            Get
                Return FI_CompID
            End Get
        End Property

        Public ReadOnly Property PositionID As Field(Of String) 
            Get
                Return FI_PositionID
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

        Public ReadOnly Property IsEVManager As Field(Of String) 
            Get
                Return FI_IsEVManager
            End Get
        End Property

    End Class

    Public Class Service
        Public Function DeleteRowByPrimaryKey(ByVal PositionRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Delete From Position")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And PositionID = @PositionID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, PositionRow.CompID.Value)
            db.AddInParameter(dbcmd, "@PositionID", DbType.String, PositionRow.PositionID.Value)

            return db.ExecuteNonQuery(dbcmd)
        End Function

        public Function DeleteRowByPrimaryKey(ByVal PositionRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Delete From Position")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And PositionID = @PositionID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, PositionRow.CompID.Value)
            db.AddInParameter(dbcmd, "@PositionID", DbType.String, PositionRow.PositionID.Value)

            return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function DeleteRowByPrimaryKey(ByVal PositionRow As Row()) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Delete From Position")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And PositionID = @PositionID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            using cn As DbConnection = db.CreateConnection()
                cn.Open()
                Dim tran As DbTransaction = cn.BeginTransaction()
                Dim inTrans As Boolean = True

                Try
                    For Each r As Row In PositionRow
                        dbcmd.Parameters.Clear()
                        db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                        db.AddInParameter(dbcmd, "@PositionID", DbType.String, r.PositionID.Value)

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

        Public Function DeleteRowByPrimaryKey(ByVal PositionRow As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Delete From Position")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And PositionID = @PositionID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())

            For Each r As Row In PositionRow
                dbcmd.Parameters.Clear()
                db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                db.AddInParameter(dbcmd, "@PositionID", DbType.String, r.PositionID.Value)

                intRowsAffected += db.ExecuteNonQuery(dbcmd, tran)
            Next
            Return intRowsAffected
        End Function

        Public Function QueryByKey(ByVal PositionRow As Row) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From Position")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And PositionID = @PositionID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, PositionRow.CompID.Value)
            db.AddInParameter(dbcmd, "@PositionID", DbType.String, PositionRow.PositionID.Value)

            Return db.ExecuteDataSet(dbcmd)
        End Function

        Public Function QueryByKey(PositionRow As Row, tran As DbTransaction) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From Position")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And PositionID = @PositionID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, PositionRow.CompID.Value)
            db.AddInParameter(dbcmd, "@PositionID", DbType.String, PositionRow.PositionID.Value)

            Return db.ExecuteDataSet(dbcmd, tran)
        End Function

        Public Function Update(ByVal PositionRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim strDot As String = String.Empty

            strSQL.AppendLine("Update Position Set")
            For i As Integer = 0 To PositionRow.FieldNames.Length - 1
                If Not PositionRow.IsIdentityField(PositionRow.FieldNames(i)) AndAlso PositionRow.IsUpdated(PositionRow.FieldNames(i)) AndAlso PositionRow.CreateUpdateSQL(PositionRow.FieldNames(i)) Then
                    strSQL.AppendLine(string.Format("{0}{1} = @{1}", strDot, PositionRow.FieldNames(i)))
                    strDot = ","
                End If
            Next
            If strDot = String.Empty Then Throw New Exception("未異動資料欄位！")
            strSQL.AppendLine("Where CompID = @PKCompID")
            strSQL.AppendLine("And PositionID = @PKPositionID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            If PositionRow.CompID.Updated Then db.AddInParameter(dbcmd, "@CompID", DbType.String, PositionRow.CompID.Value)
            If PositionRow.PositionID.Updated Then db.AddInParameter(dbcmd, "@PositionID", DbType.String, PositionRow.PositionID.Value)
            If PositionRow.Remark.Updated Then db.AddInParameter(dbcmd, "@Remark", DbType.String, PositionRow.Remark.Value)
            If PositionRow.OrganPrintFlag.Updated Then db.AddInParameter(dbcmd, "@OrganPrintFlag", DbType.String, PositionRow.OrganPrintFlag.Value)
            If PositionRow.InValidFlag.Updated Then db.AddInParameter(dbcmd, "@InValidFlag", DbType.String, PositionRow.InValidFlag.Value)
            If PositionRow.CategoryI.Updated Then db.AddInParameter(dbcmd, "@CategoryI", DbType.String, PositionRow.CategoryI.Value)
            If PositionRow.CategoryII.Updated Then db.AddInParameter(dbcmd, "@CategoryII", DbType.String, PositionRow.CategoryII.Value)
            If PositionRow.CategoryIII.Updated Then db.AddInParameter(dbcmd, "@CategoryIII", DbType.String, PositionRow.CategoryIII.Value)
            If PositionRow.SortOrder.Updated Then db.AddInParameter(dbcmd, "@SortOrder", DbType.String, PositionRow.SortOrder.Value)
            If PositionRow.LastChgComp.Updated Then db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, PositionRow.LastChgComp.Value)
            If PositionRow.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, PositionRow.LastChgID.Value)
            If PositionRow.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(PositionRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), PositionRow.LastChgDate.Value))
            If PositionRow.IsEVManager.Updated Then db.AddInParameter(dbcmd, "@IsEVManager", DbType.String, PositionRow.IsEVManager.Value)
            db.AddInParameter(dbcmd, "@PKCompID", DbType.String, IIf(PositionRow.LoadFromDataRow, PositionRow.CompID.OldValue, PositionRow.CompID.Value))
            db.AddInParameter(dbcmd, "@PKPositionID", DbType.String, IIf(PositionRow.LoadFromDataRow, PositionRow.PositionID.OldValue, PositionRow.PositionID.Value))

            Return db.ExecuteNonQuery(dbcmd)
        End Function

        Public Function Update(ByVal PositionRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim strDot As String = String.Empty

            strSQL.AppendLine("Update Position Set")
            For i As Integer = 0 To PositionRow.FieldNames.Length - 1
                If Not PositionRow.IsIdentityField(PositionRow.FieldNames(i)) AndAlso PositionRow.IsUpdated(PositionRow.FieldNames(i)) AndAlso PositionRow.CreateUpdateSQL(PositionRow.FieldNames(i)) Then
                    strSQL.AppendLine(string.Format("{0}{1} = @{1}", strDot, PositionRow.FieldNames(i)))
                    strDot = ","
                End If
            Next 
            If strDot = String.Empty Then Throw New Exception("未異動資料欄位！")
            strSQL.AppendLine("Where CompID = @PKCompID")
            strSQL.AppendLine("And PositionID = @PKPositionID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            If PositionRow.CompID.Updated Then db.AddInParameter(dbcmd, "@CompID", DbType.String, PositionRow.CompID.Value)
            If PositionRow.PositionID.Updated Then db.AddInParameter(dbcmd, "@PositionID", DbType.String, PositionRow.PositionID.Value)
            If PositionRow.Remark.Updated Then db.AddInParameter(dbcmd, "@Remark", DbType.String, PositionRow.Remark.Value)
            If PositionRow.OrganPrintFlag.Updated Then db.AddInParameter(dbcmd, "@OrganPrintFlag", DbType.String, PositionRow.OrganPrintFlag.Value)
            If PositionRow.InValidFlag.Updated Then db.AddInParameter(dbcmd, "@InValidFlag", DbType.String, PositionRow.InValidFlag.Value)
            If PositionRow.CategoryI.Updated Then db.AddInParameter(dbcmd, "@CategoryI", DbType.String, PositionRow.CategoryI.Value)
            If PositionRow.CategoryII.Updated Then db.AddInParameter(dbcmd, "@CategoryII", DbType.String, PositionRow.CategoryII.Value)
            If PositionRow.CategoryIII.Updated Then db.AddInParameter(dbcmd, "@CategoryIII", DbType.String, PositionRow.CategoryIII.Value)
            If PositionRow.SortOrder.Updated Then db.AddInParameter(dbcmd, "@SortOrder", DbType.String, PositionRow.SortOrder.Value)
            If PositionRow.LastChgComp.Updated Then db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, PositionRow.LastChgComp.Value)
            If PositionRow.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, PositionRow.LastChgID.Value)
            If PositionRow.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(PositionRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), PositionRow.LastChgDate.Value))
            If PositionRow.IsEVManager.Updated Then db.AddInParameter(dbcmd, "@IsEVManager", DbType.String, PositionRow.IsEVManager.Value)
            db.AddInParameter(dbcmd, "@PKCompID", DbType.String, IIf(PositionRow.LoadFromDataRow, PositionRow.CompID.OldValue, PositionRow.CompID.Value))
            db.AddInParameter(dbcmd, "@PKPositionID", DbType.String, IIf(PositionRow.LoadFromDataRow, PositionRow.PositionID.OldValue, PositionRow.PositionID.Value))

            Return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function Update(ByVal PositionRow As Row()) As Integer
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
                    For Each r As Row In PositionRow
                        strDot = String.Empty
                        If strSQL.Length > 0 Then strSQL.Remove(0, strSQL.Length)
                        strSQL.AppendLine("Update Position Set")
                        For i As Integer = 0 To r.FieldNames.Length - 1
                            If Not r.IsIdentityField(r.FieldNames(i)) AndAlso r.IsUpdated(r.FieldNames(i)) AndAlso r.CreateUpdateSQL(r.FieldNames(i)) Then
                                strSQL.AppendLine(String.Format("{0}{1} = @{1}", strDot, r.FieldNames(i)))
                                strDot = ","
                            End If
                        Next
                        If strDot = String.Empty Then Continue For
                        strSQL.AppendLine("Where CompID = @PKCompID")
                        strSQL.AppendLine("And PositionID = @PKPositionID")

                        dbcmd = db.GetSqlStringCommand(strSQL.ToString())
                        If r.CompID.Updated Then db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                        If r.PositionID.Updated Then db.AddInParameter(dbcmd, "@PositionID", DbType.String, r.PositionID.Value)
                        If r.Remark.Updated Then db.AddInParameter(dbcmd, "@Remark", DbType.String, r.Remark.Value)
                        If r.OrganPrintFlag.Updated Then db.AddInParameter(dbcmd, "@OrganPrintFlag", DbType.String, r.OrganPrintFlag.Value)
                        If r.InValidFlag.Updated Then db.AddInParameter(dbcmd, "@InValidFlag", DbType.String, r.InValidFlag.Value)
                        If r.CategoryI.Updated Then db.AddInParameter(dbcmd, "@CategoryI", DbType.String, r.CategoryI.Value)
                        If r.CategoryII.Updated Then db.AddInParameter(dbcmd, "@CategoryII", DbType.String, r.CategoryII.Value)
                        If r.CategoryIII.Updated Then db.AddInParameter(dbcmd, "@CategoryIII", DbType.String, r.CategoryIII.Value)
                        If r.SortOrder.Updated Then db.AddInParameter(dbcmd, "@SortOrder", DbType.String, r.SortOrder.Value)
                        If r.LastChgComp.Updated Then db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, r.LastChgComp.Value)
                        If r.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, r.LastChgID.Value)
                        If r.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(r.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), r.LastChgDate.Value))
                        If r.IsEVManager.Updated Then db.AddInParameter(dbcmd, "@IsEVManager", DbType.String, r.IsEVManager.Value)
                        db.AddInParameter(dbcmd, "@PKCompID", DbType.String, IIf(r.LoadFromDataRow, r.CompID.OldValue, r.CompID.Value))
                        db.AddInParameter(dbcmd, "@PKPositionID", DbType.String, IIf(r.LoadFromDataRow, r.PositionID.OldValue, r.PositionID.Value))

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

        Public Function Update(ByVal PositionRow As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0
            Dim strDot As String

            For Each r As Row In PositionRow
                strDot = String.Empty
                If strSQL.Length > 0 Then strSQL.Remove(0, strSQL.Length)
                strSQL.AppendLine("Update Position Set")
                For i As Integer = 0 To r.FieldNames.Length - 1
                    If Not r.IsIdentityField(r.FieldNames(i)) AndAlso r.IsUpdated(r.FieldNames(i)) AndAlso r.CreateUpdateSQL(r.FieldNames(i))
                        strSQL.AppendLine(String.Format("{0}{1} = @{1}", strDot, r.FieldNames(i)))
                        strDot = ","
                    End If
                Next
                If strDot = String.Empty Then Continue For
                strSQL.AppendLine("Where CompID = @PKCompID")
                strSQL.AppendLine("And PositionID = @PKPositionID")

                dbcmd = db.GetSqlStringCommand(strSQL.ToString())
                If r.CompID.Updated Then db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                If r.PositionID.Updated Then db.AddInParameter(dbcmd, "@PositionID", DbType.String, r.PositionID.Value)
                If r.Remark.Updated Then db.AddInParameter(dbcmd, "@Remark", DbType.String, r.Remark.Value)
                If r.OrganPrintFlag.Updated Then db.AddInParameter(dbcmd, "@OrganPrintFlag", DbType.String, r.OrganPrintFlag.Value)
                If r.InValidFlag.Updated Then db.AddInParameter(dbcmd, "@InValidFlag", DbType.String, r.InValidFlag.Value)
                If r.CategoryI.Updated Then db.AddInParameter(dbcmd, "@CategoryI", DbType.String, r.CategoryI.Value)
                If r.CategoryII.Updated Then db.AddInParameter(dbcmd, "@CategoryII", DbType.String, r.CategoryII.Value)
                If r.CategoryIII.Updated Then db.AddInParameter(dbcmd, "@CategoryIII", DbType.String, r.CategoryIII.Value)
                If r.SortOrder.Updated Then db.AddInParameter(dbcmd, "@SortOrder", DbType.String, r.SortOrder.Value)
                If r.LastChgComp.Updated Then db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, r.LastChgComp.Value)
                If r.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, r.LastChgID.Value)
                If r.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(r.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), r.LastChgDate.Value))
                If r.IsEVManager.Updated Then db.AddInParameter(dbcmd, "@IsEVManager", DbType.String, r.IsEVManager.Value)
                db.AddInParameter(dbcmd, "@PKCompID", DbType.String, IIf(r.LoadFromDataRow, r.CompID.OldValue, r.CompID.Value))
                db.AddInParameter(dbcmd, "@PKPositionID", DbType.String, IIf(r.LoadFromDataRow, r.PositionID.OldValue, r.PositionID.Value))

                intRowsAffected += db.ExecuteNonQuery(dbcmd, tran)
            Next
            Return intRowsAffected
        End Function

        Public Function IsDataExists(ByVal PositionRow As Row) As Boolean
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intCount As Integer = 0

            strSQL.AppendLine("Select Count(*) Cnt From Position")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And PositionID = @PositionID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, PositionRow.CompID.Value)
            db.AddInParameter(dbcmd, "@PositionID", DbType.String, PositionRow.PositionID.Value)

            intCount = Convert.ToInt32(db.ExecuteScalar(dbcmd))
            Return IIf(intCount > 0, True, False)
        End Function

        Public Function IsDataExists(ByVal PositionRow As Row, ByVal tran As DbTransaction) As Boolean
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intCount As Integer = 0

            strSQL.AppendLine("Select Count(*) Cnt From Position")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And PositionID = @PositionID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, PositionRow.CompID.Value)
            db.AddInParameter(dbcmd, "@PositionID", DbType.String, PositionRow.PositionID.Value)

            intCount = Convert.ToInt32(db.ExecuteScalar(dbcmd, tran))
            Return IIf(intCount > 0, True, False)
        End Function

        Public Function QuerybyWhere(WhereCondition As String) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From Position")
            If WhereCondition <> String.Empty Then strSQL.AppendLine(WhereCondition)

            Return db.ExecuteDataSet(CommandType.Text, strSQL.ToString())
        End Function

        Public Function Insert(ByVal PositionRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Insert into Position")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    CompID, PositionID, Remark, OrganPrintFlag, InValidFlag, CategoryI, CategoryII, CategoryIII,")
            strSQL.AppendLine("    SortOrder, LastChgComp, LastChgID, LastChgDate, IsEVManager")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @CompID, @PositionID, @Remark, @OrganPrintFlag, @InValidFlag, @CategoryI, @CategoryII, @CategoryIII,")
            strSQL.AppendLine("    @SortOrder, @LastChgComp, @LastChgID, @LastChgDate, @IsEVManager")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, PositionRow.CompID.Value)
            db.AddInParameter(dbcmd, "@PositionID", DbType.String, PositionRow.PositionID.Value)
            db.AddInParameter(dbcmd, "@Remark", DbType.String, PositionRow.Remark.Value)
            db.AddInParameter(dbcmd, "@OrganPrintFlag", DbType.String, PositionRow.OrganPrintFlag.Value)
            db.AddInParameter(dbcmd, "@InValidFlag", DbType.String, PositionRow.InValidFlag.Value)
            db.AddInParameter(dbcmd, "@CategoryI", DbType.String, PositionRow.CategoryI.Value)
            db.AddInParameter(dbcmd, "@CategoryII", DbType.String, PositionRow.CategoryII.Value)
            db.AddInParameter(dbcmd, "@CategoryIII", DbType.String, PositionRow.CategoryIII.Value)
            db.AddInParameter(dbcmd, "@SortOrder", DbType.String, PositionRow.SortOrder.Value)
            db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, PositionRow.LastChgComp.Value)
            db.AddInParameter(dbcmd, "@LastChgID", DbType.String, PositionRow.LastChgID.Value)
            db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(PositionRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), PositionRow.LastChgDate.Value))
            db.AddInParameter(dbcmd, "@IsEVManager", DbType.String, PositionRow.IsEVManager.Value)

            Return db.ExecuteNonQuery(dbcmd)
        End Function

        Public Function Insert(ByVal PositionRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Insert into Position")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    CompID, PositionID, Remark, OrganPrintFlag, InValidFlag, CategoryI, CategoryII, CategoryIII,")
            strSQL.AppendLine("    SortOrder, LastChgComp, LastChgID, LastChgDate, IsEVManager")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @CompID, @PositionID, @Remark, @OrganPrintFlag, @InValidFlag, @CategoryI, @CategoryII, @CategoryIII,")
            strSQL.AppendLine("    @SortOrder, @LastChgComp, @LastChgID, @LastChgDate, @IsEVManager")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, PositionRow.CompID.Value)
            db.AddInParameter(dbcmd, "@PositionID", DbType.String, PositionRow.PositionID.Value)
            db.AddInParameter(dbcmd, "@Remark", DbType.String, PositionRow.Remark.Value)
            db.AddInParameter(dbcmd, "@OrganPrintFlag", DbType.String, PositionRow.OrganPrintFlag.Value)
            db.AddInParameter(dbcmd, "@InValidFlag", DbType.String, PositionRow.InValidFlag.Value)
            db.AddInParameter(dbcmd, "@CategoryI", DbType.String, PositionRow.CategoryI.Value)
            db.AddInParameter(dbcmd, "@CategoryII", DbType.String, PositionRow.CategoryII.Value)
            db.AddInParameter(dbcmd, "@CategoryIII", DbType.String, PositionRow.CategoryIII.Value)
            db.AddInParameter(dbcmd, "@SortOrder", DbType.String, PositionRow.SortOrder.Value)
            db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, PositionRow.LastChgComp.Value)
            db.AddInParameter(dbcmd, "@LastChgID", DbType.String, PositionRow.LastChgID.Value)
            db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(PositionRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), PositionRow.LastChgDate.Value))
            db.AddInParameter(dbcmd, "@IsEVManager", DbType.String, PositionRow.IsEVManager.Value)

            Return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function Insert(ByVal PositionRow As Row()) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Insert into Position")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    CompID, PositionID, Remark, OrganPrintFlag, InValidFlag, CategoryI, CategoryII, CategoryIII,")
            strSQL.AppendLine("    SortOrder, LastChgComp, LastChgID, LastChgDate, IsEVManager")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @CompID, @PositionID, @Remark, @OrganPrintFlag, @InValidFlag, @CategoryI, @CategoryII, @CategoryIII,")
            strSQL.AppendLine("    @SortOrder, @LastChgComp, @LastChgID, @LastChgDate, @IsEVManager")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            Using cn As DbConnection = db.CreateConnection()
                cn.Open()
                Dim tran As DbTransaction = cn.BeginTransaction()
                Dim inTrans As Boolean = True

                Try
                    For Each r As Row In PositionRow
                        dbcmd.Parameters.Clear()
                        db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                        db.AddInParameter(dbcmd, "@PositionID", DbType.String, r.PositionID.Value)
                        db.AddInParameter(dbcmd, "@Remark", DbType.String, r.Remark.Value)
                        db.AddInParameter(dbcmd, "@OrganPrintFlag", DbType.String, r.OrganPrintFlag.Value)
                        db.AddInParameter(dbcmd, "@InValidFlag", DbType.String, r.InValidFlag.Value)
                        db.AddInParameter(dbcmd, "@CategoryI", DbType.String, r.CategoryI.Value)
                        db.AddInParameter(dbcmd, "@CategoryII", DbType.String, r.CategoryII.Value)
                        db.AddInParameter(dbcmd, "@CategoryIII", DbType.String, r.CategoryIII.Value)
                        db.AddInParameter(dbcmd, "@SortOrder", DbType.String, r.SortOrder.Value)
                        db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, r.LastChgComp.Value)
                        db.AddInParameter(dbcmd, "@LastChgID", DbType.String, r.LastChgID.Value)
                        db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(r.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), r.LastChgDate.Value))
                        db.AddInParameter(dbcmd, "@IsEVManager", DbType.String, r.IsEVManager.Value)

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

        Public Function Insert(ByVal PositionRow As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Insert into Position")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    CompID, PositionID, Remark, OrganPrintFlag, InValidFlag, CategoryI, CategoryII, CategoryIII,")
            strSQL.AppendLine("    SortOrder, LastChgComp, LastChgID, LastChgDate, IsEVManager")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @CompID, @PositionID, @Remark, @OrganPrintFlag, @InValidFlag, @CategoryI, @CategoryII, @CategoryIII,")
            strSQL.AppendLine("    @SortOrder, @LastChgComp, @LastChgID, @LastChgDate, @IsEVManager")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())

            For Each r As Row In PositionRow
                dbcmd.Parameters.Clear()
                db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                db.AddInParameter(dbcmd, "@PositionID", DbType.String, r.PositionID.Value)
                db.AddInParameter(dbcmd, "@Remark", DbType.String, r.Remark.Value)
                db.AddInParameter(dbcmd, "@OrganPrintFlag", DbType.String, r.OrganPrintFlag.Value)
                db.AddInParameter(dbcmd, "@InValidFlag", DbType.String, r.InValidFlag.Value)
                db.AddInParameter(dbcmd, "@CategoryI", DbType.String, r.CategoryI.Value)
                db.AddInParameter(dbcmd, "@CategoryII", DbType.String, r.CategoryII.Value)
                db.AddInParameter(dbcmd, "@CategoryIII", DbType.String, r.CategoryIII.Value)
                db.AddInParameter(dbcmd, "@SortOrder", DbType.String, r.SortOrder.Value)
                db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, r.LastChgComp.Value)
                db.AddInParameter(dbcmd, "@LastChgID", DbType.String, r.LastChgID.Value)
                db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(r.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), r.LastChgDate.Value))
                db.AddInParameter(dbcmd, "@IsEVManager", DbType.String, r.IsEVManager.Value)

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

