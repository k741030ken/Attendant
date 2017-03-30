'****************************************************************
' Table:Certification
' Created Date: 2015.06.11
'****************************************************************/
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports Microsoft.Practices.EnterpriseLibrary.Common
Imports Microsoft.Practices.EnterpriseLibrary.Data
Imports System.Data.Common
Imports System.Data

Namespace beCertification
    Public Class Table
        Private m_Rows As Rows = New Rows()
        Private m_Fields As String() = { "IDNo", "CertiDate", "LicenseName", "CategoryID", "Institution", "CertiTo", "SerialNum", "Remark", "CreatedDate" }
        Private m_Types As System.Type() = { GetType(String), GetType(Date), GetType(String), GetType(String), GetType(String), GetType(Date), GetType(String), GetType(String), GetType(Date) }
        Private m_PrimaryFields As String() = { "IDNo", "CertiDate", "LicenseName", "CategoryID", "Institution" }

        Public ReadOnly Property Rows() As beCertification.Rows 
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
        Public Sub Transfer2Row(CertificationTable As DataTable)
            For Each dr As DataRow In CertificationTable.Rows
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
                dr(m_Rows(i).CertiDate.FieldName) = m_Rows(i).CertiDate.Value
                dr(m_Rows(i).LicenseName.FieldName) = m_Rows(i).LicenseName.Value
                dr(m_Rows(i).CategoryID.FieldName) = m_Rows(i).CategoryID.Value
                dr(m_Rows(i).Institution.FieldName) = m_Rows(i).Institution.Value
                dr(m_Rows(i).CertiTo.FieldName) = m_Rows(i).CertiTo.Value
                dr(m_Rows(i).SerialNum.FieldName) = m_Rows(i).SerialNum.Value
                dr(m_Rows(i).Remark.FieldName) = m_Rows(i).Remark.Value
                dr(m_Rows(i).CreatedDate.FieldName) = m_Rows(i).CreatedDate.Value

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

        Public Sub Add(CertificationRow As Row)
            m_Rows.Add(CertificationRow)
        End Sub

        Public Sub Remove(CertificationRow As Row)
            If m_Rows.IndexOf(CertificationRow) >= 0 Then
                m_Rows.Remove(CertificationRow)
            End If
        End Sub

        Public Sub Dispose()
            m_Rows.Clear()
        End Sub

    End Class

    Public Class Row

        Private FI_IDNo As Field(Of String) = new Field(Of String)("IDNo", true)
        Private FI_CertiDate As Field(Of Date) = new Field(Of Date)("CertiDate", true)
        Private FI_LicenseName As Field(Of String) = new Field(Of String)("LicenseName", true)
        Private FI_CategoryID As Field(Of String) = new Field(Of String)("CategoryID", true)
        Private FI_Institution As Field(Of String) = new Field(Of String)("Institution", true)
        Private FI_CertiTo As Field(Of Date) = new Field(Of Date)("CertiTo", true)
        Private FI_SerialNum As Field(Of String) = new Field(Of String)("SerialNum", true)
        Private FI_Remark As Field(Of String) = new Field(Of String)("Remark", true)
        Private FI_CreatedDate As Field(Of Date) = new Field(Of Date)("CreatedDate", true)
        Private m_FieldNames As String() = { "IDNo", "CertiDate", "LicenseName", "CategoryID", "Institution", "CertiTo", "SerialNum", "Remark", "CreatedDate" }
        Private m_PrimaryFields As String() = { "IDNo", "CertiDate", "LicenseName", "CategoryID", "Institution" }
        Private m_IdentityFields As String() = {  }
        Private m_LoadFromDataRow As Boolean = False

        Private Function GetFieldValue(ByVal fieldName As String) As Object
            Select Case fieldName
                Case "IDNo"
                    Return FI_IDNo.Value
                Case "CertiDate"
                    Return FI_CertiDate.Value
                Case "LicenseName"
                    Return FI_LicenseName.Value
                Case "CategoryID"
                    Return FI_CategoryID.Value
                Case "Institution"
                    Return FI_Institution.Value
                Case "CertiTo"
                    Return FI_CertiTo.Value
                Case "SerialNum"
                    Return FI_SerialNum.Value
                Case "Remark"
                    Return FI_Remark.Value
                Case "CreatedDate"
                    Return FI_CreatedDate.Value
                Case Else
                    Return Nothing
            End Select
        End Function

        Private Sub SetFieldValue(ByVal fieldName As String, ByVal value As Object)
            Select Case fieldName
                Case "IDNo"
                    FI_IDNo.SetValue(value)
                Case "CertiDate"
                    FI_CertiDate.SetValue(value)
                Case "LicenseName"
                    FI_LicenseName.SetValue(value)
                Case "CategoryID"
                    FI_CategoryID.SetValue(value)
                Case "Institution"
                    FI_Institution.SetValue(value)
                Case "CertiTo"
                    FI_CertiTo.SetValue(value)
                Case "SerialNum"
                    FI_SerialNum.SetValue(value)
                Case "Remark"
                    FI_Remark.SetValue(value)
                Case "CreatedDate"
                    FI_CreatedDate.SetValue(value)
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
                Case "CertiDate"
                    return FI_CertiDate.Updated
                Case "LicenseName"
                    return FI_LicenseName.Updated
                Case "CategoryID"
                    return FI_CategoryID.Updated
                Case "Institution"
                    return FI_Institution.Updated
                Case "CertiTo"
                    return FI_CertiTo.Updated
                Case "SerialNum"
                    return FI_SerialNum.Updated
                Case "Remark"
                    return FI_Remark.Updated
                Case "CreatedDate"
                    return FI_CreatedDate.Updated
                Case Else
                    Throw New Exception("無此欄位！")
            End Select
        End Function

        Public Function CreateUpdateSQL(ByVal fieldName As String) As Boolean
            Select Case fieldName
                Case "IDNo"
                    return FI_IDNo.CreateUpdateSQL
                Case "CertiDate"
                    return FI_CertiDate.CreateUpdateSQL
                Case "LicenseName"
                    return FI_LicenseName.CreateUpdateSQL
                Case "CategoryID"
                    return FI_CategoryID.CreateUpdateSQL
                Case "Institution"
                    return FI_Institution.CreateUpdateSQL
                Case "CertiTo"
                    return FI_CertiTo.CreateUpdateSQL
                Case "SerialNum"
                    return FI_SerialNum.CreateUpdateSQL
                Case "Remark"
                    return FI_Remark.CreateUpdateSQL
                Case "CreatedDate"
                    return FI_CreatedDate.CreateUpdateSQL
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
            FI_CertiDate.SetInitValue(Convert.ToDateTime("1900/01/01"))
            FI_LicenseName.SetInitValue("")
            FI_CategoryID.SetInitValue("")
            FI_Institution.SetInitValue("")
            FI_CertiTo.SetInitValue(Convert.ToDateTime("1900/01/01"))
            FI_SerialNum.SetInitValue("")
            FI_Remark.SetInitValue("")
            FI_CreatedDate.SetInitValue(Convert.ToDateTime("1900/01/01"))
        End Sub

        Public Sub New(ByVal dr As System.Data.DataRow)
            FI_IDNo.SetInitValue(dr("IDNo"))
            FI_CertiDate.SetInitValue(dr("CertiDate"))
            FI_LicenseName.SetInitValue(dr("LicenseName"))
            FI_CategoryID.SetInitValue(dr("CategoryID"))
            FI_Institution.SetInitValue(dr("Institution"))
            FI_CertiTo.SetInitValue(dr("CertiTo"))
            FI_SerialNum.SetInitValue(dr("SerialNum"))
            FI_Remark.SetInitValue(dr("Remark"))
            FI_CreatedDate.SetInitValue(dr("CreatedDate"))

            m_LoadFromDataRow = True
        End Sub

        Private Sub ClearUpdated()
            FI_IDNo.Updated = False
            FI_CertiDate.Updated = False
            FI_LicenseName.Updated = False
            FI_CategoryID.Updated = False
            FI_Institution.Updated = False
            FI_CertiTo.Updated = False
            FI_SerialNum.Updated = False
            FI_Remark.Updated = False
            FI_CreatedDate.Updated = False
        End Sub

        Public ReadOnly Property IDNo As Field(Of String) 
            Get
                Return FI_IDNo
            End Get
        End Property

        Public ReadOnly Property CertiDate As Field(Of Date) 
            Get
                Return FI_CertiDate
            End Get
        End Property

        Public ReadOnly Property LicenseName As Field(Of String) 
            Get
                Return FI_LicenseName
            End Get
        End Property

        Public ReadOnly Property CategoryID As Field(Of String) 
            Get
                Return FI_CategoryID
            End Get
        End Property

        Public ReadOnly Property Institution As Field(Of String) 
            Get
                Return FI_Institution
            End Get
        End Property

        Public ReadOnly Property CertiTo As Field(Of Date) 
            Get
                Return FI_CertiTo
            End Get
        End Property

        Public ReadOnly Property SerialNum As Field(Of String) 
            Get
                Return FI_SerialNum
            End Get
        End Property

        Public ReadOnly Property Remark As Field(Of String) 
            Get
                Return FI_Remark
            End Get
        End Property

        Public ReadOnly Property CreatedDate As Field(Of Date) 
            Get
                Return FI_CreatedDate
            End Get
        End Property

    End Class

    Public Class Service
        Public Function DeleteRowByPrimaryKey(ByVal CertificationRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Delete From Certification")
            strSQL.AppendLine("Where IDNo = @IDNo")
            strSQL.AppendLine("And CertiDate = @CertiDate")
            strSQL.AppendLine("And LicenseName = @LicenseName")
            strSQL.AppendLine("And CategoryID = @CategoryID")
            strSQL.AppendLine("And Institution = @Institution")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@IDNo", DbType.String, CertificationRow.IDNo.Value)
            db.AddInParameter(dbcmd, "@CertiDate", DbType.Date, CertificationRow.CertiDate.Value)
            db.AddInParameter(dbcmd, "@LicenseName", DbType.String, CertificationRow.LicenseName.Value)
            db.AddInParameter(dbcmd, "@CategoryID", DbType.String, CertificationRow.CategoryID.Value)
            db.AddInParameter(dbcmd, "@Institution", DbType.String, CertificationRow.Institution.Value)

            return db.ExecuteNonQuery(dbcmd)
        End Function

        public Function DeleteRowByPrimaryKey(ByVal CertificationRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Delete From Certification")
            strSQL.AppendLine("Where IDNo = @IDNo")
            strSQL.AppendLine("And CertiDate = @CertiDate")
            strSQL.AppendLine("And LicenseName = @LicenseName")
            strSQL.AppendLine("And CategoryID = @CategoryID")
            strSQL.AppendLine("And Institution = @Institution")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@IDNo", DbType.String, CertificationRow.IDNo.Value)
            db.AddInParameter(dbcmd, "@CertiDate", DbType.Date, CertificationRow.CertiDate.Value)
            db.AddInParameter(dbcmd, "@LicenseName", DbType.String, CertificationRow.LicenseName.Value)
            db.AddInParameter(dbcmd, "@CategoryID", DbType.String, CertificationRow.CategoryID.Value)
            db.AddInParameter(dbcmd, "@Institution", DbType.String, CertificationRow.Institution.Value)

            return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function DeleteRowByPrimaryKey(ByVal CertificationRow As Row()) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Delete From Certification")
            strSQL.AppendLine("Where IDNo = @IDNo")
            strSQL.AppendLine("And CertiDate = @CertiDate")
            strSQL.AppendLine("And LicenseName = @LicenseName")
            strSQL.AppendLine("And CategoryID = @CategoryID")
            strSQL.AppendLine("And Institution = @Institution")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            using cn As DbConnection = db.CreateConnection()
                cn.Open()
                Dim tran As DbTransaction = cn.BeginTransaction()
                Dim inTrans As Boolean = True

                Try
                    For Each r As Row In CertificationRow
                        dbcmd.Parameters.Clear()
                        db.AddInParameter(dbcmd, "@IDNo", DbType.String, r.IDNo.Value)
                        db.AddInParameter(dbcmd, "@CertiDate", DbType.Date, r.CertiDate.Value)
                        db.AddInParameter(dbcmd, "@LicenseName", DbType.String, r.LicenseName.Value)
                        db.AddInParameter(dbcmd, "@CategoryID", DbType.String, r.CategoryID.Value)
                        db.AddInParameter(dbcmd, "@Institution", DbType.String, r.Institution.Value)

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

        Public Function DeleteRowByPrimaryKey(ByVal CertificationRow As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Delete From Certification")
            strSQL.AppendLine("Where IDNo = @IDNo")
            strSQL.AppendLine("And CertiDate = @CertiDate")
            strSQL.AppendLine("And LicenseName = @LicenseName")
            strSQL.AppendLine("And CategoryID = @CategoryID")
            strSQL.AppendLine("And Institution = @Institution")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())

            For Each r As Row In CertificationRow
                dbcmd.Parameters.Clear()
                db.AddInParameter(dbcmd, "@IDNo", DbType.String, r.IDNo.Value)
                db.AddInParameter(dbcmd, "@CertiDate", DbType.Date, r.CertiDate.Value)
                db.AddInParameter(dbcmd, "@LicenseName", DbType.String, r.LicenseName.Value)
                db.AddInParameter(dbcmd, "@CategoryID", DbType.String, r.CategoryID.Value)
                db.AddInParameter(dbcmd, "@Institution", DbType.String, r.Institution.Value)

                intRowsAffected += db.ExecuteNonQuery(dbcmd, tran)
            Next
            Return intRowsAffected
        End Function

        Public Function QueryByKey(ByVal CertificationRow As Row) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From Certification")
            strSQL.AppendLine("Where IDNo = @IDNo")
            strSQL.AppendLine("And CertiDate = @CertiDate")
            strSQL.AppendLine("And LicenseName = @LicenseName")
            strSQL.AppendLine("And CategoryID = @CategoryID")
            strSQL.AppendLine("And Institution = @Institution")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@IDNo", DbType.String, CertificationRow.IDNo.Value)
            db.AddInParameter(dbcmd, "@CertiDate", DbType.Date, CertificationRow.CertiDate.Value)
            db.AddInParameter(dbcmd, "@LicenseName", DbType.String, CertificationRow.LicenseName.Value)
            db.AddInParameter(dbcmd, "@CategoryID", DbType.String, CertificationRow.CategoryID.Value)
            db.AddInParameter(dbcmd, "@Institution", DbType.String, CertificationRow.Institution.Value)

            Return db.ExecuteDataSet(dbcmd)
        End Function

        Public Function QueryByKey(CertificationRow As Row, tran As DbTransaction) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From Certification")
            strSQL.AppendLine("Where IDNo = @IDNo")
            strSQL.AppendLine("And CertiDate = @CertiDate")
            strSQL.AppendLine("And LicenseName = @LicenseName")
            strSQL.AppendLine("And CategoryID = @CategoryID")
            strSQL.AppendLine("And Institution = @Institution")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@IDNo", DbType.String, CertificationRow.IDNo.Value)
            db.AddInParameter(dbcmd, "@CertiDate", DbType.Date, CertificationRow.CertiDate.Value)
            db.AddInParameter(dbcmd, "@LicenseName", DbType.String, CertificationRow.LicenseName.Value)
            db.AddInParameter(dbcmd, "@CategoryID", DbType.String, CertificationRow.CategoryID.Value)
            db.AddInParameter(dbcmd, "@Institution", DbType.String, CertificationRow.Institution.Value)

            Return db.ExecuteDataSet(dbcmd, tran)
        End Function

        Public Function Update(ByVal CertificationRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim strDot As String = String.Empty

            strSQL.AppendLine("Update Certification Set")
            For i As Integer = 0 To CertificationRow.FieldNames.Length - 1
                If Not CertificationRow.IsIdentityField(CertificationRow.FieldNames(i)) AndAlso CertificationRow.IsUpdated(CertificationRow.FieldNames(i)) AndAlso CertificationRow.CreateUpdateSQL(CertificationRow.FieldNames(i)) Then
                    strSQL.AppendLine(string.Format("{0}{1} = @{1}", strDot, CertificationRow.FieldNames(i)))
                    strDot = ","
                End If
            Next
            If strDot = String.Empty Then Throw New Exception("未異動資料欄位！")
            strSQL.AppendLine("Where IDNo = @PKIDNo")
            strSQL.AppendLine("And CertiDate = @PKCertiDate")
            strSQL.AppendLine("And LicenseName = @PKLicenseName")
            strSQL.AppendLine("And CategoryID = @PKCategoryID")
            strSQL.AppendLine("And Institution = @PKInstitution")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            If CertificationRow.IDNo.Updated Then db.AddInParameter(dbcmd, "@IDNo", DbType.String, CertificationRow.IDNo.Value)
            If CertificationRow.CertiDate.Updated Then db.AddInParameter(dbcmd, "@CertiDate", DbType.Date, IIf(IsDateTimeNull(CertificationRow.CertiDate.Value), Convert.ToDateTime("1900/1/1"), CertificationRow.CertiDate.Value))
            If CertificationRow.LicenseName.Updated Then db.AddInParameter(dbcmd, "@LicenseName", DbType.String, CertificationRow.LicenseName.Value)
            If CertificationRow.CategoryID.Updated Then db.AddInParameter(dbcmd, "@CategoryID", DbType.String, CertificationRow.CategoryID.Value)
            If CertificationRow.Institution.Updated Then db.AddInParameter(dbcmd, "@Institution", DbType.String, CertificationRow.Institution.Value)
            If CertificationRow.CertiTo.Updated Then db.AddInParameter(dbcmd, "@CertiTo", DbType.Date, IIf(IsDateTimeNull(CertificationRow.CertiTo.Value), Convert.ToDateTime("1900/1/1"), CertificationRow.CertiTo.Value))
            If CertificationRow.SerialNum.Updated Then db.AddInParameter(dbcmd, "@SerialNum", DbType.String, CertificationRow.SerialNum.Value)
            If CertificationRow.Remark.Updated Then db.AddInParameter(dbcmd, "@Remark", DbType.String, CertificationRow.Remark.Value)
            If CertificationRow.CreatedDate.Updated Then db.AddInParameter(dbcmd, "@CreatedDate", DbType.Date, IIf(IsDateTimeNull(CertificationRow.CreatedDate.Value), Convert.ToDateTime("1900/1/1"), CertificationRow.CreatedDate.Value))
            db.AddInParameter(dbcmd, "@PKIDNo", DbType.String, IIf(CertificationRow.LoadFromDataRow, CertificationRow.IDNo.OldValue, CertificationRow.IDNo.Value))
            db.AddInParameter(dbcmd, "@PKCertiDate", DbType.Date, IIf(CertificationRow.LoadFromDataRow, CertificationRow.CertiDate.OldValue, CertificationRow.CertiDate.Value))
            db.AddInParameter(dbcmd, "@PKLicenseName", DbType.String, IIf(CertificationRow.LoadFromDataRow, CertificationRow.LicenseName.OldValue, CertificationRow.LicenseName.Value))
            db.AddInParameter(dbcmd, "@PKCategoryID", DbType.String, IIf(CertificationRow.LoadFromDataRow, CertificationRow.CategoryID.OldValue, CertificationRow.CategoryID.Value))
            db.AddInParameter(dbcmd, "@PKInstitution", DbType.String, IIf(CertificationRow.LoadFromDataRow, CertificationRow.Institution.OldValue, CertificationRow.Institution.Value))

            Return db.ExecuteNonQuery(dbcmd)
        End Function

        Public Function Update(ByVal CertificationRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim strDot As String = String.Empty

            strSQL.AppendLine("Update Certification Set")
            For i As Integer = 0 To CertificationRow.FieldNames.Length - 1
                If Not CertificationRow.IsIdentityField(CertificationRow.FieldNames(i)) AndAlso CertificationRow.IsUpdated(CertificationRow.FieldNames(i)) AndAlso CertificationRow.CreateUpdateSQL(CertificationRow.FieldNames(i)) Then
                    strSQL.AppendLine(string.Format("{0}{1} = @{1}", strDot, CertificationRow.FieldNames(i)))
                    strDot = ","
                End If
            Next 
            If strDot = String.Empty Then Throw New Exception("未異動資料欄位！")
            strSQL.AppendLine("Where IDNo = @PKIDNo")
            strSQL.AppendLine("And CertiDate = @PKCertiDate")
            strSQL.AppendLine("And LicenseName = @PKLicenseName")
            strSQL.AppendLine("And CategoryID = @PKCategoryID")
            strSQL.AppendLine("And Institution = @PKInstitution")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            If CertificationRow.IDNo.Updated Then db.AddInParameter(dbcmd, "@IDNo", DbType.String, CertificationRow.IDNo.Value)
            If CertificationRow.CertiDate.Updated Then db.AddInParameter(dbcmd, "@CertiDate", DbType.Date, IIf(IsDateTimeNull(CertificationRow.CertiDate.Value), Convert.ToDateTime("1900/1/1"), CertificationRow.CertiDate.Value))
            If CertificationRow.LicenseName.Updated Then db.AddInParameter(dbcmd, "@LicenseName", DbType.String, CertificationRow.LicenseName.Value)
            If CertificationRow.CategoryID.Updated Then db.AddInParameter(dbcmd, "@CategoryID", DbType.String, CertificationRow.CategoryID.Value)
            If CertificationRow.Institution.Updated Then db.AddInParameter(dbcmd, "@Institution", DbType.String, CertificationRow.Institution.Value)
            If CertificationRow.CertiTo.Updated Then db.AddInParameter(dbcmd, "@CertiTo", DbType.Date, IIf(IsDateTimeNull(CertificationRow.CertiTo.Value), Convert.ToDateTime("1900/1/1"), CertificationRow.CertiTo.Value))
            If CertificationRow.SerialNum.Updated Then db.AddInParameter(dbcmd, "@SerialNum", DbType.String, CertificationRow.SerialNum.Value)
            If CertificationRow.Remark.Updated Then db.AddInParameter(dbcmd, "@Remark", DbType.String, CertificationRow.Remark.Value)
            If CertificationRow.CreatedDate.Updated Then db.AddInParameter(dbcmd, "@CreatedDate", DbType.Date, IIf(IsDateTimeNull(CertificationRow.CreatedDate.Value), Convert.ToDateTime("1900/1/1"), CertificationRow.CreatedDate.Value))
            db.AddInParameter(dbcmd, "@PKIDNo", DbType.String, IIf(CertificationRow.LoadFromDataRow, CertificationRow.IDNo.OldValue, CertificationRow.IDNo.Value))
            db.AddInParameter(dbcmd, "@PKCertiDate", DbType.Date, IIf(CertificationRow.LoadFromDataRow, CertificationRow.CertiDate.OldValue, CertificationRow.CertiDate.Value))
            db.AddInParameter(dbcmd, "@PKLicenseName", DbType.String, IIf(CertificationRow.LoadFromDataRow, CertificationRow.LicenseName.OldValue, CertificationRow.LicenseName.Value))
            db.AddInParameter(dbcmd, "@PKCategoryID", DbType.String, IIf(CertificationRow.LoadFromDataRow, CertificationRow.CategoryID.OldValue, CertificationRow.CategoryID.Value))
            db.AddInParameter(dbcmd, "@PKInstitution", DbType.String, IIf(CertificationRow.LoadFromDataRow, CertificationRow.Institution.OldValue, CertificationRow.Institution.Value))

            Return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function Update(ByVal CertificationRow As Row()) As Integer
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
                    For Each r As Row In CertificationRow
                        strDot = String.Empty
                        If strSQL.Length > 0 Then strSQL.Remove(0, strSQL.Length)
                        strSQL.AppendLine("Update Certification Set")
                        For i As Integer = 0 To r.FieldNames.Length - 1
                            If Not r.IsIdentityField(r.FieldNames(i)) AndAlso r.IsUpdated(r.FieldNames(i)) AndAlso r.CreateUpdateSQL(r.FieldNames(i)) Then
                                strSQL.AppendLine(String.Format("{0}{1} = @{1}", strDot, r.FieldNames(i)))
                                strDot = ","
                            End If
                        Next
                        If strDot = String.Empty Then Continue For
                        strSQL.AppendLine("Where IDNo = @PKIDNo")
                        strSQL.AppendLine("And CertiDate = @PKCertiDate")
                        strSQL.AppendLine("And LicenseName = @PKLicenseName")
                        strSQL.AppendLine("And CategoryID = @PKCategoryID")
                        strSQL.AppendLine("And Institution = @PKInstitution")

                        dbcmd = db.GetSqlStringCommand(strSQL.ToString())
                        If r.IDNo.Updated Then db.AddInParameter(dbcmd, "@IDNo", DbType.String, r.IDNo.Value)
                        If r.CertiDate.Updated Then db.AddInParameter(dbcmd, "@CertiDate", DbType.Date, IIf(IsDateTimeNull(r.CertiDate.Value), Convert.ToDateTime("1900/1/1"), r.CertiDate.Value))
                        If r.LicenseName.Updated Then db.AddInParameter(dbcmd, "@LicenseName", DbType.String, r.LicenseName.Value)
                        If r.CategoryID.Updated Then db.AddInParameter(dbcmd, "@CategoryID", DbType.String, r.CategoryID.Value)
                        If r.Institution.Updated Then db.AddInParameter(dbcmd, "@Institution", DbType.String, r.Institution.Value)
                        If r.CertiTo.Updated Then db.AddInParameter(dbcmd, "@CertiTo", DbType.Date, IIf(IsDateTimeNull(r.CertiTo.Value), Convert.ToDateTime("1900/1/1"), r.CertiTo.Value))
                        If r.SerialNum.Updated Then db.AddInParameter(dbcmd, "@SerialNum", DbType.String, r.SerialNum.Value)
                        If r.Remark.Updated Then db.AddInParameter(dbcmd, "@Remark", DbType.String, r.Remark.Value)
                        If r.CreatedDate.Updated Then db.AddInParameter(dbcmd, "@CreatedDate", DbType.Date, IIf(IsDateTimeNull(r.CreatedDate.Value), Convert.ToDateTime("1900/1/1"), r.CreatedDate.Value))
                        db.AddInParameter(dbcmd, "@PKIDNo", DbType.String, IIf(r.LoadFromDataRow, r.IDNo.OldValue, r.IDNo.Value))
                        db.AddInParameter(dbcmd, "@PKCertiDate", DbType.Date, IIf(r.LoadFromDataRow, r.CertiDate.OldValue, r.CertiDate.Value))
                        db.AddInParameter(dbcmd, "@PKLicenseName", DbType.String, IIf(r.LoadFromDataRow, r.LicenseName.OldValue, r.LicenseName.Value))
                        db.AddInParameter(dbcmd, "@PKCategoryID", DbType.String, IIf(r.LoadFromDataRow, r.CategoryID.OldValue, r.CategoryID.Value))
                        db.AddInParameter(dbcmd, "@PKInstitution", DbType.String, IIf(r.LoadFromDataRow, r.Institution.OldValue, r.Institution.Value))

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

        Public Function Update(ByVal CertificationRow As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0
            Dim strDot As String

            For Each r As Row In CertificationRow
                strDot = String.Empty
                If strSQL.Length > 0 Then strSQL.Remove(0, strSQL.Length)
                strSQL.AppendLine("Update Certification Set")
                For i As Integer = 0 To r.FieldNames.Length - 1
                    If Not r.IsIdentityField(r.FieldNames(i)) AndAlso r.IsUpdated(r.FieldNames(i)) AndAlso r.CreateUpdateSQL(r.FieldNames(i))
                        strSQL.AppendLine(String.Format("{0}{1} = @{1}", strDot, r.FieldNames(i)))
                        strDot = ","
                    End If
                Next
                If strDot = String.Empty Then Continue For
                strSQL.AppendLine("Where IDNo = @PKIDNo")
                strSQL.AppendLine("And CertiDate = @PKCertiDate")
                strSQL.AppendLine("And LicenseName = @PKLicenseName")
                strSQL.AppendLine("And CategoryID = @PKCategoryID")
                strSQL.AppendLine("And Institution = @PKInstitution")

                dbcmd = db.GetSqlStringCommand(strSQL.ToString())
                If r.IDNo.Updated Then db.AddInParameter(dbcmd, "@IDNo", DbType.String, r.IDNo.Value)
                If r.CertiDate.Updated Then db.AddInParameter(dbcmd, "@CertiDate", DbType.Date, IIf(IsDateTimeNull(r.CertiDate.Value), Convert.ToDateTime("1900/1/1"), r.CertiDate.Value))
                If r.LicenseName.Updated Then db.AddInParameter(dbcmd, "@LicenseName", DbType.String, r.LicenseName.Value)
                If r.CategoryID.Updated Then db.AddInParameter(dbcmd, "@CategoryID", DbType.String, r.CategoryID.Value)
                If r.Institution.Updated Then db.AddInParameter(dbcmd, "@Institution", DbType.String, r.Institution.Value)
                If r.CertiTo.Updated Then db.AddInParameter(dbcmd, "@CertiTo", DbType.Date, IIf(IsDateTimeNull(r.CertiTo.Value), Convert.ToDateTime("1900/1/1"), r.CertiTo.Value))
                If r.SerialNum.Updated Then db.AddInParameter(dbcmd, "@SerialNum", DbType.String, r.SerialNum.Value)
                If r.Remark.Updated Then db.AddInParameter(dbcmd, "@Remark", DbType.String, r.Remark.Value)
                If r.CreatedDate.Updated Then db.AddInParameter(dbcmd, "@CreatedDate", DbType.Date, IIf(IsDateTimeNull(r.CreatedDate.Value), Convert.ToDateTime("1900/1/1"), r.CreatedDate.Value))
                db.AddInParameter(dbcmd, "@PKIDNo", DbType.String, IIf(r.LoadFromDataRow, r.IDNo.OldValue, r.IDNo.Value))
                db.AddInParameter(dbcmd, "@PKCertiDate", DbType.Date, IIf(r.LoadFromDataRow, r.CertiDate.OldValue, r.CertiDate.Value))
                db.AddInParameter(dbcmd, "@PKLicenseName", DbType.String, IIf(r.LoadFromDataRow, r.LicenseName.OldValue, r.LicenseName.Value))
                db.AddInParameter(dbcmd, "@PKCategoryID", DbType.String, IIf(r.LoadFromDataRow, r.CategoryID.OldValue, r.CategoryID.Value))
                db.AddInParameter(dbcmd, "@PKInstitution", DbType.String, IIf(r.LoadFromDataRow, r.Institution.OldValue, r.Institution.Value))

                intRowsAffected += db.ExecuteNonQuery(dbcmd, tran)
            Next
            Return intRowsAffected
        End Function

        Public Function IsDataExists(ByVal CertificationRow As Row) As Boolean
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intCount As Integer = 0

            strSQL.AppendLine("Select Count(*) Cnt From Certification")
            strSQL.AppendLine("Where IDNo = @IDNo")
            strSQL.AppendLine("And CertiDate = @CertiDate")
            strSQL.AppendLine("And LicenseName = @LicenseName")
            strSQL.AppendLine("And CategoryID = @CategoryID")
            strSQL.AppendLine("And Institution = @Institution")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@IDNo", DbType.String, CertificationRow.IDNo.Value)
            db.AddInParameter(dbcmd, "@CertiDate", DbType.Date, CertificationRow.CertiDate.Value)
            db.AddInParameter(dbcmd, "@LicenseName", DbType.String, CertificationRow.LicenseName.Value)
            db.AddInParameter(dbcmd, "@CategoryID", DbType.String, CertificationRow.CategoryID.Value)
            db.AddInParameter(dbcmd, "@Institution", DbType.String, CertificationRow.Institution.Value)

            intCount = Convert.ToInt32(db.ExecuteScalar(dbcmd))
            Return IIf(intCount > 0, True, False)
        End Function

        Public Function IsDataExists(ByVal CertificationRow As Row, ByVal tran As DbTransaction) As Boolean
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intCount As Integer = 0

            strSQL.AppendLine("Select Count(*) Cnt From Certification")
            strSQL.AppendLine("Where IDNo = @IDNo")
            strSQL.AppendLine("And CertiDate = @CertiDate")
            strSQL.AppendLine("And LicenseName = @LicenseName")
            strSQL.AppendLine("And CategoryID = @CategoryID")
            strSQL.AppendLine("And Institution = @Institution")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@IDNo", DbType.String, CertificationRow.IDNo.Value)
            db.AddInParameter(dbcmd, "@CertiDate", DbType.Date, CertificationRow.CertiDate.Value)
            db.AddInParameter(dbcmd, "@LicenseName", DbType.String, CertificationRow.LicenseName.Value)
            db.AddInParameter(dbcmd, "@CategoryID", DbType.String, CertificationRow.CategoryID.Value)
            db.AddInParameter(dbcmd, "@Institution", DbType.String, CertificationRow.Institution.Value)

            intCount = Convert.ToInt32(db.ExecuteScalar(dbcmd, tran))
            Return IIf(intCount > 0, True, False)
        End Function

        Public Function QuerybyWhere(WhereCondition As String) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From Certification")
            If WhereCondition <> String.Empty Then strSQL.AppendLine(WhereCondition)

            Return db.ExecuteDataSet(CommandType.Text, strSQL.ToString())
        End Function

        Public Function Insert(ByVal CertificationRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Insert into Certification")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    IDNo, CertiDate, LicenseName, CategoryID, Institution, CertiTo, SerialNum, Remark,")
            strSQL.AppendLine("    CreatedDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @IDNo, @CertiDate, @LicenseName, @CategoryID, @Institution, @CertiTo, @SerialNum, @Remark,")
            strSQL.AppendLine("    @CreatedDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@IDNo", DbType.String, CertificationRow.IDNo.Value)
            db.AddInParameter(dbcmd, "@CertiDate", DbType.Date, IIf(IsDateTimeNull(CertificationRow.CertiDate.Value), Convert.ToDateTime("1900/1/1"), CertificationRow.CertiDate.Value))
            db.AddInParameter(dbcmd, "@LicenseName", DbType.String, CertificationRow.LicenseName.Value)
            db.AddInParameter(dbcmd, "@CategoryID", DbType.String, CertificationRow.CategoryID.Value)
            db.AddInParameter(dbcmd, "@Institution", DbType.String, CertificationRow.Institution.Value)
            db.AddInParameter(dbcmd, "@CertiTo", DbType.Date, IIf(IsDateTimeNull(CertificationRow.CertiTo.Value), Convert.ToDateTime("1900/1/1"), CertificationRow.CertiTo.Value))
            db.AddInParameter(dbcmd, "@SerialNum", DbType.String, CertificationRow.SerialNum.Value)
            db.AddInParameter(dbcmd, "@Remark", DbType.String, CertificationRow.Remark.Value)
            db.AddInParameter(dbcmd, "@CreatedDate", DbType.Date, IIf(IsDateTimeNull(CertificationRow.CreatedDate.Value), Convert.ToDateTime("1900/1/1"), CertificationRow.CreatedDate.Value))

            Return db.ExecuteNonQuery(dbcmd)
        End Function

        Public Function Insert(ByVal CertificationRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Insert into Certification")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    IDNo, CertiDate, LicenseName, CategoryID, Institution, CertiTo, SerialNum, Remark,")
            strSQL.AppendLine("    CreatedDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @IDNo, @CertiDate, @LicenseName, @CategoryID, @Institution, @CertiTo, @SerialNum, @Remark,")
            strSQL.AppendLine("    @CreatedDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@IDNo", DbType.String, CertificationRow.IDNo.Value)
            db.AddInParameter(dbcmd, "@CertiDate", DbType.Date, IIf(IsDateTimeNull(CertificationRow.CertiDate.Value), Convert.ToDateTime("1900/1/1"), CertificationRow.CertiDate.Value))
            db.AddInParameter(dbcmd, "@LicenseName", DbType.String, CertificationRow.LicenseName.Value)
            db.AddInParameter(dbcmd, "@CategoryID", DbType.String, CertificationRow.CategoryID.Value)
            db.AddInParameter(dbcmd, "@Institution", DbType.String, CertificationRow.Institution.Value)
            db.AddInParameter(dbcmd, "@CertiTo", DbType.Date, IIf(IsDateTimeNull(CertificationRow.CertiTo.Value), Convert.ToDateTime("1900/1/1"), CertificationRow.CertiTo.Value))
            db.AddInParameter(dbcmd, "@SerialNum", DbType.String, CertificationRow.SerialNum.Value)
            db.AddInParameter(dbcmd, "@Remark", DbType.String, CertificationRow.Remark.Value)
            db.AddInParameter(dbcmd, "@CreatedDate", DbType.Date, IIf(IsDateTimeNull(CertificationRow.CreatedDate.Value), Convert.ToDateTime("1900/1/1"), CertificationRow.CreatedDate.Value))

            Return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function Insert(ByVal CertificationRow As Row()) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Insert into Certification")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    IDNo, CertiDate, LicenseName, CategoryID, Institution, CertiTo, SerialNum, Remark,")
            strSQL.AppendLine("    CreatedDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @IDNo, @CertiDate, @LicenseName, @CategoryID, @Institution, @CertiTo, @SerialNum, @Remark,")
            strSQL.AppendLine("    @CreatedDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            Using cn As DbConnection = db.CreateConnection()
                cn.Open()
                Dim tran As DbTransaction = cn.BeginTransaction()
                Dim inTrans As Boolean = True

                Try
                    For Each r As Row In CertificationRow
                        dbcmd.Parameters.Clear()
                        db.AddInParameter(dbcmd, "@IDNo", DbType.String, r.IDNo.Value)
                        db.AddInParameter(dbcmd, "@CertiDate", DbType.Date, IIf(IsDateTimeNull(r.CertiDate.Value), Convert.ToDateTime("1900/1/1"), r.CertiDate.Value))
                        db.AddInParameter(dbcmd, "@LicenseName", DbType.String, r.LicenseName.Value)
                        db.AddInParameter(dbcmd, "@CategoryID", DbType.String, r.CategoryID.Value)
                        db.AddInParameter(dbcmd, "@Institution", DbType.String, r.Institution.Value)
                        db.AddInParameter(dbcmd, "@CertiTo", DbType.Date, IIf(IsDateTimeNull(r.CertiTo.Value), Convert.ToDateTime("1900/1/1"), r.CertiTo.Value))
                        db.AddInParameter(dbcmd, "@SerialNum", DbType.String, r.SerialNum.Value)
                        db.AddInParameter(dbcmd, "@Remark", DbType.String, r.Remark.Value)
                        db.AddInParameter(dbcmd, "@CreatedDate", DbType.Date, IIf(IsDateTimeNull(r.CreatedDate.Value), Convert.ToDateTime("1900/1/1"), r.CreatedDate.Value))

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

        Public Function Insert(ByVal CertificationRow As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Insert into Certification")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    IDNo, CertiDate, LicenseName, CategoryID, Institution, CertiTo, SerialNum, Remark,")
            strSQL.AppendLine("    CreatedDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @IDNo, @CertiDate, @LicenseName, @CategoryID, @Institution, @CertiTo, @SerialNum, @Remark,")
            strSQL.AppendLine("    @CreatedDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())

            For Each r As Row In CertificationRow
                dbcmd.Parameters.Clear()
                db.AddInParameter(dbcmd, "@IDNo", DbType.String, r.IDNo.Value)
                db.AddInParameter(dbcmd, "@CertiDate", DbType.Date, IIf(IsDateTimeNull(r.CertiDate.Value), Convert.ToDateTime("1900/1/1"), r.CertiDate.Value))
                db.AddInParameter(dbcmd, "@LicenseName", DbType.String, r.LicenseName.Value)
                db.AddInParameter(dbcmd, "@CategoryID", DbType.String, r.CategoryID.Value)
                db.AddInParameter(dbcmd, "@Institution", DbType.String, r.Institution.Value)
                db.AddInParameter(dbcmd, "@CertiTo", DbType.Date, IIf(IsDateTimeNull(r.CertiTo.Value), Convert.ToDateTime("1900/1/1"), r.CertiTo.Value))
                db.AddInParameter(dbcmd, "@SerialNum", DbType.String, r.SerialNum.Value)
                db.AddInParameter(dbcmd, "@Remark", DbType.String, r.Remark.Value)
                db.AddInParameter(dbcmd, "@CreatedDate", DbType.Date, IIf(IsDateTimeNull(r.CreatedDate.Value), Convert.ToDateTime("1900/1/1"), r.CreatedDate.Value))

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

