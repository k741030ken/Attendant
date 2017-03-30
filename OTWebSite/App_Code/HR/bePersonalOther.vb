'****************************************************************
' Table:PersonalOther
' Created Date: 2016.03.22
'****************************************************************/
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports Microsoft.Practices.EnterpriseLibrary.Common
Imports Microsoft.Practices.EnterpriseLibrary.Data
Imports System.Data.Common
Imports System.Data

Namespace bePersonalOther
    Public Class Table
        Private m_Rows As Rows = New Rows()
        Private m_Fields As String() = { "CompID", "EmpID", "EmpType1", "SalesFlag", "WTID", "OfficeLoginFlag", "AboriginalFlag", "IsBossFlag", "AboriginalTribe", "RecID" _
                                    , "CheckInDate", "LastChgComp", "LastChgID", "LastChgDate" }
        Private m_Types As System.Type() = { GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String) _
                                    , GetType(Date), GetType(String), GetType(String), GetType(Date) }
        Private m_PrimaryFields As String() = { "CompID", "EmpID" }

        Public ReadOnly Property Rows() As bePersonalOther.Rows 
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
        Public Sub Transfer2Row(PersonalOtherTable As DataTable)
            For Each dr As DataRow In PersonalOtherTable.Rows
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
                dr(m_Rows(i).EmpType1.FieldName) = m_Rows(i).EmpType1.Value
                dr(m_Rows(i).SalesFlag.FieldName) = m_Rows(i).SalesFlag.Value
                dr(m_Rows(i).WTID.FieldName) = m_Rows(i).WTID.Value
                dr(m_Rows(i).OfficeLoginFlag.FieldName) = m_Rows(i).OfficeLoginFlag.Value
                dr(m_Rows(i).AboriginalFlag.FieldName) = m_Rows(i).AboriginalFlag.Value
                dr(m_Rows(i).IsBossFlag.FieldName) = m_Rows(i).IsBossFlag.Value
                dr(m_Rows(i).AboriginalTribe.FieldName) = m_Rows(i).AboriginalTribe.Value
                dr(m_Rows(i).RecID.FieldName) = m_Rows(i).RecID.Value
                dr(m_Rows(i).CheckInDate.FieldName) = m_Rows(i).CheckInDate.Value
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

        Public Sub Add(PersonalOtherRow As Row)
            m_Rows.Add(PersonalOtherRow)
        End Sub

        Public Sub Remove(PersonalOtherRow As Row)
            If m_Rows.IndexOf(PersonalOtherRow) >= 0 Then
                m_Rows.Remove(PersonalOtherRow)
            End If
        End Sub

        Public Sub Dispose()
            m_Rows.Clear()
        End Sub

    End Class

    Public Class Row

        Private FI_CompID As Field(Of String) = new Field(Of String)("CompID", true)
        Private FI_EmpID As Field(Of String) = new Field(Of String)("EmpID", true)
        Private FI_EmpType1 As Field(Of String) = new Field(Of String)("EmpType1", true)
        Private FI_SalesFlag As Field(Of String) = new Field(Of String)("SalesFlag", true)
        Private FI_WTID As Field(Of String) = new Field(Of String)("WTID", true)
        Private FI_OfficeLoginFlag As Field(Of String) = new Field(Of String)("OfficeLoginFlag", true)
        Private FI_AboriginalFlag As Field(Of String) = new Field(Of String)("AboriginalFlag", true)
        Private FI_IsBossFlag As Field(Of String) = new Field(Of String)("IsBossFlag", true)
        Private FI_AboriginalTribe As Field(Of String) = new Field(Of String)("AboriginalTribe", true)
        Private FI_RecID As Field(Of String) = new Field(Of String)("RecID", true)
        Private FI_CheckInDate As Field(Of Date) = new Field(Of Date)("CheckInDate", true)
        Private FI_LastChgComp As Field(Of String) = new Field(Of String)("LastChgComp", true)
        Private FI_LastChgID As Field(Of String) = new Field(Of String)("LastChgID", true)
        Private FI_LastChgDate As Field(Of Date) = new Field(Of Date)("LastChgDate", true)
        Private m_FieldNames As String() = { "CompID", "EmpID", "EmpType1", "SalesFlag", "WTID", "OfficeLoginFlag", "AboriginalFlag", "IsBossFlag", "AboriginalTribe", "RecID" _
                                    , "CheckInDate", "LastChgComp", "LastChgID", "LastChgDate" }
        Private m_PrimaryFields As String() = { "CompID", "EmpID" }
        Private m_IdentityFields As String() = {  }
        Private m_LoadFromDataRow As Boolean = False

        Private Function GetFieldValue(ByVal fieldName As String) As Object
            Select Case fieldName
                Case "CompID"
                    Return FI_CompID.Value
                Case "EmpID"
                    Return FI_EmpID.Value
                Case "EmpType1"
                    Return FI_EmpType1.Value
                Case "SalesFlag"
                    Return FI_SalesFlag.Value
                Case "WTID"
                    Return FI_WTID.Value
                Case "OfficeLoginFlag"
                    Return FI_OfficeLoginFlag.Value
                Case "AboriginalFlag"
                    Return FI_AboriginalFlag.Value
                Case "IsBossFlag"
                    Return FI_IsBossFlag.Value
                Case "AboriginalTribe"
                    Return FI_AboriginalTribe.Value
                Case "RecID"
                    Return FI_RecID.Value
                Case "CheckInDate"
                    Return FI_CheckInDate.Value
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
                Case "EmpType1"
                    FI_EmpType1.SetValue(value)
                Case "SalesFlag"
                    FI_SalesFlag.SetValue(value)
                Case "WTID"
                    FI_WTID.SetValue(value)
                Case "OfficeLoginFlag"
                    FI_OfficeLoginFlag.SetValue(value)
                Case "AboriginalFlag"
                    FI_AboriginalFlag.SetValue(value)
                Case "IsBossFlag"
                    FI_IsBossFlag.SetValue(value)
                Case "AboriginalTribe"
                    FI_AboriginalTribe.SetValue(value)
                Case "RecID"
                    FI_RecID.SetValue(value)
                Case "CheckInDate"
                    FI_CheckInDate.SetValue(value)
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
                Case "EmpType1"
                    return FI_EmpType1.Updated
                Case "SalesFlag"
                    return FI_SalesFlag.Updated
                Case "WTID"
                    return FI_WTID.Updated
                Case "OfficeLoginFlag"
                    return FI_OfficeLoginFlag.Updated
                Case "AboriginalFlag"
                    return FI_AboriginalFlag.Updated
                Case "IsBossFlag"
                    return FI_IsBossFlag.Updated
                Case "AboriginalTribe"
                    return FI_AboriginalTribe.Updated
                Case "RecID"
                    return FI_RecID.Updated
                Case "CheckInDate"
                    return FI_CheckInDate.Updated
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
                Case "EmpType1"
                    return FI_EmpType1.CreateUpdateSQL
                Case "SalesFlag"
                    return FI_SalesFlag.CreateUpdateSQL
                Case "WTID"
                    return FI_WTID.CreateUpdateSQL
                Case "OfficeLoginFlag"
                    return FI_OfficeLoginFlag.CreateUpdateSQL
                Case "AboriginalFlag"
                    return FI_AboriginalFlag.CreateUpdateSQL
                Case "IsBossFlag"
                    return FI_IsBossFlag.CreateUpdateSQL
                Case "AboriginalTribe"
                    return FI_AboriginalTribe.CreateUpdateSQL
                Case "RecID"
                    return FI_RecID.CreateUpdateSQL
                Case "CheckInDate"
                    return FI_CheckInDate.CreateUpdateSQL
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
            FI_EmpID.SetInitValue("")
            FI_EmpType1.SetInitValue("")
            FI_SalesFlag.SetInitValue("0")
            FI_WTID.SetInitValue("")
            FI_OfficeLoginFlag.SetInitValue("0")
            FI_AboriginalFlag.SetInitValue(0)
            FI_IsBossFlag.SetInitValue("")
            FI_AboriginalTribe.SetInitValue("")
            FI_RecID.SetInitValue("")
            FI_CheckInDate.SetInitValue(Convert.ToDateTime("1900/01/01"))
            FI_LastChgComp.SetInitValue("")
            FI_LastChgID.SetInitValue("")
            FI_LastChgDate.SetInitValue(Convert.ToDateTime("1900/01/01"))
        End Sub

        Public Sub New(ByVal dr As System.Data.DataRow)
            FI_CompID.SetInitValue(dr("CompID"))
            FI_EmpID.SetInitValue(dr("EmpID"))
            FI_EmpType1.SetInitValue(dr("EmpType1"))
            FI_SalesFlag.SetInitValue(dr("SalesFlag"))
            FI_WTID.SetInitValue(dr("WTID"))
            FI_OfficeLoginFlag.SetInitValue(dr("OfficeLoginFlag"))
            FI_AboriginalFlag.SetInitValue(dr("AboriginalFlag"))
            FI_IsBossFlag.SetInitValue(dr("IsBossFlag"))
            FI_AboriginalTribe.SetInitValue(dr("AboriginalTribe"))
            FI_RecID.SetInitValue(dr("RecID"))
            FI_CheckInDate.SetInitValue(dr("CheckInDate"))
            FI_LastChgComp.SetInitValue(dr("LastChgComp"))
            FI_LastChgID.SetInitValue(dr("LastChgID"))
            FI_LastChgDate.SetInitValue(dr("LastChgDate"))

            m_LoadFromDataRow = True
        End Sub

        Private Sub ClearUpdated()
            FI_CompID.Updated = False
            FI_EmpID.Updated = False
            FI_EmpType1.Updated = False
            FI_SalesFlag.Updated = False
            FI_WTID.Updated = False
            FI_OfficeLoginFlag.Updated = False
            FI_AboriginalFlag.Updated = False
            FI_IsBossFlag.Updated = False
            FI_AboriginalTribe.Updated = False
            FI_RecID.Updated = False
            FI_CheckInDate.Updated = False
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

        Public ReadOnly Property EmpType1 As Field(Of String) 
            Get
                Return FI_EmpType1
            End Get
        End Property

        Public ReadOnly Property SalesFlag As Field(Of String) 
            Get
                Return FI_SalesFlag
            End Get
        End Property

        Public ReadOnly Property WTID As Field(Of String) 
            Get
                Return FI_WTID
            End Get
        End Property

        Public ReadOnly Property OfficeLoginFlag As Field(Of String) 
            Get
                Return FI_OfficeLoginFlag
            End Get
        End Property

        Public ReadOnly Property AboriginalFlag As Field(Of String) 
            Get
                Return FI_AboriginalFlag
            End Get
        End Property

        Public ReadOnly Property IsBossFlag As Field(Of String) 
            Get
                Return FI_IsBossFlag
            End Get
        End Property

        Public ReadOnly Property AboriginalTribe As Field(Of String) 
            Get
                Return FI_AboriginalTribe
            End Get
        End Property

        Public ReadOnly Property RecID As Field(Of String) 
            Get
                Return FI_RecID
            End Get
        End Property

        Public ReadOnly Property CheckInDate As Field(Of Date) 
            Get
                Return FI_CheckInDate
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
        Public Function DeleteRowByPrimaryKey(ByVal PersonalOtherRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Delete From PersonalOther")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And EmpID = @EmpID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, PersonalOtherRow.CompID.Value)
            db.AddInParameter(dbcmd, "@EmpID", DbType.String, PersonalOtherRow.EmpID.Value)

            return db.ExecuteNonQuery(dbcmd)
        End Function

        public Function DeleteRowByPrimaryKey(ByVal PersonalOtherRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Delete From PersonalOther")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And EmpID = @EmpID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, PersonalOtherRow.CompID.Value)
            db.AddInParameter(dbcmd, "@EmpID", DbType.String, PersonalOtherRow.EmpID.Value)

            return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function DeleteRowByPrimaryKey(ByVal PersonalOtherRow As Row()) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Delete From PersonalOther")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And EmpID = @EmpID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            using cn As DbConnection = db.CreateConnection()
                cn.Open()
                Dim tran As DbTransaction = cn.BeginTransaction()
                Dim inTrans As Boolean = True

                Try
                    For Each r As Row In PersonalOtherRow
                        dbcmd.Parameters.Clear()
                        db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
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

        Public Function DeleteRowByPrimaryKey(ByVal PersonalOtherRow As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Delete From PersonalOther")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And EmpID = @EmpID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())

            For Each r As Row In PersonalOtherRow
                dbcmd.Parameters.Clear()
                db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                db.AddInParameter(dbcmd, "@EmpID", DbType.String, r.EmpID.Value)

                intRowsAffected += db.ExecuteNonQuery(dbcmd, tran)
            Next
            Return intRowsAffected
        End Function

        Public Function QueryByKey(ByVal PersonalOtherRow As Row) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From PersonalOther")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And EmpID = @EmpID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, PersonalOtherRow.CompID.Value)
            db.AddInParameter(dbcmd, "@EmpID", DbType.String, PersonalOtherRow.EmpID.Value)

            Return db.ExecuteDataSet(dbcmd)
        End Function

        Public Function QueryByKey(PersonalOtherRow As Row, tran As DbTransaction) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From PersonalOther")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And EmpID = @EmpID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, PersonalOtherRow.CompID.Value)
            db.AddInParameter(dbcmd, "@EmpID", DbType.String, PersonalOtherRow.EmpID.Value)

            Return db.ExecuteDataSet(dbcmd, tran)
        End Function

        Public Function Update(ByVal PersonalOtherRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim strDot As String = String.Empty

            strSQL.AppendLine("Update PersonalOther Set")
            For i As Integer = 0 To PersonalOtherRow.FieldNames.Length - 1
                If Not PersonalOtherRow.IsIdentityField(PersonalOtherRow.FieldNames(i)) AndAlso PersonalOtherRow.IsUpdated(PersonalOtherRow.FieldNames(i)) AndAlso PersonalOtherRow.CreateUpdateSQL(PersonalOtherRow.FieldNames(i)) Then
                    strSQL.AppendLine(string.Format("{0}{1} = @{1}", strDot, PersonalOtherRow.FieldNames(i)))
                    strDot = ","
                End If
            Next
            If strDot = String.Empty Then Throw New Exception("未異動資料欄位！")
            strSQL.AppendLine("Where CompID = @PKCompID")
            strSQL.AppendLine("And EmpID = @PKEmpID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            If PersonalOtherRow.CompID.Updated Then db.AddInParameter(dbcmd, "@CompID", DbType.String, PersonalOtherRow.CompID.Value)
            If PersonalOtherRow.EmpID.Updated Then db.AddInParameter(dbcmd, "@EmpID", DbType.String, PersonalOtherRow.EmpID.Value)
            If PersonalOtherRow.EmpType1.Updated Then db.AddInParameter(dbcmd, "@EmpType1", DbType.String, PersonalOtherRow.EmpType1.Value)
            If PersonalOtherRow.SalesFlag.Updated Then db.AddInParameter(dbcmd, "@SalesFlag", DbType.String, PersonalOtherRow.SalesFlag.Value)
            If PersonalOtherRow.WTID.Updated Then db.AddInParameter(dbcmd, "@WTID", DbType.String, PersonalOtherRow.WTID.Value)
            If PersonalOtherRow.OfficeLoginFlag.Updated Then db.AddInParameter(dbcmd, "@OfficeLoginFlag", DbType.String, PersonalOtherRow.OfficeLoginFlag.Value)
            If PersonalOtherRow.AboriginalFlag.Updated Then db.AddInParameter(dbcmd, "@AboriginalFlag", DbType.String, PersonalOtherRow.AboriginalFlag.Value)
            If PersonalOtherRow.IsBossFlag.Updated Then db.AddInParameter(dbcmd, "@IsBossFlag", DbType.String, PersonalOtherRow.IsBossFlag.Value)
            If PersonalOtherRow.AboriginalTribe.Updated Then db.AddInParameter(dbcmd, "@AboriginalTribe", DbType.String, PersonalOtherRow.AboriginalTribe.Value)
            If PersonalOtherRow.RecID.Updated Then db.AddInParameter(dbcmd, "@RecID", DbType.String, PersonalOtherRow.RecID.Value)
            If PersonalOtherRow.CheckInDate.Updated Then db.AddInParameter(dbcmd, "@CheckInDate", DbType.Date, IIf(IsDateTimeNull(PersonalOtherRow.CheckInDate.Value), Convert.ToDateTime("1900/1/1"), PersonalOtherRow.CheckInDate.Value))
            If PersonalOtherRow.LastChgComp.Updated Then db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, PersonalOtherRow.LastChgComp.Value)
            If PersonalOtherRow.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, PersonalOtherRow.LastChgID.Value)
            If PersonalOtherRow.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(PersonalOtherRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), PersonalOtherRow.LastChgDate.Value))
            db.AddInParameter(dbcmd, "@PKCompID", DbType.String, IIf(PersonalOtherRow.LoadFromDataRow, PersonalOtherRow.CompID.OldValue, PersonalOtherRow.CompID.Value))
            db.AddInParameter(dbcmd, "@PKEmpID", DbType.String, IIf(PersonalOtherRow.LoadFromDataRow, PersonalOtherRow.EmpID.OldValue, PersonalOtherRow.EmpID.Value))

            Return db.ExecuteNonQuery(dbcmd)
        End Function

        Public Function Update(ByVal PersonalOtherRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim strDot As String = String.Empty

            strSQL.AppendLine("Update PersonalOther Set")
            For i As Integer = 0 To PersonalOtherRow.FieldNames.Length - 1
                If Not PersonalOtherRow.IsIdentityField(PersonalOtherRow.FieldNames(i)) AndAlso PersonalOtherRow.IsUpdated(PersonalOtherRow.FieldNames(i)) AndAlso PersonalOtherRow.CreateUpdateSQL(PersonalOtherRow.FieldNames(i)) Then
                    strSQL.AppendLine(string.Format("{0}{1} = @{1}", strDot, PersonalOtherRow.FieldNames(i)))
                    strDot = ","
                End If
            Next 
            If strDot = String.Empty Then Throw New Exception("未異動資料欄位！")
            strSQL.AppendLine("Where CompID = @PKCompID")
            strSQL.AppendLine("And EmpID = @PKEmpID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            If PersonalOtherRow.CompID.Updated Then db.AddInParameter(dbcmd, "@CompID", DbType.String, PersonalOtherRow.CompID.Value)
            If PersonalOtherRow.EmpID.Updated Then db.AddInParameter(dbcmd, "@EmpID", DbType.String, PersonalOtherRow.EmpID.Value)
            If PersonalOtherRow.EmpType1.Updated Then db.AddInParameter(dbcmd, "@EmpType1", DbType.String, PersonalOtherRow.EmpType1.Value)
            If PersonalOtherRow.SalesFlag.Updated Then db.AddInParameter(dbcmd, "@SalesFlag", DbType.String, PersonalOtherRow.SalesFlag.Value)
            If PersonalOtherRow.WTID.Updated Then db.AddInParameter(dbcmd, "@WTID", DbType.String, PersonalOtherRow.WTID.Value)
            If PersonalOtherRow.OfficeLoginFlag.Updated Then db.AddInParameter(dbcmd, "@OfficeLoginFlag", DbType.String, PersonalOtherRow.OfficeLoginFlag.Value)
            If PersonalOtherRow.AboriginalFlag.Updated Then db.AddInParameter(dbcmd, "@AboriginalFlag", DbType.String, PersonalOtherRow.AboriginalFlag.Value)
            If PersonalOtherRow.IsBossFlag.Updated Then db.AddInParameter(dbcmd, "@IsBossFlag", DbType.String, PersonalOtherRow.IsBossFlag.Value)
            If PersonalOtherRow.AboriginalTribe.Updated Then db.AddInParameter(dbcmd, "@AboriginalTribe", DbType.String, PersonalOtherRow.AboriginalTribe.Value)
            If PersonalOtherRow.RecID.Updated Then db.AddInParameter(dbcmd, "@RecID", DbType.String, PersonalOtherRow.RecID.Value)
            If PersonalOtherRow.CheckInDate.Updated Then db.AddInParameter(dbcmd, "@CheckInDate", DbType.Date, IIf(IsDateTimeNull(PersonalOtherRow.CheckInDate.Value), Convert.ToDateTime("1900/1/1"), PersonalOtherRow.CheckInDate.Value))
            If PersonalOtherRow.LastChgComp.Updated Then db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, PersonalOtherRow.LastChgComp.Value)
            If PersonalOtherRow.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, PersonalOtherRow.LastChgID.Value)
            If PersonalOtherRow.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(PersonalOtherRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), PersonalOtherRow.LastChgDate.Value))
            db.AddInParameter(dbcmd, "@PKCompID", DbType.String, IIf(PersonalOtherRow.LoadFromDataRow, PersonalOtherRow.CompID.OldValue, PersonalOtherRow.CompID.Value))
            db.AddInParameter(dbcmd, "@PKEmpID", DbType.String, IIf(PersonalOtherRow.LoadFromDataRow, PersonalOtherRow.EmpID.OldValue, PersonalOtherRow.EmpID.Value))

            Return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function Update(ByVal PersonalOtherRow As Row()) As Integer
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
                    For Each r As Row In PersonalOtherRow
                        strDot = String.Empty
                        If strSQL.Length > 0 Then strSQL.Remove(0, strSQL.Length)
                        strSQL.AppendLine("Update PersonalOther Set")
                        For i As Integer = 0 To r.FieldNames.Length - 1
                            If Not r.IsIdentityField(r.FieldNames(i)) AndAlso r.IsUpdated(r.FieldNames(i)) AndAlso r.CreateUpdateSQL(r.FieldNames(i)) Then
                                strSQL.AppendLine(String.Format("{0}{1} = @{1}", strDot, r.FieldNames(i)))
                                strDot = ","
                            End If
                        Next
                        If strDot = String.Empty Then Continue For
                        strSQL.AppendLine("Where CompID = @PKCompID")
                        strSQL.AppendLine("And EmpID = @PKEmpID")

                        dbcmd = db.GetSqlStringCommand(strSQL.ToString())
                        If r.CompID.Updated Then db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                        If r.EmpID.Updated Then db.AddInParameter(dbcmd, "@EmpID", DbType.String, r.EmpID.Value)
                        If r.EmpType1.Updated Then db.AddInParameter(dbcmd, "@EmpType1", DbType.String, r.EmpType1.Value)
                        If r.SalesFlag.Updated Then db.AddInParameter(dbcmd, "@SalesFlag", DbType.String, r.SalesFlag.Value)
                        If r.WTID.Updated Then db.AddInParameter(dbcmd, "@WTID", DbType.String, r.WTID.Value)
                        If r.OfficeLoginFlag.Updated Then db.AddInParameter(dbcmd, "@OfficeLoginFlag", DbType.String, r.OfficeLoginFlag.Value)
                        If r.AboriginalFlag.Updated Then db.AddInParameter(dbcmd, "@AboriginalFlag", DbType.String, r.AboriginalFlag.Value)
                        If r.IsBossFlag.Updated Then db.AddInParameter(dbcmd, "@IsBossFlag", DbType.String, r.IsBossFlag.Value)
                        If r.AboriginalTribe.Updated Then db.AddInParameter(dbcmd, "@AboriginalTribe", DbType.String, r.AboriginalTribe.Value)
                        If r.RecID.Updated Then db.AddInParameter(dbcmd, "@RecID", DbType.String, r.RecID.Value)
                        If r.CheckInDate.Updated Then db.AddInParameter(dbcmd, "@CheckInDate", DbType.Date, IIf(IsDateTimeNull(r.CheckInDate.Value), Convert.ToDateTime("1900/1/1"), r.CheckInDate.Value))
                        If r.LastChgComp.Updated Then db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, r.LastChgComp.Value)
                        If r.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, r.LastChgID.Value)
                        If r.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(r.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), r.LastChgDate.Value))
                        db.AddInParameter(dbcmd, "@PKCompID", DbType.String, IIf(r.LoadFromDataRow, r.CompID.OldValue, r.CompID.Value))
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

        Public Function Update(ByVal PersonalOtherRow As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0
            Dim strDot As String

            For Each r As Row In PersonalOtherRow
                strDot = String.Empty
                If strSQL.Length > 0 Then strSQL.Remove(0, strSQL.Length)
                strSQL.AppendLine("Update PersonalOther Set")
                For i As Integer = 0 To r.FieldNames.Length - 1
                    If Not r.IsIdentityField(r.FieldNames(i)) AndAlso r.IsUpdated(r.FieldNames(i)) AndAlso r.CreateUpdateSQL(r.FieldNames(i))
                        strSQL.AppendLine(String.Format("{0}{1} = @{1}", strDot, r.FieldNames(i)))
                        strDot = ","
                    End If
                Next
                If strDot = String.Empty Then Continue For
                strSQL.AppendLine("Where CompID = @PKCompID")
                strSQL.AppendLine("And EmpID = @PKEmpID")

                dbcmd = db.GetSqlStringCommand(strSQL.ToString())
                If r.CompID.Updated Then db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                If r.EmpID.Updated Then db.AddInParameter(dbcmd, "@EmpID", DbType.String, r.EmpID.Value)
                If r.EmpType1.Updated Then db.AddInParameter(dbcmd, "@EmpType1", DbType.String, r.EmpType1.Value)
                If r.SalesFlag.Updated Then db.AddInParameter(dbcmd, "@SalesFlag", DbType.String, r.SalesFlag.Value)
                If r.WTID.Updated Then db.AddInParameter(dbcmd, "@WTID", DbType.String, r.WTID.Value)
                If r.OfficeLoginFlag.Updated Then db.AddInParameter(dbcmd, "@OfficeLoginFlag", DbType.String, r.OfficeLoginFlag.Value)
                If r.AboriginalFlag.Updated Then db.AddInParameter(dbcmd, "@AboriginalFlag", DbType.String, r.AboriginalFlag.Value)
                If r.IsBossFlag.Updated Then db.AddInParameter(dbcmd, "@IsBossFlag", DbType.String, r.IsBossFlag.Value)
                If r.AboriginalTribe.Updated Then db.AddInParameter(dbcmd, "@AboriginalTribe", DbType.String, r.AboriginalTribe.Value)
                If r.RecID.Updated Then db.AddInParameter(dbcmd, "@RecID", DbType.String, r.RecID.Value)
                If r.CheckInDate.Updated Then db.AddInParameter(dbcmd, "@CheckInDate", DbType.Date, IIf(IsDateTimeNull(r.CheckInDate.Value), Convert.ToDateTime("1900/1/1"), r.CheckInDate.Value))
                If r.LastChgComp.Updated Then db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, r.LastChgComp.Value)
                If r.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, r.LastChgID.Value)
                If r.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(r.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), r.LastChgDate.Value))
                db.AddInParameter(dbcmd, "@PKCompID", DbType.String, IIf(r.LoadFromDataRow, r.CompID.OldValue, r.CompID.Value))
                db.AddInParameter(dbcmd, "@PKEmpID", DbType.String, IIf(r.LoadFromDataRow, r.EmpID.OldValue, r.EmpID.Value))

                intRowsAffected += db.ExecuteNonQuery(dbcmd, tran)
            Next
            Return intRowsAffected
        End Function

        Public Function IsDataExists(ByVal PersonalOtherRow As Row) As Boolean
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intCount As Integer = 0

            strSQL.AppendLine("Select Count(*) Cnt From PersonalOther")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And EmpID = @EmpID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, PersonalOtherRow.CompID.Value)
            db.AddInParameter(dbcmd, "@EmpID", DbType.String, PersonalOtherRow.EmpID.Value)

            intCount = Convert.ToInt32(db.ExecuteScalar(dbcmd))
            Return IIf(intCount > 0, True, False)
        End Function

        Public Function IsDataExists(ByVal PersonalOtherRow As Row, ByVal tran As DbTransaction) As Boolean
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intCount As Integer = 0

            strSQL.AppendLine("Select Count(*) Cnt From PersonalOther")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And EmpID = @EmpID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, PersonalOtherRow.CompID.Value)
            db.AddInParameter(dbcmd, "@EmpID", DbType.String, PersonalOtherRow.EmpID.Value)

            intCount = Convert.ToInt32(db.ExecuteScalar(dbcmd, tran))
            Return IIf(intCount > 0, True, False)
        End Function

        Public Function QuerybyWhere(WhereCondition As String) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From PersonalOther")
            If WhereCondition <> String.Empty Then strSQL.AppendLine(WhereCondition)

            Return db.ExecuteDataSet(CommandType.Text, strSQL.ToString())
        End Function

        Public Function Insert(ByVal PersonalOtherRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Insert into PersonalOther")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    CompID, EmpID, EmpType1, SalesFlag, WTID, OfficeLoginFlag, AboriginalFlag, IsBossFlag,")
            strSQL.AppendLine("    AboriginalTribe, RecID, CheckInDate, LastChgComp, LastChgID, LastChgDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @CompID, @EmpID, @EmpType1, @SalesFlag, @WTID, @OfficeLoginFlag, @AboriginalFlag, @IsBossFlag,")
            strSQL.AppendLine("    @AboriginalTribe, @RecID, @CheckInDate, @LastChgComp, @LastChgID, @LastChgDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, PersonalOtherRow.CompID.Value)
            db.AddInParameter(dbcmd, "@EmpID", DbType.String, PersonalOtherRow.EmpID.Value)
            db.AddInParameter(dbcmd, "@EmpType1", DbType.String, PersonalOtherRow.EmpType1.Value)
            db.AddInParameter(dbcmd, "@SalesFlag", DbType.String, PersonalOtherRow.SalesFlag.Value)
            db.AddInParameter(dbcmd, "@WTID", DbType.String, PersonalOtherRow.WTID.Value)
            db.AddInParameter(dbcmd, "@OfficeLoginFlag", DbType.String, PersonalOtherRow.OfficeLoginFlag.Value)
            db.AddInParameter(dbcmd, "@AboriginalFlag", DbType.String, PersonalOtherRow.AboriginalFlag.Value)
            db.AddInParameter(dbcmd, "@IsBossFlag", DbType.String, PersonalOtherRow.IsBossFlag.Value)
            db.AddInParameter(dbcmd, "@AboriginalTribe", DbType.String, PersonalOtherRow.AboriginalTribe.Value)
            db.AddInParameter(dbcmd, "@RecID", DbType.String, PersonalOtherRow.RecID.Value)
            db.AddInParameter(dbcmd, "@CheckInDate", DbType.Date, IIf(IsDateTimeNull(PersonalOtherRow.CheckInDate.Value), Convert.ToDateTime("1900/1/1"), PersonalOtherRow.CheckInDate.Value))
            db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, PersonalOtherRow.LastChgComp.Value)
            db.AddInParameter(dbcmd, "@LastChgID", DbType.String, PersonalOtherRow.LastChgID.Value)
            db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(PersonalOtherRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), PersonalOtherRow.LastChgDate.Value))

            Return db.ExecuteNonQuery(dbcmd)
        End Function

        Public Function Insert(ByVal PersonalOtherRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Insert into PersonalOther")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    CompID, EmpID, EmpType1, SalesFlag, WTID, OfficeLoginFlag, AboriginalFlag, IsBossFlag,")
            strSQL.AppendLine("    AboriginalTribe, RecID, CheckInDate, LastChgComp, LastChgID, LastChgDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @CompID, @EmpID, @EmpType1, @SalesFlag, @WTID, @OfficeLoginFlag, @AboriginalFlag, @IsBossFlag,")
            strSQL.AppendLine("    @AboriginalTribe, @RecID, @CheckInDate, @LastChgComp, @LastChgID, @LastChgDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, PersonalOtherRow.CompID.Value)
            db.AddInParameter(dbcmd, "@EmpID", DbType.String, PersonalOtherRow.EmpID.Value)
            db.AddInParameter(dbcmd, "@EmpType1", DbType.String, PersonalOtherRow.EmpType1.Value)
            db.AddInParameter(dbcmd, "@SalesFlag", DbType.String, PersonalOtherRow.SalesFlag.Value)
            db.AddInParameter(dbcmd, "@WTID", DbType.String, PersonalOtherRow.WTID.Value)
            db.AddInParameter(dbcmd, "@OfficeLoginFlag", DbType.String, PersonalOtherRow.OfficeLoginFlag.Value)
            db.AddInParameter(dbcmd, "@AboriginalFlag", DbType.String, PersonalOtherRow.AboriginalFlag.Value)
            db.AddInParameter(dbcmd, "@IsBossFlag", DbType.String, PersonalOtherRow.IsBossFlag.Value)
            db.AddInParameter(dbcmd, "@AboriginalTribe", DbType.String, PersonalOtherRow.AboriginalTribe.Value)
            db.AddInParameter(dbcmd, "@RecID", DbType.String, PersonalOtherRow.RecID.Value)
            db.AddInParameter(dbcmd, "@CheckInDate", DbType.Date, IIf(IsDateTimeNull(PersonalOtherRow.CheckInDate.Value), Convert.ToDateTime("1900/1/1"), PersonalOtherRow.CheckInDate.Value))
            db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, PersonalOtherRow.LastChgComp.Value)
            db.AddInParameter(dbcmd, "@LastChgID", DbType.String, PersonalOtherRow.LastChgID.Value)
            db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(PersonalOtherRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), PersonalOtherRow.LastChgDate.Value))

            Return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function Insert(ByVal PersonalOtherRow As Row()) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Insert into PersonalOther")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    CompID, EmpID, EmpType1, SalesFlag, WTID, OfficeLoginFlag, AboriginalFlag, IsBossFlag,")
            strSQL.AppendLine("    AboriginalTribe, RecID, CheckInDate, LastChgComp, LastChgID, LastChgDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @CompID, @EmpID, @EmpType1, @SalesFlag, @WTID, @OfficeLoginFlag, @AboriginalFlag, @IsBossFlag,")
            strSQL.AppendLine("    @AboriginalTribe, @RecID, @CheckInDate, @LastChgComp, @LastChgID, @LastChgDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            Using cn As DbConnection = db.CreateConnection()
                cn.Open()
                Dim tran As DbTransaction = cn.BeginTransaction()
                Dim inTrans As Boolean = True

                Try
                    For Each r As Row In PersonalOtherRow
                        dbcmd.Parameters.Clear()
                        db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                        db.AddInParameter(dbcmd, "@EmpID", DbType.String, r.EmpID.Value)
                        db.AddInParameter(dbcmd, "@EmpType1", DbType.String, r.EmpType1.Value)
                        db.AddInParameter(dbcmd, "@SalesFlag", DbType.String, r.SalesFlag.Value)
                        db.AddInParameter(dbcmd, "@WTID", DbType.String, r.WTID.Value)
                        db.AddInParameter(dbcmd, "@OfficeLoginFlag", DbType.String, r.OfficeLoginFlag.Value)
                        db.AddInParameter(dbcmd, "@AboriginalFlag", DbType.String, r.AboriginalFlag.Value)
                        db.AddInParameter(dbcmd, "@IsBossFlag", DbType.String, r.IsBossFlag.Value)
                        db.AddInParameter(dbcmd, "@AboriginalTribe", DbType.String, r.AboriginalTribe.Value)
                        db.AddInParameter(dbcmd, "@RecID", DbType.String, r.RecID.Value)
                        db.AddInParameter(dbcmd, "@CheckInDate", DbType.Date, IIf(IsDateTimeNull(r.CheckInDate.Value), Convert.ToDateTime("1900/1/1"), r.CheckInDate.Value))
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

        Public Function Insert(ByVal PersonalOtherRow As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Insert into PersonalOther")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    CompID, EmpID, EmpType1, SalesFlag, WTID, OfficeLoginFlag, AboriginalFlag, IsBossFlag,")
            strSQL.AppendLine("    AboriginalTribe, RecID, CheckInDate, LastChgComp, LastChgID, LastChgDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @CompID, @EmpID, @EmpType1, @SalesFlag, @WTID, @OfficeLoginFlag, @AboriginalFlag, @IsBossFlag,")
            strSQL.AppendLine("    @AboriginalTribe, @RecID, @CheckInDate, @LastChgComp, @LastChgID, @LastChgDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())

            For Each r As Row In PersonalOtherRow
                dbcmd.Parameters.Clear()
                db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                db.AddInParameter(dbcmd, "@EmpID", DbType.String, r.EmpID.Value)
                db.AddInParameter(dbcmd, "@EmpType1", DbType.String, r.EmpType1.Value)
                db.AddInParameter(dbcmd, "@SalesFlag", DbType.String, r.SalesFlag.Value)
                db.AddInParameter(dbcmd, "@WTID", DbType.String, r.WTID.Value)
                db.AddInParameter(dbcmd, "@OfficeLoginFlag", DbType.String, r.OfficeLoginFlag.Value)
                db.AddInParameter(dbcmd, "@AboriginalFlag", DbType.String, r.AboriginalFlag.Value)
                db.AddInParameter(dbcmd, "@IsBossFlag", DbType.String, r.IsBossFlag.Value)
                db.AddInParameter(dbcmd, "@AboriginalTribe", DbType.String, r.AboriginalTribe.Value)
                db.AddInParameter(dbcmd, "@RecID", DbType.String, r.RecID.Value)
                db.AddInParameter(dbcmd, "@CheckInDate", DbType.Date, IIf(IsDateTimeNull(r.CheckInDate.Value), Convert.ToDateTime("1900/1/1"), r.CheckInDate.Value))
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

