'****************************************************************
' Table:PersonalWait
' Created Date: 2014.09.23
'****************************************************************/
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports Microsoft.Practices.EnterpriseLibrary.Common
Imports Microsoft.Practices.EnterpriseLibrary.Data
Imports System.Data.Common
Imports System.Data

Namespace bePersonalWait
    Public Class Table
        Private m_Rows As Rows = New Rows()
        Private m_Fields As String() = { "ReleaseMark", "CompID", "EmpID", "RelativeIDNo", "ApplyDate", "Reason", "OldData", "NewData", "ReleaseComp", "ReleaseEmpID" _
                                    , "ReleaseDate", "Remark", "Remark1", "LastChgComp", "LastChgID", "LastChgDate" }
        Private m_Types As System.Type() = { GetType(String), GetType(String), GetType(String), GetType(String), GetType(Date), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String) _
                                    , GetType(Date), GetType(String), GetType(String), GetType(String), GetType(String), GetType(Date) }
        Private m_PrimaryFields As String() = { "CompID", "EmpID", "RelativeIDNo", "ApplyDate", "Reason", "OldData" }

        Public ReadOnly Property Rows() As bePersonalWait.Rows 
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
        Public Sub Transfer2Row(PersonalWaitTable As DataTable)
            For Each dr As DataRow In PersonalWaitTable.Rows
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

                dr(m_Rows(i).ReleaseMark.FieldName) = m_Rows(i).ReleaseMark.Value
                dr(m_Rows(i).CompID.FieldName) = m_Rows(i).CompID.Value
                dr(m_Rows(i).EmpID.FieldName) = m_Rows(i).EmpID.Value
                dr(m_Rows(i).RelativeIDNo.FieldName) = m_Rows(i).RelativeIDNo.Value
                dr(m_Rows(i).ApplyDate.FieldName) = m_Rows(i).ApplyDate.Value
                dr(m_Rows(i).Reason.FieldName) = m_Rows(i).Reason.Value
                dr(m_Rows(i).OldData.FieldName) = m_Rows(i).OldData.Value
                dr(m_Rows(i).NewData.FieldName) = m_Rows(i).NewData.Value
                dr(m_Rows(i).ReleaseComp.FieldName) = m_Rows(i).ReleaseComp.Value
                dr(m_Rows(i).ReleaseEmpID.FieldName) = m_Rows(i).ReleaseEmpID.Value
                dr(m_Rows(i).ReleaseDate.FieldName) = m_Rows(i).ReleaseDate.Value
                dr(m_Rows(i).Remark.FieldName) = m_Rows(i).Remark.Value
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

        Public Sub Add(PersonalWaitRow As Row)
            m_Rows.Add(PersonalWaitRow)
        End Sub

        Public Sub Remove(PersonalWaitRow As Row)
            If m_Rows.IndexOf(PersonalWaitRow) >= 0 Then
                m_Rows.Remove(PersonalWaitRow)
            End If
        End Sub

        Public Sub Dispose()
            m_Rows.Clear()
        End Sub

    End Class

    Public Class Row

        Private FI_ReleaseMark As Field(Of String) = new Field(Of String)("ReleaseMark", true)
        Private FI_CompID As Field(Of String) = new Field(Of String)("CompID", true)
        Private FI_EmpID As Field(Of String) = new Field(Of String)("EmpID", true)
        Private FI_RelativeIDNo As Field(Of String) = new Field(Of String)("RelativeIDNo", true)
        Private FI_ApplyDate As Field(Of Date) = new Field(Of Date)("ApplyDate", true)
        Private FI_Reason As Field(Of String) = new Field(Of String)("Reason", true)
        Private FI_OldData As Field(Of String) = new Field(Of String)("OldData", true)
        Private FI_NewData As Field(Of String) = new Field(Of String)("NewData", true)
        Private FI_ReleaseComp As Field(Of String) = new Field(Of String)("ReleaseComp", true)
        Private FI_ReleaseEmpID As Field(Of String) = new Field(Of String)("ReleaseEmpID", true)
        Private FI_ReleaseDate As Field(Of Date) = new Field(Of Date)("ReleaseDate", true)
        Private FI_Remark As Field(Of String) = new Field(Of String)("Remark", true)
        Private FI_Remark1 As Field(Of String) = new Field(Of String)("Remark1", true)
        Private FI_LastChgComp As Field(Of String) = new Field(Of String)("LastChgComp", true)
        Private FI_LastChgID As Field(Of String) = new Field(Of String)("LastChgID", true)
        Private FI_LastChgDate As Field(Of Date) = new Field(Of Date)("LastChgDate", true)
        Private m_FieldNames As String() = { "ReleaseMark", "CompID", "EmpID", "RelativeIDNo", "ApplyDate", "Reason", "OldData", "NewData", "ReleaseComp", "ReleaseEmpID" _
                                    , "ReleaseDate", "Remark", "Remark1", "LastChgComp", "LastChgID", "LastChgDate" }
        Private m_PrimaryFields As String() = { "CompID", "EmpID", "RelativeIDNo", "ApplyDate", "Reason", "OldData" }
        Private m_IdentityFields As String() = {  }
        Private m_LoadFromDataRow As Boolean = False

        Private Function GetFieldValue(ByVal fieldName As String) As Object
            Select Case fieldName
                Case "ReleaseMark"
                    Return FI_ReleaseMark.Value
                Case "CompID"
                    Return FI_CompID.Value
                Case "EmpID"
                    Return FI_EmpID.Value
                Case "RelativeIDNo"
                    Return FI_RelativeIDNo.Value
                Case "ApplyDate"
                    Return FI_ApplyDate.Value
                Case "Reason"
                    Return FI_Reason.Value
                Case "OldData"
                    Return FI_OldData.Value
                Case "NewData"
                    Return FI_NewData.Value
                Case "ReleaseComp"
                    Return FI_ReleaseComp.Value
                Case "ReleaseEmpID"
                    Return FI_ReleaseEmpID.Value
                Case "ReleaseDate"
                    Return FI_ReleaseDate.Value
                Case "Remark"
                    Return FI_Remark.Value
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
                Case "ReleaseMark"
                    FI_ReleaseMark.SetValue(value)
                Case "CompID"
                    FI_CompID.SetValue(value)
                Case "EmpID"
                    FI_EmpID.SetValue(value)
                Case "RelativeIDNo"
                    FI_RelativeIDNo.SetValue(value)
                Case "ApplyDate"
                    FI_ApplyDate.SetValue(value)
                Case "Reason"
                    FI_Reason.SetValue(value)
                Case "OldData"
                    FI_OldData.SetValue(value)
                Case "NewData"
                    FI_NewData.SetValue(value)
                Case "ReleaseComp"
                    FI_ReleaseComp.SetValue(value)
                Case "ReleaseEmpID"
                    FI_ReleaseEmpID.SetValue(value)
                Case "ReleaseDate"
                    FI_ReleaseDate.SetValue(value)
                Case "Remark"
                    FI_Remark.SetValue(value)
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
                Case "ReleaseMark"
                    return FI_ReleaseMark.Updated
                Case "CompID"
                    return FI_CompID.Updated
                Case "EmpID"
                    return FI_EmpID.Updated
                Case "RelativeIDNo"
                    return FI_RelativeIDNo.Updated
                Case "ApplyDate"
                    return FI_ApplyDate.Updated
                Case "Reason"
                    return FI_Reason.Updated
                Case "OldData"
                    return FI_OldData.Updated
                Case "NewData"
                    return FI_NewData.Updated
                Case "ReleaseComp"
                    return FI_ReleaseComp.Updated
                Case "ReleaseEmpID"
                    return FI_ReleaseEmpID.Updated
                Case "ReleaseDate"
                    return FI_ReleaseDate.Updated
                Case "Remark"
                    return FI_Remark.Updated
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
                Case "ReleaseMark"
                    return FI_ReleaseMark.CreateUpdateSQL
                Case "CompID"
                    return FI_CompID.CreateUpdateSQL
                Case "EmpID"
                    return FI_EmpID.CreateUpdateSQL
                Case "RelativeIDNo"
                    return FI_RelativeIDNo.CreateUpdateSQL
                Case "ApplyDate"
                    return FI_ApplyDate.CreateUpdateSQL
                Case "Reason"
                    return FI_Reason.CreateUpdateSQL
                Case "OldData"
                    return FI_OldData.CreateUpdateSQL
                Case "NewData"
                    return FI_NewData.CreateUpdateSQL
                Case "ReleaseComp"
                    return FI_ReleaseComp.CreateUpdateSQL
                Case "ReleaseEmpID"
                    return FI_ReleaseEmpID.CreateUpdateSQL
                Case "ReleaseDate"
                    return FI_ReleaseDate.CreateUpdateSQL
                Case "Remark"
                    return FI_Remark.CreateUpdateSQL
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
            FI_ReleaseMark.SetInitValue("0")
            FI_CompID.SetInitValue("")
            FI_EmpID.SetInitValue("")
            FI_RelativeIDNo.SetInitValue("")
            FI_ApplyDate.SetInitValue(Convert.ToDateTime("1900/01/01"))
            FI_Reason.SetInitValue("")
            FI_OldData.SetInitValue("")
            FI_NewData.SetInitValue("")
            FI_ReleaseComp.SetInitValue("")
            FI_ReleaseEmpID.SetInitValue("")
            FI_ReleaseDate.SetInitValue(Convert.ToDateTime("1900/01/01"))
            FI_Remark.SetInitValue("")
            FI_Remark1.SetInitValue("")
            FI_LastChgComp.SetInitValue("")
            FI_LastChgID.SetInitValue("")
            FI_LastChgDate.SetInitValue(Convert.ToDateTime("1900/01/01"))
        End Sub

        Public Sub New(ByVal dr As System.Data.DataRow)
            FI_ReleaseMark.SetInitValue(dr("ReleaseMark"))
            FI_CompID.SetInitValue(dr("CompID"))
            FI_EmpID.SetInitValue(dr("EmpID"))
            FI_RelativeIDNo.SetInitValue(dr("RelativeIDNo"))
            FI_ApplyDate.SetInitValue(dr("ApplyDate"))
            FI_Reason.SetInitValue(dr("Reason"))
            FI_OldData.SetInitValue(dr("OldData"))
            FI_NewData.SetInitValue(dr("NewData"))
            FI_ReleaseComp.SetInitValue(dr("ReleaseComp"))
            FI_ReleaseEmpID.SetInitValue(dr("ReleaseEmpID"))
            FI_ReleaseDate.SetInitValue(dr("ReleaseDate"))
            FI_Remark.SetInitValue(dr("Remark"))
            FI_Remark1.SetInitValue(dr("Remark1"))
            FI_LastChgComp.SetInitValue(dr("LastChgComp"))
            FI_LastChgID.SetInitValue(dr("LastChgID"))
            FI_LastChgDate.SetInitValue(dr("LastChgDate"))

            m_LoadFromDataRow = True
        End Sub

        Private Sub ClearUpdated()
            FI_ReleaseMark.Updated = False
            FI_CompID.Updated = False
            FI_EmpID.Updated = False
            FI_RelativeIDNo.Updated = False
            FI_ApplyDate.Updated = False
            FI_Reason.Updated = False
            FI_OldData.Updated = False
            FI_NewData.Updated = False
            FI_ReleaseComp.Updated = False
            FI_ReleaseEmpID.Updated = False
            FI_ReleaseDate.Updated = False
            FI_Remark.Updated = False
            FI_Remark1.Updated = False
            FI_LastChgComp.Updated = False
            FI_LastChgID.Updated = False
            FI_LastChgDate.Updated = False
        End Sub

        Public ReadOnly Property ReleaseMark As Field(Of String) 
            Get
                Return FI_ReleaseMark
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

        Public ReadOnly Property RelativeIDNo As Field(Of String) 
            Get
                Return FI_RelativeIDNo
            End Get
        End Property

        Public ReadOnly Property ApplyDate As Field(Of Date) 
            Get
                Return FI_ApplyDate
            End Get
        End Property

        Public ReadOnly Property Reason As Field(Of String) 
            Get
                Return FI_Reason
            End Get
        End Property

        Public ReadOnly Property OldData As Field(Of String) 
            Get
                Return FI_OldData
            End Get
        End Property

        Public ReadOnly Property NewData As Field(Of String) 
            Get
                Return FI_NewData
            End Get
        End Property

        Public ReadOnly Property ReleaseComp As Field(Of String) 
            Get
                Return FI_ReleaseComp
            End Get
        End Property

        Public ReadOnly Property ReleaseEmpID As Field(Of String) 
            Get
                Return FI_ReleaseEmpID
            End Get
        End Property

        Public ReadOnly Property ReleaseDate As Field(Of Date) 
            Get
                Return FI_ReleaseDate
            End Get
        End Property

        Public ReadOnly Property Remark As Field(Of String) 
            Get
                Return FI_Remark
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
        Public Function DeleteRowByPrimaryKey(ByVal PersonalWaitRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Delete From PersonalWait")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And EmpID = @EmpID")
            strSQL.AppendLine("And RelativeIDNo = @RelativeIDNo")
            strSQL.AppendLine("And ApplyDate = @ApplyDate")
            strSQL.AppendLine("And Reason = @Reason")
            strSQL.AppendLine("And OldData = @OldData")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, PersonalWaitRow.CompID.Value)
            db.AddInParameter(dbcmd, "@EmpID", DbType.String, PersonalWaitRow.EmpID.Value)
            db.AddInParameter(dbcmd, "@RelativeIDNo", DbType.String, PersonalWaitRow.RelativeIDNo.Value)
            db.AddInParameter(dbcmd, "@ApplyDate", DbType.Date, PersonalWaitRow.ApplyDate.Value)
            db.AddInParameter(dbcmd, "@Reason", DbType.String, PersonalWaitRow.Reason.Value)
            db.AddInParameter(dbcmd, "@OldData", DbType.String, PersonalWaitRow.OldData.Value)

            return db.ExecuteNonQuery(dbcmd)
        End Function

        public Function DeleteRowByPrimaryKey(ByVal PersonalWaitRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Delete From PersonalWait")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And EmpID = @EmpID")
            strSQL.AppendLine("And RelativeIDNo = @RelativeIDNo")
            strSQL.AppendLine("And ApplyDate = @ApplyDate")
            strSQL.AppendLine("And Reason = @Reason")
            strSQL.AppendLine("And OldData = @OldData")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, PersonalWaitRow.CompID.Value)
            db.AddInParameter(dbcmd, "@EmpID", DbType.String, PersonalWaitRow.EmpID.Value)
            db.AddInParameter(dbcmd, "@RelativeIDNo", DbType.String, PersonalWaitRow.RelativeIDNo.Value)
            db.AddInParameter(dbcmd, "@ApplyDate", DbType.Date, PersonalWaitRow.ApplyDate.Value)
            db.AddInParameter(dbcmd, "@Reason", DbType.String, PersonalWaitRow.Reason.Value)
            db.AddInParameter(dbcmd, "@OldData", DbType.String, PersonalWaitRow.OldData.Value)

            return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function DeleteRowByPrimaryKey(ByVal PersonalWaitRow As Row()) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Delete From PersonalWait")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And EmpID = @EmpID")
            strSQL.AppendLine("And RelativeIDNo = @RelativeIDNo")
            strSQL.AppendLine("And ApplyDate = @ApplyDate")
            strSQL.AppendLine("And Reason = @Reason")
            strSQL.AppendLine("And OldData = @OldData")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            using cn As DbConnection = db.CreateConnection()
                cn.Open()
                Dim tran As DbTransaction = cn.BeginTransaction()
                Dim inTrans As Boolean = True

                Try
                    For Each r As Row In PersonalWaitRow
                        dbcmd.Parameters.Clear()
                        db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                        db.AddInParameter(dbcmd, "@EmpID", DbType.String, r.EmpID.Value)
                        db.AddInParameter(dbcmd, "@RelativeIDNo", DbType.String, r.RelativeIDNo.Value)
                        db.AddInParameter(dbcmd, "@ApplyDate", DbType.Date, r.ApplyDate.Value)
                        db.AddInParameter(dbcmd, "@Reason", DbType.String, r.Reason.Value)
                        db.AddInParameter(dbcmd, "@OldData", DbType.String, r.OldData.Value)

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

        Public Function DeleteRowByPrimaryKey(ByVal PersonalWaitRow As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Delete From PersonalWait")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And EmpID = @EmpID")
            strSQL.AppendLine("And RelativeIDNo = @RelativeIDNo")
            strSQL.AppendLine("And ApplyDate = @ApplyDate")
            strSQL.AppendLine("And Reason = @Reason")
            strSQL.AppendLine("And OldData = @OldData")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())

            For Each r As Row In PersonalWaitRow
                dbcmd.Parameters.Clear()
                db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                db.AddInParameter(dbcmd, "@EmpID", DbType.String, r.EmpID.Value)
                db.AddInParameter(dbcmd, "@RelativeIDNo", DbType.String, r.RelativeIDNo.Value)
                db.AddInParameter(dbcmd, "@ApplyDate", DbType.Date, r.ApplyDate.Value)
                db.AddInParameter(dbcmd, "@Reason", DbType.String, r.Reason.Value)
                db.AddInParameter(dbcmd, "@OldData", DbType.String, r.OldData.Value)

                intRowsAffected += db.ExecuteNonQuery(dbcmd, tran)
            Next
            Return intRowsAffected
        End Function

        Public Function QueryByKey(ByVal PersonalWaitRow As Row) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From PersonalWait")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And EmpID = @EmpID")
            strSQL.AppendLine("And RelativeIDNo = @RelativeIDNo")
            strSQL.AppendLine("And ApplyDate = @ApplyDate")
            strSQL.AppendLine("And Reason = @Reason")
            strSQL.AppendLine("And OldData = @OldData")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, PersonalWaitRow.CompID.Value)
            db.AddInParameter(dbcmd, "@EmpID", DbType.String, PersonalWaitRow.EmpID.Value)
            db.AddInParameter(dbcmd, "@RelativeIDNo", DbType.String, PersonalWaitRow.RelativeIDNo.Value)
            db.AddInParameter(dbcmd, "@ApplyDate", DbType.Date, PersonalWaitRow.ApplyDate.Value)
            db.AddInParameter(dbcmd, "@Reason", DbType.String, PersonalWaitRow.Reason.Value)
            db.AddInParameter(dbcmd, "@OldData", DbType.String, PersonalWaitRow.OldData.Value)

            Return db.ExecuteDataSet(dbcmd)
        End Function

        Public Function QueryByKey(PersonalWaitRow As Row, tran As DbTransaction) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From PersonalWait")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And EmpID = @EmpID")
            strSQL.AppendLine("And RelativeIDNo = @RelativeIDNo")
            strSQL.AppendLine("And ApplyDate = @ApplyDate")
            strSQL.AppendLine("And Reason = @Reason")
            strSQL.AppendLine("And OldData = @OldData")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, PersonalWaitRow.CompID.Value)
            db.AddInParameter(dbcmd, "@EmpID", DbType.String, PersonalWaitRow.EmpID.Value)
            db.AddInParameter(dbcmd, "@RelativeIDNo", DbType.String, PersonalWaitRow.RelativeIDNo.Value)
            db.AddInParameter(dbcmd, "@ApplyDate", DbType.Date, PersonalWaitRow.ApplyDate.Value)
            db.AddInParameter(dbcmd, "@Reason", DbType.String, PersonalWaitRow.Reason.Value)
            db.AddInParameter(dbcmd, "@OldData", DbType.String, PersonalWaitRow.OldData.Value)

            Return db.ExecuteDataSet(dbcmd, tran)
        End Function

        Public Function Update(ByVal PersonalWaitRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim strDot As String = String.Empty

            strSQL.AppendLine("Update PersonalWait Set")
            For i As Integer = 0 To PersonalWaitRow.FieldNames.Length - 1
                If Not PersonalWaitRow.IsIdentityField(PersonalWaitRow.FieldNames(i)) AndAlso PersonalWaitRow.IsUpdated(PersonalWaitRow.FieldNames(i)) AndAlso PersonalWaitRow.CreateUpdateSQL(PersonalWaitRow.FieldNames(i)) Then
                    strSQL.AppendLine(string.Format("{0}{1} = @{1}", strDot, PersonalWaitRow.FieldNames(i)))
                    strDot = ","
                End If
            Next
            If strDot = String.Empty Then Throw New Exception("未異動資料欄位！")
            strSQL.AppendLine("Where CompID = @PKCompID")
            strSQL.AppendLine("And EmpID = @PKEmpID")
            strSQL.AppendLine("And RelativeIDNo = @PKRelativeIDNo")
            strSQL.AppendLine("And ApplyDate = @PKApplyDate")
            strSQL.AppendLine("And Reason = @PKReason")
            strSQL.AppendLine("And OldData = @PKOldData")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            If PersonalWaitRow.ReleaseMark.Updated Then db.AddInParameter(dbcmd, "@ReleaseMark", DbType.String, PersonalWaitRow.ReleaseMark.Value)
            If PersonalWaitRow.CompID.Updated Then db.AddInParameter(dbcmd, "@CompID", DbType.String, PersonalWaitRow.CompID.Value)
            If PersonalWaitRow.EmpID.Updated Then db.AddInParameter(dbcmd, "@EmpID", DbType.String, PersonalWaitRow.EmpID.Value)
            If PersonalWaitRow.RelativeIDNo.Updated Then db.AddInParameter(dbcmd, "@RelativeIDNo", DbType.String, PersonalWaitRow.RelativeIDNo.Value)
            If PersonalWaitRow.ApplyDate.Updated Then db.AddInParameter(dbcmd, "@ApplyDate", DbType.Date, IIf(IsDateTimeNull(PersonalWaitRow.ApplyDate.Value), Convert.ToDateTime("1900/1/1"), PersonalWaitRow.ApplyDate.Value))
            If PersonalWaitRow.Reason.Updated Then db.AddInParameter(dbcmd, "@Reason", DbType.String, PersonalWaitRow.Reason.Value)
            If PersonalWaitRow.OldData.Updated Then db.AddInParameter(dbcmd, "@OldData", DbType.String, PersonalWaitRow.OldData.Value)
            If PersonalWaitRow.NewData.Updated Then db.AddInParameter(dbcmd, "@NewData", DbType.String, PersonalWaitRow.NewData.Value)
            If PersonalWaitRow.ReleaseComp.Updated Then db.AddInParameter(dbcmd, "@ReleaseComp", DbType.String, PersonalWaitRow.ReleaseComp.Value)
            If PersonalWaitRow.ReleaseEmpID.Updated Then db.AddInParameter(dbcmd, "@ReleaseEmpID", DbType.String, PersonalWaitRow.ReleaseEmpID.Value)
            If PersonalWaitRow.ReleaseDate.Updated Then db.AddInParameter(dbcmd, "@ReleaseDate", DbType.Date, IIf(IsDateTimeNull(PersonalWaitRow.ReleaseDate.Value), Convert.ToDateTime("1900/1/1"), PersonalWaitRow.ReleaseDate.Value))
            If PersonalWaitRow.Remark.Updated Then db.AddInParameter(dbcmd, "@Remark", DbType.String, PersonalWaitRow.Remark.Value)
            If PersonalWaitRow.Remark1.Updated Then db.AddInParameter(dbcmd, "@Remark1", DbType.String, PersonalWaitRow.Remark1.Value)
            If PersonalWaitRow.LastChgComp.Updated Then db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, PersonalWaitRow.LastChgComp.Value)
            If PersonalWaitRow.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, PersonalWaitRow.LastChgID.Value)
            If PersonalWaitRow.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(PersonalWaitRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), PersonalWaitRow.LastChgDate.Value))
            db.AddInParameter(dbcmd, "@PKCompID", DbType.String, IIf(PersonalWaitRow.LoadFromDataRow, PersonalWaitRow.CompID.OldValue, PersonalWaitRow.CompID.Value))
            db.AddInParameter(dbcmd, "@PKEmpID", DbType.String, IIf(PersonalWaitRow.LoadFromDataRow, PersonalWaitRow.EmpID.OldValue, PersonalWaitRow.EmpID.Value))
            db.AddInParameter(dbcmd, "@PKRelativeIDNo", DbType.String, IIf(PersonalWaitRow.LoadFromDataRow, PersonalWaitRow.RelativeIDNo.OldValue, PersonalWaitRow.RelativeIDNo.Value))
            db.AddInParameter(dbcmd, "@PKApplyDate", DbType.Date, IIf(PersonalWaitRow.LoadFromDataRow, PersonalWaitRow.ApplyDate.OldValue, PersonalWaitRow.ApplyDate.Value))
            db.AddInParameter(dbcmd, "@PKReason", DbType.String, IIf(PersonalWaitRow.LoadFromDataRow, PersonalWaitRow.Reason.OldValue, PersonalWaitRow.Reason.Value))
            db.AddInParameter(dbcmd, "@PKOldData", DbType.String, IIf(PersonalWaitRow.LoadFromDataRow, PersonalWaitRow.OldData.OldValue, PersonalWaitRow.OldData.Value))

            Return db.ExecuteNonQuery(dbcmd)
        End Function

        Public Function Update(ByVal PersonalWaitRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim strDot As String = String.Empty

            strSQL.AppendLine("Update PersonalWait Set")
            For i As Integer = 0 To PersonalWaitRow.FieldNames.Length - 1
                If Not PersonalWaitRow.IsIdentityField(PersonalWaitRow.FieldNames(i)) AndAlso PersonalWaitRow.IsUpdated(PersonalWaitRow.FieldNames(i)) AndAlso PersonalWaitRow.CreateUpdateSQL(PersonalWaitRow.FieldNames(i)) Then
                    strSQL.AppendLine(string.Format("{0}{1} = @{1}", strDot, PersonalWaitRow.FieldNames(i)))
                    strDot = ","
                End If
            Next 
            If strDot = String.Empty Then Throw New Exception("未異動資料欄位！")
            strSQL.AppendLine("Where CompID = @PKCompID")
            strSQL.AppendLine("And EmpID = @PKEmpID")
            strSQL.AppendLine("And RelativeIDNo = @PKRelativeIDNo")
            strSQL.AppendLine("And ApplyDate = @PKApplyDate")
            strSQL.AppendLine("And Reason = @PKReason")
            strSQL.AppendLine("And OldData = @PKOldData")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            If PersonalWaitRow.ReleaseMark.Updated Then db.AddInParameter(dbcmd, "@ReleaseMark", DbType.String, PersonalWaitRow.ReleaseMark.Value)
            If PersonalWaitRow.CompID.Updated Then db.AddInParameter(dbcmd, "@CompID", DbType.String, PersonalWaitRow.CompID.Value)
            If PersonalWaitRow.EmpID.Updated Then db.AddInParameter(dbcmd, "@EmpID", DbType.String, PersonalWaitRow.EmpID.Value)
            If PersonalWaitRow.RelativeIDNo.Updated Then db.AddInParameter(dbcmd, "@RelativeIDNo", DbType.String, PersonalWaitRow.RelativeIDNo.Value)
            If PersonalWaitRow.ApplyDate.Updated Then db.AddInParameter(dbcmd, "@ApplyDate", DbType.Date, IIf(IsDateTimeNull(PersonalWaitRow.ApplyDate.Value), Convert.ToDateTime("1900/1/1"), PersonalWaitRow.ApplyDate.Value))
            If PersonalWaitRow.Reason.Updated Then db.AddInParameter(dbcmd, "@Reason", DbType.String, PersonalWaitRow.Reason.Value)
            If PersonalWaitRow.OldData.Updated Then db.AddInParameter(dbcmd, "@OldData", DbType.String, PersonalWaitRow.OldData.Value)
            If PersonalWaitRow.NewData.Updated Then db.AddInParameter(dbcmd, "@NewData", DbType.String, PersonalWaitRow.NewData.Value)
            If PersonalWaitRow.ReleaseComp.Updated Then db.AddInParameter(dbcmd, "@ReleaseComp", DbType.String, PersonalWaitRow.ReleaseComp.Value)
            If PersonalWaitRow.ReleaseEmpID.Updated Then db.AddInParameter(dbcmd, "@ReleaseEmpID", DbType.String, PersonalWaitRow.ReleaseEmpID.Value)
            If PersonalWaitRow.ReleaseDate.Updated Then db.AddInParameter(dbcmd, "@ReleaseDate", DbType.Date, IIf(IsDateTimeNull(PersonalWaitRow.ReleaseDate.Value), Convert.ToDateTime("1900/1/1"), PersonalWaitRow.ReleaseDate.Value))
            If PersonalWaitRow.Remark.Updated Then db.AddInParameter(dbcmd, "@Remark", DbType.String, PersonalWaitRow.Remark.Value)
            If PersonalWaitRow.Remark1.Updated Then db.AddInParameter(dbcmd, "@Remark1", DbType.String, PersonalWaitRow.Remark1.Value)
            If PersonalWaitRow.LastChgComp.Updated Then db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, PersonalWaitRow.LastChgComp.Value)
            If PersonalWaitRow.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, PersonalWaitRow.LastChgID.Value)
            If PersonalWaitRow.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(PersonalWaitRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), PersonalWaitRow.LastChgDate.Value))
            db.AddInParameter(dbcmd, "@PKCompID", DbType.String, IIf(PersonalWaitRow.LoadFromDataRow, PersonalWaitRow.CompID.OldValue, PersonalWaitRow.CompID.Value))
            db.AddInParameter(dbcmd, "@PKEmpID", DbType.String, IIf(PersonalWaitRow.LoadFromDataRow, PersonalWaitRow.EmpID.OldValue, PersonalWaitRow.EmpID.Value))
            db.AddInParameter(dbcmd, "@PKRelativeIDNo", DbType.String, IIf(PersonalWaitRow.LoadFromDataRow, PersonalWaitRow.RelativeIDNo.OldValue, PersonalWaitRow.RelativeIDNo.Value))
            db.AddInParameter(dbcmd, "@PKApplyDate", DbType.Date, IIf(PersonalWaitRow.LoadFromDataRow, PersonalWaitRow.ApplyDate.OldValue, PersonalWaitRow.ApplyDate.Value))
            db.AddInParameter(dbcmd, "@PKReason", DbType.String, IIf(PersonalWaitRow.LoadFromDataRow, PersonalWaitRow.Reason.OldValue, PersonalWaitRow.Reason.Value))
            db.AddInParameter(dbcmd, "@PKOldData", DbType.String, IIf(PersonalWaitRow.LoadFromDataRow, PersonalWaitRow.OldData.OldValue, PersonalWaitRow.OldData.Value))

            Return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function Update(ByVal PersonalWaitRow As Row()) As Integer
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
                    For Each r As Row In PersonalWaitRow
                        strDot = String.Empty
                        If strSQL.Length > 0 Then strSQL.Remove(0, strSQL.Length)
                        strSQL.AppendLine("Update PersonalWait Set")
                        For i As Integer = 0 To r.FieldNames.Length - 1
                            If Not r.IsIdentityField(r.FieldNames(i)) AndAlso r.IsUpdated(r.FieldNames(i)) AndAlso r.CreateUpdateSQL(r.FieldNames(i)) Then
                                strSQL.AppendLine(String.Format("{0}{1} = @{1}", strDot, r.FieldNames(i)))
                                strDot = ","
                            End If
                        Next
                        If strDot = String.Empty Then Continue For
                        strSQL.AppendLine("Where CompID = @PKCompID")
                        strSQL.AppendLine("And EmpID = @PKEmpID")
                        strSQL.AppendLine("And RelativeIDNo = @PKRelativeIDNo")
                        strSQL.AppendLine("And ApplyDate = @PKApplyDate")
                        strSQL.AppendLine("And Reason = @PKReason")
                        strSQL.AppendLine("And OldData = @PKOldData")

                        dbcmd = db.GetSqlStringCommand(strSQL.ToString())
                        If r.ReleaseMark.Updated Then db.AddInParameter(dbcmd, "@ReleaseMark", DbType.String, r.ReleaseMark.Value)
                        If r.CompID.Updated Then db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                        If r.EmpID.Updated Then db.AddInParameter(dbcmd, "@EmpID", DbType.String, r.EmpID.Value)
                        If r.RelativeIDNo.Updated Then db.AddInParameter(dbcmd, "@RelativeIDNo", DbType.String, r.RelativeIDNo.Value)
                        If r.ApplyDate.Updated Then db.AddInParameter(dbcmd, "@ApplyDate", DbType.Date, IIf(IsDateTimeNull(r.ApplyDate.Value), Convert.ToDateTime("1900/1/1"), r.ApplyDate.Value))
                        If r.Reason.Updated Then db.AddInParameter(dbcmd, "@Reason", DbType.String, r.Reason.Value)
                        If r.OldData.Updated Then db.AddInParameter(dbcmd, "@OldData", DbType.String, r.OldData.Value)
                        If r.NewData.Updated Then db.AddInParameter(dbcmd, "@NewData", DbType.String, r.NewData.Value)
                        If r.ReleaseComp.Updated Then db.AddInParameter(dbcmd, "@ReleaseComp", DbType.String, r.ReleaseComp.Value)
                        If r.ReleaseEmpID.Updated Then db.AddInParameter(dbcmd, "@ReleaseEmpID", DbType.String, r.ReleaseEmpID.Value)
                        If r.ReleaseDate.Updated Then db.AddInParameter(dbcmd, "@ReleaseDate", DbType.Date, IIf(IsDateTimeNull(r.ReleaseDate.Value), Convert.ToDateTime("1900/1/1"), r.ReleaseDate.Value))
                        If r.Remark.Updated Then db.AddInParameter(dbcmd, "@Remark", DbType.String, r.Remark.Value)
                        If r.Remark1.Updated Then db.AddInParameter(dbcmd, "@Remark1", DbType.String, r.Remark1.Value)
                        If r.LastChgComp.Updated Then db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, r.LastChgComp.Value)
                        If r.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, r.LastChgID.Value)
                        If r.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(r.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), r.LastChgDate.Value))
                        db.AddInParameter(dbcmd, "@PKCompID", DbType.String, IIf(r.LoadFromDataRow, r.CompID.OldValue, r.CompID.Value))
                        db.AddInParameter(dbcmd, "@PKEmpID", DbType.String, IIf(r.LoadFromDataRow, r.EmpID.OldValue, r.EmpID.Value))
                        db.AddInParameter(dbcmd, "@PKRelativeIDNo", DbType.String, IIf(r.LoadFromDataRow, r.RelativeIDNo.OldValue, r.RelativeIDNo.Value))
                        db.AddInParameter(dbcmd, "@PKApplyDate", DbType.Date, IIf(r.LoadFromDataRow, r.ApplyDate.OldValue, r.ApplyDate.Value))
                        db.AddInParameter(dbcmd, "@PKReason", DbType.String, IIf(r.LoadFromDataRow, r.Reason.OldValue, r.Reason.Value))
                        db.AddInParameter(dbcmd, "@PKOldData", DbType.String, IIf(r.LoadFromDataRow, r.OldData.OldValue, r.OldData.Value))

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

        Public Function Update(ByVal PersonalWaitRow As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0
            Dim strDot As String

            For Each r As Row In PersonalWaitRow
                strDot = String.Empty
                If strSQL.Length > 0 Then strSQL.Remove(0, strSQL.Length)
                strSQL.AppendLine("Update PersonalWait Set")
                For i As Integer = 0 To r.FieldNames.Length - 1
                    If Not r.IsIdentityField(r.FieldNames(i)) AndAlso r.IsUpdated(r.FieldNames(i)) AndAlso r.CreateUpdateSQL(r.FieldNames(i))
                        strSQL.AppendLine(String.Format("{0}{1} = @{1}", strDot, r.FieldNames(i)))
                        strDot = ","
                    End If
                Next
                If strDot = String.Empty Then Continue For
                strSQL.AppendLine("Where CompID = @PKCompID")
                strSQL.AppendLine("And EmpID = @PKEmpID")
                strSQL.AppendLine("And RelativeIDNo = @PKRelativeIDNo")
                strSQL.AppendLine("And ApplyDate = @PKApplyDate")
                strSQL.AppendLine("And Reason = @PKReason")
                strSQL.AppendLine("And OldData = @PKOldData")

                dbcmd = db.GetSqlStringCommand(strSQL.ToString())
                If r.ReleaseMark.Updated Then db.AddInParameter(dbcmd, "@ReleaseMark", DbType.String, r.ReleaseMark.Value)
                If r.CompID.Updated Then db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                If r.EmpID.Updated Then db.AddInParameter(dbcmd, "@EmpID", DbType.String, r.EmpID.Value)
                If r.RelativeIDNo.Updated Then db.AddInParameter(dbcmd, "@RelativeIDNo", DbType.String, r.RelativeIDNo.Value)
                If r.ApplyDate.Updated Then db.AddInParameter(dbcmd, "@ApplyDate", DbType.Date, IIf(IsDateTimeNull(r.ApplyDate.Value), Convert.ToDateTime("1900/1/1"), r.ApplyDate.Value))
                If r.Reason.Updated Then db.AddInParameter(dbcmd, "@Reason", DbType.String, r.Reason.Value)
                If r.OldData.Updated Then db.AddInParameter(dbcmd, "@OldData", DbType.String, r.OldData.Value)
                If r.NewData.Updated Then db.AddInParameter(dbcmd, "@NewData", DbType.String, r.NewData.Value)
                If r.ReleaseComp.Updated Then db.AddInParameter(dbcmd, "@ReleaseComp", DbType.String, r.ReleaseComp.Value)
                If r.ReleaseEmpID.Updated Then db.AddInParameter(dbcmd, "@ReleaseEmpID", DbType.String, r.ReleaseEmpID.Value)
                If r.ReleaseDate.Updated Then db.AddInParameter(dbcmd, "@ReleaseDate", DbType.Date, IIf(IsDateTimeNull(r.ReleaseDate.Value), Convert.ToDateTime("1900/1/1"), r.ReleaseDate.Value))
                If r.Remark.Updated Then db.AddInParameter(dbcmd, "@Remark", DbType.String, r.Remark.Value)
                If r.Remark1.Updated Then db.AddInParameter(dbcmd, "@Remark1", DbType.String, r.Remark1.Value)
                If r.LastChgComp.Updated Then db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, r.LastChgComp.Value)
                If r.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, r.LastChgID.Value)
                If r.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(r.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), r.LastChgDate.Value))
                db.AddInParameter(dbcmd, "@PKCompID", DbType.String, IIf(r.LoadFromDataRow, r.CompID.OldValue, r.CompID.Value))
                db.AddInParameter(dbcmd, "@PKEmpID", DbType.String, IIf(r.LoadFromDataRow, r.EmpID.OldValue, r.EmpID.Value))
                db.AddInParameter(dbcmd, "@PKRelativeIDNo", DbType.String, IIf(r.LoadFromDataRow, r.RelativeIDNo.OldValue, r.RelativeIDNo.Value))
                db.AddInParameter(dbcmd, "@PKApplyDate", DbType.Date, IIf(r.LoadFromDataRow, r.ApplyDate.OldValue, r.ApplyDate.Value))
                db.AddInParameter(dbcmd, "@PKReason", DbType.String, IIf(r.LoadFromDataRow, r.Reason.OldValue, r.Reason.Value))
                db.AddInParameter(dbcmd, "@PKOldData", DbType.String, IIf(r.LoadFromDataRow, r.OldData.OldValue, r.OldData.Value))

                intRowsAffected += db.ExecuteNonQuery(dbcmd, tran)
            Next
            Return intRowsAffected
        End Function

        Public Function IsDataExists(ByVal PersonalWaitRow As Row) As Boolean
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intCount As Integer = 0

            strSQL.AppendLine("Select Count(*) Cnt From PersonalWait")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And EmpID = @EmpID")
            strSQL.AppendLine("And RelativeIDNo = @RelativeIDNo")
            strSQL.AppendLine("And ApplyDate = @ApplyDate")
            strSQL.AppendLine("And Reason = @Reason")
            strSQL.AppendLine("And OldData = @OldData")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, PersonalWaitRow.CompID.Value)
            db.AddInParameter(dbcmd, "@EmpID", DbType.String, PersonalWaitRow.EmpID.Value)
            db.AddInParameter(dbcmd, "@RelativeIDNo", DbType.String, PersonalWaitRow.RelativeIDNo.Value)
            db.AddInParameter(dbcmd, "@ApplyDate", DbType.Date, PersonalWaitRow.ApplyDate.Value)
            db.AddInParameter(dbcmd, "@Reason", DbType.String, PersonalWaitRow.Reason.Value)
            db.AddInParameter(dbcmd, "@OldData", DbType.String, PersonalWaitRow.OldData.Value)

            intCount = Convert.ToInt32(db.ExecuteScalar(dbcmd))
            Return IIf(intCount > 0, True, False)
        End Function

        Public Function IsDataExists(ByVal PersonalWaitRow As Row, ByVal tran As DbTransaction) As Boolean
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intCount As Integer = 0

            strSQL.AppendLine("Select Count(*) Cnt From PersonalWait")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And EmpID = @EmpID")
            strSQL.AppendLine("And RelativeIDNo = @RelativeIDNo")
            strSQL.AppendLine("And ApplyDate = @ApplyDate")
            strSQL.AppendLine("And Reason = @Reason")
            strSQL.AppendLine("And OldData = @OldData")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, PersonalWaitRow.CompID.Value)
            db.AddInParameter(dbcmd, "@EmpID", DbType.String, PersonalWaitRow.EmpID.Value)
            db.AddInParameter(dbcmd, "@RelativeIDNo", DbType.String, PersonalWaitRow.RelativeIDNo.Value)
            db.AddInParameter(dbcmd, "@ApplyDate", DbType.Date, PersonalWaitRow.ApplyDate.Value)
            db.AddInParameter(dbcmd, "@Reason", DbType.String, PersonalWaitRow.Reason.Value)
            db.AddInParameter(dbcmd, "@OldData", DbType.String, PersonalWaitRow.OldData.Value)

            intCount = Convert.ToInt32(db.ExecuteScalar(dbcmd, tran))
            Return IIf(intCount > 0, True, False)
        End Function

        Public Function QuerybyWhere(WhereCondition As String) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From PersonalWait")
            If WhereCondition <> String.Empty Then strSQL.AppendLine(WhereCondition)

            Return db.ExecuteDataSet(CommandType.Text, strSQL.ToString())
        End Function

        Public Function Insert(ByVal PersonalWaitRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Insert into PersonalWait")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    ReleaseMark, CompID, EmpID, RelativeIDNo, ApplyDate, Reason, OldData, NewData, ReleaseComp,")
            strSQL.AppendLine("    ReleaseEmpID, ReleaseDate, Remark, Remark1, LastChgComp, LastChgID, LastChgDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @ReleaseMark, @CompID, @EmpID, @RelativeIDNo, @ApplyDate, @Reason, @OldData, @NewData, @ReleaseComp,")
            strSQL.AppendLine("    @ReleaseEmpID, @ReleaseDate, @Remark, @Remark1, @LastChgComp, @LastChgID, @LastChgDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@ReleaseMark", DbType.String, PersonalWaitRow.ReleaseMark.Value)
            db.AddInParameter(dbcmd, "@CompID", DbType.String, PersonalWaitRow.CompID.Value)
            db.AddInParameter(dbcmd, "@EmpID", DbType.String, PersonalWaitRow.EmpID.Value)
            db.AddInParameter(dbcmd, "@RelativeIDNo", DbType.String, PersonalWaitRow.RelativeIDNo.Value)
            db.AddInParameter(dbcmd, "@ApplyDate", DbType.Date, IIf(IsDateTimeNull(PersonalWaitRow.ApplyDate.Value), Convert.ToDateTime("1900/1/1"), PersonalWaitRow.ApplyDate.Value))
            db.AddInParameter(dbcmd, "@Reason", DbType.String, PersonalWaitRow.Reason.Value)
            db.AddInParameter(dbcmd, "@OldData", DbType.String, PersonalWaitRow.OldData.Value)
            db.AddInParameter(dbcmd, "@NewData", DbType.String, PersonalWaitRow.NewData.Value)
            db.AddInParameter(dbcmd, "@ReleaseComp", DbType.String, PersonalWaitRow.ReleaseComp.Value)
            db.AddInParameter(dbcmd, "@ReleaseEmpID", DbType.String, PersonalWaitRow.ReleaseEmpID.Value)
            db.AddInParameter(dbcmd, "@ReleaseDate", DbType.Date, IIf(IsDateTimeNull(PersonalWaitRow.ReleaseDate.Value), Convert.ToDateTime("1900/1/1"), PersonalWaitRow.ReleaseDate.Value))
            db.AddInParameter(dbcmd, "@Remark", DbType.String, PersonalWaitRow.Remark.Value)
            db.AddInParameter(dbcmd, "@Remark1", DbType.String, PersonalWaitRow.Remark1.Value)
            db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, PersonalWaitRow.LastChgComp.Value)
            db.AddInParameter(dbcmd, "@LastChgID", DbType.String, PersonalWaitRow.LastChgID.Value)
            db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(PersonalWaitRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), PersonalWaitRow.LastChgDate.Value))

            Return db.ExecuteNonQuery(dbcmd)
        End Function

        Public Function Insert(ByVal PersonalWaitRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Insert into PersonalWait")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    ReleaseMark, CompID, EmpID, RelativeIDNo, ApplyDate, Reason, OldData, NewData, ReleaseComp,")
            strSQL.AppendLine("    ReleaseEmpID, ReleaseDate, Remark, Remark1, LastChgComp, LastChgID, LastChgDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @ReleaseMark, @CompID, @EmpID, @RelativeIDNo, @ApplyDate, @Reason, @OldData, @NewData, @ReleaseComp,")
            strSQL.AppendLine("    @ReleaseEmpID, @ReleaseDate, @Remark, @Remark1, @LastChgComp, @LastChgID, @LastChgDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@ReleaseMark", DbType.String, PersonalWaitRow.ReleaseMark.Value)
            db.AddInParameter(dbcmd, "@CompID", DbType.String, PersonalWaitRow.CompID.Value)
            db.AddInParameter(dbcmd, "@EmpID", DbType.String, PersonalWaitRow.EmpID.Value)
            db.AddInParameter(dbcmd, "@RelativeIDNo", DbType.String, PersonalWaitRow.RelativeIDNo.Value)
            db.AddInParameter(dbcmd, "@ApplyDate", DbType.Date, IIf(IsDateTimeNull(PersonalWaitRow.ApplyDate.Value), Convert.ToDateTime("1900/1/1"), PersonalWaitRow.ApplyDate.Value))
            db.AddInParameter(dbcmd, "@Reason", DbType.String, PersonalWaitRow.Reason.Value)
            db.AddInParameter(dbcmd, "@OldData", DbType.String, PersonalWaitRow.OldData.Value)
            db.AddInParameter(dbcmd, "@NewData", DbType.String, PersonalWaitRow.NewData.Value)
            db.AddInParameter(dbcmd, "@ReleaseComp", DbType.String, PersonalWaitRow.ReleaseComp.Value)
            db.AddInParameter(dbcmd, "@ReleaseEmpID", DbType.String, PersonalWaitRow.ReleaseEmpID.Value)
            db.AddInParameter(dbcmd, "@ReleaseDate", DbType.Date, IIf(IsDateTimeNull(PersonalWaitRow.ReleaseDate.Value), Convert.ToDateTime("1900/1/1"), PersonalWaitRow.ReleaseDate.Value))
            db.AddInParameter(dbcmd, "@Remark", DbType.String, PersonalWaitRow.Remark.Value)
            db.AddInParameter(dbcmd, "@Remark1", DbType.String, PersonalWaitRow.Remark1.Value)
            db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, PersonalWaitRow.LastChgComp.Value)
            db.AddInParameter(dbcmd, "@LastChgID", DbType.String, PersonalWaitRow.LastChgID.Value)
            db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(PersonalWaitRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), PersonalWaitRow.LastChgDate.Value))

            Return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function Insert(ByVal PersonalWaitRow As Row()) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Insert into PersonalWait")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    ReleaseMark, CompID, EmpID, RelativeIDNo, ApplyDate, Reason, OldData, NewData, ReleaseComp,")
            strSQL.AppendLine("    ReleaseEmpID, ReleaseDate, Remark, Remark1, LastChgComp, LastChgID, LastChgDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @ReleaseMark, @CompID, @EmpID, @RelativeIDNo, @ApplyDate, @Reason, @OldData, @NewData, @ReleaseComp,")
            strSQL.AppendLine("    @ReleaseEmpID, @ReleaseDate, @Remark, @Remark1, @LastChgComp, @LastChgID, @LastChgDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            Using cn As DbConnection = db.CreateConnection()
                cn.Open()
                Dim tran As DbTransaction = cn.BeginTransaction()
                Dim inTrans As Boolean = True

                Try
                    For Each r As Row In PersonalWaitRow
                        dbcmd.Parameters.Clear()
                        db.AddInParameter(dbcmd, "@ReleaseMark", DbType.String, r.ReleaseMark.Value)
                        db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                        db.AddInParameter(dbcmd, "@EmpID", DbType.String, r.EmpID.Value)
                        db.AddInParameter(dbcmd, "@RelativeIDNo", DbType.String, r.RelativeIDNo.Value)
                        db.AddInParameter(dbcmd, "@ApplyDate", DbType.Date, IIf(IsDateTimeNull(r.ApplyDate.Value), Convert.ToDateTime("1900/1/1"), r.ApplyDate.Value))
                        db.AddInParameter(dbcmd, "@Reason", DbType.String, r.Reason.Value)
                        db.AddInParameter(dbcmd, "@OldData", DbType.String, r.OldData.Value)
                        db.AddInParameter(dbcmd, "@NewData", DbType.String, r.NewData.Value)
                        db.AddInParameter(dbcmd, "@ReleaseComp", DbType.String, r.ReleaseComp.Value)
                        db.AddInParameter(dbcmd, "@ReleaseEmpID", DbType.String, r.ReleaseEmpID.Value)
                        db.AddInParameter(dbcmd, "@ReleaseDate", DbType.Date, IIf(IsDateTimeNull(r.ReleaseDate.Value), Convert.ToDateTime("1900/1/1"), r.ReleaseDate.Value))
                        db.AddInParameter(dbcmd, "@Remark", DbType.String, r.Remark.Value)
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

        Public Function Insert(ByVal PersonalWaitRow As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Insert into PersonalWait")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    ReleaseMark, CompID, EmpID, RelativeIDNo, ApplyDate, Reason, OldData, NewData, ReleaseComp,")
            strSQL.AppendLine("    ReleaseEmpID, ReleaseDate, Remark, Remark1, LastChgComp, LastChgID, LastChgDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @ReleaseMark, @CompID, @EmpID, @RelativeIDNo, @ApplyDate, @Reason, @OldData, @NewData, @ReleaseComp,")
            strSQL.AppendLine("    @ReleaseEmpID, @ReleaseDate, @Remark, @Remark1, @LastChgComp, @LastChgID, @LastChgDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())

            For Each r As Row In PersonalWaitRow
                dbcmd.Parameters.Clear()
                db.AddInParameter(dbcmd, "@ReleaseMark", DbType.String, r.ReleaseMark.Value)
                db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                db.AddInParameter(dbcmd, "@EmpID", DbType.String, r.EmpID.Value)
                db.AddInParameter(dbcmd, "@RelativeIDNo", DbType.String, r.RelativeIDNo.Value)
                db.AddInParameter(dbcmd, "@ApplyDate", DbType.Date, IIf(IsDateTimeNull(r.ApplyDate.Value), Convert.ToDateTime("1900/1/1"), r.ApplyDate.Value))
                db.AddInParameter(dbcmd, "@Reason", DbType.String, r.Reason.Value)
                db.AddInParameter(dbcmd, "@OldData", DbType.String, r.OldData.Value)
                db.AddInParameter(dbcmd, "@NewData", DbType.String, r.NewData.Value)
                db.AddInParameter(dbcmd, "@ReleaseComp", DbType.String, r.ReleaseComp.Value)
                db.AddInParameter(dbcmd, "@ReleaseEmpID", DbType.String, r.ReleaseEmpID.Value)
                db.AddInParameter(dbcmd, "@ReleaseDate", DbType.Date, IIf(IsDateTimeNull(r.ReleaseDate.Value), Convert.ToDateTime("1900/1/1"), r.ReleaseDate.Value))
                db.AddInParameter(dbcmd, "@Remark", DbType.String, r.Remark.Value)
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

