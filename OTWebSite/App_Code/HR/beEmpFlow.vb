'****************************************************************
' Table:EmpFlow
' Created Date: 2016.11.04
'****************************************************************/
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports Microsoft.Practices.EnterpriseLibrary.Common
Imports Microsoft.Practices.EnterpriseLibrary.Data
Imports System.Data.Common
Imports System.Data

Namespace beEmpFlow
    Public Class Table
        Private m_Rows As Rows = New Rows()
        Private m_Fields As String() = { "CompID", "EmpID", "ActionID", "OrganID", "GroupType", "GroupID", "BusinessType", "EmpFlowRemarkID", "LastChgComp", "LastChgID" _
                                    , "LastChgDate" }
        Private m_Types As System.Type() = { GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String) _
                                    , GetType(Date) }
        Private m_PrimaryFields As String() = { "EmpID", "CompID", "ActionID" }

        Public ReadOnly Property Rows() As beEmpFlow.Rows 
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
        Public Sub Transfer2Row(EmpFlowTable As DataTable)
            For Each dr As DataRow In EmpFlowTable.Rows
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
                dr(m_Rows(i).ActionID.FieldName) = m_Rows(i).ActionID.Value
                dr(m_Rows(i).OrganID.FieldName) = m_Rows(i).OrganID.Value
                dr(m_Rows(i).GroupType.FieldName) = m_Rows(i).GroupType.Value
                dr(m_Rows(i).GroupID.FieldName) = m_Rows(i).GroupID.Value
                dr(m_Rows(i).BusinessType.FieldName) = m_Rows(i).BusinessType.Value
                dr(m_Rows(i).EmpFlowRemarkID.FieldName) = m_Rows(i).EmpFlowRemarkID.Value
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

        Public Sub Add(EmpFlowRow As Row)
            m_Rows.Add(EmpFlowRow)
        End Sub

        Public Sub Remove(EmpFlowRow As Row)
            If m_Rows.IndexOf(EmpFlowRow) >= 0 Then
                m_Rows.Remove(EmpFlowRow)
            End If
        End Sub

        Public Sub Dispose()
            m_Rows.Clear()
        End Sub

    End Class

    Public Class Row

        Private FI_CompID As Field(Of String) = new Field(Of String)("CompID", true)
        Private FI_EmpID As Field(Of String) = new Field(Of String)("EmpID", true)
        Private FI_ActionID As Field(Of String) = new Field(Of String)("ActionID", true)
        Private FI_OrganID As Field(Of String) = new Field(Of String)("OrganID", true)
        Private FI_GroupType As Field(Of String) = new Field(Of String)("GroupType", true)
        Private FI_GroupID As Field(Of String) = new Field(Of String)("GroupID", true)
        Private FI_BusinessType As Field(Of String) = new Field(Of String)("BusinessType", true)
        Private FI_EmpFlowRemarkID As Field(Of String) = new Field(Of String)("EmpFlowRemarkID", true)
        Private FI_LastChgComp As Field(Of String) = new Field(Of String)("LastChgComp", true)
        Private FI_LastChgID As Field(Of String) = new Field(Of String)("LastChgID", true)
        Private FI_LastChgDate As Field(Of Date) = new Field(Of Date)("LastChgDate", true)
        Private m_FieldNames As String() = { "CompID", "EmpID", "ActionID", "OrganID", "GroupType", "GroupID", "BusinessType", "EmpFlowRemarkID", "LastChgComp", "LastChgID" _
                                    , "LastChgDate" }
        Private m_PrimaryFields As String() = { "EmpID", "CompID", "ActionID" }
        Private m_IdentityFields As String() = {  }
        Private m_LoadFromDataRow As Boolean = False

        Private Function GetFieldValue(ByVal fieldName As String) As Object
            Select Case fieldName
                Case "CompID"
                    Return FI_CompID.Value
                Case "EmpID"
                    Return FI_EmpID.Value
                Case "ActionID"
                    Return FI_ActionID.Value
                Case "OrganID"
                    Return FI_OrganID.Value
                Case "GroupType"
                    Return FI_GroupType.Value
                Case "GroupID"
                    Return FI_GroupID.Value
                Case "BusinessType"
                    Return FI_BusinessType.Value
                Case "EmpFlowRemarkID"
                    Return FI_EmpFlowRemarkID.Value
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
                Case "ActionID"
                    FI_ActionID.SetValue(value)
                Case "OrganID"
                    FI_OrganID.SetValue(value)
                Case "GroupType"
                    FI_GroupType.SetValue(value)
                Case "GroupID"
                    FI_GroupID.SetValue(value)
                Case "BusinessType"
                    FI_BusinessType.SetValue(value)
                Case "EmpFlowRemarkID"
                    FI_EmpFlowRemarkID.SetValue(value)
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
                Case "ActionID"
                    return FI_ActionID.Updated
                Case "OrganID"
                    return FI_OrganID.Updated
                Case "GroupType"
                    return FI_GroupType.Updated
                Case "GroupID"
                    return FI_GroupID.Updated
                Case "BusinessType"
                    return FI_BusinessType.Updated
                Case "EmpFlowRemarkID"
                    return FI_EmpFlowRemarkID.Updated
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
                Case "ActionID"
                    return FI_ActionID.CreateUpdateSQL
                Case "OrganID"
                    return FI_OrganID.CreateUpdateSQL
                Case "GroupType"
                    return FI_GroupType.CreateUpdateSQL
                Case "GroupID"
                    return FI_GroupID.CreateUpdateSQL
                Case "BusinessType"
                    return FI_BusinessType.CreateUpdateSQL
                Case "EmpFlowRemarkID"
                    return FI_EmpFlowRemarkID.CreateUpdateSQL
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
            FI_CompID.SetInitValue("SPHBK1")
            FI_EmpID.SetInitValue("")
            FI_ActionID.SetInitValue("")
            FI_OrganID.SetInitValue("")
            FI_GroupType.SetInitValue("")
            FI_GroupID.SetInitValue("")
            FI_BusinessType.SetInitValue("")
            FI_EmpFlowRemarkID.SetInitValue("")
            FI_LastChgComp.SetInitValue("")
            FI_LastChgID.SetInitValue("")
            FI_LastChgDate.SetInitValue(Convert.ToDateTime("1900/01/01"))
        End Sub

        Public Sub New(ByVal dr As System.Data.DataRow)
            FI_CompID.SetInitValue(dr("CompID"))
            FI_EmpID.SetInitValue(dr("EmpID"))
            FI_ActionID.SetInitValue(dr("ActionID"))
            FI_OrganID.SetInitValue(dr("OrganID"))
            FI_GroupType.SetInitValue(dr("GroupType"))
            FI_GroupID.SetInitValue(dr("GroupID"))
            FI_BusinessType.SetInitValue(dr("BusinessType"))
            FI_EmpFlowRemarkID.SetInitValue(dr("EmpFlowRemarkID"))
            FI_LastChgComp.SetInitValue(dr("LastChgComp"))
            FI_LastChgID.SetInitValue(dr("LastChgID"))
            FI_LastChgDate.SetInitValue(dr("LastChgDate"))

            m_LoadFromDataRow = True
        End Sub

        Private Sub ClearUpdated()
            FI_CompID.Updated = False
            FI_EmpID.Updated = False
            FI_ActionID.Updated = False
            FI_OrganID.Updated = False
            FI_GroupType.Updated = False
            FI_GroupID.Updated = False
            FI_BusinessType.Updated = False
            FI_EmpFlowRemarkID.Updated = False
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

        Public ReadOnly Property ActionID As Field(Of String) 
            Get
                Return FI_ActionID
            End Get
        End Property

        Public ReadOnly Property OrganID As Field(Of String) 
            Get
                Return FI_OrganID
            End Get
        End Property

        Public ReadOnly Property GroupType As Field(Of String) 
            Get
                Return FI_GroupType
            End Get
        End Property

        Public ReadOnly Property GroupID As Field(Of String) 
            Get
                Return FI_GroupID
            End Get
        End Property

        Public ReadOnly Property BusinessType As Field(Of String) 
            Get
                Return FI_BusinessType
            End Get
        End Property

        Public ReadOnly Property EmpFlowRemarkID As Field(Of String) 
            Get
                Return FI_EmpFlowRemarkID
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
        Public Function DeleteRowByPrimaryKey(ByVal EmpFlowRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Delete From EmpFlow")
            strSQL.AppendLine("Where EmpID = @EmpID")
            strSQL.AppendLine("And CompID = @CompID")
            strSQL.AppendLine("And ActionID = @ActionID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@EmpID", DbType.String, EmpFlowRow.EmpID.Value)
            db.AddInParameter(dbcmd, "@CompID", DbType.String, EmpFlowRow.CompID.Value)
            db.AddInParameter(dbcmd, "@ActionID", DbType.String, EmpFlowRow.ActionID.Value)

            return db.ExecuteNonQuery(dbcmd)
        End Function

        public Function DeleteRowByPrimaryKey(ByVal EmpFlowRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Delete From EmpFlow")
            strSQL.AppendLine("Where EmpID = @EmpID")
            strSQL.AppendLine("And CompID = @CompID")
            strSQL.AppendLine("And ActionID = @ActionID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@EmpID", DbType.String, EmpFlowRow.EmpID.Value)
            db.AddInParameter(dbcmd, "@CompID", DbType.String, EmpFlowRow.CompID.Value)
            db.AddInParameter(dbcmd, "@ActionID", DbType.String, EmpFlowRow.ActionID.Value)

            return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function DeleteRowByPrimaryKey(ByVal EmpFlowRow As Row()) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Delete From EmpFlow")
            strSQL.AppendLine("Where EmpID = @EmpID")
            strSQL.AppendLine("And CompID = @CompID")
            strSQL.AppendLine("And ActionID = @ActionID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            using cn As DbConnection = db.CreateConnection()
                cn.Open()
                Dim tran As DbTransaction = cn.BeginTransaction()
                Dim inTrans As Boolean = True

                Try
                    For Each r As Row In EmpFlowRow
                        dbcmd.Parameters.Clear()
                        db.AddInParameter(dbcmd, "@EmpID", DbType.String, r.EmpID.Value)
                        db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                        db.AddInParameter(dbcmd, "@ActionID", DbType.String, r.ActionID.Value)

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

        Public Function DeleteRowByPrimaryKey(ByVal EmpFlowRow As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Delete From EmpFlow")
            strSQL.AppendLine("Where EmpID = @EmpID")
            strSQL.AppendLine("And CompID = @CompID")
            strSQL.AppendLine("And ActionID = @ActionID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())

            For Each r As Row In EmpFlowRow
                dbcmd.Parameters.Clear()
                db.AddInParameter(dbcmd, "@EmpID", DbType.String, r.EmpID.Value)
                db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                db.AddInParameter(dbcmd, "@ActionID", DbType.String, r.ActionID.Value)

                intRowsAffected += db.ExecuteNonQuery(dbcmd, tran)
            Next
            Return intRowsAffected
        End Function

        Public Function QueryByKey(ByVal EmpFlowRow As Row) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From EmpFlow")
            strSQL.AppendLine("Where EmpID = @EmpID")
            strSQL.AppendLine("And CompID = @CompID")
            strSQL.AppendLine("And ActionID = @ActionID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@EmpID", DbType.String, EmpFlowRow.EmpID.Value)
            db.AddInParameter(dbcmd, "@CompID", DbType.String, EmpFlowRow.CompID.Value)
            db.AddInParameter(dbcmd, "@ActionID", DbType.String, EmpFlowRow.ActionID.Value)

            Return db.ExecuteDataSet(dbcmd)
        End Function

        Public Function QueryByKey(EmpFlowRow As Row, tran As DbTransaction) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From EmpFlow")
            strSQL.AppendLine("Where EmpID = @EmpID")
            strSQL.AppendLine("And CompID = @CompID")
            strSQL.AppendLine("And ActionID = @ActionID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@EmpID", DbType.String, EmpFlowRow.EmpID.Value)
            db.AddInParameter(dbcmd, "@CompID", DbType.String, EmpFlowRow.CompID.Value)
            db.AddInParameter(dbcmd, "@ActionID", DbType.String, EmpFlowRow.ActionID.Value)

            Return db.ExecuteDataSet(dbcmd, tran)
        End Function

        Public Function Update(ByVal EmpFlowRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim strDot As String = String.Empty

            strSQL.AppendLine("Update EmpFlow Set")
            For i As Integer = 0 To EmpFlowRow.FieldNames.Length - 1
                If Not EmpFlowRow.IsIdentityField(EmpFlowRow.FieldNames(i)) AndAlso EmpFlowRow.IsUpdated(EmpFlowRow.FieldNames(i)) AndAlso EmpFlowRow.CreateUpdateSQL(EmpFlowRow.FieldNames(i)) Then
                    strSQL.AppendLine(string.Format("{0}{1} = @{1}", strDot, EmpFlowRow.FieldNames(i)))
                    strDot = ","
                End If
            Next
            If strDot = String.Empty Then Throw New Exception("未異動資料欄位！")
            strSQL.AppendLine("Where EmpID = @PKEmpID")
            strSQL.AppendLine("And CompID = @PKCompID")
            strSQL.AppendLine("And ActionID = @PKActionID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            If EmpFlowRow.CompID.Updated Then db.AddInParameter(dbcmd, "@CompID", DbType.String, EmpFlowRow.CompID.Value)
            If EmpFlowRow.EmpID.Updated Then db.AddInParameter(dbcmd, "@EmpID", DbType.String, EmpFlowRow.EmpID.Value)
            If EmpFlowRow.ActionID.Updated Then db.AddInParameter(dbcmd, "@ActionID", DbType.String, EmpFlowRow.ActionID.Value)
            If EmpFlowRow.OrganID.Updated Then db.AddInParameter(dbcmd, "@OrganID", DbType.String, EmpFlowRow.OrganID.Value)
            If EmpFlowRow.GroupType.Updated Then db.AddInParameter(dbcmd, "@GroupType", DbType.String, EmpFlowRow.GroupType.Value)
            If EmpFlowRow.GroupID.Updated Then db.AddInParameter(dbcmd, "@GroupID", DbType.String, EmpFlowRow.GroupID.Value)
            If EmpFlowRow.BusinessType.Updated Then db.AddInParameter(dbcmd, "@BusinessType", DbType.String, EmpFlowRow.BusinessType.Value)
            If EmpFlowRow.EmpFlowRemarkID.Updated Then db.AddInParameter(dbcmd, "@EmpFlowRemarkID", DbType.String, EmpFlowRow.EmpFlowRemarkID.Value)
            If EmpFlowRow.LastChgComp.Updated Then db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, EmpFlowRow.LastChgComp.Value)
            If EmpFlowRow.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, EmpFlowRow.LastChgID.Value)
            If EmpFlowRow.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(EmpFlowRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), EmpFlowRow.LastChgDate.Value))
            db.AddInParameter(dbcmd, "@PKEmpID", DbType.String, IIf(EmpFlowRow.LoadFromDataRow, EmpFlowRow.EmpID.OldValue, EmpFlowRow.EmpID.Value))
            db.AddInParameter(dbcmd, "@PKCompID", DbType.String, IIf(EmpFlowRow.LoadFromDataRow, EmpFlowRow.CompID.OldValue, EmpFlowRow.CompID.Value))
            db.AddInParameter(dbcmd, "@PKActionID", DbType.String, IIf(EmpFlowRow.LoadFromDataRow, EmpFlowRow.ActionID.OldValue, EmpFlowRow.ActionID.Value))

            Return db.ExecuteNonQuery(dbcmd)
        End Function

        Public Function Update(ByVal EmpFlowRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim strDot As String = String.Empty

            strSQL.AppendLine("Update EmpFlow Set")
            For i As Integer = 0 To EmpFlowRow.FieldNames.Length - 1
                If Not EmpFlowRow.IsIdentityField(EmpFlowRow.FieldNames(i)) AndAlso EmpFlowRow.IsUpdated(EmpFlowRow.FieldNames(i)) AndAlso EmpFlowRow.CreateUpdateSQL(EmpFlowRow.FieldNames(i)) Then
                    strSQL.AppendLine(string.Format("{0}{1} = @{1}", strDot, EmpFlowRow.FieldNames(i)))
                    strDot = ","
                End If
            Next 
            If strDot = String.Empty Then Throw New Exception("未異動資料欄位！")
            strSQL.AppendLine("Where EmpID = @PKEmpID")
            strSQL.AppendLine("And CompID = @PKCompID")
            strSQL.AppendLine("And ActionID = @PKActionID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            If EmpFlowRow.CompID.Updated Then db.AddInParameter(dbcmd, "@CompID", DbType.String, EmpFlowRow.CompID.Value)
            If EmpFlowRow.EmpID.Updated Then db.AddInParameter(dbcmd, "@EmpID", DbType.String, EmpFlowRow.EmpID.Value)
            If EmpFlowRow.ActionID.Updated Then db.AddInParameter(dbcmd, "@ActionID", DbType.String, EmpFlowRow.ActionID.Value)
            If EmpFlowRow.OrganID.Updated Then db.AddInParameter(dbcmd, "@OrganID", DbType.String, EmpFlowRow.OrganID.Value)
            If EmpFlowRow.GroupType.Updated Then db.AddInParameter(dbcmd, "@GroupType", DbType.String, EmpFlowRow.GroupType.Value)
            If EmpFlowRow.GroupID.Updated Then db.AddInParameter(dbcmd, "@GroupID", DbType.String, EmpFlowRow.GroupID.Value)
            If EmpFlowRow.BusinessType.Updated Then db.AddInParameter(dbcmd, "@BusinessType", DbType.String, EmpFlowRow.BusinessType.Value)
            If EmpFlowRow.EmpFlowRemarkID.Updated Then db.AddInParameter(dbcmd, "@EmpFlowRemarkID", DbType.String, EmpFlowRow.EmpFlowRemarkID.Value)
            If EmpFlowRow.LastChgComp.Updated Then db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, EmpFlowRow.LastChgComp.Value)
            If EmpFlowRow.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, EmpFlowRow.LastChgID.Value)
            If EmpFlowRow.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(EmpFlowRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), EmpFlowRow.LastChgDate.Value))
            db.AddInParameter(dbcmd, "@PKEmpID", DbType.String, IIf(EmpFlowRow.LoadFromDataRow, EmpFlowRow.EmpID.OldValue, EmpFlowRow.EmpID.Value))
            db.AddInParameter(dbcmd, "@PKCompID", DbType.String, IIf(EmpFlowRow.LoadFromDataRow, EmpFlowRow.CompID.OldValue, EmpFlowRow.CompID.Value))
            db.AddInParameter(dbcmd, "@PKActionID", DbType.String, IIf(EmpFlowRow.LoadFromDataRow, EmpFlowRow.ActionID.OldValue, EmpFlowRow.ActionID.Value))

            Return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function Update(ByVal EmpFlowRow As Row()) As Integer
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
                    For Each r As Row In EmpFlowRow
                        strDot = String.Empty
                        If strSQL.Length > 0 Then strSQL.Remove(0, strSQL.Length)
                        strSQL.AppendLine("Update EmpFlow Set")
                        For i As Integer = 0 To r.FieldNames.Length - 1
                            If Not r.IsIdentityField(r.FieldNames(i)) AndAlso r.IsUpdated(r.FieldNames(i)) AndAlso r.CreateUpdateSQL(r.FieldNames(i)) Then
                                strSQL.AppendLine(String.Format("{0}{1} = @{1}", strDot, r.FieldNames(i)))
                                strDot = ","
                            End If
                        Next
                        If strDot = String.Empty Then Continue For
                        strSQL.AppendLine("Where EmpID = @PKEmpID")
                        strSQL.AppendLine("And CompID = @PKCompID")
                        strSQL.AppendLine("And ActionID = @PKActionID")

                        dbcmd = db.GetSqlStringCommand(strSQL.ToString())
                        If r.CompID.Updated Then db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                        If r.EmpID.Updated Then db.AddInParameter(dbcmd, "@EmpID", DbType.String, r.EmpID.Value)
                        If r.ActionID.Updated Then db.AddInParameter(dbcmd, "@ActionID", DbType.String, r.ActionID.Value)
                        If r.OrganID.Updated Then db.AddInParameter(dbcmd, "@OrganID", DbType.String, r.OrganID.Value)
                        If r.GroupType.Updated Then db.AddInParameter(dbcmd, "@GroupType", DbType.String, r.GroupType.Value)
                        If r.GroupID.Updated Then db.AddInParameter(dbcmd, "@GroupID", DbType.String, r.GroupID.Value)
                        If r.BusinessType.Updated Then db.AddInParameter(dbcmd, "@BusinessType", DbType.String, r.BusinessType.Value)
                        If r.EmpFlowRemarkID.Updated Then db.AddInParameter(dbcmd, "@EmpFlowRemarkID", DbType.String, r.EmpFlowRemarkID.Value)
                        If r.LastChgComp.Updated Then db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, r.LastChgComp.Value)
                        If r.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, r.LastChgID.Value)
                        If r.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(r.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), r.LastChgDate.Value))
                        db.AddInParameter(dbcmd, "@PKEmpID", DbType.String, IIf(r.LoadFromDataRow, r.EmpID.OldValue, r.EmpID.Value))
                        db.AddInParameter(dbcmd, "@PKCompID", DbType.String, IIf(r.LoadFromDataRow, r.CompID.OldValue, r.CompID.Value))
                        db.AddInParameter(dbcmd, "@PKActionID", DbType.String, IIf(r.LoadFromDataRow, r.ActionID.OldValue, r.ActionID.Value))

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

        Public Function Update(ByVal EmpFlowRow As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0
            Dim strDot As String

            For Each r As Row In EmpFlowRow
                strDot = String.Empty
                If strSQL.Length > 0 Then strSQL.Remove(0, strSQL.Length)
                strSQL.AppendLine("Update EmpFlow Set")
                For i As Integer = 0 To r.FieldNames.Length - 1
                    If Not r.IsIdentityField(r.FieldNames(i)) AndAlso r.IsUpdated(r.FieldNames(i)) AndAlso r.CreateUpdateSQL(r.FieldNames(i))
                        strSQL.AppendLine(String.Format("{0}{1} = @{1}", strDot, r.FieldNames(i)))
                        strDot = ","
                    End If
                Next
                If strDot = String.Empty Then Continue For
                strSQL.AppendLine("Where EmpID = @PKEmpID")
                strSQL.AppendLine("And CompID = @PKCompID")
                strSQL.AppendLine("And ActionID = @PKActionID")

                dbcmd = db.GetSqlStringCommand(strSQL.ToString())
                If r.CompID.Updated Then db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                If r.EmpID.Updated Then db.AddInParameter(dbcmd, "@EmpID", DbType.String, r.EmpID.Value)
                If r.ActionID.Updated Then db.AddInParameter(dbcmd, "@ActionID", DbType.String, r.ActionID.Value)
                If r.OrganID.Updated Then db.AddInParameter(dbcmd, "@OrganID", DbType.String, r.OrganID.Value)
                If r.GroupType.Updated Then db.AddInParameter(dbcmd, "@GroupType", DbType.String, r.GroupType.Value)
                If r.GroupID.Updated Then db.AddInParameter(dbcmd, "@GroupID", DbType.String, r.GroupID.Value)
                If r.BusinessType.Updated Then db.AddInParameter(dbcmd, "@BusinessType", DbType.String, r.BusinessType.Value)
                If r.EmpFlowRemarkID.Updated Then db.AddInParameter(dbcmd, "@EmpFlowRemarkID", DbType.String, r.EmpFlowRemarkID.Value)
                If r.LastChgComp.Updated Then db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, r.LastChgComp.Value)
                If r.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, r.LastChgID.Value)
                If r.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(r.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), r.LastChgDate.Value))
                db.AddInParameter(dbcmd, "@PKEmpID", DbType.String, IIf(r.LoadFromDataRow, r.EmpID.OldValue, r.EmpID.Value))
                db.AddInParameter(dbcmd, "@PKCompID", DbType.String, IIf(r.LoadFromDataRow, r.CompID.OldValue, r.CompID.Value))
                db.AddInParameter(dbcmd, "@PKActionID", DbType.String, IIf(r.LoadFromDataRow, r.ActionID.OldValue, r.ActionID.Value))

                intRowsAffected += db.ExecuteNonQuery(dbcmd, tran)
            Next
            Return intRowsAffected
        End Function

        Public Function IsDataExists(ByVal EmpFlowRow As Row) As Boolean
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intCount As Integer = 0

            strSQL.AppendLine("Select Count(*) Cnt From EmpFlow")
            strSQL.AppendLine("Where EmpID = @EmpID")
            strSQL.AppendLine("And CompID = @CompID")
            strSQL.AppendLine("And ActionID = @ActionID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@EmpID", DbType.String, EmpFlowRow.EmpID.Value)
            db.AddInParameter(dbcmd, "@CompID", DbType.String, EmpFlowRow.CompID.Value)
            db.AddInParameter(dbcmd, "@ActionID", DbType.String, EmpFlowRow.ActionID.Value)

            intCount = Convert.ToInt32(db.ExecuteScalar(dbcmd))
            Return IIf(intCount > 0, True, False)
        End Function

        Public Function IsDataExists(ByVal EmpFlowRow As Row, ByVal tran As DbTransaction) As Boolean
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intCount As Integer = 0

            strSQL.AppendLine("Select Count(*) Cnt From EmpFlow")
            strSQL.AppendLine("Where EmpID = @EmpID")
            strSQL.AppendLine("And CompID = @CompID")
            strSQL.AppendLine("And ActionID = @ActionID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@EmpID", DbType.String, EmpFlowRow.EmpID.Value)
            db.AddInParameter(dbcmd, "@CompID", DbType.String, EmpFlowRow.CompID.Value)
            db.AddInParameter(dbcmd, "@ActionID", DbType.String, EmpFlowRow.ActionID.Value)

            intCount = Convert.ToInt32(db.ExecuteScalar(dbcmd, tran))
            Return IIf(intCount > 0, True, False)
        End Function

        Public Function QuerybyWhere(WhereCondition As String) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From EmpFlow")
            If WhereCondition <> String.Empty Then strSQL.AppendLine(WhereCondition)

            Return db.ExecuteDataSet(CommandType.Text, strSQL.ToString())
        End Function

        Public Function Insert(ByVal EmpFlowRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Insert into EmpFlow")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    CompID, EmpID, ActionID, OrganID, GroupType, GroupID, BusinessType, EmpFlowRemarkID,")
            strSQL.AppendLine("    LastChgComp, LastChgID, LastChgDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @CompID, @EmpID, @ActionID, @OrganID, @GroupType, @GroupID, @BusinessType, @EmpFlowRemarkID,")
            strSQL.AppendLine("    @LastChgComp, @LastChgID, @LastChgDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, EmpFlowRow.CompID.Value)
            db.AddInParameter(dbcmd, "@EmpID", DbType.String, EmpFlowRow.EmpID.Value)
            db.AddInParameter(dbcmd, "@ActionID", DbType.String, EmpFlowRow.ActionID.Value)
            db.AddInParameter(dbcmd, "@OrganID", DbType.String, EmpFlowRow.OrganID.Value)
            db.AddInParameter(dbcmd, "@GroupType", DbType.String, EmpFlowRow.GroupType.Value)
            db.AddInParameter(dbcmd, "@GroupID", DbType.String, EmpFlowRow.GroupID.Value)
            db.AddInParameter(dbcmd, "@BusinessType", DbType.String, EmpFlowRow.BusinessType.Value)
            db.AddInParameter(dbcmd, "@EmpFlowRemarkID", DbType.String, EmpFlowRow.EmpFlowRemarkID.Value)
            db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, EmpFlowRow.LastChgComp.Value)
            db.AddInParameter(dbcmd, "@LastChgID", DbType.String, EmpFlowRow.LastChgID.Value)
            db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(EmpFlowRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), EmpFlowRow.LastChgDate.Value))

            Return db.ExecuteNonQuery(dbcmd)
        End Function

        Public Function Insert(ByVal EmpFlowRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Insert into EmpFlow")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    CompID, EmpID, ActionID, OrganID, GroupType, GroupID, BusinessType, EmpFlowRemarkID,")
            strSQL.AppendLine("    LastChgComp, LastChgID, LastChgDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @CompID, @EmpID, @ActionID, @OrganID, @GroupType, @GroupID, @BusinessType, @EmpFlowRemarkID,")
            strSQL.AppendLine("    @LastChgComp, @LastChgID, @LastChgDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, EmpFlowRow.CompID.Value)
            db.AddInParameter(dbcmd, "@EmpID", DbType.String, EmpFlowRow.EmpID.Value)
            db.AddInParameter(dbcmd, "@ActionID", DbType.String, EmpFlowRow.ActionID.Value)
            db.AddInParameter(dbcmd, "@OrganID", DbType.String, EmpFlowRow.OrganID.Value)
            db.AddInParameter(dbcmd, "@GroupType", DbType.String, EmpFlowRow.GroupType.Value)
            db.AddInParameter(dbcmd, "@GroupID", DbType.String, EmpFlowRow.GroupID.Value)
            db.AddInParameter(dbcmd, "@BusinessType", DbType.String, EmpFlowRow.BusinessType.Value)
            db.AddInParameter(dbcmd, "@EmpFlowRemarkID", DbType.String, EmpFlowRow.EmpFlowRemarkID.Value)
            db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, EmpFlowRow.LastChgComp.Value)
            db.AddInParameter(dbcmd, "@LastChgID", DbType.String, EmpFlowRow.LastChgID.Value)
            db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(EmpFlowRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), EmpFlowRow.LastChgDate.Value))

            Return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function Insert(ByVal EmpFlowRow As Row()) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Insert into EmpFlow")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    CompID, EmpID, ActionID, OrganID, GroupType, GroupID, BusinessType, EmpFlowRemarkID,")
            strSQL.AppendLine("    LastChgComp, LastChgID, LastChgDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @CompID, @EmpID, @ActionID, @OrganID, @GroupType, @GroupID, @BusinessType, @EmpFlowRemarkID,")
            strSQL.AppendLine("    @LastChgComp, @LastChgID, @LastChgDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            Using cn As DbConnection = db.CreateConnection()
                cn.Open()
                Dim tran As DbTransaction = cn.BeginTransaction()
                Dim inTrans As Boolean = True

                Try
                    For Each r As Row In EmpFlowRow
                        dbcmd.Parameters.Clear()
                        db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                        db.AddInParameter(dbcmd, "@EmpID", DbType.String, r.EmpID.Value)
                        db.AddInParameter(dbcmd, "@ActionID", DbType.String, r.ActionID.Value)
                        db.AddInParameter(dbcmd, "@OrganID", DbType.String, r.OrganID.Value)
                        db.AddInParameter(dbcmd, "@GroupType", DbType.String, r.GroupType.Value)
                        db.AddInParameter(dbcmd, "@GroupID", DbType.String, r.GroupID.Value)
                        db.AddInParameter(dbcmd, "@BusinessType", DbType.String, r.BusinessType.Value)
                        db.AddInParameter(dbcmd, "@EmpFlowRemarkID", DbType.String, r.EmpFlowRemarkID.Value)
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

        Public Function Insert(ByVal EmpFlowRow As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Insert into EmpFlow")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    CompID, EmpID, ActionID, OrganID, GroupType, GroupID, BusinessType, EmpFlowRemarkID,")
            strSQL.AppendLine("    LastChgComp, LastChgID, LastChgDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @CompID, @EmpID, @ActionID, @OrganID, @GroupType, @GroupID, @BusinessType, @EmpFlowRemarkID,")
            strSQL.AppendLine("    @LastChgComp, @LastChgID, @LastChgDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())

            For Each r As Row In EmpFlowRow
                dbcmd.Parameters.Clear()
                db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                db.AddInParameter(dbcmd, "@EmpID", DbType.String, r.EmpID.Value)
                db.AddInParameter(dbcmd, "@ActionID", DbType.String, r.ActionID.Value)
                db.AddInParameter(dbcmd, "@OrganID", DbType.String, r.OrganID.Value)
                db.AddInParameter(dbcmd, "@GroupType", DbType.String, r.GroupType.Value)
                db.AddInParameter(dbcmd, "@GroupID", DbType.String, r.GroupID.Value)
                db.AddInParameter(dbcmd, "@BusinessType", DbType.String, r.BusinessType.Value)
                db.AddInParameter(dbcmd, "@EmpFlowRemarkID", DbType.String, r.EmpFlowRemarkID.Value)
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

