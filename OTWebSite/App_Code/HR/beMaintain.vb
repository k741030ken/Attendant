'****************************************************************
' Table:Maintain
' Created Date: 2015.05.07
'****************************************************************/
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports Microsoft.Practices.EnterpriseLibrary.Common
Imports Microsoft.Practices.EnterpriseLibrary.Data
Imports System.Data.Common
Imports System.Data

Namespace beMaintain
    Public Class Table
        Private m_Rows As Rows = New Rows()
        Private m_Fields As String() = { "CompID", "FunctionID", "Role", "EmpComp", "EmpID", "Telephone", "Fax", "LastChgComp", "LastChgID", "LastChgDate" }
        Private m_Types As System.Type() = { GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(Date) }
        Private m_PrimaryFields As String() = { "CompID", "FunctionID", "Role", "EmpComp", "EmpID" }

        Public ReadOnly Property Rows() As beMaintain.Rows 
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
        Public Sub Transfer2Row(MaintainTable As DataTable)
            For Each dr As DataRow In MaintainTable.Rows
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
                dr(m_Rows(i).FunctionID.FieldName) = m_Rows(i).FunctionID.Value
                dr(m_Rows(i).Role.FieldName) = m_Rows(i).Role.Value
                dr(m_Rows(i).EmpComp.FieldName) = m_Rows(i).EmpComp.Value
                dr(m_Rows(i).EmpID.FieldName) = m_Rows(i).EmpID.Value
                dr(m_Rows(i).Telephone.FieldName) = m_Rows(i).Telephone.Value
                dr(m_Rows(i).Fax.FieldName) = m_Rows(i).Fax.Value
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

        Public Sub Add(MaintainRow As Row)
            m_Rows.Add(MaintainRow)
        End Sub

        Public Sub Remove(MaintainRow As Row)
            If m_Rows.IndexOf(MaintainRow) >= 0 Then
                m_Rows.Remove(MaintainRow)
            End If
        End Sub

        Public Sub Dispose()
            m_Rows.Clear()
        End Sub

    End Class

    Public Class Row

        Private FI_CompID As Field(Of String) = new Field(Of String)("CompID", true)
        Private FI_FunctionID As Field(Of String) = new Field(Of String)("FunctionID", true)
        Private FI_Role As Field(Of String) = new Field(Of String)("Role", true)
        Private FI_EmpComp As Field(Of String) = new Field(Of String)("EmpComp", true)
        Private FI_EmpID As Field(Of String) = new Field(Of String)("EmpID", true)
        Private FI_Telephone As Field(Of String) = new Field(Of String)("Telephone", true)
        Private FI_Fax As Field(Of String) = new Field(Of String)("Fax", true)
        Private FI_LastChgComp As Field(Of String) = new Field(Of String)("LastChgComp", true)
        Private FI_LastChgID As Field(Of String) = new Field(Of String)("LastChgID", true)
        Private FI_LastChgDate As Field(Of Date) = new Field(Of Date)("LastChgDate", true)
        Private m_FieldNames As String() = { "CompID", "FunctionID", "Role", "EmpComp", "EmpID", "Telephone", "Fax", "LastChgComp", "LastChgID", "LastChgDate" }
        Private m_PrimaryFields As String() = { "CompID", "FunctionID", "Role", "EmpComp", "EmpID" }
        Private m_IdentityFields As String() = {  }
        Private m_LoadFromDataRow As Boolean = False

        Private Function GetFieldValue(ByVal fieldName As String) As Object
            Select Case fieldName
                Case "CompID"
                    Return FI_CompID.Value
                Case "FunctionID"
                    Return FI_FunctionID.Value
                Case "Role"
                    Return FI_Role.Value
                Case "EmpComp"
                    Return FI_EmpComp.Value
                Case "EmpID"
                    Return FI_EmpID.Value
                Case "Telephone"
                    Return FI_Telephone.Value
                Case "Fax"
                    Return FI_Fax.Value
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
                Case "FunctionID"
                    FI_FunctionID.SetValue(value)
                Case "Role"
                    FI_Role.SetValue(value)
                Case "EmpComp"
                    FI_EmpComp.SetValue(value)
                Case "EmpID"
                    FI_EmpID.SetValue(value)
                Case "Telephone"
                    FI_Telephone.SetValue(value)
                Case "Fax"
                    FI_Fax.SetValue(value)
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
                Case "FunctionID"
                    return FI_FunctionID.Updated
                Case "Role"
                    return FI_Role.Updated
                Case "EmpComp"
                    return FI_EmpComp.Updated
                Case "EmpID"
                    return FI_EmpID.Updated
                Case "Telephone"
                    return FI_Telephone.Updated
                Case "Fax"
                    return FI_Fax.Updated
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
                Case "FunctionID"
                    return FI_FunctionID.CreateUpdateSQL
                Case "Role"
                    return FI_Role.CreateUpdateSQL
                Case "EmpComp"
                    return FI_EmpComp.CreateUpdateSQL
                Case "EmpID"
                    return FI_EmpID.CreateUpdateSQL
                Case "Telephone"
                    return FI_Telephone.CreateUpdateSQL
                Case "Fax"
                    return FI_Fax.CreateUpdateSQL
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
            FI_FunctionID.SetInitValue("")
            FI_Role.SetInitValue("")
            FI_EmpComp.SetInitValue("")
            FI_EmpID.SetInitValue("")
            FI_Telephone.SetInitValue("")
            FI_Fax.SetInitValue("")
            FI_LastChgComp.SetInitValue("")
            FI_LastChgID.SetInitValue("")
            FI_LastChgDate.SetInitValue(Convert.ToDateTime("1900/01/01"))
        End Sub

        Public Sub New(ByVal dr As System.Data.DataRow)
            FI_CompID.SetInitValue(dr("CompID"))
            FI_FunctionID.SetInitValue(dr("FunctionID"))
            FI_Role.SetInitValue(dr("Role"))
            FI_EmpComp.SetInitValue(dr("EmpComp"))
            FI_EmpID.SetInitValue(dr("EmpID"))
            FI_Telephone.SetInitValue(dr("Telephone"))
            FI_Fax.SetInitValue(dr("Fax"))
            FI_LastChgComp.SetInitValue(dr("LastChgComp"))
            FI_LastChgID.SetInitValue(dr("LastChgID"))
            FI_LastChgDate.SetInitValue(dr("LastChgDate"))

            m_LoadFromDataRow = True
        End Sub

        Private Sub ClearUpdated()
            FI_CompID.Updated = False
            FI_FunctionID.Updated = False
            FI_Role.Updated = False
            FI_EmpComp.Updated = False
            FI_EmpID.Updated = False
            FI_Telephone.Updated = False
            FI_Fax.Updated = False
            FI_LastChgComp.Updated = False
            FI_LastChgID.Updated = False
            FI_LastChgDate.Updated = False
        End Sub

        Public ReadOnly Property CompID As Field(Of String) 
            Get
                Return FI_CompID
            End Get
        End Property

        Public ReadOnly Property FunctionID As Field(Of String) 
            Get
                Return FI_FunctionID
            End Get
        End Property

        Public ReadOnly Property Role As Field(Of String) 
            Get
                Return FI_Role
            End Get
        End Property

        Public ReadOnly Property EmpComp As Field(Of String) 
            Get
                Return FI_EmpComp
            End Get
        End Property

        Public ReadOnly Property EmpID As Field(Of String) 
            Get
                Return FI_EmpID
            End Get
        End Property

        Public ReadOnly Property Telephone As Field(Of String) 
            Get
                Return FI_Telephone
            End Get
        End Property

        Public ReadOnly Property Fax As Field(Of String) 
            Get
                Return FI_Fax
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
        Public Function DeleteRowByPrimaryKey(ByVal MaintainRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Delete From Maintain")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And FunctionID = @FunctionID")
            strSQL.AppendLine("And Role = @Role")
            strSQL.AppendLine("And EmpComp = @EmpComp")
            strSQL.AppendLine("And EmpID = @EmpID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, MaintainRow.CompID.Value)
            db.AddInParameter(dbcmd, "@FunctionID", DbType.String, MaintainRow.FunctionID.Value)
            db.AddInParameter(dbcmd, "@Role", DbType.String, MaintainRow.Role.Value)
            db.AddInParameter(dbcmd, "@EmpComp", DbType.String, MaintainRow.EmpComp.Value)
            db.AddInParameter(dbcmd, "@EmpID", DbType.String, MaintainRow.EmpID.Value)

            return db.ExecuteNonQuery(dbcmd)
        End Function

        public Function DeleteRowByPrimaryKey(ByVal MaintainRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Delete From Maintain")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And FunctionID = @FunctionID")
            strSQL.AppendLine("And Role = @Role")
            strSQL.AppendLine("And EmpComp = @EmpComp")
            strSQL.AppendLine("And EmpID = @EmpID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, MaintainRow.CompID.Value)
            db.AddInParameter(dbcmd, "@FunctionID", DbType.String, MaintainRow.FunctionID.Value)
            db.AddInParameter(dbcmd, "@Role", DbType.String, MaintainRow.Role.Value)
            db.AddInParameter(dbcmd, "@EmpComp", DbType.String, MaintainRow.EmpComp.Value)
            db.AddInParameter(dbcmd, "@EmpID", DbType.String, MaintainRow.EmpID.Value)

            return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function DeleteRowByPrimaryKey(ByVal MaintainRow As Row()) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Delete From Maintain")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And FunctionID = @FunctionID")
            strSQL.AppendLine("And Role = @Role")
            strSQL.AppendLine("And EmpComp = @EmpComp")
            strSQL.AppendLine("And EmpID = @EmpID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            using cn As DbConnection = db.CreateConnection()
                cn.Open()
                Dim tran As DbTransaction = cn.BeginTransaction()
                Dim inTrans As Boolean = True

                Try
                    For Each r As Row In MaintainRow
                        dbcmd.Parameters.Clear()
                        db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                        db.AddInParameter(dbcmd, "@FunctionID", DbType.String, r.FunctionID.Value)
                        db.AddInParameter(dbcmd, "@Role", DbType.String, r.Role.Value)
                        db.AddInParameter(dbcmd, "@EmpComp", DbType.String, r.EmpComp.Value)
                        db.AddInParameter(dbcmd, "@EmpID", DbType.String, r.EmpID.Value)

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

        Public Function DeleteRowByPrimaryKey(ByVal MaintainRow As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Delete From Maintain")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And FunctionID = @FunctionID")
            strSQL.AppendLine("And Role = @Role")
            strSQL.AppendLine("And EmpComp = @EmpComp")
            strSQL.AppendLine("And EmpID = @EmpID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())

            For Each r As Row In MaintainRow
                dbcmd.Parameters.Clear()
                db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                db.AddInParameter(dbcmd, "@FunctionID", DbType.String, r.FunctionID.Value)
                db.AddInParameter(dbcmd, "@Role", DbType.String, r.Role.Value)
                db.AddInParameter(dbcmd, "@EmpComp", DbType.String, r.EmpComp.Value)
                db.AddInParameter(dbcmd, "@EmpID", DbType.String, r.EmpID.Value)

                intRowsAffected += db.ExecuteNonQuery(dbcmd, tran)
            Next
            Return intRowsAffected
        End Function

        Public Function QueryByKey(ByVal MaintainRow As Row) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From Maintain")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And FunctionID = @FunctionID")
            strSQL.AppendLine("And Role = @Role")
            strSQL.AppendLine("And EmpComp = @EmpComp")
            strSQL.AppendLine("And EmpID = @EmpID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, MaintainRow.CompID.Value)
            db.AddInParameter(dbcmd, "@FunctionID", DbType.String, MaintainRow.FunctionID.Value)
            db.AddInParameter(dbcmd, "@Role", DbType.String, MaintainRow.Role.Value)
            db.AddInParameter(dbcmd, "@EmpComp", DbType.String, MaintainRow.EmpComp.Value)
            db.AddInParameter(dbcmd, "@EmpID", DbType.String, MaintainRow.EmpID.Value)

            Return db.ExecuteDataSet(dbcmd)
        End Function

        Public Function QueryByKey(MaintainRow As Row, tran As DbTransaction) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From Maintain")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And FunctionID = @FunctionID")
            strSQL.AppendLine("And Role = @Role")
            strSQL.AppendLine("And EmpComp = @EmpComp")
            strSQL.AppendLine("And EmpID = @EmpID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, MaintainRow.CompID.Value)
            db.AddInParameter(dbcmd, "@FunctionID", DbType.String, MaintainRow.FunctionID.Value)
            db.AddInParameter(dbcmd, "@Role", DbType.String, MaintainRow.Role.Value)
            db.AddInParameter(dbcmd, "@EmpComp", DbType.String, MaintainRow.EmpComp.Value)
            db.AddInParameter(dbcmd, "@EmpID", DbType.String, MaintainRow.EmpID.Value)

            Return db.ExecuteDataSet(dbcmd, tran)
        End Function

        Public Function Update(ByVal MaintainRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim strDot As String = String.Empty

            strSQL.AppendLine("Update Maintain Set")
            For i As Integer = 0 To MaintainRow.FieldNames.Length - 1
                If Not MaintainRow.IsIdentityField(MaintainRow.FieldNames(i)) AndAlso MaintainRow.IsUpdated(MaintainRow.FieldNames(i)) AndAlso MaintainRow.CreateUpdateSQL(MaintainRow.FieldNames(i)) Then
                    strSQL.AppendLine(string.Format("{0}{1} = @{1}", strDot, MaintainRow.FieldNames(i)))
                    strDot = ","
                End If
            Next
            If strDot = String.Empty Then Throw New Exception("未異動資料欄位！")
            strSQL.AppendLine("Where CompID = @PKCompID")
            strSQL.AppendLine("And FunctionID = @PKFunctionID")
            strSQL.AppendLine("And Role = @PKRole")
            strSQL.AppendLine("And EmpComp = @PKEmpComp")
            strSQL.AppendLine("And EmpID = @PKEmpID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            If MaintainRow.CompID.Updated Then db.AddInParameter(dbcmd, "@CompID", DbType.String, MaintainRow.CompID.Value)
            If MaintainRow.FunctionID.Updated Then db.AddInParameter(dbcmd, "@FunctionID", DbType.String, MaintainRow.FunctionID.Value)
            If MaintainRow.Role.Updated Then db.AddInParameter(dbcmd, "@Role", DbType.String, MaintainRow.Role.Value)
            If MaintainRow.EmpComp.Updated Then db.AddInParameter(dbcmd, "@EmpComp", DbType.String, MaintainRow.EmpComp.Value)
            If MaintainRow.EmpID.Updated Then db.AddInParameter(dbcmd, "@EmpID", DbType.String, MaintainRow.EmpID.Value)
            If MaintainRow.Telephone.Updated Then db.AddInParameter(dbcmd, "@Telephone", DbType.String, MaintainRow.Telephone.Value)
            If MaintainRow.Fax.Updated Then db.AddInParameter(dbcmd, "@Fax", DbType.String, MaintainRow.Fax.Value)
            If MaintainRow.LastChgComp.Updated Then db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, MaintainRow.LastChgComp.Value)
            If MaintainRow.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, MaintainRow.LastChgID.Value)
            If MaintainRow.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(MaintainRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), MaintainRow.LastChgDate.Value))
            db.AddInParameter(dbcmd, "@PKCompID", DbType.String, IIf(MaintainRow.LoadFromDataRow, MaintainRow.CompID.OldValue, MaintainRow.CompID.Value))
            db.AddInParameter(dbcmd, "@PKFunctionID", DbType.String, IIf(MaintainRow.LoadFromDataRow, MaintainRow.FunctionID.OldValue, MaintainRow.FunctionID.Value))
            db.AddInParameter(dbcmd, "@PKRole", DbType.String, IIf(MaintainRow.LoadFromDataRow, MaintainRow.Role.OldValue, MaintainRow.Role.Value))
            db.AddInParameter(dbcmd, "@PKEmpComp", DbType.String, IIf(MaintainRow.LoadFromDataRow, MaintainRow.EmpComp.OldValue, MaintainRow.EmpComp.Value))
            db.AddInParameter(dbcmd, "@PKEmpID", DbType.String, IIf(MaintainRow.LoadFromDataRow, MaintainRow.EmpID.OldValue, MaintainRow.EmpID.Value))

            Return db.ExecuteNonQuery(dbcmd)
        End Function

        Public Function Update(ByVal MaintainRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim strDot As String = String.Empty

            strSQL.AppendLine("Update Maintain Set")
            For i As Integer = 0 To MaintainRow.FieldNames.Length - 1
                If Not MaintainRow.IsIdentityField(MaintainRow.FieldNames(i)) AndAlso MaintainRow.IsUpdated(MaintainRow.FieldNames(i)) AndAlso MaintainRow.CreateUpdateSQL(MaintainRow.FieldNames(i)) Then
                    strSQL.AppendLine(string.Format("{0}{1} = @{1}", strDot, MaintainRow.FieldNames(i)))
                    strDot = ","
                End If
            Next 
            If strDot = String.Empty Then Throw New Exception("未異動資料欄位！")
            strSQL.AppendLine("Where CompID = @PKCompID")
            strSQL.AppendLine("And FunctionID = @PKFunctionID")
            strSQL.AppendLine("And Role = @PKRole")
            strSQL.AppendLine("And EmpComp = @PKEmpComp")
            strSQL.AppendLine("And EmpID = @PKEmpID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            If MaintainRow.CompID.Updated Then db.AddInParameter(dbcmd, "@CompID", DbType.String, MaintainRow.CompID.Value)
            If MaintainRow.FunctionID.Updated Then db.AddInParameter(dbcmd, "@FunctionID", DbType.String, MaintainRow.FunctionID.Value)
            If MaintainRow.Role.Updated Then db.AddInParameter(dbcmd, "@Role", DbType.String, MaintainRow.Role.Value)
            If MaintainRow.EmpComp.Updated Then db.AddInParameter(dbcmd, "@EmpComp", DbType.String, MaintainRow.EmpComp.Value)
            If MaintainRow.EmpID.Updated Then db.AddInParameter(dbcmd, "@EmpID", DbType.String, MaintainRow.EmpID.Value)
            If MaintainRow.Telephone.Updated Then db.AddInParameter(dbcmd, "@Telephone", DbType.String, MaintainRow.Telephone.Value)
            If MaintainRow.Fax.Updated Then db.AddInParameter(dbcmd, "@Fax", DbType.String, MaintainRow.Fax.Value)
            If MaintainRow.LastChgComp.Updated Then db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, MaintainRow.LastChgComp.Value)
            If MaintainRow.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, MaintainRow.LastChgID.Value)
            If MaintainRow.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(MaintainRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), MaintainRow.LastChgDate.Value))
            db.AddInParameter(dbcmd, "@PKCompID", DbType.String, IIf(MaintainRow.LoadFromDataRow, MaintainRow.CompID.OldValue, MaintainRow.CompID.Value))
            db.AddInParameter(dbcmd, "@PKFunctionID", DbType.String, IIf(MaintainRow.LoadFromDataRow, MaintainRow.FunctionID.OldValue, MaintainRow.FunctionID.Value))
            db.AddInParameter(dbcmd, "@PKRole", DbType.String, IIf(MaintainRow.LoadFromDataRow, MaintainRow.Role.OldValue, MaintainRow.Role.Value))
            db.AddInParameter(dbcmd, "@PKEmpComp", DbType.String, IIf(MaintainRow.LoadFromDataRow, MaintainRow.EmpComp.OldValue, MaintainRow.EmpComp.Value))
            db.AddInParameter(dbcmd, "@PKEmpID", DbType.String, IIf(MaintainRow.LoadFromDataRow, MaintainRow.EmpID.OldValue, MaintainRow.EmpID.Value))

            Return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function Update(ByVal MaintainRow As Row()) As Integer
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
                    For Each r As Row In MaintainRow
                        strDot = String.Empty
                        If strSQL.Length > 0 Then strSQL.Remove(0, strSQL.Length)
                        strSQL.AppendLine("Update Maintain Set")
                        For i As Integer = 0 To r.FieldNames.Length - 1
                            If Not r.IsIdentityField(r.FieldNames(i)) AndAlso r.IsUpdated(r.FieldNames(i)) AndAlso r.CreateUpdateSQL(r.FieldNames(i)) Then
                                strSQL.AppendLine(String.Format("{0}{1} = @{1}", strDot, r.FieldNames(i)))
                                strDot = ","
                            End If
                        Next
                        If strDot = String.Empty Then Continue For
                        strSQL.AppendLine("Where CompID = @PKCompID")
                        strSQL.AppendLine("And FunctionID = @PKFunctionID")
                        strSQL.AppendLine("And Role = @PKRole")
                        strSQL.AppendLine("And EmpComp = @PKEmpComp")
                        strSQL.AppendLine("And EmpID = @PKEmpID")

                        dbcmd = db.GetSqlStringCommand(strSQL.ToString())
                        If r.CompID.Updated Then db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                        If r.FunctionID.Updated Then db.AddInParameter(dbcmd, "@FunctionID", DbType.String, r.FunctionID.Value)
                        If r.Role.Updated Then db.AddInParameter(dbcmd, "@Role", DbType.String, r.Role.Value)
                        If r.EmpComp.Updated Then db.AddInParameter(dbcmd, "@EmpComp", DbType.String, r.EmpComp.Value)
                        If r.EmpID.Updated Then db.AddInParameter(dbcmd, "@EmpID", DbType.String, r.EmpID.Value)
                        If r.Telephone.Updated Then db.AddInParameter(dbcmd, "@Telephone", DbType.String, r.Telephone.Value)
                        If r.Fax.Updated Then db.AddInParameter(dbcmd, "@Fax", DbType.String, r.Fax.Value)
                        If r.LastChgComp.Updated Then db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, r.LastChgComp.Value)
                        If r.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, r.LastChgID.Value)
                        If r.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(r.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), r.LastChgDate.Value))
                        db.AddInParameter(dbcmd, "@PKCompID", DbType.String, IIf(r.LoadFromDataRow, r.CompID.OldValue, r.CompID.Value))
                        db.AddInParameter(dbcmd, "@PKFunctionID", DbType.String, IIf(r.LoadFromDataRow, r.FunctionID.OldValue, r.FunctionID.Value))
                        db.AddInParameter(dbcmd, "@PKRole", DbType.String, IIf(r.LoadFromDataRow, r.Role.OldValue, r.Role.Value))
                        db.AddInParameter(dbcmd, "@PKEmpComp", DbType.String, IIf(r.LoadFromDataRow, r.EmpComp.OldValue, r.EmpComp.Value))
                        db.AddInParameter(dbcmd, "@PKEmpID", DbType.String, IIf(r.LoadFromDataRow, r.EmpID.OldValue, r.EmpID.Value))

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

        Public Function Update(ByVal MaintainRow As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0
            Dim strDot As String

            For Each r As Row In MaintainRow
                strDot = String.Empty
                If strSQL.Length > 0 Then strSQL.Remove(0, strSQL.Length)
                strSQL.AppendLine("Update Maintain Set")
                For i As Integer = 0 To r.FieldNames.Length - 1
                    If Not r.IsIdentityField(r.FieldNames(i)) AndAlso r.IsUpdated(r.FieldNames(i)) AndAlso r.CreateUpdateSQL(r.FieldNames(i))
                        strSQL.AppendLine(String.Format("{0}{1} = @{1}", strDot, r.FieldNames(i)))
                        strDot = ","
                    End If
                Next
                If strDot = String.Empty Then Continue For
                strSQL.AppendLine("Where CompID = @PKCompID")
                strSQL.AppendLine("And FunctionID = @PKFunctionID")
                strSQL.AppendLine("And Role = @PKRole")
                strSQL.AppendLine("And EmpComp = @PKEmpComp")
                strSQL.AppendLine("And EmpID = @PKEmpID")

                dbcmd = db.GetSqlStringCommand(strSQL.ToString())
                If r.CompID.Updated Then db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                If r.FunctionID.Updated Then db.AddInParameter(dbcmd, "@FunctionID", DbType.String, r.FunctionID.Value)
                If r.Role.Updated Then db.AddInParameter(dbcmd, "@Role", DbType.String, r.Role.Value)
                If r.EmpComp.Updated Then db.AddInParameter(dbcmd, "@EmpComp", DbType.String, r.EmpComp.Value)
                If r.EmpID.Updated Then db.AddInParameter(dbcmd, "@EmpID", DbType.String, r.EmpID.Value)
                If r.Telephone.Updated Then db.AddInParameter(dbcmd, "@Telephone", DbType.String, r.Telephone.Value)
                If r.Fax.Updated Then db.AddInParameter(dbcmd, "@Fax", DbType.String, r.Fax.Value)
                If r.LastChgComp.Updated Then db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, r.LastChgComp.Value)
                If r.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, r.LastChgID.Value)
                If r.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(r.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), r.LastChgDate.Value))
                db.AddInParameter(dbcmd, "@PKCompID", DbType.String, IIf(r.LoadFromDataRow, r.CompID.OldValue, r.CompID.Value))
                db.AddInParameter(dbcmd, "@PKFunctionID", DbType.String, IIf(r.LoadFromDataRow, r.FunctionID.OldValue, r.FunctionID.Value))
                db.AddInParameter(dbcmd, "@PKRole", DbType.String, IIf(r.LoadFromDataRow, r.Role.OldValue, r.Role.Value))
                db.AddInParameter(dbcmd, "@PKEmpComp", DbType.String, IIf(r.LoadFromDataRow, r.EmpComp.OldValue, r.EmpComp.Value))
                db.AddInParameter(dbcmd, "@PKEmpID", DbType.String, IIf(r.LoadFromDataRow, r.EmpID.OldValue, r.EmpID.Value))

                intRowsAffected += db.ExecuteNonQuery(dbcmd, tran)
            Next
            Return intRowsAffected
        End Function

        Public Function IsDataExists(ByVal MaintainRow As Row) As Boolean
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intCount As Integer = 0

            strSQL.AppendLine("Select Count(*) Cnt From Maintain")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And FunctionID = @FunctionID")
            strSQL.AppendLine("And Role = @Role")
            strSQL.AppendLine("And EmpComp = @EmpComp")
            strSQL.AppendLine("And EmpID = @EmpID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, MaintainRow.CompID.Value)
            db.AddInParameter(dbcmd, "@FunctionID", DbType.String, MaintainRow.FunctionID.Value)
            db.AddInParameter(dbcmd, "@Role", DbType.String, MaintainRow.Role.Value)
            db.AddInParameter(dbcmd, "@EmpComp", DbType.String, MaintainRow.EmpComp.Value)
            db.AddInParameter(dbcmd, "@EmpID", DbType.String, MaintainRow.EmpID.Value)

            intCount = Convert.ToInt32(db.ExecuteScalar(dbcmd))
            Return IIf(intCount > 0, True, False)
        End Function

        Public Function IsDataExists(ByVal MaintainRow As Row, ByVal tran As DbTransaction) As Boolean
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intCount As Integer = 0

            strSQL.AppendLine("Select Count(*) Cnt From Maintain")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And FunctionID = @FunctionID")
            strSQL.AppendLine("And Role = @Role")
            strSQL.AppendLine("And EmpComp = @EmpComp")
            strSQL.AppendLine("And EmpID = @EmpID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, MaintainRow.CompID.Value)
            db.AddInParameter(dbcmd, "@FunctionID", DbType.String, MaintainRow.FunctionID.Value)
            db.AddInParameter(dbcmd, "@Role", DbType.String, MaintainRow.Role.Value)
            db.AddInParameter(dbcmd, "@EmpComp", DbType.String, MaintainRow.EmpComp.Value)
            db.AddInParameter(dbcmd, "@EmpID", DbType.String, MaintainRow.EmpID.Value)

            intCount = Convert.ToInt32(db.ExecuteScalar(dbcmd, tran))
            Return IIf(intCount > 0, True, False)
        End Function

        Public Function QuerybyWhere(WhereCondition As String) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From Maintain")
            If WhereCondition <> String.Empty Then strSQL.AppendLine(WhereCondition)

            Return db.ExecuteDataSet(CommandType.Text, strSQL.ToString())
        End Function

        Public Function Insert(ByVal MaintainRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Insert into Maintain")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    CompID, FunctionID, Role, EmpComp, EmpID, Telephone, Fax, LastChgComp, LastChgID,")
            strSQL.AppendLine("    LastChgDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @CompID, @FunctionID, @Role, @EmpComp, @EmpID, @Telephone, @Fax, @LastChgComp, @LastChgID,")
            strSQL.AppendLine("    @LastChgDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, MaintainRow.CompID.Value)
            db.AddInParameter(dbcmd, "@FunctionID", DbType.String, MaintainRow.FunctionID.Value)
            db.AddInParameter(dbcmd, "@Role", DbType.String, MaintainRow.Role.Value)
            db.AddInParameter(dbcmd, "@EmpComp", DbType.String, MaintainRow.EmpComp.Value)
            db.AddInParameter(dbcmd, "@EmpID", DbType.String, MaintainRow.EmpID.Value)
            db.AddInParameter(dbcmd, "@Telephone", DbType.String, MaintainRow.Telephone.Value)
            db.AddInParameter(dbcmd, "@Fax", DbType.String, MaintainRow.Fax.Value)
            db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, MaintainRow.LastChgComp.Value)
            db.AddInParameter(dbcmd, "@LastChgID", DbType.String, MaintainRow.LastChgID.Value)
            db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(MaintainRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), MaintainRow.LastChgDate.Value))

            Return db.ExecuteNonQuery(dbcmd)
        End Function

        Public Function Insert(ByVal MaintainRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Insert into Maintain")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    CompID, FunctionID, Role, EmpComp, EmpID, Telephone, Fax, LastChgComp, LastChgID,")
            strSQL.AppendLine("    LastChgDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @CompID, @FunctionID, @Role, @EmpComp, @EmpID, @Telephone, @Fax, @LastChgComp, @LastChgID,")
            strSQL.AppendLine("    @LastChgDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, MaintainRow.CompID.Value)
            db.AddInParameter(dbcmd, "@FunctionID", DbType.String, MaintainRow.FunctionID.Value)
            db.AddInParameter(dbcmd, "@Role", DbType.String, MaintainRow.Role.Value)
            db.AddInParameter(dbcmd, "@EmpComp", DbType.String, MaintainRow.EmpComp.Value)
            db.AddInParameter(dbcmd, "@EmpID", DbType.String, MaintainRow.EmpID.Value)
            db.AddInParameter(dbcmd, "@Telephone", DbType.String, MaintainRow.Telephone.Value)
            db.AddInParameter(dbcmd, "@Fax", DbType.String, MaintainRow.Fax.Value)
            db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, MaintainRow.LastChgComp.Value)
            db.AddInParameter(dbcmd, "@LastChgID", DbType.String, MaintainRow.LastChgID.Value)
            db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(MaintainRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), MaintainRow.LastChgDate.Value))

            Return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function Insert(ByVal MaintainRow As Row()) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Insert into Maintain")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    CompID, FunctionID, Role, EmpComp, EmpID, Telephone, Fax, LastChgComp, LastChgID,")
            strSQL.AppendLine("    LastChgDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @CompID, @FunctionID, @Role, @EmpComp, @EmpID, @Telephone, @Fax, @LastChgComp, @LastChgID,")
            strSQL.AppendLine("    @LastChgDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            Using cn As DbConnection = db.CreateConnection()
                cn.Open()
                Dim tran As DbTransaction = cn.BeginTransaction()
                Dim inTrans As Boolean = True

                Try
                    For Each r As Row In MaintainRow
                        dbcmd.Parameters.Clear()
                        db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                        db.AddInParameter(dbcmd, "@FunctionID", DbType.String, r.FunctionID.Value)
                        db.AddInParameter(dbcmd, "@Role", DbType.String, r.Role.Value)
                        db.AddInParameter(dbcmd, "@EmpComp", DbType.String, r.EmpComp.Value)
                        db.AddInParameter(dbcmd, "@EmpID", DbType.String, r.EmpID.Value)
                        db.AddInParameter(dbcmd, "@Telephone", DbType.String, r.Telephone.Value)
                        db.AddInParameter(dbcmd, "@Fax", DbType.String, r.Fax.Value)
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

        Public Function Insert(ByVal MaintainRow As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Insert into Maintain")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    CompID, FunctionID, Role, EmpComp, EmpID, Telephone, Fax, LastChgComp, LastChgID,")
            strSQL.AppendLine("    LastChgDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @CompID, @FunctionID, @Role, @EmpComp, @EmpID, @Telephone, @Fax, @LastChgComp, @LastChgID,")
            strSQL.AppendLine("    @LastChgDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())

            For Each r As Row In MaintainRow
                dbcmd.Parameters.Clear()
                db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                db.AddInParameter(dbcmd, "@FunctionID", DbType.String, r.FunctionID.Value)
                db.AddInParameter(dbcmd, "@Role", DbType.String, r.Role.Value)
                db.AddInParameter(dbcmd, "@EmpComp", DbType.String, r.EmpComp.Value)
                db.AddInParameter(dbcmd, "@EmpID", DbType.String, r.EmpID.Value)
                db.AddInParameter(dbcmd, "@Telephone", DbType.String, r.Telephone.Value)
                db.AddInParameter(dbcmd, "@Fax", DbType.String, r.Fax.Value)
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

