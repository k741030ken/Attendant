'****************************************************************
' Table:CheckInFile_SPHSC1
' Created Date: 2015.11.27
'****************************************************************/
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports Microsoft.Practices.EnterpriseLibrary.Common
Imports Microsoft.Practices.EnterpriseLibrary.Data
Imports System.Data.Common
Imports System.Data

Namespace beCheckInFile_SPHSC1
    Public Class Table
        Private m_Rows As Rows = New Rows()
        Private m_Fields As String() = { "CompID", "EmpID", "Remark", "File1", "File2", "File3", "File4", "File5", "File6", "File7" _
                                    , "File8", "File9", "File10", "File11", "File12", "File13", "File14", "File15", "File16", "File17", "File18" _
                                    , "LastChgComp", "LastChgID", "LastChgDate" }
        Private m_Types As System.Type() = { GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String) _
                                    , GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String) _
                                    , GetType(String), GetType(String), GetType(Date) }
        Private m_PrimaryFields As String() = { "CompID", "EmpID" }

        Public ReadOnly Property Rows() As beCheckInFile_SPHSC1.Rows 
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
        Public Sub Transfer2Row(CheckInFile_SPHSC1Table As DataTable)
            For Each dr As DataRow In CheckInFile_SPHSC1Table.Rows
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
                dr(m_Rows(i).Remark.FieldName) = m_Rows(i).Remark.Value
                dr(m_Rows(i).File1.FieldName) = m_Rows(i).File1.Value
                dr(m_Rows(i).File2.FieldName) = m_Rows(i).File2.Value
                dr(m_Rows(i).File3.FieldName) = m_Rows(i).File3.Value
                dr(m_Rows(i).File4.FieldName) = m_Rows(i).File4.Value
                dr(m_Rows(i).File5.FieldName) = m_Rows(i).File5.Value
                dr(m_Rows(i).File6.FieldName) = m_Rows(i).File6.Value
                dr(m_Rows(i).File7.FieldName) = m_Rows(i).File7.Value
                dr(m_Rows(i).File8.FieldName) = m_Rows(i).File8.Value
                dr(m_Rows(i).File9.FieldName) = m_Rows(i).File9.Value
                dr(m_Rows(i).File10.FieldName) = m_Rows(i).File10.Value
                dr(m_Rows(i).File11.FieldName) = m_Rows(i).File11.Value
                dr(m_Rows(i).File12.FieldName) = m_Rows(i).File12.Value
                dr(m_Rows(i).File13.FieldName) = m_Rows(i).File13.Value
                dr(m_Rows(i).File14.FieldName) = m_Rows(i).File14.Value
                dr(m_Rows(i).File15.FieldName) = m_Rows(i).File15.Value
                dr(m_Rows(i).File16.FieldName) = m_Rows(i).File16.Value
                dr(m_Rows(i).File17.FieldName) = m_Rows(i).File17.Value
                dr(m_Rows(i).File18.FieldName) = m_Rows(i).File18.Value
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

        Public Sub Add(CheckInFile_SPHSC1Row As Row)
            m_Rows.Add(CheckInFile_SPHSC1Row)
        End Sub

        Public Sub Remove(CheckInFile_SPHSC1Row As Row)
            If m_Rows.IndexOf(CheckInFile_SPHSC1Row) >= 0 Then
                m_Rows.Remove(CheckInFile_SPHSC1Row)
            End If
        End Sub

        Public Sub Dispose()
            m_Rows.Clear()
        End Sub

    End Class

    Public Class Row

        Private FI_CompID As Field(Of String) = new Field(Of String)("CompID", true)
        Private FI_EmpID As Field(Of String) = new Field(Of String)("EmpID", true)
        Private FI_Remark As Field(Of String) = new Field(Of String)("Remark", true)
        Private FI_File1 As Field(Of String) = new Field(Of String)("File1", true)
        Private FI_File2 As Field(Of String) = new Field(Of String)("File2", true)
        Private FI_File3 As Field(Of String) = new Field(Of String)("File3", true)
        Private FI_File4 As Field(Of String) = new Field(Of String)("File4", true)
        Private FI_File5 As Field(Of String) = new Field(Of String)("File5", true)
        Private FI_File6 As Field(Of String) = new Field(Of String)("File6", true)
        Private FI_File7 As Field(Of String) = new Field(Of String)("File7", true)
        Private FI_File8 As Field(Of String) = new Field(Of String)("File8", true)
        Private FI_File9 As Field(Of String) = new Field(Of String)("File9", true)
        Private FI_File10 As Field(Of String) = new Field(Of String)("File10", true)
        Private FI_File11 As Field(Of String) = new Field(Of String)("File11", true)
        Private FI_File12 As Field(Of String) = new Field(Of String)("File12", true)
        Private FI_File13 As Field(Of String) = new Field(Of String)("File13", true)
        Private FI_File14 As Field(Of String) = new Field(Of String)("File14", true)
        Private FI_File15 As Field(Of String) = new Field(Of String)("File15", true)
        Private FI_File16 As Field(Of String) = new Field(Of String)("File16", true)
        Private FI_File17 As Field(Of String) = new Field(Of String)("File17", true)
        Private FI_File18 As Field(Of String) = new Field(Of String)("File18", true)
        Private FI_LastChgComp As Field(Of String) = new Field(Of String)("LastChgComp", true)
        Private FI_LastChgID As Field(Of String) = new Field(Of String)("LastChgID", true)
        Private FI_LastChgDate As Field(Of Date) = new Field(Of Date)("LastChgDate", true)
        Private m_FieldNames As String() = { "CompID", "EmpID", "Remark", "File1", "File2", "File3", "File4", "File5", "File6", "File7" _
                                    , "File8", "File9", "File10", "File11", "File12", "File13", "File14", "File15", "File16", "File17", "File18" _
                                    , "LastChgComp", "LastChgID", "LastChgDate" }
        Private m_PrimaryFields As String() = { "CompID", "EmpID" }
        Private m_IdentityFields As String() = {  }
        Private m_LoadFromDataRow As Boolean = False

        Private Function GetFieldValue(ByVal fieldName As String) As Object
            Select Case fieldName
                Case "CompID"
                    Return FI_CompID.Value
                Case "EmpID"
                    Return FI_EmpID.Value
                Case "Remark"
                    Return FI_Remark.Value
                Case "File1"
                    Return FI_File1.Value
                Case "File2"
                    Return FI_File2.Value
                Case "File3"
                    Return FI_File3.Value
                Case "File4"
                    Return FI_File4.Value
                Case "File5"
                    Return FI_File5.Value
                Case "File6"
                    Return FI_File6.Value
                Case "File7"
                    Return FI_File7.Value
                Case "File8"
                    Return FI_File8.Value
                Case "File9"
                    Return FI_File9.Value
                Case "File10"
                    Return FI_File10.Value
                Case "File11"
                    Return FI_File11.Value
                Case "File12"
                    Return FI_File12.Value
                Case "File13"
                    Return FI_File13.Value
                Case "File14"
                    Return FI_File14.Value
                Case "File15"
                    Return FI_File15.Value
                Case "File16"
                    Return FI_File16.Value
                Case "File17"
                    Return FI_File17.Value
                Case "File18"
                    Return FI_File18.Value
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
                Case "Remark"
                    FI_Remark.SetValue(value)
                Case "File1"
                    FI_File1.SetValue(value)
                Case "File2"
                    FI_File2.SetValue(value)
                Case "File3"
                    FI_File3.SetValue(value)
                Case "File4"
                    FI_File4.SetValue(value)
                Case "File5"
                    FI_File5.SetValue(value)
                Case "File6"
                    FI_File6.SetValue(value)
                Case "File7"
                    FI_File7.SetValue(value)
                Case "File8"
                    FI_File8.SetValue(value)
                Case "File9"
                    FI_File9.SetValue(value)
                Case "File10"
                    FI_File10.SetValue(value)
                Case "File11"
                    FI_File11.SetValue(value)
                Case "File12"
                    FI_File12.SetValue(value)
                Case "File13"
                    FI_File13.SetValue(value)
                Case "File14"
                    FI_File14.SetValue(value)
                Case "File15"
                    FI_File15.SetValue(value)
                Case "File16"
                    FI_File16.SetValue(value)
                Case "File17"
                    FI_File17.SetValue(value)
                Case "File18"
                    FI_File18.SetValue(value)
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
                Case "Remark"
                    return FI_Remark.Updated
                Case "File1"
                    return FI_File1.Updated
                Case "File2"
                    return FI_File2.Updated
                Case "File3"
                    return FI_File3.Updated
                Case "File4"
                    return FI_File4.Updated
                Case "File5"
                    return FI_File5.Updated
                Case "File6"
                    return FI_File6.Updated
                Case "File7"
                    return FI_File7.Updated
                Case "File8"
                    return FI_File8.Updated
                Case "File9"
                    return FI_File9.Updated
                Case "File10"
                    return FI_File10.Updated
                Case "File11"
                    return FI_File11.Updated
                Case "File12"
                    return FI_File12.Updated
                Case "File13"
                    return FI_File13.Updated
                Case "File14"
                    return FI_File14.Updated
                Case "File15"
                    return FI_File15.Updated
                Case "File16"
                    return FI_File16.Updated
                Case "File17"
                    return FI_File17.Updated
                Case "File18"
                    return FI_File18.Updated
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
                Case "Remark"
                    return FI_Remark.CreateUpdateSQL
                Case "File1"
                    return FI_File1.CreateUpdateSQL
                Case "File2"
                    return FI_File2.CreateUpdateSQL
                Case "File3"
                    return FI_File3.CreateUpdateSQL
                Case "File4"
                    return FI_File4.CreateUpdateSQL
                Case "File5"
                    return FI_File5.CreateUpdateSQL
                Case "File6"
                    return FI_File6.CreateUpdateSQL
                Case "File7"
                    return FI_File7.CreateUpdateSQL
                Case "File8"
                    return FI_File8.CreateUpdateSQL
                Case "File9"
                    return FI_File9.CreateUpdateSQL
                Case "File10"
                    return FI_File10.CreateUpdateSQL
                Case "File11"
                    return FI_File11.CreateUpdateSQL
                Case "File12"
                    return FI_File12.CreateUpdateSQL
                Case "File13"
                    return FI_File13.CreateUpdateSQL
                Case "File14"
                    return FI_File14.CreateUpdateSQL
                Case "File15"
                    return FI_File15.CreateUpdateSQL
                Case "File16"
                    return FI_File16.CreateUpdateSQL
                Case "File17"
                    return FI_File17.CreateUpdateSQL
                Case "File18"
                    return FI_File18.CreateUpdateSQL
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
            FI_Remark.SetInitValue("")
            FI_File1.SetInitValue("0")
            FI_File2.SetInitValue("0")
            FI_File3.SetInitValue("0")
            FI_File4.SetInitValue("0")
            FI_File5.SetInitValue("0")
            FI_File6.SetInitValue("0")
            FI_File7.SetInitValue("0")
            FI_File8.SetInitValue("0")
            FI_File9.SetInitValue("0")
            FI_File10.SetInitValue("0")
            FI_File11.SetInitValue("0")
            FI_File12.SetInitValue("0")
            FI_File13.SetInitValue("0")
            FI_File14.SetInitValue("0")
            FI_File15.SetInitValue("0")
            FI_File16.SetInitValue("0")
            FI_File17.SetInitValue("0")
            FI_File18.SetInitValue("0")
            FI_LastChgComp.SetInitValue("")
            FI_LastChgID.SetInitValue("")
            FI_LastChgDate.SetInitValue(Convert.ToDateTime("1900/01/01"))
        End Sub

        Public Sub New(ByVal dr As System.Data.DataRow)
            FI_CompID.SetInitValue(dr("CompID"))
            FI_EmpID.SetInitValue(dr("EmpID"))
            FI_Remark.SetInitValue(dr("Remark"))
            FI_File1.SetInitValue(dr("File1"))
            FI_File2.SetInitValue(dr("File2"))
            FI_File3.SetInitValue(dr("File3"))
            FI_File4.SetInitValue(dr("File4"))
            FI_File5.SetInitValue(dr("File5"))
            FI_File6.SetInitValue(dr("File6"))
            FI_File7.SetInitValue(dr("File7"))
            FI_File8.SetInitValue(dr("File8"))
            FI_File9.SetInitValue(dr("File9"))
            FI_File10.SetInitValue(dr("File10"))
            FI_File11.SetInitValue(dr("File11"))
            FI_File12.SetInitValue(dr("File12"))
            FI_File13.SetInitValue(dr("File13"))
            FI_File14.SetInitValue(dr("File14"))
            FI_File15.SetInitValue(dr("File15"))
            FI_File16.SetInitValue(dr("File16"))
            FI_File17.SetInitValue(dr("File17"))
            FI_File18.SetInitValue(dr("File18"))
            FI_LastChgComp.SetInitValue(dr("LastChgComp"))
            FI_LastChgID.SetInitValue(dr("LastChgID"))
            FI_LastChgDate.SetInitValue(dr("LastChgDate"))

            m_LoadFromDataRow = True
        End Sub

        Private Sub ClearUpdated()
            FI_CompID.Updated = False
            FI_EmpID.Updated = False
            FI_Remark.Updated = False
            FI_File1.Updated = False
            FI_File2.Updated = False
            FI_File3.Updated = False
            FI_File4.Updated = False
            FI_File5.Updated = False
            FI_File6.Updated = False
            FI_File7.Updated = False
            FI_File8.Updated = False
            FI_File9.Updated = False
            FI_File10.Updated = False
            FI_File11.Updated = False
            FI_File12.Updated = False
            FI_File13.Updated = False
            FI_File14.Updated = False
            FI_File15.Updated = False
            FI_File16.Updated = False
            FI_File17.Updated = False
            FI_File18.Updated = False
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

        Public ReadOnly Property Remark As Field(Of String) 
            Get
                Return FI_Remark
            End Get
        End Property

        Public ReadOnly Property File1 As Field(Of String) 
            Get
                Return FI_File1
            End Get
        End Property

        Public ReadOnly Property File2 As Field(Of String) 
            Get
                Return FI_File2
            End Get
        End Property

        Public ReadOnly Property File3 As Field(Of String) 
            Get
                Return FI_File3
            End Get
        End Property

        Public ReadOnly Property File4 As Field(Of String) 
            Get
                Return FI_File4
            End Get
        End Property

        Public ReadOnly Property File5 As Field(Of String) 
            Get
                Return FI_File5
            End Get
        End Property

        Public ReadOnly Property File6 As Field(Of String) 
            Get
                Return FI_File6
            End Get
        End Property

        Public ReadOnly Property File7 As Field(Of String) 
            Get
                Return FI_File7
            End Get
        End Property

        Public ReadOnly Property File8 As Field(Of String) 
            Get
                Return FI_File8
            End Get
        End Property

        Public ReadOnly Property File9 As Field(Of String) 
            Get
                Return FI_File9
            End Get
        End Property

        Public ReadOnly Property File10 As Field(Of String) 
            Get
                Return FI_File10
            End Get
        End Property

        Public ReadOnly Property File11 As Field(Of String) 
            Get
                Return FI_File11
            End Get
        End Property

        Public ReadOnly Property File12 As Field(Of String) 
            Get
                Return FI_File12
            End Get
        End Property

        Public ReadOnly Property File13 As Field(Of String) 
            Get
                Return FI_File13
            End Get
        End Property

        Public ReadOnly Property File14 As Field(Of String) 
            Get
                Return FI_File14
            End Get
        End Property

        Public ReadOnly Property File15 As Field(Of String) 
            Get
                Return FI_File15
            End Get
        End Property

        Public ReadOnly Property File16 As Field(Of String) 
            Get
                Return FI_File16
            End Get
        End Property

        Public ReadOnly Property File17 As Field(Of String) 
            Get
                Return FI_File17
            End Get
        End Property

        Public ReadOnly Property File18 As Field(Of String) 
            Get
                Return FI_File18
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
        Public Function DeleteRowByPrimaryKey(ByVal CheckInFile_SPHSC1Row As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Delete From CheckInFile_SPHSC1")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And EmpID = @EmpID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, CheckInFile_SPHSC1Row.CompID.Value)
            db.AddInParameter(dbcmd, "@EmpID", DbType.String, CheckInFile_SPHSC1Row.EmpID.Value)

            return db.ExecuteNonQuery(dbcmd)
        End Function

        public Function DeleteRowByPrimaryKey(ByVal CheckInFile_SPHSC1Row As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Delete From CheckInFile_SPHSC1")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And EmpID = @EmpID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, CheckInFile_SPHSC1Row.CompID.Value)
            db.AddInParameter(dbcmd, "@EmpID", DbType.String, CheckInFile_SPHSC1Row.EmpID.Value)

            return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function DeleteRowByPrimaryKey(ByVal CheckInFile_SPHSC1Row As Row()) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Delete From CheckInFile_SPHSC1")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And EmpID = @EmpID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            using cn As DbConnection = db.CreateConnection()
                cn.Open()
                Dim tran As DbTransaction = cn.BeginTransaction()
                Dim inTrans As Boolean = True

                Try
                    For Each r As Row In CheckInFile_SPHSC1Row
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

        Public Function DeleteRowByPrimaryKey(ByVal CheckInFile_SPHSC1Row As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Delete From CheckInFile_SPHSC1")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And EmpID = @EmpID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())

            For Each r As Row In CheckInFile_SPHSC1Row
                dbcmd.Parameters.Clear()
                db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                db.AddInParameter(dbcmd, "@EmpID", DbType.String, r.EmpID.Value)

                intRowsAffected += db.ExecuteNonQuery(dbcmd, tran)
            Next
            Return intRowsAffected
        End Function

        Public Function QueryByKey(ByVal CheckInFile_SPHSC1Row As Row) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From CheckInFile_SPHSC1")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And EmpID = @EmpID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, CheckInFile_SPHSC1Row.CompID.Value)
            db.AddInParameter(dbcmd, "@EmpID", DbType.String, CheckInFile_SPHSC1Row.EmpID.Value)

            Return db.ExecuteDataSet(dbcmd)
        End Function

        Public Function QueryByKey(CheckInFile_SPHSC1Row As Row, tran As DbTransaction) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From CheckInFile_SPHSC1")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And EmpID = @EmpID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, CheckInFile_SPHSC1Row.CompID.Value)
            db.AddInParameter(dbcmd, "@EmpID", DbType.String, CheckInFile_SPHSC1Row.EmpID.Value)

            Return db.ExecuteDataSet(dbcmd, tran)
        End Function

        Public Function Update(ByVal CheckInFile_SPHSC1Row As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim strDot As String = String.Empty

            strSQL.AppendLine("Update CheckInFile_SPHSC1 Set")
            For i As Integer = 0 To CheckInFile_SPHSC1Row.FieldNames.Length - 1
                If Not CheckInFile_SPHSC1Row.IsIdentityField(CheckInFile_SPHSC1Row.FieldNames(i)) AndAlso CheckInFile_SPHSC1Row.IsUpdated(CheckInFile_SPHSC1Row.FieldNames(i)) AndAlso CheckInFile_SPHSC1Row.CreateUpdateSQL(CheckInFile_SPHSC1Row.FieldNames(i)) Then
                    strSQL.AppendLine(string.Format("{0}{1} = @{1}", strDot, CheckInFile_SPHSC1Row.FieldNames(i)))
                    strDot = ","
                End If
            Next
            If strDot = String.Empty Then Throw New Exception("未異動資料欄位！")
            strSQL.AppendLine("Where CompID = @PKCompID")
            strSQL.AppendLine("And EmpID = @PKEmpID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            If CheckInFile_SPHSC1Row.CompID.Updated Then db.AddInParameter(dbcmd, "@CompID", DbType.String, CheckInFile_SPHSC1Row.CompID.Value)
            If CheckInFile_SPHSC1Row.EmpID.Updated Then db.AddInParameter(dbcmd, "@EmpID", DbType.String, CheckInFile_SPHSC1Row.EmpID.Value)
            If CheckInFile_SPHSC1Row.Remark.Updated Then db.AddInParameter(dbcmd, "@Remark", DbType.String, CheckInFile_SPHSC1Row.Remark.Value)
            If CheckInFile_SPHSC1Row.File1.Updated Then db.AddInParameter(dbcmd, "@File1", DbType.String, CheckInFile_SPHSC1Row.File1.Value)
            If CheckInFile_SPHSC1Row.File2.Updated Then db.AddInParameter(dbcmd, "@File2", DbType.String, CheckInFile_SPHSC1Row.File2.Value)
            If CheckInFile_SPHSC1Row.File3.Updated Then db.AddInParameter(dbcmd, "@File3", DbType.String, CheckInFile_SPHSC1Row.File3.Value)
            If CheckInFile_SPHSC1Row.File4.Updated Then db.AddInParameter(dbcmd, "@File4", DbType.String, CheckInFile_SPHSC1Row.File4.Value)
            If CheckInFile_SPHSC1Row.File5.Updated Then db.AddInParameter(dbcmd, "@File5", DbType.String, CheckInFile_SPHSC1Row.File5.Value)
            If CheckInFile_SPHSC1Row.File6.Updated Then db.AddInParameter(dbcmd, "@File6", DbType.String, CheckInFile_SPHSC1Row.File6.Value)
            If CheckInFile_SPHSC1Row.File7.Updated Then db.AddInParameter(dbcmd, "@File7", DbType.String, CheckInFile_SPHSC1Row.File7.Value)
            If CheckInFile_SPHSC1Row.File8.Updated Then db.AddInParameter(dbcmd, "@File8", DbType.String, CheckInFile_SPHSC1Row.File8.Value)
            If CheckInFile_SPHSC1Row.File9.Updated Then db.AddInParameter(dbcmd, "@File9", DbType.String, CheckInFile_SPHSC1Row.File9.Value)
            If CheckInFile_SPHSC1Row.File10.Updated Then db.AddInParameter(dbcmd, "@File10", DbType.String, CheckInFile_SPHSC1Row.File10.Value)
            If CheckInFile_SPHSC1Row.File11.Updated Then db.AddInParameter(dbcmd, "@File11", DbType.String, CheckInFile_SPHSC1Row.File11.Value)
            If CheckInFile_SPHSC1Row.File12.Updated Then db.AddInParameter(dbcmd, "@File12", DbType.String, CheckInFile_SPHSC1Row.File12.Value)
            If CheckInFile_SPHSC1Row.File13.Updated Then db.AddInParameter(dbcmd, "@File13", DbType.String, CheckInFile_SPHSC1Row.File13.Value)
            If CheckInFile_SPHSC1Row.File14.Updated Then db.AddInParameter(dbcmd, "@File14", DbType.String, CheckInFile_SPHSC1Row.File14.Value)
            If CheckInFile_SPHSC1Row.File15.Updated Then db.AddInParameter(dbcmd, "@File15", DbType.String, CheckInFile_SPHSC1Row.File15.Value)
            If CheckInFile_SPHSC1Row.File16.Updated Then db.AddInParameter(dbcmd, "@File16", DbType.String, CheckInFile_SPHSC1Row.File16.Value)
            If CheckInFile_SPHSC1Row.File17.Updated Then db.AddInParameter(dbcmd, "@File17", DbType.String, CheckInFile_SPHSC1Row.File17.Value)
            If CheckInFile_SPHSC1Row.File18.Updated Then db.AddInParameter(dbcmd, "@File18", DbType.String, CheckInFile_SPHSC1Row.File18.Value)
            If CheckInFile_SPHSC1Row.LastChgComp.Updated Then db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, CheckInFile_SPHSC1Row.LastChgComp.Value)
            If CheckInFile_SPHSC1Row.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, CheckInFile_SPHSC1Row.LastChgID.Value)
            If CheckInFile_SPHSC1Row.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(CheckInFile_SPHSC1Row.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), CheckInFile_SPHSC1Row.LastChgDate.Value))
            db.AddInParameter(dbcmd, "@PKCompID", DbType.String, IIf(CheckInFile_SPHSC1Row.LoadFromDataRow, CheckInFile_SPHSC1Row.CompID.OldValue, CheckInFile_SPHSC1Row.CompID.Value))
            db.AddInParameter(dbcmd, "@PKEmpID", DbType.String, IIf(CheckInFile_SPHSC1Row.LoadFromDataRow, CheckInFile_SPHSC1Row.EmpID.OldValue, CheckInFile_SPHSC1Row.EmpID.Value))

            Return db.ExecuteNonQuery(dbcmd)
        End Function

        Public Function Update(ByVal CheckInFile_SPHSC1Row As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim strDot As String = String.Empty

            strSQL.AppendLine("Update CheckInFile_SPHSC1 Set")
            For i As Integer = 0 To CheckInFile_SPHSC1Row.FieldNames.Length - 1
                If Not CheckInFile_SPHSC1Row.IsIdentityField(CheckInFile_SPHSC1Row.FieldNames(i)) AndAlso CheckInFile_SPHSC1Row.IsUpdated(CheckInFile_SPHSC1Row.FieldNames(i)) AndAlso CheckInFile_SPHSC1Row.CreateUpdateSQL(CheckInFile_SPHSC1Row.FieldNames(i)) Then
                    strSQL.AppendLine(string.Format("{0}{1} = @{1}", strDot, CheckInFile_SPHSC1Row.FieldNames(i)))
                    strDot = ","
                End If
            Next 
            If strDot = String.Empty Then Throw New Exception("未異動資料欄位！")
            strSQL.AppendLine("Where CompID = @PKCompID")
            strSQL.AppendLine("And EmpID = @PKEmpID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            If CheckInFile_SPHSC1Row.CompID.Updated Then db.AddInParameter(dbcmd, "@CompID", DbType.String, CheckInFile_SPHSC1Row.CompID.Value)
            If CheckInFile_SPHSC1Row.EmpID.Updated Then db.AddInParameter(dbcmd, "@EmpID", DbType.String, CheckInFile_SPHSC1Row.EmpID.Value)
            If CheckInFile_SPHSC1Row.Remark.Updated Then db.AddInParameter(dbcmd, "@Remark", DbType.String, CheckInFile_SPHSC1Row.Remark.Value)
            If CheckInFile_SPHSC1Row.File1.Updated Then db.AddInParameter(dbcmd, "@File1", DbType.String, CheckInFile_SPHSC1Row.File1.Value)
            If CheckInFile_SPHSC1Row.File2.Updated Then db.AddInParameter(dbcmd, "@File2", DbType.String, CheckInFile_SPHSC1Row.File2.Value)
            If CheckInFile_SPHSC1Row.File3.Updated Then db.AddInParameter(dbcmd, "@File3", DbType.String, CheckInFile_SPHSC1Row.File3.Value)
            If CheckInFile_SPHSC1Row.File4.Updated Then db.AddInParameter(dbcmd, "@File4", DbType.String, CheckInFile_SPHSC1Row.File4.Value)
            If CheckInFile_SPHSC1Row.File5.Updated Then db.AddInParameter(dbcmd, "@File5", DbType.String, CheckInFile_SPHSC1Row.File5.Value)
            If CheckInFile_SPHSC1Row.File6.Updated Then db.AddInParameter(dbcmd, "@File6", DbType.String, CheckInFile_SPHSC1Row.File6.Value)
            If CheckInFile_SPHSC1Row.File7.Updated Then db.AddInParameter(dbcmd, "@File7", DbType.String, CheckInFile_SPHSC1Row.File7.Value)
            If CheckInFile_SPHSC1Row.File8.Updated Then db.AddInParameter(dbcmd, "@File8", DbType.String, CheckInFile_SPHSC1Row.File8.Value)
            If CheckInFile_SPHSC1Row.File9.Updated Then db.AddInParameter(dbcmd, "@File9", DbType.String, CheckInFile_SPHSC1Row.File9.Value)
            If CheckInFile_SPHSC1Row.File10.Updated Then db.AddInParameter(dbcmd, "@File10", DbType.String, CheckInFile_SPHSC1Row.File10.Value)
            If CheckInFile_SPHSC1Row.File11.Updated Then db.AddInParameter(dbcmd, "@File11", DbType.String, CheckInFile_SPHSC1Row.File11.Value)
            If CheckInFile_SPHSC1Row.File12.Updated Then db.AddInParameter(dbcmd, "@File12", DbType.String, CheckInFile_SPHSC1Row.File12.Value)
            If CheckInFile_SPHSC1Row.File13.Updated Then db.AddInParameter(dbcmd, "@File13", DbType.String, CheckInFile_SPHSC1Row.File13.Value)
            If CheckInFile_SPHSC1Row.File14.Updated Then db.AddInParameter(dbcmd, "@File14", DbType.String, CheckInFile_SPHSC1Row.File14.Value)
            If CheckInFile_SPHSC1Row.File15.Updated Then db.AddInParameter(dbcmd, "@File15", DbType.String, CheckInFile_SPHSC1Row.File15.Value)
            If CheckInFile_SPHSC1Row.File16.Updated Then db.AddInParameter(dbcmd, "@File16", DbType.String, CheckInFile_SPHSC1Row.File16.Value)
            If CheckInFile_SPHSC1Row.File17.Updated Then db.AddInParameter(dbcmd, "@File17", DbType.String, CheckInFile_SPHSC1Row.File17.Value)
            If CheckInFile_SPHSC1Row.File18.Updated Then db.AddInParameter(dbcmd, "@File18", DbType.String, CheckInFile_SPHSC1Row.File18.Value)
            If CheckInFile_SPHSC1Row.LastChgComp.Updated Then db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, CheckInFile_SPHSC1Row.LastChgComp.Value)
            If CheckInFile_SPHSC1Row.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, CheckInFile_SPHSC1Row.LastChgID.Value)
            If CheckInFile_SPHSC1Row.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(CheckInFile_SPHSC1Row.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), CheckInFile_SPHSC1Row.LastChgDate.Value))
            db.AddInParameter(dbcmd, "@PKCompID", DbType.String, IIf(CheckInFile_SPHSC1Row.LoadFromDataRow, CheckInFile_SPHSC1Row.CompID.OldValue, CheckInFile_SPHSC1Row.CompID.Value))
            db.AddInParameter(dbcmd, "@PKEmpID", DbType.String, IIf(CheckInFile_SPHSC1Row.LoadFromDataRow, CheckInFile_SPHSC1Row.EmpID.OldValue, CheckInFile_SPHSC1Row.EmpID.Value))

            Return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function Update(ByVal CheckInFile_SPHSC1Row As Row()) As Integer
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
                    For Each r As Row In CheckInFile_SPHSC1Row
                        strDot = String.Empty
                        If strSQL.Length > 0 Then strSQL.Remove(0, strSQL.Length)
                        strSQL.AppendLine("Update CheckInFile_SPHSC1 Set")
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
                        If r.Remark.Updated Then db.AddInParameter(dbcmd, "@Remark", DbType.String, r.Remark.Value)
                        If r.File1.Updated Then db.AddInParameter(dbcmd, "@File1", DbType.String, r.File1.Value)
                        If r.File2.Updated Then db.AddInParameter(dbcmd, "@File2", DbType.String, r.File2.Value)
                        If r.File3.Updated Then db.AddInParameter(dbcmd, "@File3", DbType.String, r.File3.Value)
                        If r.File4.Updated Then db.AddInParameter(dbcmd, "@File4", DbType.String, r.File4.Value)
                        If r.File5.Updated Then db.AddInParameter(dbcmd, "@File5", DbType.String, r.File5.Value)
                        If r.File6.Updated Then db.AddInParameter(dbcmd, "@File6", DbType.String, r.File6.Value)
                        If r.File7.Updated Then db.AddInParameter(dbcmd, "@File7", DbType.String, r.File7.Value)
                        If r.File8.Updated Then db.AddInParameter(dbcmd, "@File8", DbType.String, r.File8.Value)
                        If r.File9.Updated Then db.AddInParameter(dbcmd, "@File9", DbType.String, r.File9.Value)
                        If r.File10.Updated Then db.AddInParameter(dbcmd, "@File10", DbType.String, r.File10.Value)
                        If r.File11.Updated Then db.AddInParameter(dbcmd, "@File11", DbType.String, r.File11.Value)
                        If r.File12.Updated Then db.AddInParameter(dbcmd, "@File12", DbType.String, r.File12.Value)
                        If r.File13.Updated Then db.AddInParameter(dbcmd, "@File13", DbType.String, r.File13.Value)
                        If r.File14.Updated Then db.AddInParameter(dbcmd, "@File14", DbType.String, r.File14.Value)
                        If r.File15.Updated Then db.AddInParameter(dbcmd, "@File15", DbType.String, r.File15.Value)
                        If r.File16.Updated Then db.AddInParameter(dbcmd, "@File16", DbType.String, r.File16.Value)
                        If r.File17.Updated Then db.AddInParameter(dbcmd, "@File17", DbType.String, r.File17.Value)
                        If r.File18.Updated Then db.AddInParameter(dbcmd, "@File18", DbType.String, r.File18.Value)
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

        Public Function Update(ByVal CheckInFile_SPHSC1Row As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0
            Dim strDot As String

            For Each r As Row In CheckInFile_SPHSC1Row
                strDot = String.Empty
                If strSQL.Length > 0 Then strSQL.Remove(0, strSQL.Length)
                strSQL.AppendLine("Update CheckInFile_SPHSC1 Set")
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
                If r.Remark.Updated Then db.AddInParameter(dbcmd, "@Remark", DbType.String, r.Remark.Value)
                If r.File1.Updated Then db.AddInParameter(dbcmd, "@File1", DbType.String, r.File1.Value)
                If r.File2.Updated Then db.AddInParameter(dbcmd, "@File2", DbType.String, r.File2.Value)
                If r.File3.Updated Then db.AddInParameter(dbcmd, "@File3", DbType.String, r.File3.Value)
                If r.File4.Updated Then db.AddInParameter(dbcmd, "@File4", DbType.String, r.File4.Value)
                If r.File5.Updated Then db.AddInParameter(dbcmd, "@File5", DbType.String, r.File5.Value)
                If r.File6.Updated Then db.AddInParameter(dbcmd, "@File6", DbType.String, r.File6.Value)
                If r.File7.Updated Then db.AddInParameter(dbcmd, "@File7", DbType.String, r.File7.Value)
                If r.File8.Updated Then db.AddInParameter(dbcmd, "@File8", DbType.String, r.File8.Value)
                If r.File9.Updated Then db.AddInParameter(dbcmd, "@File9", DbType.String, r.File9.Value)
                If r.File10.Updated Then db.AddInParameter(dbcmd, "@File10", DbType.String, r.File10.Value)
                If r.File11.Updated Then db.AddInParameter(dbcmd, "@File11", DbType.String, r.File11.Value)
                If r.File12.Updated Then db.AddInParameter(dbcmd, "@File12", DbType.String, r.File12.Value)
                If r.File13.Updated Then db.AddInParameter(dbcmd, "@File13", DbType.String, r.File13.Value)
                If r.File14.Updated Then db.AddInParameter(dbcmd, "@File14", DbType.String, r.File14.Value)
                If r.File15.Updated Then db.AddInParameter(dbcmd, "@File15", DbType.String, r.File15.Value)
                If r.File16.Updated Then db.AddInParameter(dbcmd, "@File16", DbType.String, r.File16.Value)
                If r.File17.Updated Then db.AddInParameter(dbcmd, "@File17", DbType.String, r.File17.Value)
                If r.File18.Updated Then db.AddInParameter(dbcmd, "@File18", DbType.String, r.File18.Value)
                If r.LastChgComp.Updated Then db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, r.LastChgComp.Value)
                If r.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, r.LastChgID.Value)
                If r.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(r.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), r.LastChgDate.Value))
                db.AddInParameter(dbcmd, "@PKCompID", DbType.String, IIf(r.LoadFromDataRow, r.CompID.OldValue, r.CompID.Value))
                db.AddInParameter(dbcmd, "@PKEmpID", DbType.String, IIf(r.LoadFromDataRow, r.EmpID.OldValue, r.EmpID.Value))

                intRowsAffected += db.ExecuteNonQuery(dbcmd, tran)
            Next
            Return intRowsAffected
        End Function

        Public Function IsDataExists(ByVal CheckInFile_SPHSC1Row As Row) As Boolean
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intCount As Integer = 0

            strSQL.AppendLine("Select Count(*) Cnt From CheckInFile_SPHSC1")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And EmpID = @EmpID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, CheckInFile_SPHSC1Row.CompID.Value)
            db.AddInParameter(dbcmd, "@EmpID", DbType.String, CheckInFile_SPHSC1Row.EmpID.Value)

            intCount = Convert.ToInt32(db.ExecuteScalar(dbcmd))
            Return IIf(intCount > 0, True, False)
        End Function

        Public Function IsDataExists(ByVal CheckInFile_SPHSC1Row As Row, ByVal tran As DbTransaction) As Boolean
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intCount As Integer = 0

            strSQL.AppendLine("Select Count(*) Cnt From CheckInFile_SPHSC1")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And EmpID = @EmpID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, CheckInFile_SPHSC1Row.CompID.Value)
            db.AddInParameter(dbcmd, "@EmpID", DbType.String, CheckInFile_SPHSC1Row.EmpID.Value)

            intCount = Convert.ToInt32(db.ExecuteScalar(dbcmd, tran))
            Return IIf(intCount > 0, True, False)
        End Function

        Public Function QuerybyWhere(WhereCondition As String) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From CheckInFile_SPHSC1")
            If WhereCondition <> String.Empty Then strSQL.AppendLine(WhereCondition)

            Return db.ExecuteDataSet(CommandType.Text, strSQL.ToString())
        End Function

        Public Function Insert(ByVal CheckInFile_SPHSC1Row As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Insert into CheckInFile_SPHSC1")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    CompID, EmpID, Remark, File1, File2, File3, File4, File5, File6, File7, File8, File9,")
            strSQL.AppendLine("    File10, File11, File12, File13, File14, File15, File16, File17, File18, LastChgComp,")
            strSQL.AppendLine("    LastChgID, LastChgDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @CompID, @EmpID, @Remark, @File1, @File2, @File3, @File4, @File5, @File6, @File7, @File8, @File9,")
            strSQL.AppendLine("    @File10, @File11, @File12, @File13, @File14, @File15, @File16, @File17, @File18, @LastChgComp,")
            strSQL.AppendLine("    @LastChgID, @LastChgDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, CheckInFile_SPHSC1Row.CompID.Value)
            db.AddInParameter(dbcmd, "@EmpID", DbType.String, CheckInFile_SPHSC1Row.EmpID.Value)
            db.AddInParameter(dbcmd, "@Remark", DbType.String, CheckInFile_SPHSC1Row.Remark.Value)
            db.AddInParameter(dbcmd, "@File1", DbType.String, CheckInFile_SPHSC1Row.File1.Value)
            db.AddInParameter(dbcmd, "@File2", DbType.String, CheckInFile_SPHSC1Row.File2.Value)
            db.AddInParameter(dbcmd, "@File3", DbType.String, CheckInFile_SPHSC1Row.File3.Value)
            db.AddInParameter(dbcmd, "@File4", DbType.String, CheckInFile_SPHSC1Row.File4.Value)
            db.AddInParameter(dbcmd, "@File5", DbType.String, CheckInFile_SPHSC1Row.File5.Value)
            db.AddInParameter(dbcmd, "@File6", DbType.String, CheckInFile_SPHSC1Row.File6.Value)
            db.AddInParameter(dbcmd, "@File7", DbType.String, CheckInFile_SPHSC1Row.File7.Value)
            db.AddInParameter(dbcmd, "@File8", DbType.String, CheckInFile_SPHSC1Row.File8.Value)
            db.AddInParameter(dbcmd, "@File9", DbType.String, CheckInFile_SPHSC1Row.File9.Value)
            db.AddInParameter(dbcmd, "@File10", DbType.String, CheckInFile_SPHSC1Row.File10.Value)
            db.AddInParameter(dbcmd, "@File11", DbType.String, CheckInFile_SPHSC1Row.File11.Value)
            db.AddInParameter(dbcmd, "@File12", DbType.String, CheckInFile_SPHSC1Row.File12.Value)
            db.AddInParameter(dbcmd, "@File13", DbType.String, CheckInFile_SPHSC1Row.File13.Value)
            db.AddInParameter(dbcmd, "@File14", DbType.String, CheckInFile_SPHSC1Row.File14.Value)
            db.AddInParameter(dbcmd, "@File15", DbType.String, CheckInFile_SPHSC1Row.File15.Value)
            db.AddInParameter(dbcmd, "@File16", DbType.String, CheckInFile_SPHSC1Row.File16.Value)
            db.AddInParameter(dbcmd, "@File17", DbType.String, CheckInFile_SPHSC1Row.File17.Value)
            db.AddInParameter(dbcmd, "@File18", DbType.String, CheckInFile_SPHSC1Row.File18.Value)
            db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, CheckInFile_SPHSC1Row.LastChgComp.Value)
            db.AddInParameter(dbcmd, "@LastChgID", DbType.String, CheckInFile_SPHSC1Row.LastChgID.Value)
            db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(CheckInFile_SPHSC1Row.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), CheckInFile_SPHSC1Row.LastChgDate.Value))

            Return db.ExecuteNonQuery(dbcmd)
        End Function

        Public Function Insert(ByVal CheckInFile_SPHSC1Row As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Insert into CheckInFile_SPHSC1")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    CompID, EmpID, Remark, File1, File2, File3, File4, File5, File6, File7, File8, File9,")
            strSQL.AppendLine("    File10, File11, File12, File13, File14, File15, File16, File17, File18, LastChgComp,")
            strSQL.AppendLine("    LastChgID, LastChgDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @CompID, @EmpID, @Remark, @File1, @File2, @File3, @File4, @File5, @File6, @File7, @File8, @File9,")
            strSQL.AppendLine("    @File10, @File11, @File12, @File13, @File14, @File15, @File16, @File17, @File18, @LastChgComp,")
            strSQL.AppendLine("    @LastChgID, @LastChgDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, CheckInFile_SPHSC1Row.CompID.Value)
            db.AddInParameter(dbcmd, "@EmpID", DbType.String, CheckInFile_SPHSC1Row.EmpID.Value)
            db.AddInParameter(dbcmd, "@Remark", DbType.String, CheckInFile_SPHSC1Row.Remark.Value)
            db.AddInParameter(dbcmd, "@File1", DbType.String, CheckInFile_SPHSC1Row.File1.Value)
            db.AddInParameter(dbcmd, "@File2", DbType.String, CheckInFile_SPHSC1Row.File2.Value)
            db.AddInParameter(dbcmd, "@File3", DbType.String, CheckInFile_SPHSC1Row.File3.Value)
            db.AddInParameter(dbcmd, "@File4", DbType.String, CheckInFile_SPHSC1Row.File4.Value)
            db.AddInParameter(dbcmd, "@File5", DbType.String, CheckInFile_SPHSC1Row.File5.Value)
            db.AddInParameter(dbcmd, "@File6", DbType.String, CheckInFile_SPHSC1Row.File6.Value)
            db.AddInParameter(dbcmd, "@File7", DbType.String, CheckInFile_SPHSC1Row.File7.Value)
            db.AddInParameter(dbcmd, "@File8", DbType.String, CheckInFile_SPHSC1Row.File8.Value)
            db.AddInParameter(dbcmd, "@File9", DbType.String, CheckInFile_SPHSC1Row.File9.Value)
            db.AddInParameter(dbcmd, "@File10", DbType.String, CheckInFile_SPHSC1Row.File10.Value)
            db.AddInParameter(dbcmd, "@File11", DbType.String, CheckInFile_SPHSC1Row.File11.Value)
            db.AddInParameter(dbcmd, "@File12", DbType.String, CheckInFile_SPHSC1Row.File12.Value)
            db.AddInParameter(dbcmd, "@File13", DbType.String, CheckInFile_SPHSC1Row.File13.Value)
            db.AddInParameter(dbcmd, "@File14", DbType.String, CheckInFile_SPHSC1Row.File14.Value)
            db.AddInParameter(dbcmd, "@File15", DbType.String, CheckInFile_SPHSC1Row.File15.Value)
            db.AddInParameter(dbcmd, "@File16", DbType.String, CheckInFile_SPHSC1Row.File16.Value)
            db.AddInParameter(dbcmd, "@File17", DbType.String, CheckInFile_SPHSC1Row.File17.Value)
            db.AddInParameter(dbcmd, "@File18", DbType.String, CheckInFile_SPHSC1Row.File18.Value)
            db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, CheckInFile_SPHSC1Row.LastChgComp.Value)
            db.AddInParameter(dbcmd, "@LastChgID", DbType.String, CheckInFile_SPHSC1Row.LastChgID.Value)
            db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(CheckInFile_SPHSC1Row.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), CheckInFile_SPHSC1Row.LastChgDate.Value))

            Return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function Insert(ByVal CheckInFile_SPHSC1Row As Row()) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Insert into CheckInFile_SPHSC1")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    CompID, EmpID, Remark, File1, File2, File3, File4, File5, File6, File7, File8, File9,")
            strSQL.AppendLine("    File10, File11, File12, File13, File14, File15, File16, File17, File18, LastChgComp,")
            strSQL.AppendLine("    LastChgID, LastChgDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @CompID, @EmpID, @Remark, @File1, @File2, @File3, @File4, @File5, @File6, @File7, @File8, @File9,")
            strSQL.AppendLine("    @File10, @File11, @File12, @File13, @File14, @File15, @File16, @File17, @File18, @LastChgComp,")
            strSQL.AppendLine("    @LastChgID, @LastChgDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            Using cn As DbConnection = db.CreateConnection()
                cn.Open()
                Dim tran As DbTransaction = cn.BeginTransaction()
                Dim inTrans As Boolean = True

                Try
                    For Each r As Row In CheckInFile_SPHSC1Row
                        dbcmd.Parameters.Clear()
                        db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                        db.AddInParameter(dbcmd, "@EmpID", DbType.String, r.EmpID.Value)
                        db.AddInParameter(dbcmd, "@Remark", DbType.String, r.Remark.Value)
                        db.AddInParameter(dbcmd, "@File1", DbType.String, r.File1.Value)
                        db.AddInParameter(dbcmd, "@File2", DbType.String, r.File2.Value)
                        db.AddInParameter(dbcmd, "@File3", DbType.String, r.File3.Value)
                        db.AddInParameter(dbcmd, "@File4", DbType.String, r.File4.Value)
                        db.AddInParameter(dbcmd, "@File5", DbType.String, r.File5.Value)
                        db.AddInParameter(dbcmd, "@File6", DbType.String, r.File6.Value)
                        db.AddInParameter(dbcmd, "@File7", DbType.String, r.File7.Value)
                        db.AddInParameter(dbcmd, "@File8", DbType.String, r.File8.Value)
                        db.AddInParameter(dbcmd, "@File9", DbType.String, r.File9.Value)
                        db.AddInParameter(dbcmd, "@File10", DbType.String, r.File10.Value)
                        db.AddInParameter(dbcmd, "@File11", DbType.String, r.File11.Value)
                        db.AddInParameter(dbcmd, "@File12", DbType.String, r.File12.Value)
                        db.AddInParameter(dbcmd, "@File13", DbType.String, r.File13.Value)
                        db.AddInParameter(dbcmd, "@File14", DbType.String, r.File14.Value)
                        db.AddInParameter(dbcmd, "@File15", DbType.String, r.File15.Value)
                        db.AddInParameter(dbcmd, "@File16", DbType.String, r.File16.Value)
                        db.AddInParameter(dbcmd, "@File17", DbType.String, r.File17.Value)
                        db.AddInParameter(dbcmd, "@File18", DbType.String, r.File18.Value)
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

        Public Function Insert(ByVal CheckInFile_SPHSC1Row As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Insert into CheckInFile_SPHSC1")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    CompID, EmpID, Remark, File1, File2, File3, File4, File5, File6, File7, File8, File9,")
            strSQL.AppendLine("    File10, File11, File12, File13, File14, File15, File16, File17, File18, LastChgComp,")
            strSQL.AppendLine("    LastChgID, LastChgDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @CompID, @EmpID, @Remark, @File1, @File2, @File3, @File4, @File5, @File6, @File7, @File8, @File9,")
            strSQL.AppendLine("    @File10, @File11, @File12, @File13, @File14, @File15, @File16, @File17, @File18, @LastChgComp,")
            strSQL.AppendLine("    @LastChgID, @LastChgDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())

            For Each r As Row In CheckInFile_SPHSC1Row
                dbcmd.Parameters.Clear()
                db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                db.AddInParameter(dbcmd, "@EmpID", DbType.String, r.EmpID.Value)
                db.AddInParameter(dbcmd, "@Remark", DbType.String, r.Remark.Value)
                db.AddInParameter(dbcmd, "@File1", DbType.String, r.File1.Value)
                db.AddInParameter(dbcmd, "@File2", DbType.String, r.File2.Value)
                db.AddInParameter(dbcmd, "@File3", DbType.String, r.File3.Value)
                db.AddInParameter(dbcmd, "@File4", DbType.String, r.File4.Value)
                db.AddInParameter(dbcmd, "@File5", DbType.String, r.File5.Value)
                db.AddInParameter(dbcmd, "@File6", DbType.String, r.File6.Value)
                db.AddInParameter(dbcmd, "@File7", DbType.String, r.File7.Value)
                db.AddInParameter(dbcmd, "@File8", DbType.String, r.File8.Value)
                db.AddInParameter(dbcmd, "@File9", DbType.String, r.File9.Value)
                db.AddInParameter(dbcmd, "@File10", DbType.String, r.File10.Value)
                db.AddInParameter(dbcmd, "@File11", DbType.String, r.File11.Value)
                db.AddInParameter(dbcmd, "@File12", DbType.String, r.File12.Value)
                db.AddInParameter(dbcmd, "@File13", DbType.String, r.File13.Value)
                db.AddInParameter(dbcmd, "@File14", DbType.String, r.File14.Value)
                db.AddInParameter(dbcmd, "@File15", DbType.String, r.File15.Value)
                db.AddInParameter(dbcmd, "@File16", DbType.String, r.File16.Value)
                db.AddInParameter(dbcmd, "@File17", DbType.String, r.File17.Value)
                db.AddInParameter(dbcmd, "@File18", DbType.String, r.File18.Value)
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

