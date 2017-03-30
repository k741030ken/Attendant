'****************************************************************
' Table:TaxParameterOrgan
' Created Date: 2016.05.13
'****************************************************************/
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports Microsoft.Practices.EnterpriseLibrary.Common
Imports Microsoft.Practices.EnterpriseLibrary.Data
Imports System.Data.Common
Imports System.Data

Namespace beTaxParameterOrgan
    Public Class Table
        Private m_Rows As Rows = New Rows()
        Private m_Fields As String() = { "CompID", "InvoiceNo", "InvoiceOrganID", "InvoiceName", "HeadOfficeFlag", "TaxCityCode", "TaxUnitNo", "LastChgComp", "LastChgID", "LastChgDate" }
        Private m_Types As System.Type() = { GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(Date) }
        Private m_PrimaryFields As String() = { "CompID", "InvoiceNo" }

        Public ReadOnly Property Rows() As beTaxParameterOrgan.Rows 
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
        Public Sub Transfer2Row(TaxParameterOrganTable As DataTable)
            For Each dr As DataRow In TaxParameterOrganTable.Rows
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
                dr(m_Rows(i).InvoiceNo.FieldName) = m_Rows(i).InvoiceNo.Value
                dr(m_Rows(i).InvoiceOrganID.FieldName) = m_Rows(i).InvoiceOrganID.Value
                dr(m_Rows(i).InvoiceName.FieldName) = m_Rows(i).InvoiceName.Value
                dr(m_Rows(i).HeadOfficeFlag.FieldName) = m_Rows(i).HeadOfficeFlag.Value
                dr(m_Rows(i).TaxCityCode.FieldName) = m_Rows(i).TaxCityCode.Value
                dr(m_Rows(i).TaxUnitNo.FieldName) = m_Rows(i).TaxUnitNo.Value
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

        Public Sub Add(TaxParameterOrganRow As Row)
            m_Rows.Add(TaxParameterOrganRow)
        End Sub

        Public Sub Remove(TaxParameterOrganRow As Row)
            If m_Rows.IndexOf(TaxParameterOrganRow) >= 0 Then
                m_Rows.Remove(TaxParameterOrganRow)
            End If
        End Sub

        Public Sub Dispose()
            m_Rows.Clear()
        End Sub

    End Class

    Public Class Row

        Private FI_CompID As Field(Of String) = new Field(Of String)("CompID", true)
        Private FI_InvoiceNo As Field(Of String) = new Field(Of String)("InvoiceNo", true)
        Private FI_InvoiceOrganID As Field(Of String) = new Field(Of String)("InvoiceOrganID", true)
        Private FI_InvoiceName As Field(Of String) = new Field(Of String)("InvoiceName", true)
        Private FI_HeadOfficeFlag As Field(Of String) = new Field(Of String)("HeadOfficeFlag", true)
        Private FI_TaxCityCode As Field(Of String) = new Field(Of String)("TaxCityCode", true)
        Private FI_TaxUnitNo As Field(Of String) = new Field(Of String)("TaxUnitNo", true)
        Private FI_LastChgComp As Field(Of String) = new Field(Of String)("LastChgComp", true)
        Private FI_LastChgID As Field(Of String) = new Field(Of String)("LastChgID", true)
        Private FI_LastChgDate As Field(Of Date) = new Field(Of Date)("LastChgDate", true)
        Private m_FieldNames As String() = { "CompID", "InvoiceNo", "InvoiceOrganID", "InvoiceName", "HeadOfficeFlag", "TaxCityCode", "TaxUnitNo", "LastChgComp", "LastChgID", "LastChgDate" }
        Private m_PrimaryFields As String() = { "CompID", "InvoiceNo" }
        Private m_IdentityFields As String() = {  }
        Private m_LoadFromDataRow As Boolean = False

        Private Function GetFieldValue(ByVal fieldName As String) As Object
            Select Case fieldName
                Case "CompID"
                    Return FI_CompID.Value
                Case "InvoiceNo"
                    Return FI_InvoiceNo.Value
                Case "InvoiceOrganID"
                    Return FI_InvoiceOrganID.Value
                Case "InvoiceName"
                    Return FI_InvoiceName.Value
                Case "HeadOfficeFlag"
                    Return FI_HeadOfficeFlag.Value
                Case "TaxCityCode"
                    Return FI_TaxCityCode.Value
                Case "TaxUnitNo"
                    Return FI_TaxUnitNo.Value
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
                Case "InvoiceNo"
                    FI_InvoiceNo.SetValue(value)
                Case "InvoiceOrganID"
                    FI_InvoiceOrganID.SetValue(value)
                Case "InvoiceName"
                    FI_InvoiceName.SetValue(value)
                Case "HeadOfficeFlag"
                    FI_HeadOfficeFlag.SetValue(value)
                Case "TaxCityCode"
                    FI_TaxCityCode.SetValue(value)
                Case "TaxUnitNo"
                    FI_TaxUnitNo.SetValue(value)
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
                Case "InvoiceNo"
                    return FI_InvoiceNo.Updated
                Case "InvoiceOrganID"
                    return FI_InvoiceOrganID.Updated
                Case "InvoiceName"
                    return FI_InvoiceName.Updated
                Case "HeadOfficeFlag"
                    return FI_HeadOfficeFlag.Updated
                Case "TaxCityCode"
                    return FI_TaxCityCode.Updated
                Case "TaxUnitNo"
                    return FI_TaxUnitNo.Updated
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
                Case "InvoiceNo"
                    return FI_InvoiceNo.CreateUpdateSQL
                Case "InvoiceOrganID"
                    return FI_InvoiceOrganID.CreateUpdateSQL
                Case "InvoiceName"
                    return FI_InvoiceName.CreateUpdateSQL
                Case "HeadOfficeFlag"
                    return FI_HeadOfficeFlag.CreateUpdateSQL
                Case "TaxCityCode"
                    return FI_TaxCityCode.CreateUpdateSQL
                Case "TaxUnitNo"
                    return FI_TaxUnitNo.CreateUpdateSQL
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
            FI_InvoiceNo.SetInitValue("")
            FI_InvoiceOrganID.SetInitValue("")
            FI_InvoiceName.SetInitValue("")
            FI_HeadOfficeFlag.SetInitValue("0")
            FI_TaxCityCode.SetInitValue("")
            FI_TaxUnitNo.SetInitValue("")
            FI_LastChgComp.SetInitValue("")
            FI_LastChgID.SetInitValue("")
            FI_LastChgDate.SetInitValue(Convert.ToDateTime("1900/01/01"))
        End Sub

        Public Sub New(ByVal dr As System.Data.DataRow)
            FI_CompID.SetInitValue(dr("CompID"))
            FI_InvoiceNo.SetInitValue(dr("InvoiceNo"))
            FI_InvoiceOrganID.SetInitValue(dr("InvoiceOrganID"))
            FI_InvoiceName.SetInitValue(dr("InvoiceName"))
            FI_HeadOfficeFlag.SetInitValue(dr("HeadOfficeFlag"))
            FI_TaxCityCode.SetInitValue(dr("TaxCityCode"))
            FI_TaxUnitNo.SetInitValue(dr("TaxUnitNo"))
            FI_LastChgComp.SetInitValue(dr("LastChgComp"))
            FI_LastChgID.SetInitValue(dr("LastChgID"))
            FI_LastChgDate.SetInitValue(dr("LastChgDate"))

            m_LoadFromDataRow = True
        End Sub

        Private Sub ClearUpdated()
            FI_CompID.Updated = False
            FI_InvoiceNo.Updated = False
            FI_InvoiceOrganID.Updated = False
            FI_InvoiceName.Updated = False
            FI_HeadOfficeFlag.Updated = False
            FI_TaxCityCode.Updated = False
            FI_TaxUnitNo.Updated = False
            FI_LastChgComp.Updated = False
            FI_LastChgID.Updated = False
            FI_LastChgDate.Updated = False
        End Sub

        Public ReadOnly Property CompID As Field(Of String) 
            Get
                Return FI_CompID
            End Get
        End Property

        Public ReadOnly Property InvoiceNo As Field(Of String) 
            Get
                Return FI_InvoiceNo
            End Get
        End Property

        Public ReadOnly Property InvoiceOrganID As Field(Of String) 
            Get
                Return FI_InvoiceOrganID
            End Get
        End Property

        Public ReadOnly Property InvoiceName As Field(Of String) 
            Get
                Return FI_InvoiceName
            End Get
        End Property

        Public ReadOnly Property HeadOfficeFlag As Field(Of String) 
            Get
                Return FI_HeadOfficeFlag
            End Get
        End Property

        Public ReadOnly Property TaxCityCode As Field(Of String) 
            Get
                Return FI_TaxCityCode
            End Get
        End Property

        Public ReadOnly Property TaxUnitNo As Field(Of String) 
            Get
                Return FI_TaxUnitNo
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
        Public Function DeleteRowByPrimaryKey(ByVal TaxParameterOrganRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Delete From TaxParameterOrgan")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And InvoiceNo = @InvoiceNo")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, TaxParameterOrganRow.CompID.Value)
            db.AddInParameter(dbcmd, "@InvoiceNo", DbType.String, TaxParameterOrganRow.InvoiceNo.Value)

            return db.ExecuteNonQuery(dbcmd)
        End Function

        public Function DeleteRowByPrimaryKey(ByVal TaxParameterOrganRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Delete From TaxParameterOrgan")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And InvoiceNo = @InvoiceNo")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, TaxParameterOrganRow.CompID.Value)
            db.AddInParameter(dbcmd, "@InvoiceNo", DbType.String, TaxParameterOrganRow.InvoiceNo.Value)

            return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function DeleteRowByPrimaryKey(ByVal TaxParameterOrganRow As Row()) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Delete From TaxParameterOrgan")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And InvoiceNo = @InvoiceNo")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            using cn As DbConnection = db.CreateConnection()
                cn.Open()
                Dim tran As DbTransaction = cn.BeginTransaction()
                Dim inTrans As Boolean = True

                Try
                    For Each r As Row In TaxParameterOrganRow
                        dbcmd.Parameters.Clear()
                        db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                        db.AddInParameter(dbcmd, "@InvoiceNo", DbType.String, r.InvoiceNo.Value)

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

        Public Function DeleteRowByPrimaryKey(ByVal TaxParameterOrganRow As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Delete From TaxParameterOrgan")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And InvoiceNo = @InvoiceNo")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())

            For Each r As Row In TaxParameterOrganRow
                dbcmd.Parameters.Clear()
                db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                db.AddInParameter(dbcmd, "@InvoiceNo", DbType.String, r.InvoiceNo.Value)

                intRowsAffected += db.ExecuteNonQuery(dbcmd, tran)
            Next
            Return intRowsAffected
        End Function

        Public Function QueryByKey(ByVal TaxParameterOrganRow As Row) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From TaxParameterOrgan")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And InvoiceNo = @InvoiceNo")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, TaxParameterOrganRow.CompID.Value)
            db.AddInParameter(dbcmd, "@InvoiceNo", DbType.String, TaxParameterOrganRow.InvoiceNo.Value)

            Return db.ExecuteDataSet(dbcmd)
        End Function

        Public Function QueryByKey(TaxParameterOrganRow As Row, tran As DbTransaction) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From TaxParameterOrgan")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And InvoiceNo = @InvoiceNo")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, TaxParameterOrganRow.CompID.Value)
            db.AddInParameter(dbcmd, "@InvoiceNo", DbType.String, TaxParameterOrganRow.InvoiceNo.Value)

            Return db.ExecuteDataSet(dbcmd, tran)
        End Function

        Public Function Update(ByVal TaxParameterOrganRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim strDot As String = String.Empty

            strSQL.AppendLine("Update TaxParameterOrgan Set")
            For i As Integer = 0 To TaxParameterOrganRow.FieldNames.Length - 1
                If Not TaxParameterOrganRow.IsIdentityField(TaxParameterOrganRow.FieldNames(i)) AndAlso TaxParameterOrganRow.IsUpdated(TaxParameterOrganRow.FieldNames(i)) AndAlso TaxParameterOrganRow.CreateUpdateSQL(TaxParameterOrganRow.FieldNames(i)) Then
                    strSQL.AppendLine(string.Format("{0}{1} = @{1}", strDot, TaxParameterOrganRow.FieldNames(i)))
                    strDot = ","
                End If
            Next
            If strDot = String.Empty Then Throw New Exception("未異動資料欄位！")
            strSQL.AppendLine("Where CompID = @PKCompID")
            strSQL.AppendLine("And InvoiceNo = @PKInvoiceNo")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            If TaxParameterOrganRow.CompID.Updated Then db.AddInParameter(dbcmd, "@CompID", DbType.String, TaxParameterOrganRow.CompID.Value)
            If TaxParameterOrganRow.InvoiceNo.Updated Then db.AddInParameter(dbcmd, "@InvoiceNo", DbType.String, TaxParameterOrganRow.InvoiceNo.Value)
            If TaxParameterOrganRow.InvoiceOrganID.Updated Then db.AddInParameter(dbcmd, "@InvoiceOrganID", DbType.String, TaxParameterOrganRow.InvoiceOrganID.Value)
            If TaxParameterOrganRow.InvoiceName.Updated Then db.AddInParameter(dbcmd, "@InvoiceName", DbType.String, TaxParameterOrganRow.InvoiceName.Value)
            If TaxParameterOrganRow.HeadOfficeFlag.Updated Then db.AddInParameter(dbcmd, "@HeadOfficeFlag", DbType.String, TaxParameterOrganRow.HeadOfficeFlag.Value)
            If TaxParameterOrganRow.TaxCityCode.Updated Then db.AddInParameter(dbcmd, "@TaxCityCode", DbType.String, TaxParameterOrganRow.TaxCityCode.Value)
            If TaxParameterOrganRow.TaxUnitNo.Updated Then db.AddInParameter(dbcmd, "@TaxUnitNo", DbType.String, TaxParameterOrganRow.TaxUnitNo.Value)
            If TaxParameterOrganRow.LastChgComp.Updated Then db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, TaxParameterOrganRow.LastChgComp.Value)
            If TaxParameterOrganRow.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, TaxParameterOrganRow.LastChgID.Value)
            If TaxParameterOrganRow.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(TaxParameterOrganRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), TaxParameterOrganRow.LastChgDate.Value))
            db.AddInParameter(dbcmd, "@PKCompID", DbType.String, IIf(TaxParameterOrganRow.LoadFromDataRow, TaxParameterOrganRow.CompID.OldValue, TaxParameterOrganRow.CompID.Value))
            db.AddInParameter(dbcmd, "@PKInvoiceNo", DbType.String, IIf(TaxParameterOrganRow.LoadFromDataRow, TaxParameterOrganRow.InvoiceNo.OldValue, TaxParameterOrganRow.InvoiceNo.Value))

            Return db.ExecuteNonQuery(dbcmd)
        End Function

        Public Function Update(ByVal TaxParameterOrganRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim strDot As String = String.Empty

            strSQL.AppendLine("Update TaxParameterOrgan Set")
            For i As Integer = 0 To TaxParameterOrganRow.FieldNames.Length - 1
                If Not TaxParameterOrganRow.IsIdentityField(TaxParameterOrganRow.FieldNames(i)) AndAlso TaxParameterOrganRow.IsUpdated(TaxParameterOrganRow.FieldNames(i)) AndAlso TaxParameterOrganRow.CreateUpdateSQL(TaxParameterOrganRow.FieldNames(i)) Then
                    strSQL.AppendLine(string.Format("{0}{1} = @{1}", strDot, TaxParameterOrganRow.FieldNames(i)))
                    strDot = ","
                End If
            Next 
            If strDot = String.Empty Then Throw New Exception("未異動資料欄位！")
            strSQL.AppendLine("Where CompID = @PKCompID")
            strSQL.AppendLine("And InvoiceNo = @PKInvoiceNo")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            If TaxParameterOrganRow.CompID.Updated Then db.AddInParameter(dbcmd, "@CompID", DbType.String, TaxParameterOrganRow.CompID.Value)
            If TaxParameterOrganRow.InvoiceNo.Updated Then db.AddInParameter(dbcmd, "@InvoiceNo", DbType.String, TaxParameterOrganRow.InvoiceNo.Value)
            If TaxParameterOrganRow.InvoiceOrganID.Updated Then db.AddInParameter(dbcmd, "@InvoiceOrganID", DbType.String, TaxParameterOrganRow.InvoiceOrganID.Value)
            If TaxParameterOrganRow.InvoiceName.Updated Then db.AddInParameter(dbcmd, "@InvoiceName", DbType.String, TaxParameterOrganRow.InvoiceName.Value)
            If TaxParameterOrganRow.HeadOfficeFlag.Updated Then db.AddInParameter(dbcmd, "@HeadOfficeFlag", DbType.String, TaxParameterOrganRow.HeadOfficeFlag.Value)
            If TaxParameterOrganRow.TaxCityCode.Updated Then db.AddInParameter(dbcmd, "@TaxCityCode", DbType.String, TaxParameterOrganRow.TaxCityCode.Value)
            If TaxParameterOrganRow.TaxUnitNo.Updated Then db.AddInParameter(dbcmd, "@TaxUnitNo", DbType.String, TaxParameterOrganRow.TaxUnitNo.Value)
            If TaxParameterOrganRow.LastChgComp.Updated Then db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, TaxParameterOrganRow.LastChgComp.Value)
            If TaxParameterOrganRow.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, TaxParameterOrganRow.LastChgID.Value)
            If TaxParameterOrganRow.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(TaxParameterOrganRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), TaxParameterOrganRow.LastChgDate.Value))
            db.AddInParameter(dbcmd, "@PKCompID", DbType.String, IIf(TaxParameterOrganRow.LoadFromDataRow, TaxParameterOrganRow.CompID.OldValue, TaxParameterOrganRow.CompID.Value))
            db.AddInParameter(dbcmd, "@PKInvoiceNo", DbType.String, IIf(TaxParameterOrganRow.LoadFromDataRow, TaxParameterOrganRow.InvoiceNo.OldValue, TaxParameterOrganRow.InvoiceNo.Value))

            Return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function Update(ByVal TaxParameterOrganRow As Row()) As Integer
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
                    For Each r As Row In TaxParameterOrganRow
                        strDot = String.Empty
                        If strSQL.Length > 0 Then strSQL.Remove(0, strSQL.Length)
                        strSQL.AppendLine("Update TaxParameterOrgan Set")
                        For i As Integer = 0 To r.FieldNames.Length - 1
                            If Not r.IsIdentityField(r.FieldNames(i)) AndAlso r.IsUpdated(r.FieldNames(i)) AndAlso r.CreateUpdateSQL(r.FieldNames(i)) Then
                                strSQL.AppendLine(String.Format("{0}{1} = @{1}", strDot, r.FieldNames(i)))
                                strDot = ","
                            End If
                        Next
                        If strDot = String.Empty Then Continue For
                        strSQL.AppendLine("Where CompID = @PKCompID")
                        strSQL.AppendLine("And InvoiceNo = @PKInvoiceNo")

                        dbcmd = db.GetSqlStringCommand(strSQL.ToString())
                        If r.CompID.Updated Then db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                        If r.InvoiceNo.Updated Then db.AddInParameter(dbcmd, "@InvoiceNo", DbType.String, r.InvoiceNo.Value)
                        If r.InvoiceOrganID.Updated Then db.AddInParameter(dbcmd, "@InvoiceOrganID", DbType.String, r.InvoiceOrganID.Value)
                        If r.InvoiceName.Updated Then db.AddInParameter(dbcmd, "@InvoiceName", DbType.String, r.InvoiceName.Value)
                        If r.HeadOfficeFlag.Updated Then db.AddInParameter(dbcmd, "@HeadOfficeFlag", DbType.String, r.HeadOfficeFlag.Value)
                        If r.TaxCityCode.Updated Then db.AddInParameter(dbcmd, "@TaxCityCode", DbType.String, r.TaxCityCode.Value)
                        If r.TaxUnitNo.Updated Then db.AddInParameter(dbcmd, "@TaxUnitNo", DbType.String, r.TaxUnitNo.Value)
                        If r.LastChgComp.Updated Then db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, r.LastChgComp.Value)
                        If r.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, r.LastChgID.Value)
                        If r.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(r.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), r.LastChgDate.Value))
                        db.AddInParameter(dbcmd, "@PKCompID", DbType.String, IIf(r.LoadFromDataRow, r.CompID.OldValue, r.CompID.Value))
                        db.AddInParameter(dbcmd, "@PKInvoiceNo", DbType.String, IIf(r.LoadFromDataRow, r.InvoiceNo.OldValue, r.InvoiceNo.Value))

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

        Public Function Update(ByVal TaxParameterOrganRow As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0
            Dim strDot As String

            For Each r As Row In TaxParameterOrganRow
                strDot = String.Empty
                If strSQL.Length > 0 Then strSQL.Remove(0, strSQL.Length)
                strSQL.AppendLine("Update TaxParameterOrgan Set")
                For i As Integer = 0 To r.FieldNames.Length - 1
                    If Not r.IsIdentityField(r.FieldNames(i)) AndAlso r.IsUpdated(r.FieldNames(i)) AndAlso r.CreateUpdateSQL(r.FieldNames(i))
                        strSQL.AppendLine(String.Format("{0}{1} = @{1}", strDot, r.FieldNames(i)))
                        strDot = ","
                    End If
                Next
                If strDot = String.Empty Then Continue For
                strSQL.AppendLine("Where CompID = @PKCompID")
                strSQL.AppendLine("And InvoiceNo = @PKInvoiceNo")

                dbcmd = db.GetSqlStringCommand(strSQL.ToString())
                If r.CompID.Updated Then db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                If r.InvoiceNo.Updated Then db.AddInParameter(dbcmd, "@InvoiceNo", DbType.String, r.InvoiceNo.Value)
                If r.InvoiceOrganID.Updated Then db.AddInParameter(dbcmd, "@InvoiceOrganID", DbType.String, r.InvoiceOrganID.Value)
                If r.InvoiceName.Updated Then db.AddInParameter(dbcmd, "@InvoiceName", DbType.String, r.InvoiceName.Value)
                If r.HeadOfficeFlag.Updated Then db.AddInParameter(dbcmd, "@HeadOfficeFlag", DbType.String, r.HeadOfficeFlag.Value)
                If r.TaxCityCode.Updated Then db.AddInParameter(dbcmd, "@TaxCityCode", DbType.String, r.TaxCityCode.Value)
                If r.TaxUnitNo.Updated Then db.AddInParameter(dbcmd, "@TaxUnitNo", DbType.String, r.TaxUnitNo.Value)
                If r.LastChgComp.Updated Then db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, r.LastChgComp.Value)
                If r.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, r.LastChgID.Value)
                If r.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(r.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), r.LastChgDate.Value))
                db.AddInParameter(dbcmd, "@PKCompID", DbType.String, IIf(r.LoadFromDataRow, r.CompID.OldValue, r.CompID.Value))
                db.AddInParameter(dbcmd, "@PKInvoiceNo", DbType.String, IIf(r.LoadFromDataRow, r.InvoiceNo.OldValue, r.InvoiceNo.Value))

                intRowsAffected += db.ExecuteNonQuery(dbcmd, tran)
            Next
            Return intRowsAffected
        End Function

        Public Function IsDataExists(ByVal TaxParameterOrganRow As Row) As Boolean
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intCount As Integer = 0

            strSQL.AppendLine("Select Count(*) Cnt From TaxParameterOrgan")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And InvoiceNo = @InvoiceNo")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, TaxParameterOrganRow.CompID.Value)
            db.AddInParameter(dbcmd, "@InvoiceNo", DbType.String, TaxParameterOrganRow.InvoiceNo.Value)

            intCount = Convert.ToInt32(db.ExecuteScalar(dbcmd))
            Return IIf(intCount > 0, True, False)
        End Function

        Public Function IsDataExists(ByVal TaxParameterOrganRow As Row, ByVal tran As DbTransaction) As Boolean
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intCount As Integer = 0

            strSQL.AppendLine("Select Count(*) Cnt From TaxParameterOrgan")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And InvoiceNo = @InvoiceNo")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, TaxParameterOrganRow.CompID.Value)
            db.AddInParameter(dbcmd, "@InvoiceNo", DbType.String, TaxParameterOrganRow.InvoiceNo.Value)

            intCount = Convert.ToInt32(db.ExecuteScalar(dbcmd, tran))
            Return IIf(intCount > 0, True, False)
        End Function

        Public Function QuerybyWhere(WhereCondition As String) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From TaxParameterOrgan")
            If WhereCondition <> String.Empty Then strSQL.AppendLine(WhereCondition)

            Return db.ExecuteDataSet(CommandType.Text, strSQL.ToString())
        End Function

        Public Function Insert(ByVal TaxParameterOrganRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Insert into TaxParameterOrgan")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    CompID, InvoiceNo, InvoiceOrganID, InvoiceName, HeadOfficeFlag, TaxCityCode, TaxUnitNo,")
            strSQL.AppendLine("    LastChgComp, LastChgID, LastChgDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @CompID, @InvoiceNo, @InvoiceOrganID, @InvoiceName, @HeadOfficeFlag, @TaxCityCode, @TaxUnitNo,")
            strSQL.AppendLine("    @LastChgComp, @LastChgID, @LastChgDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, TaxParameterOrganRow.CompID.Value)
            db.AddInParameter(dbcmd, "@InvoiceNo", DbType.String, TaxParameterOrganRow.InvoiceNo.Value)
            db.AddInParameter(dbcmd, "@InvoiceOrganID", DbType.String, TaxParameterOrganRow.InvoiceOrganID.Value)
            db.AddInParameter(dbcmd, "@InvoiceName", DbType.String, TaxParameterOrganRow.InvoiceName.Value)
            db.AddInParameter(dbcmd, "@HeadOfficeFlag", DbType.String, TaxParameterOrganRow.HeadOfficeFlag.Value)
            db.AddInParameter(dbcmd, "@TaxCityCode", DbType.String, TaxParameterOrganRow.TaxCityCode.Value)
            db.AddInParameter(dbcmd, "@TaxUnitNo", DbType.String, TaxParameterOrganRow.TaxUnitNo.Value)
            db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, TaxParameterOrganRow.LastChgComp.Value)
            db.AddInParameter(dbcmd, "@LastChgID", DbType.String, TaxParameterOrganRow.LastChgID.Value)
            db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(TaxParameterOrganRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), TaxParameterOrganRow.LastChgDate.Value))

            Return db.ExecuteNonQuery(dbcmd)
        End Function

        Public Function Insert(ByVal TaxParameterOrganRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Insert into TaxParameterOrgan")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    CompID, InvoiceNo, InvoiceOrganID, InvoiceName, HeadOfficeFlag, TaxCityCode, TaxUnitNo,")
            strSQL.AppendLine("    LastChgComp, LastChgID, LastChgDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @CompID, @InvoiceNo, @InvoiceOrganID, @InvoiceName, @HeadOfficeFlag, @TaxCityCode, @TaxUnitNo,")
            strSQL.AppendLine("    @LastChgComp, @LastChgID, @LastChgDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, TaxParameterOrganRow.CompID.Value)
            db.AddInParameter(dbcmd, "@InvoiceNo", DbType.String, TaxParameterOrganRow.InvoiceNo.Value)
            db.AddInParameter(dbcmd, "@InvoiceOrganID", DbType.String, TaxParameterOrganRow.InvoiceOrganID.Value)
            db.AddInParameter(dbcmd, "@InvoiceName", DbType.String, TaxParameterOrganRow.InvoiceName.Value)
            db.AddInParameter(dbcmd, "@HeadOfficeFlag", DbType.String, TaxParameterOrganRow.HeadOfficeFlag.Value)
            db.AddInParameter(dbcmd, "@TaxCityCode", DbType.String, TaxParameterOrganRow.TaxCityCode.Value)
            db.AddInParameter(dbcmd, "@TaxUnitNo", DbType.String, TaxParameterOrganRow.TaxUnitNo.Value)
            db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, TaxParameterOrganRow.LastChgComp.Value)
            db.AddInParameter(dbcmd, "@LastChgID", DbType.String, TaxParameterOrganRow.LastChgID.Value)
            db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(TaxParameterOrganRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), TaxParameterOrganRow.LastChgDate.Value))

            Return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function Insert(ByVal TaxParameterOrganRow As Row()) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Insert into TaxParameterOrgan")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    CompID, InvoiceNo, InvoiceOrganID, InvoiceName, HeadOfficeFlag, TaxCityCode, TaxUnitNo,")
            strSQL.AppendLine("    LastChgComp, LastChgID, LastChgDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @CompID, @InvoiceNo, @InvoiceOrganID, @InvoiceName, @HeadOfficeFlag, @TaxCityCode, @TaxUnitNo,")
            strSQL.AppendLine("    @LastChgComp, @LastChgID, @LastChgDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            Using cn As DbConnection = db.CreateConnection()
                cn.Open()
                Dim tran As DbTransaction = cn.BeginTransaction()
                Dim inTrans As Boolean = True

                Try
                    For Each r As Row In TaxParameterOrganRow
                        dbcmd.Parameters.Clear()
                        db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                        db.AddInParameter(dbcmd, "@InvoiceNo", DbType.String, r.InvoiceNo.Value)
                        db.AddInParameter(dbcmd, "@InvoiceOrganID", DbType.String, r.InvoiceOrganID.Value)
                        db.AddInParameter(dbcmd, "@InvoiceName", DbType.String, r.InvoiceName.Value)
                        db.AddInParameter(dbcmd, "@HeadOfficeFlag", DbType.String, r.HeadOfficeFlag.Value)
                        db.AddInParameter(dbcmd, "@TaxCityCode", DbType.String, r.TaxCityCode.Value)
                        db.AddInParameter(dbcmd, "@TaxUnitNo", DbType.String, r.TaxUnitNo.Value)
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

        Public Function Insert(ByVal TaxParameterOrganRow As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Insert into TaxParameterOrgan")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    CompID, InvoiceNo, InvoiceOrganID, InvoiceName, HeadOfficeFlag, TaxCityCode, TaxUnitNo,")
            strSQL.AppendLine("    LastChgComp, LastChgID, LastChgDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @CompID, @InvoiceNo, @InvoiceOrganID, @InvoiceName, @HeadOfficeFlag, @TaxCityCode, @TaxUnitNo,")
            strSQL.AppendLine("    @LastChgComp, @LastChgID, @LastChgDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())

            For Each r As Row In TaxParameterOrganRow
                dbcmd.Parameters.Clear()
                db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                db.AddInParameter(dbcmd, "@InvoiceNo", DbType.String, r.InvoiceNo.Value)
                db.AddInParameter(dbcmd, "@InvoiceOrganID", DbType.String, r.InvoiceOrganID.Value)
                db.AddInParameter(dbcmd, "@InvoiceName", DbType.String, r.InvoiceName.Value)
                db.AddInParameter(dbcmd, "@HeadOfficeFlag", DbType.String, r.HeadOfficeFlag.Value)
                db.AddInParameter(dbcmd, "@TaxCityCode", DbType.String, r.TaxCityCode.Value)
                db.AddInParameter(dbcmd, "@TaxUnitNo", DbType.String, r.TaxUnitNo.Value)
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

