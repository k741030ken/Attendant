'****************************************************************
' Table:Family
' Created Date: 2016.06.25
'****************************************************************/
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports Microsoft.Practices.EnterpriseLibrary.Common
Imports Microsoft.Practices.EnterpriseLibrary.Data
Imports System.Data.Common
Imports System.Data

Namespace beFamily
    Public Class Table
        Private m_Rows As Rows = New Rows()
        Private m_Fields As String() = { "IDNo", "RelativeIDNo", "RelativeID", "Name", "NameN", "BirthDate", "Occupation", "IndustryType", "Company", "DeleteMark" _
                                    , "HeaStatus", "GrpStatus", "IdentityID", "GrpKind", "HeaDate", "GrpDate", "IsBSPEmp", "LastChgComp", "LastChgID", "LastChgDate", "Rm_LastChgDate" }
        Private m_Types As System.Type() = { GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(Date), GetType(String), GetType(String), GetType(String), GetType(String) _
                                    , GetType(String), GetType(String), GetType(String), GetType(String), GetType(Date), GetType(Date), GetType(String), GetType(String), GetType(String), GetType(Date), GetType(Date) }
        Private m_PrimaryFields As String() = { "IDNo", "RelativeIDNo" }

        Public ReadOnly Property Rows() As beFamily.Rows 
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
        Public Sub Transfer2Row(FamilyTable As DataTable)
            For Each dr As DataRow In FamilyTable.Rows
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
                dr(m_Rows(i).RelativeIDNo.FieldName) = m_Rows(i).RelativeIDNo.Value
                dr(m_Rows(i).RelativeID.FieldName) = m_Rows(i).RelativeID.Value
                dr(m_Rows(i).Name.FieldName) = m_Rows(i).Name.Value
                dr(m_Rows(i).NameN.FieldName) = m_Rows(i).NameN.Value
                dr(m_Rows(i).BirthDate.FieldName) = m_Rows(i).BirthDate.Value
                dr(m_Rows(i).Occupation.FieldName) = m_Rows(i).Occupation.Value
                dr(m_Rows(i).IndustryType.FieldName) = m_Rows(i).IndustryType.Value
                dr(m_Rows(i).Company.FieldName) = m_Rows(i).Company.Value
                dr(m_Rows(i).DeleteMark.FieldName) = m_Rows(i).DeleteMark.Value
                dr(m_Rows(i).HeaStatus.FieldName) = m_Rows(i).HeaStatus.Value
                dr(m_Rows(i).GrpStatus.FieldName) = m_Rows(i).GrpStatus.Value
                dr(m_Rows(i).IdentityID.FieldName) = m_Rows(i).IdentityID.Value
                dr(m_Rows(i).GrpKind.FieldName) = m_Rows(i).GrpKind.Value
                dr(m_Rows(i).HeaDate.FieldName) = m_Rows(i).HeaDate.Value
                dr(m_Rows(i).GrpDate.FieldName) = m_Rows(i).GrpDate.Value
                dr(m_Rows(i).IsBSPEmp.FieldName) = m_Rows(i).IsBSPEmp.Value
                dr(m_Rows(i).LastChgComp.FieldName) = m_Rows(i).LastChgComp.Value
                dr(m_Rows(i).LastChgID.FieldName) = m_Rows(i).LastChgID.Value
                dr(m_Rows(i).LastChgDate.FieldName) = m_Rows(i).LastChgDate.Value
                dr(m_Rows(i).Rm_LastChgDate.FieldName) = m_Rows(i).Rm_LastChgDate.Value

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

        Public Sub Add(FamilyRow As Row)
            m_Rows.Add(FamilyRow)
        End Sub

        Public Sub Remove(FamilyRow As Row)
            If m_Rows.IndexOf(FamilyRow) >= 0 Then
                m_Rows.Remove(FamilyRow)
            End If
        End Sub

        Public Sub Dispose()
            m_Rows.Clear()
        End Sub

    End Class

    Public Class Row

        Private FI_IDNo As Field(Of String) = new Field(Of String)("IDNo", true)
        Private FI_RelativeIDNo As Field(Of String) = new Field(Of String)("RelativeIDNo", true)
        Private FI_RelativeID As Field(Of String) = new Field(Of String)("RelativeID", true)
        Private FI_Name As Field(Of String) = new Field(Of String)("Name", true)
        Private FI_NameN As Field(Of String) = new Field(Of String)("NameN", true)
        Private FI_BirthDate As Field(Of Date) = new Field(Of Date)("BirthDate", true)
        Private FI_Occupation As Field(Of String) = new Field(Of String)("Occupation", true)
        Private FI_IndustryType As Field(Of String) = new Field(Of String)("IndustryType", true)
        Private FI_Company As Field(Of String) = new Field(Of String)("Company", true)
        Private FI_DeleteMark As Field(Of String) = new Field(Of String)("DeleteMark", true)
        Private FI_HeaStatus As Field(Of String) = new Field(Of String)("HeaStatus", true)
        Private FI_GrpStatus As Field(Of String) = new Field(Of String)("GrpStatus", true)
        Private FI_IdentityID As Field(Of String) = new Field(Of String)("IdentityID", true)
        Private FI_GrpKind As Field(Of String) = new Field(Of String)("GrpKind", true)
        Private FI_HeaDate As Field(Of Date) = new Field(Of Date)("HeaDate", true)
        Private FI_GrpDate As Field(Of Date) = new Field(Of Date)("GrpDate", true)
        Private FI_IsBSPEmp As Field(Of String) = new Field(Of String)("IsBSPEmp", true)
        Private FI_LastChgComp As Field(Of String) = new Field(Of String)("LastChgComp", true)
        Private FI_LastChgID As Field(Of String) = new Field(Of String)("LastChgID", true)
        Private FI_LastChgDate As Field(Of Date) = new Field(Of Date)("LastChgDate", true)
        Private FI_Rm_LastChgDate As Field(Of Date) = new Field(Of Date)("Rm_LastChgDate", true)
        Private m_FieldNames As String() = { "IDNo", "RelativeIDNo", "RelativeID", "Name", "NameN", "BirthDate", "Occupation", "IndustryType", "Company", "DeleteMark" _
                                    , "HeaStatus", "GrpStatus", "IdentityID", "GrpKind", "HeaDate", "GrpDate", "IsBSPEmp", "LastChgComp", "LastChgID", "LastChgDate", "Rm_LastChgDate" }
        Private m_PrimaryFields As String() = { "IDNo", "RelativeIDNo" }
        Private m_IdentityFields As String() = {  }
        Private m_LoadFromDataRow As Boolean = False

        Private Function GetFieldValue(ByVal fieldName As String) As Object
            Select Case fieldName
                Case "IDNo"
                    Return FI_IDNo.Value
                Case "RelativeIDNo"
                    Return FI_RelativeIDNo.Value
                Case "RelativeID"
                    Return FI_RelativeID.Value
                Case "Name"
                    Return FI_Name.Value
                Case "NameN"
                    Return FI_NameN.Value
                Case "BirthDate"
                    Return FI_BirthDate.Value
                Case "Occupation"
                    Return FI_Occupation.Value
                Case "IndustryType"
                    Return FI_IndustryType.Value
                Case "Company"
                    Return FI_Company.Value
                Case "DeleteMark"
                    Return FI_DeleteMark.Value
                Case "HeaStatus"
                    Return FI_HeaStatus.Value
                Case "GrpStatus"
                    Return FI_GrpStatus.Value
                Case "IdentityID"
                    Return FI_IdentityID.Value
                Case "GrpKind"
                    Return FI_GrpKind.Value
                Case "HeaDate"
                    Return FI_HeaDate.Value
                Case "GrpDate"
                    Return FI_GrpDate.Value
                Case "IsBSPEmp"
                    Return FI_IsBSPEmp.Value
                Case "LastChgComp"
                    Return FI_LastChgComp.Value
                Case "LastChgID"
                    Return FI_LastChgID.Value
                Case "LastChgDate"
                    Return FI_LastChgDate.Value
                Case "Rm_LastChgDate"
                    Return FI_Rm_LastChgDate.Value
                Case Else
                    Return Nothing
            End Select
        End Function

        Private Sub SetFieldValue(ByVal fieldName As String, ByVal value As Object)
            Select Case fieldName
                Case "IDNo"
                    FI_IDNo.SetValue(value)
                Case "RelativeIDNo"
                    FI_RelativeIDNo.SetValue(value)
                Case "RelativeID"
                    FI_RelativeID.SetValue(value)
                Case "Name"
                    FI_Name.SetValue(value)
                Case "NameN"
                    FI_NameN.SetValue(value)
                Case "BirthDate"
                    FI_BirthDate.SetValue(value)
                Case "Occupation"
                    FI_Occupation.SetValue(value)
                Case "IndustryType"
                    FI_IndustryType.SetValue(value)
                Case "Company"
                    FI_Company.SetValue(value)
                Case "DeleteMark"
                    FI_DeleteMark.SetValue(value)
                Case "HeaStatus"
                    FI_HeaStatus.SetValue(value)
                Case "GrpStatus"
                    FI_GrpStatus.SetValue(value)
                Case "IdentityID"
                    FI_IdentityID.SetValue(value)
                Case "GrpKind"
                    FI_GrpKind.SetValue(value)
                Case "HeaDate"
                    FI_HeaDate.SetValue(value)
                Case "GrpDate"
                    FI_GrpDate.SetValue(value)
                Case "IsBSPEmp"
                    FI_IsBSPEmp.SetValue(value)
                Case "LastChgComp"
                    FI_LastChgComp.SetValue(value)
                Case "LastChgID"
                    FI_LastChgID.SetValue(value)
                Case "LastChgDate"
                    FI_LastChgDate.SetValue(value)
                Case "Rm_LastChgDate"
                    FI_Rm_LastChgDate.SetValue(value)
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
                Case "RelativeIDNo"
                    return FI_RelativeIDNo.Updated
                Case "RelativeID"
                    return FI_RelativeID.Updated
                Case "Name"
                    return FI_Name.Updated
                Case "NameN"
                    return FI_NameN.Updated
                Case "BirthDate"
                    return FI_BirthDate.Updated
                Case "Occupation"
                    return FI_Occupation.Updated
                Case "IndustryType"
                    return FI_IndustryType.Updated
                Case "Company"
                    return FI_Company.Updated
                Case "DeleteMark"
                    return FI_DeleteMark.Updated
                Case "HeaStatus"
                    return FI_HeaStatus.Updated
                Case "GrpStatus"
                    return FI_GrpStatus.Updated
                Case "IdentityID"
                    return FI_IdentityID.Updated
                Case "GrpKind"
                    return FI_GrpKind.Updated
                Case "HeaDate"
                    return FI_HeaDate.Updated
                Case "GrpDate"
                    return FI_GrpDate.Updated
                Case "IsBSPEmp"
                    return FI_IsBSPEmp.Updated
                Case "LastChgComp"
                    return FI_LastChgComp.Updated
                Case "LastChgID"
                    return FI_LastChgID.Updated
                Case "LastChgDate"
                    return FI_LastChgDate.Updated
                Case "Rm_LastChgDate"
                    return FI_Rm_LastChgDate.Updated
                Case Else
                    Throw New Exception("無此欄位！")
            End Select
        End Function

        Public Function CreateUpdateSQL(ByVal fieldName As String) As Boolean
            Select Case fieldName
                Case "IDNo"
                    return FI_IDNo.CreateUpdateSQL
                Case "RelativeIDNo"
                    return FI_RelativeIDNo.CreateUpdateSQL
                Case "RelativeID"
                    return FI_RelativeID.CreateUpdateSQL
                Case "Name"
                    return FI_Name.CreateUpdateSQL
                Case "NameN"
                    return FI_NameN.CreateUpdateSQL
                Case "BirthDate"
                    return FI_BirthDate.CreateUpdateSQL
                Case "Occupation"
                    return FI_Occupation.CreateUpdateSQL
                Case "IndustryType"
                    return FI_IndustryType.CreateUpdateSQL
                Case "Company"
                    return FI_Company.CreateUpdateSQL
                Case "DeleteMark"
                    return FI_DeleteMark.CreateUpdateSQL
                Case "HeaStatus"
                    return FI_HeaStatus.CreateUpdateSQL
                Case "GrpStatus"
                    return FI_GrpStatus.CreateUpdateSQL
                Case "IdentityID"
                    return FI_IdentityID.CreateUpdateSQL
                Case "GrpKind"
                    return FI_GrpKind.CreateUpdateSQL
                Case "HeaDate"
                    return FI_HeaDate.CreateUpdateSQL
                Case "GrpDate"
                    return FI_GrpDate.CreateUpdateSQL
                Case "IsBSPEmp"
                    return FI_IsBSPEmp.CreateUpdateSQL
                Case "LastChgComp"
                    return FI_LastChgComp.CreateUpdateSQL
                Case "LastChgID"
                    return FI_LastChgID.CreateUpdateSQL
                Case "LastChgDate"
                    return FI_LastChgDate.CreateUpdateSQL
                Case "Rm_LastChgDate"
                    return FI_Rm_LastChgDate.CreateUpdateSQL
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
            FI_RelativeIDNo.SetInitValue("")
            FI_RelativeID.SetInitValue("")
            FI_Name.SetInitValue("")
            FI_NameN.SetInitValue("")
            FI_BirthDate.SetInitValue(Convert.ToDateTime("1900/01/01"))
            FI_Occupation.SetInitValue("")
            FI_IndustryType.SetInitValue("")
            FI_Company.SetInitValue("")
            FI_DeleteMark.SetInitValue("0")
            FI_HeaStatus.SetInitValue("0")
            FI_GrpStatus.SetInitValue("0")
            FI_IdentityID.SetInitValue("01")
            FI_GrpKind.SetInitValue("0000000000")
            FI_HeaDate.SetInitValue(Convert.ToDateTime("1900/01/01"))
            FI_GrpDate.SetInitValue(Convert.ToDateTime("1900/01/01"))
            FI_IsBSPEmp.SetInitValue("0")
            FI_LastChgComp.SetInitValue("")
            FI_LastChgID.SetInitValue("")
            FI_LastChgDate.SetInitValue(Convert.ToDateTime("1900/01/01"))
            FI_Rm_LastChgDate.SetInitValue(Convert.ToDateTime("1900/01/01"))
        End Sub

        Public Sub New(ByVal dr As System.Data.DataRow)
            FI_IDNo.SetInitValue(dr("IDNo"))
            FI_RelativeIDNo.SetInitValue(dr("RelativeIDNo"))
            FI_RelativeID.SetInitValue(dr("RelativeID"))
            FI_Name.SetInitValue(dr("Name"))
            FI_NameN.SetInitValue(dr("NameN"))
            FI_BirthDate.SetInitValue(dr("BirthDate"))
            FI_Occupation.SetInitValue(dr("Occupation"))
            FI_IndustryType.SetInitValue(dr("IndustryType"))
            FI_Company.SetInitValue(dr("Company"))
            FI_DeleteMark.SetInitValue(dr("DeleteMark"))
            FI_HeaStatus.SetInitValue(dr("HeaStatus"))
            FI_GrpStatus.SetInitValue(dr("GrpStatus"))
            FI_IdentityID.SetInitValue(dr("IdentityID"))
            FI_GrpKind.SetInitValue(dr("GrpKind"))
            FI_HeaDate.SetInitValue(dr("HeaDate"))
            FI_GrpDate.SetInitValue(dr("GrpDate"))
            FI_IsBSPEmp.SetInitValue(dr("IsBSPEmp"))
            FI_LastChgComp.SetInitValue(dr("LastChgComp"))
            FI_LastChgID.SetInitValue(dr("LastChgID"))
            FI_LastChgDate.SetInitValue(dr("LastChgDate"))
            FI_Rm_LastChgDate.SetInitValue(dr("Rm_LastChgDate"))

            m_LoadFromDataRow = True
        End Sub

        Private Sub ClearUpdated()
            FI_IDNo.Updated = False
            FI_RelativeIDNo.Updated = False
            FI_RelativeID.Updated = False
            FI_Name.Updated = False
            FI_NameN.Updated = False
            FI_BirthDate.Updated = False
            FI_Occupation.Updated = False
            FI_IndustryType.Updated = False
            FI_Company.Updated = False
            FI_DeleteMark.Updated = False
            FI_HeaStatus.Updated = False
            FI_GrpStatus.Updated = False
            FI_IdentityID.Updated = False
            FI_GrpKind.Updated = False
            FI_HeaDate.Updated = False
            FI_GrpDate.Updated = False
            FI_IsBSPEmp.Updated = False
            FI_LastChgComp.Updated = False
            FI_LastChgID.Updated = False
            FI_LastChgDate.Updated = False
            FI_Rm_LastChgDate.Updated = False
        End Sub

        Public ReadOnly Property IDNo As Field(Of String) 
            Get
                Return FI_IDNo
            End Get
        End Property

        Public ReadOnly Property RelativeIDNo As Field(Of String) 
            Get
                Return FI_RelativeIDNo
            End Get
        End Property

        Public ReadOnly Property RelativeID As Field(Of String) 
            Get
                Return FI_RelativeID
            End Get
        End Property

        Public ReadOnly Property Name As Field(Of String) 
            Get
                Return FI_Name
            End Get
        End Property

        Public ReadOnly Property NameN As Field(Of String) 
            Get
                Return FI_NameN
            End Get
        End Property

        Public ReadOnly Property BirthDate As Field(Of Date) 
            Get
                Return FI_BirthDate
            End Get
        End Property

        Public ReadOnly Property Occupation As Field(Of String) 
            Get
                Return FI_Occupation
            End Get
        End Property

        Public ReadOnly Property IndustryType As Field(Of String) 
            Get
                Return FI_IndustryType
            End Get
        End Property

        Public ReadOnly Property Company As Field(Of String) 
            Get
                Return FI_Company
            End Get
        End Property

        Public ReadOnly Property DeleteMark As Field(Of String) 
            Get
                Return FI_DeleteMark
            End Get
        End Property

        Public ReadOnly Property HeaStatus As Field(Of String) 
            Get
                Return FI_HeaStatus
            End Get
        End Property

        Public ReadOnly Property GrpStatus As Field(Of String) 
            Get
                Return FI_GrpStatus
            End Get
        End Property

        Public ReadOnly Property IdentityID As Field(Of String) 
            Get
                Return FI_IdentityID
            End Get
        End Property

        Public ReadOnly Property GrpKind As Field(Of String) 
            Get
                Return FI_GrpKind
            End Get
        End Property

        Public ReadOnly Property HeaDate As Field(Of Date) 
            Get
                Return FI_HeaDate
            End Get
        End Property

        Public ReadOnly Property GrpDate As Field(Of Date) 
            Get
                Return FI_GrpDate
            End Get
        End Property

        Public ReadOnly Property IsBSPEmp As Field(Of String) 
            Get
                Return FI_IsBSPEmp
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

        Public ReadOnly Property Rm_LastChgDate As Field(Of Date) 
            Get
                Return FI_Rm_LastChgDate
            End Get
        End Property

    End Class

    Public Class Service
        Public Function DeleteRowByPrimaryKey(ByVal FamilyRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Delete From Family")
            strSQL.AppendLine("Where IDNo = @IDNo")
            strSQL.AppendLine("And RelativeIDNo = @RelativeIDNo")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@IDNo", DbType.String, FamilyRow.IDNo.Value)
            db.AddInParameter(dbcmd, "@RelativeIDNo", DbType.String, FamilyRow.RelativeIDNo.Value)

            return db.ExecuteNonQuery(dbcmd)
        End Function

        public Function DeleteRowByPrimaryKey(ByVal FamilyRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Delete From Family")
            strSQL.AppendLine("Where IDNo = @IDNo")
            strSQL.AppendLine("And RelativeIDNo = @RelativeIDNo")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@IDNo", DbType.String, FamilyRow.IDNo.Value)
            db.AddInParameter(dbcmd, "@RelativeIDNo", DbType.String, FamilyRow.RelativeIDNo.Value)

            return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function DeleteRowByPrimaryKey(ByVal FamilyRow As Row()) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Delete From Family")
            strSQL.AppendLine("Where IDNo = @IDNo")
            strSQL.AppendLine("And RelativeIDNo = @RelativeIDNo")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            using cn As DbConnection = db.CreateConnection()
                cn.Open()
                Dim tran As DbTransaction = cn.BeginTransaction()
                Dim inTrans As Boolean = True

                Try
                    For Each r As Row In FamilyRow
                        dbcmd.Parameters.Clear()
                        db.AddInParameter(dbcmd, "@IDNo", DbType.String, r.IDNo.Value)
                        db.AddInParameter(dbcmd, "@RelativeIDNo", DbType.String, r.RelativeIDNo.Value)

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

        Public Function DeleteRowByPrimaryKey(ByVal FamilyRow As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Delete From Family")
            strSQL.AppendLine("Where IDNo = @IDNo")
            strSQL.AppendLine("And RelativeIDNo = @RelativeIDNo")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())

            For Each r As Row In FamilyRow
                dbcmd.Parameters.Clear()
                db.AddInParameter(dbcmd, "@IDNo", DbType.String, r.IDNo.Value)
                db.AddInParameter(dbcmd, "@RelativeIDNo", DbType.String, r.RelativeIDNo.Value)

                intRowsAffected += db.ExecuteNonQuery(dbcmd, tran)
            Next
            Return intRowsAffected
        End Function

        Public Function QueryByKey(ByVal FamilyRow As Row) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From Family")
            strSQL.AppendLine("Where IDNo = @IDNo")
            strSQL.AppendLine("And RelativeIDNo = @RelativeIDNo")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@IDNo", DbType.String, FamilyRow.IDNo.Value)
            db.AddInParameter(dbcmd, "@RelativeIDNo", DbType.String, FamilyRow.RelativeIDNo.Value)

            Return db.ExecuteDataSet(dbcmd)
        End Function

        Public Function QueryByKey(FamilyRow As Row, tran As DbTransaction) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From Family")
            strSQL.AppendLine("Where IDNo = @IDNo")
            strSQL.AppendLine("And RelativeIDNo = @RelativeIDNo")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@IDNo", DbType.String, FamilyRow.IDNo.Value)
            db.AddInParameter(dbcmd, "@RelativeIDNo", DbType.String, FamilyRow.RelativeIDNo.Value)

            Return db.ExecuteDataSet(dbcmd, tran)
        End Function

        Public Function Update(ByVal FamilyRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim strDot As String = String.Empty

            strSQL.AppendLine("Update Family Set")
            For i As Integer = 0 To FamilyRow.FieldNames.Length - 1
                If Not FamilyRow.IsIdentityField(FamilyRow.FieldNames(i)) AndAlso FamilyRow.IsUpdated(FamilyRow.FieldNames(i)) AndAlso FamilyRow.CreateUpdateSQL(FamilyRow.FieldNames(i)) Then
                    strSQL.AppendLine(string.Format("{0}{1} = @{1}", strDot, FamilyRow.FieldNames(i)))
                    strDot = ","
                End If
            Next
            If strDot = String.Empty Then Throw New Exception("未異動資料欄位！")
            strSQL.AppendLine("Where IDNo = @PKIDNo")
            strSQL.AppendLine("And RelativeIDNo = @PKRelativeIDNo")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            If FamilyRow.IDNo.Updated Then db.AddInParameter(dbcmd, "@IDNo", DbType.String, FamilyRow.IDNo.Value)
            If FamilyRow.RelativeIDNo.Updated Then db.AddInParameter(dbcmd, "@RelativeIDNo", DbType.String, FamilyRow.RelativeIDNo.Value)
            If FamilyRow.RelativeID.Updated Then db.AddInParameter(dbcmd, "@RelativeID", DbType.String, FamilyRow.RelativeID.Value)
            If FamilyRow.Name.Updated Then db.AddInParameter(dbcmd, "@Name", DbType.String, FamilyRow.Name.Value)
            If FamilyRow.NameN.Updated Then db.AddInParameter(dbcmd, "@NameN", DbType.String, FamilyRow.NameN.Value)
            If FamilyRow.BirthDate.Updated Then db.AddInParameter(dbcmd, "@BirthDate", DbType.Date, IIf(IsDateTimeNull(FamilyRow.BirthDate.Value), Convert.ToDateTime("1900/1/1"), FamilyRow.BirthDate.Value))
            If FamilyRow.Occupation.Updated Then db.AddInParameter(dbcmd, "@Occupation", DbType.String, FamilyRow.Occupation.Value)
            If FamilyRow.IndustryType.Updated Then db.AddInParameter(dbcmd, "@IndustryType", DbType.String, FamilyRow.IndustryType.Value)
            If FamilyRow.Company.Updated Then db.AddInParameter(dbcmd, "@Company", DbType.String, FamilyRow.Company.Value)
            If FamilyRow.DeleteMark.Updated Then db.AddInParameter(dbcmd, "@DeleteMark", DbType.String, FamilyRow.DeleteMark.Value)
            If FamilyRow.HeaStatus.Updated Then db.AddInParameter(dbcmd, "@HeaStatus", DbType.String, FamilyRow.HeaStatus.Value)
            If FamilyRow.GrpStatus.Updated Then db.AddInParameter(dbcmd, "@GrpStatus", DbType.String, FamilyRow.GrpStatus.Value)
            If FamilyRow.IdentityID.Updated Then db.AddInParameter(dbcmd, "@IdentityID", DbType.String, FamilyRow.IdentityID.Value)
            If FamilyRow.GrpKind.Updated Then db.AddInParameter(dbcmd, "@GrpKind", DbType.String, FamilyRow.GrpKind.Value)
            If FamilyRow.HeaDate.Updated Then db.AddInParameter(dbcmd, "@HeaDate", DbType.Date, IIf(IsDateTimeNull(FamilyRow.HeaDate.Value), Convert.ToDateTime("1900/1/1"), FamilyRow.HeaDate.Value))
            If FamilyRow.GrpDate.Updated Then db.AddInParameter(dbcmd, "@GrpDate", DbType.Date, IIf(IsDateTimeNull(FamilyRow.GrpDate.Value), Convert.ToDateTime("1900/1/1"), FamilyRow.GrpDate.Value))
            If FamilyRow.IsBSPEmp.Updated Then db.AddInParameter(dbcmd, "@IsBSPEmp", DbType.String, FamilyRow.IsBSPEmp.Value)
            If FamilyRow.LastChgComp.Updated Then db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, FamilyRow.LastChgComp.Value)
            If FamilyRow.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, FamilyRow.LastChgID.Value)
            If FamilyRow.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(FamilyRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), FamilyRow.LastChgDate.Value))
            If FamilyRow.Rm_LastChgDate.Updated Then db.AddInParameter(dbcmd, "@Rm_LastChgDate", DbType.Date, IIf(IsDateTimeNull(FamilyRow.Rm_LastChgDate.Value), Convert.ToDateTime("1900/1/1"), FamilyRow.Rm_LastChgDate.Value))
            db.AddInParameter(dbcmd, "@PKIDNo", DbType.String, IIf(FamilyRow.LoadFromDataRow, FamilyRow.IDNo.OldValue, FamilyRow.IDNo.Value))
            db.AddInParameter(dbcmd, "@PKRelativeIDNo", DbType.String, IIf(FamilyRow.LoadFromDataRow, FamilyRow.RelativeIDNo.OldValue, FamilyRow.RelativeIDNo.Value))

            Return db.ExecuteNonQuery(dbcmd)
        End Function

        Public Function Update(ByVal FamilyRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim strDot As String = String.Empty

            strSQL.AppendLine("Update Family Set")
            For i As Integer = 0 To FamilyRow.FieldNames.Length - 1
                If Not FamilyRow.IsIdentityField(FamilyRow.FieldNames(i)) AndAlso FamilyRow.IsUpdated(FamilyRow.FieldNames(i)) AndAlso FamilyRow.CreateUpdateSQL(FamilyRow.FieldNames(i)) Then
                    strSQL.AppendLine(string.Format("{0}{1} = @{1}", strDot, FamilyRow.FieldNames(i)))
                    strDot = ","
                End If
            Next 
            If strDot = String.Empty Then Throw New Exception("未異動資料欄位！")
            strSQL.AppendLine("Where IDNo = @PKIDNo")
            strSQL.AppendLine("And RelativeIDNo = @PKRelativeIDNo")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            If FamilyRow.IDNo.Updated Then db.AddInParameter(dbcmd, "@IDNo", DbType.String, FamilyRow.IDNo.Value)
            If FamilyRow.RelativeIDNo.Updated Then db.AddInParameter(dbcmd, "@RelativeIDNo", DbType.String, FamilyRow.RelativeIDNo.Value)
            If FamilyRow.RelativeID.Updated Then db.AddInParameter(dbcmd, "@RelativeID", DbType.String, FamilyRow.RelativeID.Value)
            If FamilyRow.Name.Updated Then db.AddInParameter(dbcmd, "@Name", DbType.String, FamilyRow.Name.Value)
            If FamilyRow.NameN.Updated Then db.AddInParameter(dbcmd, "@NameN", DbType.String, FamilyRow.NameN.Value)
            If FamilyRow.BirthDate.Updated Then db.AddInParameter(dbcmd, "@BirthDate", DbType.Date, IIf(IsDateTimeNull(FamilyRow.BirthDate.Value), Convert.ToDateTime("1900/1/1"), FamilyRow.BirthDate.Value))
            If FamilyRow.Occupation.Updated Then db.AddInParameter(dbcmd, "@Occupation", DbType.String, FamilyRow.Occupation.Value)
            If FamilyRow.IndustryType.Updated Then db.AddInParameter(dbcmd, "@IndustryType", DbType.String, FamilyRow.IndustryType.Value)
            If FamilyRow.Company.Updated Then db.AddInParameter(dbcmd, "@Company", DbType.String, FamilyRow.Company.Value)
            If FamilyRow.DeleteMark.Updated Then db.AddInParameter(dbcmd, "@DeleteMark", DbType.String, FamilyRow.DeleteMark.Value)
            If FamilyRow.HeaStatus.Updated Then db.AddInParameter(dbcmd, "@HeaStatus", DbType.String, FamilyRow.HeaStatus.Value)
            If FamilyRow.GrpStatus.Updated Then db.AddInParameter(dbcmd, "@GrpStatus", DbType.String, FamilyRow.GrpStatus.Value)
            If FamilyRow.IdentityID.Updated Then db.AddInParameter(dbcmd, "@IdentityID", DbType.String, FamilyRow.IdentityID.Value)
            If FamilyRow.GrpKind.Updated Then db.AddInParameter(dbcmd, "@GrpKind", DbType.String, FamilyRow.GrpKind.Value)
            If FamilyRow.HeaDate.Updated Then db.AddInParameter(dbcmd, "@HeaDate", DbType.Date, IIf(IsDateTimeNull(FamilyRow.HeaDate.Value), Convert.ToDateTime("1900/1/1"), FamilyRow.HeaDate.Value))
            If FamilyRow.GrpDate.Updated Then db.AddInParameter(dbcmd, "@GrpDate", DbType.Date, IIf(IsDateTimeNull(FamilyRow.GrpDate.Value), Convert.ToDateTime("1900/1/1"), FamilyRow.GrpDate.Value))
            If FamilyRow.IsBSPEmp.Updated Then db.AddInParameter(dbcmd, "@IsBSPEmp", DbType.String, FamilyRow.IsBSPEmp.Value)
            If FamilyRow.LastChgComp.Updated Then db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, FamilyRow.LastChgComp.Value)
            If FamilyRow.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, FamilyRow.LastChgID.Value)
            If FamilyRow.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(FamilyRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), FamilyRow.LastChgDate.Value))
            If FamilyRow.Rm_LastChgDate.Updated Then db.AddInParameter(dbcmd, "@Rm_LastChgDate", DbType.Date, IIf(IsDateTimeNull(FamilyRow.Rm_LastChgDate.Value), Convert.ToDateTime("1900/1/1"), FamilyRow.Rm_LastChgDate.Value))
            db.AddInParameter(dbcmd, "@PKIDNo", DbType.String, IIf(FamilyRow.LoadFromDataRow, FamilyRow.IDNo.OldValue, FamilyRow.IDNo.Value))
            db.AddInParameter(dbcmd, "@PKRelativeIDNo", DbType.String, IIf(FamilyRow.LoadFromDataRow, FamilyRow.RelativeIDNo.OldValue, FamilyRow.RelativeIDNo.Value))

            Return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function Update(ByVal FamilyRow As Row()) As Integer
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
                    For Each r As Row In FamilyRow
                        strDot = String.Empty
                        If strSQL.Length > 0 Then strSQL.Remove(0, strSQL.Length)
                        strSQL.AppendLine("Update Family Set")
                        For i As Integer = 0 To r.FieldNames.Length - 1
                            If Not r.IsIdentityField(r.FieldNames(i)) AndAlso r.IsUpdated(r.FieldNames(i)) AndAlso r.CreateUpdateSQL(r.FieldNames(i)) Then
                                strSQL.AppendLine(String.Format("{0}{1} = @{1}", strDot, r.FieldNames(i)))
                                strDot = ","
                            End If
                        Next
                        If strDot = String.Empty Then Continue For
                        strSQL.AppendLine("Where IDNo = @PKIDNo")
                        strSQL.AppendLine("And RelativeIDNo = @PKRelativeIDNo")

                        dbcmd = db.GetSqlStringCommand(strSQL.ToString())
                        If r.IDNo.Updated Then db.AddInParameter(dbcmd, "@IDNo", DbType.String, r.IDNo.Value)
                        If r.RelativeIDNo.Updated Then db.AddInParameter(dbcmd, "@RelativeIDNo", DbType.String, r.RelativeIDNo.Value)
                        If r.RelativeID.Updated Then db.AddInParameter(dbcmd, "@RelativeID", DbType.String, r.RelativeID.Value)
                        If r.Name.Updated Then db.AddInParameter(dbcmd, "@Name", DbType.String, r.Name.Value)
                        If r.NameN.Updated Then db.AddInParameter(dbcmd, "@NameN", DbType.String, r.NameN.Value)
                        If r.BirthDate.Updated Then db.AddInParameter(dbcmd, "@BirthDate", DbType.Date, IIf(IsDateTimeNull(r.BirthDate.Value), Convert.ToDateTime("1900/1/1"), r.BirthDate.Value))
                        If r.Occupation.Updated Then db.AddInParameter(dbcmd, "@Occupation", DbType.String, r.Occupation.Value)
                        If r.IndustryType.Updated Then db.AddInParameter(dbcmd, "@IndustryType", DbType.String, r.IndustryType.Value)
                        If r.Company.Updated Then db.AddInParameter(dbcmd, "@Company", DbType.String, r.Company.Value)
                        If r.DeleteMark.Updated Then db.AddInParameter(dbcmd, "@DeleteMark", DbType.String, r.DeleteMark.Value)
                        If r.HeaStatus.Updated Then db.AddInParameter(dbcmd, "@HeaStatus", DbType.String, r.HeaStatus.Value)
                        If r.GrpStatus.Updated Then db.AddInParameter(dbcmd, "@GrpStatus", DbType.String, r.GrpStatus.Value)
                        If r.IdentityID.Updated Then db.AddInParameter(dbcmd, "@IdentityID", DbType.String, r.IdentityID.Value)
                        If r.GrpKind.Updated Then db.AddInParameter(dbcmd, "@GrpKind", DbType.String, r.GrpKind.Value)
                        If r.HeaDate.Updated Then db.AddInParameter(dbcmd, "@HeaDate", DbType.Date, IIf(IsDateTimeNull(r.HeaDate.Value), Convert.ToDateTime("1900/1/1"), r.HeaDate.Value))
                        If r.GrpDate.Updated Then db.AddInParameter(dbcmd, "@GrpDate", DbType.Date, IIf(IsDateTimeNull(r.GrpDate.Value), Convert.ToDateTime("1900/1/1"), r.GrpDate.Value))
                        If r.IsBSPEmp.Updated Then db.AddInParameter(dbcmd, "@IsBSPEmp", DbType.String, r.IsBSPEmp.Value)
                        If r.LastChgComp.Updated Then db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, r.LastChgComp.Value)
                        If r.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, r.LastChgID.Value)
                        If r.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(r.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), r.LastChgDate.Value))
                        If r.Rm_LastChgDate.Updated Then db.AddInParameter(dbcmd, "@Rm_LastChgDate", DbType.Date, IIf(IsDateTimeNull(r.Rm_LastChgDate.Value), Convert.ToDateTime("1900/1/1"), r.Rm_LastChgDate.Value))
                        db.AddInParameter(dbcmd, "@PKIDNo", DbType.String, IIf(r.LoadFromDataRow, r.IDNo.OldValue, r.IDNo.Value))
                        db.AddInParameter(dbcmd, "@PKRelativeIDNo", DbType.String, IIf(r.LoadFromDataRow, r.RelativeIDNo.OldValue, r.RelativeIDNo.Value))

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

        Public Function Update(ByVal FamilyRow As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0
            Dim strDot As String

            For Each r As Row In FamilyRow
                strDot = String.Empty
                If strSQL.Length > 0 Then strSQL.Remove(0, strSQL.Length)
                strSQL.AppendLine("Update Family Set")
                For i As Integer = 0 To r.FieldNames.Length - 1
                    If Not r.IsIdentityField(r.FieldNames(i)) AndAlso r.IsUpdated(r.FieldNames(i)) AndAlso r.CreateUpdateSQL(r.FieldNames(i))
                        strSQL.AppendLine(String.Format("{0}{1} = @{1}", strDot, r.FieldNames(i)))
                        strDot = ","
                    End If
                Next
                If strDot = String.Empty Then Continue For
                strSQL.AppendLine("Where IDNo = @PKIDNo")
                strSQL.AppendLine("And RelativeIDNo = @PKRelativeIDNo")

                dbcmd = db.GetSqlStringCommand(strSQL.ToString())
                If r.IDNo.Updated Then db.AddInParameter(dbcmd, "@IDNo", DbType.String, r.IDNo.Value)
                If r.RelativeIDNo.Updated Then db.AddInParameter(dbcmd, "@RelativeIDNo", DbType.String, r.RelativeIDNo.Value)
                If r.RelativeID.Updated Then db.AddInParameter(dbcmd, "@RelativeID", DbType.String, r.RelativeID.Value)
                If r.Name.Updated Then db.AddInParameter(dbcmd, "@Name", DbType.String, r.Name.Value)
                If r.NameN.Updated Then db.AddInParameter(dbcmd, "@NameN", DbType.String, r.NameN.Value)
                If r.BirthDate.Updated Then db.AddInParameter(dbcmd, "@BirthDate", DbType.Date, IIf(IsDateTimeNull(r.BirthDate.Value), Convert.ToDateTime("1900/1/1"), r.BirthDate.Value))
                If r.Occupation.Updated Then db.AddInParameter(dbcmd, "@Occupation", DbType.String, r.Occupation.Value)
                If r.IndustryType.Updated Then db.AddInParameter(dbcmd, "@IndustryType", DbType.String, r.IndustryType.Value)
                If r.Company.Updated Then db.AddInParameter(dbcmd, "@Company", DbType.String, r.Company.Value)
                If r.DeleteMark.Updated Then db.AddInParameter(dbcmd, "@DeleteMark", DbType.String, r.DeleteMark.Value)
                If r.HeaStatus.Updated Then db.AddInParameter(dbcmd, "@HeaStatus", DbType.String, r.HeaStatus.Value)
                If r.GrpStatus.Updated Then db.AddInParameter(dbcmd, "@GrpStatus", DbType.String, r.GrpStatus.Value)
                If r.IdentityID.Updated Then db.AddInParameter(dbcmd, "@IdentityID", DbType.String, r.IdentityID.Value)
                If r.GrpKind.Updated Then db.AddInParameter(dbcmd, "@GrpKind", DbType.String, r.GrpKind.Value)
                If r.HeaDate.Updated Then db.AddInParameter(dbcmd, "@HeaDate", DbType.Date, IIf(IsDateTimeNull(r.HeaDate.Value), Convert.ToDateTime("1900/1/1"), r.HeaDate.Value))
                If r.GrpDate.Updated Then db.AddInParameter(dbcmd, "@GrpDate", DbType.Date, IIf(IsDateTimeNull(r.GrpDate.Value), Convert.ToDateTime("1900/1/1"), r.GrpDate.Value))
                If r.IsBSPEmp.Updated Then db.AddInParameter(dbcmd, "@IsBSPEmp", DbType.String, r.IsBSPEmp.Value)
                If r.LastChgComp.Updated Then db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, r.LastChgComp.Value)
                If r.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, r.LastChgID.Value)
                If r.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(r.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), r.LastChgDate.Value))
                If r.Rm_LastChgDate.Updated Then db.AddInParameter(dbcmd, "@Rm_LastChgDate", DbType.Date, IIf(IsDateTimeNull(r.Rm_LastChgDate.Value), Convert.ToDateTime("1900/1/1"), r.Rm_LastChgDate.Value))
                db.AddInParameter(dbcmd, "@PKIDNo", DbType.String, IIf(r.LoadFromDataRow, r.IDNo.OldValue, r.IDNo.Value))
                db.AddInParameter(dbcmd, "@PKRelativeIDNo", DbType.String, IIf(r.LoadFromDataRow, r.RelativeIDNo.OldValue, r.RelativeIDNo.Value))

                intRowsAffected += db.ExecuteNonQuery(dbcmd, tran)
            Next
            Return intRowsAffected
        End Function

        Public Function IsDataExists(ByVal FamilyRow As Row) As Boolean
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intCount As Integer = 0

            strSQL.AppendLine("Select Count(*) Cnt From Family")
            strSQL.AppendLine("Where IDNo = @IDNo")
            strSQL.AppendLine("And RelativeIDNo = @RelativeIDNo")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@IDNo", DbType.String, FamilyRow.IDNo.Value)
            db.AddInParameter(dbcmd, "@RelativeIDNo", DbType.String, FamilyRow.RelativeIDNo.Value)

            intCount = Convert.ToInt32(db.ExecuteScalar(dbcmd))
            Return IIf(intCount > 0, True, False)
        End Function

        Public Function IsDataExists(ByVal FamilyRow As Row, ByVal tran As DbTransaction) As Boolean
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intCount As Integer = 0

            strSQL.AppendLine("Select Count(*) Cnt From Family")
            strSQL.AppendLine("Where IDNo = @IDNo")
            strSQL.AppendLine("And RelativeIDNo = @RelativeIDNo")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@IDNo", DbType.String, FamilyRow.IDNo.Value)
            db.AddInParameter(dbcmd, "@RelativeIDNo", DbType.String, FamilyRow.RelativeIDNo.Value)

            intCount = Convert.ToInt32(db.ExecuteScalar(dbcmd, tran))
            Return IIf(intCount > 0, True, False)
        End Function

        Public Function QuerybyWhere(WhereCondition As String) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From Family")
            If WhereCondition <> String.Empty Then strSQL.AppendLine(WhereCondition)

            Return db.ExecuteDataSet(CommandType.Text, strSQL.ToString())
        End Function

        Public Function Insert(ByVal FamilyRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Insert into Family")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    IDNo, RelativeIDNo, RelativeID, Name, NameN, BirthDate, Occupation, IndustryType, Company,")
            strSQL.AppendLine("    DeleteMark, HeaStatus, GrpStatus, IdentityID, GrpKind, HeaDate, GrpDate, IsBSPEmp,")
            strSQL.AppendLine("    LastChgComp, LastChgID, LastChgDate, Rm_LastChgDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @IDNo, @RelativeIDNo, @RelativeID, @Name, @NameN, @BirthDate, @Occupation, @IndustryType, @Company,")
            strSQL.AppendLine("    @DeleteMark, @HeaStatus, @GrpStatus, @IdentityID, @GrpKind, @HeaDate, @GrpDate, @IsBSPEmp,")
            strSQL.AppendLine("    @LastChgComp, @LastChgID, @LastChgDate, @Rm_LastChgDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@IDNo", DbType.String, FamilyRow.IDNo.Value)
            db.AddInParameter(dbcmd, "@RelativeIDNo", DbType.String, FamilyRow.RelativeIDNo.Value)
            db.AddInParameter(dbcmd, "@RelativeID", DbType.String, FamilyRow.RelativeID.Value)
            db.AddInParameter(dbcmd, "@Name", DbType.String, FamilyRow.Name.Value)
            db.AddInParameter(dbcmd, "@NameN", DbType.String, FamilyRow.NameN.Value)
            db.AddInParameter(dbcmd, "@BirthDate", DbType.Date, IIf(IsDateTimeNull(FamilyRow.BirthDate.Value), Convert.ToDateTime("1900/1/1"), FamilyRow.BirthDate.Value))
            db.AddInParameter(dbcmd, "@Occupation", DbType.String, FamilyRow.Occupation.Value)
            db.AddInParameter(dbcmd, "@IndustryType", DbType.String, FamilyRow.IndustryType.Value)
            db.AddInParameter(dbcmd, "@Company", DbType.String, FamilyRow.Company.Value)
            db.AddInParameter(dbcmd, "@DeleteMark", DbType.String, FamilyRow.DeleteMark.Value)
            db.AddInParameter(dbcmd, "@HeaStatus", DbType.String, FamilyRow.HeaStatus.Value)
            db.AddInParameter(dbcmd, "@GrpStatus", DbType.String, FamilyRow.GrpStatus.Value)
            db.AddInParameter(dbcmd, "@IdentityID", DbType.String, FamilyRow.IdentityID.Value)
            db.AddInParameter(dbcmd, "@GrpKind", DbType.String, FamilyRow.GrpKind.Value)
            db.AddInParameter(dbcmd, "@HeaDate", DbType.Date, IIf(IsDateTimeNull(FamilyRow.HeaDate.Value), Convert.ToDateTime("1900/1/1"), FamilyRow.HeaDate.Value))
            db.AddInParameter(dbcmd, "@GrpDate", DbType.Date, IIf(IsDateTimeNull(FamilyRow.GrpDate.Value), Convert.ToDateTime("1900/1/1"), FamilyRow.GrpDate.Value))
            db.AddInParameter(dbcmd, "@IsBSPEmp", DbType.String, FamilyRow.IsBSPEmp.Value)
            db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, FamilyRow.LastChgComp.Value)
            db.AddInParameter(dbcmd, "@LastChgID", DbType.String, FamilyRow.LastChgID.Value)
            db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(FamilyRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), FamilyRow.LastChgDate.Value))
            db.AddInParameter(dbcmd, "@Rm_LastChgDate", DbType.Date, IIf(IsDateTimeNull(FamilyRow.Rm_LastChgDate.Value), Convert.ToDateTime("1900/1/1"), FamilyRow.Rm_LastChgDate.Value))

            Return db.ExecuteNonQuery(dbcmd)
        End Function

        Public Function Insert(ByVal FamilyRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Insert into Family")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    IDNo, RelativeIDNo, RelativeID, Name, NameN, BirthDate, Occupation, IndustryType, Company,")
            strSQL.AppendLine("    DeleteMark, HeaStatus, GrpStatus, IdentityID, GrpKind, HeaDate, GrpDate, IsBSPEmp,")
            strSQL.AppendLine("    LastChgComp, LastChgID, LastChgDate, Rm_LastChgDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @IDNo, @RelativeIDNo, @RelativeID, @Name, @NameN, @BirthDate, @Occupation, @IndustryType, @Company,")
            strSQL.AppendLine("    @DeleteMark, @HeaStatus, @GrpStatus, @IdentityID, @GrpKind, @HeaDate, @GrpDate, @IsBSPEmp,")
            strSQL.AppendLine("    @LastChgComp, @LastChgID, @LastChgDate, @Rm_LastChgDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@IDNo", DbType.String, FamilyRow.IDNo.Value)
            db.AddInParameter(dbcmd, "@RelativeIDNo", DbType.String, FamilyRow.RelativeIDNo.Value)
            db.AddInParameter(dbcmd, "@RelativeID", DbType.String, FamilyRow.RelativeID.Value)
            db.AddInParameter(dbcmd, "@Name", DbType.String, FamilyRow.Name.Value)
            db.AddInParameter(dbcmd, "@NameN", DbType.String, FamilyRow.NameN.Value)
            db.AddInParameter(dbcmd, "@BirthDate", DbType.Date, IIf(IsDateTimeNull(FamilyRow.BirthDate.Value), Convert.ToDateTime("1900/1/1"), FamilyRow.BirthDate.Value))
            db.AddInParameter(dbcmd, "@Occupation", DbType.String, FamilyRow.Occupation.Value)
            db.AddInParameter(dbcmd, "@IndustryType", DbType.String, FamilyRow.IndustryType.Value)
            db.AddInParameter(dbcmd, "@Company", DbType.String, FamilyRow.Company.Value)
            db.AddInParameter(dbcmd, "@DeleteMark", DbType.String, FamilyRow.DeleteMark.Value)
            db.AddInParameter(dbcmd, "@HeaStatus", DbType.String, FamilyRow.HeaStatus.Value)
            db.AddInParameter(dbcmd, "@GrpStatus", DbType.String, FamilyRow.GrpStatus.Value)
            db.AddInParameter(dbcmd, "@IdentityID", DbType.String, FamilyRow.IdentityID.Value)
            db.AddInParameter(dbcmd, "@GrpKind", DbType.String, FamilyRow.GrpKind.Value)
            db.AddInParameter(dbcmd, "@HeaDate", DbType.Date, IIf(IsDateTimeNull(FamilyRow.HeaDate.Value), Convert.ToDateTime("1900/1/1"), FamilyRow.HeaDate.Value))
            db.AddInParameter(dbcmd, "@GrpDate", DbType.Date, IIf(IsDateTimeNull(FamilyRow.GrpDate.Value), Convert.ToDateTime("1900/1/1"), FamilyRow.GrpDate.Value))
            db.AddInParameter(dbcmd, "@IsBSPEmp", DbType.String, FamilyRow.IsBSPEmp.Value)
            db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, FamilyRow.LastChgComp.Value)
            db.AddInParameter(dbcmd, "@LastChgID", DbType.String, FamilyRow.LastChgID.Value)
            db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(FamilyRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), FamilyRow.LastChgDate.Value))
            db.AddInParameter(dbcmd, "@Rm_LastChgDate", DbType.Date, IIf(IsDateTimeNull(FamilyRow.Rm_LastChgDate.Value), Convert.ToDateTime("1900/1/1"), FamilyRow.Rm_LastChgDate.Value))

            Return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function Insert(ByVal FamilyRow As Row()) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Insert into Family")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    IDNo, RelativeIDNo, RelativeID, Name, NameN, BirthDate, Occupation, IndustryType, Company,")
            strSQL.AppendLine("    DeleteMark, HeaStatus, GrpStatus, IdentityID, GrpKind, HeaDate, GrpDate, IsBSPEmp,")
            strSQL.AppendLine("    LastChgComp, LastChgID, LastChgDate, Rm_LastChgDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @IDNo, @RelativeIDNo, @RelativeID, @Name, @NameN, @BirthDate, @Occupation, @IndustryType, @Company,")
            strSQL.AppendLine("    @DeleteMark, @HeaStatus, @GrpStatus, @IdentityID, @GrpKind, @HeaDate, @GrpDate, @IsBSPEmp,")
            strSQL.AppendLine("    @LastChgComp, @LastChgID, @LastChgDate, @Rm_LastChgDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            Using cn As DbConnection = db.CreateConnection()
                cn.Open()
                Dim tran As DbTransaction = cn.BeginTransaction()
                Dim inTrans As Boolean = True

                Try
                    For Each r As Row In FamilyRow
                        dbcmd.Parameters.Clear()
                        db.AddInParameter(dbcmd, "@IDNo", DbType.String, r.IDNo.Value)
                        db.AddInParameter(dbcmd, "@RelativeIDNo", DbType.String, r.RelativeIDNo.Value)
                        db.AddInParameter(dbcmd, "@RelativeID", DbType.String, r.RelativeID.Value)
                        db.AddInParameter(dbcmd, "@Name", DbType.String, r.Name.Value)
                        db.AddInParameter(dbcmd, "@NameN", DbType.String, r.NameN.Value)
                        db.AddInParameter(dbcmd, "@BirthDate", DbType.Date, IIf(IsDateTimeNull(r.BirthDate.Value), Convert.ToDateTime("1900/1/1"), r.BirthDate.Value))
                        db.AddInParameter(dbcmd, "@Occupation", DbType.String, r.Occupation.Value)
                        db.AddInParameter(dbcmd, "@IndustryType", DbType.String, r.IndustryType.Value)
                        db.AddInParameter(dbcmd, "@Company", DbType.String, r.Company.Value)
                        db.AddInParameter(dbcmd, "@DeleteMark", DbType.String, r.DeleteMark.Value)
                        db.AddInParameter(dbcmd, "@HeaStatus", DbType.String, r.HeaStatus.Value)
                        db.AddInParameter(dbcmd, "@GrpStatus", DbType.String, r.GrpStatus.Value)
                        db.AddInParameter(dbcmd, "@IdentityID", DbType.String, r.IdentityID.Value)
                        db.AddInParameter(dbcmd, "@GrpKind", DbType.String, r.GrpKind.Value)
                        db.AddInParameter(dbcmd, "@HeaDate", DbType.Date, IIf(IsDateTimeNull(r.HeaDate.Value), Convert.ToDateTime("1900/1/1"), r.HeaDate.Value))
                        db.AddInParameter(dbcmd, "@GrpDate", DbType.Date, IIf(IsDateTimeNull(r.GrpDate.Value), Convert.ToDateTime("1900/1/1"), r.GrpDate.Value))
                        db.AddInParameter(dbcmd, "@IsBSPEmp", DbType.String, r.IsBSPEmp.Value)
                        db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, r.LastChgComp.Value)
                        db.AddInParameter(dbcmd, "@LastChgID", DbType.String, r.LastChgID.Value)
                        db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(r.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), r.LastChgDate.Value))
                        db.AddInParameter(dbcmd, "@Rm_LastChgDate", DbType.Date, IIf(IsDateTimeNull(r.Rm_LastChgDate.Value), Convert.ToDateTime("1900/1/1"), r.Rm_LastChgDate.Value))

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

        Public Function Insert(ByVal FamilyRow As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Insert into Family")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    IDNo, RelativeIDNo, RelativeID, Name, NameN, BirthDate, Occupation, IndustryType, Company,")
            strSQL.AppendLine("    DeleteMark, HeaStatus, GrpStatus, IdentityID, GrpKind, HeaDate, GrpDate, IsBSPEmp,")
            strSQL.AppendLine("    LastChgComp, LastChgID, LastChgDate, Rm_LastChgDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @IDNo, @RelativeIDNo, @RelativeID, @Name, @NameN, @BirthDate, @Occupation, @IndustryType, @Company,")
            strSQL.AppendLine("    @DeleteMark, @HeaStatus, @GrpStatus, @IdentityID, @GrpKind, @HeaDate, @GrpDate, @IsBSPEmp,")
            strSQL.AppendLine("    @LastChgComp, @LastChgID, @LastChgDate, @Rm_LastChgDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())

            For Each r As Row In FamilyRow
                dbcmd.Parameters.Clear()
                db.AddInParameter(dbcmd, "@IDNo", DbType.String, r.IDNo.Value)
                db.AddInParameter(dbcmd, "@RelativeIDNo", DbType.String, r.RelativeIDNo.Value)
                db.AddInParameter(dbcmd, "@RelativeID", DbType.String, r.RelativeID.Value)
                db.AddInParameter(dbcmd, "@Name", DbType.String, r.Name.Value)
                db.AddInParameter(dbcmd, "@NameN", DbType.String, r.NameN.Value)
                db.AddInParameter(dbcmd, "@BirthDate", DbType.Date, IIf(IsDateTimeNull(r.BirthDate.Value), Convert.ToDateTime("1900/1/1"), r.BirthDate.Value))
                db.AddInParameter(dbcmd, "@Occupation", DbType.String, r.Occupation.Value)
                db.AddInParameter(dbcmd, "@IndustryType", DbType.String, r.IndustryType.Value)
                db.AddInParameter(dbcmd, "@Company", DbType.String, r.Company.Value)
                db.AddInParameter(dbcmd, "@DeleteMark", DbType.String, r.DeleteMark.Value)
                db.AddInParameter(dbcmd, "@HeaStatus", DbType.String, r.HeaStatus.Value)
                db.AddInParameter(dbcmd, "@GrpStatus", DbType.String, r.GrpStatus.Value)
                db.AddInParameter(dbcmd, "@IdentityID", DbType.String, r.IdentityID.Value)
                db.AddInParameter(dbcmd, "@GrpKind", DbType.String, r.GrpKind.Value)
                db.AddInParameter(dbcmd, "@HeaDate", DbType.Date, IIf(IsDateTimeNull(r.HeaDate.Value), Convert.ToDateTime("1900/1/1"), r.HeaDate.Value))
                db.AddInParameter(dbcmd, "@GrpDate", DbType.Date, IIf(IsDateTimeNull(r.GrpDate.Value), Convert.ToDateTime("1900/1/1"), r.GrpDate.Value))
                db.AddInParameter(dbcmd, "@IsBSPEmp", DbType.String, r.IsBSPEmp.Value)
                db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, r.LastChgComp.Value)
                db.AddInParameter(dbcmd, "@LastChgID", DbType.String, r.LastChgID.Value)
                db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(r.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), r.LastChgDate.Value))
                db.AddInParameter(dbcmd, "@Rm_LastChgDate", DbType.Date, IIf(IsDateTimeNull(r.Rm_LastChgDate.Value), Convert.ToDateTime("1900/1/1"), r.Rm_LastChgDate.Value))

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

