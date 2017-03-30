'****************************************************************
' Table:NaturalDisaster
' Created Date: 2016.12.02
'****************************************************************/
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports Microsoft.Practices.EnterpriseLibrary.Common
Imports Microsoft.Practices.EnterpriseLibrary.Data
Imports System.Data.Common
Imports System.Data

Namespace beNaturalDisaster
    Public Class Table
        Private m_Rows As Rows = New Rows()
        Private m_Fields As String() = { "CityCode", "AreaCode", "Type", "DisasterDate", "BeginTime", "EndTime", "LastChgComp", "LastChgID", "LastChgDate" }
        Private m_Types As System.Type() = { GetType(String), GetType(String), GetType(String), GetType(Date), GetType(String), GetType(String), GetType(String), GetType(String), GetType(Date) }
        Private m_PrimaryFields As String() = { "CityCode", "AreaCode", "Type", "DisasterDate" }

        Public ReadOnly Property Rows() As beNaturalDisaster.Rows 
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
        Public Sub Transfer2Row(NaturalDisasterTable As DataTable)
            For Each dr As DataRow In NaturalDisasterTable.Rows
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

                dr(m_Rows(i).CityCode.FieldName) = m_Rows(i).CityCode.Value
                dr(m_Rows(i).AreaCode.FieldName) = m_Rows(i).AreaCode.Value
                dr(m_Rows(i).Type.FieldName) = m_Rows(i).Type.Value
                dr(m_Rows(i).DisasterDate.FieldName) = m_Rows(i).DisasterDate.Value
                dr(m_Rows(i).BeginTime.FieldName) = m_Rows(i).BeginTime.Value
                dr(m_Rows(i).EndTime.FieldName) = m_Rows(i).EndTime.Value
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

        Public Sub Add(NaturalDisasterRow As Row)
            m_Rows.Add(NaturalDisasterRow)
        End Sub

        Public Sub Remove(NaturalDisasterRow As Row)
            If m_Rows.IndexOf(NaturalDisasterRow) >= 0 Then
                m_Rows.Remove(NaturalDisasterRow)
            End If
        End Sub

        Public Sub Dispose()
            m_Rows.Clear()
        End Sub

    End Class

    Public Class Row

        Private FI_CityCode As Field(Of String) = new Field(Of String)("CityCode", true)
        Private FI_AreaCode As Field(Of String) = new Field(Of String)("AreaCode", true)
        Private FI_Type As Field(Of String) = new Field(Of String)("Type", true)
        Private FI_DisasterDate As Field(Of Date) = new Field(Of Date)("DisasterDate", true)
        Private FI_BeginTime As Field(Of String) = new Field(Of String)("BeginTime", true)
        Private FI_EndTime As Field(Of String) = new Field(Of String)("EndTime", true)
        Private FI_LastChgComp As Field(Of String) = new Field(Of String)("LastChgComp", true)
        Private FI_LastChgID As Field(Of String) = new Field(Of String)("LastChgID", true)
        Private FI_LastChgDate As Field(Of Date) = new Field(Of Date)("LastChgDate", true)
        Private m_FieldNames As String() = { "CityCode", "AreaCode", "Type", "DisasterDate", "BeginTime", "EndTime", "LastChgComp", "LastChgID", "LastChgDate" }
        Private m_PrimaryFields As String() = { "CityCode", "AreaCode", "Type", "DisasterDate" }
        Private m_IdentityFields As String() = {  }
        Private m_LoadFromDataRow As Boolean = False

        Private Function GetFieldValue(ByVal fieldName As String) As Object
            Select Case fieldName
                Case "CityCode"
                    Return FI_CityCode.Value
                Case "AreaCode"
                    Return FI_AreaCode.Value
                Case "Type"
                    Return FI_Type.Value
                Case "DisasterDate"
                    Return FI_DisasterDate.Value
                Case "BeginTime"
                    Return FI_BeginTime.Value
                Case "EndTime"
                    Return FI_EndTime.Value
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
                Case "CityCode"
                    FI_CityCode.SetValue(value)
                Case "AreaCode"
                    FI_AreaCode.SetValue(value)
                Case "Type"
                    FI_Type.SetValue(value)
                Case "DisasterDate"
                    FI_DisasterDate.SetValue(value)
                Case "BeginTime"
                    FI_BeginTime.SetValue(value)
                Case "EndTime"
                    FI_EndTime.SetValue(value)
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
                Case "CityCode"
                    return FI_CityCode.Updated
                Case "AreaCode"
                    return FI_AreaCode.Updated
                Case "Type"
                    return FI_Type.Updated
                Case "DisasterDate"
                    return FI_DisasterDate.Updated
                Case "BeginTime"
                    return FI_BeginTime.Updated
                Case "EndTime"
                    return FI_EndTime.Updated
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
                Case "CityCode"
                    return FI_CityCode.CreateUpdateSQL
                Case "AreaCode"
                    return FI_AreaCode.CreateUpdateSQL
                Case "Type"
                    return FI_Type.CreateUpdateSQL
                Case "DisasterDate"
                    return FI_DisasterDate.CreateUpdateSQL
                Case "BeginTime"
                    return FI_BeginTime.CreateUpdateSQL
                Case "EndTime"
                    return FI_EndTime.CreateUpdateSQL
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
            FI_CityCode.SetInitValue("")
            FI_AreaCode.SetInitValue("")
            FI_Type.SetInitValue("")
            FI_DisasterDate.SetInitValue(Convert.ToDateTime("1900/01/01"))
            FI_BeginTime.SetInitValue("")
            FI_EndTime.SetInitValue("")
            FI_LastChgComp.SetInitValue("")
            FI_LastChgID.SetInitValue("")
            FI_LastChgDate.SetInitValue(Convert.ToDateTime("1900/01/01"))
        End Sub

        Public Sub New(ByVal dr As System.Data.DataRow)
            FI_CityCode.SetInitValue(dr("CityCode"))
            FI_AreaCode.SetInitValue(dr("AreaCode"))
            FI_Type.SetInitValue(dr("Type"))
            FI_DisasterDate.SetInitValue(dr("DisasterDate"))
            FI_BeginTime.SetInitValue(dr("BeginTime"))
            FI_EndTime.SetInitValue(dr("EndTime"))
            FI_LastChgComp.SetInitValue(dr("LastChgComp"))
            FI_LastChgID.SetInitValue(dr("LastChgID"))
            FI_LastChgDate.SetInitValue(dr("LastChgDate"))

            m_LoadFromDataRow = True
        End Sub

        Private Sub ClearUpdated()
            FI_CityCode.Updated = False
            FI_AreaCode.Updated = False
            FI_Type.Updated = False
            FI_DisasterDate.Updated = False
            FI_BeginTime.Updated = False
            FI_EndTime.Updated = False
            FI_LastChgComp.Updated = False
            FI_LastChgID.Updated = False
            FI_LastChgDate.Updated = False
        End Sub

        Public ReadOnly Property CityCode As Field(Of String) 
            Get
                Return FI_CityCode
            End Get
        End Property

        Public ReadOnly Property AreaCode As Field(Of String) 
            Get
                Return FI_AreaCode
            End Get
        End Property

        Public ReadOnly Property Type As Field(Of String) 
            Get
                Return FI_Type
            End Get
        End Property

        Public ReadOnly Property DisasterDate As Field(Of Date) 
            Get
                Return FI_DisasterDate
            End Get
        End Property

        Public ReadOnly Property BeginTime As Field(Of String) 
            Get
                Return FI_BeginTime
            End Get
        End Property

        Public ReadOnly Property EndTime As Field(Of String) 
            Get
                Return FI_EndTime
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
        Public Function DeleteRowByPrimaryKey(ByVal NaturalDisasterRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Delete From NaturalDisaster")
            strSQL.AppendLine("Where CityCode = @CityCode")
            strSQL.AppendLine("And AreaCode = @AreaCode")
            strSQL.AppendLine("And Type = @Type")
            strSQL.AppendLine("And DisasterDate = @DisasterDate")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CityCode", DbType.String, NaturalDisasterRow.CityCode.Value)
            db.AddInParameter(dbcmd, "@AreaCode", DbType.String, NaturalDisasterRow.AreaCode.Value)
            db.AddInParameter(dbcmd, "@Type", DbType.String, NaturalDisasterRow.Type.Value)
            db.AddInParameter(dbcmd, "@DisasterDate", DbType.Date, NaturalDisasterRow.DisasterDate.Value)

            return db.ExecuteNonQuery(dbcmd)
        End Function

        public Function DeleteRowByPrimaryKey(ByVal NaturalDisasterRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Delete From NaturalDisaster")
            strSQL.AppendLine("Where CityCode = @CityCode")
            strSQL.AppendLine("And AreaCode = @AreaCode")
            strSQL.AppendLine("And Type = @Type")
            strSQL.AppendLine("And DisasterDate = @DisasterDate")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CityCode", DbType.String, NaturalDisasterRow.CityCode.Value)
            db.AddInParameter(dbcmd, "@AreaCode", DbType.String, NaturalDisasterRow.AreaCode.Value)
            db.AddInParameter(dbcmd, "@Type", DbType.String, NaturalDisasterRow.Type.Value)
            db.AddInParameter(dbcmd, "@DisasterDate", DbType.Date, NaturalDisasterRow.DisasterDate.Value)

            return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function DeleteRowByPrimaryKey(ByVal NaturalDisasterRow As Row()) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Delete From NaturalDisaster")
            strSQL.AppendLine("Where CityCode = @CityCode")
            strSQL.AppendLine("And AreaCode = @AreaCode")
            strSQL.AppendLine("And Type = @Type")
            strSQL.AppendLine("And DisasterDate = @DisasterDate")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            using cn As DbConnection = db.CreateConnection()
                cn.Open()
                Dim tran As DbTransaction = cn.BeginTransaction()
                Dim inTrans As Boolean = True

                Try
                    For Each r As Row In NaturalDisasterRow
                        dbcmd.Parameters.Clear()
                        db.AddInParameter(dbcmd, "@CityCode", DbType.String, r.CityCode.Value)
                        db.AddInParameter(dbcmd, "@AreaCode", DbType.String, r.AreaCode.Value)
                        db.AddInParameter(dbcmd, "@Type", DbType.String, r.Type.Value)
                        db.AddInParameter(dbcmd, "@DisasterDate", DbType.Date, r.DisasterDate.Value)

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

        Public Function DeleteRowByPrimaryKey(ByVal NaturalDisasterRow As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Delete From NaturalDisaster")
            strSQL.AppendLine("Where CityCode = @CityCode")
            strSQL.AppendLine("And AreaCode = @AreaCode")
            strSQL.AppendLine("And Type = @Type")
            strSQL.AppendLine("And DisasterDate = @DisasterDate")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())

            For Each r As Row In NaturalDisasterRow
                dbcmd.Parameters.Clear()
                db.AddInParameter(dbcmd, "@CityCode", DbType.String, r.CityCode.Value)
                db.AddInParameter(dbcmd, "@AreaCode", DbType.String, r.AreaCode.Value)
                db.AddInParameter(dbcmd, "@Type", DbType.String, r.Type.Value)
                db.AddInParameter(dbcmd, "@DisasterDate", DbType.Date, r.DisasterDate.Value)

                intRowsAffected += db.ExecuteNonQuery(dbcmd, tran)
            Next
            Return intRowsAffected
        End Function

        Public Function QueryByKey(ByVal NaturalDisasterRow As Row) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From NaturalDisaster")
            strSQL.AppendLine("Where CityCode = @CityCode")
            strSQL.AppendLine("And AreaCode = @AreaCode")
            strSQL.AppendLine("And Type = @Type")
            strSQL.AppendLine("And DisasterDate = @DisasterDate")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CityCode", DbType.String, NaturalDisasterRow.CityCode.Value)
            db.AddInParameter(dbcmd, "@AreaCode", DbType.String, NaturalDisasterRow.AreaCode.Value)
            db.AddInParameter(dbcmd, "@Type", DbType.String, NaturalDisasterRow.Type.Value)
            db.AddInParameter(dbcmd, "@DisasterDate", DbType.Date, NaturalDisasterRow.DisasterDate.Value)

            Return db.ExecuteDataSet(dbcmd)
        End Function

        Public Function QueryByKey(NaturalDisasterRow As Row, tran As DbTransaction) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From NaturalDisaster")
            strSQL.AppendLine("Where CityCode = @CityCode")
            strSQL.AppendLine("And AreaCode = @AreaCode")
            strSQL.AppendLine("And Type = @Type")
            strSQL.AppendLine("And DisasterDate = @DisasterDate")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CityCode", DbType.String, NaturalDisasterRow.CityCode.Value)
            db.AddInParameter(dbcmd, "@AreaCode", DbType.String, NaturalDisasterRow.AreaCode.Value)
            db.AddInParameter(dbcmd, "@Type", DbType.String, NaturalDisasterRow.Type.Value)
            db.AddInParameter(dbcmd, "@DisasterDate", DbType.Date, NaturalDisasterRow.DisasterDate.Value)

            Return db.ExecuteDataSet(dbcmd, tran)
        End Function

        Public Function Update(ByVal NaturalDisasterRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim strDot As String = String.Empty

            strSQL.AppendLine("Update NaturalDisaster Set")
            For i As Integer = 0 To NaturalDisasterRow.FieldNames.Length - 1
                If Not NaturalDisasterRow.IsIdentityField(NaturalDisasterRow.FieldNames(i)) AndAlso NaturalDisasterRow.IsUpdated(NaturalDisasterRow.FieldNames(i)) AndAlso NaturalDisasterRow.CreateUpdateSQL(NaturalDisasterRow.FieldNames(i)) Then
                    strSQL.AppendLine(string.Format("{0}{1} = @{1}", strDot, NaturalDisasterRow.FieldNames(i)))
                    strDot = ","
                End If
            Next
            If strDot = String.Empty Then Throw New Exception("未異動資料欄位！")
            strSQL.AppendLine("Where CityCode = @PKCityCode")
            strSQL.AppendLine("And AreaCode = @PKAreaCode")
            strSQL.AppendLine("And Type = @PKType")
            strSQL.AppendLine("And DisasterDate = @PKDisasterDate")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            If NaturalDisasterRow.CityCode.Updated Then db.AddInParameter(dbcmd, "@CityCode", DbType.String, NaturalDisasterRow.CityCode.Value)
            If NaturalDisasterRow.AreaCode.Updated Then db.AddInParameter(dbcmd, "@AreaCode", DbType.String, NaturalDisasterRow.AreaCode.Value)
            If NaturalDisasterRow.Type.Updated Then db.AddInParameter(dbcmd, "@Type", DbType.String, NaturalDisasterRow.Type.Value)
            If NaturalDisasterRow.DisasterDate.Updated Then db.AddInParameter(dbcmd, "@DisasterDate", DbType.Date, IIf(IsDateTimeNull(NaturalDisasterRow.DisasterDate.Value), Convert.ToDateTime("1900/1/1"), NaturalDisasterRow.DisasterDate.Value))
            If NaturalDisasterRow.BeginTime.Updated Then db.AddInParameter(dbcmd, "@BeginTime", DbType.String, NaturalDisasterRow.BeginTime.Value)
            If NaturalDisasterRow.EndTime.Updated Then db.AddInParameter(dbcmd, "@EndTime", DbType.String, NaturalDisasterRow.EndTime.Value)
            If NaturalDisasterRow.LastChgComp.Updated Then db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, NaturalDisasterRow.LastChgComp.Value)
            If NaturalDisasterRow.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, NaturalDisasterRow.LastChgID.Value)
            If NaturalDisasterRow.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(NaturalDisasterRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), NaturalDisasterRow.LastChgDate.Value))
            db.AddInParameter(dbcmd, "@PKCityCode", DbType.String, IIf(NaturalDisasterRow.LoadFromDataRow, NaturalDisasterRow.CityCode.OldValue, NaturalDisasterRow.CityCode.Value))
            db.AddInParameter(dbcmd, "@PKAreaCode", DbType.String, IIf(NaturalDisasterRow.LoadFromDataRow, NaturalDisasterRow.AreaCode.OldValue, NaturalDisasterRow.AreaCode.Value))
            db.AddInParameter(dbcmd, "@PKType", DbType.String, IIf(NaturalDisasterRow.LoadFromDataRow, NaturalDisasterRow.Type.OldValue, NaturalDisasterRow.Type.Value))
            db.AddInParameter(dbcmd, "@PKDisasterDate", DbType.Date, IIf(NaturalDisasterRow.LoadFromDataRow, NaturalDisasterRow.DisasterDate.OldValue, NaturalDisasterRow.DisasterDate.Value))

            Return db.ExecuteNonQuery(dbcmd)
        End Function

        Public Function Update(ByVal NaturalDisasterRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim strDot As String = String.Empty

            strSQL.AppendLine("Update NaturalDisaster Set")
            For i As Integer = 0 To NaturalDisasterRow.FieldNames.Length - 1
                If Not NaturalDisasterRow.IsIdentityField(NaturalDisasterRow.FieldNames(i)) AndAlso NaturalDisasterRow.IsUpdated(NaturalDisasterRow.FieldNames(i)) AndAlso NaturalDisasterRow.CreateUpdateSQL(NaturalDisasterRow.FieldNames(i)) Then
                    strSQL.AppendLine(string.Format("{0}{1} = @{1}", strDot, NaturalDisasterRow.FieldNames(i)))
                    strDot = ","
                End If
            Next 
            If strDot = String.Empty Then Throw New Exception("未異動資料欄位！")
            strSQL.AppendLine("Where CityCode = @PKCityCode")
            strSQL.AppendLine("And AreaCode = @PKAreaCode")
            strSQL.AppendLine("And Type = @PKType")
            strSQL.AppendLine("And DisasterDate = @PKDisasterDate")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            If NaturalDisasterRow.CityCode.Updated Then db.AddInParameter(dbcmd, "@CityCode", DbType.String, NaturalDisasterRow.CityCode.Value)
            If NaturalDisasterRow.AreaCode.Updated Then db.AddInParameter(dbcmd, "@AreaCode", DbType.String, NaturalDisasterRow.AreaCode.Value)
            If NaturalDisasterRow.Type.Updated Then db.AddInParameter(dbcmd, "@Type", DbType.String, NaturalDisasterRow.Type.Value)
            If NaturalDisasterRow.DisasterDate.Updated Then db.AddInParameter(dbcmd, "@DisasterDate", DbType.Date, IIf(IsDateTimeNull(NaturalDisasterRow.DisasterDate.Value), Convert.ToDateTime("1900/1/1"), NaturalDisasterRow.DisasterDate.Value))
            If NaturalDisasterRow.BeginTime.Updated Then db.AddInParameter(dbcmd, "@BeginTime", DbType.String, NaturalDisasterRow.BeginTime.Value)
            If NaturalDisasterRow.EndTime.Updated Then db.AddInParameter(dbcmd, "@EndTime", DbType.String, NaturalDisasterRow.EndTime.Value)
            If NaturalDisasterRow.LastChgComp.Updated Then db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, NaturalDisasterRow.LastChgComp.Value)
            If NaturalDisasterRow.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, NaturalDisasterRow.LastChgID.Value)
            If NaturalDisasterRow.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(NaturalDisasterRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), NaturalDisasterRow.LastChgDate.Value))
            db.AddInParameter(dbcmd, "@PKCityCode", DbType.String, IIf(NaturalDisasterRow.LoadFromDataRow, NaturalDisasterRow.CityCode.OldValue, NaturalDisasterRow.CityCode.Value))
            db.AddInParameter(dbcmd, "@PKAreaCode", DbType.String, IIf(NaturalDisasterRow.LoadFromDataRow, NaturalDisasterRow.AreaCode.OldValue, NaturalDisasterRow.AreaCode.Value))
            db.AddInParameter(dbcmd, "@PKType", DbType.String, IIf(NaturalDisasterRow.LoadFromDataRow, NaturalDisasterRow.Type.OldValue, NaturalDisasterRow.Type.Value))
            db.AddInParameter(dbcmd, "@PKDisasterDate", DbType.Date, IIf(NaturalDisasterRow.LoadFromDataRow, NaturalDisasterRow.DisasterDate.OldValue, NaturalDisasterRow.DisasterDate.Value))

            Return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function Update(ByVal NaturalDisasterRow As Row()) As Integer
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
                    For Each r As Row In NaturalDisasterRow
                        strDot = String.Empty
                        If strSQL.Length > 0 Then strSQL.Remove(0, strSQL.Length)
                        strSQL.AppendLine("Update NaturalDisaster Set")
                        For i As Integer = 0 To r.FieldNames.Length - 1
                            If Not r.IsIdentityField(r.FieldNames(i)) AndAlso r.IsUpdated(r.FieldNames(i)) AndAlso r.CreateUpdateSQL(r.FieldNames(i)) Then
                                strSQL.AppendLine(String.Format("{0}{1} = @{1}", strDot, r.FieldNames(i)))
                                strDot = ","
                            End If
                        Next
                        If strDot = String.Empty Then Continue For
                        strSQL.AppendLine("Where CityCode = @PKCityCode")
                        strSQL.AppendLine("And AreaCode = @PKAreaCode")
                        strSQL.AppendLine("And Type = @PKType")
                        strSQL.AppendLine("And DisasterDate = @PKDisasterDate")

                        dbcmd = db.GetSqlStringCommand(strSQL.ToString())
                        If r.CityCode.Updated Then db.AddInParameter(dbcmd, "@CityCode", DbType.String, r.CityCode.Value)
                        If r.AreaCode.Updated Then db.AddInParameter(dbcmd, "@AreaCode", DbType.String, r.AreaCode.Value)
                        If r.Type.Updated Then db.AddInParameter(dbcmd, "@Type", DbType.String, r.Type.Value)
                        If r.DisasterDate.Updated Then db.AddInParameter(dbcmd, "@DisasterDate", DbType.Date, IIf(IsDateTimeNull(r.DisasterDate.Value), Convert.ToDateTime("1900/1/1"), r.DisasterDate.Value))
                        If r.BeginTime.Updated Then db.AddInParameter(dbcmd, "@BeginTime", DbType.String, r.BeginTime.Value)
                        If r.EndTime.Updated Then db.AddInParameter(dbcmd, "@EndTime", DbType.String, r.EndTime.Value)
                        If r.LastChgComp.Updated Then db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, r.LastChgComp.Value)
                        If r.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, r.LastChgID.Value)
                        If r.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(r.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), r.LastChgDate.Value))
                        db.AddInParameter(dbcmd, "@PKCityCode", DbType.String, IIf(r.LoadFromDataRow, r.CityCode.OldValue, r.CityCode.Value))
                        db.AddInParameter(dbcmd, "@PKAreaCode", DbType.String, IIf(r.LoadFromDataRow, r.AreaCode.OldValue, r.AreaCode.Value))
                        db.AddInParameter(dbcmd, "@PKType", DbType.String, IIf(r.LoadFromDataRow, r.Type.OldValue, r.Type.Value))
                        db.AddInParameter(dbcmd, "@PKDisasterDate", DbType.Date, IIf(r.LoadFromDataRow, r.DisasterDate.OldValue, r.DisasterDate.Value))

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

        Public Function Update(ByVal NaturalDisasterRow As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0
            Dim strDot As String

            For Each r As Row In NaturalDisasterRow
                strDot = String.Empty
                If strSQL.Length > 0 Then strSQL.Remove(0, strSQL.Length)
                strSQL.AppendLine("Update NaturalDisaster Set")
                For i As Integer = 0 To r.FieldNames.Length - 1
                    If Not r.IsIdentityField(r.FieldNames(i)) AndAlso r.IsUpdated(r.FieldNames(i)) AndAlso r.CreateUpdateSQL(r.FieldNames(i))
                        strSQL.AppendLine(String.Format("{0}{1} = @{1}", strDot, r.FieldNames(i)))
                        strDot = ","
                    End If
                Next
                If strDot = String.Empty Then Continue For
                strSQL.AppendLine("Where CityCode = @PKCityCode")
                strSQL.AppendLine("And AreaCode = @PKAreaCode")
                strSQL.AppendLine("And Type = @PKType")
                strSQL.AppendLine("And DisasterDate = @PKDisasterDate")

                dbcmd = db.GetSqlStringCommand(strSQL.ToString())
                If r.CityCode.Updated Then db.AddInParameter(dbcmd, "@CityCode", DbType.String, r.CityCode.Value)
                If r.AreaCode.Updated Then db.AddInParameter(dbcmd, "@AreaCode", DbType.String, r.AreaCode.Value)
                If r.Type.Updated Then db.AddInParameter(dbcmd, "@Type", DbType.String, r.Type.Value)
                If r.DisasterDate.Updated Then db.AddInParameter(dbcmd, "@DisasterDate", DbType.Date, IIf(IsDateTimeNull(r.DisasterDate.Value), Convert.ToDateTime("1900/1/1"), r.DisasterDate.Value))
                If r.BeginTime.Updated Then db.AddInParameter(dbcmd, "@BeginTime", DbType.String, r.BeginTime.Value)
                If r.EndTime.Updated Then db.AddInParameter(dbcmd, "@EndTime", DbType.String, r.EndTime.Value)
                If r.LastChgComp.Updated Then db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, r.LastChgComp.Value)
                If r.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, r.LastChgID.Value)
                If r.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(r.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), r.LastChgDate.Value))
                db.AddInParameter(dbcmd, "@PKCityCode", DbType.String, IIf(r.LoadFromDataRow, r.CityCode.OldValue, r.CityCode.Value))
                db.AddInParameter(dbcmd, "@PKAreaCode", DbType.String, IIf(r.LoadFromDataRow, r.AreaCode.OldValue, r.AreaCode.Value))
                db.AddInParameter(dbcmd, "@PKType", DbType.String, IIf(r.LoadFromDataRow, r.Type.OldValue, r.Type.Value))
                db.AddInParameter(dbcmd, "@PKDisasterDate", DbType.Date, IIf(r.LoadFromDataRow, r.DisasterDate.OldValue, r.DisasterDate.Value))

                intRowsAffected += db.ExecuteNonQuery(dbcmd, tran)
            Next
            Return intRowsAffected
        End Function

        Public Function IsDataExists(ByVal NaturalDisasterRow As Row) As Boolean
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intCount As Integer = 0

            strSQL.AppendLine("Select Count(*) Cnt From NaturalDisaster")
            strSQL.AppendLine("Where CityCode = @CityCode")
            strSQL.AppendLine("And AreaCode = @AreaCode")
            strSQL.AppendLine("And Type = @Type")
            strSQL.AppendLine("And DisasterDate = @DisasterDate")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CityCode", DbType.String, NaturalDisasterRow.CityCode.Value)
            db.AddInParameter(dbcmd, "@AreaCode", DbType.String, NaturalDisasterRow.AreaCode.Value)
            db.AddInParameter(dbcmd, "@Type", DbType.String, NaturalDisasterRow.Type.Value)
            db.AddInParameter(dbcmd, "@DisasterDate", DbType.Date, NaturalDisasterRow.DisasterDate.Value)

            intCount = Convert.ToInt32(db.ExecuteScalar(dbcmd))
            Return IIf(intCount > 0, True, False)
        End Function

        Public Function IsDataExists(ByVal NaturalDisasterRow As Row, ByVal tran As DbTransaction) As Boolean
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intCount As Integer = 0

            strSQL.AppendLine("Select Count(*) Cnt From NaturalDisaster")
            strSQL.AppendLine("Where CityCode = @CityCode")
            strSQL.AppendLine("And AreaCode = @AreaCode")
            strSQL.AppendLine("And Type = @Type")
            strSQL.AppendLine("And DisasterDate = @DisasterDate")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CityCode", DbType.String, NaturalDisasterRow.CityCode.Value)
            db.AddInParameter(dbcmd, "@AreaCode", DbType.String, NaturalDisasterRow.AreaCode.Value)
            db.AddInParameter(dbcmd, "@Type", DbType.String, NaturalDisasterRow.Type.Value)
            db.AddInParameter(dbcmd, "@DisasterDate", DbType.Date, NaturalDisasterRow.DisasterDate.Value)

            intCount = Convert.ToInt32(db.ExecuteScalar(dbcmd, tran))
            Return IIf(intCount > 0, True, False)
        End Function

        Public Function QuerybyWhere(WhereCondition As String) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From NaturalDisaster")
            If WhereCondition <> String.Empty Then strSQL.AppendLine(WhereCondition)

            Return db.ExecuteDataSet(CommandType.Text, strSQL.ToString())
        End Function

        Public Function Insert(ByVal NaturalDisasterRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Insert into NaturalDisaster")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    CityCode, AreaCode, Type, DisasterDate, BeginTime, EndTime, LastChgComp, LastChgID,")
            strSQL.AppendLine("    LastChgDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @CityCode, @AreaCode, @Type, @DisasterDate, @BeginTime, @EndTime, @LastChgComp, @LastChgID,")
            strSQL.AppendLine("    @LastChgDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CityCode", DbType.String, NaturalDisasterRow.CityCode.Value)
            db.AddInParameter(dbcmd, "@AreaCode", DbType.String, NaturalDisasterRow.AreaCode.Value)
            db.AddInParameter(dbcmd, "@Type", DbType.String, NaturalDisasterRow.Type.Value)
            db.AddInParameter(dbcmd, "@DisasterDate", DbType.Date, IIf(IsDateTimeNull(NaturalDisasterRow.DisasterDate.Value), Convert.ToDateTime("1900/1/1"), NaturalDisasterRow.DisasterDate.Value))
            db.AddInParameter(dbcmd, "@BeginTime", DbType.String, NaturalDisasterRow.BeginTime.Value)
            db.AddInParameter(dbcmd, "@EndTime", DbType.String, NaturalDisasterRow.EndTime.Value)
            db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, NaturalDisasterRow.LastChgComp.Value)
            db.AddInParameter(dbcmd, "@LastChgID", DbType.String, NaturalDisasterRow.LastChgID.Value)
            db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(NaturalDisasterRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), NaturalDisasterRow.LastChgDate.Value))

            Return db.ExecuteNonQuery(dbcmd)
        End Function

        Public Function Insert(ByVal NaturalDisasterRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Insert into NaturalDisaster")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    CityCode, AreaCode, Type, DisasterDate, BeginTime, EndTime, LastChgComp, LastChgID,")
            strSQL.AppendLine("    LastChgDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @CityCode, @AreaCode, @Type, @DisasterDate, @BeginTime, @EndTime, @LastChgComp, @LastChgID,")
            strSQL.AppendLine("    @LastChgDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CityCode", DbType.String, NaturalDisasterRow.CityCode.Value)
            db.AddInParameter(dbcmd, "@AreaCode", DbType.String, NaturalDisasterRow.AreaCode.Value)
            db.AddInParameter(dbcmd, "@Type", DbType.String, NaturalDisasterRow.Type.Value)
            db.AddInParameter(dbcmd, "@DisasterDate", DbType.Date, IIf(IsDateTimeNull(NaturalDisasterRow.DisasterDate.Value), Convert.ToDateTime("1900/1/1"), NaturalDisasterRow.DisasterDate.Value))
            db.AddInParameter(dbcmd, "@BeginTime", DbType.String, NaturalDisasterRow.BeginTime.Value)
            db.AddInParameter(dbcmd, "@EndTime", DbType.String, NaturalDisasterRow.EndTime.Value)
            db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, NaturalDisasterRow.LastChgComp.Value)
            db.AddInParameter(dbcmd, "@LastChgID", DbType.String, NaturalDisasterRow.LastChgID.Value)
            db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(NaturalDisasterRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), NaturalDisasterRow.LastChgDate.Value))

            Return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function Insert(ByVal NaturalDisasterRow As Row()) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Insert into NaturalDisaster")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    CityCode, AreaCode, Type, DisasterDate, BeginTime, EndTime, LastChgComp, LastChgID,")
            strSQL.AppendLine("    LastChgDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @CityCode, @AreaCode, @Type, @DisasterDate, @BeginTime, @EndTime, @LastChgComp, @LastChgID,")
            strSQL.AppendLine("    @LastChgDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            Using cn As DbConnection = db.CreateConnection()
                cn.Open()
                Dim tran As DbTransaction = cn.BeginTransaction()
                Dim inTrans As Boolean = True

                Try
                    For Each r As Row In NaturalDisasterRow
                        dbcmd.Parameters.Clear()
                        db.AddInParameter(dbcmd, "@CityCode", DbType.String, r.CityCode.Value)
                        db.AddInParameter(dbcmd, "@AreaCode", DbType.String, r.AreaCode.Value)
                        db.AddInParameter(dbcmd, "@Type", DbType.String, r.Type.Value)
                        db.AddInParameter(dbcmd, "@DisasterDate", DbType.Date, IIf(IsDateTimeNull(r.DisasterDate.Value), Convert.ToDateTime("1900/1/1"), r.DisasterDate.Value))
                        db.AddInParameter(dbcmd, "@BeginTime", DbType.String, r.BeginTime.Value)
                        db.AddInParameter(dbcmd, "@EndTime", DbType.String, r.EndTime.Value)
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

        Public Function Insert(ByVal NaturalDisasterRow As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Insert into NaturalDisaster")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    CityCode, AreaCode, Type, DisasterDate, BeginTime, EndTime, LastChgComp, LastChgID,")
            strSQL.AppendLine("    LastChgDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @CityCode, @AreaCode, @Type, @DisasterDate, @BeginTime, @EndTime, @LastChgComp, @LastChgID,")
            strSQL.AppendLine("    @LastChgDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())

            For Each r As Row In NaturalDisasterRow
                dbcmd.Parameters.Clear()
                db.AddInParameter(dbcmd, "@CityCode", DbType.String, r.CityCode.Value)
                db.AddInParameter(dbcmd, "@AreaCode", DbType.String, r.AreaCode.Value)
                db.AddInParameter(dbcmd, "@Type", DbType.String, r.Type.Value)
                db.AddInParameter(dbcmd, "@DisasterDate", DbType.Date, IIf(IsDateTimeNull(r.DisasterDate.Value), Convert.ToDateTime("1900/1/1"), r.DisasterDate.Value))
                db.AddInParameter(dbcmd, "@BeginTime", DbType.String, r.BeginTime.Value)
                db.AddInParameter(dbcmd, "@EndTime", DbType.String, r.EndTime.Value)
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

