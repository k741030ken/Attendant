'****************************************************************
' Table:EmpSenComp
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

Namespace beEmpSenComp
    Public Class Table
        Private m_Rows As Rows = New Rows()
        Private m_Fields As String() = { "rowid", "Seq", "IDNo", "CompID", "EmpID", "ValidDateB", "ValidDateE", "ValidDateB_Sinopac", "ValidDateE_Sinopac", "Days" _
                                    , "Days_SPHOLD", "ConFlag", "CurrentSen", "ConSen", "TotSen", "TotSen_SPHOLD", "TotSen_SPHOLD1", "TotSen_SPHOLD2", "LastChgComp", "LastChgID", "LastChgDate" }
        Private m_Types As System.Type() = { GetType(Integer), GetType(Integer), GetType(String), GetType(String), GetType(String), GetType(Date), GetType(Date), GetType(Date), GetType(Date), GetType(Decimal) _
                                    , GetType(Decimal), GetType(String), GetType(Decimal), GetType(Decimal), GetType(Decimal), GetType(Decimal), GetType(Decimal), GetType(Decimal), GetType(String), GetType(String), GetType(Date) }
        Private m_PrimaryFields As String() = { "IDNo", "CompID", "EmpID", "ValidDateB" }

        Public ReadOnly Property Rows() As beEmpSenComp.Rows 
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
        Public Sub Transfer2Row(EmpSenCompTable As DataTable)
            For Each dr As DataRow In EmpSenCompTable.Rows
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
                dr(m_Rows(i).IDNo.FieldName) = m_Rows(i).IDNo.Value
                dr(m_Rows(i).CompID.FieldName) = m_Rows(i).CompID.Value
                dr(m_Rows(i).EmpID.FieldName) = m_Rows(i).EmpID.Value
                dr(m_Rows(i).ValidDateB.FieldName) = m_Rows(i).ValidDateB.Value
                dr(m_Rows(i).ValidDateE.FieldName) = m_Rows(i).ValidDateE.Value
                dr(m_Rows(i).ValidDateB_Sinopac.FieldName) = m_Rows(i).ValidDateB_Sinopac.Value
                dr(m_Rows(i).ValidDateE_Sinopac.FieldName) = m_Rows(i).ValidDateE_Sinopac.Value
                dr(m_Rows(i).Days.FieldName) = m_Rows(i).Days.Value
                dr(m_Rows(i).Days_SPHOLD.FieldName) = m_Rows(i).Days_SPHOLD.Value
                dr(m_Rows(i).ConFlag.FieldName) = m_Rows(i).ConFlag.Value
                dr(m_Rows(i).CurrentSen.FieldName) = m_Rows(i).CurrentSen.Value
                dr(m_Rows(i).ConSen.FieldName) = m_Rows(i).ConSen.Value
                dr(m_Rows(i).TotSen.FieldName) = m_Rows(i).TotSen.Value
                dr(m_Rows(i).TotSen_SPHOLD.FieldName) = m_Rows(i).TotSen_SPHOLD.Value
                dr(m_Rows(i).TotSen_SPHOLD1.FieldName) = m_Rows(i).TotSen_SPHOLD1.Value
                dr(m_Rows(i).TotSen_SPHOLD2.FieldName) = m_Rows(i).TotSen_SPHOLD2.Value
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

        Public Sub Add(EmpSenCompRow As Row)
            m_Rows.Add(EmpSenCompRow)
        End Sub

        Public Sub Remove(EmpSenCompRow As Row)
            If m_Rows.IndexOf(EmpSenCompRow) >= 0 Then
                m_Rows.Remove(EmpSenCompRow)
            End If
        End Sub

        Public Sub Dispose()
            m_Rows.Clear()
        End Sub

    End Class

    Public Class Row

        Private FI_rowid As Field(Of Integer) = new Field(Of Integer)("rowid", true)
        Private FI_Seq As Field(Of Integer) = new Field(Of Integer)("Seq", true)
        Private FI_IDNo As Field(Of String) = new Field(Of String)("IDNo", true)
        Private FI_CompID As Field(Of String) = new Field(Of String)("CompID", true)
        Private FI_EmpID As Field(Of String) = new Field(Of String)("EmpID", true)
        Private FI_ValidDateB As Field(Of Date) = new Field(Of Date)("ValidDateB", true)
        Private FI_ValidDateE As Field(Of Date) = new Field(Of Date)("ValidDateE", true)
        Private FI_ValidDateB_Sinopac As Field(Of Date) = new Field(Of Date)("ValidDateB_Sinopac", true)
        Private FI_ValidDateE_Sinopac As Field(Of Date) = new Field(Of Date)("ValidDateE_Sinopac", true)
        Private FI_Days As Field(Of Decimal) = new Field(Of Decimal)("Days", true)
        Private FI_Days_SPHOLD As Field(Of Decimal) = new Field(Of Decimal)("Days_SPHOLD", true)
        Private FI_ConFlag As Field(Of String) = new Field(Of String)("ConFlag", true)
        Private FI_CurrentSen As Field(Of Decimal) = new Field(Of Decimal)("CurrentSen", true)
        Private FI_ConSen As Field(Of Decimal) = new Field(Of Decimal)("ConSen", true)
        Private FI_TotSen As Field(Of Decimal) = new Field(Of Decimal)("TotSen", true)
        Private FI_TotSen_SPHOLD As Field(Of Decimal) = new Field(Of Decimal)("TotSen_SPHOLD", true)
        Private FI_TotSen_SPHOLD1 As Field(Of Decimal) = new Field(Of Decimal)("TotSen_SPHOLD1", true)
        Private FI_TotSen_SPHOLD2 As Field(Of Decimal) = new Field(Of Decimal)("TotSen_SPHOLD2", true)
        Private FI_LastChgComp As Field(Of String) = new Field(Of String)("LastChgComp", true)
        Private FI_LastChgID As Field(Of String) = new Field(Of String)("LastChgID", true)
        Private FI_LastChgDate As Field(Of Date) = new Field(Of Date)("LastChgDate", true)
        Private m_FieldNames As String() = { "rowid", "Seq", "IDNo", "CompID", "EmpID", "ValidDateB", "ValidDateE", "ValidDateB_Sinopac", "ValidDateE_Sinopac", "Days" _
                                    , "Days_SPHOLD", "ConFlag", "CurrentSen", "ConSen", "TotSen", "TotSen_SPHOLD", "TotSen_SPHOLD1", "TotSen_SPHOLD2", "LastChgComp", "LastChgID", "LastChgDate" }
        Private m_PrimaryFields As String() = { "IDNo", "CompID", "EmpID", "ValidDateB" }
        Private m_IdentityFields As String() = {  }
        Private m_LoadFromDataRow As Boolean = False

        Private Function GetFieldValue(ByVal fieldName As String) As Object
            Select Case fieldName
                Case "rowid"
                    Return FI_rowid.Value
                Case "Seq"
                    Return FI_Seq.Value
                Case "IDNo"
                    Return FI_IDNo.Value
                Case "CompID"
                    Return FI_CompID.Value
                Case "EmpID"
                    Return FI_EmpID.Value
                Case "ValidDateB"
                    Return FI_ValidDateB.Value
                Case "ValidDateE"
                    Return FI_ValidDateE.Value
                Case "ValidDateB_Sinopac"
                    Return FI_ValidDateB_Sinopac.Value
                Case "ValidDateE_Sinopac"
                    Return FI_ValidDateE_Sinopac.Value
                Case "Days"
                    Return FI_Days.Value
                Case "Days_SPHOLD"
                    Return FI_Days_SPHOLD.Value
                Case "ConFlag"
                    Return FI_ConFlag.Value
                Case "CurrentSen"
                    Return FI_CurrentSen.Value
                Case "ConSen"
                    Return FI_ConSen.Value
                Case "TotSen"
                    Return FI_TotSen.Value
                Case "TotSen_SPHOLD"
                    Return FI_TotSen_SPHOLD.Value
                Case "TotSen_SPHOLD1"
                    Return FI_TotSen_SPHOLD1.Value
                Case "TotSen_SPHOLD2"
                    Return FI_TotSen_SPHOLD2.Value
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
                Case "IDNo"
                    FI_IDNo.SetValue(value)
                Case "CompID"
                    FI_CompID.SetValue(value)
                Case "EmpID"
                    FI_EmpID.SetValue(value)
                Case "ValidDateB"
                    FI_ValidDateB.SetValue(value)
                Case "ValidDateE"
                    FI_ValidDateE.SetValue(value)
                Case "ValidDateB_Sinopac"
                    FI_ValidDateB_Sinopac.SetValue(value)
                Case "ValidDateE_Sinopac"
                    FI_ValidDateE_Sinopac.SetValue(value)
                Case "Days"
                    FI_Days.SetValue(value)
                Case "Days_SPHOLD"
                    FI_Days_SPHOLD.SetValue(value)
                Case "ConFlag"
                    FI_ConFlag.SetValue(value)
                Case "CurrentSen"
                    FI_CurrentSen.SetValue(value)
                Case "ConSen"
                    FI_ConSen.SetValue(value)
                Case "TotSen"
                    FI_TotSen.SetValue(value)
                Case "TotSen_SPHOLD"
                    FI_TotSen_SPHOLD.SetValue(value)
                Case "TotSen_SPHOLD1"
                    FI_TotSen_SPHOLD1.SetValue(value)
                Case "TotSen_SPHOLD2"
                    FI_TotSen_SPHOLD2.SetValue(value)
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
                Case "IDNo"
                    return FI_IDNo.Updated
                Case "CompID"
                    return FI_CompID.Updated
                Case "EmpID"
                    return FI_EmpID.Updated
                Case "ValidDateB"
                    return FI_ValidDateB.Updated
                Case "ValidDateE"
                    return FI_ValidDateE.Updated
                Case "ValidDateB_Sinopac"
                    return FI_ValidDateB_Sinopac.Updated
                Case "ValidDateE_Sinopac"
                    return FI_ValidDateE_Sinopac.Updated
                Case "Days"
                    return FI_Days.Updated
                Case "Days_SPHOLD"
                    return FI_Days_SPHOLD.Updated
                Case "ConFlag"
                    return FI_ConFlag.Updated
                Case "CurrentSen"
                    return FI_CurrentSen.Updated
                Case "ConSen"
                    return FI_ConSen.Updated
                Case "TotSen"
                    return FI_TotSen.Updated
                Case "TotSen_SPHOLD"
                    return FI_TotSen_SPHOLD.Updated
                Case "TotSen_SPHOLD1"
                    return FI_TotSen_SPHOLD1.Updated
                Case "TotSen_SPHOLD2"
                    return FI_TotSen_SPHOLD2.Updated
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
                Case "IDNo"
                    return FI_IDNo.CreateUpdateSQL
                Case "CompID"
                    return FI_CompID.CreateUpdateSQL
                Case "EmpID"
                    return FI_EmpID.CreateUpdateSQL
                Case "ValidDateB"
                    return FI_ValidDateB.CreateUpdateSQL
                Case "ValidDateE"
                    return FI_ValidDateE.CreateUpdateSQL
                Case "ValidDateB_Sinopac"
                    return FI_ValidDateB_Sinopac.CreateUpdateSQL
                Case "ValidDateE_Sinopac"
                    return FI_ValidDateE_Sinopac.CreateUpdateSQL
                Case "Days"
                    return FI_Days.CreateUpdateSQL
                Case "Days_SPHOLD"
                    return FI_Days_SPHOLD.CreateUpdateSQL
                Case "ConFlag"
                    return FI_ConFlag.CreateUpdateSQL
                Case "CurrentSen"
                    return FI_CurrentSen.CreateUpdateSQL
                Case "ConSen"
                    return FI_ConSen.CreateUpdateSQL
                Case "TotSen"
                    return FI_TotSen.CreateUpdateSQL
                Case "TotSen_SPHOLD"
                    return FI_TotSen_SPHOLD.CreateUpdateSQL
                Case "TotSen_SPHOLD1"
                    return FI_TotSen_SPHOLD1.CreateUpdateSQL
                Case "TotSen_SPHOLD2"
                    return FI_TotSen_SPHOLD2.CreateUpdateSQL
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
            FI_IDNo.SetInitValue("")
            FI_CompID.SetInitValue("")
            FI_EmpID.SetInitValue("")
            FI_ValidDateB.SetInitValue(Convert.ToDateTime("1900/01/01"))
            FI_ValidDateE.SetInitValue(Convert.ToDateTime("1900/01/01"))
            FI_ValidDateB_Sinopac.SetInitValue(Convert.ToDateTime("1900/01/01"))
            FI_ValidDateE_Sinopac.SetInitValue(Convert.ToDateTime("1900/01/01"))
            FI_Days.SetInitValue(0)
            FI_Days_SPHOLD.SetInitValue(0)
            FI_ConFlag.SetInitValue("0")
            FI_CurrentSen.SetInitValue(0)
            FI_ConSen.SetInitValue(0)
            FI_TotSen.SetInitValue(0)
            FI_TotSen_SPHOLD.SetInitValue(0)
            FI_TotSen_SPHOLD1.SetInitValue(0)
            FI_TotSen_SPHOLD2.SetInitValue(0)
            FI_LastChgComp.SetInitValue("")
            FI_LastChgID.SetInitValue("")
            FI_LastChgDate.SetInitValue(Convert.ToDateTime("1900/01/01"))
        End Sub

        Public Sub New(ByVal dr As System.Data.DataRow)
            FI_rowid.SetInitValue(dr("rowid"))
            FI_Seq.SetInitValue(dr("Seq"))
            FI_IDNo.SetInitValue(dr("IDNo"))
            FI_CompID.SetInitValue(dr("CompID"))
            FI_EmpID.SetInitValue(dr("EmpID"))
            FI_ValidDateB.SetInitValue(dr("ValidDateB"))
            FI_ValidDateE.SetInitValue(dr("ValidDateE"))
            FI_ValidDateB_Sinopac.SetInitValue(dr("ValidDateB_Sinopac"))
            FI_ValidDateE_Sinopac.SetInitValue(dr("ValidDateE_Sinopac"))
            FI_Days.SetInitValue(dr("Days"))
            FI_Days_SPHOLD.SetInitValue(dr("Days_SPHOLD"))
            FI_ConFlag.SetInitValue(dr("ConFlag"))
            FI_CurrentSen.SetInitValue(dr("CurrentSen"))
            FI_ConSen.SetInitValue(dr("ConSen"))
            FI_TotSen.SetInitValue(dr("TotSen"))
            FI_TotSen_SPHOLD.SetInitValue(dr("TotSen_SPHOLD"))
            FI_TotSen_SPHOLD1.SetInitValue(dr("TotSen_SPHOLD1"))
            FI_TotSen_SPHOLD2.SetInitValue(dr("TotSen_SPHOLD2"))
            FI_LastChgComp.SetInitValue(dr("LastChgComp"))
            FI_LastChgID.SetInitValue(dr("LastChgID"))
            FI_LastChgDate.SetInitValue(dr("LastChgDate"))

            m_LoadFromDataRow = True
        End Sub

        Private Sub ClearUpdated()
            FI_rowid.Updated = False
            FI_Seq.Updated = False
            FI_IDNo.Updated = False
            FI_CompID.Updated = False
            FI_EmpID.Updated = False
            FI_ValidDateB.Updated = False
            FI_ValidDateE.Updated = False
            FI_ValidDateB_Sinopac.Updated = False
            FI_ValidDateE_Sinopac.Updated = False
            FI_Days.Updated = False
            FI_Days_SPHOLD.Updated = False
            FI_ConFlag.Updated = False
            FI_CurrentSen.Updated = False
            FI_ConSen.Updated = False
            FI_TotSen.Updated = False
            FI_TotSen_SPHOLD.Updated = False
            FI_TotSen_SPHOLD1.Updated = False
            FI_TotSen_SPHOLD2.Updated = False
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

        Public ReadOnly Property IDNo As Field(Of String) 
            Get
                Return FI_IDNo
            End Get
        End Property

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

        Public ReadOnly Property ValidDateB_Sinopac As Field(Of Date) 
            Get
                Return FI_ValidDateB_Sinopac
            End Get
        End Property

        Public ReadOnly Property ValidDateE_Sinopac As Field(Of Date) 
            Get
                Return FI_ValidDateE_Sinopac
            End Get
        End Property

        Public ReadOnly Property Days As Field(Of Decimal) 
            Get
                Return FI_Days
            End Get
        End Property

        Public ReadOnly Property Days_SPHOLD As Field(Of Decimal) 
            Get
                Return FI_Days_SPHOLD
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

        Public ReadOnly Property TotSen_SPHOLD As Field(Of Decimal) 
            Get
                Return FI_TotSen_SPHOLD
            End Get
        End Property

        Public ReadOnly Property TotSen_SPHOLD1 As Field(Of Decimal) 
            Get
                Return FI_TotSen_SPHOLD1
            End Get
        End Property

        Public ReadOnly Property TotSen_SPHOLD2 As Field(Of Decimal) 
            Get
                Return FI_TotSen_SPHOLD2
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
        Public Function DeleteRowByPrimaryKey(ByVal EmpSenCompRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Delete From EmpSenComp")
            strSQL.AppendLine("Where IDNo = @IDNo")
            strSQL.AppendLine("And CompID = @CompID")
            strSQL.AppendLine("And EmpID = @EmpID")
            strSQL.AppendLine("And ValidDateB = @ValidDateB")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@IDNo", DbType.String, EmpSenCompRow.IDNo.Value)
            db.AddInParameter(dbcmd, "@CompID", DbType.String, EmpSenCompRow.CompID.Value)
            db.AddInParameter(dbcmd, "@EmpID", DbType.String, EmpSenCompRow.EmpID.Value)
            db.AddInParameter(dbcmd, "@ValidDateB", DbType.Date, EmpSenCompRow.ValidDateB.Value)

            return db.ExecuteNonQuery(dbcmd)
        End Function

        public Function DeleteRowByPrimaryKey(ByVal EmpSenCompRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Delete From EmpSenComp")
            strSQL.AppendLine("Where IDNo = @IDNo")
            strSQL.AppendLine("And CompID = @CompID")
            strSQL.AppendLine("And EmpID = @EmpID")
            strSQL.AppendLine("And ValidDateB = @ValidDateB")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@IDNo", DbType.String, EmpSenCompRow.IDNo.Value)
            db.AddInParameter(dbcmd, "@CompID", DbType.String, EmpSenCompRow.CompID.Value)
            db.AddInParameter(dbcmd, "@EmpID", DbType.String, EmpSenCompRow.EmpID.Value)
            db.AddInParameter(dbcmd, "@ValidDateB", DbType.Date, EmpSenCompRow.ValidDateB.Value)

            return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function DeleteRowByPrimaryKey(ByVal EmpSenCompRow As Row()) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Delete From EmpSenComp")
            strSQL.AppendLine("Where IDNo = @IDNo")
            strSQL.AppendLine("And CompID = @CompID")
            strSQL.AppendLine("And EmpID = @EmpID")
            strSQL.AppendLine("And ValidDateB = @ValidDateB")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            using cn As DbConnection = db.CreateConnection()
                cn.Open()
                Dim tran As DbTransaction = cn.BeginTransaction()
                Dim inTrans As Boolean = True

                Try
                    For Each r As Row In EmpSenCompRow
                        dbcmd.Parameters.Clear()
                        db.AddInParameter(dbcmd, "@IDNo", DbType.String, r.IDNo.Value)
                        db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                        db.AddInParameter(dbcmd, "@EmpID", DbType.String, r.EmpID.Value)
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

        Public Function DeleteRowByPrimaryKey(ByVal EmpSenCompRow As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Delete From EmpSenComp")
            strSQL.AppendLine("Where IDNo = @IDNo")
            strSQL.AppendLine("And CompID = @CompID")
            strSQL.AppendLine("And EmpID = @EmpID")
            strSQL.AppendLine("And ValidDateB = @ValidDateB")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())

            For Each r As Row In EmpSenCompRow
                dbcmd.Parameters.Clear()
                db.AddInParameter(dbcmd, "@IDNo", DbType.String, r.IDNo.Value)
                db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                db.AddInParameter(dbcmd, "@EmpID", DbType.String, r.EmpID.Value)
                db.AddInParameter(dbcmd, "@ValidDateB", DbType.Date, r.ValidDateB.Value)

                intRowsAffected += db.ExecuteNonQuery(dbcmd, tran)
            Next
            Return intRowsAffected
        End Function

        Public Function QueryByKey(ByVal EmpSenCompRow As Row) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From EmpSenComp")
            strSQL.AppendLine("Where IDNo = @IDNo")
            strSQL.AppendLine("And CompID = @CompID")
            strSQL.AppendLine("And EmpID = @EmpID")
            strSQL.AppendLine("And ValidDateB = @ValidDateB")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@IDNo", DbType.String, EmpSenCompRow.IDNo.Value)
            db.AddInParameter(dbcmd, "@CompID", DbType.String, EmpSenCompRow.CompID.Value)
            db.AddInParameter(dbcmd, "@EmpID", DbType.String, EmpSenCompRow.EmpID.Value)
            db.AddInParameter(dbcmd, "@ValidDateB", DbType.Date, EmpSenCompRow.ValidDateB.Value)

            Return db.ExecuteDataSet(dbcmd)
        End Function

        Public Function QueryByKey(EmpSenCompRow As Row, tran As DbTransaction) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From EmpSenComp")
            strSQL.AppendLine("Where IDNo = @IDNo")
            strSQL.AppendLine("And CompID = @CompID")
            strSQL.AppendLine("And EmpID = @EmpID")
            strSQL.AppendLine("And ValidDateB = @ValidDateB")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@IDNo", DbType.String, EmpSenCompRow.IDNo.Value)
            db.AddInParameter(dbcmd, "@CompID", DbType.String, EmpSenCompRow.CompID.Value)
            db.AddInParameter(dbcmd, "@EmpID", DbType.String, EmpSenCompRow.EmpID.Value)
            db.AddInParameter(dbcmd, "@ValidDateB", DbType.Date, EmpSenCompRow.ValidDateB.Value)

            Return db.ExecuteDataSet(dbcmd, tran)
        End Function

        Public Function Update(ByVal EmpSenCompRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim strDot As String = String.Empty

            strSQL.AppendLine("Update EmpSenComp Set")
            For i As Integer = 0 To EmpSenCompRow.FieldNames.Length - 1
                If Not EmpSenCompRow.IsIdentityField(EmpSenCompRow.FieldNames(i)) AndAlso EmpSenCompRow.IsUpdated(EmpSenCompRow.FieldNames(i)) AndAlso EmpSenCompRow.CreateUpdateSQL(EmpSenCompRow.FieldNames(i)) Then
                    strSQL.AppendLine(string.Format("{0}{1} = @{1}", strDot, EmpSenCompRow.FieldNames(i)))
                    strDot = ","
                End If
            Next
            If strDot = String.Empty Then Throw New Exception("未異動資料欄位！")
            strSQL.AppendLine("Where IDNo = @PKIDNo")
            strSQL.AppendLine("And CompID = @PKCompID")
            strSQL.AppendLine("And EmpID = @PKEmpID")
            strSQL.AppendLine("And ValidDateB = @PKValidDateB")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            If EmpSenCompRow.rowid.Updated Then db.AddInParameter(dbcmd, "@rowid", DbType.Int32, EmpSenCompRow.rowid.Value)
            If EmpSenCompRow.Seq.Updated Then db.AddInParameter(dbcmd, "@Seq", DbType.Int32, EmpSenCompRow.Seq.Value)
            If EmpSenCompRow.IDNo.Updated Then db.AddInParameter(dbcmd, "@IDNo", DbType.String, EmpSenCompRow.IDNo.Value)
            If EmpSenCompRow.CompID.Updated Then db.AddInParameter(dbcmd, "@CompID", DbType.String, EmpSenCompRow.CompID.Value)
            If EmpSenCompRow.EmpID.Updated Then db.AddInParameter(dbcmd, "@EmpID", DbType.String, EmpSenCompRow.EmpID.Value)
            If EmpSenCompRow.ValidDateB.Updated Then db.AddInParameter(dbcmd, "@ValidDateB", DbType.Date, IIf(IsDateTimeNull(EmpSenCompRow.ValidDateB.Value), Convert.ToDateTime("1900/1/1"), EmpSenCompRow.ValidDateB.Value))
            If EmpSenCompRow.ValidDateE.Updated Then db.AddInParameter(dbcmd, "@ValidDateE", DbType.Date, IIf(IsDateTimeNull(EmpSenCompRow.ValidDateE.Value), Convert.ToDateTime("1900/1/1"), EmpSenCompRow.ValidDateE.Value))
            If EmpSenCompRow.ValidDateB_Sinopac.Updated Then db.AddInParameter(dbcmd, "@ValidDateB_Sinopac", DbType.Date, IIf(IsDateTimeNull(EmpSenCompRow.ValidDateB_Sinopac.Value), Convert.ToDateTime("1900/1/1"), EmpSenCompRow.ValidDateB_Sinopac.Value))
            If EmpSenCompRow.ValidDateE_Sinopac.Updated Then db.AddInParameter(dbcmd, "@ValidDateE_Sinopac", DbType.Date, IIf(IsDateTimeNull(EmpSenCompRow.ValidDateE_Sinopac.Value), Convert.ToDateTime("1900/1/1"), EmpSenCompRow.ValidDateE_Sinopac.Value))
            If EmpSenCompRow.Days.Updated Then db.AddInParameter(dbcmd, "@Days", DbType.Decimal, EmpSenCompRow.Days.Value)
            If EmpSenCompRow.Days_SPHOLD.Updated Then db.AddInParameter(dbcmd, "@Days_SPHOLD", DbType.Decimal, EmpSenCompRow.Days_SPHOLD.Value)
            If EmpSenCompRow.ConFlag.Updated Then db.AddInParameter(dbcmd, "@ConFlag", DbType.String, EmpSenCompRow.ConFlag.Value)
            If EmpSenCompRow.CurrentSen.Updated Then db.AddInParameter(dbcmd, "@CurrentSen", DbType.Decimal, EmpSenCompRow.CurrentSen.Value)
            If EmpSenCompRow.ConSen.Updated Then db.AddInParameter(dbcmd, "@ConSen", DbType.Decimal, EmpSenCompRow.ConSen.Value)
            If EmpSenCompRow.TotSen.Updated Then db.AddInParameter(dbcmd, "@TotSen", DbType.Decimal, EmpSenCompRow.TotSen.Value)
            If EmpSenCompRow.TotSen_SPHOLD.Updated Then db.AddInParameter(dbcmd, "@TotSen_SPHOLD", DbType.Decimal, EmpSenCompRow.TotSen_SPHOLD.Value)
            If EmpSenCompRow.TotSen_SPHOLD1.Updated Then db.AddInParameter(dbcmd, "@TotSen_SPHOLD1", DbType.Decimal, EmpSenCompRow.TotSen_SPHOLD1.Value)
            If EmpSenCompRow.TotSen_SPHOLD2.Updated Then db.AddInParameter(dbcmd, "@TotSen_SPHOLD2", DbType.Decimal, EmpSenCompRow.TotSen_SPHOLD2.Value)
            If EmpSenCompRow.LastChgComp.Updated Then db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, EmpSenCompRow.LastChgComp.Value)
            If EmpSenCompRow.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, EmpSenCompRow.LastChgID.Value)
            If EmpSenCompRow.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(EmpSenCompRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), EmpSenCompRow.LastChgDate.Value))
            db.AddInParameter(dbcmd, "@PKIDNo", DbType.String, IIf(EmpSenCompRow.LoadFromDataRow, EmpSenCompRow.IDNo.OldValue, EmpSenCompRow.IDNo.Value))
            db.AddInParameter(dbcmd, "@PKCompID", DbType.String, IIf(EmpSenCompRow.LoadFromDataRow, EmpSenCompRow.CompID.OldValue, EmpSenCompRow.CompID.Value))
            db.AddInParameter(dbcmd, "@PKEmpID", DbType.String, IIf(EmpSenCompRow.LoadFromDataRow, EmpSenCompRow.EmpID.OldValue, EmpSenCompRow.EmpID.Value))
            db.AddInParameter(dbcmd, "@PKValidDateB", DbType.Date, IIf(EmpSenCompRow.LoadFromDataRow, EmpSenCompRow.ValidDateB.OldValue, EmpSenCompRow.ValidDateB.Value))

            Return db.ExecuteNonQuery(dbcmd)
        End Function

        Public Function Update(ByVal EmpSenCompRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim strDot As String = String.Empty

            strSQL.AppendLine("Update EmpSenComp Set")
            For i As Integer = 0 To EmpSenCompRow.FieldNames.Length - 1
                If Not EmpSenCompRow.IsIdentityField(EmpSenCompRow.FieldNames(i)) AndAlso EmpSenCompRow.IsUpdated(EmpSenCompRow.FieldNames(i)) AndAlso EmpSenCompRow.CreateUpdateSQL(EmpSenCompRow.FieldNames(i)) Then
                    strSQL.AppendLine(string.Format("{0}{1} = @{1}", strDot, EmpSenCompRow.FieldNames(i)))
                    strDot = ","
                End If
            Next 
            If strDot = String.Empty Then Throw New Exception("未異動資料欄位！")
            strSQL.AppendLine("Where IDNo = @PKIDNo")
            strSQL.AppendLine("And CompID = @PKCompID")
            strSQL.AppendLine("And EmpID = @PKEmpID")
            strSQL.AppendLine("And ValidDateB = @PKValidDateB")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            If EmpSenCompRow.rowid.Updated Then db.AddInParameter(dbcmd, "@rowid", DbType.Int32, EmpSenCompRow.rowid.Value)
            If EmpSenCompRow.Seq.Updated Then db.AddInParameter(dbcmd, "@Seq", DbType.Int32, EmpSenCompRow.Seq.Value)
            If EmpSenCompRow.IDNo.Updated Then db.AddInParameter(dbcmd, "@IDNo", DbType.String, EmpSenCompRow.IDNo.Value)
            If EmpSenCompRow.CompID.Updated Then db.AddInParameter(dbcmd, "@CompID", DbType.String, EmpSenCompRow.CompID.Value)
            If EmpSenCompRow.EmpID.Updated Then db.AddInParameter(dbcmd, "@EmpID", DbType.String, EmpSenCompRow.EmpID.Value)
            If EmpSenCompRow.ValidDateB.Updated Then db.AddInParameter(dbcmd, "@ValidDateB", DbType.Date, IIf(IsDateTimeNull(EmpSenCompRow.ValidDateB.Value), Convert.ToDateTime("1900/1/1"), EmpSenCompRow.ValidDateB.Value))
            If EmpSenCompRow.ValidDateE.Updated Then db.AddInParameter(dbcmd, "@ValidDateE", DbType.Date, IIf(IsDateTimeNull(EmpSenCompRow.ValidDateE.Value), Convert.ToDateTime("1900/1/1"), EmpSenCompRow.ValidDateE.Value))
            If EmpSenCompRow.ValidDateB_Sinopac.Updated Then db.AddInParameter(dbcmd, "@ValidDateB_Sinopac", DbType.Date, IIf(IsDateTimeNull(EmpSenCompRow.ValidDateB_Sinopac.Value), Convert.ToDateTime("1900/1/1"), EmpSenCompRow.ValidDateB_Sinopac.Value))
            If EmpSenCompRow.ValidDateE_Sinopac.Updated Then db.AddInParameter(dbcmd, "@ValidDateE_Sinopac", DbType.Date, IIf(IsDateTimeNull(EmpSenCompRow.ValidDateE_Sinopac.Value), Convert.ToDateTime("1900/1/1"), EmpSenCompRow.ValidDateE_Sinopac.Value))
            If EmpSenCompRow.Days.Updated Then db.AddInParameter(dbcmd, "@Days", DbType.Decimal, EmpSenCompRow.Days.Value)
            If EmpSenCompRow.Days_SPHOLD.Updated Then db.AddInParameter(dbcmd, "@Days_SPHOLD", DbType.Decimal, EmpSenCompRow.Days_SPHOLD.Value)
            If EmpSenCompRow.ConFlag.Updated Then db.AddInParameter(dbcmd, "@ConFlag", DbType.String, EmpSenCompRow.ConFlag.Value)
            If EmpSenCompRow.CurrentSen.Updated Then db.AddInParameter(dbcmd, "@CurrentSen", DbType.Decimal, EmpSenCompRow.CurrentSen.Value)
            If EmpSenCompRow.ConSen.Updated Then db.AddInParameter(dbcmd, "@ConSen", DbType.Decimal, EmpSenCompRow.ConSen.Value)
            If EmpSenCompRow.TotSen.Updated Then db.AddInParameter(dbcmd, "@TotSen", DbType.Decimal, EmpSenCompRow.TotSen.Value)
            If EmpSenCompRow.TotSen_SPHOLD.Updated Then db.AddInParameter(dbcmd, "@TotSen_SPHOLD", DbType.Decimal, EmpSenCompRow.TotSen_SPHOLD.Value)
            If EmpSenCompRow.TotSen_SPHOLD1.Updated Then db.AddInParameter(dbcmd, "@TotSen_SPHOLD1", DbType.Decimal, EmpSenCompRow.TotSen_SPHOLD1.Value)
            If EmpSenCompRow.TotSen_SPHOLD2.Updated Then db.AddInParameter(dbcmd, "@TotSen_SPHOLD2", DbType.Decimal, EmpSenCompRow.TotSen_SPHOLD2.Value)
            If EmpSenCompRow.LastChgComp.Updated Then db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, EmpSenCompRow.LastChgComp.Value)
            If EmpSenCompRow.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, EmpSenCompRow.LastChgID.Value)
            If EmpSenCompRow.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(EmpSenCompRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), EmpSenCompRow.LastChgDate.Value))
            db.AddInParameter(dbcmd, "@PKIDNo", DbType.String, IIf(EmpSenCompRow.LoadFromDataRow, EmpSenCompRow.IDNo.OldValue, EmpSenCompRow.IDNo.Value))
            db.AddInParameter(dbcmd, "@PKCompID", DbType.String, IIf(EmpSenCompRow.LoadFromDataRow, EmpSenCompRow.CompID.OldValue, EmpSenCompRow.CompID.Value))
            db.AddInParameter(dbcmd, "@PKEmpID", DbType.String, IIf(EmpSenCompRow.LoadFromDataRow, EmpSenCompRow.EmpID.OldValue, EmpSenCompRow.EmpID.Value))
            db.AddInParameter(dbcmd, "@PKValidDateB", DbType.Date, IIf(EmpSenCompRow.LoadFromDataRow, EmpSenCompRow.ValidDateB.OldValue, EmpSenCompRow.ValidDateB.Value))

            Return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function Update(ByVal EmpSenCompRow As Row()) As Integer
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
                    For Each r As Row In EmpSenCompRow
                        strDot = String.Empty
                        If strSQL.Length > 0 Then strSQL.Remove(0, strSQL.Length)
                        strSQL.AppendLine("Update EmpSenComp Set")
                        For i As Integer = 0 To r.FieldNames.Length - 1
                            If Not r.IsIdentityField(r.FieldNames(i)) AndAlso r.IsUpdated(r.FieldNames(i)) AndAlso r.CreateUpdateSQL(r.FieldNames(i)) Then
                                strSQL.AppendLine(String.Format("{0}{1} = @{1}", strDot, r.FieldNames(i)))
                                strDot = ","
                            End If
                        Next
                        If strDot = String.Empty Then Continue For
                        strSQL.AppendLine("Where IDNo = @PKIDNo")
                        strSQL.AppendLine("And CompID = @PKCompID")
                        strSQL.AppendLine("And EmpID = @PKEmpID")
                        strSQL.AppendLine("And ValidDateB = @PKValidDateB")

                        dbcmd = db.GetSqlStringCommand(strSQL.ToString())
                        If r.rowid.Updated Then db.AddInParameter(dbcmd, "@rowid", DbType.Int32, r.rowid.Value)
                        If r.Seq.Updated Then db.AddInParameter(dbcmd, "@Seq", DbType.Int32, r.Seq.Value)
                        If r.IDNo.Updated Then db.AddInParameter(dbcmd, "@IDNo", DbType.String, r.IDNo.Value)
                        If r.CompID.Updated Then db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                        If r.EmpID.Updated Then db.AddInParameter(dbcmd, "@EmpID", DbType.String, r.EmpID.Value)
                        If r.ValidDateB.Updated Then db.AddInParameter(dbcmd, "@ValidDateB", DbType.Date, IIf(IsDateTimeNull(r.ValidDateB.Value), Convert.ToDateTime("1900/1/1"), r.ValidDateB.Value))
                        If r.ValidDateE.Updated Then db.AddInParameter(dbcmd, "@ValidDateE", DbType.Date, IIf(IsDateTimeNull(r.ValidDateE.Value), Convert.ToDateTime("1900/1/1"), r.ValidDateE.Value))
                        If r.ValidDateB_Sinopac.Updated Then db.AddInParameter(dbcmd, "@ValidDateB_Sinopac", DbType.Date, IIf(IsDateTimeNull(r.ValidDateB_Sinopac.Value), Convert.ToDateTime("1900/1/1"), r.ValidDateB_Sinopac.Value))
                        If r.ValidDateE_Sinopac.Updated Then db.AddInParameter(dbcmd, "@ValidDateE_Sinopac", DbType.Date, IIf(IsDateTimeNull(r.ValidDateE_Sinopac.Value), Convert.ToDateTime("1900/1/1"), r.ValidDateE_Sinopac.Value))
                        If r.Days.Updated Then db.AddInParameter(dbcmd, "@Days", DbType.Decimal, r.Days.Value)
                        If r.Days_SPHOLD.Updated Then db.AddInParameter(dbcmd, "@Days_SPHOLD", DbType.Decimal, r.Days_SPHOLD.Value)
                        If r.ConFlag.Updated Then db.AddInParameter(dbcmd, "@ConFlag", DbType.String, r.ConFlag.Value)
                        If r.CurrentSen.Updated Then db.AddInParameter(dbcmd, "@CurrentSen", DbType.Decimal, r.CurrentSen.Value)
                        If r.ConSen.Updated Then db.AddInParameter(dbcmd, "@ConSen", DbType.Decimal, r.ConSen.Value)
                        If r.TotSen.Updated Then db.AddInParameter(dbcmd, "@TotSen", DbType.Decimal, r.TotSen.Value)
                        If r.TotSen_SPHOLD.Updated Then db.AddInParameter(dbcmd, "@TotSen_SPHOLD", DbType.Decimal, r.TotSen_SPHOLD.Value)
                        If r.TotSen_SPHOLD1.Updated Then db.AddInParameter(dbcmd, "@TotSen_SPHOLD1", DbType.Decimal, r.TotSen_SPHOLD1.Value)
                        If r.TotSen_SPHOLD2.Updated Then db.AddInParameter(dbcmd, "@TotSen_SPHOLD2", DbType.Decimal, r.TotSen_SPHOLD2.Value)
                        If r.LastChgComp.Updated Then db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, r.LastChgComp.Value)
                        If r.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, r.LastChgID.Value)
                        If r.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(r.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), r.LastChgDate.Value))
                        db.AddInParameter(dbcmd, "@PKIDNo", DbType.String, IIf(r.LoadFromDataRow, r.IDNo.OldValue, r.IDNo.Value))
                        db.AddInParameter(dbcmd, "@PKCompID", DbType.String, IIf(r.LoadFromDataRow, r.CompID.OldValue, r.CompID.Value))
                        db.AddInParameter(dbcmd, "@PKEmpID", DbType.String, IIf(r.LoadFromDataRow, r.EmpID.OldValue, r.EmpID.Value))
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

        Public Function Update(ByVal EmpSenCompRow As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0
            Dim strDot As String

            For Each r As Row In EmpSenCompRow
                strDot = String.Empty
                If strSQL.Length > 0 Then strSQL.Remove(0, strSQL.Length)
                strSQL.AppendLine("Update EmpSenComp Set")
                For i As Integer = 0 To r.FieldNames.Length - 1
                    If Not r.IsIdentityField(r.FieldNames(i)) AndAlso r.IsUpdated(r.FieldNames(i)) AndAlso r.CreateUpdateSQL(r.FieldNames(i))
                        strSQL.AppendLine(String.Format("{0}{1} = @{1}", strDot, r.FieldNames(i)))
                        strDot = ","
                    End If
                Next
                If strDot = String.Empty Then Continue For
                strSQL.AppendLine("Where IDNo = @PKIDNo")
                strSQL.AppendLine("And CompID = @PKCompID")
                strSQL.AppendLine("And EmpID = @PKEmpID")
                strSQL.AppendLine("And ValidDateB = @PKValidDateB")

                dbcmd = db.GetSqlStringCommand(strSQL.ToString())
                If r.rowid.Updated Then db.AddInParameter(dbcmd, "@rowid", DbType.Int32, r.rowid.Value)
                If r.Seq.Updated Then db.AddInParameter(dbcmd, "@Seq", DbType.Int32, r.Seq.Value)
                If r.IDNo.Updated Then db.AddInParameter(dbcmd, "@IDNo", DbType.String, r.IDNo.Value)
                If r.CompID.Updated Then db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                If r.EmpID.Updated Then db.AddInParameter(dbcmd, "@EmpID", DbType.String, r.EmpID.Value)
                If r.ValidDateB.Updated Then db.AddInParameter(dbcmd, "@ValidDateB", DbType.Date, IIf(IsDateTimeNull(r.ValidDateB.Value), Convert.ToDateTime("1900/1/1"), r.ValidDateB.Value))
                If r.ValidDateE.Updated Then db.AddInParameter(dbcmd, "@ValidDateE", DbType.Date, IIf(IsDateTimeNull(r.ValidDateE.Value), Convert.ToDateTime("1900/1/1"), r.ValidDateE.Value))
                If r.ValidDateB_Sinopac.Updated Then db.AddInParameter(dbcmd, "@ValidDateB_Sinopac", DbType.Date, IIf(IsDateTimeNull(r.ValidDateB_Sinopac.Value), Convert.ToDateTime("1900/1/1"), r.ValidDateB_Sinopac.Value))
                If r.ValidDateE_Sinopac.Updated Then db.AddInParameter(dbcmd, "@ValidDateE_Sinopac", DbType.Date, IIf(IsDateTimeNull(r.ValidDateE_Sinopac.Value), Convert.ToDateTime("1900/1/1"), r.ValidDateE_Sinopac.Value))
                If r.Days.Updated Then db.AddInParameter(dbcmd, "@Days", DbType.Decimal, r.Days.Value)
                If r.Days_SPHOLD.Updated Then db.AddInParameter(dbcmd, "@Days_SPHOLD", DbType.Decimal, r.Days_SPHOLD.Value)
                If r.ConFlag.Updated Then db.AddInParameter(dbcmd, "@ConFlag", DbType.String, r.ConFlag.Value)
                If r.CurrentSen.Updated Then db.AddInParameter(dbcmd, "@CurrentSen", DbType.Decimal, r.CurrentSen.Value)
                If r.ConSen.Updated Then db.AddInParameter(dbcmd, "@ConSen", DbType.Decimal, r.ConSen.Value)
                If r.TotSen.Updated Then db.AddInParameter(dbcmd, "@TotSen", DbType.Decimal, r.TotSen.Value)
                If r.TotSen_SPHOLD.Updated Then db.AddInParameter(dbcmd, "@TotSen_SPHOLD", DbType.Decimal, r.TotSen_SPHOLD.Value)
                If r.TotSen_SPHOLD1.Updated Then db.AddInParameter(dbcmd, "@TotSen_SPHOLD1", DbType.Decimal, r.TotSen_SPHOLD1.Value)
                If r.TotSen_SPHOLD2.Updated Then db.AddInParameter(dbcmd, "@TotSen_SPHOLD2", DbType.Decimal, r.TotSen_SPHOLD2.Value)
                If r.LastChgComp.Updated Then db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, r.LastChgComp.Value)
                If r.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, r.LastChgID.Value)
                If r.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(r.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), r.LastChgDate.Value))
                db.AddInParameter(dbcmd, "@PKIDNo", DbType.String, IIf(r.LoadFromDataRow, r.IDNo.OldValue, r.IDNo.Value))
                db.AddInParameter(dbcmd, "@PKCompID", DbType.String, IIf(r.LoadFromDataRow, r.CompID.OldValue, r.CompID.Value))
                db.AddInParameter(dbcmd, "@PKEmpID", DbType.String, IIf(r.LoadFromDataRow, r.EmpID.OldValue, r.EmpID.Value))
                db.AddInParameter(dbcmd, "@PKValidDateB", DbType.Date, IIf(r.LoadFromDataRow, r.ValidDateB.OldValue, r.ValidDateB.Value))

                intRowsAffected += db.ExecuteNonQuery(dbcmd, tran)
            Next
            Return intRowsAffected
        End Function

        Public Function IsDataExists(ByVal EmpSenCompRow As Row) As Boolean
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intCount As Integer = 0

            strSQL.AppendLine("Select Count(*) Cnt From EmpSenComp")
            strSQL.AppendLine("Where IDNo = @IDNo")
            strSQL.AppendLine("And CompID = @CompID")
            strSQL.AppendLine("And EmpID = @EmpID")
            strSQL.AppendLine("And ValidDateB = @ValidDateB")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@IDNo", DbType.String, EmpSenCompRow.IDNo.Value)
            db.AddInParameter(dbcmd, "@CompID", DbType.String, EmpSenCompRow.CompID.Value)
            db.AddInParameter(dbcmd, "@EmpID", DbType.String, EmpSenCompRow.EmpID.Value)
            db.AddInParameter(dbcmd, "@ValidDateB", DbType.Date, EmpSenCompRow.ValidDateB.Value)

            intCount = Convert.ToInt32(db.ExecuteScalar(dbcmd))
            Return IIf(intCount > 0, True, False)
        End Function

        Public Function IsDataExists(ByVal EmpSenCompRow As Row, ByVal tran As DbTransaction) As Boolean
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intCount As Integer = 0

            strSQL.AppendLine("Select Count(*) Cnt From EmpSenComp")
            strSQL.AppendLine("Where IDNo = @IDNo")
            strSQL.AppendLine("And CompID = @CompID")
            strSQL.AppendLine("And EmpID = @EmpID")
            strSQL.AppendLine("And ValidDateB = @ValidDateB")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@IDNo", DbType.String, EmpSenCompRow.IDNo.Value)
            db.AddInParameter(dbcmd, "@CompID", DbType.String, EmpSenCompRow.CompID.Value)
            db.AddInParameter(dbcmd, "@EmpID", DbType.String, EmpSenCompRow.EmpID.Value)
            db.AddInParameter(dbcmd, "@ValidDateB", DbType.Date, EmpSenCompRow.ValidDateB.Value)

            intCount = Convert.ToInt32(db.ExecuteScalar(dbcmd, tran))
            Return IIf(intCount > 0, True, False)
        End Function

        Public Function QuerybyWhere(WhereCondition As String) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From EmpSenComp")
            If WhereCondition <> String.Empty Then strSQL.AppendLine(WhereCondition)

            Return db.ExecuteDataSet(CommandType.Text, strSQL.ToString())
        End Function

        Public Function Insert(ByVal EmpSenCompRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Insert into EmpSenComp")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    rowid, Seq, IDNo, CompID, EmpID, ValidDateB, ValidDateE, ValidDateB_Sinopac, ValidDateE_Sinopac,")
            strSQL.AppendLine("    Days, Days_SPHOLD, ConFlag, CurrentSen, ConSen, TotSen, TotSen_SPHOLD, TotSen_SPHOLD1,")
            strSQL.AppendLine("    TotSen_SPHOLD2, LastChgComp, LastChgID, LastChgDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @rowid, @Seq, @IDNo, @CompID, @EmpID, @ValidDateB, @ValidDateE, @ValidDateB_Sinopac, @ValidDateE_Sinopac,")
            strSQL.AppendLine("    @Days, @Days_SPHOLD, @ConFlag, @CurrentSen, @ConSen, @TotSen, @TotSen_SPHOLD, @TotSen_SPHOLD1,")
            strSQL.AppendLine("    @TotSen_SPHOLD2, @LastChgComp, @LastChgID, @LastChgDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@rowid", DbType.Int32, EmpSenCompRow.rowid.Value)
            db.AddInParameter(dbcmd, "@Seq", DbType.Int32, EmpSenCompRow.Seq.Value)
            db.AddInParameter(dbcmd, "@IDNo", DbType.String, EmpSenCompRow.IDNo.Value)
            db.AddInParameter(dbcmd, "@CompID", DbType.String, EmpSenCompRow.CompID.Value)
            db.AddInParameter(dbcmd, "@EmpID", DbType.String, EmpSenCompRow.EmpID.Value)
            db.AddInParameter(dbcmd, "@ValidDateB", DbType.Date, IIf(IsDateTimeNull(EmpSenCompRow.ValidDateB.Value), Convert.ToDateTime("1900/1/1"), EmpSenCompRow.ValidDateB.Value))
            db.AddInParameter(dbcmd, "@ValidDateE", DbType.Date, IIf(IsDateTimeNull(EmpSenCompRow.ValidDateE.Value), Convert.ToDateTime("1900/1/1"), EmpSenCompRow.ValidDateE.Value))
            db.AddInParameter(dbcmd, "@ValidDateB_Sinopac", DbType.Date, IIf(IsDateTimeNull(EmpSenCompRow.ValidDateB_Sinopac.Value), Convert.ToDateTime("1900/1/1"), EmpSenCompRow.ValidDateB_Sinopac.Value))
            db.AddInParameter(dbcmd, "@ValidDateE_Sinopac", DbType.Date, IIf(IsDateTimeNull(EmpSenCompRow.ValidDateE_Sinopac.Value), Convert.ToDateTime("1900/1/1"), EmpSenCompRow.ValidDateE_Sinopac.Value))
            db.AddInParameter(dbcmd, "@Days", DbType.Decimal, EmpSenCompRow.Days.Value)
            db.AddInParameter(dbcmd, "@Days_SPHOLD", DbType.Decimal, EmpSenCompRow.Days_SPHOLD.Value)
            db.AddInParameter(dbcmd, "@ConFlag", DbType.String, EmpSenCompRow.ConFlag.Value)
            db.AddInParameter(dbcmd, "@CurrentSen", DbType.Decimal, EmpSenCompRow.CurrentSen.Value)
            db.AddInParameter(dbcmd, "@ConSen", DbType.Decimal, EmpSenCompRow.ConSen.Value)
            db.AddInParameter(dbcmd, "@TotSen", DbType.Decimal, EmpSenCompRow.TotSen.Value)
            db.AddInParameter(dbcmd, "@TotSen_SPHOLD", DbType.Decimal, EmpSenCompRow.TotSen_SPHOLD.Value)
            db.AddInParameter(dbcmd, "@TotSen_SPHOLD1", DbType.Decimal, EmpSenCompRow.TotSen_SPHOLD1.Value)
            db.AddInParameter(dbcmd, "@TotSen_SPHOLD2", DbType.Decimal, EmpSenCompRow.TotSen_SPHOLD2.Value)
            db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, EmpSenCompRow.LastChgComp.Value)
            db.AddInParameter(dbcmd, "@LastChgID", DbType.String, EmpSenCompRow.LastChgID.Value)
            db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(EmpSenCompRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), EmpSenCompRow.LastChgDate.Value))

            Return db.ExecuteNonQuery(dbcmd)
        End Function

        Public Function Insert(ByVal EmpSenCompRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Insert into EmpSenComp")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    rowid, Seq, IDNo, CompID, EmpID, ValidDateB, ValidDateE, ValidDateB_Sinopac, ValidDateE_Sinopac,")
            strSQL.AppendLine("    Days, Days_SPHOLD, ConFlag, CurrentSen, ConSen, TotSen, TotSen_SPHOLD, TotSen_SPHOLD1,")
            strSQL.AppendLine("    TotSen_SPHOLD2, LastChgComp, LastChgID, LastChgDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @rowid, @Seq, @IDNo, @CompID, @EmpID, @ValidDateB, @ValidDateE, @ValidDateB_Sinopac, @ValidDateE_Sinopac,")
            strSQL.AppendLine("    @Days, @Days_SPHOLD, @ConFlag, @CurrentSen, @ConSen, @TotSen, @TotSen_SPHOLD, @TotSen_SPHOLD1,")
            strSQL.AppendLine("    @TotSen_SPHOLD2, @LastChgComp, @LastChgID, @LastChgDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@rowid", DbType.Int32, EmpSenCompRow.rowid.Value)
            db.AddInParameter(dbcmd, "@Seq", DbType.Int32, EmpSenCompRow.Seq.Value)
            db.AddInParameter(dbcmd, "@IDNo", DbType.String, EmpSenCompRow.IDNo.Value)
            db.AddInParameter(dbcmd, "@CompID", DbType.String, EmpSenCompRow.CompID.Value)
            db.AddInParameter(dbcmd, "@EmpID", DbType.String, EmpSenCompRow.EmpID.Value)
            db.AddInParameter(dbcmd, "@ValidDateB", DbType.Date, IIf(IsDateTimeNull(EmpSenCompRow.ValidDateB.Value), Convert.ToDateTime("1900/1/1"), EmpSenCompRow.ValidDateB.Value))
            db.AddInParameter(dbcmd, "@ValidDateE", DbType.Date, IIf(IsDateTimeNull(EmpSenCompRow.ValidDateE.Value), Convert.ToDateTime("1900/1/1"), EmpSenCompRow.ValidDateE.Value))
            db.AddInParameter(dbcmd, "@ValidDateB_Sinopac", DbType.Date, IIf(IsDateTimeNull(EmpSenCompRow.ValidDateB_Sinopac.Value), Convert.ToDateTime("1900/1/1"), EmpSenCompRow.ValidDateB_Sinopac.Value))
            db.AddInParameter(dbcmd, "@ValidDateE_Sinopac", DbType.Date, IIf(IsDateTimeNull(EmpSenCompRow.ValidDateE_Sinopac.Value), Convert.ToDateTime("1900/1/1"), EmpSenCompRow.ValidDateE_Sinopac.Value))
            db.AddInParameter(dbcmd, "@Days", DbType.Decimal, EmpSenCompRow.Days.Value)
            db.AddInParameter(dbcmd, "@Days_SPHOLD", DbType.Decimal, EmpSenCompRow.Days_SPHOLD.Value)
            db.AddInParameter(dbcmd, "@ConFlag", DbType.String, EmpSenCompRow.ConFlag.Value)
            db.AddInParameter(dbcmd, "@CurrentSen", DbType.Decimal, EmpSenCompRow.CurrentSen.Value)
            db.AddInParameter(dbcmd, "@ConSen", DbType.Decimal, EmpSenCompRow.ConSen.Value)
            db.AddInParameter(dbcmd, "@TotSen", DbType.Decimal, EmpSenCompRow.TotSen.Value)
            db.AddInParameter(dbcmd, "@TotSen_SPHOLD", DbType.Decimal, EmpSenCompRow.TotSen_SPHOLD.Value)
            db.AddInParameter(dbcmd, "@TotSen_SPHOLD1", DbType.Decimal, EmpSenCompRow.TotSen_SPHOLD1.Value)
            db.AddInParameter(dbcmd, "@TotSen_SPHOLD2", DbType.Decimal, EmpSenCompRow.TotSen_SPHOLD2.Value)
            db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, EmpSenCompRow.LastChgComp.Value)
            db.AddInParameter(dbcmd, "@LastChgID", DbType.String, EmpSenCompRow.LastChgID.Value)
            db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(EmpSenCompRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), EmpSenCompRow.LastChgDate.Value))

            Return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function Insert(ByVal EmpSenCompRow As Row()) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Insert into EmpSenComp")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    rowid, Seq, IDNo, CompID, EmpID, ValidDateB, ValidDateE, ValidDateB_Sinopac, ValidDateE_Sinopac,")
            strSQL.AppendLine("    Days, Days_SPHOLD, ConFlag, CurrentSen, ConSen, TotSen, TotSen_SPHOLD, TotSen_SPHOLD1,")
            strSQL.AppendLine("    TotSen_SPHOLD2, LastChgComp, LastChgID, LastChgDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @rowid, @Seq, @IDNo, @CompID, @EmpID, @ValidDateB, @ValidDateE, @ValidDateB_Sinopac, @ValidDateE_Sinopac,")
            strSQL.AppendLine("    @Days, @Days_SPHOLD, @ConFlag, @CurrentSen, @ConSen, @TotSen, @TotSen_SPHOLD, @TotSen_SPHOLD1,")
            strSQL.AppendLine("    @TotSen_SPHOLD2, @LastChgComp, @LastChgID, @LastChgDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            Using cn As DbConnection = db.CreateConnection()
                cn.Open()
                Dim tran As DbTransaction = cn.BeginTransaction()
                Dim inTrans As Boolean = True

                Try
                    For Each r As Row In EmpSenCompRow
                        dbcmd.Parameters.Clear()
                        db.AddInParameter(dbcmd, "@rowid", DbType.Int32, r.rowid.Value)
                        db.AddInParameter(dbcmd, "@Seq", DbType.Int32, r.Seq.Value)
                        db.AddInParameter(dbcmd, "@IDNo", DbType.String, r.IDNo.Value)
                        db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                        db.AddInParameter(dbcmd, "@EmpID", DbType.String, r.EmpID.Value)
                        db.AddInParameter(dbcmd, "@ValidDateB", DbType.Date, IIf(IsDateTimeNull(r.ValidDateB.Value), Convert.ToDateTime("1900/1/1"), r.ValidDateB.Value))
                        db.AddInParameter(dbcmd, "@ValidDateE", DbType.Date, IIf(IsDateTimeNull(r.ValidDateE.Value), Convert.ToDateTime("1900/1/1"), r.ValidDateE.Value))
                        db.AddInParameter(dbcmd, "@ValidDateB_Sinopac", DbType.Date, IIf(IsDateTimeNull(r.ValidDateB_Sinopac.Value), Convert.ToDateTime("1900/1/1"), r.ValidDateB_Sinopac.Value))
                        db.AddInParameter(dbcmd, "@ValidDateE_Sinopac", DbType.Date, IIf(IsDateTimeNull(r.ValidDateE_Sinopac.Value), Convert.ToDateTime("1900/1/1"), r.ValidDateE_Sinopac.Value))
                        db.AddInParameter(dbcmd, "@Days", DbType.Decimal, r.Days.Value)
                        db.AddInParameter(dbcmd, "@Days_SPHOLD", DbType.Decimal, r.Days_SPHOLD.Value)
                        db.AddInParameter(dbcmd, "@ConFlag", DbType.String, r.ConFlag.Value)
                        db.AddInParameter(dbcmd, "@CurrentSen", DbType.Decimal, r.CurrentSen.Value)
                        db.AddInParameter(dbcmd, "@ConSen", DbType.Decimal, r.ConSen.Value)
                        db.AddInParameter(dbcmd, "@TotSen", DbType.Decimal, r.TotSen.Value)
                        db.AddInParameter(dbcmd, "@TotSen_SPHOLD", DbType.Decimal, r.TotSen_SPHOLD.Value)
                        db.AddInParameter(dbcmd, "@TotSen_SPHOLD1", DbType.Decimal, r.TotSen_SPHOLD1.Value)
                        db.AddInParameter(dbcmd, "@TotSen_SPHOLD2", DbType.Decimal, r.TotSen_SPHOLD2.Value)
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

        Public Function Insert(ByVal EmpSenCompRow As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Insert into EmpSenComp")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    rowid, Seq, IDNo, CompID, EmpID, ValidDateB, ValidDateE, ValidDateB_Sinopac, ValidDateE_Sinopac,")
            strSQL.AppendLine("    Days, Days_SPHOLD, ConFlag, CurrentSen, ConSen, TotSen, TotSen_SPHOLD, TotSen_SPHOLD1,")
            strSQL.AppendLine("    TotSen_SPHOLD2, LastChgComp, LastChgID, LastChgDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @rowid, @Seq, @IDNo, @CompID, @EmpID, @ValidDateB, @ValidDateE, @ValidDateB_Sinopac, @ValidDateE_Sinopac,")
            strSQL.AppendLine("    @Days, @Days_SPHOLD, @ConFlag, @CurrentSen, @ConSen, @TotSen, @TotSen_SPHOLD, @TotSen_SPHOLD1,")
            strSQL.AppendLine("    @TotSen_SPHOLD2, @LastChgComp, @LastChgID, @LastChgDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())

            For Each r As Row In EmpSenCompRow
                dbcmd.Parameters.Clear()
                db.AddInParameter(dbcmd, "@rowid", DbType.Int32, r.rowid.Value)
                db.AddInParameter(dbcmd, "@Seq", DbType.Int32, r.Seq.Value)
                db.AddInParameter(dbcmd, "@IDNo", DbType.String, r.IDNo.Value)
                db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                db.AddInParameter(dbcmd, "@EmpID", DbType.String, r.EmpID.Value)
                db.AddInParameter(dbcmd, "@ValidDateB", DbType.Date, IIf(IsDateTimeNull(r.ValidDateB.Value), Convert.ToDateTime("1900/1/1"), r.ValidDateB.Value))
                db.AddInParameter(dbcmd, "@ValidDateE", DbType.Date, IIf(IsDateTimeNull(r.ValidDateE.Value), Convert.ToDateTime("1900/1/1"), r.ValidDateE.Value))
                db.AddInParameter(dbcmd, "@ValidDateB_Sinopac", DbType.Date, IIf(IsDateTimeNull(r.ValidDateB_Sinopac.Value), Convert.ToDateTime("1900/1/1"), r.ValidDateB_Sinopac.Value))
                db.AddInParameter(dbcmd, "@ValidDateE_Sinopac", DbType.Date, IIf(IsDateTimeNull(r.ValidDateE_Sinopac.Value), Convert.ToDateTime("1900/1/1"), r.ValidDateE_Sinopac.Value))
                db.AddInParameter(dbcmd, "@Days", DbType.Decimal, r.Days.Value)
                db.AddInParameter(dbcmd, "@Days_SPHOLD", DbType.Decimal, r.Days_SPHOLD.Value)
                db.AddInParameter(dbcmd, "@ConFlag", DbType.String, r.ConFlag.Value)
                db.AddInParameter(dbcmd, "@CurrentSen", DbType.Decimal, r.CurrentSen.Value)
                db.AddInParameter(dbcmd, "@ConSen", DbType.Decimal, r.ConSen.Value)
                db.AddInParameter(dbcmd, "@TotSen", DbType.Decimal, r.TotSen.Value)
                db.AddInParameter(dbcmd, "@TotSen_SPHOLD", DbType.Decimal, r.TotSen_SPHOLD.Value)
                db.AddInParameter(dbcmd, "@TotSen_SPHOLD1", DbType.Decimal, r.TotSen_SPHOLD1.Value)
                db.AddInParameter(dbcmd, "@TotSen_SPHOLD2", DbType.Decimal, r.TotSen_SPHOLD2.Value)
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

