'****************************************************************
' Table:EmpSenWorkType
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

Namespace beEmpSenWorkType
    Public Class Table
        Private m_Rows As Rows = New Rows()
        Private m_Fields As String() = { "rowid", "Seq", "CompID", "PreCompID", "Reason", "EmpID", "IDNo", "WorkTypeID", "WorkType", "ValidDateB" _
                                    , "ValidDateE", "Days", "ConFlag", "CurrentSen", "ConSen", "TotSen", "CategoryI", "TotCategoryI", "CategoryII", "TotCategoryII", "CategoryIII" _
                                    , "TotCategoryIII", "LastChgComp", "LastChgID", "LastChgDate" }
        Private m_Types As System.Type() = { GetType(Integer), GetType(Integer), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(Date) _
                                    , GetType(Date), GetType(Decimal), GetType(String), GetType(Decimal), GetType(Decimal), GetType(Decimal), GetType(String), GetType(Decimal), GetType(String), GetType(Decimal), GetType(String) _
                                    , GetType(Decimal), GetType(String), GetType(String), GetType(Date) }
        Private m_PrimaryFields As String() = { "CompID", "Reason", "EmpID", "WorkTypeID", "ValidDateB" }

        Public ReadOnly Property Rows() As beEmpSenWorkType.Rows 
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
        Public Sub Transfer2Row(EmpSenWorkTypeTable As DataTable)
            For Each dr As DataRow In EmpSenWorkTypeTable.Rows
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
                dr(m_Rows(i).WorkTypeID.FieldName) = m_Rows(i).WorkTypeID.Value
                dr(m_Rows(i).WorkType.FieldName) = m_Rows(i).WorkType.Value
                dr(m_Rows(i).ValidDateB.FieldName) = m_Rows(i).ValidDateB.Value
                dr(m_Rows(i).ValidDateE.FieldName) = m_Rows(i).ValidDateE.Value
                dr(m_Rows(i).Days.FieldName) = m_Rows(i).Days.Value
                dr(m_Rows(i).ConFlag.FieldName) = m_Rows(i).ConFlag.Value
                dr(m_Rows(i).CurrentSen.FieldName) = m_Rows(i).CurrentSen.Value
                dr(m_Rows(i).ConSen.FieldName) = m_Rows(i).ConSen.Value
                dr(m_Rows(i).TotSen.FieldName) = m_Rows(i).TotSen.Value
                dr(m_Rows(i).CategoryI.FieldName) = m_Rows(i).CategoryI.Value
                dr(m_Rows(i).TotCategoryI.FieldName) = m_Rows(i).TotCategoryI.Value
                dr(m_Rows(i).CategoryII.FieldName) = m_Rows(i).CategoryII.Value
                dr(m_Rows(i).TotCategoryII.FieldName) = m_Rows(i).TotCategoryII.Value
                dr(m_Rows(i).CategoryIII.FieldName) = m_Rows(i).CategoryIII.Value
                dr(m_Rows(i).TotCategoryIII.FieldName) = m_Rows(i).TotCategoryIII.Value
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

        Public Sub Add(EmpSenWorkTypeRow As Row)
            m_Rows.Add(EmpSenWorkTypeRow)
        End Sub

        Public Sub Remove(EmpSenWorkTypeRow As Row)
            If m_Rows.IndexOf(EmpSenWorkTypeRow) >= 0 Then
                m_Rows.Remove(EmpSenWorkTypeRow)
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
        Private FI_WorkTypeID As Field(Of String) = new Field(Of String)("WorkTypeID", true)
        Private FI_WorkType As Field(Of String) = new Field(Of String)("WorkType", true)
        Private FI_ValidDateB As Field(Of Date) = new Field(Of Date)("ValidDateB", true)
        Private FI_ValidDateE As Field(Of Date) = new Field(Of Date)("ValidDateE", true)
        Private FI_Days As Field(Of Decimal) = new Field(Of Decimal)("Days", true)
        Private FI_ConFlag As Field(Of String) = new Field(Of String)("ConFlag", true)
        Private FI_CurrentSen As Field(Of Decimal) = new Field(Of Decimal)("CurrentSen", true)
        Private FI_ConSen As Field(Of Decimal) = new Field(Of Decimal)("ConSen", true)
        Private FI_TotSen As Field(Of Decimal) = new Field(Of Decimal)("TotSen", true)
        Private FI_CategoryI As Field(Of String) = new Field(Of String)("CategoryI", true)
        Private FI_TotCategoryI As Field(Of Decimal) = new Field(Of Decimal)("TotCategoryI", true)
        Private FI_CategoryII As Field(Of String) = new Field(Of String)("CategoryII", true)
        Private FI_TotCategoryII As Field(Of Decimal) = new Field(Of Decimal)("TotCategoryII", true)
        Private FI_CategoryIII As Field(Of String) = new Field(Of String)("CategoryIII", true)
        Private FI_TotCategoryIII As Field(Of Decimal) = new Field(Of Decimal)("TotCategoryIII", true)
        Private FI_LastChgComp As Field(Of String) = new Field(Of String)("LastChgComp", true)
        Private FI_LastChgID As Field(Of String) = new Field(Of String)("LastChgID", true)
        Private FI_LastChgDate As Field(Of Date) = new Field(Of Date)("LastChgDate", true)
        Private m_FieldNames As String() = { "rowid", "Seq", "CompID", "PreCompID", "Reason", "EmpID", "IDNo", "WorkTypeID", "WorkType", "ValidDateB" _
                                    , "ValidDateE", "Days", "ConFlag", "CurrentSen", "ConSen", "TotSen", "CategoryI", "TotCategoryI", "CategoryII", "TotCategoryII", "CategoryIII" _
                                    , "TotCategoryIII", "LastChgComp", "LastChgID", "LastChgDate" }
        Private m_PrimaryFields As String() = { "CompID", "Reason", "EmpID", "WorkTypeID", "ValidDateB" }
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
                Case "WorkTypeID"
                    Return FI_WorkTypeID.Value
                Case "WorkType"
                    Return FI_WorkType.Value
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
                Case "CategoryI"
                    Return FI_CategoryI.Value
                Case "TotCategoryI"
                    Return FI_TotCategoryI.Value
                Case "CategoryII"
                    Return FI_CategoryII.Value
                Case "TotCategoryII"
                    Return FI_TotCategoryII.Value
                Case "CategoryIII"
                    Return FI_CategoryIII.Value
                Case "TotCategoryIII"
                    Return FI_TotCategoryIII.Value
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
                Case "WorkTypeID"
                    FI_WorkTypeID.SetValue(value)
                Case "WorkType"
                    FI_WorkType.SetValue(value)
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
                Case "CategoryI"
                    FI_CategoryI.SetValue(value)
                Case "TotCategoryI"
                    FI_TotCategoryI.SetValue(value)
                Case "CategoryII"
                    FI_CategoryII.SetValue(value)
                Case "TotCategoryII"
                    FI_TotCategoryII.SetValue(value)
                Case "CategoryIII"
                    FI_CategoryIII.SetValue(value)
                Case "TotCategoryIII"
                    FI_TotCategoryIII.SetValue(value)
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
                Case "WorkTypeID"
                    return FI_WorkTypeID.Updated
                Case "WorkType"
                    return FI_WorkType.Updated
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
                Case "CategoryI"
                    return FI_CategoryI.Updated
                Case "TotCategoryI"
                    return FI_TotCategoryI.Updated
                Case "CategoryII"
                    return FI_CategoryII.Updated
                Case "TotCategoryII"
                    return FI_TotCategoryII.Updated
                Case "CategoryIII"
                    return FI_CategoryIII.Updated
                Case "TotCategoryIII"
                    return FI_TotCategoryIII.Updated
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
                Case "WorkTypeID"
                    return FI_WorkTypeID.CreateUpdateSQL
                Case "WorkType"
                    return FI_WorkType.CreateUpdateSQL
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
                Case "CategoryI"
                    return FI_CategoryI.CreateUpdateSQL
                Case "TotCategoryI"
                    return FI_TotCategoryI.CreateUpdateSQL
                Case "CategoryII"
                    return FI_CategoryII.CreateUpdateSQL
                Case "TotCategoryII"
                    return FI_TotCategoryII.CreateUpdateSQL
                Case "CategoryIII"
                    return FI_CategoryIII.CreateUpdateSQL
                Case "TotCategoryIII"
                    return FI_TotCategoryIII.CreateUpdateSQL
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
            FI_WorkTypeID.SetInitValue("")
            FI_WorkType.SetInitValue("")
            FI_ValidDateB.SetInitValue(Convert.ToDateTime("1900/01/01"))
            FI_ValidDateE.SetInitValue(Convert.ToDateTime("1900/01/01"))
            FI_Days.SetInitValue(0)
            FI_ConFlag.SetInitValue(0)
            FI_CurrentSen.SetInitValue(0)
            FI_ConSen.SetInitValue(0)
            FI_TotSen.SetInitValue(0)
            FI_CategoryI.SetInitValue("")
            FI_TotCategoryI.SetInitValue(0)
            FI_CategoryII.SetInitValue("")
            FI_TotCategoryII.SetInitValue(0)
            FI_CategoryIII.SetInitValue("")
            FI_TotCategoryIII.SetInitValue(0)
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
            FI_WorkTypeID.SetInitValue(dr("WorkTypeID"))
            FI_WorkType.SetInitValue(dr("WorkType"))
            FI_ValidDateB.SetInitValue(dr("ValidDateB"))
            FI_ValidDateE.SetInitValue(dr("ValidDateE"))
            FI_Days.SetInitValue(dr("Days"))
            FI_ConFlag.SetInitValue(dr("ConFlag"))
            FI_CurrentSen.SetInitValue(dr("CurrentSen"))
            FI_ConSen.SetInitValue(dr("ConSen"))
            FI_TotSen.SetInitValue(dr("TotSen"))
            FI_CategoryI.SetInitValue(dr("CategoryI"))
            FI_TotCategoryI.SetInitValue(dr("TotCategoryI"))
            FI_CategoryII.SetInitValue(dr("CategoryII"))
            FI_TotCategoryII.SetInitValue(dr("TotCategoryII"))
            FI_CategoryIII.SetInitValue(dr("CategoryIII"))
            FI_TotCategoryIII.SetInitValue(dr("TotCategoryIII"))
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
            FI_WorkTypeID.Updated = False
            FI_WorkType.Updated = False
            FI_ValidDateB.Updated = False
            FI_ValidDateE.Updated = False
            FI_Days.Updated = False
            FI_ConFlag.Updated = False
            FI_CurrentSen.Updated = False
            FI_ConSen.Updated = False
            FI_TotSen.Updated = False
            FI_CategoryI.Updated = False
            FI_TotCategoryI.Updated = False
            FI_CategoryII.Updated = False
            FI_TotCategoryII.Updated = False
            FI_CategoryIII.Updated = False
            FI_TotCategoryIII.Updated = False
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

        Public ReadOnly Property WorkTypeID As Field(Of String) 
            Get
                Return FI_WorkTypeID
            End Get
        End Property

        Public ReadOnly Property WorkType As Field(Of String) 
            Get
                Return FI_WorkType
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

        Public ReadOnly Property CategoryI As Field(Of String) 
            Get
                Return FI_CategoryI
            End Get
        End Property

        Public ReadOnly Property TotCategoryI As Field(Of Decimal) 
            Get
                Return FI_TotCategoryI
            End Get
        End Property

        Public ReadOnly Property CategoryII As Field(Of String) 
            Get
                Return FI_CategoryII
            End Get
        End Property

        Public ReadOnly Property TotCategoryII As Field(Of Decimal) 
            Get
                Return FI_TotCategoryII
            End Get
        End Property

        Public ReadOnly Property CategoryIII As Field(Of String) 
            Get
                Return FI_CategoryIII
            End Get
        End Property

        Public ReadOnly Property TotCategoryIII As Field(Of Decimal) 
            Get
                Return FI_TotCategoryIII
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
        Public Function DeleteRowByPrimaryKey(ByVal EmpSenWorkTypeRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Delete From EmpSenWorkType")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And Reason = @Reason")
            strSQL.AppendLine("And EmpID = @EmpID")
            strSQL.AppendLine("And WorkTypeID = @WorkTypeID")
            strSQL.AppendLine("And ValidDateB = @ValidDateB")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, EmpSenWorkTypeRow.CompID.Value)
            db.AddInParameter(dbcmd, "@Reason", DbType.String, EmpSenWorkTypeRow.Reason.Value)
            db.AddInParameter(dbcmd, "@EmpID", DbType.String, EmpSenWorkTypeRow.EmpID.Value)
            db.AddInParameter(dbcmd, "@WorkTypeID", DbType.String, EmpSenWorkTypeRow.WorkTypeID.Value)
            db.AddInParameter(dbcmd, "@ValidDateB", DbType.Date, EmpSenWorkTypeRow.ValidDateB.Value)

            return db.ExecuteNonQuery(dbcmd)
        End Function

        public Function DeleteRowByPrimaryKey(ByVal EmpSenWorkTypeRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Delete From EmpSenWorkType")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And Reason = @Reason")
            strSQL.AppendLine("And EmpID = @EmpID")
            strSQL.AppendLine("And WorkTypeID = @WorkTypeID")
            strSQL.AppendLine("And ValidDateB = @ValidDateB")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, EmpSenWorkTypeRow.CompID.Value)
            db.AddInParameter(dbcmd, "@Reason", DbType.String, EmpSenWorkTypeRow.Reason.Value)
            db.AddInParameter(dbcmd, "@EmpID", DbType.String, EmpSenWorkTypeRow.EmpID.Value)
            db.AddInParameter(dbcmd, "@WorkTypeID", DbType.String, EmpSenWorkTypeRow.WorkTypeID.Value)
            db.AddInParameter(dbcmd, "@ValidDateB", DbType.Date, EmpSenWorkTypeRow.ValidDateB.Value)

            return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function DeleteRowByPrimaryKey(ByVal EmpSenWorkTypeRow As Row()) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Delete From EmpSenWorkType")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And Reason = @Reason")
            strSQL.AppendLine("And EmpID = @EmpID")
            strSQL.AppendLine("And WorkTypeID = @WorkTypeID")
            strSQL.AppendLine("And ValidDateB = @ValidDateB")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            using cn As DbConnection = db.CreateConnection()
                cn.Open()
                Dim tran As DbTransaction = cn.BeginTransaction()
                Dim inTrans As Boolean = True

                Try
                    For Each r As Row In EmpSenWorkTypeRow
                        dbcmd.Parameters.Clear()
                        db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                        db.AddInParameter(dbcmd, "@Reason", DbType.String, r.Reason.Value)
                        db.AddInParameter(dbcmd, "@EmpID", DbType.String, r.EmpID.Value)
                        db.AddInParameter(dbcmd, "@WorkTypeID", DbType.String, r.WorkTypeID.Value)
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

        Public Function DeleteRowByPrimaryKey(ByVal EmpSenWorkTypeRow As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Delete From EmpSenWorkType")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And Reason = @Reason")
            strSQL.AppendLine("And EmpID = @EmpID")
            strSQL.AppendLine("And WorkTypeID = @WorkTypeID")
            strSQL.AppendLine("And ValidDateB = @ValidDateB")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())

            For Each r As Row In EmpSenWorkTypeRow
                dbcmd.Parameters.Clear()
                db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                db.AddInParameter(dbcmd, "@Reason", DbType.String, r.Reason.Value)
                db.AddInParameter(dbcmd, "@EmpID", DbType.String, r.EmpID.Value)
                db.AddInParameter(dbcmd, "@WorkTypeID", DbType.String, r.WorkTypeID.Value)
                db.AddInParameter(dbcmd, "@ValidDateB", DbType.Date, r.ValidDateB.Value)

                intRowsAffected += db.ExecuteNonQuery(dbcmd, tran)
            Next
            Return intRowsAffected
        End Function

        Public Function QueryByKey(ByVal EmpSenWorkTypeRow As Row) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From EmpSenWorkType")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And Reason = @Reason")
            strSQL.AppendLine("And EmpID = @EmpID")
            strSQL.AppendLine("And WorkTypeID = @WorkTypeID")
            strSQL.AppendLine("And ValidDateB = @ValidDateB")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, EmpSenWorkTypeRow.CompID.Value)
            db.AddInParameter(dbcmd, "@Reason", DbType.String, EmpSenWorkTypeRow.Reason.Value)
            db.AddInParameter(dbcmd, "@EmpID", DbType.String, EmpSenWorkTypeRow.EmpID.Value)
            db.AddInParameter(dbcmd, "@WorkTypeID", DbType.String, EmpSenWorkTypeRow.WorkTypeID.Value)
            db.AddInParameter(dbcmd, "@ValidDateB", DbType.Date, EmpSenWorkTypeRow.ValidDateB.Value)

            Return db.ExecuteDataSet(dbcmd)
        End Function

        Public Function QueryByKey(EmpSenWorkTypeRow As Row, tran As DbTransaction) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From EmpSenWorkType")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And Reason = @Reason")
            strSQL.AppendLine("And EmpID = @EmpID")
            strSQL.AppendLine("And WorkTypeID = @WorkTypeID")
            strSQL.AppendLine("And ValidDateB = @ValidDateB")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, EmpSenWorkTypeRow.CompID.Value)
            db.AddInParameter(dbcmd, "@Reason", DbType.String, EmpSenWorkTypeRow.Reason.Value)
            db.AddInParameter(dbcmd, "@EmpID", DbType.String, EmpSenWorkTypeRow.EmpID.Value)
            db.AddInParameter(dbcmd, "@WorkTypeID", DbType.String, EmpSenWorkTypeRow.WorkTypeID.Value)
            db.AddInParameter(dbcmd, "@ValidDateB", DbType.Date, EmpSenWorkTypeRow.ValidDateB.Value)

            Return db.ExecuteDataSet(dbcmd, tran)
        End Function

        Public Function Update(ByVal EmpSenWorkTypeRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim strDot As String = String.Empty

            strSQL.AppendLine("Update EmpSenWorkType Set")
            For i As Integer = 0 To EmpSenWorkTypeRow.FieldNames.Length - 1
                If Not EmpSenWorkTypeRow.IsIdentityField(EmpSenWorkTypeRow.FieldNames(i)) AndAlso EmpSenWorkTypeRow.IsUpdated(EmpSenWorkTypeRow.FieldNames(i)) AndAlso EmpSenWorkTypeRow.CreateUpdateSQL(EmpSenWorkTypeRow.FieldNames(i)) Then
                    strSQL.AppendLine(string.Format("{0}{1} = @{1}", strDot, EmpSenWorkTypeRow.FieldNames(i)))
                    strDot = ","
                End If
            Next
            If strDot = String.Empty Then Throw New Exception("未異動資料欄位！")
            strSQL.AppendLine("Where CompID = @PKCompID")
            strSQL.AppendLine("And Reason = @PKReason")
            strSQL.AppendLine("And EmpID = @PKEmpID")
            strSQL.AppendLine("And WorkTypeID = @PKWorkTypeID")
            strSQL.AppendLine("And ValidDateB = @PKValidDateB")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            If EmpSenWorkTypeRow.rowid.Updated Then db.AddInParameter(dbcmd, "@rowid", DbType.Int32, EmpSenWorkTypeRow.rowid.Value)
            If EmpSenWorkTypeRow.Seq.Updated Then db.AddInParameter(dbcmd, "@Seq", DbType.Int32, EmpSenWorkTypeRow.Seq.Value)
            If EmpSenWorkTypeRow.CompID.Updated Then db.AddInParameter(dbcmd, "@CompID", DbType.String, EmpSenWorkTypeRow.CompID.Value)
            If EmpSenWorkTypeRow.PreCompID.Updated Then db.AddInParameter(dbcmd, "@PreCompID", DbType.String, EmpSenWorkTypeRow.PreCompID.Value)
            If EmpSenWorkTypeRow.Reason.Updated Then db.AddInParameter(dbcmd, "@Reason", DbType.String, EmpSenWorkTypeRow.Reason.Value)
            If EmpSenWorkTypeRow.EmpID.Updated Then db.AddInParameter(dbcmd, "@EmpID", DbType.String, EmpSenWorkTypeRow.EmpID.Value)
            If EmpSenWorkTypeRow.IDNo.Updated Then db.AddInParameter(dbcmd, "@IDNo", DbType.String, EmpSenWorkTypeRow.IDNo.Value)
            If EmpSenWorkTypeRow.WorkTypeID.Updated Then db.AddInParameter(dbcmd, "@WorkTypeID", DbType.String, EmpSenWorkTypeRow.WorkTypeID.Value)
            If EmpSenWorkTypeRow.WorkType.Updated Then db.AddInParameter(dbcmd, "@WorkType", DbType.String, EmpSenWorkTypeRow.WorkType.Value)
            If EmpSenWorkTypeRow.ValidDateB.Updated Then db.AddInParameter(dbcmd, "@ValidDateB", DbType.Date, IIf(IsDateTimeNull(EmpSenWorkTypeRow.ValidDateB.Value), Convert.ToDateTime("1900/1/1"), EmpSenWorkTypeRow.ValidDateB.Value))
            If EmpSenWorkTypeRow.ValidDateE.Updated Then db.AddInParameter(dbcmd, "@ValidDateE", DbType.Date, IIf(IsDateTimeNull(EmpSenWorkTypeRow.ValidDateE.Value), Convert.ToDateTime("1900/1/1"), EmpSenWorkTypeRow.ValidDateE.Value))
            If EmpSenWorkTypeRow.Days.Updated Then db.AddInParameter(dbcmd, "@Days", DbType.Decimal, EmpSenWorkTypeRow.Days.Value)
            If EmpSenWorkTypeRow.ConFlag.Updated Then db.AddInParameter(dbcmd, "@ConFlag", DbType.String, EmpSenWorkTypeRow.ConFlag.Value)
            If EmpSenWorkTypeRow.CurrentSen.Updated Then db.AddInParameter(dbcmd, "@CurrentSen", DbType.Decimal, EmpSenWorkTypeRow.CurrentSen.Value)
            If EmpSenWorkTypeRow.ConSen.Updated Then db.AddInParameter(dbcmd, "@ConSen", DbType.Decimal, EmpSenWorkTypeRow.ConSen.Value)
            If EmpSenWorkTypeRow.TotSen.Updated Then db.AddInParameter(dbcmd, "@TotSen", DbType.Decimal, EmpSenWorkTypeRow.TotSen.Value)
            If EmpSenWorkTypeRow.CategoryI.Updated Then db.AddInParameter(dbcmd, "@CategoryI", DbType.String, EmpSenWorkTypeRow.CategoryI.Value)
            If EmpSenWorkTypeRow.TotCategoryI.Updated Then db.AddInParameter(dbcmd, "@TotCategoryI", DbType.Decimal, EmpSenWorkTypeRow.TotCategoryI.Value)
            If EmpSenWorkTypeRow.CategoryII.Updated Then db.AddInParameter(dbcmd, "@CategoryII", DbType.String, EmpSenWorkTypeRow.CategoryII.Value)
            If EmpSenWorkTypeRow.TotCategoryII.Updated Then db.AddInParameter(dbcmd, "@TotCategoryII", DbType.Decimal, EmpSenWorkTypeRow.TotCategoryII.Value)
            If EmpSenWorkTypeRow.CategoryIII.Updated Then db.AddInParameter(dbcmd, "@CategoryIII", DbType.String, EmpSenWorkTypeRow.CategoryIII.Value)
            If EmpSenWorkTypeRow.TotCategoryIII.Updated Then db.AddInParameter(dbcmd, "@TotCategoryIII", DbType.Decimal, EmpSenWorkTypeRow.TotCategoryIII.Value)
            If EmpSenWorkTypeRow.LastChgComp.Updated Then db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, EmpSenWorkTypeRow.LastChgComp.Value)
            If EmpSenWorkTypeRow.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, EmpSenWorkTypeRow.LastChgID.Value)
            If EmpSenWorkTypeRow.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(EmpSenWorkTypeRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), EmpSenWorkTypeRow.LastChgDate.Value))
            db.AddInParameter(dbcmd, "@PKCompID", DbType.String, IIf(EmpSenWorkTypeRow.LoadFromDataRow, EmpSenWorkTypeRow.CompID.OldValue, EmpSenWorkTypeRow.CompID.Value))
            db.AddInParameter(dbcmd, "@PKReason", DbType.String, IIf(EmpSenWorkTypeRow.LoadFromDataRow, EmpSenWorkTypeRow.Reason.OldValue, EmpSenWorkTypeRow.Reason.Value))
            db.AddInParameter(dbcmd, "@PKEmpID", DbType.String, IIf(EmpSenWorkTypeRow.LoadFromDataRow, EmpSenWorkTypeRow.EmpID.OldValue, EmpSenWorkTypeRow.EmpID.Value))
            db.AddInParameter(dbcmd, "@PKWorkTypeID", DbType.String, IIf(EmpSenWorkTypeRow.LoadFromDataRow, EmpSenWorkTypeRow.WorkTypeID.OldValue, EmpSenWorkTypeRow.WorkTypeID.Value))
            db.AddInParameter(dbcmd, "@PKValidDateB", DbType.Date, IIf(EmpSenWorkTypeRow.LoadFromDataRow, EmpSenWorkTypeRow.ValidDateB.OldValue, EmpSenWorkTypeRow.ValidDateB.Value))

            Return db.ExecuteNonQuery(dbcmd)
        End Function

        Public Function Update(ByVal EmpSenWorkTypeRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim strDot As String = String.Empty

            strSQL.AppendLine("Update EmpSenWorkType Set")
            For i As Integer = 0 To EmpSenWorkTypeRow.FieldNames.Length - 1
                If Not EmpSenWorkTypeRow.IsIdentityField(EmpSenWorkTypeRow.FieldNames(i)) AndAlso EmpSenWorkTypeRow.IsUpdated(EmpSenWorkTypeRow.FieldNames(i)) AndAlso EmpSenWorkTypeRow.CreateUpdateSQL(EmpSenWorkTypeRow.FieldNames(i)) Then
                    strSQL.AppendLine(string.Format("{0}{1} = @{1}", strDot, EmpSenWorkTypeRow.FieldNames(i)))
                    strDot = ","
                End If
            Next 
            If strDot = String.Empty Then Throw New Exception("未異動資料欄位！")
            strSQL.AppendLine("Where CompID = @PKCompID")
            strSQL.AppendLine("And Reason = @PKReason")
            strSQL.AppendLine("And EmpID = @PKEmpID")
            strSQL.AppendLine("And WorkTypeID = @PKWorkTypeID")
            strSQL.AppendLine("And ValidDateB = @PKValidDateB")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            If EmpSenWorkTypeRow.rowid.Updated Then db.AddInParameter(dbcmd, "@rowid", DbType.Int32, EmpSenWorkTypeRow.rowid.Value)
            If EmpSenWorkTypeRow.Seq.Updated Then db.AddInParameter(dbcmd, "@Seq", DbType.Int32, EmpSenWorkTypeRow.Seq.Value)
            If EmpSenWorkTypeRow.CompID.Updated Then db.AddInParameter(dbcmd, "@CompID", DbType.String, EmpSenWorkTypeRow.CompID.Value)
            If EmpSenWorkTypeRow.PreCompID.Updated Then db.AddInParameter(dbcmd, "@PreCompID", DbType.String, EmpSenWorkTypeRow.PreCompID.Value)
            If EmpSenWorkTypeRow.Reason.Updated Then db.AddInParameter(dbcmd, "@Reason", DbType.String, EmpSenWorkTypeRow.Reason.Value)
            If EmpSenWorkTypeRow.EmpID.Updated Then db.AddInParameter(dbcmd, "@EmpID", DbType.String, EmpSenWorkTypeRow.EmpID.Value)
            If EmpSenWorkTypeRow.IDNo.Updated Then db.AddInParameter(dbcmd, "@IDNo", DbType.String, EmpSenWorkTypeRow.IDNo.Value)
            If EmpSenWorkTypeRow.WorkTypeID.Updated Then db.AddInParameter(dbcmd, "@WorkTypeID", DbType.String, EmpSenWorkTypeRow.WorkTypeID.Value)
            If EmpSenWorkTypeRow.WorkType.Updated Then db.AddInParameter(dbcmd, "@WorkType", DbType.String, EmpSenWorkTypeRow.WorkType.Value)
            If EmpSenWorkTypeRow.ValidDateB.Updated Then db.AddInParameter(dbcmd, "@ValidDateB", DbType.Date, IIf(IsDateTimeNull(EmpSenWorkTypeRow.ValidDateB.Value), Convert.ToDateTime("1900/1/1"), EmpSenWorkTypeRow.ValidDateB.Value))
            If EmpSenWorkTypeRow.ValidDateE.Updated Then db.AddInParameter(dbcmd, "@ValidDateE", DbType.Date, IIf(IsDateTimeNull(EmpSenWorkTypeRow.ValidDateE.Value), Convert.ToDateTime("1900/1/1"), EmpSenWorkTypeRow.ValidDateE.Value))
            If EmpSenWorkTypeRow.Days.Updated Then db.AddInParameter(dbcmd, "@Days", DbType.Decimal, EmpSenWorkTypeRow.Days.Value)
            If EmpSenWorkTypeRow.ConFlag.Updated Then db.AddInParameter(dbcmd, "@ConFlag", DbType.String, EmpSenWorkTypeRow.ConFlag.Value)
            If EmpSenWorkTypeRow.CurrentSen.Updated Then db.AddInParameter(dbcmd, "@CurrentSen", DbType.Decimal, EmpSenWorkTypeRow.CurrentSen.Value)
            If EmpSenWorkTypeRow.ConSen.Updated Then db.AddInParameter(dbcmd, "@ConSen", DbType.Decimal, EmpSenWorkTypeRow.ConSen.Value)
            If EmpSenWorkTypeRow.TotSen.Updated Then db.AddInParameter(dbcmd, "@TotSen", DbType.Decimal, EmpSenWorkTypeRow.TotSen.Value)
            If EmpSenWorkTypeRow.CategoryI.Updated Then db.AddInParameter(dbcmd, "@CategoryI", DbType.String, EmpSenWorkTypeRow.CategoryI.Value)
            If EmpSenWorkTypeRow.TotCategoryI.Updated Then db.AddInParameter(dbcmd, "@TotCategoryI", DbType.Decimal, EmpSenWorkTypeRow.TotCategoryI.Value)
            If EmpSenWorkTypeRow.CategoryII.Updated Then db.AddInParameter(dbcmd, "@CategoryII", DbType.String, EmpSenWorkTypeRow.CategoryII.Value)
            If EmpSenWorkTypeRow.TotCategoryII.Updated Then db.AddInParameter(dbcmd, "@TotCategoryII", DbType.Decimal, EmpSenWorkTypeRow.TotCategoryII.Value)
            If EmpSenWorkTypeRow.CategoryIII.Updated Then db.AddInParameter(dbcmd, "@CategoryIII", DbType.String, EmpSenWorkTypeRow.CategoryIII.Value)
            If EmpSenWorkTypeRow.TotCategoryIII.Updated Then db.AddInParameter(dbcmd, "@TotCategoryIII", DbType.Decimal, EmpSenWorkTypeRow.TotCategoryIII.Value)
            If EmpSenWorkTypeRow.LastChgComp.Updated Then db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, EmpSenWorkTypeRow.LastChgComp.Value)
            If EmpSenWorkTypeRow.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, EmpSenWorkTypeRow.LastChgID.Value)
            If EmpSenWorkTypeRow.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(EmpSenWorkTypeRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), EmpSenWorkTypeRow.LastChgDate.Value))
            db.AddInParameter(dbcmd, "@PKCompID", DbType.String, IIf(EmpSenWorkTypeRow.LoadFromDataRow, EmpSenWorkTypeRow.CompID.OldValue, EmpSenWorkTypeRow.CompID.Value))
            db.AddInParameter(dbcmd, "@PKReason", DbType.String, IIf(EmpSenWorkTypeRow.LoadFromDataRow, EmpSenWorkTypeRow.Reason.OldValue, EmpSenWorkTypeRow.Reason.Value))
            db.AddInParameter(dbcmd, "@PKEmpID", DbType.String, IIf(EmpSenWorkTypeRow.LoadFromDataRow, EmpSenWorkTypeRow.EmpID.OldValue, EmpSenWorkTypeRow.EmpID.Value))
            db.AddInParameter(dbcmd, "@PKWorkTypeID", DbType.String, IIf(EmpSenWorkTypeRow.LoadFromDataRow, EmpSenWorkTypeRow.WorkTypeID.OldValue, EmpSenWorkTypeRow.WorkTypeID.Value))
            db.AddInParameter(dbcmd, "@PKValidDateB", DbType.Date, IIf(EmpSenWorkTypeRow.LoadFromDataRow, EmpSenWorkTypeRow.ValidDateB.OldValue, EmpSenWorkTypeRow.ValidDateB.Value))

            Return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function Update(ByVal EmpSenWorkTypeRow As Row()) As Integer
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
                    For Each r As Row In EmpSenWorkTypeRow
                        strDot = String.Empty
                        If strSQL.Length > 0 Then strSQL.Remove(0, strSQL.Length)
                        strSQL.AppendLine("Update EmpSenWorkType Set")
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
                        strSQL.AppendLine("And WorkTypeID = @PKWorkTypeID")
                        strSQL.AppendLine("And ValidDateB = @PKValidDateB")

                        dbcmd = db.GetSqlStringCommand(strSQL.ToString())
                        If r.rowid.Updated Then db.AddInParameter(dbcmd, "@rowid", DbType.Int32, r.rowid.Value)
                        If r.Seq.Updated Then db.AddInParameter(dbcmd, "@Seq", DbType.Int32, r.Seq.Value)
                        If r.CompID.Updated Then db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                        If r.PreCompID.Updated Then db.AddInParameter(dbcmd, "@PreCompID", DbType.String, r.PreCompID.Value)
                        If r.Reason.Updated Then db.AddInParameter(dbcmd, "@Reason", DbType.String, r.Reason.Value)
                        If r.EmpID.Updated Then db.AddInParameter(dbcmd, "@EmpID", DbType.String, r.EmpID.Value)
                        If r.IDNo.Updated Then db.AddInParameter(dbcmd, "@IDNo", DbType.String, r.IDNo.Value)
                        If r.WorkTypeID.Updated Then db.AddInParameter(dbcmd, "@WorkTypeID", DbType.String, r.WorkTypeID.Value)
                        If r.WorkType.Updated Then db.AddInParameter(dbcmd, "@WorkType", DbType.String, r.WorkType.Value)
                        If r.ValidDateB.Updated Then db.AddInParameter(dbcmd, "@ValidDateB", DbType.Date, IIf(IsDateTimeNull(r.ValidDateB.Value), Convert.ToDateTime("1900/1/1"), r.ValidDateB.Value))
                        If r.ValidDateE.Updated Then db.AddInParameter(dbcmd, "@ValidDateE", DbType.Date, IIf(IsDateTimeNull(r.ValidDateE.Value), Convert.ToDateTime("1900/1/1"), r.ValidDateE.Value))
                        If r.Days.Updated Then db.AddInParameter(dbcmd, "@Days", DbType.Decimal, r.Days.Value)
                        If r.ConFlag.Updated Then db.AddInParameter(dbcmd, "@ConFlag", DbType.String, r.ConFlag.Value)
                        If r.CurrentSen.Updated Then db.AddInParameter(dbcmd, "@CurrentSen", DbType.Decimal, r.CurrentSen.Value)
                        If r.ConSen.Updated Then db.AddInParameter(dbcmd, "@ConSen", DbType.Decimal, r.ConSen.Value)
                        If r.TotSen.Updated Then db.AddInParameter(dbcmd, "@TotSen", DbType.Decimal, r.TotSen.Value)
                        If r.CategoryI.Updated Then db.AddInParameter(dbcmd, "@CategoryI", DbType.String, r.CategoryI.Value)
                        If r.TotCategoryI.Updated Then db.AddInParameter(dbcmd, "@TotCategoryI", DbType.Decimal, r.TotCategoryI.Value)
                        If r.CategoryII.Updated Then db.AddInParameter(dbcmd, "@CategoryII", DbType.String, r.CategoryII.Value)
                        If r.TotCategoryII.Updated Then db.AddInParameter(dbcmd, "@TotCategoryII", DbType.Decimal, r.TotCategoryII.Value)
                        If r.CategoryIII.Updated Then db.AddInParameter(dbcmd, "@CategoryIII", DbType.String, r.CategoryIII.Value)
                        If r.TotCategoryIII.Updated Then db.AddInParameter(dbcmd, "@TotCategoryIII", DbType.Decimal, r.TotCategoryIII.Value)
                        If r.LastChgComp.Updated Then db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, r.LastChgComp.Value)
                        If r.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, r.LastChgID.Value)
                        If r.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(r.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), r.LastChgDate.Value))
                        db.AddInParameter(dbcmd, "@PKCompID", DbType.String, IIf(r.LoadFromDataRow, r.CompID.OldValue, r.CompID.Value))
                        db.AddInParameter(dbcmd, "@PKReason", DbType.String, IIf(r.LoadFromDataRow, r.Reason.OldValue, r.Reason.Value))
                        db.AddInParameter(dbcmd, "@PKEmpID", DbType.String, IIf(r.LoadFromDataRow, r.EmpID.OldValue, r.EmpID.Value))
                        db.AddInParameter(dbcmd, "@PKWorkTypeID", DbType.String, IIf(r.LoadFromDataRow, r.WorkTypeID.OldValue, r.WorkTypeID.Value))
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

        Public Function Update(ByVal EmpSenWorkTypeRow As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0
            Dim strDot As String

            For Each r As Row In EmpSenWorkTypeRow
                strDot = String.Empty
                If strSQL.Length > 0 Then strSQL.Remove(0, strSQL.Length)
                strSQL.AppendLine("Update EmpSenWorkType Set")
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
                strSQL.AppendLine("And WorkTypeID = @PKWorkTypeID")
                strSQL.AppendLine("And ValidDateB = @PKValidDateB")

                dbcmd = db.GetSqlStringCommand(strSQL.ToString())
                If r.rowid.Updated Then db.AddInParameter(dbcmd, "@rowid", DbType.Int32, r.rowid.Value)
                If r.Seq.Updated Then db.AddInParameter(dbcmd, "@Seq", DbType.Int32, r.Seq.Value)
                If r.CompID.Updated Then db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                If r.PreCompID.Updated Then db.AddInParameter(dbcmd, "@PreCompID", DbType.String, r.PreCompID.Value)
                If r.Reason.Updated Then db.AddInParameter(dbcmd, "@Reason", DbType.String, r.Reason.Value)
                If r.EmpID.Updated Then db.AddInParameter(dbcmd, "@EmpID", DbType.String, r.EmpID.Value)
                If r.IDNo.Updated Then db.AddInParameter(dbcmd, "@IDNo", DbType.String, r.IDNo.Value)
                If r.WorkTypeID.Updated Then db.AddInParameter(dbcmd, "@WorkTypeID", DbType.String, r.WorkTypeID.Value)
                If r.WorkType.Updated Then db.AddInParameter(dbcmd, "@WorkType", DbType.String, r.WorkType.Value)
                If r.ValidDateB.Updated Then db.AddInParameter(dbcmd, "@ValidDateB", DbType.Date, IIf(IsDateTimeNull(r.ValidDateB.Value), Convert.ToDateTime("1900/1/1"), r.ValidDateB.Value))
                If r.ValidDateE.Updated Then db.AddInParameter(dbcmd, "@ValidDateE", DbType.Date, IIf(IsDateTimeNull(r.ValidDateE.Value), Convert.ToDateTime("1900/1/1"), r.ValidDateE.Value))
                If r.Days.Updated Then db.AddInParameter(dbcmd, "@Days", DbType.Decimal, r.Days.Value)
                If r.ConFlag.Updated Then db.AddInParameter(dbcmd, "@ConFlag", DbType.String, r.ConFlag.Value)
                If r.CurrentSen.Updated Then db.AddInParameter(dbcmd, "@CurrentSen", DbType.Decimal, r.CurrentSen.Value)
                If r.ConSen.Updated Then db.AddInParameter(dbcmd, "@ConSen", DbType.Decimal, r.ConSen.Value)
                If r.TotSen.Updated Then db.AddInParameter(dbcmd, "@TotSen", DbType.Decimal, r.TotSen.Value)
                If r.CategoryI.Updated Then db.AddInParameter(dbcmd, "@CategoryI", DbType.String, r.CategoryI.Value)
                If r.TotCategoryI.Updated Then db.AddInParameter(dbcmd, "@TotCategoryI", DbType.Decimal, r.TotCategoryI.Value)
                If r.CategoryII.Updated Then db.AddInParameter(dbcmd, "@CategoryII", DbType.String, r.CategoryII.Value)
                If r.TotCategoryII.Updated Then db.AddInParameter(dbcmd, "@TotCategoryII", DbType.Decimal, r.TotCategoryII.Value)
                If r.CategoryIII.Updated Then db.AddInParameter(dbcmd, "@CategoryIII", DbType.String, r.CategoryIII.Value)
                If r.TotCategoryIII.Updated Then db.AddInParameter(dbcmd, "@TotCategoryIII", DbType.Decimal, r.TotCategoryIII.Value)
                If r.LastChgComp.Updated Then db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, r.LastChgComp.Value)
                If r.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, r.LastChgID.Value)
                If r.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(r.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), r.LastChgDate.Value))
                db.AddInParameter(dbcmd, "@PKCompID", DbType.String, IIf(r.LoadFromDataRow, r.CompID.OldValue, r.CompID.Value))
                db.AddInParameter(dbcmd, "@PKReason", DbType.String, IIf(r.LoadFromDataRow, r.Reason.OldValue, r.Reason.Value))
                db.AddInParameter(dbcmd, "@PKEmpID", DbType.String, IIf(r.LoadFromDataRow, r.EmpID.OldValue, r.EmpID.Value))
                db.AddInParameter(dbcmd, "@PKWorkTypeID", DbType.String, IIf(r.LoadFromDataRow, r.WorkTypeID.OldValue, r.WorkTypeID.Value))
                db.AddInParameter(dbcmd, "@PKValidDateB", DbType.Date, IIf(r.LoadFromDataRow, r.ValidDateB.OldValue, r.ValidDateB.Value))

                intRowsAffected += db.ExecuteNonQuery(dbcmd, tran)
            Next
            Return intRowsAffected
        End Function

        Public Function IsDataExists(ByVal EmpSenWorkTypeRow As Row) As Boolean
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intCount As Integer = 0

            strSQL.AppendLine("Select Count(*) Cnt From EmpSenWorkType")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And Reason = @Reason")
            strSQL.AppendLine("And EmpID = @EmpID")
            strSQL.AppendLine("And WorkTypeID = @WorkTypeID")
            strSQL.AppendLine("And ValidDateB = @ValidDateB")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, EmpSenWorkTypeRow.CompID.Value)
            db.AddInParameter(dbcmd, "@Reason", DbType.String, EmpSenWorkTypeRow.Reason.Value)
            db.AddInParameter(dbcmd, "@EmpID", DbType.String, EmpSenWorkTypeRow.EmpID.Value)
            db.AddInParameter(dbcmd, "@WorkTypeID", DbType.String, EmpSenWorkTypeRow.WorkTypeID.Value)
            db.AddInParameter(dbcmd, "@ValidDateB", DbType.Date, EmpSenWorkTypeRow.ValidDateB.Value)

            intCount = Convert.ToInt32(db.ExecuteScalar(dbcmd))
            Return IIf(intCount > 0, True, False)
        End Function

        Public Function IsDataExists(ByVal EmpSenWorkTypeRow As Row, ByVal tran As DbTransaction) As Boolean
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intCount As Integer = 0

            strSQL.AppendLine("Select Count(*) Cnt From EmpSenWorkType")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And Reason = @Reason")
            strSQL.AppendLine("And EmpID = @EmpID")
            strSQL.AppendLine("And WorkTypeID = @WorkTypeID")
            strSQL.AppendLine("And ValidDateB = @ValidDateB")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, EmpSenWorkTypeRow.CompID.Value)
            db.AddInParameter(dbcmd, "@Reason", DbType.String, EmpSenWorkTypeRow.Reason.Value)
            db.AddInParameter(dbcmd, "@EmpID", DbType.String, EmpSenWorkTypeRow.EmpID.Value)
            db.AddInParameter(dbcmd, "@WorkTypeID", DbType.String, EmpSenWorkTypeRow.WorkTypeID.Value)
            db.AddInParameter(dbcmd, "@ValidDateB", DbType.Date, EmpSenWorkTypeRow.ValidDateB.Value)

            intCount = Convert.ToInt32(db.ExecuteScalar(dbcmd, tran))
            Return IIf(intCount > 0, True, False)
        End Function

        Public Function QuerybyWhere(WhereCondition As String) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From EmpSenWorkType")
            If WhereCondition <> String.Empty Then strSQL.AppendLine(WhereCondition)

            Return db.ExecuteDataSet(CommandType.Text, strSQL.ToString())
        End Function

        Public Function Insert(ByVal EmpSenWorkTypeRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Insert into EmpSenWorkType")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    rowid, Seq, CompID, PreCompID, Reason, EmpID, IDNo, WorkTypeID, WorkType, ValidDateB,")
            strSQL.AppendLine("    ValidDateE, Days, ConFlag, CurrentSen, ConSen, TotSen, CategoryI, TotCategoryI, CategoryII,")
            strSQL.AppendLine("    TotCategoryII, CategoryIII, TotCategoryIII, LastChgComp, LastChgID, LastChgDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @rowid, @Seq, @CompID, @PreCompID, @Reason, @EmpID, @IDNo, @WorkTypeID, @WorkType, @ValidDateB,")
            strSQL.AppendLine("    @ValidDateE, @Days, @ConFlag, @CurrentSen, @ConSen, @TotSen, @CategoryI, @TotCategoryI, @CategoryII,")
            strSQL.AppendLine("    @TotCategoryII, @CategoryIII, @TotCategoryIII, @LastChgComp, @LastChgID, @LastChgDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@rowid", DbType.Int32, EmpSenWorkTypeRow.rowid.Value)
            db.AddInParameter(dbcmd, "@Seq", DbType.Int32, EmpSenWorkTypeRow.Seq.Value)
            db.AddInParameter(dbcmd, "@CompID", DbType.String, EmpSenWorkTypeRow.CompID.Value)
            db.AddInParameter(dbcmd, "@PreCompID", DbType.String, EmpSenWorkTypeRow.PreCompID.Value)
            db.AddInParameter(dbcmd, "@Reason", DbType.String, EmpSenWorkTypeRow.Reason.Value)
            db.AddInParameter(dbcmd, "@EmpID", DbType.String, EmpSenWorkTypeRow.EmpID.Value)
            db.AddInParameter(dbcmd, "@IDNo", DbType.String, EmpSenWorkTypeRow.IDNo.Value)
            db.AddInParameter(dbcmd, "@WorkTypeID", DbType.String, EmpSenWorkTypeRow.WorkTypeID.Value)
            db.AddInParameter(dbcmd, "@WorkType", DbType.String, EmpSenWorkTypeRow.WorkType.Value)
            db.AddInParameter(dbcmd, "@ValidDateB", DbType.Date, IIf(IsDateTimeNull(EmpSenWorkTypeRow.ValidDateB.Value), Convert.ToDateTime("1900/1/1"), EmpSenWorkTypeRow.ValidDateB.Value))
            db.AddInParameter(dbcmd, "@ValidDateE", DbType.Date, IIf(IsDateTimeNull(EmpSenWorkTypeRow.ValidDateE.Value), Convert.ToDateTime("1900/1/1"), EmpSenWorkTypeRow.ValidDateE.Value))
            db.AddInParameter(dbcmd, "@Days", DbType.Decimal, EmpSenWorkTypeRow.Days.Value)
            db.AddInParameter(dbcmd, "@ConFlag", DbType.String, EmpSenWorkTypeRow.ConFlag.Value)
            db.AddInParameter(dbcmd, "@CurrentSen", DbType.Decimal, EmpSenWorkTypeRow.CurrentSen.Value)
            db.AddInParameter(dbcmd, "@ConSen", DbType.Decimal, EmpSenWorkTypeRow.ConSen.Value)
            db.AddInParameter(dbcmd, "@TotSen", DbType.Decimal, EmpSenWorkTypeRow.TotSen.Value)
            db.AddInParameter(dbcmd, "@CategoryI", DbType.String, EmpSenWorkTypeRow.CategoryI.Value)
            db.AddInParameter(dbcmd, "@TotCategoryI", DbType.Decimal, EmpSenWorkTypeRow.TotCategoryI.Value)
            db.AddInParameter(dbcmd, "@CategoryII", DbType.String, EmpSenWorkTypeRow.CategoryII.Value)
            db.AddInParameter(dbcmd, "@TotCategoryII", DbType.Decimal, EmpSenWorkTypeRow.TotCategoryII.Value)
            db.AddInParameter(dbcmd, "@CategoryIII", DbType.String, EmpSenWorkTypeRow.CategoryIII.Value)
            db.AddInParameter(dbcmd, "@TotCategoryIII", DbType.Decimal, EmpSenWorkTypeRow.TotCategoryIII.Value)
            db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, EmpSenWorkTypeRow.LastChgComp.Value)
            db.AddInParameter(dbcmd, "@LastChgID", DbType.String, EmpSenWorkTypeRow.LastChgID.Value)
            db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(EmpSenWorkTypeRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), EmpSenWorkTypeRow.LastChgDate.Value))

            Return db.ExecuteNonQuery(dbcmd)
        End Function

        Public Function Insert(ByVal EmpSenWorkTypeRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Insert into EmpSenWorkType")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    rowid, Seq, CompID, PreCompID, Reason, EmpID, IDNo, WorkTypeID, WorkType, ValidDateB,")
            strSQL.AppendLine("    ValidDateE, Days, ConFlag, CurrentSen, ConSen, TotSen, CategoryI, TotCategoryI, CategoryII,")
            strSQL.AppendLine("    TotCategoryII, CategoryIII, TotCategoryIII, LastChgComp, LastChgID, LastChgDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @rowid, @Seq, @CompID, @PreCompID, @Reason, @EmpID, @IDNo, @WorkTypeID, @WorkType, @ValidDateB,")
            strSQL.AppendLine("    @ValidDateE, @Days, @ConFlag, @CurrentSen, @ConSen, @TotSen, @CategoryI, @TotCategoryI, @CategoryII,")
            strSQL.AppendLine("    @TotCategoryII, @CategoryIII, @TotCategoryIII, @LastChgComp, @LastChgID, @LastChgDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@rowid", DbType.Int32, EmpSenWorkTypeRow.rowid.Value)
            db.AddInParameter(dbcmd, "@Seq", DbType.Int32, EmpSenWorkTypeRow.Seq.Value)
            db.AddInParameter(dbcmd, "@CompID", DbType.String, EmpSenWorkTypeRow.CompID.Value)
            db.AddInParameter(dbcmd, "@PreCompID", DbType.String, EmpSenWorkTypeRow.PreCompID.Value)
            db.AddInParameter(dbcmd, "@Reason", DbType.String, EmpSenWorkTypeRow.Reason.Value)
            db.AddInParameter(dbcmd, "@EmpID", DbType.String, EmpSenWorkTypeRow.EmpID.Value)
            db.AddInParameter(dbcmd, "@IDNo", DbType.String, EmpSenWorkTypeRow.IDNo.Value)
            db.AddInParameter(dbcmd, "@WorkTypeID", DbType.String, EmpSenWorkTypeRow.WorkTypeID.Value)
            db.AddInParameter(dbcmd, "@WorkType", DbType.String, EmpSenWorkTypeRow.WorkType.Value)
            db.AddInParameter(dbcmd, "@ValidDateB", DbType.Date, IIf(IsDateTimeNull(EmpSenWorkTypeRow.ValidDateB.Value), Convert.ToDateTime("1900/1/1"), EmpSenWorkTypeRow.ValidDateB.Value))
            db.AddInParameter(dbcmd, "@ValidDateE", DbType.Date, IIf(IsDateTimeNull(EmpSenWorkTypeRow.ValidDateE.Value), Convert.ToDateTime("1900/1/1"), EmpSenWorkTypeRow.ValidDateE.Value))
            db.AddInParameter(dbcmd, "@Days", DbType.Decimal, EmpSenWorkTypeRow.Days.Value)
            db.AddInParameter(dbcmd, "@ConFlag", DbType.String, EmpSenWorkTypeRow.ConFlag.Value)
            db.AddInParameter(dbcmd, "@CurrentSen", DbType.Decimal, EmpSenWorkTypeRow.CurrentSen.Value)
            db.AddInParameter(dbcmd, "@ConSen", DbType.Decimal, EmpSenWorkTypeRow.ConSen.Value)
            db.AddInParameter(dbcmd, "@TotSen", DbType.Decimal, EmpSenWorkTypeRow.TotSen.Value)
            db.AddInParameter(dbcmd, "@CategoryI", DbType.String, EmpSenWorkTypeRow.CategoryI.Value)
            db.AddInParameter(dbcmd, "@TotCategoryI", DbType.Decimal, EmpSenWorkTypeRow.TotCategoryI.Value)
            db.AddInParameter(dbcmd, "@CategoryII", DbType.String, EmpSenWorkTypeRow.CategoryII.Value)
            db.AddInParameter(dbcmd, "@TotCategoryII", DbType.Decimal, EmpSenWorkTypeRow.TotCategoryII.Value)
            db.AddInParameter(dbcmd, "@CategoryIII", DbType.String, EmpSenWorkTypeRow.CategoryIII.Value)
            db.AddInParameter(dbcmd, "@TotCategoryIII", DbType.Decimal, EmpSenWorkTypeRow.TotCategoryIII.Value)
            db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, EmpSenWorkTypeRow.LastChgComp.Value)
            db.AddInParameter(dbcmd, "@LastChgID", DbType.String, EmpSenWorkTypeRow.LastChgID.Value)
            db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(EmpSenWorkTypeRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), EmpSenWorkTypeRow.LastChgDate.Value))

            Return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function Insert(ByVal EmpSenWorkTypeRow As Row()) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Insert into EmpSenWorkType")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    rowid, Seq, CompID, PreCompID, Reason, EmpID, IDNo, WorkTypeID, WorkType, ValidDateB,")
            strSQL.AppendLine("    ValidDateE, Days, ConFlag, CurrentSen, ConSen, TotSen, CategoryI, TotCategoryI, CategoryII,")
            strSQL.AppendLine("    TotCategoryII, CategoryIII, TotCategoryIII, LastChgComp, LastChgID, LastChgDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @rowid, @Seq, @CompID, @PreCompID, @Reason, @EmpID, @IDNo, @WorkTypeID, @WorkType, @ValidDateB,")
            strSQL.AppendLine("    @ValidDateE, @Days, @ConFlag, @CurrentSen, @ConSen, @TotSen, @CategoryI, @TotCategoryI, @CategoryII,")
            strSQL.AppendLine("    @TotCategoryII, @CategoryIII, @TotCategoryIII, @LastChgComp, @LastChgID, @LastChgDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            Using cn As DbConnection = db.CreateConnection()
                cn.Open()
                Dim tran As DbTransaction = cn.BeginTransaction()
                Dim inTrans As Boolean = True

                Try
                    For Each r As Row In EmpSenWorkTypeRow
                        dbcmd.Parameters.Clear()
                        db.AddInParameter(dbcmd, "@rowid", DbType.Int32, r.rowid.Value)
                        db.AddInParameter(dbcmd, "@Seq", DbType.Int32, r.Seq.Value)
                        db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                        db.AddInParameter(dbcmd, "@PreCompID", DbType.String, r.PreCompID.Value)
                        db.AddInParameter(dbcmd, "@Reason", DbType.String, r.Reason.Value)
                        db.AddInParameter(dbcmd, "@EmpID", DbType.String, r.EmpID.Value)
                        db.AddInParameter(dbcmd, "@IDNo", DbType.String, r.IDNo.Value)
                        db.AddInParameter(dbcmd, "@WorkTypeID", DbType.String, r.WorkTypeID.Value)
                        db.AddInParameter(dbcmd, "@WorkType", DbType.String, r.WorkType.Value)
                        db.AddInParameter(dbcmd, "@ValidDateB", DbType.Date, IIf(IsDateTimeNull(r.ValidDateB.Value), Convert.ToDateTime("1900/1/1"), r.ValidDateB.Value))
                        db.AddInParameter(dbcmd, "@ValidDateE", DbType.Date, IIf(IsDateTimeNull(r.ValidDateE.Value), Convert.ToDateTime("1900/1/1"), r.ValidDateE.Value))
                        db.AddInParameter(dbcmd, "@Days", DbType.Decimal, r.Days.Value)
                        db.AddInParameter(dbcmd, "@ConFlag", DbType.String, r.ConFlag.Value)
                        db.AddInParameter(dbcmd, "@CurrentSen", DbType.Decimal, r.CurrentSen.Value)
                        db.AddInParameter(dbcmd, "@ConSen", DbType.Decimal, r.ConSen.Value)
                        db.AddInParameter(dbcmd, "@TotSen", DbType.Decimal, r.TotSen.Value)
                        db.AddInParameter(dbcmd, "@CategoryI", DbType.String, r.CategoryI.Value)
                        db.AddInParameter(dbcmd, "@TotCategoryI", DbType.Decimal, r.TotCategoryI.Value)
                        db.AddInParameter(dbcmd, "@CategoryII", DbType.String, r.CategoryII.Value)
                        db.AddInParameter(dbcmd, "@TotCategoryII", DbType.Decimal, r.TotCategoryII.Value)
                        db.AddInParameter(dbcmd, "@CategoryIII", DbType.String, r.CategoryIII.Value)
                        db.AddInParameter(dbcmd, "@TotCategoryIII", DbType.Decimal, r.TotCategoryIII.Value)
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

        Public Function Insert(ByVal EmpSenWorkTypeRow As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Insert into EmpSenWorkType")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    rowid, Seq, CompID, PreCompID, Reason, EmpID, IDNo, WorkTypeID, WorkType, ValidDateB,")
            strSQL.AppendLine("    ValidDateE, Days, ConFlag, CurrentSen, ConSen, TotSen, CategoryI, TotCategoryI, CategoryII,")
            strSQL.AppendLine("    TotCategoryII, CategoryIII, TotCategoryIII, LastChgComp, LastChgID, LastChgDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @rowid, @Seq, @CompID, @PreCompID, @Reason, @EmpID, @IDNo, @WorkTypeID, @WorkType, @ValidDateB,")
            strSQL.AppendLine("    @ValidDateE, @Days, @ConFlag, @CurrentSen, @ConSen, @TotSen, @CategoryI, @TotCategoryI, @CategoryII,")
            strSQL.AppendLine("    @TotCategoryII, @CategoryIII, @TotCategoryIII, @LastChgComp, @LastChgID, @LastChgDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())

            For Each r As Row In EmpSenWorkTypeRow
                dbcmd.Parameters.Clear()
                db.AddInParameter(dbcmd, "@rowid", DbType.Int32, r.rowid.Value)
                db.AddInParameter(dbcmd, "@Seq", DbType.Int32, r.Seq.Value)
                db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                db.AddInParameter(dbcmd, "@PreCompID", DbType.String, r.PreCompID.Value)
                db.AddInParameter(dbcmd, "@Reason", DbType.String, r.Reason.Value)
                db.AddInParameter(dbcmd, "@EmpID", DbType.String, r.EmpID.Value)
                db.AddInParameter(dbcmd, "@IDNo", DbType.String, r.IDNo.Value)
                db.AddInParameter(dbcmd, "@WorkTypeID", DbType.String, r.WorkTypeID.Value)
                db.AddInParameter(dbcmd, "@WorkType", DbType.String, r.WorkType.Value)
                db.AddInParameter(dbcmd, "@ValidDateB", DbType.Date, IIf(IsDateTimeNull(r.ValidDateB.Value), Convert.ToDateTime("1900/1/1"), r.ValidDateB.Value))
                db.AddInParameter(dbcmd, "@ValidDateE", DbType.Date, IIf(IsDateTimeNull(r.ValidDateE.Value), Convert.ToDateTime("1900/1/1"), r.ValidDateE.Value))
                db.AddInParameter(dbcmd, "@Days", DbType.Decimal, r.Days.Value)
                db.AddInParameter(dbcmd, "@ConFlag", DbType.String, r.ConFlag.Value)
                db.AddInParameter(dbcmd, "@CurrentSen", DbType.Decimal, r.CurrentSen.Value)
                db.AddInParameter(dbcmd, "@ConSen", DbType.Decimal, r.ConSen.Value)
                db.AddInParameter(dbcmd, "@TotSen", DbType.Decimal, r.TotSen.Value)
                db.AddInParameter(dbcmd, "@CategoryI", DbType.String, r.CategoryI.Value)
                db.AddInParameter(dbcmd, "@TotCategoryI", DbType.Decimal, r.TotCategoryI.Value)
                db.AddInParameter(dbcmd, "@CategoryII", DbType.String, r.CategoryII.Value)
                db.AddInParameter(dbcmd, "@TotCategoryII", DbType.Decimal, r.TotCategoryII.Value)
                db.AddInParameter(dbcmd, "@CategoryIII", DbType.String, r.CategoryIII.Value)
                db.AddInParameter(dbcmd, "@TotCategoryIII", DbType.Decimal, r.TotCategoryIII.Value)
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

