'****************************************************************
' Table:SC_BatchRule
' Created Date: 2014.08.06
'****************************************************************/
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports Microsoft.Practices.EnterpriseLibrary.Common
Imports Microsoft.Practices.EnterpriseLibrary.Data
Imports System.Data.Common
Imports System.Data

Namespace beSC_BatchRule
    Public Class Table
        Private m_Rows As Rows = New Rows()
        Private m_Fields As String() = { "DeptID", "OrganID", "WorkTypeID", "UpdateSeq", "BanMark", "GroupID", "BusinessFlag", "Description" }
        Private m_Types As System.Type() = { GetType(String), GetType(String), GetType(String), GetType(Integer), GetType(String), GetType(String), GetType(String), GetType(String) }
        Private m_PrimaryFields As String() = { "DeptID", "OrganID", "WorkTypeID", "UpdateSeq" }

        Public ReadOnly Property Rows() As beSC_BatchRule.Rows 
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
        Public Sub Transfer2Row(SC_BatchRuleTable As DataTable)
            For Each dr As DataRow In SC_BatchRuleTable.Rows
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

                dr(m_Rows(i).DeptID.FieldName) = m_Rows(i).DeptID.Value
                dr(m_Rows(i).OrganID.FieldName) = m_Rows(i).OrganID.Value
                dr(m_Rows(i).WorkTypeID.FieldName) = m_Rows(i).WorkTypeID.Value
                dr(m_Rows(i).UpdateSeq.FieldName) = m_Rows(i).UpdateSeq.Value
                dr(m_Rows(i).BanMark.FieldName) = m_Rows(i).BanMark.Value
                dr(m_Rows(i).GroupID.FieldName) = m_Rows(i).GroupID.Value
                dr(m_Rows(i).BusinessFlag.FieldName) = m_Rows(i).BusinessFlag.Value
                dr(m_Rows(i).Description.FieldName) = m_Rows(i).Description.Value

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

        Public Sub Add(SC_BatchRuleRow As Row)
            m_Rows.Add(SC_BatchRuleRow)
        End Sub

        Public Sub Remove(SC_BatchRuleRow As Row)
            If m_Rows.IndexOf(SC_BatchRuleRow) >= 0 Then
                m_Rows.Remove(SC_BatchRuleRow)
            End If
        End Sub

        Public Sub Dispose()
            m_Rows.Clear()
        End Sub

    End Class

    Public Class Row

        Private FI_DeptID As Field(Of String) = new Field(Of String)("DeptID", true)
        Private FI_OrganID As Field(Of String) = new Field(Of String)("OrganID", true)
        Private FI_WorkTypeID As Field(Of String) = new Field(Of String)("WorkTypeID", true)
        Private FI_UpdateSeq As Field(Of Integer) = new Field(Of Integer)("UpdateSeq", true)
        Private FI_BanMark As Field(Of String) = new Field(Of String)("BanMark", true)
        Private FI_GroupID As Field(Of String) = new Field(Of String)("GroupID", true)
        Private FI_BusinessFlag As Field(Of String) = new Field(Of String)("BusinessFlag", true)
        Private FI_Description As Field(Of String) = new Field(Of String)("Description", true)
        Private m_FieldNames As String() = { "DeptID", "OrganID", "WorkTypeID", "UpdateSeq", "BanMark", "GroupID", "BusinessFlag", "Description" }
        Private m_PrimaryFields As String() = { "DeptID", "OrganID", "WorkTypeID", "UpdateSeq" }
        Private m_IdentityFields As String() = {  }
        Private m_LoadFromDataRow As Boolean = False

        Private Function GetFieldValue(ByVal fieldName As String) As Object
            Select Case fieldName
                Case "DeptID"
                    Return FI_DeptID.Value
                Case "OrganID"
                    Return FI_OrganID.Value
                Case "WorkTypeID"
                    Return FI_WorkTypeID.Value
                Case "UpdateSeq"
                    Return FI_UpdateSeq.Value
                Case "BanMark"
                    Return FI_BanMark.Value
                Case "GroupID"
                    Return FI_GroupID.Value
                Case "BusinessFlag"
                    Return FI_BusinessFlag.Value
                Case "Description"
                    Return FI_Description.Value
                Case Else
                    Return Nothing
            End Select
        End Function

        Private Sub SetFieldValue(ByVal fieldName As String, ByVal value As Object)
            Select Case fieldName
                Case "DeptID"
                    FI_DeptID.SetValue(value)
                Case "OrganID"
                    FI_OrganID.SetValue(value)
                Case "WorkTypeID"
                    FI_WorkTypeID.SetValue(value)
                Case "UpdateSeq"
                    FI_UpdateSeq.SetValue(value)
                Case "BanMark"
                    FI_BanMark.SetValue(value)
                Case "GroupID"
                    FI_GroupID.SetValue(value)
                Case "BusinessFlag"
                    FI_BusinessFlag.SetValue(value)
                Case "Description"
                    FI_Description.SetValue(value)
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
                Case "DeptID"
                    return FI_DeptID.Updated
                Case "OrganID"
                    return FI_OrganID.Updated
                Case "WorkTypeID"
                    return FI_WorkTypeID.Updated
                Case "UpdateSeq"
                    return FI_UpdateSeq.Updated
                Case "BanMark"
                    return FI_BanMark.Updated
                Case "GroupID"
                    return FI_GroupID.Updated
                Case "BusinessFlag"
                    return FI_BusinessFlag.Updated
                Case "Description"
                    return FI_Description.Updated
                Case Else
                    Throw New Exception("無此欄位！")
            End Select
        End Function

        Public Function CreateUpdateSQL(ByVal fieldName As String) As Boolean
            Select Case fieldName
                Case "DeptID"
                    return FI_DeptID.CreateUpdateSQL
                Case "OrganID"
                    return FI_OrganID.CreateUpdateSQL
                Case "WorkTypeID"
                    return FI_WorkTypeID.CreateUpdateSQL
                Case "UpdateSeq"
                    return FI_UpdateSeq.CreateUpdateSQL
                Case "BanMark"
                    return FI_BanMark.CreateUpdateSQL
                Case "GroupID"
                    return FI_GroupID.CreateUpdateSQL
                Case "BusinessFlag"
                    return FI_BusinessFlag.CreateUpdateSQL
                Case "Description"
                    return FI_Description.CreateUpdateSQL
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
            FI_DeptID.SetInitValue("")
            FI_OrganID.SetInitValue("")
            FI_WorkTypeID.SetInitValue("")
            FI_UpdateSeq.SetInitValue("")
            FI_BanMark.SetInitValue("0")
            FI_GroupID.SetInitValue("")
            FI_BusinessFlag.SetInitValue("0")
        End Sub

        Public Sub New(ByVal dr As System.Data.DataRow)
            FI_DeptID.SetInitValue(dr("DeptID"))
            FI_OrganID.SetInitValue(dr("OrganID"))
            FI_WorkTypeID.SetInitValue(dr("WorkTypeID"))
            FI_UpdateSeq.SetInitValue(dr("UpdateSeq"))
            FI_BanMark.SetInitValue(dr("BanMark"))
            FI_GroupID.SetInitValue(dr("GroupID"))
            FI_BusinessFlag.SetInitValue(dr("BusinessFlag"))
            FI_Description.SetInitValue(dr("Description"))

            m_LoadFromDataRow = True
        End Sub

        Private Sub ClearUpdated()
            FI_DeptID.Updated = False
            FI_OrganID.Updated = False
            FI_WorkTypeID.Updated = False
            FI_UpdateSeq.Updated = False
            FI_BanMark.Updated = False
            FI_GroupID.Updated = False
            FI_BusinessFlag.Updated = False
            FI_Description.Updated = False
        End Sub

        Public ReadOnly Property DeptID As Field(Of String) 
            Get
                Return FI_DeptID
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

        Public ReadOnly Property UpdateSeq As Field(Of Integer) 
            Get
                Return FI_UpdateSeq
            End Get
        End Property

        Public ReadOnly Property BanMark As Field(Of String) 
            Get
                Return FI_BanMark
            End Get
        End Property

        Public ReadOnly Property GroupID As Field(Of String) 
            Get
                Return FI_GroupID
            End Get
        End Property

        Public ReadOnly Property BusinessFlag As Field(Of String) 
            Get
                Return FI_BusinessFlag
            End Get
        End Property

        Public ReadOnly Property Description As Field(Of String) 
            Get
                Return FI_Description
            End Get
        End Property

    End Class

    Public Class Service
        Public Function DeleteRowByPrimaryKey(ByVal SC_BatchRuleRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Delete From SC_BatchRule")
            strSQL.AppendLine("Where DeptID = @DeptID")
            strSQL.AppendLine("And OrganID = @OrganID")
            strSQL.AppendLine("And WorkTypeID = @WorkTypeID")
            strSQL.AppendLine("And UpdateSeq = @UpdateSeq")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@DeptID", DbType.String, SC_BatchRuleRow.DeptID.Value)
            db.AddInParameter(dbcmd, "@OrganID", DbType.String, SC_BatchRuleRow.OrganID.Value)
            db.AddInParameter(dbcmd, "@WorkTypeID", DbType.String, SC_BatchRuleRow.WorkTypeID.Value)
            db.AddInParameter(dbcmd, "@UpdateSeq", DbType.Int32, SC_BatchRuleRow.UpdateSeq.Value)

            return db.ExecuteNonQuery(dbcmd)
        End Function

        public Function DeleteRowByPrimaryKey(ByVal SC_BatchRuleRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Delete From SC_BatchRule")
            strSQL.AppendLine("Where DeptID = @DeptID")
            strSQL.AppendLine("And OrganID = @OrganID")
            strSQL.AppendLine("And WorkTypeID = @WorkTypeID")
            strSQL.AppendLine("And UpdateSeq = @UpdateSeq")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@DeptID", DbType.String, SC_BatchRuleRow.DeptID.Value)
            db.AddInParameter(dbcmd, "@OrganID", DbType.String, SC_BatchRuleRow.OrganID.Value)
            db.AddInParameter(dbcmd, "@WorkTypeID", DbType.String, SC_BatchRuleRow.WorkTypeID.Value)
            db.AddInParameter(dbcmd, "@UpdateSeq", DbType.Int32, SC_BatchRuleRow.UpdateSeq.Value)

            return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function DeleteRowByPrimaryKey(ByVal SC_BatchRuleRow As Row()) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Delete From SC_BatchRule")
            strSQL.AppendLine("Where DeptID = @DeptID")
            strSQL.AppendLine("And OrganID = @OrganID")
            strSQL.AppendLine("And WorkTypeID = @WorkTypeID")
            strSQL.AppendLine("And UpdateSeq = @UpdateSeq")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            using cn As DbConnection = db.CreateConnection()
                cn.Open()
                Dim tran As DbTransaction = cn.BeginTransaction()
                Dim inTrans As Boolean = True

                Try
                    For Each r As Row In SC_BatchRuleRow
                        dbcmd.Parameters.Clear()
                        db.AddInParameter(dbcmd, "@DeptID", DbType.String, r.DeptID.Value)
                        db.AddInParameter(dbcmd, "@OrganID", DbType.String, r.OrganID.Value)
                        db.AddInParameter(dbcmd, "@WorkTypeID", DbType.String, r.WorkTypeID.Value)
                        db.AddInParameter(dbcmd, "@UpdateSeq", DbType.Int32, r.UpdateSeq.Value)

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

        Public Function DeleteRowByPrimaryKey(ByVal SC_BatchRuleRow As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Delete From SC_BatchRule")
            strSQL.AppendLine("Where DeptID = @DeptID")
            strSQL.AppendLine("And OrganID = @OrganID")
            strSQL.AppendLine("And WorkTypeID = @WorkTypeID")
            strSQL.AppendLine("And UpdateSeq = @UpdateSeq")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())

            For Each r As Row In SC_BatchRuleRow
                dbcmd.Parameters.Clear()
                db.AddInParameter(dbcmd, "@DeptID", DbType.String, r.DeptID.Value)
                db.AddInParameter(dbcmd, "@OrganID", DbType.String, r.OrganID.Value)
                db.AddInParameter(dbcmd, "@WorkTypeID", DbType.String, r.WorkTypeID.Value)
                db.AddInParameter(dbcmd, "@UpdateSeq", DbType.Int32, r.UpdateSeq.Value)

                intRowsAffected += db.ExecuteNonQuery(dbcmd, tran)
            Next
            Return intRowsAffected
        End Function

        Public Function QueryByKey(ByVal SC_BatchRuleRow As Row) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From SC_BatchRule")
            strSQL.AppendLine("Where DeptID = @DeptID")
            strSQL.AppendLine("And OrganID = @OrganID")
            strSQL.AppendLine("And WorkTypeID = @WorkTypeID")
            strSQL.AppendLine("And UpdateSeq = @UpdateSeq")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@DeptID", DbType.String, SC_BatchRuleRow.DeptID.Value)
            db.AddInParameter(dbcmd, "@OrganID", DbType.String, SC_BatchRuleRow.OrganID.Value)
            db.AddInParameter(dbcmd, "@WorkTypeID", DbType.String, SC_BatchRuleRow.WorkTypeID.Value)
            db.AddInParameter(dbcmd, "@UpdateSeq", DbType.Int32, SC_BatchRuleRow.UpdateSeq.Value)

            Return db.ExecuteDataSet(dbcmd)
        End Function

        Public Function QueryByKey(SC_BatchRuleRow As Row, tran As DbTransaction) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From SC_BatchRule")
            strSQL.AppendLine("Where DeptID = @DeptID")
            strSQL.AppendLine("And OrganID = @OrganID")
            strSQL.AppendLine("And WorkTypeID = @WorkTypeID")
            strSQL.AppendLine("And UpdateSeq = @UpdateSeq")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@DeptID", DbType.String, SC_BatchRuleRow.DeptID.Value)
            db.AddInParameter(dbcmd, "@OrganID", DbType.String, SC_BatchRuleRow.OrganID.Value)
            db.AddInParameter(dbcmd, "@WorkTypeID", DbType.String, SC_BatchRuleRow.WorkTypeID.Value)
            db.AddInParameter(dbcmd, "@UpdateSeq", DbType.Int32, SC_BatchRuleRow.UpdateSeq.Value)

            Return db.ExecuteDataSet(dbcmd, tran)
        End Function

        Public Function Update(ByVal SC_BatchRuleRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim strDot As String = String.Empty

            strSQL.AppendLine("Update SC_BatchRule Set")
            For i As Integer = 0 To SC_BatchRuleRow.FieldNames.Length - 1
                If Not SC_BatchRuleRow.IsIdentityField(SC_BatchRuleRow.FieldNames(i)) AndAlso SC_BatchRuleRow.IsUpdated(SC_BatchRuleRow.FieldNames(i)) AndAlso SC_BatchRuleRow.CreateUpdateSQL(SC_BatchRuleRow.FieldNames(i)) Then
                    strSQL.AppendLine(string.Format("{0}{1} = @{1}", strDot, SC_BatchRuleRow.FieldNames(i)))
                    strDot = ","
                End If
            Next
            If strDot = String.Empty Then Throw New Exception("未異動資料欄位！")
            strSQL.AppendLine("Where DeptID = @PKDeptID")
            strSQL.AppendLine("And OrganID = @PKOrganID")
            strSQL.AppendLine("And WorkTypeID = @PKWorkTypeID")
            strSQL.AppendLine("And UpdateSeq = @PKUpdateSeq")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            If SC_BatchRuleRow.DeptID.Updated Then db.AddInParameter(dbcmd, "@DeptID", DbType.String, SC_BatchRuleRow.DeptID.Value)
            If SC_BatchRuleRow.OrganID.Updated Then db.AddInParameter(dbcmd, "@OrganID", DbType.String, SC_BatchRuleRow.OrganID.Value)
            If SC_BatchRuleRow.WorkTypeID.Updated Then db.AddInParameter(dbcmd, "@WorkTypeID", DbType.String, SC_BatchRuleRow.WorkTypeID.Value)
            If SC_BatchRuleRow.UpdateSeq.Updated Then db.AddInParameter(dbcmd, "@UpdateSeq", DbType.Int32, SC_BatchRuleRow.UpdateSeq.Value)
            If SC_BatchRuleRow.BanMark.Updated Then db.AddInParameter(dbcmd, "@BanMark", DbType.String, SC_BatchRuleRow.BanMark.Value)
            If SC_BatchRuleRow.GroupID.Updated Then db.AddInParameter(dbcmd, "@GroupID", DbType.String, SC_BatchRuleRow.GroupID.Value)
            If SC_BatchRuleRow.BusinessFlag.Updated Then db.AddInParameter(dbcmd, "@BusinessFlag", DbType.String, SC_BatchRuleRow.BusinessFlag.Value)
            If SC_BatchRuleRow.Description.Updated Then db.AddInParameter(dbcmd, "@Description", DbType.String, SC_BatchRuleRow.Description.Value)
            db.AddInParameter(dbcmd, "@PKDeptID", DbType.String, IIf(SC_BatchRuleRow.LoadFromDataRow, SC_BatchRuleRow.DeptID.OldValue, SC_BatchRuleRow.DeptID.Value))
            db.AddInParameter(dbcmd, "@PKOrganID", DbType.String, IIf(SC_BatchRuleRow.LoadFromDataRow, SC_BatchRuleRow.OrganID.OldValue, SC_BatchRuleRow.OrganID.Value))
            db.AddInParameter(dbcmd, "@PKWorkTypeID", DbType.String, IIf(SC_BatchRuleRow.LoadFromDataRow, SC_BatchRuleRow.WorkTypeID.OldValue, SC_BatchRuleRow.WorkTypeID.Value))
            db.AddInParameter(dbcmd, "@PKUpdateSeq", DbType.Int32, IIf(SC_BatchRuleRow.LoadFromDataRow, SC_BatchRuleRow.UpdateSeq.OldValue, SC_BatchRuleRow.UpdateSeq.Value))

            Return db.ExecuteNonQuery(dbcmd)
        End Function

        Public Function Update(ByVal SC_BatchRuleRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim strDot As String = String.Empty

            strSQL.AppendLine("Update SC_BatchRule Set")
            For i As Integer = 0 To SC_BatchRuleRow.FieldNames.Length - 1
                If Not SC_BatchRuleRow.IsIdentityField(SC_BatchRuleRow.FieldNames(i)) AndAlso SC_BatchRuleRow.IsUpdated(SC_BatchRuleRow.FieldNames(i)) AndAlso SC_BatchRuleRow.CreateUpdateSQL(SC_BatchRuleRow.FieldNames(i)) Then
                    strSQL.AppendLine(string.Format("{0}{1} = @{1}", strDot, SC_BatchRuleRow.FieldNames(i)))
                    strDot = ","
                End If
            Next 
            If strDot = String.Empty Then Throw New Exception("未異動資料欄位！")
            strSQL.AppendLine("Where DeptID = @PKDeptID")
            strSQL.AppendLine("And OrganID = @PKOrganID")
            strSQL.AppendLine("And WorkTypeID = @PKWorkTypeID")
            strSQL.AppendLine("And UpdateSeq = @PKUpdateSeq")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            If SC_BatchRuleRow.DeptID.Updated Then db.AddInParameter(dbcmd, "@DeptID", DbType.String, SC_BatchRuleRow.DeptID.Value)
            If SC_BatchRuleRow.OrganID.Updated Then db.AddInParameter(dbcmd, "@OrganID", DbType.String, SC_BatchRuleRow.OrganID.Value)
            If SC_BatchRuleRow.WorkTypeID.Updated Then db.AddInParameter(dbcmd, "@WorkTypeID", DbType.String, SC_BatchRuleRow.WorkTypeID.Value)
            If SC_BatchRuleRow.UpdateSeq.Updated Then db.AddInParameter(dbcmd, "@UpdateSeq", DbType.Int32, SC_BatchRuleRow.UpdateSeq.Value)
            If SC_BatchRuleRow.BanMark.Updated Then db.AddInParameter(dbcmd, "@BanMark", DbType.String, SC_BatchRuleRow.BanMark.Value)
            If SC_BatchRuleRow.GroupID.Updated Then db.AddInParameter(dbcmd, "@GroupID", DbType.String, SC_BatchRuleRow.GroupID.Value)
            If SC_BatchRuleRow.BusinessFlag.Updated Then db.AddInParameter(dbcmd, "@BusinessFlag", DbType.String, SC_BatchRuleRow.BusinessFlag.Value)
            If SC_BatchRuleRow.Description.Updated Then db.AddInParameter(dbcmd, "@Description", DbType.String, SC_BatchRuleRow.Description.Value)
            db.AddInParameter(dbcmd, "@PKDeptID", DbType.String, IIf(SC_BatchRuleRow.LoadFromDataRow, SC_BatchRuleRow.DeptID.OldValue, SC_BatchRuleRow.DeptID.Value))
            db.AddInParameter(dbcmd, "@PKOrganID", DbType.String, IIf(SC_BatchRuleRow.LoadFromDataRow, SC_BatchRuleRow.OrganID.OldValue, SC_BatchRuleRow.OrganID.Value))
            db.AddInParameter(dbcmd, "@PKWorkTypeID", DbType.String, IIf(SC_BatchRuleRow.LoadFromDataRow, SC_BatchRuleRow.WorkTypeID.OldValue, SC_BatchRuleRow.WorkTypeID.Value))
            db.AddInParameter(dbcmd, "@PKUpdateSeq", DbType.Int32, IIf(SC_BatchRuleRow.LoadFromDataRow, SC_BatchRuleRow.UpdateSeq.OldValue, SC_BatchRuleRow.UpdateSeq.Value))

            Return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function Update(ByVal SC_BatchRuleRow As Row()) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0
            Dim strDot As String

            Using cn As DbConnection = db.CreateConnection()
                cn.Open()
                Dim tran As DbTransaction = cn.BeginTransaction()
                Dim inTrans As Boolean = True

                Try
                    For Each r As Row In SC_BatchRuleRow
                        strDot = String.Empty
                        If strSQL.Length > 0 Then strSQL.Remove(0, strSQL.Length)
                        strSQL.AppendLine("Update SC_BatchRule Set")
                        For i As Integer = 0 To r.FieldNames.Length - 1
                            If Not r.IsIdentityField(r.FieldNames(i)) AndAlso r.IsUpdated(r.FieldNames(i)) AndAlso r.CreateUpdateSQL(r.FieldNames(i)) Then
                                strSQL.AppendLine(String.Format("{0}{1} = @{1}", strDot, r.FieldNames(i)))
                                strDot = ","
                            End If
                        Next
                        If strDot = String.Empty Then Continue For
                        strSQL.AppendLine("Where DeptID = @PKDeptID")
                        strSQL.AppendLine("And OrganID = @PKOrganID")
                        strSQL.AppendLine("And WorkTypeID = @PKWorkTypeID")
                        strSQL.AppendLine("And UpdateSeq = @PKUpdateSeq")

                        dbcmd = db.GetSqlStringCommand(strSQL.ToString())
                        If r.DeptID.Updated Then db.AddInParameter(dbcmd, "@DeptID", DbType.String, r.DeptID.Value)
                        If r.OrganID.Updated Then db.AddInParameter(dbcmd, "@OrganID", DbType.String, r.OrganID.Value)
                        If r.WorkTypeID.Updated Then db.AddInParameter(dbcmd, "@WorkTypeID", DbType.String, r.WorkTypeID.Value)
                        If r.UpdateSeq.Updated Then db.AddInParameter(dbcmd, "@UpdateSeq", DbType.Int32, r.UpdateSeq.Value)
                        If r.BanMark.Updated Then db.AddInParameter(dbcmd, "@BanMark", DbType.String, r.BanMark.Value)
                        If r.GroupID.Updated Then db.AddInParameter(dbcmd, "@GroupID", DbType.String, r.GroupID.Value)
                        If r.BusinessFlag.Updated Then db.AddInParameter(dbcmd, "@BusinessFlag", DbType.String, r.BusinessFlag.Value)
                        If r.Description.Updated Then db.AddInParameter(dbcmd, "@Description", DbType.String, r.Description.Value)
                        db.AddInParameter(dbcmd, "@PKDeptID", DbType.String, IIf(r.LoadFromDataRow, r.DeptID.OldValue, r.DeptID.Value))
                        db.AddInParameter(dbcmd, "@PKOrganID", DbType.String, IIf(r.LoadFromDataRow, r.OrganID.OldValue, r.OrganID.Value))
                        db.AddInParameter(dbcmd, "@PKWorkTypeID", DbType.String, IIf(r.LoadFromDataRow, r.WorkTypeID.OldValue, r.WorkTypeID.Value))
                        db.AddInParameter(dbcmd, "@PKUpdateSeq", DbType.Int32, IIf(r.LoadFromDataRow, r.UpdateSeq.OldValue, r.UpdateSeq.Value))

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

        Public Function Update(ByVal SC_BatchRuleRow As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0
            Dim strDot As String

            For Each r As Row In SC_BatchRuleRow
                strDot = String.Empty
                If strSQL.Length > 0 Then strSQL.Remove(0, strSQL.Length)
                strSQL.AppendLine("Update SC_BatchRule Set")
                For i As Integer = 0 To r.FieldNames.Length - 1
                    If Not r.IsIdentityField(r.FieldNames(i)) AndAlso r.IsUpdated(r.FieldNames(i)) AndAlso r.CreateUpdateSQL(r.FieldNames(i))
                        strSQL.AppendLine(String.Format("{0}{1} = @{1}", strDot, r.FieldNames(i)))
                        strDot = ","
                    End If
                Next
                If strDot = String.Empty Then Continue For
                strSQL.AppendLine("Where DeptID = @PKDeptID")
                strSQL.AppendLine("And OrganID = @PKOrganID")
                strSQL.AppendLine("And WorkTypeID = @PKWorkTypeID")
                strSQL.AppendLine("And UpdateSeq = @PKUpdateSeq")

                dbcmd = db.GetSqlStringCommand(strSQL.ToString())
                If r.DeptID.Updated Then db.AddInParameter(dbcmd, "@DeptID", DbType.String, r.DeptID.Value)
                If r.OrganID.Updated Then db.AddInParameter(dbcmd, "@OrganID", DbType.String, r.OrganID.Value)
                If r.WorkTypeID.Updated Then db.AddInParameter(dbcmd, "@WorkTypeID", DbType.String, r.WorkTypeID.Value)
                If r.UpdateSeq.Updated Then db.AddInParameter(dbcmd, "@UpdateSeq", DbType.Int32, r.UpdateSeq.Value)
                If r.BanMark.Updated Then db.AddInParameter(dbcmd, "@BanMark", DbType.String, r.BanMark.Value)
                If r.GroupID.Updated Then db.AddInParameter(dbcmd, "@GroupID", DbType.String, r.GroupID.Value)
                If r.BusinessFlag.Updated Then db.AddInParameter(dbcmd, "@BusinessFlag", DbType.String, r.BusinessFlag.Value)
                If r.Description.Updated Then db.AddInParameter(dbcmd, "@Description", DbType.String, r.Description.Value)
                db.AddInParameter(dbcmd, "@PKDeptID", DbType.String, IIf(r.LoadFromDataRow, r.DeptID.OldValue, r.DeptID.Value))
                db.AddInParameter(dbcmd, "@PKOrganID", DbType.String, IIf(r.LoadFromDataRow, r.OrganID.OldValue, r.OrganID.Value))
                db.AddInParameter(dbcmd, "@PKWorkTypeID", DbType.String, IIf(r.LoadFromDataRow, r.WorkTypeID.OldValue, r.WorkTypeID.Value))
                db.AddInParameter(dbcmd, "@PKUpdateSeq", DbType.Int32, IIf(r.LoadFromDataRow, r.UpdateSeq.OldValue, r.UpdateSeq.Value))

                intRowsAffected += db.ExecuteNonQuery(dbcmd, tran)
            Next
            Return intRowsAffected
        End Function

        Public Function IsDataExists(ByVal SC_BatchRuleRow As Row) As Boolean
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intCount As Integer = 0

            strSQL.AppendLine("Select Count(*) Cnt From SC_BatchRule")
            strSQL.AppendLine("Where DeptID = @DeptID")
            strSQL.AppendLine("And OrganID = @OrganID")
            strSQL.AppendLine("And WorkTypeID = @WorkTypeID")
            strSQL.AppendLine("And UpdateSeq = @UpdateSeq")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@DeptID", DbType.String, SC_BatchRuleRow.DeptID.Value)
            db.AddInParameter(dbcmd, "@OrganID", DbType.String, SC_BatchRuleRow.OrganID.Value)
            db.AddInParameter(dbcmd, "@WorkTypeID", DbType.String, SC_BatchRuleRow.WorkTypeID.Value)
            db.AddInParameter(dbcmd, "@UpdateSeq", DbType.Int32, SC_BatchRuleRow.UpdateSeq.Value)

            intCount = Convert.ToInt32(db.ExecuteScalar(dbcmd))
            Return IIf(intCount > 0, True, False)
        End Function

        Public Function IsDataExists(ByVal SC_BatchRuleRow As Row, ByVal tran As DbTransaction) As Boolean
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intCount As Integer = 0

            strSQL.AppendLine("Select Count(*) Cnt From SC_BatchRule")
            strSQL.AppendLine("Where DeptID = @DeptID")
            strSQL.AppendLine("And OrganID = @OrganID")
            strSQL.AppendLine("And WorkTypeID = @WorkTypeID")
            strSQL.AppendLine("And UpdateSeq = @UpdateSeq")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@DeptID", DbType.String, SC_BatchRuleRow.DeptID.Value)
            db.AddInParameter(dbcmd, "@OrganID", DbType.String, SC_BatchRuleRow.OrganID.Value)
            db.AddInParameter(dbcmd, "@WorkTypeID", DbType.String, SC_BatchRuleRow.WorkTypeID.Value)
            db.AddInParameter(dbcmd, "@UpdateSeq", DbType.Int32, SC_BatchRuleRow.UpdateSeq.Value)

            intCount = Convert.ToInt32(db.ExecuteScalar(dbcmd, tran))
            Return IIf(intCount > 0, True, False)
        End Function

        Public Function QuerybyWhere(WhereCondition As String) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From SC_BatchRule")
            If WhereCondition <> String.Empty Then strSQL.AppendLine(WhereCondition)

            Return db.ExecuteDataSet(CommandType.Text, strSQL.ToString())
        End Function

        Public Function Insert(ByVal SC_BatchRuleRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Insert into SC_BatchRule")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    DeptID, OrganID, WorkTypeID, UpdateSeq, BanMark, GroupID, BusinessFlag, Description")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @DeptID, @OrganID, @WorkTypeID, @UpdateSeq, @BanMark, @GroupID, @BusinessFlag, @Description")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@DeptID", DbType.String, SC_BatchRuleRow.DeptID.Value)
            db.AddInParameter(dbcmd, "@OrganID", DbType.String, SC_BatchRuleRow.OrganID.Value)
            db.AddInParameter(dbcmd, "@WorkTypeID", DbType.String, SC_BatchRuleRow.WorkTypeID.Value)
            db.AddInParameter(dbcmd, "@UpdateSeq", DbType.Int32, SC_BatchRuleRow.UpdateSeq.Value)
            db.AddInParameter(dbcmd, "@BanMark", DbType.String, SC_BatchRuleRow.BanMark.Value)
            db.AddInParameter(dbcmd, "@GroupID", DbType.String, SC_BatchRuleRow.GroupID.Value)
            db.AddInParameter(dbcmd, "@BusinessFlag", DbType.String, SC_BatchRuleRow.BusinessFlag.Value)
            db.AddInParameter(dbcmd, "@Description", DbType.String, SC_BatchRuleRow.Description.Value)

            Return db.ExecuteNonQuery(dbcmd)
        End Function

        Public Function Insert(ByVal SC_BatchRuleRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Insert into SC_BatchRule")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    DeptID, OrganID, WorkTypeID, UpdateSeq, BanMark, GroupID, BusinessFlag, Description")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @DeptID, @OrganID, @WorkTypeID, @UpdateSeq, @BanMark, @GroupID, @BusinessFlag, @Description")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@DeptID", DbType.String, SC_BatchRuleRow.DeptID.Value)
            db.AddInParameter(dbcmd, "@OrganID", DbType.String, SC_BatchRuleRow.OrganID.Value)
            db.AddInParameter(dbcmd, "@WorkTypeID", DbType.String, SC_BatchRuleRow.WorkTypeID.Value)
            db.AddInParameter(dbcmd, "@UpdateSeq", DbType.Int32, SC_BatchRuleRow.UpdateSeq.Value)
            db.AddInParameter(dbcmd, "@BanMark", DbType.String, SC_BatchRuleRow.BanMark.Value)
            db.AddInParameter(dbcmd, "@GroupID", DbType.String, SC_BatchRuleRow.GroupID.Value)
            db.AddInParameter(dbcmd, "@BusinessFlag", DbType.String, SC_BatchRuleRow.BusinessFlag.Value)
            db.AddInParameter(dbcmd, "@Description", DbType.String, SC_BatchRuleRow.Description.Value)

            Return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function Insert(ByVal SC_BatchRuleRow As Row()) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Insert into SC_BatchRule")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    DeptID, OrganID, WorkTypeID, UpdateSeq, BanMark, GroupID, BusinessFlag, Description")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @DeptID, @OrganID, @WorkTypeID, @UpdateSeq, @BanMark, @GroupID, @BusinessFlag, @Description")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            Using cn As DbConnection = db.CreateConnection()
                cn.Open()
                Dim tran As DbTransaction = cn.BeginTransaction()
                Dim inTrans As Boolean = True

                Try
                    For Each r As Row In SC_BatchRuleRow
                        dbcmd.Parameters.Clear()
                        db.AddInParameter(dbcmd, "@DeptID", DbType.String, r.DeptID.Value)
                        db.AddInParameter(dbcmd, "@OrganID", DbType.String, r.OrganID.Value)
                        db.AddInParameter(dbcmd, "@WorkTypeID", DbType.String, r.WorkTypeID.Value)
                        db.AddInParameter(dbcmd, "@UpdateSeq", DbType.Int32, r.UpdateSeq.Value)
                        db.AddInParameter(dbcmd, "@BanMark", DbType.String, r.BanMark.Value)
                        db.AddInParameter(dbcmd, "@GroupID", DbType.String, r.GroupID.Value)
                        db.AddInParameter(dbcmd, "@BusinessFlag", DbType.String, r.BusinessFlag.Value)
                        db.AddInParameter(dbcmd, "@Description", DbType.String, r.Description.Value)

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

        Public Function Insert(ByVal SC_BatchRuleRow As Row(), ByVal tran As DbTransaction) As Boolean
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Insert into SC_BatchRule")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    DeptID, OrganID, WorkTypeID, UpdateSeq, BanMark, GroupID, BusinessFlag, Description")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @DeptID, @OrganID, @WorkTypeID, @UpdateSeq, @BanMark, @GroupID, @BusinessFlag, @Description")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())

            For Each r As Row In SC_BatchRuleRow
                dbcmd.Parameters.Clear()
                db.AddInParameter(dbcmd, "@DeptID", DbType.String, r.DeptID.Value)
                db.AddInParameter(dbcmd, "@OrganID", DbType.String, r.OrganID.Value)
                db.AddInParameter(dbcmd, "@WorkTypeID", DbType.String, r.WorkTypeID.Value)
                db.AddInParameter(dbcmd, "@UpdateSeq", DbType.Int32, r.UpdateSeq.Value)
                db.AddInParameter(dbcmd, "@BanMark", DbType.String, r.BanMark.Value)
                db.AddInParameter(dbcmd, "@GroupID", DbType.String, r.GroupID.Value)
                db.AddInParameter(dbcmd, "@BusinessFlag", DbType.String, r.BusinessFlag.Value)
                db.AddInParameter(dbcmd, "@Description", DbType.String, r.Description.Value)

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

