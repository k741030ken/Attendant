'****************************************************************
' Table:VisaCountry
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

Namespace beVisaCountry
    Public Class Table
        Private m_Rows As Rows = New Rows()
        Private m_Fields As String() = { "Country", "CountryName", "OfficeName", "Address", "LastChgComp", "LastChgID", "LastChgDate" }
        Private m_Types As System.Type() = { GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(Date) }
        Private m_PrimaryFields As String() = { "Country" }

        Public ReadOnly Property Rows() As beVisaCountry.Rows 
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
        Public Sub Transfer2Row(VisaCountryTable As DataTable)
            For Each dr As DataRow In VisaCountryTable.Rows
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

                dr(m_Rows(i).Country.FieldName) = m_Rows(i).Country.Value
                dr(m_Rows(i).CountryName.FieldName) = m_Rows(i).CountryName.Value
                dr(m_Rows(i).OfficeName.FieldName) = m_Rows(i).OfficeName.Value
                dr(m_Rows(i).Address.FieldName) = m_Rows(i).Address.Value
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

        Public Sub Add(VisaCountryRow As Row)
            m_Rows.Add(VisaCountryRow)
        End Sub

        Public Sub Remove(VisaCountryRow As Row)
            If m_Rows.IndexOf(VisaCountryRow) >= 0 Then
                m_Rows.Remove(VisaCountryRow)
            End If
        End Sub

        Public Sub Dispose()
            m_Rows.Clear()
        End Sub

    End Class

    Public Class Row

        Private FI_Country As Field(Of String) = new Field(Of String)("Country", true)
        Private FI_CountryName As Field(Of String) = new Field(Of String)("CountryName", true)
        Private FI_OfficeName As Field(Of String) = new Field(Of String)("OfficeName", true)
        Private FI_Address As Field(Of String) = new Field(Of String)("Address", true)
        Private FI_LastChgComp As Field(Of String) = new Field(Of String)("LastChgComp", true)
        Private FI_LastChgID As Field(Of String) = new Field(Of String)("LastChgID", true)
        Private FI_LastChgDate As Field(Of Date) = new Field(Of Date)("LastChgDate", true)
        Private m_FieldNames As String() = { "Country", "CountryName", "OfficeName", "Address", "LastChgComp", "LastChgID", "LastChgDate" }
        Private m_PrimaryFields As String() = { "Country" }
        Private m_IdentityFields As String() = {  }
        Private m_LoadFromDataRow As Boolean = False

        Private Function GetFieldValue(ByVal fieldName As String) As Object
            Select Case fieldName
                Case "Country"
                    Return FI_Country.Value
                Case "CountryName"
                    Return FI_CountryName.Value
                Case "OfficeName"
                    Return FI_OfficeName.Value
                Case "Address"
                    Return FI_Address.Value
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
                Case "Country"
                    FI_Country.SetValue(value)
                Case "CountryName"
                    FI_CountryName.SetValue(value)
                Case "OfficeName"
                    FI_OfficeName.SetValue(value)
                Case "Address"
                    FI_Address.SetValue(value)
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
                Case "Country"
                    return FI_Country.Updated
                Case "CountryName"
                    return FI_CountryName.Updated
                Case "OfficeName"
                    return FI_OfficeName.Updated
                Case "Address"
                    return FI_Address.Updated
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
                Case "Country"
                    return FI_Country.CreateUpdateSQL
                Case "CountryName"
                    return FI_CountryName.CreateUpdateSQL
                Case "OfficeName"
                    return FI_OfficeName.CreateUpdateSQL
                Case "Address"
                    return FI_Address.CreateUpdateSQL
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
            FI_Country.SetInitValue("")
            FI_CountryName.SetInitValue("")
            FI_OfficeName.SetInitValue("")
            FI_Address.SetInitValue("")
            FI_LastChgComp.SetInitValue("")
            FI_LastChgID.SetInitValue("")
            FI_LastChgDate.SetInitValue(Convert.ToDateTime("1900/01/01"))
        End Sub

        Public Sub New(ByVal dr As System.Data.DataRow)
            FI_Country.SetInitValue(dr("Country"))
            FI_CountryName.SetInitValue(dr("CountryName"))
            FI_OfficeName.SetInitValue(dr("OfficeName"))
            FI_Address.SetInitValue(dr("Address"))
            FI_LastChgComp.SetInitValue(dr("LastChgComp"))
            FI_LastChgID.SetInitValue(dr("LastChgID"))
            FI_LastChgDate.SetInitValue(dr("LastChgDate"))

            m_LoadFromDataRow = True
        End Sub

        Private Sub ClearUpdated()
            FI_Country.Updated = False
            FI_CountryName.Updated = False
            FI_OfficeName.Updated = False
            FI_Address.Updated = False
            FI_LastChgComp.Updated = False
            FI_LastChgID.Updated = False
            FI_LastChgDate.Updated = False
        End Sub

        Public ReadOnly Property Country As Field(Of String) 
            Get
                Return FI_Country
            End Get
        End Property

        Public ReadOnly Property CountryName As Field(Of String) 
            Get
                Return FI_CountryName
            End Get
        End Property

        Public ReadOnly Property OfficeName As Field(Of String) 
            Get
                Return FI_OfficeName
            End Get
        End Property

        Public ReadOnly Property Address As Field(Of String) 
            Get
                Return FI_Address
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
        Public Function DeleteRowByPrimaryKey(ByVal VisaCountryRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Delete From VisaCountry")
            strSQL.AppendLine("Where Country = @Country")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@Country", DbType.String, VisaCountryRow.Country.Value)

            return db.ExecuteNonQuery(dbcmd)
        End Function

        public Function DeleteRowByPrimaryKey(ByVal VisaCountryRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Delete From VisaCountry")
            strSQL.AppendLine("Where Country = @Country")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@Country", DbType.String, VisaCountryRow.Country.Value)

            return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function DeleteRowByPrimaryKey(ByVal VisaCountryRow As Row()) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Delete From VisaCountry")
            strSQL.AppendLine("Where Country = @Country")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            using cn As DbConnection = db.CreateConnection()
                cn.Open()
                Dim tran As DbTransaction = cn.BeginTransaction()
                Dim inTrans As Boolean = True

                Try
                    For Each r As Row In VisaCountryRow
                        dbcmd.Parameters.Clear()
                        db.AddInParameter(dbcmd, "@Country", DbType.String, r.Country.Value)

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

        Public Function DeleteRowByPrimaryKey(ByVal VisaCountryRow As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Delete From VisaCountry")
            strSQL.AppendLine("Where Country = @Country")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())

            For Each r As Row In VisaCountryRow
                dbcmd.Parameters.Clear()
                db.AddInParameter(dbcmd, "@Country", DbType.String, r.Country.Value)

                intRowsAffected += db.ExecuteNonQuery(dbcmd, tran)
            Next
            Return intRowsAffected
        End Function

        Public Function QueryByKey(ByVal VisaCountryRow As Row) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From VisaCountry")
            strSQL.AppendLine("Where Country = @Country")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@Country", DbType.String, VisaCountryRow.Country.Value)

            Return db.ExecuteDataSet(dbcmd)
        End Function

        Public Function QueryByKey(VisaCountryRow As Row, tran As DbTransaction) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From VisaCountry")
            strSQL.AppendLine("Where Country = @Country")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@Country", DbType.String, VisaCountryRow.Country.Value)

            Return db.ExecuteDataSet(dbcmd, tran)
        End Function

        Public Function Update(ByVal VisaCountryRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim strDot As String = String.Empty

            strSQL.AppendLine("Update VisaCountry Set")
            For i As Integer = 0 To VisaCountryRow.FieldNames.Length - 1
                If Not VisaCountryRow.IsIdentityField(VisaCountryRow.FieldNames(i)) AndAlso VisaCountryRow.IsUpdated(VisaCountryRow.FieldNames(i)) AndAlso VisaCountryRow.CreateUpdateSQL(VisaCountryRow.FieldNames(i)) Then
                    strSQL.AppendLine(string.Format("{0}{1} = @{1}", strDot, VisaCountryRow.FieldNames(i)))
                    strDot = ","
                End If
            Next
            If strDot = String.Empty Then Throw New Exception("未異動資料欄位！")
            strSQL.AppendLine("Where Country = @PKCountry")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            If VisaCountryRow.Country.Updated Then db.AddInParameter(dbcmd, "@Country", DbType.String, VisaCountryRow.Country.Value)
            If VisaCountryRow.CountryName.Updated Then db.AddInParameter(dbcmd, "@CountryName", DbType.String, VisaCountryRow.CountryName.Value)
            If VisaCountryRow.OfficeName.Updated Then db.AddInParameter(dbcmd, "@OfficeName", DbType.String, VisaCountryRow.OfficeName.Value)
            If VisaCountryRow.Address.Updated Then db.AddInParameter(dbcmd, "@Address", DbType.String, VisaCountryRow.Address.Value)
            If VisaCountryRow.LastChgComp.Updated Then db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, VisaCountryRow.LastChgComp.Value)
            If VisaCountryRow.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, VisaCountryRow.LastChgID.Value)
            If VisaCountryRow.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(VisaCountryRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), VisaCountryRow.LastChgDate.Value))
            db.AddInParameter(dbcmd, "@PKCountry", DbType.String, IIf(VisaCountryRow.LoadFromDataRow, VisaCountryRow.Country.OldValue, VisaCountryRow.Country.Value))

            Return db.ExecuteNonQuery(dbcmd)
        End Function

        Public Function Update(ByVal VisaCountryRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim strDot As String = String.Empty

            strSQL.AppendLine("Update VisaCountry Set")
            For i As Integer = 0 To VisaCountryRow.FieldNames.Length - 1
                If Not VisaCountryRow.IsIdentityField(VisaCountryRow.FieldNames(i)) AndAlso VisaCountryRow.IsUpdated(VisaCountryRow.FieldNames(i)) AndAlso VisaCountryRow.CreateUpdateSQL(VisaCountryRow.FieldNames(i)) Then
                    strSQL.AppendLine(string.Format("{0}{1} = @{1}", strDot, VisaCountryRow.FieldNames(i)))
                    strDot = ","
                End If
            Next 
            If strDot = String.Empty Then Throw New Exception("未異動資料欄位！")
            strSQL.AppendLine("Where Country = @PKCountry")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            If VisaCountryRow.Country.Updated Then db.AddInParameter(dbcmd, "@Country", DbType.String, VisaCountryRow.Country.Value)
            If VisaCountryRow.CountryName.Updated Then db.AddInParameter(dbcmd, "@CountryName", DbType.String, VisaCountryRow.CountryName.Value)
            If VisaCountryRow.OfficeName.Updated Then db.AddInParameter(dbcmd, "@OfficeName", DbType.String, VisaCountryRow.OfficeName.Value)
            If VisaCountryRow.Address.Updated Then db.AddInParameter(dbcmd, "@Address", DbType.String, VisaCountryRow.Address.Value)
            If VisaCountryRow.LastChgComp.Updated Then db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, VisaCountryRow.LastChgComp.Value)
            If VisaCountryRow.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, VisaCountryRow.LastChgID.Value)
            If VisaCountryRow.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(VisaCountryRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), VisaCountryRow.LastChgDate.Value))
            db.AddInParameter(dbcmd, "@PKCountry", DbType.String, IIf(VisaCountryRow.LoadFromDataRow, VisaCountryRow.Country.OldValue, VisaCountryRow.Country.Value))

            Return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function Update(ByVal VisaCountryRow As Row()) As Integer
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
                    For Each r As Row In VisaCountryRow
                        strDot = String.Empty
                        If strSQL.Length > 0 Then strSQL.Remove(0, strSQL.Length)
                        strSQL.AppendLine("Update VisaCountry Set")
                        For i As Integer = 0 To r.FieldNames.Length - 1
                            If Not r.IsIdentityField(r.FieldNames(i)) AndAlso r.IsUpdated(r.FieldNames(i)) AndAlso r.CreateUpdateSQL(r.FieldNames(i)) Then
                                strSQL.AppendLine(String.Format("{0}{1} = @{1}", strDot, r.FieldNames(i)))
                                strDot = ","
                            End If
                        Next
                        If strDot = String.Empty Then Continue For
                        strSQL.AppendLine("Where Country = @PKCountry")

                        dbcmd = db.GetSqlStringCommand(strSQL.ToString())
                        If r.Country.Updated Then db.AddInParameter(dbcmd, "@Country", DbType.String, r.Country.Value)
                        If r.CountryName.Updated Then db.AddInParameter(dbcmd, "@CountryName", DbType.String, r.CountryName.Value)
                        If r.OfficeName.Updated Then db.AddInParameter(dbcmd, "@OfficeName", DbType.String, r.OfficeName.Value)
                        If r.Address.Updated Then db.AddInParameter(dbcmd, "@Address", DbType.String, r.Address.Value)
                        If r.LastChgComp.Updated Then db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, r.LastChgComp.Value)
                        If r.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, r.LastChgID.Value)
                        If r.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(r.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), r.LastChgDate.Value))
                        db.AddInParameter(dbcmd, "@PKCountry", DbType.String, IIf(r.LoadFromDataRow, r.Country.OldValue, r.Country.Value))

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

        Public Function Update(ByVal VisaCountryRow As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0
            Dim strDot As String

            For Each r As Row In VisaCountryRow
                strDot = String.Empty
                If strSQL.Length > 0 Then strSQL.Remove(0, strSQL.Length)
                strSQL.AppendLine("Update VisaCountry Set")
                For i As Integer = 0 To r.FieldNames.Length - 1
                    If Not r.IsIdentityField(r.FieldNames(i)) AndAlso r.IsUpdated(r.FieldNames(i)) AndAlso r.CreateUpdateSQL(r.FieldNames(i))
                        strSQL.AppendLine(String.Format("{0}{1} = @{1}", strDot, r.FieldNames(i)))
                        strDot = ","
                    End If
                Next
                If strDot = String.Empty Then Continue For
                strSQL.AppendLine("Where Country = @PKCountry")

                dbcmd = db.GetSqlStringCommand(strSQL.ToString())
                If r.Country.Updated Then db.AddInParameter(dbcmd, "@Country", DbType.String, r.Country.Value)
                If r.CountryName.Updated Then db.AddInParameter(dbcmd, "@CountryName", DbType.String, r.CountryName.Value)
                If r.OfficeName.Updated Then db.AddInParameter(dbcmd, "@OfficeName", DbType.String, r.OfficeName.Value)
                If r.Address.Updated Then db.AddInParameter(dbcmd, "@Address", DbType.String, r.Address.Value)
                If r.LastChgComp.Updated Then db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, r.LastChgComp.Value)
                If r.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, r.LastChgID.Value)
                If r.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(r.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), r.LastChgDate.Value))
                db.AddInParameter(dbcmd, "@PKCountry", DbType.String, IIf(r.LoadFromDataRow, r.Country.OldValue, r.Country.Value))

                intRowsAffected += db.ExecuteNonQuery(dbcmd, tran)
            Next
            Return intRowsAffected
        End Function

        Public Function IsDataExists(ByVal VisaCountryRow As Row) As Boolean
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intCount As Integer = 0

            strSQL.AppendLine("Select Count(*) Cnt From VisaCountry")
            strSQL.AppendLine("Where Country = @Country")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@Country", DbType.String, VisaCountryRow.Country.Value)

            intCount = Convert.ToInt32(db.ExecuteScalar(dbcmd))
            Return IIf(intCount > 0, True, False)
        End Function

        Public Function IsDataExists(ByVal VisaCountryRow As Row, ByVal tran As DbTransaction) As Boolean
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intCount As Integer = 0

            strSQL.AppendLine("Select Count(*) Cnt From VisaCountry")
            strSQL.AppendLine("Where Country = @Country")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@Country", DbType.String, VisaCountryRow.Country.Value)

            intCount = Convert.ToInt32(db.ExecuteScalar(dbcmd, tran))
            Return IIf(intCount > 0, True, False)
        End Function

        Public Function QuerybyWhere(WhereCondition As String) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From VisaCountry")
            If WhereCondition <> String.Empty Then strSQL.AppendLine(WhereCondition)

            Return db.ExecuteDataSet(CommandType.Text, strSQL.ToString())
        End Function

        Public Function Insert(ByVal VisaCountryRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Insert into VisaCountry")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    Country, CountryName, OfficeName, Address, LastChgComp, LastChgID, LastChgDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @Country, @CountryName, @OfficeName, @Address, @LastChgComp, @LastChgID, @LastChgDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@Country", DbType.String, VisaCountryRow.Country.Value)
            db.AddInParameter(dbcmd, "@CountryName", DbType.String, VisaCountryRow.CountryName.Value)
            db.AddInParameter(dbcmd, "@OfficeName", DbType.String, VisaCountryRow.OfficeName.Value)
            db.AddInParameter(dbcmd, "@Address", DbType.String, VisaCountryRow.Address.Value)
            db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, VisaCountryRow.LastChgComp.Value)
            db.AddInParameter(dbcmd, "@LastChgID", DbType.String, VisaCountryRow.LastChgID.Value)
            db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(VisaCountryRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), VisaCountryRow.LastChgDate.Value))

            Return db.ExecuteNonQuery(dbcmd)
        End Function

        Public Function Insert(ByVal VisaCountryRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Insert into VisaCountry")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    Country, CountryName, OfficeName, Address, LastChgComp, LastChgID, LastChgDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @Country, @CountryName, @OfficeName, @Address, @LastChgComp, @LastChgID, @LastChgDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@Country", DbType.String, VisaCountryRow.Country.Value)
            db.AddInParameter(dbcmd, "@CountryName", DbType.String, VisaCountryRow.CountryName.Value)
            db.AddInParameter(dbcmd, "@OfficeName", DbType.String, VisaCountryRow.OfficeName.Value)
            db.AddInParameter(dbcmd, "@Address", DbType.String, VisaCountryRow.Address.Value)
            db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, VisaCountryRow.LastChgComp.Value)
            db.AddInParameter(dbcmd, "@LastChgID", DbType.String, VisaCountryRow.LastChgID.Value)
            db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(VisaCountryRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), VisaCountryRow.LastChgDate.Value))

            Return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function Insert(ByVal VisaCountryRow As Row()) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Insert into VisaCountry")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    Country, CountryName, OfficeName, Address, LastChgComp, LastChgID, LastChgDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @Country, @CountryName, @OfficeName, @Address, @LastChgComp, @LastChgID, @LastChgDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            Using cn As DbConnection = db.CreateConnection()
                cn.Open()
                Dim tran As DbTransaction = cn.BeginTransaction()
                Dim inTrans As Boolean = True

                Try
                    For Each r As Row In VisaCountryRow
                        dbcmd.Parameters.Clear()
                        db.AddInParameter(dbcmd, "@Country", DbType.String, r.Country.Value)
                        db.AddInParameter(dbcmd, "@CountryName", DbType.String, r.CountryName.Value)
                        db.AddInParameter(dbcmd, "@OfficeName", DbType.String, r.OfficeName.Value)
                        db.AddInParameter(dbcmd, "@Address", DbType.String, r.Address.Value)
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

        Public Function Insert(ByVal VisaCountryRow As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Insert into VisaCountry")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    Country, CountryName, OfficeName, Address, LastChgComp, LastChgID, LastChgDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @Country, @CountryName, @OfficeName, @Address, @LastChgComp, @LastChgID, @LastChgDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())

            For Each r As Row In VisaCountryRow
                dbcmd.Parameters.Clear()
                db.AddInParameter(dbcmd, "@Country", DbType.String, r.Country.Value)
                db.AddInParameter(dbcmd, "@CountryName", DbType.String, r.CountryName.Value)
                db.AddInParameter(dbcmd, "@OfficeName", DbType.String, r.OfficeName.Value)
                db.AddInParameter(dbcmd, "@Address", DbType.String, r.Address.Value)
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

