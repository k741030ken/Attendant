'****************************************************************
' Table:EmpSenOrgType
' Created Date: 2015.08.18
'****************************************************************/
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports Microsoft.Practices.EnterpriseLibrary.Common
Imports Microsoft.Practices.EnterpriseLibrary.Data
Imports System.Data.Common
Imports System.Data

Namespace beEmpSenOrgType
    Public Class Table
        Private m_Rows As Rows = New Rows()
        Private m_Fields As String() = { "rowid", "Seq", "CompID", "PreCompID", "Reason", "EmpID", "IDNo", "OrgCompID", "OrgType", "OrgTypeName" _
                                    , "OrganID", "OrgName", "ValidDateB", "ValidDateE", "Days", "ConFlag", "CurrentSen", "ConSen", "TotSen", "LastChgComp", "LastChgID" _
                                    , "LastChgDate" }
        Private m_Types As System.Type() = { GetType(Integer), GetType(Integer), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String) _
                                    , GetType(String), GetType(String), GetType(Date), GetType(Date), GetType(Decimal), GetType(String), GetType(Decimal), GetType(Decimal), GetType(Decimal), GetType(String), GetType(String) _
                                    , GetType(Date) }
        Private m_PrimaryFields As String() = { "CompID", "Reason", "EmpID", "OrgType", "ValidDateB" }

        Public ReadOnly Property Rows() As beEmpSenOrgType.Rows 
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
        Public Sub Transfer2Row(EmpSenOrgTypeTable As DataTable)
            For Each dr As DataRow In EmpSenOrgTypeTable.Rows
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

                dr(m_Rows(i).rowid.FieldName) = m_Rows(i).rowid.Value
                dr(m_Rows(i).Seq.FieldName) = m_Rows(i).Seq.Value
                dr(m_Rows(i).CompID.FieldName) = m_Rows(i).CompID.Value
                dr(m_Rows(i).PreCompID.FieldName) = m_Rows(i).PreCompID.Value
                dr(m_Rows(i).Reason.FieldName) = m_Rows(i).Reason.Value
                dr(m_Rows(i).EmpID.FieldName) = m_Rows(i).EmpID.Value
                dr(m_Rows(i).IDNo.FieldName) = m_Rows(i).IDNo.Value
                dr(m_Rows(i).OrgCompID.FieldName) = m_Rows(i).OrgCompID.Value
                dr(m_Rows(i).OrgType.FieldName) = m_Rows(i).OrgType.Value
                dr(m_Rows(i).OrgTypeName.FieldName) = m_Rows(i).OrgTypeName.Value
                dr(m_Rows(i).OrganID.FieldName) = m_Rows(i).OrganID.Value
                dr(m_Rows(i).OrgName.FieldName) = m_Rows(i).OrgName.Value
                dr(m_Rows(i).ValidDateB.FieldName) = m_Rows(i).ValidDateB.Value
                dr(m_Rows(i).ValidDateE.FieldName) = m_Rows(i).ValidDateE.Value
                dr(m_Rows(i).Days.FieldName) = m_Rows(i).Days.Value
                dr(m_Rows(i).ConFlag.FieldName) = m_Rows(i).ConFlag.Value
                dr(m_Rows(i).CurrentSen.FieldName) = m_Rows(i).CurrentSen.Value
                dr(m_Rows(i).ConSen.FieldName) = m_Rows(i).ConSen.Value
                dr(m_Rows(i).TotSen.FieldName) = m_Rows(i).TotSen.Value
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

        Public Sub Add(EmpSenOrgTypeRow As Row)
            m_Rows.Add(EmpSenOrgTypeRow)
        End Sub

        Public Sub Remove(EmpSenOrgTypeRow As Row)
            If m_Rows.IndexOf(EmpSenOrgTypeRow) >= 0 Then
                m_Rows.Remove(EmpSenOrgTypeRow)
            End If
        End Sub

        Public Sub Dispose()
            m_Rows.Clear()
        End Sub

    End Class

    Public Class Row

        Private FI_rowid As Field(Of Integer) = new Field(Of Integer)("rowid", true)
        Private FI_Seq As Field(Of Integer) = new Field(Of Integer)("Seq", true)
        Private FI_CompID As Field(Of String) = new Field(Of String)("CompID", true)
        Private FI_PreCompID As Field(Of String) = new Field(Of String)("PreCompID", true)
        Private FI_Reason As Field(Of String) = new Field(Of String)("Reason", true)
        Private FI_EmpID As Field(Of String) = new Field(Of String)("EmpID", true)
        Private FI_IDNo As Field(Of String) = new Field(Of String)("IDNo", true)
        Private FI_OrgCompID As Field(Of String) = new Field(Of String)("OrgCompID", true)
        Private FI_OrgType As Field(Of String) = new Field(Of String)("OrgType", true)
        Private FI_OrgTypeName As Field(Of String) = new Field(Of String)("OrgTypeName", true)
        Private FI_OrganID As Field(Of String) = new Field(Of String)("OrganID", true)
        Private FI_OrgName As Field(Of String) = new Field(Of String)("OrgName", true)
        Private FI_ValidDateB As Field(Of Date) = new Field(Of Date)("ValidDateB", true)
        Private FI_ValidDateE As Field(Of Date) = new Field(Of Date)("ValidDateE", true)
        Private FI_Days As Field(Of Decimal) = new Field(Of Decimal)("Days", true)
        Private FI_ConFlag As Field(Of String) = new Field(Of String)("ConFlag", true)
        Private FI_CurrentSen As Field(Of Decimal) = new Field(Of Decimal)("CurrentSen", true)
        Private FI_ConSen As Field(Of Decimal) = new Field(Of Decimal)("ConSen", true)
        Private FI_TotSen As Field(Of Decimal) = new Field(Of Decimal)("TotSen", true)
        Private FI_LastChgComp As Field(Of String) = new Field(Of String)("LastChgComp", true)
        Private FI_LastChgID As Field(Of String) = new Field(Of String)("LastChgID", true)
        Private FI_LastChgDate As Field(Of Date) = new Field(Of Date)("LastChgDate", true)
        Private m_FieldNames As String() = { "rowid", "Seq", "CompID", "PreCompID", "Reason", "EmpID", "IDNo", "OrgCompID", "OrgType", "OrgTypeName" _
                                    , "OrganID", "OrgName", "ValidDateB", "ValidDateE", "Days", "ConFlag", "CurrentSen", "ConSen", "TotSen", "LastChgComp", "LastChgID" _
                                    , "LastChgDate" }
        Private m_PrimaryFields As String() = { "CompID", "Reason", "EmpID", "OrgType", "ValidDateB" }
        Private m_IdentityFields As String() = {  }
        Private m_LoadFromDataRow As Boolean = False

        Private Function GetFieldValue(ByVal fieldName As String) As Object
            Select Case fieldName
                Case "rowid"
                    Return FI_rowid.Value
                Case "Seq"
                    Return FI_Seq.Value
                Case "CompID"
                    Return FI_CompID.Value
                Case "PreCompID"
                    Return FI_PreCompID.Value
                Case "Reason"
                    Return FI_Reason.Value
                Case "EmpID"
                    Return FI_EmpID.Value
                Case "IDNo"
                    Return FI_IDNo.Value
                Case "OrgCompID"
                    Return FI_OrgCompID.Value
                Case "OrgType"
                    Return FI_OrgType.Value
                Case "OrgTypeName"
                    Return FI_OrgTypeName.Value
                Case "OrganID"
                    Return FI_OrganID.Value
                Case "OrgName"
                    Return FI_OrgName.Value
                Case "ValidDateB"
                    Return FI_ValidDateB.Value
                Case "ValidDateE"
                    Return FI_ValidDateE.Value
                Case "Days"
                    Return FI_Days.Value
                Case "ConFlag"
                    Return FI_ConFlag.Value
                Case "CurrentSen"
                    Return FI_CurrentSen.Value
                Case "ConSen"
                    Return FI_ConSen.Value
                Case "TotSen"
                    Return FI_TotSen.Value
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
                Case "rowid"
                    FI_rowid.SetValue(value)
                Case "Seq"
                    FI_Seq.SetValue(value)
                Case "CompID"
                    FI_CompID.SetValue(value)
                Case "PreCompID"
                    FI_PreCompID.SetValue(value)
                Case "Reason"
                    FI_Reason.SetValue(value)
                Case "EmpID"
                    FI_EmpID.SetValue(value)
                Case "IDNo"
                    FI_IDNo.SetValue(value)
                Case "OrgCompID"
                    FI_OrgCompID.SetValue(value)
                Case "OrgType"
                    FI_OrgType.SetValue(value)
                Case "OrgTypeName"
                    FI_OrgTypeName.SetValue(value)
                Case "OrganID"
                    FI_OrganID.SetValue(value)
                Case "OrgName"
                    FI_OrgName.SetValue(value)
                Case "ValidDateB"
                    FI_ValidDateB.SetValue(value)
                Case "ValidDateE"
                    FI_ValidDateE.SetValue(value)
                Case "Days"
                    FI_Days.SetValue(value)
                Case "ConFlag"
                    FI_ConFlag.SetValue(value)
                Case "CurrentSen"
                    FI_CurrentSen.SetValue(value)
                Case "ConSen"
                    FI_ConSen.SetValue(value)
                Case "TotSen"
                    FI_TotSen.SetValue(value)
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
                Case "rowid"
                    return FI_rowid.Updated
                Case "Seq"
                    return FI_Seq.Updated
                Case "CompID"
                    return FI_CompID.Updated
                Case "PreCompID"
                    return FI_PreCompID.Updated
                Case "Reason"
                    return FI_Reason.Updated
                Case "EmpID"
                    return FI_EmpID.Updated
                Case "IDNo"
                    return FI_IDNo.Updated
                Case "OrgCompID"
                    return FI_OrgCompID.Updated
                Case "OrgType"
                    return FI_OrgType.Updated
                Case "OrgTypeName"
                    return FI_OrgTypeName.Updated
                Case "OrganID"
                    return FI_OrganID.Updated
                Case "OrgName"
                    return FI_OrgName.Updated
                Case "ValidDateB"
                    return FI_ValidDateB.Updated
                Case "ValidDateE"
                    return FI_ValidDateE.Updated
                Case "Days"
                    return FI_Days.Updated
                Case "ConFlag"
                    return FI_ConFlag.Updated
                Case "CurrentSen"
                    return FI_CurrentSen.Updated
                Case "ConSen"
                    return FI_ConSen.Updated
                Case "TotSen"
                    return FI_TotSen.Updated
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
                Case "rowid"
                    return FI_rowid.CreateUpdateSQL
                Case "Seq"
                    return FI_Seq.CreateUpdateSQL
                Case "CompID"
                    return FI_CompID.CreateUpdateSQL
                Case "PreCompID"
                    return FI_PreCompID.CreateUpdateSQL
                Case "Reason"
                    return FI_Reason.CreateUpdateSQL
                Case "EmpID"
                    return FI_EmpID.CreateUpdateSQL
                Case "IDNo"
                    return FI_IDNo.CreateUpdateSQL
                Case "OrgCompID"
                    return FI_OrgCompID.CreateUpdateSQL
                Case "OrgType"
                    return FI_OrgType.CreateUpdateSQL
                Case "OrgTypeName"
                    return FI_OrgTypeName.CreateUpdateSQL
                Case "OrganID"
                    return FI_OrganID.CreateUpdateSQL
                Case "OrgName"
                    return FI_OrgName.CreateUpdateSQL
                Case "ValidDateB"
                    return FI_ValidDateB.CreateUpdateSQL
                Case "ValidDateE"
                    return FI_ValidDateE.CreateUpdateSQL
                Case "Days"
                    return FI_Days.CreateUpdateSQL
                Case "ConFlag"
                    return FI_ConFlag.CreateUpdateSQL
                Case "CurrentSen"
                    return FI_CurrentSen.CreateUpdateSQL
                Case "ConSen"
                    return FI_ConSen.CreateUpdateSQL
                Case "TotSen"
                    return FI_TotSen.CreateUpdateSQL
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
            FI_rowid.SetInitValue(0)
            FI_Seq.SetInitValue(0)
            FI_CompID.SetInitValue("")
            FI_PreCompID.SetInitValue("")
            FI_Reason.SetInitValue("")
            FI_EmpID.SetInitValue("")
            FI_IDNo.SetInitValue("")
            FI_OrgCompID.SetInitValue("")
            FI_OrgType.SetInitValue("")
            FI_OrgTypeName.SetInitValue("")
            FI_OrganID.SetInitValue("")
            FI_OrgName.SetInitValue("")
            FI_ValidDateB.SetInitValue(Convert.ToDateTime("1900/01/01"))
            FI_ValidDateE.SetInitValue(Convert.ToDateTime("1900/01/01"))
            FI_Days.SetInitValue(0)
            FI_ConFlag.SetInitValue("0")
            FI_CurrentSen.SetInitValue(0)
            FI_ConSen.SetInitValue(0)
            FI_TotSen.SetInitValue(0)
            FI_LastChgComp.SetInitValue("")
            FI_LastChgID.SetInitValue("")
            FI_LastChgDate.SetInitValue(Convert.ToDateTime("1900/01/01"))
        End Sub

        Public Sub New(ByVal dr As System.Data.DataRow)
            FI_rowid.SetInitValue(dr("rowid"))
            FI_Seq.SetInitValue(dr("Seq"))
            FI_CompID.SetInitValue(dr("CompID"))
            FI_PreCompID.SetInitValue(dr("PreCompID"))
            FI_Reason.SetInitValue(dr("Reason"))
            FI_EmpID.SetInitValue(dr("EmpID"))
            FI_IDNo.SetInitValue(dr("IDNo"))
            FI_OrgCompID.SetInitValue(dr("OrgCompID"))
            FI_OrgType.SetInitValue(dr("OrgType"))
            FI_OrgTypeName.SetInitValue(dr("OrgTypeName"))
            FI_OrganID.SetInitValue(dr("OrganID"))
            FI_OrgName.SetInitValue(dr("OrgName"))
            FI_ValidDateB.SetInitValue(dr("ValidDateB"))
            FI_ValidDateE.SetInitValue(dr("ValidDateE"))
            FI_Days.SetInitValue(dr("Days"))
            FI_ConFlag.SetInitValue(dr("ConFlag"))
            FI_CurrentSen.SetInitValue(dr("CurrentSen"))
            FI_ConSen.SetInitValue(dr("ConSen"))
            FI_TotSen.SetInitValue(dr("TotSen"))
            FI_LastChgComp.SetInitValue(dr("LastChgComp"))
            FI_LastChgID.SetInitValue(dr("LastChgID"))
            FI_LastChgDate.SetInitValue(dr("LastChgDate"))

            m_LoadFromDataRow = True
        End Sub

        Private Sub ClearUpdated()
            FI_rowid.Updated = False
            FI_Seq.Updated = False
            FI_CompID.Updated = False
            FI_PreCompID.Updated = False
            FI_Reason.Updated = False
            FI_EmpID.Updated = False
            FI_IDNo.Updated = False
            FI_OrgCompID.Updated = False
            FI_OrgType.Updated = False
            FI_OrgTypeName.Updated = False
            FI_OrganID.Updated = False
            FI_OrgName.Updated = False
            FI_ValidDateB.Updated = False
            FI_ValidDateE.Updated = False
            FI_Days.Updated = False
            FI_ConFlag.Updated = False
            FI_CurrentSen.Updated = False
            FI_ConSen.Updated = False
            FI_TotSen.Updated = False
            FI_LastChgComp.Updated = False
            FI_LastChgID.Updated = False
            FI_LastChgDate.Updated = False
        End Sub

        Public ReadOnly Property rowid As Field(Of Integer) 
            Get
                Return FI_rowid
            End Get
        End Property

        Public ReadOnly Property Seq As Field(Of Integer) 
            Get
                Return FI_Seq
            End Get
        End Property

        Public ReadOnly Property CompID As Field(Of String) 
            Get
                Return FI_CompID
            End Get
        End Property

        Public ReadOnly Property PreCompID As Field(Of String) 
            Get
                Return FI_PreCompID
            End Get
        End Property

        Public ReadOnly Property Reason As Field(Of String) 
            Get
                Return FI_Reason
            End Get
        End Property

        Public ReadOnly Property EmpID As Field(Of String) 
            Get
                Return FI_EmpID
            End Get
        End Property

        Public ReadOnly Property IDNo As Field(Of String) 
            Get
                Return FI_IDNo
            End Get
        End Property

        Public ReadOnly Property OrgCompID As Field(Of String) 
            Get
                Return FI_OrgCompID
            End Get
        End Property

        Public ReadOnly Property OrgType As Field(Of String) 
            Get
                Return FI_OrgType
            End Get
        End Property

        Public ReadOnly Property OrgTypeName As Field(Of String) 
            Get
                Return FI_OrgTypeName
            End Get
        End Property

        Public ReadOnly Property OrganID As Field(Of String) 
            Get
                Return FI_OrganID
            End Get
        End Property

        Public ReadOnly Property OrgName As Field(Of String) 
            Get
                Return FI_OrgName
            End Get
        End Property

        Public ReadOnly Property ValidDateB As Field(Of Date) 
            Get
                Return FI_ValidDateB
            End Get
        End Property

        Public ReadOnly Property ValidDateE As Field(Of Date) 
            Get
                Return FI_ValidDateE
            End Get
        End Property

        Public ReadOnly Property Days As Field(Of Decimal) 
            Get
                Return FI_Days
            End Get
        End Property

        Public ReadOnly Property ConFlag As Field(Of String) 
            Get
                Return FI_ConFlag
            End Get
        End Property

        Public ReadOnly Property CurrentSen As Field(Of Decimal) 
            Get
                Return FI_CurrentSen
            End Get
        End Property

        Public ReadOnly Property ConSen As Field(Of Decimal) 
            Get
                Return FI_ConSen
            End Get
        End Property

        Public ReadOnly Property TotSen As Field(Of Decimal) 
            Get
                Return FI_TotSen
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
        Public Function DeleteRowByPrimaryKey(ByVal EmpSenOrgTypeRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Delete From EmpSenOrgType")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And Reason = @Reason")
            strSQL.AppendLine("And EmpID = @EmpID")
            strSQL.AppendLine("And OrgType = @OrgType")
            strSQL.AppendLine("And ValidDateB = @ValidDateB")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, EmpSenOrgTypeRow.CompID.Value)
            db.AddInParameter(dbcmd, "@Reason", DbType.String, EmpSenOrgTypeRow.Reason.Value)
            db.AddInParameter(dbcmd, "@EmpID", DbType.String, EmpSenOrgTypeRow.EmpID.Value)
            db.AddInParameter(dbcmd, "@OrgType", DbType.String, EmpSenOrgTypeRow.OrgType.Value)
            db.AddInParameter(dbcmd, "@ValidDateB", DbType.Date, EmpSenOrgTypeRow.ValidDateB.Value)

            return db.ExecuteNonQuery(dbcmd)
        End Function

        public Function DeleteRowByPrimaryKey(ByVal EmpSenOrgTypeRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Delete From EmpSenOrgType")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And Reason = @Reason")
            strSQL.AppendLine("And EmpID = @EmpID")
            strSQL.AppendLine("And OrgType = @OrgType")
            strSQL.AppendLine("And ValidDateB = @ValidDateB")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, EmpSenOrgTypeRow.CompID.Value)
            db.AddInParameter(dbcmd, "@Reason", DbType.String, EmpSenOrgTypeRow.Reason.Value)
            db.AddInParameter(dbcmd, "@EmpID", DbType.String, EmpSenOrgTypeRow.EmpID.Value)
            db.AddInParameter(dbcmd, "@OrgType", DbType.String, EmpSenOrgTypeRow.OrgType.Value)
            db.AddInParameter(dbcmd, "@ValidDateB", DbType.Date, EmpSenOrgTypeRow.ValidDateB.Value)

            return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function DeleteRowByPrimaryKey(ByVal EmpSenOrgTypeRow As Row()) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Delete From EmpSenOrgType")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And Reason = @Reason")
            strSQL.AppendLine("And EmpID = @EmpID")
            strSQL.AppendLine("And OrgType = @OrgType")
            strSQL.AppendLine("And ValidDateB = @ValidDateB")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            using cn As DbConnection = db.CreateConnection()
                cn.Open()
                Dim tran As DbTransaction = cn.BeginTransaction()
                Dim inTrans As Boolean = True

                Try
                    For Each r As Row In EmpSenOrgTypeRow
                        dbcmd.Parameters.Clear()
                        db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                        db.AddInParameter(dbcmd, "@Reason", DbType.String, r.Reason.Value)
                        db.AddInParameter(dbcmd, "@EmpID", DbType.String, r.EmpID.Value)
                        db.AddInParameter(dbcmd, "@OrgType", DbType.String, r.OrgType.Value)
                        db.AddInParameter(dbcmd, "@ValidDateB", DbType.Date, r.ValidDateB.Value)

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

        Public Function DeleteRowByPrimaryKey(ByVal EmpSenOrgTypeRow As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Delete From EmpSenOrgType")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And Reason = @Reason")
            strSQL.AppendLine("And EmpID = @EmpID")
            strSQL.AppendLine("And OrgType = @OrgType")
            strSQL.AppendLine("And ValidDateB = @ValidDateB")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())

            For Each r As Row In EmpSenOrgTypeRow
                dbcmd.Parameters.Clear()
                db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                db.AddInParameter(dbcmd, "@Reason", DbType.String, r.Reason.Value)
                db.AddInParameter(dbcmd, "@EmpID", DbType.String, r.EmpID.Value)
                db.AddInParameter(dbcmd, "@OrgType", DbType.String, r.OrgType.Value)
                db.AddInParameter(dbcmd, "@ValidDateB", DbType.Date, r.ValidDateB.Value)

                intRowsAffected += db.ExecuteNonQuery(dbcmd, tran)
            Next
            Return intRowsAffected
        End Function

        Public Function QueryByKey(ByVal EmpSenOrgTypeRow As Row) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From EmpSenOrgType")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And Reason = @Reason")
            strSQL.AppendLine("And EmpID = @EmpID")
            strSQL.AppendLine("And OrgType = @OrgType")
            strSQL.AppendLine("And ValidDateB = @ValidDateB")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, EmpSenOrgTypeRow.CompID.Value)
            db.AddInParameter(dbcmd, "@Reason", DbType.String, EmpSenOrgTypeRow.Reason.Value)
            db.AddInParameter(dbcmd, "@EmpID", DbType.String, EmpSenOrgTypeRow.EmpID.Value)
            db.AddInParameter(dbcmd, "@OrgType", DbType.String, EmpSenOrgTypeRow.OrgType.Value)
            db.AddInParameter(dbcmd, "@ValidDateB", DbType.Date, EmpSenOrgTypeRow.ValidDateB.Value)

            Return db.ExecuteDataSet(dbcmd)
        End Function

        Public Function QueryByKey(EmpSenOrgTypeRow As Row, tran As DbTransaction) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From EmpSenOrgType")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And Reason = @Reason")
            strSQL.AppendLine("And EmpID = @EmpID")
            strSQL.AppendLine("And OrgType = @OrgType")
            strSQL.AppendLine("And ValidDateB = @ValidDateB")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, EmpSenOrgTypeRow.CompID.Value)
            db.AddInParameter(dbcmd, "@Reason", DbType.String, EmpSenOrgTypeRow.Reason.Value)
            db.AddInParameter(dbcmd, "@EmpID", DbType.String, EmpSenOrgTypeRow.EmpID.Value)
            db.AddInParameter(dbcmd, "@OrgType", DbType.String, EmpSenOrgTypeRow.OrgType.Value)
            db.AddInParameter(dbcmd, "@ValidDateB", DbType.Date, EmpSenOrgTypeRow.ValidDateB.Value)

            Return db.ExecuteDataSet(dbcmd, tran)
        End Function

        Public Function Update(ByVal EmpSenOrgTypeRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim strDot As String = String.Empty

            strSQL.AppendLine("Update EmpSenOrgType Set")
            For i As Integer = 0 To EmpSenOrgTypeRow.FieldNames.Length - 1
                If Not EmpSenOrgTypeRow.IsIdentityField(EmpSenOrgTypeRow.FieldNames(i)) AndAlso EmpSenOrgTypeRow.IsUpdated(EmpSenOrgTypeRow.FieldNames(i)) AndAlso EmpSenOrgTypeRow.CreateUpdateSQL(EmpSenOrgTypeRow.FieldNames(i)) Then
                    strSQL.AppendLine(string.Format("{0}{1} = @{1}", strDot, EmpSenOrgTypeRow.FieldNames(i)))
                    strDot = ","
                End If
            Next
            If strDot = String.Empty Then Throw New Exception("未異動資料欄位！")
            strSQL.AppendLine("Where CompID = @PKCompID")
            strSQL.AppendLine("And Reason = @PKReason")
            strSQL.AppendLine("And EmpID = @PKEmpID")
            strSQL.AppendLine("And OrgType = @PKOrgType")
            strSQL.AppendLine("And ValidDateB = @PKValidDateB")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            If EmpSenOrgTypeRow.rowid.Updated Then db.AddInParameter(dbcmd, "@rowid", DbType.Int32, EmpSenOrgTypeRow.rowid.Value)
            If EmpSenOrgTypeRow.Seq.Updated Then db.AddInParameter(dbcmd, "@Seq", DbType.Int32, EmpSenOrgTypeRow.Seq.Value)
            If EmpSenOrgTypeRow.CompID.Updated Then db.AddInParameter(dbcmd, "@CompID", DbType.String, EmpSenOrgTypeRow.CompID.Value)
            If EmpSenOrgTypeRow.PreCompID.Updated Then db.AddInParameter(dbcmd, "@PreCompID", DbType.String, EmpSenOrgTypeRow.PreCompID.Value)
            If EmpSenOrgTypeRow.Reason.Updated Then db.AddInParameter(dbcmd, "@Reason", DbType.String, EmpSenOrgTypeRow.Reason.Value)
            If EmpSenOrgTypeRow.EmpID.Updated Then db.AddInParameter(dbcmd, "@EmpID", DbType.String, EmpSenOrgTypeRow.EmpID.Value)
            If EmpSenOrgTypeRow.IDNo.Updated Then db.AddInParameter(dbcmd, "@IDNo", DbType.String, EmpSenOrgTypeRow.IDNo.Value)
            If EmpSenOrgTypeRow.OrgCompID.Updated Then db.AddInParameter(dbcmd, "@OrgCompID", DbType.String, EmpSenOrgTypeRow.OrgCompID.Value)
            If EmpSenOrgTypeRow.OrgType.Updated Then db.AddInParameter(dbcmd, "@OrgType", DbType.String, EmpSenOrgTypeRow.OrgType.Value)
            If EmpSenOrgTypeRow.OrgTypeName.Updated Then db.AddInParameter(dbcmd, "@OrgTypeName", DbType.String, EmpSenOrgTypeRow.OrgTypeName.Value)
            If EmpSenOrgTypeRow.OrganID.Updated Then db.AddInParameter(dbcmd, "@OrganID", DbType.String, EmpSenOrgTypeRow.OrganID.Value)
            If EmpSenOrgTypeRow.OrgName.Updated Then db.AddInParameter(dbcmd, "@OrgName", DbType.String, EmpSenOrgTypeRow.OrgName.Value)
            If EmpSenOrgTypeRow.ValidDateB.Updated Then db.AddInParameter(dbcmd, "@ValidDateB", DbType.Date, IIf(IsDateTimeNull(EmpSenOrgTypeRow.ValidDateB.Value), Convert.ToDateTime("1900/1/1"), EmpSenOrgTypeRow.ValidDateB.Value))
            If EmpSenOrgTypeRow.ValidDateE.Updated Then db.AddInParameter(dbcmd, "@ValidDateE", DbType.Date, IIf(IsDateTimeNull(EmpSenOrgTypeRow.ValidDateE.Value), Convert.ToDateTime("1900/1/1"), EmpSenOrgTypeRow.ValidDateE.Value))
            If EmpSenOrgTypeRow.Days.Updated Then db.AddInParameter(dbcmd, "@Days", DbType.Decimal, EmpSenOrgTypeRow.Days.Value)
            If EmpSenOrgTypeRow.ConFlag.Updated Then db.AddInParameter(dbcmd, "@ConFlag", DbType.String, EmpSenOrgTypeRow.ConFlag.Value)
            If EmpSenOrgTypeRow.CurrentSen.Updated Then db.AddInParameter(dbcmd, "@CurrentSen", DbType.Decimal, EmpSenOrgTypeRow.CurrentSen.Value)
            If EmpSenOrgTypeRow.ConSen.Updated Then db.AddInParameter(dbcmd, "@ConSen", DbType.Decimal, EmpSenOrgTypeRow.ConSen.Value)
            If EmpSenOrgTypeRow.TotSen.Updated Then db.AddInParameter(dbcmd, "@TotSen", DbType.Decimal, EmpSenOrgTypeRow.TotSen.Value)
            If EmpSenOrgTypeRow.LastChgComp.Updated Then db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, EmpSenOrgTypeRow.LastChgComp.Value)
            If EmpSenOrgTypeRow.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, EmpSenOrgTypeRow.LastChgID.Value)
            If EmpSenOrgTypeRow.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(EmpSenOrgTypeRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), EmpSenOrgTypeRow.LastChgDate.Value))
            db.AddInParameter(dbcmd, "@PKCompID", DbType.String, IIf(EmpSenOrgTypeRow.LoadFromDataRow, EmpSenOrgTypeRow.CompID.OldValue, EmpSenOrgTypeRow.CompID.Value))
            db.AddInParameter(dbcmd, "@PKReason", DbType.String, IIf(EmpSenOrgTypeRow.LoadFromDataRow, EmpSenOrgTypeRow.Reason.OldValue, EmpSenOrgTypeRow.Reason.Value))
            db.AddInParameter(dbcmd, "@PKEmpID", DbType.String, IIf(EmpSenOrgTypeRow.LoadFromDataRow, EmpSenOrgTypeRow.EmpID.OldValue, EmpSenOrgTypeRow.EmpID.Value))
            db.AddInParameter(dbcmd, "@PKOrgType", DbType.String, IIf(EmpSenOrgTypeRow.LoadFromDataRow, EmpSenOrgTypeRow.OrgType.OldValue, EmpSenOrgTypeRow.OrgType.Value))
            db.AddInParameter(dbcmd, "@PKValidDateB", DbType.Date, IIf(EmpSenOrgTypeRow.LoadFromDataRow, EmpSenOrgTypeRow.ValidDateB.OldValue, EmpSenOrgTypeRow.ValidDateB.Value))

            Return db.ExecuteNonQuery(dbcmd)
        End Function

        Public Function Update(ByVal EmpSenOrgTypeRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim strDot As String = String.Empty

            strSQL.AppendLine("Update EmpSenOrgType Set")
            For i As Integer = 0 To EmpSenOrgTypeRow.FieldNames.Length - 1
                If Not EmpSenOrgTypeRow.IsIdentityField(EmpSenOrgTypeRow.FieldNames(i)) AndAlso EmpSenOrgTypeRow.IsUpdated(EmpSenOrgTypeRow.FieldNames(i)) AndAlso EmpSenOrgTypeRow.CreateUpdateSQL(EmpSenOrgTypeRow.FieldNames(i)) Then
                    strSQL.AppendLine(string.Format("{0}{1} = @{1}", strDot, EmpSenOrgTypeRow.FieldNames(i)))
                    strDot = ","
                End If
            Next 
            If strDot = String.Empty Then Throw New Exception("未異動資料欄位！")
            strSQL.AppendLine("Where CompID = @PKCompID")
            strSQL.AppendLine("And Reason = @PKReason")
            strSQL.AppendLine("And EmpID = @PKEmpID")
            strSQL.AppendLine("And OrgType = @PKOrgType")
            strSQL.AppendLine("And ValidDateB = @PKValidDateB")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            If EmpSenOrgTypeRow.rowid.Updated Then db.AddInParameter(dbcmd, "@rowid", DbType.Int32, EmpSenOrgTypeRow.rowid.Value)
            If EmpSenOrgTypeRow.Seq.Updated Then db.AddInParameter(dbcmd, "@Seq", DbType.Int32, EmpSenOrgTypeRow.Seq.Value)
            If EmpSenOrgTypeRow.CompID.Updated Then db.AddInParameter(dbcmd, "@CompID", DbType.String, EmpSenOrgTypeRow.CompID.Value)
            If EmpSenOrgTypeRow.PreCompID.Updated Then db.AddInParameter(dbcmd, "@PreCompID", DbType.String, EmpSenOrgTypeRow.PreCompID.Value)
            If EmpSenOrgTypeRow.Reason.Updated Then db.AddInParameter(dbcmd, "@Reason", DbType.String, EmpSenOrgTypeRow.Reason.Value)
            If EmpSenOrgTypeRow.EmpID.Updated Then db.AddInParameter(dbcmd, "@EmpID", DbType.String, EmpSenOrgTypeRow.EmpID.Value)
            If EmpSenOrgTypeRow.IDNo.Updated Then db.AddInParameter(dbcmd, "@IDNo", DbType.String, EmpSenOrgTypeRow.IDNo.Value)
            If EmpSenOrgTypeRow.OrgCompID.Updated Then db.AddInParameter(dbcmd, "@OrgCompID", DbType.String, EmpSenOrgTypeRow.OrgCompID.Value)
            If EmpSenOrgTypeRow.OrgType.Updated Then db.AddInParameter(dbcmd, "@OrgType", DbType.String, EmpSenOrgTypeRow.OrgType.Value)
            If EmpSenOrgTypeRow.OrgTypeName.Updated Then db.AddInParameter(dbcmd, "@OrgTypeName", DbType.String, EmpSenOrgTypeRow.OrgTypeName.Value)
            If EmpSenOrgTypeRow.OrganID.Updated Then db.AddInParameter(dbcmd, "@OrganID", DbType.String, EmpSenOrgTypeRow.OrganID.Value)
            If EmpSenOrgTypeRow.OrgName.Updated Then db.AddInParameter(dbcmd, "@OrgName", DbType.String, EmpSenOrgTypeRow.OrgName.Value)
            If EmpSenOrgTypeRow.ValidDateB.Updated Then db.AddInParameter(dbcmd, "@ValidDateB", DbType.Date, IIf(IsDateTimeNull(EmpSenOrgTypeRow.ValidDateB.Value), Convert.ToDateTime("1900/1/1"), EmpSenOrgTypeRow.ValidDateB.Value))
            If EmpSenOrgTypeRow.ValidDateE.Updated Then db.AddInParameter(dbcmd, "@ValidDateE", DbType.Date, IIf(IsDateTimeNull(EmpSenOrgTypeRow.ValidDateE.Value), Convert.ToDateTime("1900/1/1"), EmpSenOrgTypeRow.ValidDateE.Value))
            If EmpSenOrgTypeRow.Days.Updated Then db.AddInParameter(dbcmd, "@Days", DbType.Decimal, EmpSenOrgTypeRow.Days.Value)
            If EmpSenOrgTypeRow.ConFlag.Updated Then db.AddInParameter(dbcmd, "@ConFlag", DbType.String, EmpSenOrgTypeRow.ConFlag.Value)
            If EmpSenOrgTypeRow.CurrentSen.Updated Then db.AddInParameter(dbcmd, "@CurrentSen", DbType.Decimal, EmpSenOrgTypeRow.CurrentSen.Value)
            If EmpSenOrgTypeRow.ConSen.Updated Then db.AddInParameter(dbcmd, "@ConSen", DbType.Decimal, EmpSenOrgTypeRow.ConSen.Value)
            If EmpSenOrgTypeRow.TotSen.Updated Then db.AddInParameter(dbcmd, "@TotSen", DbType.Decimal, EmpSenOrgTypeRow.TotSen.Value)
            If EmpSenOrgTypeRow.LastChgComp.Updated Then db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, EmpSenOrgTypeRow.LastChgComp.Value)
            If EmpSenOrgTypeRow.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, EmpSenOrgTypeRow.LastChgID.Value)
            If EmpSenOrgTypeRow.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(EmpSenOrgTypeRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), EmpSenOrgTypeRow.LastChgDate.Value))
            db.AddInParameter(dbcmd, "@PKCompID", DbType.String, IIf(EmpSenOrgTypeRow.LoadFromDataRow, EmpSenOrgTypeRow.CompID.OldValue, EmpSenOrgTypeRow.CompID.Value))
            db.AddInParameter(dbcmd, "@PKReason", DbType.String, IIf(EmpSenOrgTypeRow.LoadFromDataRow, EmpSenOrgTypeRow.Reason.OldValue, EmpSenOrgTypeRow.Reason.Value))
            db.AddInParameter(dbcmd, "@PKEmpID", DbType.String, IIf(EmpSenOrgTypeRow.LoadFromDataRow, EmpSenOrgTypeRow.EmpID.OldValue, EmpSenOrgTypeRow.EmpID.Value))
            db.AddInParameter(dbcmd, "@PKOrgType", DbType.String, IIf(EmpSenOrgTypeRow.LoadFromDataRow, EmpSenOrgTypeRow.OrgType.OldValue, EmpSenOrgTypeRow.OrgType.Value))
            db.AddInParameter(dbcmd, "@PKValidDateB", DbType.Date, IIf(EmpSenOrgTypeRow.LoadFromDataRow, EmpSenOrgTypeRow.ValidDateB.OldValue, EmpSenOrgTypeRow.ValidDateB.Value))

            Return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function Update(ByVal EmpSenOrgTypeRow As Row()) As Integer
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
                    For Each r As Row In EmpSenOrgTypeRow
                        strDot = String.Empty
                        If strSQL.Length > 0 Then strSQL.Remove(0, strSQL.Length)
                        strSQL.AppendLine("Update EmpSenOrgType Set")
                        For i As Integer = 0 To r.FieldNames.Length - 1
                            If Not r.IsIdentityField(r.FieldNames(i)) AndAlso r.IsUpdated(r.FieldNames(i)) AndAlso r.CreateUpdateSQL(r.FieldNames(i)) Then
                                strSQL.AppendLine(String.Format("{0}{1} = @{1}", strDot, r.FieldNames(i)))
                                strDot = ","
                            End If
                        Next
                        If strDot = String.Empty Then Continue For
                        strSQL.AppendLine("Where CompID = @PKCompID")
                        strSQL.AppendLine("And Reason = @PKReason")
                        strSQL.AppendLine("And EmpID = @PKEmpID")
                        strSQL.AppendLine("And OrgType = @PKOrgType")
                        strSQL.AppendLine("And ValidDateB = @PKValidDateB")

                        dbcmd = db.GetSqlStringCommand(strSQL.ToString())
                        If r.rowid.Updated Then db.AddInParameter(dbcmd, "@rowid", DbType.Int32, r.rowid.Value)
                        If r.Seq.Updated Then db.AddInParameter(dbcmd, "@Seq", DbType.Int32, r.Seq.Value)
                        If r.CompID.Updated Then db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                        If r.PreCompID.Updated Then db.AddInParameter(dbcmd, "@PreCompID", DbType.String, r.PreCompID.Value)
                        If r.Reason.Updated Then db.AddInParameter(dbcmd, "@Reason", DbType.String, r.Reason.Value)
                        If r.EmpID.Updated Then db.AddInParameter(dbcmd, "@EmpID", DbType.String, r.EmpID.Value)
                        If r.IDNo.Updated Then db.AddInParameter(dbcmd, "@IDNo", DbType.String, r.IDNo.Value)
                        If r.OrgCompID.Updated Then db.AddInParameter(dbcmd, "@OrgCompID", DbType.String, r.OrgCompID.Value)
                        If r.OrgType.Updated Then db.AddInParameter(dbcmd, "@OrgType", DbType.String, r.OrgType.Value)
                        If r.OrgTypeName.Updated Then db.AddInParameter(dbcmd, "@OrgTypeName", DbType.String, r.OrgTypeName.Value)
                        If r.OrganID.Updated Then db.AddInParameter(dbcmd, "@OrganID", DbType.String, r.OrganID.Value)
                        If r.OrgName.Updated Then db.AddInParameter(dbcmd, "@OrgName", DbType.String, r.OrgName.Value)
                        If r.ValidDateB.Updated Then db.AddInParameter(dbcmd, "@ValidDateB", DbType.Date, IIf(IsDateTimeNull(r.ValidDateB.Value), Convert.ToDateTime("1900/1/1"), r.ValidDateB.Value))
                        If r.ValidDateE.Updated Then db.AddInParameter(dbcmd, "@ValidDateE", DbType.Date, IIf(IsDateTimeNull(r.ValidDateE.Value), Convert.ToDateTime("1900/1/1"), r.ValidDateE.Value))
                        If r.Days.Updated Then db.AddInParameter(dbcmd, "@Days", DbType.Decimal, r.Days.Value)
                        If r.ConFlag.Updated Then db.AddInParameter(dbcmd, "@ConFlag", DbType.String, r.ConFlag.Value)
                        If r.CurrentSen.Updated Then db.AddInParameter(dbcmd, "@CurrentSen", DbType.Decimal, r.CurrentSen.Value)
                        If r.ConSen.Updated Then db.AddInParameter(dbcmd, "@ConSen", DbType.Decimal, r.ConSen.Value)
                        If r.TotSen.Updated Then db.AddInParameter(dbcmd, "@TotSen", DbType.Decimal, r.TotSen.Value)
                        If r.LastChgComp.Updated Then db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, r.LastChgComp.Value)
                        If r.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, r.LastChgID.Value)
                        If r.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(r.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), r.LastChgDate.Value))
                        db.AddInParameter(dbcmd, "@PKCompID", DbType.String, IIf(r.LoadFromDataRow, r.CompID.OldValue, r.CompID.Value))
                        db.AddInParameter(dbcmd, "@PKReason", DbType.String, IIf(r.LoadFromDataRow, r.Reason.OldValue, r.Reason.Value))
                        db.AddInParameter(dbcmd, "@PKEmpID", DbType.String, IIf(r.LoadFromDataRow, r.EmpID.OldValue, r.EmpID.Value))
                        db.AddInParameter(dbcmd, "@PKOrgType", DbType.String, IIf(r.LoadFromDataRow, r.OrgType.OldValue, r.OrgType.Value))
                        db.AddInParameter(dbcmd, "@PKValidDateB", DbType.Date, IIf(r.LoadFromDataRow, r.ValidDateB.OldValue, r.ValidDateB.Value))

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

        Public Function Update(ByVal EmpSenOrgTypeRow As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0
            Dim strDot As String

            For Each r As Row In EmpSenOrgTypeRow
                strDot = String.Empty
                If strSQL.Length > 0 Then strSQL.Remove(0, strSQL.Length)
                strSQL.AppendLine("Update EmpSenOrgType Set")
                For i As Integer = 0 To r.FieldNames.Length - 1
                    If Not r.IsIdentityField(r.FieldNames(i)) AndAlso r.IsUpdated(r.FieldNames(i)) AndAlso r.CreateUpdateSQL(r.FieldNames(i))
                        strSQL.AppendLine(String.Format("{0}{1} = @{1}", strDot, r.FieldNames(i)))
                        strDot = ","
                    End If
                Next
                If strDot = String.Empty Then Continue For
                strSQL.AppendLine("Where CompID = @PKCompID")
                strSQL.AppendLine("And Reason = @PKReason")
                strSQL.AppendLine("And EmpID = @PKEmpID")
                strSQL.AppendLine("And OrgType = @PKOrgType")
                strSQL.AppendLine("And ValidDateB = @PKValidDateB")

                dbcmd = db.GetSqlStringCommand(strSQL.ToString())
                If r.rowid.Updated Then db.AddInParameter(dbcmd, "@rowid", DbType.Int32, r.rowid.Value)
                If r.Seq.Updated Then db.AddInParameter(dbcmd, "@Seq", DbType.Int32, r.Seq.Value)
                If r.CompID.Updated Then db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                If r.PreCompID.Updated Then db.AddInParameter(dbcmd, "@PreCompID", DbType.String, r.PreCompID.Value)
                If r.Reason.Updated Then db.AddInParameter(dbcmd, "@Reason", DbType.String, r.Reason.Value)
                If r.EmpID.Updated Then db.AddInParameter(dbcmd, "@EmpID", DbType.String, r.EmpID.Value)
                If r.IDNo.Updated Then db.AddInParameter(dbcmd, "@IDNo", DbType.String, r.IDNo.Value)
                If r.OrgCompID.Updated Then db.AddInParameter(dbcmd, "@OrgCompID", DbType.String, r.OrgCompID.Value)
                If r.OrgType.Updated Then db.AddInParameter(dbcmd, "@OrgType", DbType.String, r.OrgType.Value)
                If r.OrgTypeName.Updated Then db.AddInParameter(dbcmd, "@OrgTypeName", DbType.String, r.OrgTypeName.Value)
                If r.OrganID.Updated Then db.AddInParameter(dbcmd, "@OrganID", DbType.String, r.OrganID.Value)
                If r.OrgName.Updated Then db.AddInParameter(dbcmd, "@OrgName", DbType.String, r.OrgName.Value)
                If r.ValidDateB.Updated Then db.AddInParameter(dbcmd, "@ValidDateB", DbType.Date, IIf(IsDateTimeNull(r.ValidDateB.Value), Convert.ToDateTime("1900/1/1"), r.ValidDateB.Value))
                If r.ValidDateE.Updated Then db.AddInParameter(dbcmd, "@ValidDateE", DbType.Date, IIf(IsDateTimeNull(r.ValidDateE.Value), Convert.ToDateTime("1900/1/1"), r.ValidDateE.Value))
                If r.Days.Updated Then db.AddInParameter(dbcmd, "@Days", DbType.Decimal, r.Days.Value)
                If r.ConFlag.Updated Then db.AddInParameter(dbcmd, "@ConFlag", DbType.String, r.ConFlag.Value)
                If r.CurrentSen.Updated Then db.AddInParameter(dbcmd, "@CurrentSen", DbType.Decimal, r.CurrentSen.Value)
                If r.ConSen.Updated Then db.AddInParameter(dbcmd, "@ConSen", DbType.Decimal, r.ConSen.Value)
                If r.TotSen.Updated Then db.AddInParameter(dbcmd, "@TotSen", DbType.Decimal, r.TotSen.Value)
                If r.LastChgComp.Updated Then db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, r.LastChgComp.Value)
                If r.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, r.LastChgID.Value)
                If r.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(r.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), r.LastChgDate.Value))
                db.AddInParameter(dbcmd, "@PKCompID", DbType.String, IIf(r.LoadFromDataRow, r.CompID.OldValue, r.CompID.Value))
                db.AddInParameter(dbcmd, "@PKReason", DbType.String, IIf(r.LoadFromDataRow, r.Reason.OldValue, r.Reason.Value))
                db.AddInParameter(dbcmd, "@PKEmpID", DbType.String, IIf(r.LoadFromDataRow, r.EmpID.OldValue, r.EmpID.Value))
                db.AddInParameter(dbcmd, "@PKOrgType", DbType.String, IIf(r.LoadFromDataRow, r.OrgType.OldValue, r.OrgType.Value))
                db.AddInParameter(dbcmd, "@PKValidDateB", DbType.Date, IIf(r.LoadFromDataRow, r.ValidDateB.OldValue, r.ValidDateB.Value))

                intRowsAffected += db.ExecuteNonQuery(dbcmd, tran)
            Next
            Return intRowsAffected
        End Function

        Public Function IsDataExists(ByVal EmpSenOrgTypeRow As Row) As Boolean
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intCount As Integer = 0

            strSQL.AppendLine("Select Count(*) Cnt From EmpSenOrgType")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And Reason = @Reason")
            strSQL.AppendLine("And EmpID = @EmpID")
            strSQL.AppendLine("And OrgType = @OrgType")
            strSQL.AppendLine("And ValidDateB = @ValidDateB")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, EmpSenOrgTypeRow.CompID.Value)
            db.AddInParameter(dbcmd, "@Reason", DbType.String, EmpSenOrgTypeRow.Reason.Value)
            db.AddInParameter(dbcmd, "@EmpID", DbType.String, EmpSenOrgTypeRow.EmpID.Value)
            db.AddInParameter(dbcmd, "@OrgType", DbType.String, EmpSenOrgTypeRow.OrgType.Value)
            db.AddInParameter(dbcmd, "@ValidDateB", DbType.Date, EmpSenOrgTypeRow.ValidDateB.Value)

            intCount = Convert.ToInt32(db.ExecuteScalar(dbcmd))
            Return IIf(intCount > 0, True, False)
        End Function

        Public Function IsDataExists(ByVal EmpSenOrgTypeRow As Row, ByVal tran As DbTransaction) As Boolean
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intCount As Integer = 0

            strSQL.AppendLine("Select Count(*) Cnt From EmpSenOrgType")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And Reason = @Reason")
            strSQL.AppendLine("And EmpID = @EmpID")
            strSQL.AppendLine("And OrgType = @OrgType")
            strSQL.AppendLine("And ValidDateB = @ValidDateB")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, EmpSenOrgTypeRow.CompID.Value)
            db.AddInParameter(dbcmd, "@Reason", DbType.String, EmpSenOrgTypeRow.Reason.Value)
            db.AddInParameter(dbcmd, "@EmpID", DbType.String, EmpSenOrgTypeRow.EmpID.Value)
            db.AddInParameter(dbcmd, "@OrgType", DbType.String, EmpSenOrgTypeRow.OrgType.Value)
            db.AddInParameter(dbcmd, "@ValidDateB", DbType.Date, EmpSenOrgTypeRow.ValidDateB.Value)

            intCount = Convert.ToInt32(db.ExecuteScalar(dbcmd, tran))
            Return IIf(intCount > 0, True, False)
        End Function

        Public Function QuerybyWhere(WhereCondition As String) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From EmpSenOrgType")
            If WhereCondition <> String.Empty Then strSQL.AppendLine(WhereCondition)

            Return db.ExecuteDataSet(CommandType.Text, strSQL.ToString())
        End Function

        Public Function Insert(ByVal EmpSenOrgTypeRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Insert into EmpSenOrgType")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    rowid, Seq, CompID, PreCompID, Reason, EmpID, IDNo, OrgCompID, OrgType, OrgTypeName,")
            strSQL.AppendLine("    OrganID, OrgName, ValidDateB, ValidDateE, Days, ConFlag, CurrentSen, ConSen, TotSen,")
            strSQL.AppendLine("    LastChgComp, LastChgID, LastChgDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @rowid, @Seq, @CompID, @PreCompID, @Reason, @EmpID, @IDNo, @OrgCompID, @OrgType, @OrgTypeName,")
            strSQL.AppendLine("    @OrganID, @OrgName, @ValidDateB, @ValidDateE, @Days, @ConFlag, @CurrentSen, @ConSen, @TotSen,")
            strSQL.AppendLine("    @LastChgComp, @LastChgID, @LastChgDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@rowid", DbType.Int32, EmpSenOrgTypeRow.rowid.Value)
            db.AddInParameter(dbcmd, "@Seq", DbType.Int32, EmpSenOrgTypeRow.Seq.Value)
            db.AddInParameter(dbcmd, "@CompID", DbType.String, EmpSenOrgTypeRow.CompID.Value)
            db.AddInParameter(dbcmd, "@PreCompID", DbType.String, EmpSenOrgTypeRow.PreCompID.Value)
            db.AddInParameter(dbcmd, "@Reason", DbType.String, EmpSenOrgTypeRow.Reason.Value)
            db.AddInParameter(dbcmd, "@EmpID", DbType.String, EmpSenOrgTypeRow.EmpID.Value)
            db.AddInParameter(dbcmd, "@IDNo", DbType.String, EmpSenOrgTypeRow.IDNo.Value)
            db.AddInParameter(dbcmd, "@OrgCompID", DbType.String, EmpSenOrgTypeRow.OrgCompID.Value)
            db.AddInParameter(dbcmd, "@OrgType", DbType.String, EmpSenOrgTypeRow.OrgType.Value)
            db.AddInParameter(dbcmd, "@OrgTypeName", DbType.String, EmpSenOrgTypeRow.OrgTypeName.Value)
            db.AddInParameter(dbcmd, "@OrganID", DbType.String, EmpSenOrgTypeRow.OrganID.Value)
            db.AddInParameter(dbcmd, "@OrgName", DbType.String, EmpSenOrgTypeRow.OrgName.Value)
            db.AddInParameter(dbcmd, "@ValidDateB", DbType.Date, IIf(IsDateTimeNull(EmpSenOrgTypeRow.ValidDateB.Value), Convert.ToDateTime("1900/1/1"), EmpSenOrgTypeRow.ValidDateB.Value))
            db.AddInParameter(dbcmd, "@ValidDateE", DbType.Date, IIf(IsDateTimeNull(EmpSenOrgTypeRow.ValidDateE.Value), Convert.ToDateTime("1900/1/1"), EmpSenOrgTypeRow.ValidDateE.Value))
            db.AddInParameter(dbcmd, "@Days", DbType.Decimal, EmpSenOrgTypeRow.Days.Value)
            db.AddInParameter(dbcmd, "@ConFlag", DbType.String, EmpSenOrgTypeRow.ConFlag.Value)
            db.AddInParameter(dbcmd, "@CurrentSen", DbType.Decimal, EmpSenOrgTypeRow.CurrentSen.Value)
            db.AddInParameter(dbcmd, "@ConSen", DbType.Decimal, EmpSenOrgTypeRow.ConSen.Value)
            db.AddInParameter(dbcmd, "@TotSen", DbType.Decimal, EmpSenOrgTypeRow.TotSen.Value)
            db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, EmpSenOrgTypeRow.LastChgComp.Value)
            db.AddInParameter(dbcmd, "@LastChgID", DbType.String, EmpSenOrgTypeRow.LastChgID.Value)
            db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(EmpSenOrgTypeRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), EmpSenOrgTypeRow.LastChgDate.Value))

            Return db.ExecuteNonQuery(dbcmd)
        End Function

        Public Function Insert(ByVal EmpSenOrgTypeRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Insert into EmpSenOrgType")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    rowid, Seq, CompID, PreCompID, Reason, EmpID, IDNo, OrgCompID, OrgType, OrgTypeName,")
            strSQL.AppendLine("    OrganID, OrgName, ValidDateB, ValidDateE, Days, ConFlag, CurrentSen, ConSen, TotSen,")
            strSQL.AppendLine("    LastChgComp, LastChgID, LastChgDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @rowid, @Seq, @CompID, @PreCompID, @Reason, @EmpID, @IDNo, @OrgCompID, @OrgType, @OrgTypeName,")
            strSQL.AppendLine("    @OrganID, @OrgName, @ValidDateB, @ValidDateE, @Days, @ConFlag, @CurrentSen, @ConSen, @TotSen,")
            strSQL.AppendLine("    @LastChgComp, @LastChgID, @LastChgDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@rowid", DbType.Int32, EmpSenOrgTypeRow.rowid.Value)
            db.AddInParameter(dbcmd, "@Seq", DbType.Int32, EmpSenOrgTypeRow.Seq.Value)
            db.AddInParameter(dbcmd, "@CompID", DbType.String, EmpSenOrgTypeRow.CompID.Value)
            db.AddInParameter(dbcmd, "@PreCompID", DbType.String, EmpSenOrgTypeRow.PreCompID.Value)
            db.AddInParameter(dbcmd, "@Reason", DbType.String, EmpSenOrgTypeRow.Reason.Value)
            db.AddInParameter(dbcmd, "@EmpID", DbType.String, EmpSenOrgTypeRow.EmpID.Value)
            db.AddInParameter(dbcmd, "@IDNo", DbType.String, EmpSenOrgTypeRow.IDNo.Value)
            db.AddInParameter(dbcmd, "@OrgCompID", DbType.String, EmpSenOrgTypeRow.OrgCompID.Value)
            db.AddInParameter(dbcmd, "@OrgType", DbType.String, EmpSenOrgTypeRow.OrgType.Value)
            db.AddInParameter(dbcmd, "@OrgTypeName", DbType.String, EmpSenOrgTypeRow.OrgTypeName.Value)
            db.AddInParameter(dbcmd, "@OrganID", DbType.String, EmpSenOrgTypeRow.OrganID.Value)
            db.AddInParameter(dbcmd, "@OrgName", DbType.String, EmpSenOrgTypeRow.OrgName.Value)
            db.AddInParameter(dbcmd, "@ValidDateB", DbType.Date, IIf(IsDateTimeNull(EmpSenOrgTypeRow.ValidDateB.Value), Convert.ToDateTime("1900/1/1"), EmpSenOrgTypeRow.ValidDateB.Value))
            db.AddInParameter(dbcmd, "@ValidDateE", DbType.Date, IIf(IsDateTimeNull(EmpSenOrgTypeRow.ValidDateE.Value), Convert.ToDateTime("1900/1/1"), EmpSenOrgTypeRow.ValidDateE.Value))
            db.AddInParameter(dbcmd, "@Days", DbType.Decimal, EmpSenOrgTypeRow.Days.Value)
            db.AddInParameter(dbcmd, "@ConFlag", DbType.String, EmpSenOrgTypeRow.ConFlag.Value)
            db.AddInParameter(dbcmd, "@CurrentSen", DbType.Decimal, EmpSenOrgTypeRow.CurrentSen.Value)
            db.AddInParameter(dbcmd, "@ConSen", DbType.Decimal, EmpSenOrgTypeRow.ConSen.Value)
            db.AddInParameter(dbcmd, "@TotSen", DbType.Decimal, EmpSenOrgTypeRow.TotSen.Value)
            db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, EmpSenOrgTypeRow.LastChgComp.Value)
            db.AddInParameter(dbcmd, "@LastChgID", DbType.String, EmpSenOrgTypeRow.LastChgID.Value)
            db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(EmpSenOrgTypeRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), EmpSenOrgTypeRow.LastChgDate.Value))

            Return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function Insert(ByVal EmpSenOrgTypeRow As Row()) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Insert into EmpSenOrgType")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    rowid, Seq, CompID, PreCompID, Reason, EmpID, IDNo, OrgCompID, OrgType, OrgTypeName,")
            strSQL.AppendLine("    OrganID, OrgName, ValidDateB, ValidDateE, Days, ConFlag, CurrentSen, ConSen, TotSen,")
            strSQL.AppendLine("    LastChgComp, LastChgID, LastChgDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @rowid, @Seq, @CompID, @PreCompID, @Reason, @EmpID, @IDNo, @OrgCompID, @OrgType, @OrgTypeName,")
            strSQL.AppendLine("    @OrganID, @OrgName, @ValidDateB, @ValidDateE, @Days, @ConFlag, @CurrentSen, @ConSen, @TotSen,")
            strSQL.AppendLine("    @LastChgComp, @LastChgID, @LastChgDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            Using cn As DbConnection = db.CreateConnection()
                cn.Open()
                Dim tran As DbTransaction = cn.BeginTransaction()
                Dim inTrans As Boolean = True

                Try
                    For Each r As Row In EmpSenOrgTypeRow
                        dbcmd.Parameters.Clear()
                        db.AddInParameter(dbcmd, "@rowid", DbType.Int32, r.rowid.Value)
                        db.AddInParameter(dbcmd, "@Seq", DbType.Int32, r.Seq.Value)
                        db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                        db.AddInParameter(dbcmd, "@PreCompID", DbType.String, r.PreCompID.Value)
                        db.AddInParameter(dbcmd, "@Reason", DbType.String, r.Reason.Value)
                        db.AddInParameter(dbcmd, "@EmpID", DbType.String, r.EmpID.Value)
                        db.AddInParameter(dbcmd, "@IDNo", DbType.String, r.IDNo.Value)
                        db.AddInParameter(dbcmd, "@OrgCompID", DbType.String, r.OrgCompID.Value)
                        db.AddInParameter(dbcmd, "@OrgType", DbType.String, r.OrgType.Value)
                        db.AddInParameter(dbcmd, "@OrgTypeName", DbType.String, r.OrgTypeName.Value)
                        db.AddInParameter(dbcmd, "@OrganID", DbType.String, r.OrganID.Value)
                        db.AddInParameter(dbcmd, "@OrgName", DbType.String, r.OrgName.Value)
                        db.AddInParameter(dbcmd, "@ValidDateB", DbType.Date, IIf(IsDateTimeNull(r.ValidDateB.Value), Convert.ToDateTime("1900/1/1"), r.ValidDateB.Value))
                        db.AddInParameter(dbcmd, "@ValidDateE", DbType.Date, IIf(IsDateTimeNull(r.ValidDateE.Value), Convert.ToDateTime("1900/1/1"), r.ValidDateE.Value))
                        db.AddInParameter(dbcmd, "@Days", DbType.Decimal, r.Days.Value)
                        db.AddInParameter(dbcmd, "@ConFlag", DbType.String, r.ConFlag.Value)
                        db.AddInParameter(dbcmd, "@CurrentSen", DbType.Decimal, r.CurrentSen.Value)
                        db.AddInParameter(dbcmd, "@ConSen", DbType.Decimal, r.ConSen.Value)
                        db.AddInParameter(dbcmd, "@TotSen", DbType.Decimal, r.TotSen.Value)
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

        Public Function Insert(ByVal EmpSenOrgTypeRow As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Insert into EmpSenOrgType")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    rowid, Seq, CompID, PreCompID, Reason, EmpID, IDNo, OrgCompID, OrgType, OrgTypeName,")
            strSQL.AppendLine("    OrganID, OrgName, ValidDateB, ValidDateE, Days, ConFlag, CurrentSen, ConSen, TotSen,")
            strSQL.AppendLine("    LastChgComp, LastChgID, LastChgDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @rowid, @Seq, @CompID, @PreCompID, @Reason, @EmpID, @IDNo, @OrgCompID, @OrgType, @OrgTypeName,")
            strSQL.AppendLine("    @OrganID, @OrgName, @ValidDateB, @ValidDateE, @Days, @ConFlag, @CurrentSen, @ConSen, @TotSen,")
            strSQL.AppendLine("    @LastChgComp, @LastChgID, @LastChgDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())

            For Each r As Row In EmpSenOrgTypeRow
                dbcmd.Parameters.Clear()
                db.AddInParameter(dbcmd, "@rowid", DbType.Int32, r.rowid.Value)
                db.AddInParameter(dbcmd, "@Seq", DbType.Int32, r.Seq.Value)
                db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                db.AddInParameter(dbcmd, "@PreCompID", DbType.String, r.PreCompID.Value)
                db.AddInParameter(dbcmd, "@Reason", DbType.String, r.Reason.Value)
                db.AddInParameter(dbcmd, "@EmpID", DbType.String, r.EmpID.Value)
                db.AddInParameter(dbcmd, "@IDNo", DbType.String, r.IDNo.Value)
                db.AddInParameter(dbcmd, "@OrgCompID", DbType.String, r.OrgCompID.Value)
                db.AddInParameter(dbcmd, "@OrgType", DbType.String, r.OrgType.Value)
                db.AddInParameter(dbcmd, "@OrgTypeName", DbType.String, r.OrgTypeName.Value)
                db.AddInParameter(dbcmd, "@OrganID", DbType.String, r.OrganID.Value)
                db.AddInParameter(dbcmd, "@OrgName", DbType.String, r.OrgName.Value)
                db.AddInParameter(dbcmd, "@ValidDateB", DbType.Date, IIf(IsDateTimeNull(r.ValidDateB.Value), Convert.ToDateTime("1900/1/1"), r.ValidDateB.Value))
                db.AddInParameter(dbcmd, "@ValidDateE", DbType.Date, IIf(IsDateTimeNull(r.ValidDateE.Value), Convert.ToDateTime("1900/1/1"), r.ValidDateE.Value))
                db.AddInParameter(dbcmd, "@Days", DbType.Decimal, r.Days.Value)
                db.AddInParameter(dbcmd, "@ConFlag", DbType.String, r.ConFlag.Value)
                db.AddInParameter(dbcmd, "@CurrentSen", DbType.Decimal, r.CurrentSen.Value)
                db.AddInParameter(dbcmd, "@ConSen", DbType.Decimal, r.ConSen.Value)
                db.AddInParameter(dbcmd, "@TotSen", DbType.Decimal, r.TotSen.Value)
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

