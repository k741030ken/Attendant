'****************************************************************
' Table:TaxFamily
' Created Date: 2014.09.25
'****************************************************************/
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports Microsoft.Practices.EnterpriseLibrary.Common
Imports Microsoft.Practices.EnterpriseLibrary.Data
Imports System.Data.Common
Imports System.Data

Namespace beTaxFamily
    Public Class Table
        Private m_Rows As Rows = New Rows()
        Private m_Fields As String() = { "CompID", "EmpID", "RelativeIDNo", "ReleaseMark", "ReasonID", "CloseDate", "Address", "Remark" }
        Private m_Types As System.Type() = { GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(Date), GetType(String), GetType(String) }
        Private m_PrimaryFields As String() = { "CompID", "EmpID", "RelativeIDNo", "ReleaseMark" }

        Public ReadOnly Property Rows() As beTaxFamily.Rows 
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
        Public Sub Transfer2Row(TaxFamilyTable As DataTable)
            For Each dr As DataRow In TaxFamilyTable.Rows
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
                dr(m_Rows(i).RelativeIDNo.FieldName) = m_Rows(i).RelativeIDNo.Value
                dr(m_Rows(i).ReleaseMark.FieldName) = m_Rows(i).ReleaseMark.Value
                dr(m_Rows(i).ReasonID.FieldName) = m_Rows(i).ReasonID.Value
                dr(m_Rows(i).CloseDate.FieldName) = m_Rows(i).CloseDate.Value
                dr(m_Rows(i).Address.FieldName) = m_Rows(i).Address.Value
                dr(m_Rows(i).Remark.FieldName) = m_Rows(i).Remark.Value

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

        Public Sub Add(TaxFamilyRow As Row)
            m_Rows.Add(TaxFamilyRow)
        End Sub

        Public Sub Remove(TaxFamilyRow As Row)
            If m_Rows.IndexOf(TaxFamilyRow) >= 0 Then
                m_Rows.Remove(TaxFamilyRow)
            End If
        End Sub

        Public Sub Dispose()
            m_Rows.Clear()
        End Sub

    End Class

    Public Class Row

        Private FI_CompID As Field(Of String) = new Field(Of String)("CompID", true)
        Private FI_EmpID As Field(Of String) = new Field(Of String)("EmpID", true)
        Private FI_RelativeIDNo As Field(Of String) = new Field(Of String)("RelativeIDNo", true)
        Private FI_ReleaseMark As Field(Of String) = new Field(Of String)("ReleaseMark", true)
        Private FI_ReasonID As Field(Of String) = new Field(Of String)("ReasonID", true)
        Private FI_CloseDate As Field(Of Date) = new Field(Of Date)("CloseDate", true)
        Private FI_Address As Field(Of String) = new Field(Of String)("Address", true)
        Private FI_Remark As Field(Of String) = new Field(Of String)("Remark", true)
        Private m_FieldNames As String() = { "CompID", "EmpID", "RelativeIDNo", "ReleaseMark", "ReasonID", "CloseDate", "Address", "Remark" }
        Private m_PrimaryFields As String() = { "CompID", "EmpID", "RelativeIDNo", "ReleaseMark" }
        Private m_IdentityFields As String() = {  }
        Private m_LoadFromDataRow As Boolean = False

        Private Function GetFieldValue(ByVal fieldName As String) As Object
            Select Case fieldName
                Case "CompID"
                    Return FI_CompID.Value
                Case "EmpID"
                    Return FI_EmpID.Value
                Case "RelativeIDNo"
                    Return FI_RelativeIDNo.Value
                Case "ReleaseMark"
                    Return FI_ReleaseMark.Value
                Case "ReasonID"
                    Return FI_ReasonID.Value
                Case "CloseDate"
                    Return FI_CloseDate.Value
                Case "Address"
                    Return FI_Address.Value
                Case "Remark"
                    Return FI_Remark.Value
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
                Case "RelativeIDNo"
                    FI_RelativeIDNo.SetValue(value)
                Case "ReleaseMark"
                    FI_ReleaseMark.SetValue(value)
                Case "ReasonID"
                    FI_ReasonID.SetValue(value)
                Case "CloseDate"
                    FI_CloseDate.SetValue(value)
                Case "Address"
                    FI_Address.SetValue(value)
                Case "Remark"
                    FI_Remark.SetValue(value)
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
                Case "RelativeIDNo"
                    return FI_RelativeIDNo.Updated
                Case "ReleaseMark"
                    return FI_ReleaseMark.Updated
                Case "ReasonID"
                    return FI_ReasonID.Updated
                Case "CloseDate"
                    return FI_CloseDate.Updated
                Case "Address"
                    return FI_Address.Updated
                Case "Remark"
                    return FI_Remark.Updated
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
                Case "RelativeIDNo"
                    return FI_RelativeIDNo.CreateUpdateSQL
                Case "ReleaseMark"
                    return FI_ReleaseMark.CreateUpdateSQL
                Case "ReasonID"
                    return FI_ReasonID.CreateUpdateSQL
                Case "CloseDate"
                    return FI_CloseDate.CreateUpdateSQL
                Case "Address"
                    return FI_Address.CreateUpdateSQL
                Case "Remark"
                    return FI_Remark.CreateUpdateSQL
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
            FI_RelativeIDNo.SetInitValue("")
            FI_ReleaseMark.SetInitValue("")
            FI_ReasonID.SetInitValue("0")
            FI_CloseDate.SetInitValue(Convert.ToDateTime("1900/01/01"))
            FI_Address.SetInitValue("")
            FI_Remark.SetInitValue("")
        End Sub

        Public Sub New(ByVal dr As System.Data.DataRow)
            FI_CompID.SetInitValue(dr("CompID"))
            FI_EmpID.SetInitValue(dr("EmpID"))
            FI_RelativeIDNo.SetInitValue(dr("RelativeIDNo"))
            FI_ReleaseMark.SetInitValue(dr("ReleaseMark"))
            FI_ReasonID.SetInitValue(dr("ReasonID"))
            FI_CloseDate.SetInitValue(dr("CloseDate"))
            FI_Address.SetInitValue(dr("Address"))
            FI_Remark.SetInitValue(dr("Remark"))

            m_LoadFromDataRow = True
        End Sub

        Private Sub ClearUpdated()
            FI_CompID.Updated = False
            FI_EmpID.Updated = False
            FI_RelativeIDNo.Updated = False
            FI_ReleaseMark.Updated = False
            FI_ReasonID.Updated = False
            FI_CloseDate.Updated = False
            FI_Address.Updated = False
            FI_Remark.Updated = False
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

        Public ReadOnly Property RelativeIDNo As Field(Of String) 
            Get
                Return FI_RelativeIDNo
            End Get
        End Property

        Public ReadOnly Property ReleaseMark As Field(Of String) 
            Get
                Return FI_ReleaseMark
            End Get
        End Property

        Public ReadOnly Property ReasonID As Field(Of String) 
            Get
                Return FI_ReasonID
            End Get
        End Property

        Public ReadOnly Property CloseDate As Field(Of Date) 
            Get
                Return FI_CloseDate
            End Get
        End Property

        Public ReadOnly Property Address As Field(Of String) 
            Get
                Return FI_Address
            End Get
        End Property

        Public ReadOnly Property Remark As Field(Of String) 
            Get
                Return FI_Remark
            End Get
        End Property

    End Class

    Public Class Service
        Public Function DeleteRowByPrimaryKey(ByVal TaxFamilyRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Delete From TaxFamily")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And EmpID = @EmpID")
            strSQL.AppendLine("And RelativeIDNo = @RelativeIDNo")
            strSQL.AppendLine("And ReleaseMark = @ReleaseMark")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, TaxFamilyRow.CompID.Value)
            db.AddInParameter(dbcmd, "@EmpID", DbType.String, TaxFamilyRow.EmpID.Value)
            db.AddInParameter(dbcmd, "@RelativeIDNo", DbType.String, TaxFamilyRow.RelativeIDNo.Value)
            db.AddInParameter(dbcmd, "@ReleaseMark", DbType.String, TaxFamilyRow.ReleaseMark.Value)

            return db.ExecuteNonQuery(dbcmd)
        End Function

        public Function DeleteRowByPrimaryKey(ByVal TaxFamilyRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Delete From TaxFamily")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And EmpID = @EmpID")
            strSQL.AppendLine("And RelativeIDNo = @RelativeIDNo")
            strSQL.AppendLine("And ReleaseMark = @ReleaseMark")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, TaxFamilyRow.CompID.Value)
            db.AddInParameter(dbcmd, "@EmpID", DbType.String, TaxFamilyRow.EmpID.Value)
            db.AddInParameter(dbcmd, "@RelativeIDNo", DbType.String, TaxFamilyRow.RelativeIDNo.Value)
            db.AddInParameter(dbcmd, "@ReleaseMark", DbType.String, TaxFamilyRow.ReleaseMark.Value)

            return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function DeleteRowByPrimaryKey(ByVal TaxFamilyRow As Row()) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Delete From TaxFamily")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And EmpID = @EmpID")
            strSQL.AppendLine("And RelativeIDNo = @RelativeIDNo")
            strSQL.AppendLine("And ReleaseMark = @ReleaseMark")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            using cn As DbConnection = db.CreateConnection()
                cn.Open()
                Dim tran As DbTransaction = cn.BeginTransaction()
                Dim inTrans As Boolean = True

                Try
                    For Each r As Row In TaxFamilyRow
                        dbcmd.Parameters.Clear()
                        db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                        db.AddInParameter(dbcmd, "@EmpID", DbType.String, r.EmpID.Value)
                        db.AddInParameter(dbcmd, "@RelativeIDNo", DbType.String, r.RelativeIDNo.Value)
                        db.AddInParameter(dbcmd, "@ReleaseMark", DbType.String, r.ReleaseMark.Value)

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

        Public Function DeleteRowByPrimaryKey(ByVal TaxFamilyRow As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Delete From TaxFamily")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And EmpID = @EmpID")
            strSQL.AppendLine("And RelativeIDNo = @RelativeIDNo")
            strSQL.AppendLine("And ReleaseMark = @ReleaseMark")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())

            For Each r As Row In TaxFamilyRow
                dbcmd.Parameters.Clear()
                db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                db.AddInParameter(dbcmd, "@EmpID", DbType.String, r.EmpID.Value)
                db.AddInParameter(dbcmd, "@RelativeIDNo", DbType.String, r.RelativeIDNo.Value)
                db.AddInParameter(dbcmd, "@ReleaseMark", DbType.String, r.ReleaseMark.Value)

                intRowsAffected += db.ExecuteNonQuery(dbcmd, tran)
            Next
            Return intRowsAffected
        End Function

        Public Function QueryByKey(ByVal TaxFamilyRow As Row) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From TaxFamily")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And EmpID = @EmpID")
            strSQL.AppendLine("And RelativeIDNo = @RelativeIDNo")
            strSQL.AppendLine("And ReleaseMark = @ReleaseMark")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, TaxFamilyRow.CompID.Value)
            db.AddInParameter(dbcmd, "@EmpID", DbType.String, TaxFamilyRow.EmpID.Value)
            db.AddInParameter(dbcmd, "@RelativeIDNo", DbType.String, TaxFamilyRow.RelativeIDNo.Value)
            db.AddInParameter(dbcmd, "@ReleaseMark", DbType.String, TaxFamilyRow.ReleaseMark.Value)

            Return db.ExecuteDataSet(dbcmd)
        End Function

        Public Function QueryByKey(TaxFamilyRow As Row, tran As DbTransaction) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From TaxFamily")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And EmpID = @EmpID")
            strSQL.AppendLine("And RelativeIDNo = @RelativeIDNo")
            strSQL.AppendLine("And ReleaseMark = @ReleaseMark")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, TaxFamilyRow.CompID.Value)
            db.AddInParameter(dbcmd, "@EmpID", DbType.String, TaxFamilyRow.EmpID.Value)
            db.AddInParameter(dbcmd, "@RelativeIDNo", DbType.String, TaxFamilyRow.RelativeIDNo.Value)
            db.AddInParameter(dbcmd, "@ReleaseMark", DbType.String, TaxFamilyRow.ReleaseMark.Value)

            Return db.ExecuteDataSet(dbcmd, tran)
        End Function

        Public Function Update(ByVal TaxFamilyRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim strDot As String = String.Empty

            strSQL.AppendLine("Update TaxFamily Set")
            For i As Integer = 0 To TaxFamilyRow.FieldNames.Length - 1
                If Not TaxFamilyRow.IsIdentityField(TaxFamilyRow.FieldNames(i)) AndAlso TaxFamilyRow.IsUpdated(TaxFamilyRow.FieldNames(i)) AndAlso TaxFamilyRow.CreateUpdateSQL(TaxFamilyRow.FieldNames(i)) Then
                    strSQL.AppendLine(string.Format("{0}{1} = @{1}", strDot, TaxFamilyRow.FieldNames(i)))
                    strDot = ","
                End If
            Next
            If strDot = String.Empty Then Throw New Exception("未異動資料欄位！")
            strSQL.AppendLine("Where CompID = @PKCompID")
            strSQL.AppendLine("And EmpID = @PKEmpID")
            strSQL.AppendLine("And RelativeIDNo = @PKRelativeIDNo")
            strSQL.AppendLine("And ReleaseMark = @PKReleaseMark")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            If TaxFamilyRow.CompID.Updated Then db.AddInParameter(dbcmd, "@CompID", DbType.String, TaxFamilyRow.CompID.Value)
            If TaxFamilyRow.EmpID.Updated Then db.AddInParameter(dbcmd, "@EmpID", DbType.String, TaxFamilyRow.EmpID.Value)
            If TaxFamilyRow.RelativeIDNo.Updated Then db.AddInParameter(dbcmd, "@RelativeIDNo", DbType.String, TaxFamilyRow.RelativeIDNo.Value)
            If TaxFamilyRow.ReleaseMark.Updated Then db.AddInParameter(dbcmd, "@ReleaseMark", DbType.String, TaxFamilyRow.ReleaseMark.Value)
            If TaxFamilyRow.ReasonID.Updated Then db.AddInParameter(dbcmd, "@ReasonID", DbType.String, TaxFamilyRow.ReasonID.Value)
            If TaxFamilyRow.CloseDate.Updated Then db.AddInParameter(dbcmd, "@CloseDate", DbType.Date, IIf(IsDateTimeNull(TaxFamilyRow.CloseDate.Value), Convert.ToDateTime("1900/1/1"), TaxFamilyRow.CloseDate.Value))
            If TaxFamilyRow.Address.Updated Then db.AddInParameter(dbcmd, "@Address", DbType.String, TaxFamilyRow.Address.Value)
            If TaxFamilyRow.Remark.Updated Then db.AddInParameter(dbcmd, "@Remark", DbType.String, TaxFamilyRow.Remark.Value)
            db.AddInParameter(dbcmd, "@PKCompID", DbType.String, IIf(TaxFamilyRow.LoadFromDataRow, TaxFamilyRow.CompID.OldValue, TaxFamilyRow.CompID.Value))
            db.AddInParameter(dbcmd, "@PKEmpID", DbType.String, IIf(TaxFamilyRow.LoadFromDataRow, TaxFamilyRow.EmpID.OldValue, TaxFamilyRow.EmpID.Value))
            db.AddInParameter(dbcmd, "@PKRelativeIDNo", DbType.String, IIf(TaxFamilyRow.LoadFromDataRow, TaxFamilyRow.RelativeIDNo.OldValue, TaxFamilyRow.RelativeIDNo.Value))
            db.AddInParameter(dbcmd, "@PKReleaseMark", DbType.String, IIf(TaxFamilyRow.LoadFromDataRow, TaxFamilyRow.ReleaseMark.OldValue, TaxFamilyRow.ReleaseMark.Value))

            Return db.ExecuteNonQuery(dbcmd)
        End Function

        Public Function Update(ByVal TaxFamilyRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim strDot As String = String.Empty

            strSQL.AppendLine("Update TaxFamily Set")
            For i As Integer = 0 To TaxFamilyRow.FieldNames.Length - 1
                If Not TaxFamilyRow.IsIdentityField(TaxFamilyRow.FieldNames(i)) AndAlso TaxFamilyRow.IsUpdated(TaxFamilyRow.FieldNames(i)) AndAlso TaxFamilyRow.CreateUpdateSQL(TaxFamilyRow.FieldNames(i)) Then
                    strSQL.AppendLine(string.Format("{0}{1} = @{1}", strDot, TaxFamilyRow.FieldNames(i)))
                    strDot = ","
                End If
            Next 
            If strDot = String.Empty Then Throw New Exception("未異動資料欄位！")
            strSQL.AppendLine("Where CompID = @PKCompID")
            strSQL.AppendLine("And EmpID = @PKEmpID")
            strSQL.AppendLine("And RelativeIDNo = @PKRelativeIDNo")
            strSQL.AppendLine("And ReleaseMark = @PKReleaseMark")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            If TaxFamilyRow.CompID.Updated Then db.AddInParameter(dbcmd, "@CompID", DbType.String, TaxFamilyRow.CompID.Value)
            If TaxFamilyRow.EmpID.Updated Then db.AddInParameter(dbcmd, "@EmpID", DbType.String, TaxFamilyRow.EmpID.Value)
            If TaxFamilyRow.RelativeIDNo.Updated Then db.AddInParameter(dbcmd, "@RelativeIDNo", DbType.String, TaxFamilyRow.RelativeIDNo.Value)
            If TaxFamilyRow.ReleaseMark.Updated Then db.AddInParameter(dbcmd, "@ReleaseMark", DbType.String, TaxFamilyRow.ReleaseMark.Value)
            If TaxFamilyRow.ReasonID.Updated Then db.AddInParameter(dbcmd, "@ReasonID", DbType.String, TaxFamilyRow.ReasonID.Value)
            If TaxFamilyRow.CloseDate.Updated Then db.AddInParameter(dbcmd, "@CloseDate", DbType.Date, IIf(IsDateTimeNull(TaxFamilyRow.CloseDate.Value), Convert.ToDateTime("1900/1/1"), TaxFamilyRow.CloseDate.Value))
            If TaxFamilyRow.Address.Updated Then db.AddInParameter(dbcmd, "@Address", DbType.String, TaxFamilyRow.Address.Value)
            If TaxFamilyRow.Remark.Updated Then db.AddInParameter(dbcmd, "@Remark", DbType.String, TaxFamilyRow.Remark.Value)
            db.AddInParameter(dbcmd, "@PKCompID", DbType.String, IIf(TaxFamilyRow.LoadFromDataRow, TaxFamilyRow.CompID.OldValue, TaxFamilyRow.CompID.Value))
            db.AddInParameter(dbcmd, "@PKEmpID", DbType.String, IIf(TaxFamilyRow.LoadFromDataRow, TaxFamilyRow.EmpID.OldValue, TaxFamilyRow.EmpID.Value))
            db.AddInParameter(dbcmd, "@PKRelativeIDNo", DbType.String, IIf(TaxFamilyRow.LoadFromDataRow, TaxFamilyRow.RelativeIDNo.OldValue, TaxFamilyRow.RelativeIDNo.Value))
            db.AddInParameter(dbcmd, "@PKReleaseMark", DbType.String, IIf(TaxFamilyRow.LoadFromDataRow, TaxFamilyRow.ReleaseMark.OldValue, TaxFamilyRow.ReleaseMark.Value))

            Return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function Update(ByVal TaxFamilyRow As Row()) As Integer
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
                    For Each r As Row In TaxFamilyRow
                        strDot = String.Empty
                        If strSQL.Length > 0 Then strSQL.Remove(0, strSQL.Length)
                        strSQL.AppendLine("Update TaxFamily Set")
                        For i As Integer = 0 To r.FieldNames.Length - 1
                            If Not r.IsIdentityField(r.FieldNames(i)) AndAlso r.IsUpdated(r.FieldNames(i)) AndAlso r.CreateUpdateSQL(r.FieldNames(i)) Then
                                strSQL.AppendLine(String.Format("{0}{1} = @{1}", strDot, r.FieldNames(i)))
                                strDot = ","
                            End If
                        Next
                        If strDot = String.Empty Then Continue For
                        strSQL.AppendLine("Where CompID = @PKCompID")
                        strSQL.AppendLine("And EmpID = @PKEmpID")
                        strSQL.AppendLine("And RelativeIDNo = @PKRelativeIDNo")
                        strSQL.AppendLine("And ReleaseMark = @PKReleaseMark")

                        dbcmd = db.GetSqlStringCommand(strSQL.ToString())
                        If r.CompID.Updated Then db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                        If r.EmpID.Updated Then db.AddInParameter(dbcmd, "@EmpID", DbType.String, r.EmpID.Value)
                        If r.RelativeIDNo.Updated Then db.AddInParameter(dbcmd, "@RelativeIDNo", DbType.String, r.RelativeIDNo.Value)
                        If r.ReleaseMark.Updated Then db.AddInParameter(dbcmd, "@ReleaseMark", DbType.String, r.ReleaseMark.Value)
                        If r.ReasonID.Updated Then db.AddInParameter(dbcmd, "@ReasonID", DbType.String, r.ReasonID.Value)
                        If r.CloseDate.Updated Then db.AddInParameter(dbcmd, "@CloseDate", DbType.Date, IIf(IsDateTimeNull(r.CloseDate.Value), Convert.ToDateTime("1900/1/1"), r.CloseDate.Value))
                        If r.Address.Updated Then db.AddInParameter(dbcmd, "@Address", DbType.String, r.Address.Value)
                        If r.Remark.Updated Then db.AddInParameter(dbcmd, "@Remark", DbType.String, r.Remark.Value)
                        db.AddInParameter(dbcmd, "@PKCompID", DbType.String, IIf(r.LoadFromDataRow, r.CompID.OldValue, r.CompID.Value))
                        db.AddInParameter(dbcmd, "@PKEmpID", DbType.String, IIf(r.LoadFromDataRow, r.EmpID.OldValue, r.EmpID.Value))
                        db.AddInParameter(dbcmd, "@PKRelativeIDNo", DbType.String, IIf(r.LoadFromDataRow, r.RelativeIDNo.OldValue, r.RelativeIDNo.Value))
                        db.AddInParameter(dbcmd, "@PKReleaseMark", DbType.String, IIf(r.LoadFromDataRow, r.ReleaseMark.OldValue, r.ReleaseMark.Value))

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

        Public Function Update(ByVal TaxFamilyRow As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0
            Dim strDot As String

            For Each r As Row In TaxFamilyRow
                strDot = String.Empty
                If strSQL.Length > 0 Then strSQL.Remove(0, strSQL.Length)
                strSQL.AppendLine("Update TaxFamily Set")
                For i As Integer = 0 To r.FieldNames.Length - 1
                    If Not r.IsIdentityField(r.FieldNames(i)) AndAlso r.IsUpdated(r.FieldNames(i)) AndAlso r.CreateUpdateSQL(r.FieldNames(i))
                        strSQL.AppendLine(String.Format("{0}{1} = @{1}", strDot, r.FieldNames(i)))
                        strDot = ","
                    End If
                Next
                If strDot = String.Empty Then Continue For
                strSQL.AppendLine("Where CompID = @PKCompID")
                strSQL.AppendLine("And EmpID = @PKEmpID")
                strSQL.AppendLine("And RelativeIDNo = @PKRelativeIDNo")
                strSQL.AppendLine("And ReleaseMark = @PKReleaseMark")

                dbcmd = db.GetSqlStringCommand(strSQL.ToString())
                If r.CompID.Updated Then db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                If r.EmpID.Updated Then db.AddInParameter(dbcmd, "@EmpID", DbType.String, r.EmpID.Value)
                If r.RelativeIDNo.Updated Then db.AddInParameter(dbcmd, "@RelativeIDNo", DbType.String, r.RelativeIDNo.Value)
                If r.ReleaseMark.Updated Then db.AddInParameter(dbcmd, "@ReleaseMark", DbType.String, r.ReleaseMark.Value)
                If r.ReasonID.Updated Then db.AddInParameter(dbcmd, "@ReasonID", DbType.String, r.ReasonID.Value)
                If r.CloseDate.Updated Then db.AddInParameter(dbcmd, "@CloseDate", DbType.Date, IIf(IsDateTimeNull(r.CloseDate.Value), Convert.ToDateTime("1900/1/1"), r.CloseDate.Value))
                If r.Address.Updated Then db.AddInParameter(dbcmd, "@Address", DbType.String, r.Address.Value)
                If r.Remark.Updated Then db.AddInParameter(dbcmd, "@Remark", DbType.String, r.Remark.Value)
                db.AddInParameter(dbcmd, "@PKCompID", DbType.String, IIf(r.LoadFromDataRow, r.CompID.OldValue, r.CompID.Value))
                db.AddInParameter(dbcmd, "@PKEmpID", DbType.String, IIf(r.LoadFromDataRow, r.EmpID.OldValue, r.EmpID.Value))
                db.AddInParameter(dbcmd, "@PKRelativeIDNo", DbType.String, IIf(r.LoadFromDataRow, r.RelativeIDNo.OldValue, r.RelativeIDNo.Value))
                db.AddInParameter(dbcmd, "@PKReleaseMark", DbType.String, IIf(r.LoadFromDataRow, r.ReleaseMark.OldValue, r.ReleaseMark.Value))

                intRowsAffected += db.ExecuteNonQuery(dbcmd, tran)
            Next
            Return intRowsAffected
        End Function

        Public Function IsDataExists(ByVal TaxFamilyRow As Row) As Boolean
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intCount As Integer = 0

            strSQL.AppendLine("Select Count(*) Cnt From TaxFamily")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And EmpID = @EmpID")
            strSQL.AppendLine("And RelativeIDNo = @RelativeIDNo")
            strSQL.AppendLine("And ReleaseMark = @ReleaseMark")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, TaxFamilyRow.CompID.Value)
            db.AddInParameter(dbcmd, "@EmpID", DbType.String, TaxFamilyRow.EmpID.Value)
            db.AddInParameter(dbcmd, "@RelativeIDNo", DbType.String, TaxFamilyRow.RelativeIDNo.Value)
            db.AddInParameter(dbcmd, "@ReleaseMark", DbType.String, TaxFamilyRow.ReleaseMark.Value)

            intCount = Convert.ToInt32(db.ExecuteScalar(dbcmd))
            Return IIf(intCount > 0, True, False)
        End Function

        Public Function IsDataExists(ByVal TaxFamilyRow As Row, ByVal tran As DbTransaction) As Boolean
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intCount As Integer = 0

            strSQL.AppendLine("Select Count(*) Cnt From TaxFamily")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And EmpID = @EmpID")
            strSQL.AppendLine("And RelativeIDNo = @RelativeIDNo")
            strSQL.AppendLine("And ReleaseMark = @ReleaseMark")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, TaxFamilyRow.CompID.Value)
            db.AddInParameter(dbcmd, "@EmpID", DbType.String, TaxFamilyRow.EmpID.Value)
            db.AddInParameter(dbcmd, "@RelativeIDNo", DbType.String, TaxFamilyRow.RelativeIDNo.Value)
            db.AddInParameter(dbcmd, "@ReleaseMark", DbType.String, TaxFamilyRow.ReleaseMark.Value)

            intCount = Convert.ToInt32(db.ExecuteScalar(dbcmd, tran))
            Return IIf(intCount > 0, True, False)
        End Function

        Public Function QuerybyWhere(WhereCondition As String) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From TaxFamily")
            If WhereCondition <> String.Empty Then strSQL.AppendLine(WhereCondition)

            Return db.ExecuteDataSet(CommandType.Text, strSQL.ToString())
        End Function

        Public Function Insert(ByVal TaxFamilyRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Insert into TaxFamily")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    CompID, EmpID, RelativeIDNo, ReleaseMark, ReasonID, CloseDate, Address, Remark")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @CompID, @EmpID, @RelativeIDNo, @ReleaseMark, @ReasonID, @CloseDate, @Address, @Remark")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, TaxFamilyRow.CompID.Value)
            db.AddInParameter(dbcmd, "@EmpID", DbType.String, TaxFamilyRow.EmpID.Value)
            db.AddInParameter(dbcmd, "@RelativeIDNo", DbType.String, TaxFamilyRow.RelativeIDNo.Value)
            db.AddInParameter(dbcmd, "@ReleaseMark", DbType.String, TaxFamilyRow.ReleaseMark.Value)
            db.AddInParameter(dbcmd, "@ReasonID", DbType.String, TaxFamilyRow.ReasonID.Value)
            db.AddInParameter(dbcmd, "@CloseDate", DbType.Date, IIf(IsDateTimeNull(TaxFamilyRow.CloseDate.Value), Convert.ToDateTime("1900/1/1"), TaxFamilyRow.CloseDate.Value))
            db.AddInParameter(dbcmd, "@Address", DbType.String, TaxFamilyRow.Address.Value)
            db.AddInParameter(dbcmd, "@Remark", DbType.String, TaxFamilyRow.Remark.Value)

            Return db.ExecuteNonQuery(dbcmd)
        End Function

        Public Function Insert(ByVal TaxFamilyRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Insert into TaxFamily")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    CompID, EmpID, RelativeIDNo, ReleaseMark, ReasonID, CloseDate, Address, Remark")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @CompID, @EmpID, @RelativeIDNo, @ReleaseMark, @ReasonID, @CloseDate, @Address, @Remark")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, TaxFamilyRow.CompID.Value)
            db.AddInParameter(dbcmd, "@EmpID", DbType.String, TaxFamilyRow.EmpID.Value)
            db.AddInParameter(dbcmd, "@RelativeIDNo", DbType.String, TaxFamilyRow.RelativeIDNo.Value)
            db.AddInParameter(dbcmd, "@ReleaseMark", DbType.String, TaxFamilyRow.ReleaseMark.Value)
            db.AddInParameter(dbcmd, "@ReasonID", DbType.String, TaxFamilyRow.ReasonID.Value)
            db.AddInParameter(dbcmd, "@CloseDate", DbType.Date, IIf(IsDateTimeNull(TaxFamilyRow.CloseDate.Value), Convert.ToDateTime("1900/1/1"), TaxFamilyRow.CloseDate.Value))
            db.AddInParameter(dbcmd, "@Address", DbType.String, TaxFamilyRow.Address.Value)
            db.AddInParameter(dbcmd, "@Remark", DbType.String, TaxFamilyRow.Remark.Value)

            Return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function Insert(ByVal TaxFamilyRow As Row()) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Insert into TaxFamily")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    CompID, EmpID, RelativeIDNo, ReleaseMark, ReasonID, CloseDate, Address, Remark")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @CompID, @EmpID, @RelativeIDNo, @ReleaseMark, @ReasonID, @CloseDate, @Address, @Remark")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            Using cn As DbConnection = db.CreateConnection()
                cn.Open()
                Dim tran As DbTransaction = cn.BeginTransaction()
                Dim inTrans As Boolean = True

                Try
                    For Each r As Row In TaxFamilyRow
                        dbcmd.Parameters.Clear()
                        db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                        db.AddInParameter(dbcmd, "@EmpID", DbType.String, r.EmpID.Value)
                        db.AddInParameter(dbcmd, "@RelativeIDNo", DbType.String, r.RelativeIDNo.Value)
                        db.AddInParameter(dbcmd, "@ReleaseMark", DbType.String, r.ReleaseMark.Value)
                        db.AddInParameter(dbcmd, "@ReasonID", DbType.String, r.ReasonID.Value)
                        db.AddInParameter(dbcmd, "@CloseDate", DbType.Date, IIf(IsDateTimeNull(r.CloseDate.Value), Convert.ToDateTime("1900/1/1"), r.CloseDate.Value))
                        db.AddInParameter(dbcmd, "@Address", DbType.String, r.Address.Value)
                        db.AddInParameter(dbcmd, "@Remark", DbType.String, r.Remark.Value)

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

        Public Function Insert(ByVal TaxFamilyRow As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Insert into TaxFamily")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    CompID, EmpID, RelativeIDNo, ReleaseMark, ReasonID, CloseDate, Address, Remark")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @CompID, @EmpID, @RelativeIDNo, @ReleaseMark, @ReasonID, @CloseDate, @Address, @Remark")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())

            For Each r As Row In TaxFamilyRow
                dbcmd.Parameters.Clear()
                db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                db.AddInParameter(dbcmd, "@EmpID", DbType.String, r.EmpID.Value)
                db.AddInParameter(dbcmd, "@RelativeIDNo", DbType.String, r.RelativeIDNo.Value)
                db.AddInParameter(dbcmd, "@ReleaseMark", DbType.String, r.ReleaseMark.Value)
                db.AddInParameter(dbcmd, "@ReasonID", DbType.String, r.ReasonID.Value)
                db.AddInParameter(dbcmd, "@CloseDate", DbType.Date, IIf(IsDateTimeNull(r.CloseDate.Value), Convert.ToDateTime("1900/1/1"), r.CloseDate.Value))
                db.AddInParameter(dbcmd, "@Address", DbType.String, r.Address.Value)
                db.AddInParameter(dbcmd, "@Remark", DbType.String, r.Remark.Value)

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

