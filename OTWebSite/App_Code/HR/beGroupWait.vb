'****************************************************************
' Table:GroupWait
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

Namespace beGroupWait
    Public Class Table
        Private m_Rows As Rows = New Rows()
        Private m_Fields As String() = { "CompID", "WaitType", "WaitDate", "EmpID", "RelativeIDNo", "Source", "Name", "BirthDate", "GrpLvl", "RelativeID" _
                                    , "GrpDate", "MarryDate", "DeathDate", "TransferDate", "NotifyFlag", "WelfareFlag", "GrpUnitID", "Reason", "Remark1", "LastChgComp", "LastChgID" _
                                    , "LastChgDate" }
        Private m_Types As System.Type() = { GetType(String), GetType(String), GetType(Date), GetType(String), GetType(String), GetType(String), GetType(String), GetType(Date), GetType(String), GetType(String) _
                                    , GetType(Date), GetType(Date), GetType(Date), GetType(Date), GetType(String), GetType(String), GetType(Integer), GetType(String), GetType(String), GetType(String), GetType(String) _
                                    , GetType(Date) }
        Private m_PrimaryFields As String() = { "CompID", "WaitType", "WaitDate", "EmpID", "RelativeIDNo" }

        Public ReadOnly Property Rows() As beGroupWait.Rows 
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
        Public Sub Transfer2Row(GroupWaitTable As DataTable)
            For Each dr As DataRow In GroupWaitTable.Rows
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
                dr(m_Rows(i).WaitType.FieldName) = m_Rows(i).WaitType.Value
                dr(m_Rows(i).WaitDate.FieldName) = m_Rows(i).WaitDate.Value
                dr(m_Rows(i).EmpID.FieldName) = m_Rows(i).EmpID.Value
                dr(m_Rows(i).RelativeIDNo.FieldName) = m_Rows(i).RelativeIDNo.Value
                dr(m_Rows(i).Source.FieldName) = m_Rows(i).Source.Value
                dr(m_Rows(i).Name.FieldName) = m_Rows(i).Name.Value
                dr(m_Rows(i).BirthDate.FieldName) = m_Rows(i).BirthDate.Value
                dr(m_Rows(i).GrpLvl.FieldName) = m_Rows(i).GrpLvl.Value
                dr(m_Rows(i).RelativeID.FieldName) = m_Rows(i).RelativeID.Value
                dr(m_Rows(i).GrpDate.FieldName) = m_Rows(i).GrpDate.Value
                dr(m_Rows(i).MarryDate.FieldName) = m_Rows(i).MarryDate.Value
                dr(m_Rows(i).DeathDate.FieldName) = m_Rows(i).DeathDate.Value
                dr(m_Rows(i).TransferDate.FieldName) = m_Rows(i).TransferDate.Value
                dr(m_Rows(i).NotifyFlag.FieldName) = m_Rows(i).NotifyFlag.Value
                dr(m_Rows(i).WelfareFlag.FieldName) = m_Rows(i).WelfareFlag.Value
                dr(m_Rows(i).GrpUnitID.FieldName) = m_Rows(i).GrpUnitID.Value
                dr(m_Rows(i).Reason.FieldName) = m_Rows(i).Reason.Value
                dr(m_Rows(i).Remark1.FieldName) = m_Rows(i).Remark1.Value
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

        Public Sub Add(GroupWaitRow As Row)
            m_Rows.Add(GroupWaitRow)
        End Sub

        Public Sub Remove(GroupWaitRow As Row)
            If m_Rows.IndexOf(GroupWaitRow) >= 0 Then
                m_Rows.Remove(GroupWaitRow)
            End If
        End Sub

        Public Sub Dispose()
            m_Rows.Clear()
        End Sub

    End Class

    Public Class Row

        Private FI_CompID As Field(Of String) = new Field(Of String)("CompID", true)
        Private FI_WaitType As Field(Of String) = new Field(Of String)("WaitType", true)
        Private FI_WaitDate As Field(Of Date) = new Field(Of Date)("WaitDate", true)
        Private FI_EmpID As Field(Of String) = new Field(Of String)("EmpID", true)
        Private FI_RelativeIDNo As Field(Of String) = new Field(Of String)("RelativeIDNo", true)
        Private FI_Source As Field(Of String) = new Field(Of String)("Source", true)
        Private FI_Name As Field(Of String) = new Field(Of String)("Name", true)
        Private FI_BirthDate As Field(Of Date) = new Field(Of Date)("BirthDate", true)
        Private FI_GrpLvl As Field(Of String) = new Field(Of String)("GrpLvl", true)
        Private FI_RelativeID As Field(Of String) = new Field(Of String)("RelativeID", true)
        Private FI_GrpDate As Field(Of Date) = new Field(Of Date)("GrpDate", true)
        Private FI_MarryDate As Field(Of Date) = new Field(Of Date)("MarryDate", true)
        Private FI_DeathDate As Field(Of Date) = new Field(Of Date)("DeathDate", true)
        Private FI_TransferDate As Field(Of Date) = new Field(Of Date)("TransferDate", true)
        Private FI_NotifyFlag As Field(Of String) = new Field(Of String)("NotifyFlag", true)
        Private FI_WelfareFlag As Field(Of String) = new Field(Of String)("WelfareFlag", true)
        Private FI_GrpUnitID As Field(Of Integer) = new Field(Of Integer)("GrpUnitID", true)
        Private FI_Reason As Field(Of String) = new Field(Of String)("Reason", true)
        Private FI_Remark1 As Field(Of String) = new Field(Of String)("Remark1", true)
        Private FI_LastChgComp As Field(Of String) = new Field(Of String)("LastChgComp", true)
        Private FI_LastChgID As Field(Of String) = new Field(Of String)("LastChgID", true)
        Private FI_LastChgDate As Field(Of Date) = new Field(Of Date)("LastChgDate", true)
        Private m_FieldNames As String() = { "CompID", "WaitType", "WaitDate", "EmpID", "RelativeIDNo", "Source", "Name", "BirthDate", "GrpLvl", "RelativeID" _
                                    , "GrpDate", "MarryDate", "DeathDate", "TransferDate", "NotifyFlag", "WelfareFlag", "GrpUnitID", "Reason", "Remark1", "LastChgComp", "LastChgID" _
                                    , "LastChgDate" }
        Private m_PrimaryFields As String() = { "CompID", "WaitType", "WaitDate", "EmpID", "RelativeIDNo" }
        Private m_IdentityFields As String() = {  }
        Private m_LoadFromDataRow As Boolean = False

        Private Function GetFieldValue(ByVal fieldName As String) As Object
            Select Case fieldName
                Case "CompID"
                    Return FI_CompID.Value
                Case "WaitType"
                    Return FI_WaitType.Value
                Case "WaitDate"
                    Return FI_WaitDate.Value
                Case "EmpID"
                    Return FI_EmpID.Value
                Case "RelativeIDNo"
                    Return FI_RelativeIDNo.Value
                Case "Source"
                    Return FI_Source.Value
                Case "Name"
                    Return FI_Name.Value
                Case "BirthDate"
                    Return FI_BirthDate.Value
                Case "GrpLvl"
                    Return FI_GrpLvl.Value
                Case "RelativeID"
                    Return FI_RelativeID.Value
                Case "GrpDate"
                    Return FI_GrpDate.Value
                Case "MarryDate"
                    Return FI_MarryDate.Value
                Case "DeathDate"
                    Return FI_DeathDate.Value
                Case "TransferDate"
                    Return FI_TransferDate.Value
                Case "NotifyFlag"
                    Return FI_NotifyFlag.Value
                Case "WelfareFlag"
                    Return FI_WelfareFlag.Value
                Case "GrpUnitID"
                    Return FI_GrpUnitID.Value
                Case "Reason"
                    Return FI_Reason.Value
                Case "Remark1"
                    Return FI_Remark1.Value
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
                Case "WaitType"
                    FI_WaitType.SetValue(value)
                Case "WaitDate"
                    FI_WaitDate.SetValue(value)
                Case "EmpID"
                    FI_EmpID.SetValue(value)
                Case "RelativeIDNo"
                    FI_RelativeIDNo.SetValue(value)
                Case "Source"
                    FI_Source.SetValue(value)
                Case "Name"
                    FI_Name.SetValue(value)
                Case "BirthDate"
                    FI_BirthDate.SetValue(value)
                Case "GrpLvl"
                    FI_GrpLvl.SetValue(value)
                Case "RelativeID"
                    FI_RelativeID.SetValue(value)
                Case "GrpDate"
                    FI_GrpDate.SetValue(value)
                Case "MarryDate"
                    FI_MarryDate.SetValue(value)
                Case "DeathDate"
                    FI_DeathDate.SetValue(value)
                Case "TransferDate"
                    FI_TransferDate.SetValue(value)
                Case "NotifyFlag"
                    FI_NotifyFlag.SetValue(value)
                Case "WelfareFlag"
                    FI_WelfareFlag.SetValue(value)
                Case "GrpUnitID"
                    FI_GrpUnitID.SetValue(value)
                Case "Reason"
                    FI_Reason.SetValue(value)
                Case "Remark1"
                    FI_Remark1.SetValue(value)
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
                Case "WaitType"
                    return FI_WaitType.Updated
                Case "WaitDate"
                    return FI_WaitDate.Updated
                Case "EmpID"
                    return FI_EmpID.Updated
                Case "RelativeIDNo"
                    return FI_RelativeIDNo.Updated
                Case "Source"
                    return FI_Source.Updated
                Case "Name"
                    return FI_Name.Updated
                Case "BirthDate"
                    return FI_BirthDate.Updated
                Case "GrpLvl"
                    return FI_GrpLvl.Updated
                Case "RelativeID"
                    return FI_RelativeID.Updated
                Case "GrpDate"
                    return FI_GrpDate.Updated
                Case "MarryDate"
                    return FI_MarryDate.Updated
                Case "DeathDate"
                    return FI_DeathDate.Updated
                Case "TransferDate"
                    return FI_TransferDate.Updated
                Case "NotifyFlag"
                    return FI_NotifyFlag.Updated
                Case "WelfareFlag"
                    return FI_WelfareFlag.Updated
                Case "GrpUnitID"
                    return FI_GrpUnitID.Updated
                Case "Reason"
                    return FI_Reason.Updated
                Case "Remark1"
                    return FI_Remark1.Updated
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
                Case "WaitType"
                    return FI_WaitType.CreateUpdateSQL
                Case "WaitDate"
                    return FI_WaitDate.CreateUpdateSQL
                Case "EmpID"
                    return FI_EmpID.CreateUpdateSQL
                Case "RelativeIDNo"
                    return FI_RelativeIDNo.CreateUpdateSQL
                Case "Source"
                    return FI_Source.CreateUpdateSQL
                Case "Name"
                    return FI_Name.CreateUpdateSQL
                Case "BirthDate"
                    return FI_BirthDate.CreateUpdateSQL
                Case "GrpLvl"
                    return FI_GrpLvl.CreateUpdateSQL
                Case "RelativeID"
                    return FI_RelativeID.CreateUpdateSQL
                Case "GrpDate"
                    return FI_GrpDate.CreateUpdateSQL
                Case "MarryDate"
                    return FI_MarryDate.CreateUpdateSQL
                Case "DeathDate"
                    return FI_DeathDate.CreateUpdateSQL
                Case "TransferDate"
                    return FI_TransferDate.CreateUpdateSQL
                Case "NotifyFlag"
                    return FI_NotifyFlag.CreateUpdateSQL
                Case "WelfareFlag"
                    return FI_WelfareFlag.CreateUpdateSQL
                Case "GrpUnitID"
                    return FI_GrpUnitID.CreateUpdateSQL
                Case "Reason"
                    return FI_Reason.CreateUpdateSQL
                Case "Remark1"
                    return FI_Remark1.CreateUpdateSQL
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
            FI_WaitType.SetInitValue("")
            FI_WaitDate.SetInitValue(Convert.ToDateTime("1900/01/01"))
            FI_EmpID.SetInitValue("")
            FI_RelativeIDNo.SetInitValue("")
            FI_Source.SetInitValue("")
            FI_Name.SetInitValue("")
            FI_BirthDate.SetInitValue(Convert.ToDateTime("1900/01/01"))
            FI_GrpLvl.SetInitValue("")
            FI_RelativeID.SetInitValue("")
            FI_GrpDate.SetInitValue(Convert.ToDateTime("1900/01/01"))
            FI_MarryDate.SetInitValue(Convert.ToDateTime("1900/01/01"))
            FI_DeathDate.SetInitValue(Convert.ToDateTime("1900/01/01"))
            FI_TransferDate.SetInitValue(Convert.ToDateTime("1900/01/01"))
            FI_NotifyFlag.SetInitValue("0")
            FI_WelfareFlag.SetInitValue("0")
            FI_GrpUnitID.SetInitValue(0)
            FI_Reason.SetInitValue(" ")
            FI_Remark1.SetInitValue("")
            FI_LastChgComp.SetInitValue("")
            FI_LastChgID.SetInitValue("")
            FI_LastChgDate.SetInitValue(Convert.ToDateTime("1900/01/01"))
        End Sub

        Public Sub New(ByVal dr As System.Data.DataRow)
            FI_CompID.SetInitValue(dr("CompID"))
            FI_WaitType.SetInitValue(dr("WaitType"))
            FI_WaitDate.SetInitValue(dr("WaitDate"))
            FI_EmpID.SetInitValue(dr("EmpID"))
            FI_RelativeIDNo.SetInitValue(dr("RelativeIDNo"))
            FI_Source.SetInitValue(dr("Source"))
            FI_Name.SetInitValue(dr("Name"))
            FI_BirthDate.SetInitValue(dr("BirthDate"))
            FI_GrpLvl.SetInitValue(dr("GrpLvl"))
            FI_RelativeID.SetInitValue(dr("RelativeID"))
            FI_GrpDate.SetInitValue(dr("GrpDate"))
            FI_MarryDate.SetInitValue(dr("MarryDate"))
            FI_DeathDate.SetInitValue(dr("DeathDate"))
            FI_TransferDate.SetInitValue(dr("TransferDate"))
            FI_NotifyFlag.SetInitValue(dr("NotifyFlag"))
            FI_WelfareFlag.SetInitValue(dr("WelfareFlag"))
            FI_GrpUnitID.SetInitValue(dr("GrpUnitID"))
            FI_Reason.SetInitValue(dr("Reason"))
            FI_Remark1.SetInitValue(dr("Remark1"))
            FI_LastChgComp.SetInitValue(dr("LastChgComp"))
            FI_LastChgID.SetInitValue(dr("LastChgID"))
            FI_LastChgDate.SetInitValue(dr("LastChgDate"))

            m_LoadFromDataRow = True
        End Sub

        Private Sub ClearUpdated()
            FI_CompID.Updated = False
            FI_WaitType.Updated = False
            FI_WaitDate.Updated = False
            FI_EmpID.Updated = False
            FI_RelativeIDNo.Updated = False
            FI_Source.Updated = False
            FI_Name.Updated = False
            FI_BirthDate.Updated = False
            FI_GrpLvl.Updated = False
            FI_RelativeID.Updated = False
            FI_GrpDate.Updated = False
            FI_MarryDate.Updated = False
            FI_DeathDate.Updated = False
            FI_TransferDate.Updated = False
            FI_NotifyFlag.Updated = False
            FI_WelfareFlag.Updated = False
            FI_GrpUnitID.Updated = False
            FI_Reason.Updated = False
            FI_Remark1.Updated = False
            FI_LastChgComp.Updated = False
            FI_LastChgID.Updated = False
            FI_LastChgDate.Updated = False
        End Sub

        Public ReadOnly Property CompID As Field(Of String) 
            Get
                Return FI_CompID
            End Get
        End Property

        Public ReadOnly Property WaitType As Field(Of String) 
            Get
                Return FI_WaitType
            End Get
        End Property

        Public ReadOnly Property WaitDate As Field(Of Date) 
            Get
                Return FI_WaitDate
            End Get
        End Property

        Public ReadOnly Property EmpID As Field(Of String) 
            Get
                Return FI_EmpID
            End Get
        End Property

        Public ReadOnly Property RelativeIDNo As Field(Of String) 
            Get
                Return FI_RelativeIDNo
            End Get
        End Property

        Public ReadOnly Property Source As Field(Of String) 
            Get
                Return FI_Source
            End Get
        End Property

        Public ReadOnly Property Name As Field(Of String) 
            Get
                Return FI_Name
            End Get
        End Property

        Public ReadOnly Property BirthDate As Field(Of Date) 
            Get
                Return FI_BirthDate
            End Get
        End Property

        Public ReadOnly Property GrpLvl As Field(Of String) 
            Get
                Return FI_GrpLvl
            End Get
        End Property

        Public ReadOnly Property RelativeID As Field(Of String) 
            Get
                Return FI_RelativeID
            End Get
        End Property

        Public ReadOnly Property GrpDate As Field(Of Date) 
            Get
                Return FI_GrpDate
            End Get
        End Property

        Public ReadOnly Property MarryDate As Field(Of Date) 
            Get
                Return FI_MarryDate
            End Get
        End Property

        Public ReadOnly Property DeathDate As Field(Of Date) 
            Get
                Return FI_DeathDate
            End Get
        End Property

        Public ReadOnly Property TransferDate As Field(Of Date) 
            Get
                Return FI_TransferDate
            End Get
        End Property

        Public ReadOnly Property NotifyFlag As Field(Of String) 
            Get
                Return FI_NotifyFlag
            End Get
        End Property

        Public ReadOnly Property WelfareFlag As Field(Of String) 
            Get
                Return FI_WelfareFlag
            End Get
        End Property

        Public ReadOnly Property GrpUnitID As Field(Of Integer) 
            Get
                Return FI_GrpUnitID
            End Get
        End Property

        Public ReadOnly Property Reason As Field(Of String) 
            Get
                Return FI_Reason
            End Get
        End Property

        Public ReadOnly Property Remark1 As Field(Of String) 
            Get
                Return FI_Remark1
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
        Public Function DeleteRowByPrimaryKey(ByVal GroupWaitRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Delete From GroupWait")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And WaitType = @WaitType")
            strSQL.AppendLine("And WaitDate = @WaitDate")
            strSQL.AppendLine("And EmpID = @EmpID")
            strSQL.AppendLine("And RelativeIDNo = @RelativeIDNo")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, GroupWaitRow.CompID.Value)
            db.AddInParameter(dbcmd, "@WaitType", DbType.String, GroupWaitRow.WaitType.Value)
            db.AddInParameter(dbcmd, "@WaitDate", DbType.Date, GroupWaitRow.WaitDate.Value)
            db.AddInParameter(dbcmd, "@EmpID", DbType.String, GroupWaitRow.EmpID.Value)
            db.AddInParameter(dbcmd, "@RelativeIDNo", DbType.String, GroupWaitRow.RelativeIDNo.Value)

            return db.ExecuteNonQuery(dbcmd)
        End Function

        public Function DeleteRowByPrimaryKey(ByVal GroupWaitRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Delete From GroupWait")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And WaitType = @WaitType")
            strSQL.AppendLine("And WaitDate = @WaitDate")
            strSQL.AppendLine("And EmpID = @EmpID")
            strSQL.AppendLine("And RelativeIDNo = @RelativeIDNo")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, GroupWaitRow.CompID.Value)
            db.AddInParameter(dbcmd, "@WaitType", DbType.String, GroupWaitRow.WaitType.Value)
            db.AddInParameter(dbcmd, "@WaitDate", DbType.Date, GroupWaitRow.WaitDate.Value)
            db.AddInParameter(dbcmd, "@EmpID", DbType.String, GroupWaitRow.EmpID.Value)
            db.AddInParameter(dbcmd, "@RelativeIDNo", DbType.String, GroupWaitRow.RelativeIDNo.Value)

            return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function DeleteRowByPrimaryKey(ByVal GroupWaitRow As Row()) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Delete From GroupWait")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And WaitType = @WaitType")
            strSQL.AppendLine("And WaitDate = @WaitDate")
            strSQL.AppendLine("And EmpID = @EmpID")
            strSQL.AppendLine("And RelativeIDNo = @RelativeIDNo")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            using cn As DbConnection = db.CreateConnection()
                cn.Open()
                Dim tran As DbTransaction = cn.BeginTransaction()
                Dim inTrans As Boolean = True

                Try
                    For Each r As Row In GroupWaitRow
                        dbcmd.Parameters.Clear()
                        db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                        db.AddInParameter(dbcmd, "@WaitType", DbType.String, r.WaitType.Value)
                        db.AddInParameter(dbcmd, "@WaitDate", DbType.Date, r.WaitDate.Value)
                        db.AddInParameter(dbcmd, "@EmpID", DbType.String, r.EmpID.Value)
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

        Public Function DeleteRowByPrimaryKey(ByVal GroupWaitRow As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Delete From GroupWait")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And WaitType = @WaitType")
            strSQL.AppendLine("And WaitDate = @WaitDate")
            strSQL.AppendLine("And EmpID = @EmpID")
            strSQL.AppendLine("And RelativeIDNo = @RelativeIDNo")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())

            For Each r As Row In GroupWaitRow
                dbcmd.Parameters.Clear()
                db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                db.AddInParameter(dbcmd, "@WaitType", DbType.String, r.WaitType.Value)
                db.AddInParameter(dbcmd, "@WaitDate", DbType.Date, r.WaitDate.Value)
                db.AddInParameter(dbcmd, "@EmpID", DbType.String, r.EmpID.Value)
                db.AddInParameter(dbcmd, "@RelativeIDNo", DbType.String, r.RelativeIDNo.Value)

                intRowsAffected += db.ExecuteNonQuery(dbcmd, tran)
            Next
            Return intRowsAffected
        End Function

        Public Function QueryByKey(ByVal GroupWaitRow As Row) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From GroupWait")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And WaitType = @WaitType")
            strSQL.AppendLine("And WaitDate = @WaitDate")
            strSQL.AppendLine("And EmpID = @EmpID")
            strSQL.AppendLine("And RelativeIDNo = @RelativeIDNo")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, GroupWaitRow.CompID.Value)
            db.AddInParameter(dbcmd, "@WaitType", DbType.String, GroupWaitRow.WaitType.Value)
            db.AddInParameter(dbcmd, "@WaitDate", DbType.Date, GroupWaitRow.WaitDate.Value)
            db.AddInParameter(dbcmd, "@EmpID", DbType.String, GroupWaitRow.EmpID.Value)
            db.AddInParameter(dbcmd, "@RelativeIDNo", DbType.String, GroupWaitRow.RelativeIDNo.Value)

            Return db.ExecuteDataSet(dbcmd)
        End Function

        Public Function QueryByKey(GroupWaitRow As Row, tran As DbTransaction) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From GroupWait")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And WaitType = @WaitType")
            strSQL.AppendLine("And WaitDate = @WaitDate")
            strSQL.AppendLine("And EmpID = @EmpID")
            strSQL.AppendLine("And RelativeIDNo = @RelativeIDNo")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, GroupWaitRow.CompID.Value)
            db.AddInParameter(dbcmd, "@WaitType", DbType.String, GroupWaitRow.WaitType.Value)
            db.AddInParameter(dbcmd, "@WaitDate", DbType.Date, GroupWaitRow.WaitDate.Value)
            db.AddInParameter(dbcmd, "@EmpID", DbType.String, GroupWaitRow.EmpID.Value)
            db.AddInParameter(dbcmd, "@RelativeIDNo", DbType.String, GroupWaitRow.RelativeIDNo.Value)

            Return db.ExecuteDataSet(dbcmd, tran)
        End Function

        Public Function Update(ByVal GroupWaitRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim strDot As String = String.Empty

            strSQL.AppendLine("Update GroupWait Set")
            For i As Integer = 0 To GroupWaitRow.FieldNames.Length - 1
                If Not GroupWaitRow.IsIdentityField(GroupWaitRow.FieldNames(i)) AndAlso GroupWaitRow.IsUpdated(GroupWaitRow.FieldNames(i)) AndAlso GroupWaitRow.CreateUpdateSQL(GroupWaitRow.FieldNames(i)) Then
                    strSQL.AppendLine(string.Format("{0}{1} = @{1}", strDot, GroupWaitRow.FieldNames(i)))
                    strDot = ","
                End If
            Next
            If strDot = String.Empty Then Throw New Exception("未異動資料欄位！")
            strSQL.AppendLine("Where CompID = @PKCompID")
            strSQL.AppendLine("And WaitType = @PKWaitType")
            strSQL.AppendLine("And WaitDate = @PKWaitDate")
            strSQL.AppendLine("And EmpID = @PKEmpID")
            strSQL.AppendLine("And RelativeIDNo = @PKRelativeIDNo")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            If GroupWaitRow.CompID.Updated Then db.AddInParameter(dbcmd, "@CompID", DbType.String, GroupWaitRow.CompID.Value)
            If GroupWaitRow.WaitType.Updated Then db.AddInParameter(dbcmd, "@WaitType", DbType.String, GroupWaitRow.WaitType.Value)
            If GroupWaitRow.WaitDate.Updated Then db.AddInParameter(dbcmd, "@WaitDate", DbType.Date, IIf(IsDateTimeNull(GroupWaitRow.WaitDate.Value), Convert.ToDateTime("1900/1/1"), GroupWaitRow.WaitDate.Value))
            If GroupWaitRow.EmpID.Updated Then db.AddInParameter(dbcmd, "@EmpID", DbType.String, GroupWaitRow.EmpID.Value)
            If GroupWaitRow.RelativeIDNo.Updated Then db.AddInParameter(dbcmd, "@RelativeIDNo", DbType.String, GroupWaitRow.RelativeIDNo.Value)
            If GroupWaitRow.Source.Updated Then db.AddInParameter(dbcmd, "@Source", DbType.String, GroupWaitRow.Source.Value)
            If GroupWaitRow.Name.Updated Then db.AddInParameter(dbcmd, "@Name", DbType.String, GroupWaitRow.Name.Value)
            If GroupWaitRow.BirthDate.Updated Then db.AddInParameter(dbcmd, "@BirthDate", DbType.Date, IIf(IsDateTimeNull(GroupWaitRow.BirthDate.Value), Convert.ToDateTime("1900/1/1"), GroupWaitRow.BirthDate.Value))
            If GroupWaitRow.GrpLvl.Updated Then db.AddInParameter(dbcmd, "@GrpLvl", DbType.String, GroupWaitRow.GrpLvl.Value)
            If GroupWaitRow.RelativeID.Updated Then db.AddInParameter(dbcmd, "@RelativeID", DbType.String, GroupWaitRow.RelativeID.Value)
            If GroupWaitRow.GrpDate.Updated Then db.AddInParameter(dbcmd, "@GrpDate", DbType.Date, IIf(IsDateTimeNull(GroupWaitRow.GrpDate.Value), Convert.ToDateTime("1900/1/1"), GroupWaitRow.GrpDate.Value))
            If GroupWaitRow.MarryDate.Updated Then db.AddInParameter(dbcmd, "@MarryDate", DbType.Date, IIf(IsDateTimeNull(GroupWaitRow.MarryDate.Value), Convert.ToDateTime("1900/1/1"), GroupWaitRow.MarryDate.Value))
            If GroupWaitRow.DeathDate.Updated Then db.AddInParameter(dbcmd, "@DeathDate", DbType.Date, IIf(IsDateTimeNull(GroupWaitRow.DeathDate.Value), Convert.ToDateTime("1900/1/1"), GroupWaitRow.DeathDate.Value))
            If GroupWaitRow.TransferDate.Updated Then db.AddInParameter(dbcmd, "@TransferDate", DbType.Date, IIf(IsDateTimeNull(GroupWaitRow.TransferDate.Value), Convert.ToDateTime("1900/1/1"), GroupWaitRow.TransferDate.Value))
            If GroupWaitRow.NotifyFlag.Updated Then db.AddInParameter(dbcmd, "@NotifyFlag", DbType.String, GroupWaitRow.NotifyFlag.Value)
            If GroupWaitRow.WelfareFlag.Updated Then db.AddInParameter(dbcmd, "@WelfareFlag", DbType.String, GroupWaitRow.WelfareFlag.Value)
            If GroupWaitRow.GrpUnitID.Updated Then db.AddInParameter(dbcmd, "@GrpUnitID", DbType.Int32, GroupWaitRow.GrpUnitID.Value)
            If GroupWaitRow.Reason.Updated Then db.AddInParameter(dbcmd, "@Reason", DbType.String, GroupWaitRow.Reason.Value)
            If GroupWaitRow.Remark1.Updated Then db.AddInParameter(dbcmd, "@Remark1", DbType.String, GroupWaitRow.Remark1.Value)
            If GroupWaitRow.LastChgComp.Updated Then db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, GroupWaitRow.LastChgComp.Value)
            If GroupWaitRow.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, GroupWaitRow.LastChgID.Value)
            If GroupWaitRow.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(GroupWaitRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), GroupWaitRow.LastChgDate.Value))
            db.AddInParameter(dbcmd, "@PKCompID", DbType.String, IIf(GroupWaitRow.LoadFromDataRow, GroupWaitRow.CompID.OldValue, GroupWaitRow.CompID.Value))
            db.AddInParameter(dbcmd, "@PKWaitType", DbType.String, IIf(GroupWaitRow.LoadFromDataRow, GroupWaitRow.WaitType.OldValue, GroupWaitRow.WaitType.Value))
            db.AddInParameter(dbcmd, "@PKWaitDate", DbType.Date, IIf(GroupWaitRow.LoadFromDataRow, GroupWaitRow.WaitDate.OldValue, GroupWaitRow.WaitDate.Value))
            db.AddInParameter(dbcmd, "@PKEmpID", DbType.String, IIf(GroupWaitRow.LoadFromDataRow, GroupWaitRow.EmpID.OldValue, GroupWaitRow.EmpID.Value))
            db.AddInParameter(dbcmd, "@PKRelativeIDNo", DbType.String, IIf(GroupWaitRow.LoadFromDataRow, GroupWaitRow.RelativeIDNo.OldValue, GroupWaitRow.RelativeIDNo.Value))

            Return db.ExecuteNonQuery(dbcmd)
        End Function

        Public Function Update(ByVal GroupWaitRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim strDot As String = String.Empty

            strSQL.AppendLine("Update GroupWait Set")
            For i As Integer = 0 To GroupWaitRow.FieldNames.Length - 1
                If Not GroupWaitRow.IsIdentityField(GroupWaitRow.FieldNames(i)) AndAlso GroupWaitRow.IsUpdated(GroupWaitRow.FieldNames(i)) AndAlso GroupWaitRow.CreateUpdateSQL(GroupWaitRow.FieldNames(i)) Then
                    strSQL.AppendLine(string.Format("{0}{1} = @{1}", strDot, GroupWaitRow.FieldNames(i)))
                    strDot = ","
                End If
            Next 
            If strDot = String.Empty Then Throw New Exception("未異動資料欄位！")
            strSQL.AppendLine("Where CompID = @PKCompID")
            strSQL.AppendLine("And WaitType = @PKWaitType")
            strSQL.AppendLine("And WaitDate = @PKWaitDate")
            strSQL.AppendLine("And EmpID = @PKEmpID")
            strSQL.AppendLine("And RelativeIDNo = @PKRelativeIDNo")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            If GroupWaitRow.CompID.Updated Then db.AddInParameter(dbcmd, "@CompID", DbType.String, GroupWaitRow.CompID.Value)
            If GroupWaitRow.WaitType.Updated Then db.AddInParameter(dbcmd, "@WaitType", DbType.String, GroupWaitRow.WaitType.Value)
            If GroupWaitRow.WaitDate.Updated Then db.AddInParameter(dbcmd, "@WaitDate", DbType.Date, IIf(IsDateTimeNull(GroupWaitRow.WaitDate.Value), Convert.ToDateTime("1900/1/1"), GroupWaitRow.WaitDate.Value))
            If GroupWaitRow.EmpID.Updated Then db.AddInParameter(dbcmd, "@EmpID", DbType.String, GroupWaitRow.EmpID.Value)
            If GroupWaitRow.RelativeIDNo.Updated Then db.AddInParameter(dbcmd, "@RelativeIDNo", DbType.String, GroupWaitRow.RelativeIDNo.Value)
            If GroupWaitRow.Source.Updated Then db.AddInParameter(dbcmd, "@Source", DbType.String, GroupWaitRow.Source.Value)
            If GroupWaitRow.Name.Updated Then db.AddInParameter(dbcmd, "@Name", DbType.String, GroupWaitRow.Name.Value)
            If GroupWaitRow.BirthDate.Updated Then db.AddInParameter(dbcmd, "@BirthDate", DbType.Date, IIf(IsDateTimeNull(GroupWaitRow.BirthDate.Value), Convert.ToDateTime("1900/1/1"), GroupWaitRow.BirthDate.Value))
            If GroupWaitRow.GrpLvl.Updated Then db.AddInParameter(dbcmd, "@GrpLvl", DbType.String, GroupWaitRow.GrpLvl.Value)
            If GroupWaitRow.RelativeID.Updated Then db.AddInParameter(dbcmd, "@RelativeID", DbType.String, GroupWaitRow.RelativeID.Value)
            If GroupWaitRow.GrpDate.Updated Then db.AddInParameter(dbcmd, "@GrpDate", DbType.Date, IIf(IsDateTimeNull(GroupWaitRow.GrpDate.Value), Convert.ToDateTime("1900/1/1"), GroupWaitRow.GrpDate.Value))
            If GroupWaitRow.MarryDate.Updated Then db.AddInParameter(dbcmd, "@MarryDate", DbType.Date, IIf(IsDateTimeNull(GroupWaitRow.MarryDate.Value), Convert.ToDateTime("1900/1/1"), GroupWaitRow.MarryDate.Value))
            If GroupWaitRow.DeathDate.Updated Then db.AddInParameter(dbcmd, "@DeathDate", DbType.Date, IIf(IsDateTimeNull(GroupWaitRow.DeathDate.Value), Convert.ToDateTime("1900/1/1"), GroupWaitRow.DeathDate.Value))
            If GroupWaitRow.TransferDate.Updated Then db.AddInParameter(dbcmd, "@TransferDate", DbType.Date, IIf(IsDateTimeNull(GroupWaitRow.TransferDate.Value), Convert.ToDateTime("1900/1/1"), GroupWaitRow.TransferDate.Value))
            If GroupWaitRow.NotifyFlag.Updated Then db.AddInParameter(dbcmd, "@NotifyFlag", DbType.String, GroupWaitRow.NotifyFlag.Value)
            If GroupWaitRow.WelfareFlag.Updated Then db.AddInParameter(dbcmd, "@WelfareFlag", DbType.String, GroupWaitRow.WelfareFlag.Value)
            If GroupWaitRow.GrpUnitID.Updated Then db.AddInParameter(dbcmd, "@GrpUnitID", DbType.Int32, GroupWaitRow.GrpUnitID.Value)
            If GroupWaitRow.Reason.Updated Then db.AddInParameter(dbcmd, "@Reason", DbType.String, GroupWaitRow.Reason.Value)
            If GroupWaitRow.Remark1.Updated Then db.AddInParameter(dbcmd, "@Remark1", DbType.String, GroupWaitRow.Remark1.Value)
            If GroupWaitRow.LastChgComp.Updated Then db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, GroupWaitRow.LastChgComp.Value)
            If GroupWaitRow.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, GroupWaitRow.LastChgID.Value)
            If GroupWaitRow.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(GroupWaitRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), GroupWaitRow.LastChgDate.Value))
            db.AddInParameter(dbcmd, "@PKCompID", DbType.String, IIf(GroupWaitRow.LoadFromDataRow, GroupWaitRow.CompID.OldValue, GroupWaitRow.CompID.Value))
            db.AddInParameter(dbcmd, "@PKWaitType", DbType.String, IIf(GroupWaitRow.LoadFromDataRow, GroupWaitRow.WaitType.OldValue, GroupWaitRow.WaitType.Value))
            db.AddInParameter(dbcmd, "@PKWaitDate", DbType.Date, IIf(GroupWaitRow.LoadFromDataRow, GroupWaitRow.WaitDate.OldValue, GroupWaitRow.WaitDate.Value))
            db.AddInParameter(dbcmd, "@PKEmpID", DbType.String, IIf(GroupWaitRow.LoadFromDataRow, GroupWaitRow.EmpID.OldValue, GroupWaitRow.EmpID.Value))
            db.AddInParameter(dbcmd, "@PKRelativeIDNo", DbType.String, IIf(GroupWaitRow.LoadFromDataRow, GroupWaitRow.RelativeIDNo.OldValue, GroupWaitRow.RelativeIDNo.Value))

            Return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function Update(ByVal GroupWaitRow As Row()) As Integer
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
                    For Each r As Row In GroupWaitRow
                        strDot = String.Empty
                        If strSQL.Length > 0 Then strSQL.Remove(0, strSQL.Length)
                        strSQL.AppendLine("Update GroupWait Set")
                        For i As Integer = 0 To r.FieldNames.Length - 1
                            If Not r.IsIdentityField(r.FieldNames(i)) AndAlso r.IsUpdated(r.FieldNames(i)) AndAlso r.CreateUpdateSQL(r.FieldNames(i)) Then
                                strSQL.AppendLine(String.Format("{0}{1} = @{1}", strDot, r.FieldNames(i)))
                                strDot = ","
                            End If
                        Next
                        If strDot = String.Empty Then Continue For
                        strSQL.AppendLine("Where CompID = @PKCompID")
                        strSQL.AppendLine("And WaitType = @PKWaitType")
                        strSQL.AppendLine("And WaitDate = @PKWaitDate")
                        strSQL.AppendLine("And EmpID = @PKEmpID")
                        strSQL.AppendLine("And RelativeIDNo = @PKRelativeIDNo")

                        dbcmd = db.GetSqlStringCommand(strSQL.ToString())
                        If r.CompID.Updated Then db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                        If r.WaitType.Updated Then db.AddInParameter(dbcmd, "@WaitType", DbType.String, r.WaitType.Value)
                        If r.WaitDate.Updated Then db.AddInParameter(dbcmd, "@WaitDate", DbType.Date, IIf(IsDateTimeNull(r.WaitDate.Value), Convert.ToDateTime("1900/1/1"), r.WaitDate.Value))
                        If r.EmpID.Updated Then db.AddInParameter(dbcmd, "@EmpID", DbType.String, r.EmpID.Value)
                        If r.RelativeIDNo.Updated Then db.AddInParameter(dbcmd, "@RelativeIDNo", DbType.String, r.RelativeIDNo.Value)
                        If r.Source.Updated Then db.AddInParameter(dbcmd, "@Source", DbType.String, r.Source.Value)
                        If r.Name.Updated Then db.AddInParameter(dbcmd, "@Name", DbType.String, r.Name.Value)
                        If r.BirthDate.Updated Then db.AddInParameter(dbcmd, "@BirthDate", DbType.Date, IIf(IsDateTimeNull(r.BirthDate.Value), Convert.ToDateTime("1900/1/1"), r.BirthDate.Value))
                        If r.GrpLvl.Updated Then db.AddInParameter(dbcmd, "@GrpLvl", DbType.String, r.GrpLvl.Value)
                        If r.RelativeID.Updated Then db.AddInParameter(dbcmd, "@RelativeID", DbType.String, r.RelativeID.Value)
                        If r.GrpDate.Updated Then db.AddInParameter(dbcmd, "@GrpDate", DbType.Date, IIf(IsDateTimeNull(r.GrpDate.Value), Convert.ToDateTime("1900/1/1"), r.GrpDate.Value))
                        If r.MarryDate.Updated Then db.AddInParameter(dbcmd, "@MarryDate", DbType.Date, IIf(IsDateTimeNull(r.MarryDate.Value), Convert.ToDateTime("1900/1/1"), r.MarryDate.Value))
                        If r.DeathDate.Updated Then db.AddInParameter(dbcmd, "@DeathDate", DbType.Date, IIf(IsDateTimeNull(r.DeathDate.Value), Convert.ToDateTime("1900/1/1"), r.DeathDate.Value))
                        If r.TransferDate.Updated Then db.AddInParameter(dbcmd, "@TransferDate", DbType.Date, IIf(IsDateTimeNull(r.TransferDate.Value), Convert.ToDateTime("1900/1/1"), r.TransferDate.Value))
                        If r.NotifyFlag.Updated Then db.AddInParameter(dbcmd, "@NotifyFlag", DbType.String, r.NotifyFlag.Value)
                        If r.WelfareFlag.Updated Then db.AddInParameter(dbcmd, "@WelfareFlag", DbType.String, r.WelfareFlag.Value)
                        If r.GrpUnitID.Updated Then db.AddInParameter(dbcmd, "@GrpUnitID", DbType.Int32, r.GrpUnitID.Value)
                        If r.Reason.Updated Then db.AddInParameter(dbcmd, "@Reason", DbType.String, r.Reason.Value)
                        If r.Remark1.Updated Then db.AddInParameter(dbcmd, "@Remark1", DbType.String, r.Remark1.Value)
                        If r.LastChgComp.Updated Then db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, r.LastChgComp.Value)
                        If r.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, r.LastChgID.Value)
                        If r.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(r.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), r.LastChgDate.Value))
                        db.AddInParameter(dbcmd, "@PKCompID", DbType.String, IIf(r.LoadFromDataRow, r.CompID.OldValue, r.CompID.Value))
                        db.AddInParameter(dbcmd, "@PKWaitType", DbType.String, IIf(r.LoadFromDataRow, r.WaitType.OldValue, r.WaitType.Value))
                        db.AddInParameter(dbcmd, "@PKWaitDate", DbType.Date, IIf(r.LoadFromDataRow, r.WaitDate.OldValue, r.WaitDate.Value))
                        db.AddInParameter(dbcmd, "@PKEmpID", DbType.String, IIf(r.LoadFromDataRow, r.EmpID.OldValue, r.EmpID.Value))
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

        Public Function Update(ByVal GroupWaitRow As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0
            Dim strDot As String

            For Each r As Row In GroupWaitRow
                strDot = String.Empty
                If strSQL.Length > 0 Then strSQL.Remove(0, strSQL.Length)
                strSQL.AppendLine("Update GroupWait Set")
                For i As Integer = 0 To r.FieldNames.Length - 1
                    If Not r.IsIdentityField(r.FieldNames(i)) AndAlso r.IsUpdated(r.FieldNames(i)) AndAlso r.CreateUpdateSQL(r.FieldNames(i))
                        strSQL.AppendLine(String.Format("{0}{1} = @{1}", strDot, r.FieldNames(i)))
                        strDot = ","
                    End If
                Next
                If strDot = String.Empty Then Continue For
                strSQL.AppendLine("Where CompID = @PKCompID")
                strSQL.AppendLine("And WaitType = @PKWaitType")
                strSQL.AppendLine("And WaitDate = @PKWaitDate")
                strSQL.AppendLine("And EmpID = @PKEmpID")
                strSQL.AppendLine("And RelativeIDNo = @PKRelativeIDNo")

                dbcmd = db.GetSqlStringCommand(strSQL.ToString())
                If r.CompID.Updated Then db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                If r.WaitType.Updated Then db.AddInParameter(dbcmd, "@WaitType", DbType.String, r.WaitType.Value)
                If r.WaitDate.Updated Then db.AddInParameter(dbcmd, "@WaitDate", DbType.Date, IIf(IsDateTimeNull(r.WaitDate.Value), Convert.ToDateTime("1900/1/1"), r.WaitDate.Value))
                If r.EmpID.Updated Then db.AddInParameter(dbcmd, "@EmpID", DbType.String, r.EmpID.Value)
                If r.RelativeIDNo.Updated Then db.AddInParameter(dbcmd, "@RelativeIDNo", DbType.String, r.RelativeIDNo.Value)
                If r.Source.Updated Then db.AddInParameter(dbcmd, "@Source", DbType.String, r.Source.Value)
                If r.Name.Updated Then db.AddInParameter(dbcmd, "@Name", DbType.String, r.Name.Value)
                If r.BirthDate.Updated Then db.AddInParameter(dbcmd, "@BirthDate", DbType.Date, IIf(IsDateTimeNull(r.BirthDate.Value), Convert.ToDateTime("1900/1/1"), r.BirthDate.Value))
                If r.GrpLvl.Updated Then db.AddInParameter(dbcmd, "@GrpLvl", DbType.String, r.GrpLvl.Value)
                If r.RelativeID.Updated Then db.AddInParameter(dbcmd, "@RelativeID", DbType.String, r.RelativeID.Value)
                If r.GrpDate.Updated Then db.AddInParameter(dbcmd, "@GrpDate", DbType.Date, IIf(IsDateTimeNull(r.GrpDate.Value), Convert.ToDateTime("1900/1/1"), r.GrpDate.Value))
                If r.MarryDate.Updated Then db.AddInParameter(dbcmd, "@MarryDate", DbType.Date, IIf(IsDateTimeNull(r.MarryDate.Value), Convert.ToDateTime("1900/1/1"), r.MarryDate.Value))
                If r.DeathDate.Updated Then db.AddInParameter(dbcmd, "@DeathDate", DbType.Date, IIf(IsDateTimeNull(r.DeathDate.Value), Convert.ToDateTime("1900/1/1"), r.DeathDate.Value))
                If r.TransferDate.Updated Then db.AddInParameter(dbcmd, "@TransferDate", DbType.Date, IIf(IsDateTimeNull(r.TransferDate.Value), Convert.ToDateTime("1900/1/1"), r.TransferDate.Value))
                If r.NotifyFlag.Updated Then db.AddInParameter(dbcmd, "@NotifyFlag", DbType.String, r.NotifyFlag.Value)
                If r.WelfareFlag.Updated Then db.AddInParameter(dbcmd, "@WelfareFlag", DbType.String, r.WelfareFlag.Value)
                If r.GrpUnitID.Updated Then db.AddInParameter(dbcmd, "@GrpUnitID", DbType.Int32, r.GrpUnitID.Value)
                If r.Reason.Updated Then db.AddInParameter(dbcmd, "@Reason", DbType.String, r.Reason.Value)
                If r.Remark1.Updated Then db.AddInParameter(dbcmd, "@Remark1", DbType.String, r.Remark1.Value)
                If r.LastChgComp.Updated Then db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, r.LastChgComp.Value)
                If r.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, r.LastChgID.Value)
                If r.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(r.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), r.LastChgDate.Value))
                db.AddInParameter(dbcmd, "@PKCompID", DbType.String, IIf(r.LoadFromDataRow, r.CompID.OldValue, r.CompID.Value))
                db.AddInParameter(dbcmd, "@PKWaitType", DbType.String, IIf(r.LoadFromDataRow, r.WaitType.OldValue, r.WaitType.Value))
                db.AddInParameter(dbcmd, "@PKWaitDate", DbType.Date, IIf(r.LoadFromDataRow, r.WaitDate.OldValue, r.WaitDate.Value))
                db.AddInParameter(dbcmd, "@PKEmpID", DbType.String, IIf(r.LoadFromDataRow, r.EmpID.OldValue, r.EmpID.Value))
                db.AddInParameter(dbcmd, "@PKRelativeIDNo", DbType.String, IIf(r.LoadFromDataRow, r.RelativeIDNo.OldValue, r.RelativeIDNo.Value))

                intRowsAffected += db.ExecuteNonQuery(dbcmd, tran)
            Next
            Return intRowsAffected
        End Function

        Public Function IsDataExists(ByVal GroupWaitRow As Row) As Boolean
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intCount As Integer = 0

            strSQL.AppendLine("Select Count(*) Cnt From GroupWait")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And WaitType = @WaitType")
            strSQL.AppendLine("And WaitDate = @WaitDate")
            strSQL.AppendLine("And EmpID = @EmpID")
            strSQL.AppendLine("And RelativeIDNo = @RelativeIDNo")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, GroupWaitRow.CompID.Value)
            db.AddInParameter(dbcmd, "@WaitType", DbType.String, GroupWaitRow.WaitType.Value)
            db.AddInParameter(dbcmd, "@WaitDate", DbType.Date, GroupWaitRow.WaitDate.Value)
            db.AddInParameter(dbcmd, "@EmpID", DbType.String, GroupWaitRow.EmpID.Value)
            db.AddInParameter(dbcmd, "@RelativeIDNo", DbType.String, GroupWaitRow.RelativeIDNo.Value)

            intCount = Convert.ToInt32(db.ExecuteScalar(dbcmd))
            Return IIf(intCount > 0, True, False)
        End Function

        Public Function IsDataExists(ByVal GroupWaitRow As Row, ByVal tran As DbTransaction) As Boolean
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intCount As Integer = 0

            strSQL.AppendLine("Select Count(*) Cnt From GroupWait")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And WaitType = @WaitType")
            strSQL.AppendLine("And WaitDate = @WaitDate")
            strSQL.AppendLine("And EmpID = @EmpID")
            strSQL.AppendLine("And RelativeIDNo = @RelativeIDNo")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, GroupWaitRow.CompID.Value)
            db.AddInParameter(dbcmd, "@WaitType", DbType.String, GroupWaitRow.WaitType.Value)
            db.AddInParameter(dbcmd, "@WaitDate", DbType.Date, GroupWaitRow.WaitDate.Value)
            db.AddInParameter(dbcmd, "@EmpID", DbType.String, GroupWaitRow.EmpID.Value)
            db.AddInParameter(dbcmd, "@RelativeIDNo", DbType.String, GroupWaitRow.RelativeIDNo.Value)

            intCount = Convert.ToInt32(db.ExecuteScalar(dbcmd, tran))
            Return IIf(intCount > 0, True, False)
        End Function

        Public Function QuerybyWhere(WhereCondition As String) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From GroupWait")
            If WhereCondition <> String.Empty Then strSQL.AppendLine(WhereCondition)

            Return db.ExecuteDataSet(CommandType.Text, strSQL.ToString())
        End Function

        Public Function Insert(ByVal GroupWaitRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Insert into GroupWait")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    CompID, WaitType, WaitDate, EmpID, RelativeIDNo, Source, Name, BirthDate, GrpLvl,")
            strSQL.AppendLine("    RelativeID, GrpDate, MarryDate, DeathDate, TransferDate, NotifyFlag, WelfareFlag, GrpUnitID,")
            strSQL.AppendLine("    Reason, Remark1, LastChgComp, LastChgID, LastChgDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @CompID, @WaitType, @WaitDate, @EmpID, @RelativeIDNo, @Source, @Name, @BirthDate, @GrpLvl,")
            strSQL.AppendLine("    @RelativeID, @GrpDate, @MarryDate, @DeathDate, @TransferDate, @NotifyFlag, @WelfareFlag, @GrpUnitID,")
            strSQL.AppendLine("    @Reason, @Remark1, @LastChgComp, @LastChgID, @LastChgDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, GroupWaitRow.CompID.Value)
            db.AddInParameter(dbcmd, "@WaitType", DbType.String, GroupWaitRow.WaitType.Value)
            db.AddInParameter(dbcmd, "@WaitDate", DbType.Date, IIf(IsDateTimeNull(GroupWaitRow.WaitDate.Value), Convert.ToDateTime("1900/1/1"), GroupWaitRow.WaitDate.Value))
            db.AddInParameter(dbcmd, "@EmpID", DbType.String, GroupWaitRow.EmpID.Value)
            db.AddInParameter(dbcmd, "@RelativeIDNo", DbType.String, GroupWaitRow.RelativeIDNo.Value)
            db.AddInParameter(dbcmd, "@Source", DbType.String, GroupWaitRow.Source.Value)
            db.AddInParameter(dbcmd, "@Name", DbType.String, GroupWaitRow.Name.Value)
            db.AddInParameter(dbcmd, "@BirthDate", DbType.Date, IIf(IsDateTimeNull(GroupWaitRow.BirthDate.Value), Convert.ToDateTime("1900/1/1"), GroupWaitRow.BirthDate.Value))
            db.AddInParameter(dbcmd, "@GrpLvl", DbType.String, GroupWaitRow.GrpLvl.Value)
            db.AddInParameter(dbcmd, "@RelativeID", DbType.String, GroupWaitRow.RelativeID.Value)
            db.AddInParameter(dbcmd, "@GrpDate", DbType.Date, IIf(IsDateTimeNull(GroupWaitRow.GrpDate.Value), Convert.ToDateTime("1900/1/1"), GroupWaitRow.GrpDate.Value))
            db.AddInParameter(dbcmd, "@MarryDate", DbType.Date, IIf(IsDateTimeNull(GroupWaitRow.MarryDate.Value), Convert.ToDateTime("1900/1/1"), GroupWaitRow.MarryDate.Value))
            db.AddInParameter(dbcmd, "@DeathDate", DbType.Date, IIf(IsDateTimeNull(GroupWaitRow.DeathDate.Value), Convert.ToDateTime("1900/1/1"), GroupWaitRow.DeathDate.Value))
            db.AddInParameter(dbcmd, "@TransferDate", DbType.Date, IIf(IsDateTimeNull(GroupWaitRow.TransferDate.Value), Convert.ToDateTime("1900/1/1"), GroupWaitRow.TransferDate.Value))
            db.AddInParameter(dbcmd, "@NotifyFlag", DbType.String, GroupWaitRow.NotifyFlag.Value)
            db.AddInParameter(dbcmd, "@WelfareFlag", DbType.String, GroupWaitRow.WelfareFlag.Value)
            db.AddInParameter(dbcmd, "@GrpUnitID", DbType.Int32, GroupWaitRow.GrpUnitID.Value)
            db.AddInParameter(dbcmd, "@Reason", DbType.String, GroupWaitRow.Reason.Value)
            db.AddInParameter(dbcmd, "@Remark1", DbType.String, GroupWaitRow.Remark1.Value)
            db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, GroupWaitRow.LastChgComp.Value)
            db.AddInParameter(dbcmd, "@LastChgID", DbType.String, GroupWaitRow.LastChgID.Value)
            db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(GroupWaitRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), GroupWaitRow.LastChgDate.Value))

            Return db.ExecuteNonQuery(dbcmd)
        End Function

        Public Function Insert(ByVal GroupWaitRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Insert into GroupWait")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    CompID, WaitType, WaitDate, EmpID, RelativeIDNo, Source, Name, BirthDate, GrpLvl,")
            strSQL.AppendLine("    RelativeID, GrpDate, MarryDate, DeathDate, TransferDate, NotifyFlag, WelfareFlag, GrpUnitID,")
            strSQL.AppendLine("    Reason, Remark1, LastChgComp, LastChgID, LastChgDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @CompID, @WaitType, @WaitDate, @EmpID, @RelativeIDNo, @Source, @Name, @BirthDate, @GrpLvl,")
            strSQL.AppendLine("    @RelativeID, @GrpDate, @MarryDate, @DeathDate, @TransferDate, @NotifyFlag, @WelfareFlag, @GrpUnitID,")
            strSQL.AppendLine("    @Reason, @Remark1, @LastChgComp, @LastChgID, @LastChgDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, GroupWaitRow.CompID.Value)
            db.AddInParameter(dbcmd, "@WaitType", DbType.String, GroupWaitRow.WaitType.Value)
            db.AddInParameter(dbcmd, "@WaitDate", DbType.Date, IIf(IsDateTimeNull(GroupWaitRow.WaitDate.Value), Convert.ToDateTime("1900/1/1"), GroupWaitRow.WaitDate.Value))
            db.AddInParameter(dbcmd, "@EmpID", DbType.String, GroupWaitRow.EmpID.Value)
            db.AddInParameter(dbcmd, "@RelativeIDNo", DbType.String, GroupWaitRow.RelativeIDNo.Value)
            db.AddInParameter(dbcmd, "@Source", DbType.String, GroupWaitRow.Source.Value)
            db.AddInParameter(dbcmd, "@Name", DbType.String, GroupWaitRow.Name.Value)
            db.AddInParameter(dbcmd, "@BirthDate", DbType.Date, IIf(IsDateTimeNull(GroupWaitRow.BirthDate.Value), Convert.ToDateTime("1900/1/1"), GroupWaitRow.BirthDate.Value))
            db.AddInParameter(dbcmd, "@GrpLvl", DbType.String, GroupWaitRow.GrpLvl.Value)
            db.AddInParameter(dbcmd, "@RelativeID", DbType.String, GroupWaitRow.RelativeID.Value)
            db.AddInParameter(dbcmd, "@GrpDate", DbType.Date, IIf(IsDateTimeNull(GroupWaitRow.GrpDate.Value), Convert.ToDateTime("1900/1/1"), GroupWaitRow.GrpDate.Value))
            db.AddInParameter(dbcmd, "@MarryDate", DbType.Date, IIf(IsDateTimeNull(GroupWaitRow.MarryDate.Value), Convert.ToDateTime("1900/1/1"), GroupWaitRow.MarryDate.Value))
            db.AddInParameter(dbcmd, "@DeathDate", DbType.Date, IIf(IsDateTimeNull(GroupWaitRow.DeathDate.Value), Convert.ToDateTime("1900/1/1"), GroupWaitRow.DeathDate.Value))
            db.AddInParameter(dbcmd, "@TransferDate", DbType.Date, IIf(IsDateTimeNull(GroupWaitRow.TransferDate.Value), Convert.ToDateTime("1900/1/1"), GroupWaitRow.TransferDate.Value))
            db.AddInParameter(dbcmd, "@NotifyFlag", DbType.String, GroupWaitRow.NotifyFlag.Value)
            db.AddInParameter(dbcmd, "@WelfareFlag", DbType.String, GroupWaitRow.WelfareFlag.Value)
            db.AddInParameter(dbcmd, "@GrpUnitID", DbType.Int32, GroupWaitRow.GrpUnitID.Value)
            db.AddInParameter(dbcmd, "@Reason", DbType.String, GroupWaitRow.Reason.Value)
            db.AddInParameter(dbcmd, "@Remark1", DbType.String, GroupWaitRow.Remark1.Value)
            db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, GroupWaitRow.LastChgComp.Value)
            db.AddInParameter(dbcmd, "@LastChgID", DbType.String, GroupWaitRow.LastChgID.Value)
            db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(GroupWaitRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), GroupWaitRow.LastChgDate.Value))

            Return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function Insert(ByVal GroupWaitRow As Row()) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Insert into GroupWait")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    CompID, WaitType, WaitDate, EmpID, RelativeIDNo, Source, Name, BirthDate, GrpLvl,")
            strSQL.AppendLine("    RelativeID, GrpDate, MarryDate, DeathDate, TransferDate, NotifyFlag, WelfareFlag, GrpUnitID,")
            strSQL.AppendLine("    Reason, Remark1, LastChgComp, LastChgID, LastChgDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @CompID, @WaitType, @WaitDate, @EmpID, @RelativeIDNo, @Source, @Name, @BirthDate, @GrpLvl,")
            strSQL.AppendLine("    @RelativeID, @GrpDate, @MarryDate, @DeathDate, @TransferDate, @NotifyFlag, @WelfareFlag, @GrpUnitID,")
            strSQL.AppendLine("    @Reason, @Remark1, @LastChgComp, @LastChgID, @LastChgDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            Using cn As DbConnection = db.CreateConnection()
                cn.Open()
                Dim tran As DbTransaction = cn.BeginTransaction()
                Dim inTrans As Boolean = True

                Try
                    For Each r As Row In GroupWaitRow
                        dbcmd.Parameters.Clear()
                        db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                        db.AddInParameter(dbcmd, "@WaitType", DbType.String, r.WaitType.Value)
                        db.AddInParameter(dbcmd, "@WaitDate", DbType.Date, IIf(IsDateTimeNull(r.WaitDate.Value), Convert.ToDateTime("1900/1/1"), r.WaitDate.Value))
                        db.AddInParameter(dbcmd, "@EmpID", DbType.String, r.EmpID.Value)
                        db.AddInParameter(dbcmd, "@RelativeIDNo", DbType.String, r.RelativeIDNo.Value)
                        db.AddInParameter(dbcmd, "@Source", DbType.String, r.Source.Value)
                        db.AddInParameter(dbcmd, "@Name", DbType.String, r.Name.Value)
                        db.AddInParameter(dbcmd, "@BirthDate", DbType.Date, IIf(IsDateTimeNull(r.BirthDate.Value), Convert.ToDateTime("1900/1/1"), r.BirthDate.Value))
                        db.AddInParameter(dbcmd, "@GrpLvl", DbType.String, r.GrpLvl.Value)
                        db.AddInParameter(dbcmd, "@RelativeID", DbType.String, r.RelativeID.Value)
                        db.AddInParameter(dbcmd, "@GrpDate", DbType.Date, IIf(IsDateTimeNull(r.GrpDate.Value), Convert.ToDateTime("1900/1/1"), r.GrpDate.Value))
                        db.AddInParameter(dbcmd, "@MarryDate", DbType.Date, IIf(IsDateTimeNull(r.MarryDate.Value), Convert.ToDateTime("1900/1/1"), r.MarryDate.Value))
                        db.AddInParameter(dbcmd, "@DeathDate", DbType.Date, IIf(IsDateTimeNull(r.DeathDate.Value), Convert.ToDateTime("1900/1/1"), r.DeathDate.Value))
                        db.AddInParameter(dbcmd, "@TransferDate", DbType.Date, IIf(IsDateTimeNull(r.TransferDate.Value), Convert.ToDateTime("1900/1/1"), r.TransferDate.Value))
                        db.AddInParameter(dbcmd, "@NotifyFlag", DbType.String, r.NotifyFlag.Value)
                        db.AddInParameter(dbcmd, "@WelfareFlag", DbType.String, r.WelfareFlag.Value)
                        db.AddInParameter(dbcmd, "@GrpUnitID", DbType.Int32, r.GrpUnitID.Value)
                        db.AddInParameter(dbcmd, "@Reason", DbType.String, r.Reason.Value)
                        db.AddInParameter(dbcmd, "@Remark1", DbType.String, r.Remark1.Value)
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

        Public Function Insert(ByVal GroupWaitRow As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Insert into GroupWait")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    CompID, WaitType, WaitDate, EmpID, RelativeIDNo, Source, Name, BirthDate, GrpLvl,")
            strSQL.AppendLine("    RelativeID, GrpDate, MarryDate, DeathDate, TransferDate, NotifyFlag, WelfareFlag, GrpUnitID,")
            strSQL.AppendLine("    Reason, Remark1, LastChgComp, LastChgID, LastChgDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @CompID, @WaitType, @WaitDate, @EmpID, @RelativeIDNo, @Source, @Name, @BirthDate, @GrpLvl,")
            strSQL.AppendLine("    @RelativeID, @GrpDate, @MarryDate, @DeathDate, @TransferDate, @NotifyFlag, @WelfareFlag, @GrpUnitID,")
            strSQL.AppendLine("    @Reason, @Remark1, @LastChgComp, @LastChgID, @LastChgDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())

            For Each r As Row In GroupWaitRow
                dbcmd.Parameters.Clear()
                db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                db.AddInParameter(dbcmd, "@WaitType", DbType.String, r.WaitType.Value)
                db.AddInParameter(dbcmd, "@WaitDate", DbType.Date, IIf(IsDateTimeNull(r.WaitDate.Value), Convert.ToDateTime("1900/1/1"), r.WaitDate.Value))
                db.AddInParameter(dbcmd, "@EmpID", DbType.String, r.EmpID.Value)
                db.AddInParameter(dbcmd, "@RelativeIDNo", DbType.String, r.RelativeIDNo.Value)
                db.AddInParameter(dbcmd, "@Source", DbType.String, r.Source.Value)
                db.AddInParameter(dbcmd, "@Name", DbType.String, r.Name.Value)
                db.AddInParameter(dbcmd, "@BirthDate", DbType.Date, IIf(IsDateTimeNull(r.BirthDate.Value), Convert.ToDateTime("1900/1/1"), r.BirthDate.Value))
                db.AddInParameter(dbcmd, "@GrpLvl", DbType.String, r.GrpLvl.Value)
                db.AddInParameter(dbcmd, "@RelativeID", DbType.String, r.RelativeID.Value)
                db.AddInParameter(dbcmd, "@GrpDate", DbType.Date, IIf(IsDateTimeNull(r.GrpDate.Value), Convert.ToDateTime("1900/1/1"), r.GrpDate.Value))
                db.AddInParameter(dbcmd, "@MarryDate", DbType.Date, IIf(IsDateTimeNull(r.MarryDate.Value), Convert.ToDateTime("1900/1/1"), r.MarryDate.Value))
                db.AddInParameter(dbcmd, "@DeathDate", DbType.Date, IIf(IsDateTimeNull(r.DeathDate.Value), Convert.ToDateTime("1900/1/1"), r.DeathDate.Value))
                db.AddInParameter(dbcmd, "@TransferDate", DbType.Date, IIf(IsDateTimeNull(r.TransferDate.Value), Convert.ToDateTime("1900/1/1"), r.TransferDate.Value))
                db.AddInParameter(dbcmd, "@NotifyFlag", DbType.String, r.NotifyFlag.Value)
                db.AddInParameter(dbcmd, "@WelfareFlag", DbType.String, r.WelfareFlag.Value)
                db.AddInParameter(dbcmd, "@GrpUnitID", DbType.Int32, r.GrpUnitID.Value)
                db.AddInParameter(dbcmd, "@Reason", DbType.String, r.Reason.Value)
                db.AddInParameter(dbcmd, "@Remark1", DbType.String, r.Remark1.Value)
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

