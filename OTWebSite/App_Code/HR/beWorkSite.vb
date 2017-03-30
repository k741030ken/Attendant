'****************************************************************
' Table:WorkSite
' Created Date: 2017.02.23
'****************************************************************/
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports Microsoft.Practices.EnterpriseLibrary.Common
Imports Microsoft.Practices.EnterpriseLibrary.Data
Imports System.Data.Common
Imports System.Data

Namespace beWorkSite
    Public Class Table
        Private m_Rows As Rows = New Rows()
        Private m_Fields As String() = { "CompID", "WorkSiteID", "Remark", "EmpCount", "BranchFlag", "BuildingFlag", "InvoiceNo", "WorkSiteCode", "CountryCode", "AreaCode" _
                                    , "Telephone", "ExtNo", "Address", "DialIn", "DialOut", "LastChgComp", "LastChgID", "LastChgDate", "ExtYards", "CityCode" }
        Private m_Types As System.Type() = { GetType(String), GetType(String), GetType(String), GetType(Integer), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String) _
                                    , GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(Date), GetType(Integer), GetType(String) }
        Private m_PrimaryFields As String() = { "CompID", "WorkSiteID" }

        Public ReadOnly Property Rows() As beWorkSite.Rows 
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
        Public Sub Transfer2Row(WorkSiteTable As DataTable)
            For Each dr As DataRow In WorkSiteTable.Rows
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
                dr(m_Rows(i).WorkSiteID.FieldName) = m_Rows(i).WorkSiteID.Value
                dr(m_Rows(i).Remark.FieldName) = m_Rows(i).Remark.Value
                dr(m_Rows(i).EmpCount.FieldName) = m_Rows(i).EmpCount.Value
                dr(m_Rows(i).BranchFlag.FieldName) = m_Rows(i).BranchFlag.Value
                dr(m_Rows(i).BuildingFlag.FieldName) = m_Rows(i).BuildingFlag.Value
                dr(m_Rows(i).InvoiceNo.FieldName) = m_Rows(i).InvoiceNo.Value
                dr(m_Rows(i).WorkSiteCode.FieldName) = m_Rows(i).WorkSiteCode.Value
                dr(m_Rows(i).CountryCode.FieldName) = m_Rows(i).CountryCode.Value
                dr(m_Rows(i).AreaCode.FieldName) = m_Rows(i).AreaCode.Value
                dr(m_Rows(i).Telephone.FieldName) = m_Rows(i).Telephone.Value
                dr(m_Rows(i).ExtNo.FieldName) = m_Rows(i).ExtNo.Value
                dr(m_Rows(i).Address.FieldName) = m_Rows(i).Address.Value
                dr(m_Rows(i).DialIn.FieldName) = m_Rows(i).DialIn.Value
                dr(m_Rows(i).DialOut.FieldName) = m_Rows(i).DialOut.Value
                dr(m_Rows(i).LastChgComp.FieldName) = m_Rows(i).LastChgComp.Value
                dr(m_Rows(i).LastChgID.FieldName) = m_Rows(i).LastChgID.Value
                dr(m_Rows(i).LastChgDate.FieldName) = m_Rows(i).LastChgDate.Value
                dr(m_Rows(i).ExtYards.FieldName) = m_Rows(i).ExtYards.Value
                dr(m_Rows(i).CityCode.FieldName) = m_Rows(i).CityCode.Value

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

        Public Sub Add(WorkSiteRow As Row)
            m_Rows.Add(WorkSiteRow)
        End Sub

        Public Sub Remove(WorkSiteRow As Row)
            If m_Rows.IndexOf(WorkSiteRow) >= 0 Then
                m_Rows.Remove(WorkSiteRow)
            End If
        End Sub

        Public Sub Dispose()
            m_Rows.Clear()
        End Sub

    End Class

    Public Class Row

        Private FI_CompID As Field(Of String) = new Field(Of String)("CompID", true)
        Private FI_WorkSiteID As Field(Of String) = new Field(Of String)("WorkSiteID", true)
        Private FI_Remark As Field(Of String) = new Field(Of String)("Remark", true)
        Private FI_EmpCount As Field(Of Integer) = new Field(Of Integer)("EmpCount", true)
        Private FI_BranchFlag As Field(Of String) = new Field(Of String)("BranchFlag", true)
        Private FI_BuildingFlag As Field(Of String) = new Field(Of String)("BuildingFlag", true)
        Private FI_InvoiceNo As Field(Of String) = new Field(Of String)("InvoiceNo", true)
        Private FI_WorkSiteCode As Field(Of String) = new Field(Of String)("WorkSiteCode", true)
        Private FI_CountryCode As Field(Of String) = new Field(Of String)("CountryCode", true)
        Private FI_AreaCode As Field(Of String) = new Field(Of String)("AreaCode", true)
        Private FI_Telephone As Field(Of String) = new Field(Of String)("Telephone", true)
        Private FI_ExtNo As Field(Of String) = new Field(Of String)("ExtNo", true)
        Private FI_Address As Field(Of String) = new Field(Of String)("Address", true)
        Private FI_DialIn As Field(Of String) = new Field(Of String)("DialIn", true)
        Private FI_DialOut As Field(Of String) = new Field(Of String)("DialOut", true)
        Private FI_LastChgComp As Field(Of String) = new Field(Of String)("LastChgComp", true)
        Private FI_LastChgID As Field(Of String) = new Field(Of String)("LastChgID", true)
        Private FI_LastChgDate As Field(Of Date) = new Field(Of Date)("LastChgDate", true)
        Private FI_ExtYards As Field(Of Integer) = new Field(Of Integer)("ExtYards", true)
        Private FI_CityCode As Field(Of String) = new Field(Of String)("CityCode", true)
        Private m_FieldNames As String() = { "CompID", "WorkSiteID", "Remark", "EmpCount", "BranchFlag", "BuildingFlag", "InvoiceNo", "WorkSiteCode", "CountryCode", "AreaCode" _
                                    , "Telephone", "ExtNo", "Address", "DialIn", "DialOut", "LastChgComp", "LastChgID", "LastChgDate", "ExtYards", "CityCode" }
        Private m_PrimaryFields As String() = { "CompID", "WorkSiteID" }
        Private m_IdentityFields As String() = {  }
        Private m_LoadFromDataRow As Boolean = False

        Private Function GetFieldValue(ByVal fieldName As String) As Object
            Select Case fieldName
                Case "CompID"
                    Return FI_CompID.Value
                Case "WorkSiteID"
                    Return FI_WorkSiteID.Value
                Case "Remark"
                    Return FI_Remark.Value
                Case "EmpCount"
                    Return FI_EmpCount.Value
                Case "BranchFlag"
                    Return FI_BranchFlag.Value
                Case "BuildingFlag"
                    Return FI_BuildingFlag.Value
                Case "InvoiceNo"
                    Return FI_InvoiceNo.Value
                Case "WorkSiteCode"
                    Return FI_WorkSiteCode.Value
                Case "CountryCode"
                    Return FI_CountryCode.Value
                Case "AreaCode"
                    Return FI_AreaCode.Value
                Case "Telephone"
                    Return FI_Telephone.Value
                Case "ExtNo"
                    Return FI_ExtNo.Value
                Case "Address"
                    Return FI_Address.Value
                Case "DialIn"
                    Return FI_DialIn.Value
                Case "DialOut"
                    Return FI_DialOut.Value
                Case "LastChgComp"
                    Return FI_LastChgComp.Value
                Case "LastChgID"
                    Return FI_LastChgID.Value
                Case "LastChgDate"
                    Return FI_LastChgDate.Value
                Case "ExtYards"
                    Return FI_ExtYards.Value
                Case "CityCode"
                    Return FI_CityCode.Value
                Case Else
                    Return Nothing
            End Select
        End Function

        Private Sub SetFieldValue(ByVal fieldName As String, ByVal value As Object)
            Select Case fieldName
                Case "CompID"
                    FI_CompID.SetValue(value)
                Case "WorkSiteID"
                    FI_WorkSiteID.SetValue(value)
                Case "Remark"
                    FI_Remark.SetValue(value)
                Case "EmpCount"
                    FI_EmpCount.SetValue(value)
                Case "BranchFlag"
                    FI_BranchFlag.SetValue(value)
                Case "BuildingFlag"
                    FI_BuildingFlag.SetValue(value)
                Case "InvoiceNo"
                    FI_InvoiceNo.SetValue(value)
                Case "WorkSiteCode"
                    FI_WorkSiteCode.SetValue(value)
                Case "CountryCode"
                    FI_CountryCode.SetValue(value)
                Case "AreaCode"
                    FI_AreaCode.SetValue(value)
                Case "Telephone"
                    FI_Telephone.SetValue(value)
                Case "ExtNo"
                    FI_ExtNo.SetValue(value)
                Case "Address"
                    FI_Address.SetValue(value)
                Case "DialIn"
                    FI_DialIn.SetValue(value)
                Case "DialOut"
                    FI_DialOut.SetValue(value)
                Case "LastChgComp"
                    FI_LastChgComp.SetValue(value)
                Case "LastChgID"
                    FI_LastChgID.SetValue(value)
                Case "LastChgDate"
                    FI_LastChgDate.SetValue(value)
                Case "ExtYards"
                    FI_ExtYards.SetValue(value)
                Case "CityCode"
                    FI_CityCode.SetValue(value)
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
                Case "WorkSiteID"
                    return FI_WorkSiteID.Updated
                Case "Remark"
                    return FI_Remark.Updated
                Case "EmpCount"
                    return FI_EmpCount.Updated
                Case "BranchFlag"
                    return FI_BranchFlag.Updated
                Case "BuildingFlag"
                    return FI_BuildingFlag.Updated
                Case "InvoiceNo"
                    return FI_InvoiceNo.Updated
                Case "WorkSiteCode"
                    return FI_WorkSiteCode.Updated
                Case "CountryCode"
                    return FI_CountryCode.Updated
                Case "AreaCode"
                    return FI_AreaCode.Updated
                Case "Telephone"
                    return FI_Telephone.Updated
                Case "ExtNo"
                    return FI_ExtNo.Updated
                Case "Address"
                    return FI_Address.Updated
                Case "DialIn"
                    return FI_DialIn.Updated
                Case "DialOut"
                    return FI_DialOut.Updated
                Case "LastChgComp"
                    return FI_LastChgComp.Updated
                Case "LastChgID"
                    return FI_LastChgID.Updated
                Case "LastChgDate"
                    return FI_LastChgDate.Updated
                Case "ExtYards"
                    return FI_ExtYards.Updated
                Case "CityCode"
                    return FI_CityCode.Updated
                Case Else
                    Throw New Exception("無此欄位！")
            End Select
        End Function

        Public Function CreateUpdateSQL(ByVal fieldName As String) As Boolean
            Select Case fieldName
                Case "CompID"
                    return FI_CompID.CreateUpdateSQL
                Case "WorkSiteID"
                    return FI_WorkSiteID.CreateUpdateSQL
                Case "Remark"
                    return FI_Remark.CreateUpdateSQL
                Case "EmpCount"
                    return FI_EmpCount.CreateUpdateSQL
                Case "BranchFlag"
                    return FI_BranchFlag.CreateUpdateSQL
                Case "BuildingFlag"
                    return FI_BuildingFlag.CreateUpdateSQL
                Case "InvoiceNo"
                    return FI_InvoiceNo.CreateUpdateSQL
                Case "WorkSiteCode"
                    return FI_WorkSiteCode.CreateUpdateSQL
                Case "CountryCode"
                    return FI_CountryCode.CreateUpdateSQL
                Case "AreaCode"
                    return FI_AreaCode.CreateUpdateSQL
                Case "Telephone"
                    return FI_Telephone.CreateUpdateSQL
                Case "ExtNo"
                    return FI_ExtNo.CreateUpdateSQL
                Case "Address"
                    return FI_Address.CreateUpdateSQL
                Case "DialIn"
                    return FI_DialIn.CreateUpdateSQL
                Case "DialOut"
                    return FI_DialOut.CreateUpdateSQL
                Case "LastChgComp"
                    return FI_LastChgComp.CreateUpdateSQL
                Case "LastChgID"
                    return FI_LastChgID.CreateUpdateSQL
                Case "LastChgDate"
                    return FI_LastChgDate.CreateUpdateSQL
                Case "ExtYards"
                    return FI_ExtYards.CreateUpdateSQL
                Case "CityCode"
                    return FI_CityCode.CreateUpdateSQL
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
            FI_WorkSiteID.SetInitValue("")
            FI_Remark.SetInitValue("")
            FI_EmpCount.SetInitValue(0)
            FI_BranchFlag.SetInitValue("0")
            FI_BuildingFlag.SetInitValue("0")
            FI_InvoiceNo.SetInitValue("")
            FI_WorkSiteCode.SetInitValue("")
            FI_CountryCode.SetInitValue("")
            FI_AreaCode.SetInitValue("")
            FI_Telephone.SetInitValue("")
            FI_ExtNo.SetInitValue("")
            FI_Address.SetInitValue("")
            FI_DialIn.SetInitValue("")
            FI_DialOut.SetInitValue("")
            FI_LastChgComp.SetInitValue("")
            FI_LastChgID.SetInitValue("")
            FI_LastChgDate.SetInitValue(Convert.ToDateTime("1900/01/01"))
            FI_ExtYards.SetInitValue(4)
            FI_CityCode.SetInitValue("")
        End Sub

        Public Sub New(ByVal dr As System.Data.DataRow)
            FI_CompID.SetInitValue(dr("CompID"))
            FI_WorkSiteID.SetInitValue(dr("WorkSiteID"))
            FI_Remark.SetInitValue(dr("Remark"))
            FI_EmpCount.SetInitValue(dr("EmpCount"))
            FI_BranchFlag.SetInitValue(dr("BranchFlag"))
            FI_BuildingFlag.SetInitValue(dr("BuildingFlag"))
            FI_InvoiceNo.SetInitValue(dr("InvoiceNo"))
            FI_WorkSiteCode.SetInitValue(dr("WorkSiteCode"))
            FI_CountryCode.SetInitValue(dr("CountryCode"))
            FI_AreaCode.SetInitValue(dr("AreaCode"))
            FI_Telephone.SetInitValue(dr("Telephone"))
            FI_ExtNo.SetInitValue(dr("ExtNo"))
            FI_Address.SetInitValue(dr("Address"))
            FI_DialIn.SetInitValue(dr("DialIn"))
            FI_DialOut.SetInitValue(dr("DialOut"))
            FI_LastChgComp.SetInitValue(dr("LastChgComp"))
            FI_LastChgID.SetInitValue(dr("LastChgID"))
            FI_LastChgDate.SetInitValue(dr("LastChgDate"))
            FI_ExtYards.SetInitValue(dr("ExtYards"))
            FI_CityCode.SetInitValue(dr("CityCode"))

            m_LoadFromDataRow = True
        End Sub

        Private Sub ClearUpdated()
            FI_CompID.Updated = False
            FI_WorkSiteID.Updated = False
            FI_Remark.Updated = False
            FI_EmpCount.Updated = False
            FI_BranchFlag.Updated = False
            FI_BuildingFlag.Updated = False
            FI_InvoiceNo.Updated = False
            FI_WorkSiteCode.Updated = False
            FI_CountryCode.Updated = False
            FI_AreaCode.Updated = False
            FI_Telephone.Updated = False
            FI_ExtNo.Updated = False
            FI_Address.Updated = False
            FI_DialIn.Updated = False
            FI_DialOut.Updated = False
            FI_LastChgComp.Updated = False
            FI_LastChgID.Updated = False
            FI_LastChgDate.Updated = False
            FI_ExtYards.Updated = False
            FI_CityCode.Updated = False
        End Sub

        Public ReadOnly Property CompID As Field(Of String) 
            Get
                Return FI_CompID
            End Get
        End Property

        Public ReadOnly Property WorkSiteID As Field(Of String) 
            Get
                Return FI_WorkSiteID
            End Get
        End Property

        Public ReadOnly Property Remark As Field(Of String) 
            Get
                Return FI_Remark
            End Get
        End Property

        Public ReadOnly Property EmpCount As Field(Of Integer) 
            Get
                Return FI_EmpCount
            End Get
        End Property

        Public ReadOnly Property BranchFlag As Field(Of String) 
            Get
                Return FI_BranchFlag
            End Get
        End Property

        Public ReadOnly Property BuildingFlag As Field(Of String) 
            Get
                Return FI_BuildingFlag
            End Get
        End Property

        Public ReadOnly Property InvoiceNo As Field(Of String) 
            Get
                Return FI_InvoiceNo
            End Get
        End Property

        Public ReadOnly Property WorkSiteCode As Field(Of String) 
            Get
                Return FI_WorkSiteCode
            End Get
        End Property

        Public ReadOnly Property CountryCode As Field(Of String) 
            Get
                Return FI_CountryCode
            End Get
        End Property

        Public ReadOnly Property AreaCode As Field(Of String) 
            Get
                Return FI_AreaCode
            End Get
        End Property

        Public ReadOnly Property Telephone As Field(Of String) 
            Get
                Return FI_Telephone
            End Get
        End Property

        Public ReadOnly Property ExtNo As Field(Of String) 
            Get
                Return FI_ExtNo
            End Get
        End Property

        Public ReadOnly Property Address As Field(Of String) 
            Get
                Return FI_Address
            End Get
        End Property

        Public ReadOnly Property DialIn As Field(Of String) 
            Get
                Return FI_DialIn
            End Get
        End Property

        Public ReadOnly Property DialOut As Field(Of String) 
            Get
                Return FI_DialOut
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

        Public ReadOnly Property ExtYards As Field(Of Integer) 
            Get
                Return FI_ExtYards
            End Get
        End Property

        Public ReadOnly Property CityCode As Field(Of String) 
            Get
                Return FI_CityCode
            End Get
        End Property

    End Class

    Public Class Service
        Public Function DeleteRowByPrimaryKey(ByVal WorkSiteRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Delete From WorkSite")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And WorkSiteID = @WorkSiteID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, WorkSiteRow.CompID.Value)
            db.AddInParameter(dbcmd, "@WorkSiteID", DbType.String, WorkSiteRow.WorkSiteID.Value)

            return db.ExecuteNonQuery(dbcmd)
        End Function

        public Function DeleteRowByPrimaryKey(ByVal WorkSiteRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Delete From WorkSite")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And WorkSiteID = @WorkSiteID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, WorkSiteRow.CompID.Value)
            db.AddInParameter(dbcmd, "@WorkSiteID", DbType.String, WorkSiteRow.WorkSiteID.Value)

            return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function DeleteRowByPrimaryKey(ByVal WorkSiteRow As Row()) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Delete From WorkSite")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And WorkSiteID = @WorkSiteID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            using cn As DbConnection = db.CreateConnection()
                cn.Open()
                Dim tran As DbTransaction = cn.BeginTransaction()
                Dim inTrans As Boolean = True

                Try
                    For Each r As Row In WorkSiteRow
                        dbcmd.Parameters.Clear()
                        db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                        db.AddInParameter(dbcmd, "@WorkSiteID", DbType.String, r.WorkSiteID.Value)

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

        Public Function DeleteRowByPrimaryKey(ByVal WorkSiteRow As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Delete From WorkSite")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And WorkSiteID = @WorkSiteID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())

            For Each r As Row In WorkSiteRow
                dbcmd.Parameters.Clear()
                db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                db.AddInParameter(dbcmd, "@WorkSiteID", DbType.String, r.WorkSiteID.Value)

                intRowsAffected += db.ExecuteNonQuery(dbcmd, tran)
            Next
            Return intRowsAffected
        End Function

        Public Function QueryByKey(ByVal WorkSiteRow As Row) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From WorkSite")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And WorkSiteID = @WorkSiteID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, WorkSiteRow.CompID.Value)
            db.AddInParameter(dbcmd, "@WorkSiteID", DbType.String, WorkSiteRow.WorkSiteID.Value)

            Return db.ExecuteDataSet(dbcmd)
        End Function

        Public Function QueryByKey(WorkSiteRow As Row, tran As DbTransaction) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From WorkSite")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And WorkSiteID = @WorkSiteID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, WorkSiteRow.CompID.Value)
            db.AddInParameter(dbcmd, "@WorkSiteID", DbType.String, WorkSiteRow.WorkSiteID.Value)

            Return db.ExecuteDataSet(dbcmd, tran)
        End Function

        Public Function Update(ByVal WorkSiteRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim strDot As String = String.Empty

            strSQL.AppendLine("Update WorkSite Set")
            For i As Integer = 0 To WorkSiteRow.FieldNames.Length - 1
                If Not WorkSiteRow.IsIdentityField(WorkSiteRow.FieldNames(i)) AndAlso WorkSiteRow.IsUpdated(WorkSiteRow.FieldNames(i)) AndAlso WorkSiteRow.CreateUpdateSQL(WorkSiteRow.FieldNames(i)) Then
                    strSQL.AppendLine(string.Format("{0}{1} = @{1}", strDot, WorkSiteRow.FieldNames(i)))
                    strDot = ","
                End If
            Next
            If strDot = String.Empty Then Throw New Exception("未異動資料欄位！")
            strSQL.AppendLine("Where CompID = @PKCompID")
            strSQL.AppendLine("And WorkSiteID = @PKWorkSiteID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            If WorkSiteRow.CompID.Updated Then db.AddInParameter(dbcmd, "@CompID", DbType.String, WorkSiteRow.CompID.Value)
            If WorkSiteRow.WorkSiteID.Updated Then db.AddInParameter(dbcmd, "@WorkSiteID", DbType.String, WorkSiteRow.WorkSiteID.Value)
            If WorkSiteRow.Remark.Updated Then db.AddInParameter(dbcmd, "@Remark", DbType.String, WorkSiteRow.Remark.Value)
            If WorkSiteRow.EmpCount.Updated Then db.AddInParameter(dbcmd, "@EmpCount", DbType.Int32, WorkSiteRow.EmpCount.Value)
            If WorkSiteRow.BranchFlag.Updated Then db.AddInParameter(dbcmd, "@BranchFlag", DbType.String, WorkSiteRow.BranchFlag.Value)
            If WorkSiteRow.BuildingFlag.Updated Then db.AddInParameter(dbcmd, "@BuildingFlag", DbType.String, WorkSiteRow.BuildingFlag.Value)
            If WorkSiteRow.InvoiceNo.Updated Then db.AddInParameter(dbcmd, "@InvoiceNo", DbType.String, WorkSiteRow.InvoiceNo.Value)
            If WorkSiteRow.WorkSiteCode.Updated Then db.AddInParameter(dbcmd, "@WorkSiteCode", DbType.String, WorkSiteRow.WorkSiteCode.Value)
            If WorkSiteRow.CountryCode.Updated Then db.AddInParameter(dbcmd, "@CountryCode", DbType.String, WorkSiteRow.CountryCode.Value)
            If WorkSiteRow.AreaCode.Updated Then db.AddInParameter(dbcmd, "@AreaCode", DbType.String, WorkSiteRow.AreaCode.Value)
            If WorkSiteRow.Telephone.Updated Then db.AddInParameter(dbcmd, "@Telephone", DbType.String, WorkSiteRow.Telephone.Value)
            If WorkSiteRow.ExtNo.Updated Then db.AddInParameter(dbcmd, "@ExtNo", DbType.String, WorkSiteRow.ExtNo.Value)
            If WorkSiteRow.Address.Updated Then db.AddInParameter(dbcmd, "@Address", DbType.String, WorkSiteRow.Address.Value)
            If WorkSiteRow.DialIn.Updated Then db.AddInParameter(dbcmd, "@DialIn", DbType.String, WorkSiteRow.DialIn.Value)
            If WorkSiteRow.DialOut.Updated Then db.AddInParameter(dbcmd, "@DialOut", DbType.String, WorkSiteRow.DialOut.Value)
            If WorkSiteRow.LastChgComp.Updated Then db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, WorkSiteRow.LastChgComp.Value)
            If WorkSiteRow.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, WorkSiteRow.LastChgID.Value)
            If WorkSiteRow.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(WorkSiteRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), WorkSiteRow.LastChgDate.Value))
            If WorkSiteRow.ExtYards.Updated Then db.AddInParameter(dbcmd, "@ExtYards", DbType.Int32, WorkSiteRow.ExtYards.Value)
            If WorkSiteRow.CityCode.Updated Then db.AddInParameter(dbcmd, "@CityCode", DbType.String, WorkSiteRow.CityCode.Value)
            db.AddInParameter(dbcmd, "@PKCompID", DbType.String, IIf(WorkSiteRow.LoadFromDataRow, WorkSiteRow.CompID.OldValue, WorkSiteRow.CompID.Value))
            db.AddInParameter(dbcmd, "@PKWorkSiteID", DbType.String, IIf(WorkSiteRow.LoadFromDataRow, WorkSiteRow.WorkSiteID.OldValue, WorkSiteRow.WorkSiteID.Value))

            Return db.ExecuteNonQuery(dbcmd)
        End Function

        Public Function Update(ByVal WorkSiteRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim strDot As String = String.Empty

            strSQL.AppendLine("Update WorkSite Set")
            For i As Integer = 0 To WorkSiteRow.FieldNames.Length - 1
                If Not WorkSiteRow.IsIdentityField(WorkSiteRow.FieldNames(i)) AndAlso WorkSiteRow.IsUpdated(WorkSiteRow.FieldNames(i)) AndAlso WorkSiteRow.CreateUpdateSQL(WorkSiteRow.FieldNames(i)) Then
                    strSQL.AppendLine(string.Format("{0}{1} = @{1}", strDot, WorkSiteRow.FieldNames(i)))
                    strDot = ","
                End If
            Next 
            If strDot = String.Empty Then Throw New Exception("未異動資料欄位！")
            strSQL.AppendLine("Where CompID = @PKCompID")
            strSQL.AppendLine("And WorkSiteID = @PKWorkSiteID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            If WorkSiteRow.CompID.Updated Then db.AddInParameter(dbcmd, "@CompID", DbType.String, WorkSiteRow.CompID.Value)
            If WorkSiteRow.WorkSiteID.Updated Then db.AddInParameter(dbcmd, "@WorkSiteID", DbType.String, WorkSiteRow.WorkSiteID.Value)
            If WorkSiteRow.Remark.Updated Then db.AddInParameter(dbcmd, "@Remark", DbType.String, WorkSiteRow.Remark.Value)
            If WorkSiteRow.EmpCount.Updated Then db.AddInParameter(dbcmd, "@EmpCount", DbType.Int32, WorkSiteRow.EmpCount.Value)
            If WorkSiteRow.BranchFlag.Updated Then db.AddInParameter(dbcmd, "@BranchFlag", DbType.String, WorkSiteRow.BranchFlag.Value)
            If WorkSiteRow.BuildingFlag.Updated Then db.AddInParameter(dbcmd, "@BuildingFlag", DbType.String, WorkSiteRow.BuildingFlag.Value)
            If WorkSiteRow.InvoiceNo.Updated Then db.AddInParameter(dbcmd, "@InvoiceNo", DbType.String, WorkSiteRow.InvoiceNo.Value)
            If WorkSiteRow.WorkSiteCode.Updated Then db.AddInParameter(dbcmd, "@WorkSiteCode", DbType.String, WorkSiteRow.WorkSiteCode.Value)
            If WorkSiteRow.CountryCode.Updated Then db.AddInParameter(dbcmd, "@CountryCode", DbType.String, WorkSiteRow.CountryCode.Value)
            If WorkSiteRow.AreaCode.Updated Then db.AddInParameter(dbcmd, "@AreaCode", DbType.String, WorkSiteRow.AreaCode.Value)
            If WorkSiteRow.Telephone.Updated Then db.AddInParameter(dbcmd, "@Telephone", DbType.String, WorkSiteRow.Telephone.Value)
            If WorkSiteRow.ExtNo.Updated Then db.AddInParameter(dbcmd, "@ExtNo", DbType.String, WorkSiteRow.ExtNo.Value)
            If WorkSiteRow.Address.Updated Then db.AddInParameter(dbcmd, "@Address", DbType.String, WorkSiteRow.Address.Value)
            If WorkSiteRow.DialIn.Updated Then db.AddInParameter(dbcmd, "@DialIn", DbType.String, WorkSiteRow.DialIn.Value)
            If WorkSiteRow.DialOut.Updated Then db.AddInParameter(dbcmd, "@DialOut", DbType.String, WorkSiteRow.DialOut.Value)
            If WorkSiteRow.LastChgComp.Updated Then db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, WorkSiteRow.LastChgComp.Value)
            If WorkSiteRow.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, WorkSiteRow.LastChgID.Value)
            If WorkSiteRow.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(WorkSiteRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), WorkSiteRow.LastChgDate.Value))
            If WorkSiteRow.ExtYards.Updated Then db.AddInParameter(dbcmd, "@ExtYards", DbType.Int32, WorkSiteRow.ExtYards.Value)
            If WorkSiteRow.CityCode.Updated Then db.AddInParameter(dbcmd, "@CityCode", DbType.String, WorkSiteRow.CityCode.Value)
            db.AddInParameter(dbcmd, "@PKCompID", DbType.String, IIf(WorkSiteRow.LoadFromDataRow, WorkSiteRow.CompID.OldValue, WorkSiteRow.CompID.Value))
            db.AddInParameter(dbcmd, "@PKWorkSiteID", DbType.String, IIf(WorkSiteRow.LoadFromDataRow, WorkSiteRow.WorkSiteID.OldValue, WorkSiteRow.WorkSiteID.Value))

            Return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function Update(ByVal WorkSiteRow As Row()) As Integer
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
                    For Each r As Row In WorkSiteRow
                        strDot = String.Empty
                        If strSQL.Length > 0 Then strSQL.Remove(0, strSQL.Length)
                        strSQL.AppendLine("Update WorkSite Set")
                        For i As Integer = 0 To r.FieldNames.Length - 1
                            If Not r.IsIdentityField(r.FieldNames(i)) AndAlso r.IsUpdated(r.FieldNames(i)) AndAlso r.CreateUpdateSQL(r.FieldNames(i)) Then
                                strSQL.AppendLine(String.Format("{0}{1} = @{1}", strDot, r.FieldNames(i)))
                                strDot = ","
                            End If
                        Next
                        If strDot = String.Empty Then Continue For
                        strSQL.AppendLine("Where CompID = @PKCompID")
                        strSQL.AppendLine("And WorkSiteID = @PKWorkSiteID")

                        dbcmd = db.GetSqlStringCommand(strSQL.ToString())
                        If r.CompID.Updated Then db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                        If r.WorkSiteID.Updated Then db.AddInParameter(dbcmd, "@WorkSiteID", DbType.String, r.WorkSiteID.Value)
                        If r.Remark.Updated Then db.AddInParameter(dbcmd, "@Remark", DbType.String, r.Remark.Value)
                        If r.EmpCount.Updated Then db.AddInParameter(dbcmd, "@EmpCount", DbType.Int32, r.EmpCount.Value)
                        If r.BranchFlag.Updated Then db.AddInParameter(dbcmd, "@BranchFlag", DbType.String, r.BranchFlag.Value)
                        If r.BuildingFlag.Updated Then db.AddInParameter(dbcmd, "@BuildingFlag", DbType.String, r.BuildingFlag.Value)
                        If r.InvoiceNo.Updated Then db.AddInParameter(dbcmd, "@InvoiceNo", DbType.String, r.InvoiceNo.Value)
                        If r.WorkSiteCode.Updated Then db.AddInParameter(dbcmd, "@WorkSiteCode", DbType.String, r.WorkSiteCode.Value)
                        If r.CountryCode.Updated Then db.AddInParameter(dbcmd, "@CountryCode", DbType.String, r.CountryCode.Value)
                        If r.AreaCode.Updated Then db.AddInParameter(dbcmd, "@AreaCode", DbType.String, r.AreaCode.Value)
                        If r.Telephone.Updated Then db.AddInParameter(dbcmd, "@Telephone", DbType.String, r.Telephone.Value)
                        If r.ExtNo.Updated Then db.AddInParameter(dbcmd, "@ExtNo", DbType.String, r.ExtNo.Value)
                        If r.Address.Updated Then db.AddInParameter(dbcmd, "@Address", DbType.String, r.Address.Value)
                        If r.DialIn.Updated Then db.AddInParameter(dbcmd, "@DialIn", DbType.String, r.DialIn.Value)
                        If r.DialOut.Updated Then db.AddInParameter(dbcmd, "@DialOut", DbType.String, r.DialOut.Value)
                        If r.LastChgComp.Updated Then db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, r.LastChgComp.Value)
                        If r.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, r.LastChgID.Value)
                        If r.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(r.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), r.LastChgDate.Value))
                        If r.ExtYards.Updated Then db.AddInParameter(dbcmd, "@ExtYards", DbType.Int32, r.ExtYards.Value)
                        If r.CityCode.Updated Then db.AddInParameter(dbcmd, "@CityCode", DbType.String, r.CityCode.Value)
                        db.AddInParameter(dbcmd, "@PKCompID", DbType.String, IIf(r.LoadFromDataRow, r.CompID.OldValue, r.CompID.Value))
                        db.AddInParameter(dbcmd, "@PKWorkSiteID", DbType.String, IIf(r.LoadFromDataRow, r.WorkSiteID.OldValue, r.WorkSiteID.Value))

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

        Public Function Update(ByVal WorkSiteRow As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0
            Dim strDot As String

            For Each r As Row In WorkSiteRow
                strDot = String.Empty
                If strSQL.Length > 0 Then strSQL.Remove(0, strSQL.Length)
                strSQL.AppendLine("Update WorkSite Set")
                For i As Integer = 0 To r.FieldNames.Length - 1
                    If Not r.IsIdentityField(r.FieldNames(i)) AndAlso r.IsUpdated(r.FieldNames(i)) AndAlso r.CreateUpdateSQL(r.FieldNames(i))
                        strSQL.AppendLine(String.Format("{0}{1} = @{1}", strDot, r.FieldNames(i)))
                        strDot = ","
                    End If
                Next
                If strDot = String.Empty Then Continue For
                strSQL.AppendLine("Where CompID = @PKCompID")
                strSQL.AppendLine("And WorkSiteID = @PKWorkSiteID")

                dbcmd = db.GetSqlStringCommand(strSQL.ToString())
                If r.CompID.Updated Then db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                If r.WorkSiteID.Updated Then db.AddInParameter(dbcmd, "@WorkSiteID", DbType.String, r.WorkSiteID.Value)
                If r.Remark.Updated Then db.AddInParameter(dbcmd, "@Remark", DbType.String, r.Remark.Value)
                If r.EmpCount.Updated Then db.AddInParameter(dbcmd, "@EmpCount", DbType.Int32, r.EmpCount.Value)
                If r.BranchFlag.Updated Then db.AddInParameter(dbcmd, "@BranchFlag", DbType.String, r.BranchFlag.Value)
                If r.BuildingFlag.Updated Then db.AddInParameter(dbcmd, "@BuildingFlag", DbType.String, r.BuildingFlag.Value)
                If r.InvoiceNo.Updated Then db.AddInParameter(dbcmd, "@InvoiceNo", DbType.String, r.InvoiceNo.Value)
                If r.WorkSiteCode.Updated Then db.AddInParameter(dbcmd, "@WorkSiteCode", DbType.String, r.WorkSiteCode.Value)
                If r.CountryCode.Updated Then db.AddInParameter(dbcmd, "@CountryCode", DbType.String, r.CountryCode.Value)
                If r.AreaCode.Updated Then db.AddInParameter(dbcmd, "@AreaCode", DbType.String, r.AreaCode.Value)
                If r.Telephone.Updated Then db.AddInParameter(dbcmd, "@Telephone", DbType.String, r.Telephone.Value)
                If r.ExtNo.Updated Then db.AddInParameter(dbcmd, "@ExtNo", DbType.String, r.ExtNo.Value)
                If r.Address.Updated Then db.AddInParameter(dbcmd, "@Address", DbType.String, r.Address.Value)
                If r.DialIn.Updated Then db.AddInParameter(dbcmd, "@DialIn", DbType.String, r.DialIn.Value)
                If r.DialOut.Updated Then db.AddInParameter(dbcmd, "@DialOut", DbType.String, r.DialOut.Value)
                If r.LastChgComp.Updated Then db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, r.LastChgComp.Value)
                If r.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, r.LastChgID.Value)
                If r.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(r.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), r.LastChgDate.Value))
                If r.ExtYards.Updated Then db.AddInParameter(dbcmd, "@ExtYards", DbType.Int32, r.ExtYards.Value)
                If r.CityCode.Updated Then db.AddInParameter(dbcmd, "@CityCode", DbType.String, r.CityCode.Value)
                db.AddInParameter(dbcmd, "@PKCompID", DbType.String, IIf(r.LoadFromDataRow, r.CompID.OldValue, r.CompID.Value))
                db.AddInParameter(dbcmd, "@PKWorkSiteID", DbType.String, IIf(r.LoadFromDataRow, r.WorkSiteID.OldValue, r.WorkSiteID.Value))

                intRowsAffected += db.ExecuteNonQuery(dbcmd, tran)
            Next
            Return intRowsAffected
        End Function

        Public Function IsDataExists(ByVal WorkSiteRow As Row) As Boolean
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intCount As Integer = 0

            strSQL.AppendLine("Select Count(*) Cnt From WorkSite")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And WorkSiteID = @WorkSiteID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, WorkSiteRow.CompID.Value)
            db.AddInParameter(dbcmd, "@WorkSiteID", DbType.String, WorkSiteRow.WorkSiteID.Value)

            intCount = Convert.ToInt32(db.ExecuteScalar(dbcmd))
            Return IIf(intCount > 0, True, False)
        End Function

        Public Function IsDataExists(ByVal WorkSiteRow As Row, ByVal tran As DbTransaction) As Boolean
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intCount As Integer = 0

            strSQL.AppendLine("Select Count(*) Cnt From WorkSite")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And WorkSiteID = @WorkSiteID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, WorkSiteRow.CompID.Value)
            db.AddInParameter(dbcmd, "@WorkSiteID", DbType.String, WorkSiteRow.WorkSiteID.Value)

            intCount = Convert.ToInt32(db.ExecuteScalar(dbcmd, tran))
            Return IIf(intCount > 0, True, False)
        End Function

        Public Function QuerybyWhere(WhereCondition As String) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From WorkSite")
            If WhereCondition <> String.Empty Then strSQL.AppendLine(WhereCondition)

            Return db.ExecuteDataSet(CommandType.Text, strSQL.ToString())
        End Function

        Public Function Insert(ByVal WorkSiteRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Insert into WorkSite")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    CompID, WorkSiteID, Remark, EmpCount, BranchFlag, BuildingFlag, InvoiceNo, WorkSiteCode,")
            strSQL.AppendLine("    CountryCode, AreaCode, Telephone, ExtNo, Address, DialIn, DialOut, LastChgComp, LastChgID,")
            strSQL.AppendLine("    LastChgDate, ExtYards, CityCode")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @CompID, @WorkSiteID, @Remark, @EmpCount, @BranchFlag, @BuildingFlag, @InvoiceNo, @WorkSiteCode,")
            strSQL.AppendLine("    @CountryCode, @AreaCode, @Telephone, @ExtNo, @Address, @DialIn, @DialOut, @LastChgComp, @LastChgID,")
            strSQL.AppendLine("    @LastChgDate, @ExtYards, @CityCode")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, WorkSiteRow.CompID.Value)
            db.AddInParameter(dbcmd, "@WorkSiteID", DbType.String, WorkSiteRow.WorkSiteID.Value)
            db.AddInParameter(dbcmd, "@Remark", DbType.String, WorkSiteRow.Remark.Value)
            db.AddInParameter(dbcmd, "@EmpCount", DbType.Int32, WorkSiteRow.EmpCount.Value)
            db.AddInParameter(dbcmd, "@BranchFlag", DbType.String, WorkSiteRow.BranchFlag.Value)
            db.AddInParameter(dbcmd, "@BuildingFlag", DbType.String, WorkSiteRow.BuildingFlag.Value)
            db.AddInParameter(dbcmd, "@InvoiceNo", DbType.String, WorkSiteRow.InvoiceNo.Value)
            db.AddInParameter(dbcmd, "@WorkSiteCode", DbType.String, WorkSiteRow.WorkSiteCode.Value)
            db.AddInParameter(dbcmd, "@CountryCode", DbType.String, WorkSiteRow.CountryCode.Value)
            db.AddInParameter(dbcmd, "@AreaCode", DbType.String, WorkSiteRow.AreaCode.Value)
            db.AddInParameter(dbcmd, "@Telephone", DbType.String, WorkSiteRow.Telephone.Value)
            db.AddInParameter(dbcmd, "@ExtNo", DbType.String, WorkSiteRow.ExtNo.Value)
            db.AddInParameter(dbcmd, "@Address", DbType.String, WorkSiteRow.Address.Value)
            db.AddInParameter(dbcmd, "@DialIn", DbType.String, WorkSiteRow.DialIn.Value)
            db.AddInParameter(dbcmd, "@DialOut", DbType.String, WorkSiteRow.DialOut.Value)
            db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, WorkSiteRow.LastChgComp.Value)
            db.AddInParameter(dbcmd, "@LastChgID", DbType.String, WorkSiteRow.LastChgID.Value)
            db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(WorkSiteRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), WorkSiteRow.LastChgDate.Value))
            db.AddInParameter(dbcmd, "@ExtYards", DbType.Int32, WorkSiteRow.ExtYards.Value)
            db.AddInParameter(dbcmd, "@CityCode", DbType.String, WorkSiteRow.CityCode.Value)

            Return db.ExecuteNonQuery(dbcmd)
        End Function

        Public Function Insert(ByVal WorkSiteRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Insert into WorkSite")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    CompID, WorkSiteID, Remark, EmpCount, BranchFlag, BuildingFlag, InvoiceNo, WorkSiteCode,")
            strSQL.AppendLine("    CountryCode, AreaCode, Telephone, ExtNo, Address, DialIn, DialOut, LastChgComp, LastChgID,")
            strSQL.AppendLine("    LastChgDate, ExtYards, CityCode")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @CompID, @WorkSiteID, @Remark, @EmpCount, @BranchFlag, @BuildingFlag, @InvoiceNo, @WorkSiteCode,")
            strSQL.AppendLine("    @CountryCode, @AreaCode, @Telephone, @ExtNo, @Address, @DialIn, @DialOut, @LastChgComp, @LastChgID,")
            strSQL.AppendLine("    @LastChgDate, @ExtYards, @CityCode")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, WorkSiteRow.CompID.Value)
            db.AddInParameter(dbcmd, "@WorkSiteID", DbType.String, WorkSiteRow.WorkSiteID.Value)
            db.AddInParameter(dbcmd, "@Remark", DbType.String, WorkSiteRow.Remark.Value)
            db.AddInParameter(dbcmd, "@EmpCount", DbType.Int32, WorkSiteRow.EmpCount.Value)
            db.AddInParameter(dbcmd, "@BranchFlag", DbType.String, WorkSiteRow.BranchFlag.Value)
            db.AddInParameter(dbcmd, "@BuildingFlag", DbType.String, WorkSiteRow.BuildingFlag.Value)
            db.AddInParameter(dbcmd, "@InvoiceNo", DbType.String, WorkSiteRow.InvoiceNo.Value)
            db.AddInParameter(dbcmd, "@WorkSiteCode", DbType.String, WorkSiteRow.WorkSiteCode.Value)
            db.AddInParameter(dbcmd, "@CountryCode", DbType.String, WorkSiteRow.CountryCode.Value)
            db.AddInParameter(dbcmd, "@AreaCode", DbType.String, WorkSiteRow.AreaCode.Value)
            db.AddInParameter(dbcmd, "@Telephone", DbType.String, WorkSiteRow.Telephone.Value)
            db.AddInParameter(dbcmd, "@ExtNo", DbType.String, WorkSiteRow.ExtNo.Value)
            db.AddInParameter(dbcmd, "@Address", DbType.String, WorkSiteRow.Address.Value)
            db.AddInParameter(dbcmd, "@DialIn", DbType.String, WorkSiteRow.DialIn.Value)
            db.AddInParameter(dbcmd, "@DialOut", DbType.String, WorkSiteRow.DialOut.Value)
            db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, WorkSiteRow.LastChgComp.Value)
            db.AddInParameter(dbcmd, "@LastChgID", DbType.String, WorkSiteRow.LastChgID.Value)
            db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(WorkSiteRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), WorkSiteRow.LastChgDate.Value))
            db.AddInParameter(dbcmd, "@ExtYards", DbType.Int32, WorkSiteRow.ExtYards.Value)
            db.AddInParameter(dbcmd, "@CityCode", DbType.String, WorkSiteRow.CityCode.Value)

            Return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function Insert(ByVal WorkSiteRow As Row()) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Insert into WorkSite")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    CompID, WorkSiteID, Remark, EmpCount, BranchFlag, BuildingFlag, InvoiceNo, WorkSiteCode,")
            strSQL.AppendLine("    CountryCode, AreaCode, Telephone, ExtNo, Address, DialIn, DialOut, LastChgComp, LastChgID,")
            strSQL.AppendLine("    LastChgDate, ExtYards, CityCode")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @CompID, @WorkSiteID, @Remark, @EmpCount, @BranchFlag, @BuildingFlag, @InvoiceNo, @WorkSiteCode,")
            strSQL.AppendLine("    @CountryCode, @AreaCode, @Telephone, @ExtNo, @Address, @DialIn, @DialOut, @LastChgComp, @LastChgID,")
            strSQL.AppendLine("    @LastChgDate, @ExtYards, @CityCode")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            Using cn As DbConnection = db.CreateConnection()
                cn.Open()
                Dim tran As DbTransaction = cn.BeginTransaction()
                Dim inTrans As Boolean = True

                Try
                    For Each r As Row In WorkSiteRow
                        dbcmd.Parameters.Clear()
                        db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                        db.AddInParameter(dbcmd, "@WorkSiteID", DbType.String, r.WorkSiteID.Value)
                        db.AddInParameter(dbcmd, "@Remark", DbType.String, r.Remark.Value)
                        db.AddInParameter(dbcmd, "@EmpCount", DbType.Int32, r.EmpCount.Value)
                        db.AddInParameter(dbcmd, "@BranchFlag", DbType.String, r.BranchFlag.Value)
                        db.AddInParameter(dbcmd, "@BuildingFlag", DbType.String, r.BuildingFlag.Value)
                        db.AddInParameter(dbcmd, "@InvoiceNo", DbType.String, r.InvoiceNo.Value)
                        db.AddInParameter(dbcmd, "@WorkSiteCode", DbType.String, r.WorkSiteCode.Value)
                        db.AddInParameter(dbcmd, "@CountryCode", DbType.String, r.CountryCode.Value)
                        db.AddInParameter(dbcmd, "@AreaCode", DbType.String, r.AreaCode.Value)
                        db.AddInParameter(dbcmd, "@Telephone", DbType.String, r.Telephone.Value)
                        db.AddInParameter(dbcmd, "@ExtNo", DbType.String, r.ExtNo.Value)
                        db.AddInParameter(dbcmd, "@Address", DbType.String, r.Address.Value)
                        db.AddInParameter(dbcmd, "@DialIn", DbType.String, r.DialIn.Value)
                        db.AddInParameter(dbcmd, "@DialOut", DbType.String, r.DialOut.Value)
                        db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, r.LastChgComp.Value)
                        db.AddInParameter(dbcmd, "@LastChgID", DbType.String, r.LastChgID.Value)
                        db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(r.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), r.LastChgDate.Value))
                        db.AddInParameter(dbcmd, "@ExtYards", DbType.Int32, r.ExtYards.Value)
                        db.AddInParameter(dbcmd, "@CityCode", DbType.String, r.CityCode.Value)

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

        Public Function Insert(ByVal WorkSiteRow As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Insert into WorkSite")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    CompID, WorkSiteID, Remark, EmpCount, BranchFlag, BuildingFlag, InvoiceNo, WorkSiteCode,")
            strSQL.AppendLine("    CountryCode, AreaCode, Telephone, ExtNo, Address, DialIn, DialOut, LastChgComp, LastChgID,")
            strSQL.AppendLine("    LastChgDate, ExtYards, CityCode")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @CompID, @WorkSiteID, @Remark, @EmpCount, @BranchFlag, @BuildingFlag, @InvoiceNo, @WorkSiteCode,")
            strSQL.AppendLine("    @CountryCode, @AreaCode, @Telephone, @ExtNo, @Address, @DialIn, @DialOut, @LastChgComp, @LastChgID,")
            strSQL.AppendLine("    @LastChgDate, @ExtYards, @CityCode")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())

            For Each r As Row In WorkSiteRow
                dbcmd.Parameters.Clear()
                db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                db.AddInParameter(dbcmd, "@WorkSiteID", DbType.String, r.WorkSiteID.Value)
                db.AddInParameter(dbcmd, "@Remark", DbType.String, r.Remark.Value)
                db.AddInParameter(dbcmd, "@EmpCount", DbType.Int32, r.EmpCount.Value)
                db.AddInParameter(dbcmd, "@BranchFlag", DbType.String, r.BranchFlag.Value)
                db.AddInParameter(dbcmd, "@BuildingFlag", DbType.String, r.BuildingFlag.Value)
                db.AddInParameter(dbcmd, "@InvoiceNo", DbType.String, r.InvoiceNo.Value)
                db.AddInParameter(dbcmd, "@WorkSiteCode", DbType.String, r.WorkSiteCode.Value)
                db.AddInParameter(dbcmd, "@CountryCode", DbType.String, r.CountryCode.Value)
                db.AddInParameter(dbcmd, "@AreaCode", DbType.String, r.AreaCode.Value)
                db.AddInParameter(dbcmd, "@Telephone", DbType.String, r.Telephone.Value)
                db.AddInParameter(dbcmd, "@ExtNo", DbType.String, r.ExtNo.Value)
                db.AddInParameter(dbcmd, "@Address", DbType.String, r.Address.Value)
                db.AddInParameter(dbcmd, "@DialIn", DbType.String, r.DialIn.Value)
                db.AddInParameter(dbcmd, "@DialOut", DbType.String, r.DialOut.Value)
                db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, r.LastChgComp.Value)
                db.AddInParameter(dbcmd, "@LastChgID", DbType.String, r.LastChgID.Value)
                db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(r.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), r.LastChgDate.Value))
                db.AddInParameter(dbcmd, "@ExtYards", DbType.Int32, r.ExtYards.Value)
                db.AddInParameter(dbcmd, "@CityCode", DbType.String, r.CityCode.Value)

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

