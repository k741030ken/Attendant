'****************************************************************
' Table:PersonalLog
' Created Date: 2015.05.28
'****************************************************************/
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports Microsoft.Practices.EnterpriseLibrary.Common
Imports Microsoft.Practices.EnterpriseLibrary.Data
Imports System.Data.Common
Imports System.Data

Namespace bePersonalLog
    Public Class Table
        Private m_Rows As Rows = New Rows()
        Private m_Fields As String() = { "CompID", "EmpID", "RelativeIDNo", "Name", "BirthDate", "RelativeID", "ModifyDate", "Reason", "OldData", "NewData" _
                                    , "EffDate", "Remark" }
        Private m_Types As System.Type() = { GetType(String), GetType(String), GetType(String), GetType(String), GetType(Date), GetType(String), GetType(Date), GetType(String), GetType(String), GetType(String) _
                                    , GetType(Date), GetType(String) }
        Private m_PrimaryFields As String() = {  }

        Public ReadOnly Property Rows() As bePersonalLog.Rows 
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
        Public Sub Transfer2Row(PersonalLogTable As DataTable)
            For Each dr As DataRow In PersonalLogTable.Rows
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
                dr(m_Rows(i).RelativeIDNo.FieldName) = m_Rows(i).RelativeIDNo.Value
                dr(m_Rows(i).Name.FieldName) = m_Rows(i).Name.Value
                dr(m_Rows(i).BirthDate.FieldName) = m_Rows(i).BirthDate.Value
                dr(m_Rows(i).RelativeID.FieldName) = m_Rows(i).RelativeID.Value
                dr(m_Rows(i).ModifyDate.FieldName) = m_Rows(i).ModifyDate.Value
                dr(m_Rows(i).Reason.FieldName) = m_Rows(i).Reason.Value
                dr(m_Rows(i).OldData.FieldName) = m_Rows(i).OldData.Value
                dr(m_Rows(i).NewData.FieldName) = m_Rows(i).NewData.Value
                dr(m_Rows(i).EffDate.FieldName) = m_Rows(i).EffDate.Value
                dr(m_Rows(i).Remark.FieldName) = m_Rows(i).Remark.Value

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

        Public Sub Add(PersonalLogRow As Row)
            m_Rows.Add(PersonalLogRow)
        End Sub

        Public Sub Remove(PersonalLogRow As Row)
            If m_Rows.IndexOf(PersonalLogRow) >= 0 Then
                m_Rows.Remove(PersonalLogRow)
            End If
        End Sub

        Public Sub Dispose()
            m_Rows.Clear()
        End Sub

    End Class

    Public Class Row

        Private FI_CompID As Field(Of String) = new Field(Of String)("CompID", true)
        Private FI_EmpID As Field(Of String) = new Field(Of String)("EmpID", true)
        Private FI_RelativeIDNo As Field(Of String) = new Field(Of String)("RelativeIDNo", true)
        Private FI_Name As Field(Of String) = new Field(Of String)("Name", true)
        Private FI_BirthDate As Field(Of Date) = new Field(Of Date)("BirthDate", true)
        Private FI_RelativeID As Field(Of String) = new Field(Of String)("RelativeID", true)
        Private FI_ModifyDate As Field(Of Date) = new Field(Of Date)("ModifyDate", true)
        Private FI_Reason As Field(Of String) = new Field(Of String)("Reason", true)
        Private FI_OldData As Field(Of String) = new Field(Of String)("OldData", true)
        Private FI_NewData As Field(Of String) = new Field(Of String)("NewData", true)
        Private FI_EffDate As Field(Of Date) = new Field(Of Date)("EffDate", true)
        Private FI_Remark As Field(Of String) = new Field(Of String)("Remark", true)
        Private m_FieldNames As String() = { "CompID", "EmpID", "RelativeIDNo", "Name", "BirthDate", "RelativeID", "ModifyDate", "Reason", "OldData", "NewData" _
                                    , "EffDate", "Remark" }
        Private m_PrimaryFields As String() = {  }
        Private m_IdentityFields As String() = {  }
        Private m_LoadFromDataRow As Boolean = False

        Private Function GetFieldValue(ByVal fieldName As String) As Object
            Select Case fieldName
                Case "CompID"
                    Return FI_CompID.Value
                Case "EmpID"
                    Return FI_EmpID.Value
                Case "RelativeIDNo"
                    Return FI_RelativeIDNo.Value
                Case "Name"
                    Return FI_Name.Value
                Case "BirthDate"
                    Return FI_BirthDate.Value
                Case "RelativeID"
                    Return FI_RelativeID.Value
                Case "ModifyDate"
                    Return FI_ModifyDate.Value
                Case "Reason"
                    Return FI_Reason.Value
                Case "OldData"
                    Return FI_OldData.Value
                Case "NewData"
                    Return FI_NewData.Value
                Case "EffDate"
                    Return FI_EffDate.Value
                Case "Remark"
                    Return FI_Remark.Value
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
                Case "RelativeIDNo"
                    FI_RelativeIDNo.SetValue(value)
                Case "Name"
                    FI_Name.SetValue(value)
                Case "BirthDate"
                    FI_BirthDate.SetValue(value)
                Case "RelativeID"
                    FI_RelativeID.SetValue(value)
                Case "ModifyDate"
                    FI_ModifyDate.SetValue(value)
                Case "Reason"
                    FI_Reason.SetValue(value)
                Case "OldData"
                    FI_OldData.SetValue(value)
                Case "NewData"
                    FI_NewData.SetValue(value)
                Case "EffDate"
                    FI_EffDate.SetValue(value)
                Case "Remark"
                    FI_Remark.SetValue(value)
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
                Case "RelativeIDNo"
                    return FI_RelativeIDNo.Updated
                Case "Name"
                    return FI_Name.Updated
                Case "BirthDate"
                    return FI_BirthDate.Updated
                Case "RelativeID"
                    return FI_RelativeID.Updated
                Case "ModifyDate"
                    return FI_ModifyDate.Updated
                Case "Reason"
                    return FI_Reason.Updated
                Case "OldData"
                    return FI_OldData.Updated
                Case "NewData"
                    return FI_NewData.Updated
                Case "EffDate"
                    return FI_EffDate.Updated
                Case "Remark"
                    return FI_Remark.Updated
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
                Case "RelativeIDNo"
                    return FI_RelativeIDNo.CreateUpdateSQL
                Case "Name"
                    return FI_Name.CreateUpdateSQL
                Case "BirthDate"
                    return FI_BirthDate.CreateUpdateSQL
                Case "RelativeID"
                    return FI_RelativeID.CreateUpdateSQL
                Case "ModifyDate"
                    return FI_ModifyDate.CreateUpdateSQL
                Case "Reason"
                    return FI_Reason.CreateUpdateSQL
                Case "OldData"
                    return FI_OldData.CreateUpdateSQL
                Case "NewData"
                    return FI_NewData.CreateUpdateSQL
                Case "EffDate"
                    return FI_EffDate.CreateUpdateSQL
                Case "Remark"
                    return FI_Remark.CreateUpdateSQL
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
            FI_CompID.SetInitValue("SPHBK1")
            FI_EmpID.SetInitValue("")
            FI_RelativeIDNo.SetInitValue("")
            FI_Name.SetInitValue("")
            FI_BirthDate.SetInitValue(Convert.ToDateTime("1900/01/01"))
            FI_RelativeID.SetInitValue("")
            FI_ModifyDate.SetInitValue(Convert.ToDateTime("1900/01/01"))
            FI_Reason.SetInitValue("")
            FI_OldData.SetInitValue("")
            FI_NewData.SetInitValue("")
            FI_EffDate.SetInitValue(Convert.ToDateTime("1900/01/01"))
            FI_Remark.SetInitValue("")
        End Sub

        Public Sub New(ByVal dr As System.Data.DataRow)
            FI_CompID.SetInitValue(dr("CompID"))
            FI_EmpID.SetInitValue(dr("EmpID"))
            FI_RelativeIDNo.SetInitValue(dr("RelativeIDNo"))
            FI_Name.SetInitValue(dr("Name"))
            FI_BirthDate.SetInitValue(dr("BirthDate"))
            FI_RelativeID.SetInitValue(dr("RelativeID"))
            FI_ModifyDate.SetInitValue(dr("ModifyDate"))
            FI_Reason.SetInitValue(dr("Reason"))
            FI_OldData.SetInitValue(dr("OldData"))
            FI_NewData.SetInitValue(dr("NewData"))
            FI_EffDate.SetInitValue(dr("EffDate"))
            FI_Remark.SetInitValue(dr("Remark"))

            m_LoadFromDataRow = True
        End Sub

        Private Sub ClearUpdated()
            FI_CompID.Updated = False
            FI_EmpID.Updated = False
            FI_RelativeIDNo.Updated = False
            FI_Name.Updated = False
            FI_BirthDate.Updated = False
            FI_RelativeID.Updated = False
            FI_ModifyDate.Updated = False
            FI_Reason.Updated = False
            FI_OldData.Updated = False
            FI_NewData.Updated = False
            FI_EffDate.Updated = False
            FI_Remark.Updated = False
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

        Public ReadOnly Property RelativeIDNo As Field(Of String) 
            Get
                Return FI_RelativeIDNo
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

        Public ReadOnly Property RelativeID As Field(Of String) 
            Get
                Return FI_RelativeID
            End Get
        End Property

        Public ReadOnly Property ModifyDate As Field(Of Date) 
            Get
                Return FI_ModifyDate
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

        Public ReadOnly Property EffDate As Field(Of Date) 
            Get
                Return FI_EffDate
            End Get
        End Property

        Public ReadOnly Property Remark As Field(Of String) 
            Get
                Return FI_Remark
            End Get
        End Property

    End Class

    Public Class Service
        Public Function QuerybyWhere(WhereCondition As String) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From PersonalLog")
            If WhereCondition <> String.Empty Then strSQL.AppendLine(WhereCondition)

            Return db.ExecuteDataSet(CommandType.Text, strSQL.ToString())
        End Function

        Public Function Insert(ByVal PersonalLogRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Insert into PersonalLog")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    CompID, EmpID, RelativeIDNo, Name, BirthDate, RelativeID, ModifyDate, Reason, OldData,")
            strSQL.AppendLine("    NewData, EffDate, Remark")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @CompID, @EmpID, @RelativeIDNo, @Name, @BirthDate, @RelativeID, @ModifyDate, @Reason, @OldData,")
            strSQL.AppendLine("    @NewData, @EffDate, @Remark")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, PersonalLogRow.CompID.Value)
            db.AddInParameter(dbcmd, "@EmpID", DbType.String, PersonalLogRow.EmpID.Value)
            db.AddInParameter(dbcmd, "@RelativeIDNo", DbType.String, PersonalLogRow.RelativeIDNo.Value)
            db.AddInParameter(dbcmd, "@Name", DbType.String, PersonalLogRow.Name.Value)
            db.AddInParameter(dbcmd, "@BirthDate", DbType.Date, IIf(IsDateTimeNull(PersonalLogRow.BirthDate.Value), Convert.ToDateTime("1900/1/1"), PersonalLogRow.BirthDate.Value))
            db.AddInParameter(dbcmd, "@RelativeID", DbType.String, PersonalLogRow.RelativeID.Value)
            db.AddInParameter(dbcmd, "@ModifyDate", DbType.Date, IIf(IsDateTimeNull(PersonalLogRow.ModifyDate.Value), Convert.ToDateTime("1900/1/1"), PersonalLogRow.ModifyDate.Value))
            db.AddInParameter(dbcmd, "@Reason", DbType.String, PersonalLogRow.Reason.Value)
            db.AddInParameter(dbcmd, "@OldData", DbType.String, PersonalLogRow.OldData.Value)
            db.AddInParameter(dbcmd, "@NewData", DbType.String, PersonalLogRow.NewData.Value)
            db.AddInParameter(dbcmd, "@EffDate", DbType.Date, IIf(IsDateTimeNull(PersonalLogRow.EffDate.Value), Convert.ToDateTime("1900/1/1"), PersonalLogRow.EffDate.Value))
            db.AddInParameter(dbcmd, "@Remark", DbType.String, PersonalLogRow.Remark.Value)

            Return db.ExecuteNonQuery(dbcmd)
        End Function

        Public Function Insert(ByVal PersonalLogRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Insert into PersonalLog")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    CompID, EmpID, RelativeIDNo, Name, BirthDate, RelativeID, ModifyDate, Reason, OldData,")
            strSQL.AppendLine("    NewData, EffDate, Remark")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @CompID, @EmpID, @RelativeIDNo, @Name, @BirthDate, @RelativeID, @ModifyDate, @Reason, @OldData,")
            strSQL.AppendLine("    @NewData, @EffDate, @Remark")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, PersonalLogRow.CompID.Value)
            db.AddInParameter(dbcmd, "@EmpID", DbType.String, PersonalLogRow.EmpID.Value)
            db.AddInParameter(dbcmd, "@RelativeIDNo", DbType.String, PersonalLogRow.RelativeIDNo.Value)
            db.AddInParameter(dbcmd, "@Name", DbType.String, PersonalLogRow.Name.Value)
            db.AddInParameter(dbcmd, "@BirthDate", DbType.Date, IIf(IsDateTimeNull(PersonalLogRow.BirthDate.Value), Convert.ToDateTime("1900/1/1"), PersonalLogRow.BirthDate.Value))
            db.AddInParameter(dbcmd, "@RelativeID", DbType.String, PersonalLogRow.RelativeID.Value)
            db.AddInParameter(dbcmd, "@ModifyDate", DbType.Date, IIf(IsDateTimeNull(PersonalLogRow.ModifyDate.Value), Convert.ToDateTime("1900/1/1"), PersonalLogRow.ModifyDate.Value))
            db.AddInParameter(dbcmd, "@Reason", DbType.String, PersonalLogRow.Reason.Value)
            db.AddInParameter(dbcmd, "@OldData", DbType.String, PersonalLogRow.OldData.Value)
            db.AddInParameter(dbcmd, "@NewData", DbType.String, PersonalLogRow.NewData.Value)
            db.AddInParameter(dbcmd, "@EffDate", DbType.Date, IIf(IsDateTimeNull(PersonalLogRow.EffDate.Value), Convert.ToDateTime("1900/1/1"), PersonalLogRow.EffDate.Value))
            db.AddInParameter(dbcmd, "@Remark", DbType.String, PersonalLogRow.Remark.Value)

            Return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function Insert(ByVal PersonalLogRow As Row()) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Insert into PersonalLog")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    CompID, EmpID, RelativeIDNo, Name, BirthDate, RelativeID, ModifyDate, Reason, OldData,")
            strSQL.AppendLine("    NewData, EffDate, Remark")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @CompID, @EmpID, @RelativeIDNo, @Name, @BirthDate, @RelativeID, @ModifyDate, @Reason, @OldData,")
            strSQL.AppendLine("    @NewData, @EffDate, @Remark")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            Using cn As DbConnection = db.CreateConnection()
                cn.Open()
                Dim tran As DbTransaction = cn.BeginTransaction()
                Dim inTrans As Boolean = True

                Try
                    For Each r As Row In PersonalLogRow
                        dbcmd.Parameters.Clear()
                        db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                        db.AddInParameter(dbcmd, "@EmpID", DbType.String, r.EmpID.Value)
                        db.AddInParameter(dbcmd, "@RelativeIDNo", DbType.String, r.RelativeIDNo.Value)
                        db.AddInParameter(dbcmd, "@Name", DbType.String, r.Name.Value)
                        db.AddInParameter(dbcmd, "@BirthDate", DbType.Date, IIf(IsDateTimeNull(r.BirthDate.Value), Convert.ToDateTime("1900/1/1"), r.BirthDate.Value))
                        db.AddInParameter(dbcmd, "@RelativeID", DbType.String, r.RelativeID.Value)
                        db.AddInParameter(dbcmd, "@ModifyDate", DbType.Date, IIf(IsDateTimeNull(r.ModifyDate.Value), Convert.ToDateTime("1900/1/1"), r.ModifyDate.Value))
                        db.AddInParameter(dbcmd, "@Reason", DbType.String, r.Reason.Value)
                        db.AddInParameter(dbcmd, "@OldData", DbType.String, r.OldData.Value)
                        db.AddInParameter(dbcmd, "@NewData", DbType.String, r.NewData.Value)
                        db.AddInParameter(dbcmd, "@EffDate", DbType.Date, IIf(IsDateTimeNull(r.EffDate.Value), Convert.ToDateTime("1900/1/1"), r.EffDate.Value))
                        db.AddInParameter(dbcmd, "@Remark", DbType.String, r.Remark.Value)

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

        Public Function Insert(ByVal PersonalLogRow As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Insert into PersonalLog")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    CompID, EmpID, RelativeIDNo, Name, BirthDate, RelativeID, ModifyDate, Reason, OldData,")
            strSQL.AppendLine("    NewData, EffDate, Remark")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @CompID, @EmpID, @RelativeIDNo, @Name, @BirthDate, @RelativeID, @ModifyDate, @Reason, @OldData,")
            strSQL.AppendLine("    @NewData, @EffDate, @Remark")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())

            For Each r As Row In PersonalLogRow
                dbcmd.Parameters.Clear()
                db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                db.AddInParameter(dbcmd, "@EmpID", DbType.String, r.EmpID.Value)
                db.AddInParameter(dbcmd, "@RelativeIDNo", DbType.String, r.RelativeIDNo.Value)
                db.AddInParameter(dbcmd, "@Name", DbType.String, r.Name.Value)
                db.AddInParameter(dbcmd, "@BirthDate", DbType.Date, IIf(IsDateTimeNull(r.BirthDate.Value), Convert.ToDateTime("1900/1/1"), r.BirthDate.Value))
                db.AddInParameter(dbcmd, "@RelativeID", DbType.String, r.RelativeID.Value)
                db.AddInParameter(dbcmd, "@ModifyDate", DbType.Date, IIf(IsDateTimeNull(r.ModifyDate.Value), Convert.ToDateTime("1900/1/1"), r.ModifyDate.Value))
                db.AddInParameter(dbcmd, "@Reason", DbType.String, r.Reason.Value)
                db.AddInParameter(dbcmd, "@OldData", DbType.String, r.OldData.Value)
                db.AddInParameter(dbcmd, "@NewData", DbType.String, r.NewData.Value)
                db.AddInParameter(dbcmd, "@EffDate", DbType.Date, IIf(IsDateTimeNull(r.EffDate.Value), Convert.ToDateTime("1900/1/1"), r.EffDate.Value))
                db.AddInParameter(dbcmd, "@Remark", DbType.String, r.Remark.Value)

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

