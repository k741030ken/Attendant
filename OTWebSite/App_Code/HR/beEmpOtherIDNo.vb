'****************************************************************
' Table:EmpOtherIDNo
' Created Date: 2016.08.03
'****************************************************************/
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports Microsoft.Practices.EnterpriseLibrary.Common
Imports Microsoft.Practices.EnterpriseLibrary.Data
Imports System.Data.Common
Imports System.Data

Namespace beEmpOtherIDNo
    Public Class Table
        Private m_Rows As Rows = New Rows()
        Private m_Fields As String() = { "IDNo", "OtherIDNo", "IDType", "IDExpireDate", "LastChgComp", "LastChgID", "LastChgDate" }
        Private m_Types As System.Type() = { GetType(String), GetType(String), GetType(String), GetType(Date), GetType(String), GetType(String), GetType(Date) }
        Private m_PrimaryFields As String() = { "IDNo", "OtherIDNo", "IDType" }

        Public ReadOnly Property Rows() As beEmpOtherIDNo.Rows 
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
        Public Sub Transfer2Row(EmpOtherIDNoTable As DataTable)
            For Each dr As DataRow In EmpOtherIDNoTable.Rows
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

                dr(m_Rows(i).IDNo.FieldName) = m_Rows(i).IDNo.Value
                dr(m_Rows(i).OtherIDNo.FieldName) = m_Rows(i).OtherIDNo.Value
                dr(m_Rows(i).IDType.FieldName) = m_Rows(i).IDType.Value
                dr(m_Rows(i).IDExpireDate.FieldName) = m_Rows(i).IDExpireDate.Value
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

        Public Sub Add(EmpOtherIDNoRow As Row)
            m_Rows.Add(EmpOtherIDNoRow)
        End Sub

        Public Sub Remove(EmpOtherIDNoRow As Row)
            If m_Rows.IndexOf(EmpOtherIDNoRow) >= 0 Then
                m_Rows.Remove(EmpOtherIDNoRow)
            End If
        End Sub

        Public Sub Dispose()
            m_Rows.Clear()
        End Sub

    End Class

    Public Class Row

        Private FI_IDNo As Field(Of String) = new Field(Of String)("IDNo", true)
        Private FI_OtherIDNo As Field(Of String) = new Field(Of String)("OtherIDNo", true)
        Private FI_IDType As Field(Of String) = new Field(Of String)("IDType", true)
        Private FI_IDExpireDate As Field(Of Date) = new Field(Of Date)("IDExpireDate", true)
        Private FI_LastChgComp As Field(Of String) = new Field(Of String)("LastChgComp", true)
        Private FI_LastChgID As Field(Of String) = new Field(Of String)("LastChgID", true)
        Private FI_LastChgDate As Field(Of Date) = new Field(Of Date)("LastChgDate", true)
        Private m_FieldNames As String() = { "IDNo", "OtherIDNo", "IDType", "IDExpireDate", "LastChgComp", "LastChgID", "LastChgDate" }
        Private m_PrimaryFields As String() = { "IDNo", "OtherIDNo", "IDType" }
        Private m_IdentityFields As String() = {  }
        Private m_LoadFromDataRow As Boolean = False

        Private Function GetFieldValue(ByVal fieldName As String) As Object
            Select Case fieldName
                Case "IDNo"
                    Return FI_IDNo.Value
                Case "OtherIDNo"
                    Return FI_OtherIDNo.Value
                Case "IDType"
                    Return FI_IDType.Value
                Case "IDExpireDate"
                    Return FI_IDExpireDate.Value
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
                Case "IDNo"
                    FI_IDNo.SetValue(value)
                Case "OtherIDNo"
                    FI_OtherIDNo.SetValue(value)
                Case "IDType"
                    FI_IDType.SetValue(value)
                Case "IDExpireDate"
                    FI_IDExpireDate.SetValue(value)
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
                Case "IDNo"
                    return FI_IDNo.Updated
                Case "OtherIDNo"
                    return FI_OtherIDNo.Updated
                Case "IDType"
                    return FI_IDType.Updated
                Case "IDExpireDate"
                    return FI_IDExpireDate.Updated
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
                Case "IDNo"
                    return FI_IDNo.CreateUpdateSQL
                Case "OtherIDNo"
                    return FI_OtherIDNo.CreateUpdateSQL
                Case "IDType"
                    return FI_IDType.CreateUpdateSQL
                Case "IDExpireDate"
                    return FI_IDExpireDate.CreateUpdateSQL
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
            FI_IDNo.SetInitValue("")
            FI_OtherIDNo.SetInitValue("")
            FI_IDType.SetInitValue("")
            FI_IDExpireDate.SetInitValue(Convert.ToDateTime("1900/01/01"))
            FI_LastChgComp.SetInitValue("")
            FI_LastChgID.SetInitValue("")
            FI_LastChgDate.SetInitValue(Convert.ToDateTime("1900/01/01"))
        End Sub

        Public Sub New(ByVal dr As System.Data.DataRow)
            FI_IDNo.SetInitValue(dr("IDNo"))
            FI_OtherIDNo.SetInitValue(dr("OtherIDNo"))
            FI_IDType.SetInitValue(dr("IDType"))
            FI_IDExpireDate.SetInitValue(dr("IDExpireDate"))
            FI_LastChgComp.SetInitValue(dr("LastChgComp"))
            FI_LastChgID.SetInitValue(dr("LastChgID"))
            FI_LastChgDate.SetInitValue(dr("LastChgDate"))

            m_LoadFromDataRow = True
        End Sub

        Private Sub ClearUpdated()
            FI_IDNo.Updated = False
            FI_OtherIDNo.Updated = False
            FI_IDType.Updated = False
            FI_IDExpireDate.Updated = False
            FI_LastChgComp.Updated = False
            FI_LastChgID.Updated = False
            FI_LastChgDate.Updated = False
        End Sub

        Public ReadOnly Property IDNo As Field(Of String) 
            Get
                Return FI_IDNo
            End Get
        End Property

        Public ReadOnly Property OtherIDNo As Field(Of String) 
            Get
                Return FI_OtherIDNo
            End Get
        End Property

        Public ReadOnly Property IDType As Field(Of String) 
            Get
                Return FI_IDType
            End Get
        End Property

        Public ReadOnly Property IDExpireDate As Field(Of Date) 
            Get
                Return FI_IDExpireDate
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
        Public Function DeleteRowByPrimaryKey(ByVal EmpOtherIDNoRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Delete From EmpOtherIDNo")
            strSQL.AppendLine("Where IDNo = @IDNo")
            strSQL.AppendLine("And OtherIDNo = @OtherIDNo")
            strSQL.AppendLine("And IDType = @IDType")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@IDNo", DbType.String, EmpOtherIDNoRow.IDNo.Value)
            db.AddInParameter(dbcmd, "@OtherIDNo", DbType.String, EmpOtherIDNoRow.OtherIDNo.Value)
            db.AddInParameter(dbcmd, "@IDType", DbType.String, EmpOtherIDNoRow.IDType.Value)

            return db.ExecuteNonQuery(dbcmd)
        End Function

        public Function DeleteRowByPrimaryKey(ByVal EmpOtherIDNoRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Delete From EmpOtherIDNo")
            strSQL.AppendLine("Where IDNo = @IDNo")
            strSQL.AppendLine("And OtherIDNo = @OtherIDNo")
            strSQL.AppendLine("And IDType = @IDType")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@IDNo", DbType.String, EmpOtherIDNoRow.IDNo.Value)
            db.AddInParameter(dbcmd, "@OtherIDNo", DbType.String, EmpOtherIDNoRow.OtherIDNo.Value)
            db.AddInParameter(dbcmd, "@IDType", DbType.String, EmpOtherIDNoRow.IDType.Value)

            return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function DeleteRowByPrimaryKey(ByVal EmpOtherIDNoRow As Row()) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Delete From EmpOtherIDNo")
            strSQL.AppendLine("Where IDNo = @IDNo")
            strSQL.AppendLine("And OtherIDNo = @OtherIDNo")
            strSQL.AppendLine("And IDType = @IDType")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            using cn As DbConnection = db.CreateConnection()
                cn.Open()
                Dim tran As DbTransaction = cn.BeginTransaction()
                Dim inTrans As Boolean = True

                Try
                    For Each r As Row In EmpOtherIDNoRow
                        dbcmd.Parameters.Clear()
                        db.AddInParameter(dbcmd, "@IDNo", DbType.String, r.IDNo.Value)
                        db.AddInParameter(dbcmd, "@OtherIDNo", DbType.String, r.OtherIDNo.Value)
                        db.AddInParameter(dbcmd, "@IDType", DbType.String, r.IDType.Value)

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

        Public Function DeleteRowByPrimaryKey(ByVal EmpOtherIDNoRow As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Delete From EmpOtherIDNo")
            strSQL.AppendLine("Where IDNo = @IDNo")
            strSQL.AppendLine("And OtherIDNo = @OtherIDNo")
            strSQL.AppendLine("And IDType = @IDType")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())

            For Each r As Row In EmpOtherIDNoRow
                dbcmd.Parameters.Clear()
                db.AddInParameter(dbcmd, "@IDNo", DbType.String, r.IDNo.Value)
                db.AddInParameter(dbcmd, "@OtherIDNo", DbType.String, r.OtherIDNo.Value)
                db.AddInParameter(dbcmd, "@IDType", DbType.String, r.IDType.Value)

                intRowsAffected += db.ExecuteNonQuery(dbcmd, tran)
            Next
            Return intRowsAffected
        End Function

        Public Function QueryByKey(ByVal EmpOtherIDNoRow As Row) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From EmpOtherIDNo")
            strSQL.AppendLine("Where IDNo = @IDNo")
            strSQL.AppendLine("And OtherIDNo = @OtherIDNo")
            strSQL.AppendLine("And IDType = @IDType")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@IDNo", DbType.String, EmpOtherIDNoRow.IDNo.Value)
            db.AddInParameter(dbcmd, "@OtherIDNo", DbType.String, EmpOtherIDNoRow.OtherIDNo.Value)
            db.AddInParameter(dbcmd, "@IDType", DbType.String, EmpOtherIDNoRow.IDType.Value)

            Return db.ExecuteDataSet(dbcmd)
        End Function

        Public Function QueryByKey(EmpOtherIDNoRow As Row, tran As DbTransaction) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From EmpOtherIDNo")
            strSQL.AppendLine("Where IDNo = @IDNo")
            strSQL.AppendLine("And OtherIDNo = @OtherIDNo")
            strSQL.AppendLine("And IDType = @IDType")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@IDNo", DbType.String, EmpOtherIDNoRow.IDNo.Value)
            db.AddInParameter(dbcmd, "@OtherIDNo", DbType.String, EmpOtherIDNoRow.OtherIDNo.Value)
            db.AddInParameter(dbcmd, "@IDType", DbType.String, EmpOtherIDNoRow.IDType.Value)

            Return db.ExecuteDataSet(dbcmd, tran)
        End Function

        Public Function Update(ByVal EmpOtherIDNoRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim strDot As String = String.Empty

            strSQL.AppendLine("Update EmpOtherIDNo Set")
            For i As Integer = 0 To EmpOtherIDNoRow.FieldNames.Length - 1
                If Not EmpOtherIDNoRow.IsIdentityField(EmpOtherIDNoRow.FieldNames(i)) AndAlso EmpOtherIDNoRow.IsUpdated(EmpOtherIDNoRow.FieldNames(i)) AndAlso EmpOtherIDNoRow.CreateUpdateSQL(EmpOtherIDNoRow.FieldNames(i)) Then
                    strSQL.AppendLine(string.Format("{0}{1} = @{1}", strDot, EmpOtherIDNoRow.FieldNames(i)))
                    strDot = ","
                End If
            Next
            If strDot = String.Empty Then Throw New Exception("未異動資料欄位！")
            strSQL.AppendLine("Where IDNo = @PKIDNo")
            strSQL.AppendLine("And OtherIDNo = @PKOtherIDNo")
            strSQL.AppendLine("And IDType = @PKIDType")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            If EmpOtherIDNoRow.IDNo.Updated Then db.AddInParameter(dbcmd, "@IDNo", DbType.String, EmpOtherIDNoRow.IDNo.Value)
            If EmpOtherIDNoRow.OtherIDNo.Updated Then db.AddInParameter(dbcmd, "@OtherIDNo", DbType.String, EmpOtherIDNoRow.OtherIDNo.Value)
            If EmpOtherIDNoRow.IDType.Updated Then db.AddInParameter(dbcmd, "@IDType", DbType.String, EmpOtherIDNoRow.IDType.Value)
            If EmpOtherIDNoRow.IDExpireDate.Updated Then db.AddInParameter(dbcmd, "@IDExpireDate", DbType.Date, IIf(IsDateTimeNull(EmpOtherIDNoRow.IDExpireDate.Value), Convert.ToDateTime("1900/1/1"), EmpOtherIDNoRow.IDExpireDate.Value))
            If EmpOtherIDNoRow.LastChgComp.Updated Then db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, EmpOtherIDNoRow.LastChgComp.Value)
            If EmpOtherIDNoRow.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, EmpOtherIDNoRow.LastChgID.Value)
            If EmpOtherIDNoRow.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(EmpOtherIDNoRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), EmpOtherIDNoRow.LastChgDate.Value))
            db.AddInParameter(dbcmd, "@PKIDNo", DbType.String, IIf(EmpOtherIDNoRow.LoadFromDataRow, EmpOtherIDNoRow.IDNo.OldValue, EmpOtherIDNoRow.IDNo.Value))
            db.AddInParameter(dbcmd, "@PKOtherIDNo", DbType.String, IIf(EmpOtherIDNoRow.LoadFromDataRow, EmpOtherIDNoRow.OtherIDNo.OldValue, EmpOtherIDNoRow.OtherIDNo.Value))
            db.AddInParameter(dbcmd, "@PKIDType", DbType.String, IIf(EmpOtherIDNoRow.LoadFromDataRow, EmpOtherIDNoRow.IDType.OldValue, EmpOtherIDNoRow.IDType.Value))

            Return db.ExecuteNonQuery(dbcmd)
        End Function

        Public Function Update(ByVal EmpOtherIDNoRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim strDot As String = String.Empty

            strSQL.AppendLine("Update EmpOtherIDNo Set")
            For i As Integer = 0 To EmpOtherIDNoRow.FieldNames.Length - 1
                If Not EmpOtherIDNoRow.IsIdentityField(EmpOtherIDNoRow.FieldNames(i)) AndAlso EmpOtherIDNoRow.IsUpdated(EmpOtherIDNoRow.FieldNames(i)) AndAlso EmpOtherIDNoRow.CreateUpdateSQL(EmpOtherIDNoRow.FieldNames(i)) Then
                    strSQL.AppendLine(string.Format("{0}{1} = @{1}", strDot, EmpOtherIDNoRow.FieldNames(i)))
                    strDot = ","
                End If
            Next 
            If strDot = String.Empty Then Throw New Exception("未異動資料欄位！")
            strSQL.AppendLine("Where IDNo = @PKIDNo")
            strSQL.AppendLine("And OtherIDNo = @PKOtherIDNo")
            strSQL.AppendLine("And IDType = @PKIDType")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            If EmpOtherIDNoRow.IDNo.Updated Then db.AddInParameter(dbcmd, "@IDNo", DbType.String, EmpOtherIDNoRow.IDNo.Value)
            If EmpOtherIDNoRow.OtherIDNo.Updated Then db.AddInParameter(dbcmd, "@OtherIDNo", DbType.String, EmpOtherIDNoRow.OtherIDNo.Value)
            If EmpOtherIDNoRow.IDType.Updated Then db.AddInParameter(dbcmd, "@IDType", DbType.String, EmpOtherIDNoRow.IDType.Value)
            If EmpOtherIDNoRow.IDExpireDate.Updated Then db.AddInParameter(dbcmd, "@IDExpireDate", DbType.Date, IIf(IsDateTimeNull(EmpOtherIDNoRow.IDExpireDate.Value), Convert.ToDateTime("1900/1/1"), EmpOtherIDNoRow.IDExpireDate.Value))
            If EmpOtherIDNoRow.LastChgComp.Updated Then db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, EmpOtherIDNoRow.LastChgComp.Value)
            If EmpOtherIDNoRow.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, EmpOtherIDNoRow.LastChgID.Value)
            If EmpOtherIDNoRow.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(EmpOtherIDNoRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), EmpOtherIDNoRow.LastChgDate.Value))
            db.AddInParameter(dbcmd, "@PKIDNo", DbType.String, IIf(EmpOtherIDNoRow.LoadFromDataRow, EmpOtherIDNoRow.IDNo.OldValue, EmpOtherIDNoRow.IDNo.Value))
            db.AddInParameter(dbcmd, "@PKOtherIDNo", DbType.String, IIf(EmpOtherIDNoRow.LoadFromDataRow, EmpOtherIDNoRow.OtherIDNo.OldValue, EmpOtherIDNoRow.OtherIDNo.Value))
            db.AddInParameter(dbcmd, "@PKIDType", DbType.String, IIf(EmpOtherIDNoRow.LoadFromDataRow, EmpOtherIDNoRow.IDType.OldValue, EmpOtherIDNoRow.IDType.Value))

            Return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function Update(ByVal EmpOtherIDNoRow As Row()) As Integer
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
                    For Each r As Row In EmpOtherIDNoRow
                        strDot = String.Empty
                        If strSQL.Length > 0 Then strSQL.Remove(0, strSQL.Length)
                        strSQL.AppendLine("Update EmpOtherIDNo Set")
                        For i As Integer = 0 To r.FieldNames.Length - 1
                            If Not r.IsIdentityField(r.FieldNames(i)) AndAlso r.IsUpdated(r.FieldNames(i)) AndAlso r.CreateUpdateSQL(r.FieldNames(i)) Then
                                strSQL.AppendLine(String.Format("{0}{1} = @{1}", strDot, r.FieldNames(i)))
                                strDot = ","
                            End If
                        Next
                        If strDot = String.Empty Then Continue For
                        strSQL.AppendLine("Where IDNo = @PKIDNo")
                        strSQL.AppendLine("And OtherIDNo = @PKOtherIDNo")
                        strSQL.AppendLine("And IDType = @PKIDType")

                        dbcmd = db.GetSqlStringCommand(strSQL.ToString())
                        If r.IDNo.Updated Then db.AddInParameter(dbcmd, "@IDNo", DbType.String, r.IDNo.Value)
                        If r.OtherIDNo.Updated Then db.AddInParameter(dbcmd, "@OtherIDNo", DbType.String, r.OtherIDNo.Value)
                        If r.IDType.Updated Then db.AddInParameter(dbcmd, "@IDType", DbType.String, r.IDType.Value)
                        If r.IDExpireDate.Updated Then db.AddInParameter(dbcmd, "@IDExpireDate", DbType.Date, IIf(IsDateTimeNull(r.IDExpireDate.Value), Convert.ToDateTime("1900/1/1"), r.IDExpireDate.Value))
                        If r.LastChgComp.Updated Then db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, r.LastChgComp.Value)
                        If r.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, r.LastChgID.Value)
                        If r.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(r.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), r.LastChgDate.Value))
                        db.AddInParameter(dbcmd, "@PKIDNo", DbType.String, IIf(r.LoadFromDataRow, r.IDNo.OldValue, r.IDNo.Value))
                        db.AddInParameter(dbcmd, "@PKOtherIDNo", DbType.String, IIf(r.LoadFromDataRow, r.OtherIDNo.OldValue, r.OtherIDNo.Value))
                        db.AddInParameter(dbcmd, "@PKIDType", DbType.String, IIf(r.LoadFromDataRow, r.IDType.OldValue, r.IDType.Value))

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

        Public Function Update(ByVal EmpOtherIDNoRow As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0
            Dim strDot As String

            For Each r As Row In EmpOtherIDNoRow
                strDot = String.Empty
                If strSQL.Length > 0 Then strSQL.Remove(0, strSQL.Length)
                strSQL.AppendLine("Update EmpOtherIDNo Set")
                For i As Integer = 0 To r.FieldNames.Length - 1
                    If Not r.IsIdentityField(r.FieldNames(i)) AndAlso r.IsUpdated(r.FieldNames(i)) AndAlso r.CreateUpdateSQL(r.FieldNames(i))
                        strSQL.AppendLine(String.Format("{0}{1} = @{1}", strDot, r.FieldNames(i)))
                        strDot = ","
                    End If
                Next
                If strDot = String.Empty Then Continue For
                strSQL.AppendLine("Where IDNo = @PKIDNo")
                strSQL.AppendLine("And OtherIDNo = @PKOtherIDNo")
                strSQL.AppendLine("And IDType = @PKIDType")

                dbcmd = db.GetSqlStringCommand(strSQL.ToString())
                If r.IDNo.Updated Then db.AddInParameter(dbcmd, "@IDNo", DbType.String, r.IDNo.Value)
                If r.OtherIDNo.Updated Then db.AddInParameter(dbcmd, "@OtherIDNo", DbType.String, r.OtherIDNo.Value)
                If r.IDType.Updated Then db.AddInParameter(dbcmd, "@IDType", DbType.String, r.IDType.Value)
                If r.IDExpireDate.Updated Then db.AddInParameter(dbcmd, "@IDExpireDate", DbType.Date, IIf(IsDateTimeNull(r.IDExpireDate.Value), Convert.ToDateTime("1900/1/1"), r.IDExpireDate.Value))
                If r.LastChgComp.Updated Then db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, r.LastChgComp.Value)
                If r.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, r.LastChgID.Value)
                If r.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(r.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), r.LastChgDate.Value))
                db.AddInParameter(dbcmd, "@PKIDNo", DbType.String, IIf(r.LoadFromDataRow, r.IDNo.OldValue, r.IDNo.Value))
                db.AddInParameter(dbcmd, "@PKOtherIDNo", DbType.String, IIf(r.LoadFromDataRow, r.OtherIDNo.OldValue, r.OtherIDNo.Value))
                db.AddInParameter(dbcmd, "@PKIDType", DbType.String, IIf(r.LoadFromDataRow, r.IDType.OldValue, r.IDType.Value))

                intRowsAffected += db.ExecuteNonQuery(dbcmd, tran)
            Next
            Return intRowsAffected
        End Function

        Public Function IsDataExists(ByVal EmpOtherIDNoRow As Row) As Boolean
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intCount As Integer = 0

            strSQL.AppendLine("Select Count(*) Cnt From EmpOtherIDNo")
            strSQL.AppendLine("Where IDNo = @IDNo")
            strSQL.AppendLine("And OtherIDNo = @OtherIDNo")
            strSQL.AppendLine("And IDType = @IDType")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@IDNo", DbType.String, EmpOtherIDNoRow.IDNo.Value)
            db.AddInParameter(dbcmd, "@OtherIDNo", DbType.String, EmpOtherIDNoRow.OtherIDNo.Value)
            db.AddInParameter(dbcmd, "@IDType", DbType.String, EmpOtherIDNoRow.IDType.Value)

            intCount = Convert.ToInt32(db.ExecuteScalar(dbcmd))
            Return IIf(intCount > 0, True, False)
        End Function

        Public Function IsDataExists(ByVal EmpOtherIDNoRow As Row, ByVal tran As DbTransaction) As Boolean
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intCount As Integer = 0

            strSQL.AppendLine("Select Count(*) Cnt From EmpOtherIDNo")
            strSQL.AppendLine("Where IDNo = @IDNo")
            strSQL.AppendLine("And OtherIDNo = @OtherIDNo")
            strSQL.AppendLine("And IDType = @IDType")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@IDNo", DbType.String, EmpOtherIDNoRow.IDNo.Value)
            db.AddInParameter(dbcmd, "@OtherIDNo", DbType.String, EmpOtherIDNoRow.OtherIDNo.Value)
            db.AddInParameter(dbcmd, "@IDType", DbType.String, EmpOtherIDNoRow.IDType.Value)

            intCount = Convert.ToInt32(db.ExecuteScalar(dbcmd, tran))
            Return IIf(intCount > 0, True, False)
        End Function

        Public Function QuerybyWhere(WhereCondition As String) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From EmpOtherIDNo")
            If WhereCondition <> String.Empty Then strSQL.AppendLine(WhereCondition)

            Return db.ExecuteDataSet(CommandType.Text, strSQL.ToString())
        End Function

        Public Function Insert(ByVal EmpOtherIDNoRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Insert into EmpOtherIDNo")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    IDNo, OtherIDNo, IDType, IDExpireDate, LastChgComp, LastChgID, LastChgDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @IDNo, @OtherIDNo, @IDType, @IDExpireDate, @LastChgComp, @LastChgID, @LastChgDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@IDNo", DbType.String, EmpOtherIDNoRow.IDNo.Value)
            db.AddInParameter(dbcmd, "@OtherIDNo", DbType.String, EmpOtherIDNoRow.OtherIDNo.Value)
            db.AddInParameter(dbcmd, "@IDType", DbType.String, EmpOtherIDNoRow.IDType.Value)
            db.AddInParameter(dbcmd, "@IDExpireDate", DbType.Date, IIf(IsDateTimeNull(EmpOtherIDNoRow.IDExpireDate.Value), Convert.ToDateTime("1900/1/1"), EmpOtherIDNoRow.IDExpireDate.Value))
            db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, EmpOtherIDNoRow.LastChgComp.Value)
            db.AddInParameter(dbcmd, "@LastChgID", DbType.String, EmpOtherIDNoRow.LastChgID.Value)
            db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(EmpOtherIDNoRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), EmpOtherIDNoRow.LastChgDate.Value))

            Return db.ExecuteNonQuery(dbcmd)
        End Function

        Public Function Insert(ByVal EmpOtherIDNoRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Insert into EmpOtherIDNo")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    IDNo, OtherIDNo, IDType, IDExpireDate, LastChgComp, LastChgID, LastChgDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @IDNo, @OtherIDNo, @IDType, @IDExpireDate, @LastChgComp, @LastChgID, @LastChgDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@IDNo", DbType.String, EmpOtherIDNoRow.IDNo.Value)
            db.AddInParameter(dbcmd, "@OtherIDNo", DbType.String, EmpOtherIDNoRow.OtherIDNo.Value)
            db.AddInParameter(dbcmd, "@IDType", DbType.String, EmpOtherIDNoRow.IDType.Value)
            db.AddInParameter(dbcmd, "@IDExpireDate", DbType.Date, IIf(IsDateTimeNull(EmpOtherIDNoRow.IDExpireDate.Value), Convert.ToDateTime("1900/1/1"), EmpOtherIDNoRow.IDExpireDate.Value))
            db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, EmpOtherIDNoRow.LastChgComp.Value)
            db.AddInParameter(dbcmd, "@LastChgID", DbType.String, EmpOtherIDNoRow.LastChgID.Value)
            db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(EmpOtherIDNoRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), EmpOtherIDNoRow.LastChgDate.Value))

            Return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function Insert(ByVal EmpOtherIDNoRow As Row()) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Insert into EmpOtherIDNo")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    IDNo, OtherIDNo, IDType, IDExpireDate, LastChgComp, LastChgID, LastChgDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @IDNo, @OtherIDNo, @IDType, @IDExpireDate, @LastChgComp, @LastChgID, @LastChgDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            Using cn As DbConnection = db.CreateConnection()
                cn.Open()
                Dim tran As DbTransaction = cn.BeginTransaction()
                Dim inTrans As Boolean = True

                Try
                    For Each r As Row In EmpOtherIDNoRow
                        dbcmd.Parameters.Clear()
                        db.AddInParameter(dbcmd, "@IDNo", DbType.String, r.IDNo.Value)
                        db.AddInParameter(dbcmd, "@OtherIDNo", DbType.String, r.OtherIDNo.Value)
                        db.AddInParameter(dbcmd, "@IDType", DbType.String, r.IDType.Value)
                        db.AddInParameter(dbcmd, "@IDExpireDate", DbType.Date, IIf(IsDateTimeNull(r.IDExpireDate.Value), Convert.ToDateTime("1900/1/1"), r.IDExpireDate.Value))
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

        Public Function Insert(ByVal EmpOtherIDNoRow As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Insert into EmpOtherIDNo")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    IDNo, OtherIDNo, IDType, IDExpireDate, LastChgComp, LastChgID, LastChgDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @IDNo, @OtherIDNo, @IDType, @IDExpireDate, @LastChgComp, @LastChgID, @LastChgDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())

            For Each r As Row In EmpOtherIDNoRow
                dbcmd.Parameters.Clear()
                db.AddInParameter(dbcmd, "@IDNo", DbType.String, r.IDNo.Value)
                db.AddInParameter(dbcmd, "@OtherIDNo", DbType.String, r.OtherIDNo.Value)
                db.AddInParameter(dbcmd, "@IDType", DbType.String, r.IDType.Value)
                db.AddInParameter(dbcmd, "@IDExpireDate", DbType.Date, IIf(IsDateTimeNull(r.IDExpireDate.Value), Convert.ToDateTime("1900/1/1"), r.IDExpireDate.Value))
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

