'****************************************************************
' Table:Training
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

Namespace beTraining
    Public Class Table
        Private m_Rows As Rows = New Rows()
        Private m_Fields As String() = { "IDNo", "BeginDate", "LessonName", "LessonID", "ActivityID", "EndDate", "Hours", "LessonUnit", "Fee", "DeptName" _
                                    , "KindName", "LastChgDate" }
        Private m_Types As System.Type() = { GetType(String), GetType(Date), GetType(String), GetType(String), GetType(String), GetType(Date), GetType(Decimal), GetType(String), GetType(Decimal), GetType(String) _
                                    , GetType(String), GetType(Date) }
        Private m_PrimaryFields As String() = { "IDNo", "BeginDate", "LessonName", "LessonID", "ActivityID" }

        Public ReadOnly Property Rows() As beTraining.Rows 
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
        Public Sub Transfer2Row(TrainingTable As DataTable)
            For Each dr As DataRow In TrainingTable.Rows
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
                dr(m_Rows(i).BeginDate.FieldName) = m_Rows(i).BeginDate.Value
                dr(m_Rows(i).LessonName.FieldName) = m_Rows(i).LessonName.Value
                dr(m_Rows(i).LessonID.FieldName) = m_Rows(i).LessonID.Value
                dr(m_Rows(i).ActivityID.FieldName) = m_Rows(i).ActivityID.Value
                dr(m_Rows(i).EndDate.FieldName) = m_Rows(i).EndDate.Value
                dr(m_Rows(i).Hours.FieldName) = m_Rows(i).Hours.Value
                dr(m_Rows(i).LessonUnit.FieldName) = m_Rows(i).LessonUnit.Value
                dr(m_Rows(i).Fee.FieldName) = m_Rows(i).Fee.Value
                dr(m_Rows(i).DeptName.FieldName) = m_Rows(i).DeptName.Value
                dr(m_Rows(i).KindName.FieldName) = m_Rows(i).KindName.Value
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

        Public Sub Add(TrainingRow As Row)
            m_Rows.Add(TrainingRow)
        End Sub

        Public Sub Remove(TrainingRow As Row)
            If m_Rows.IndexOf(TrainingRow) >= 0 Then
                m_Rows.Remove(TrainingRow)
            End If
        End Sub

        Public Sub Dispose()
            m_Rows.Clear()
        End Sub

    End Class

    Public Class Row

        Private FI_IDNo As Field(Of String) = new Field(Of String)("IDNo", true)
        Private FI_BeginDate As Field(Of Date) = new Field(Of Date)("BeginDate", true)
        Private FI_LessonName As Field(Of String) = new Field(Of String)("LessonName", true)
        Private FI_LessonID As Field(Of String) = new Field(Of String)("LessonID", true)
        Private FI_ActivityID As Field(Of String) = new Field(Of String)("ActivityID", true)
        Private FI_EndDate As Field(Of Date) = new Field(Of Date)("EndDate", true)
        Private FI_Hours As Field(Of Decimal) = new Field(Of Decimal)("Hours", true)
        Private FI_LessonUnit As Field(Of String) = new Field(Of String)("LessonUnit", true)
        Private FI_Fee As Field(Of Decimal) = new Field(Of Decimal)("Fee", true)
        Private FI_DeptName As Field(Of String) = new Field(Of String)("DeptName", true)
        Private FI_KindName As Field(Of String) = new Field(Of String)("KindName", true)
        Private FI_LastChgDate As Field(Of Date) = new Field(Of Date)("LastChgDate", true)
        Private m_FieldNames As String() = { "IDNo", "BeginDate", "LessonName", "LessonID", "ActivityID", "EndDate", "Hours", "LessonUnit", "Fee", "DeptName" _
                                    , "KindName", "LastChgDate" }
        Private m_PrimaryFields As String() = { "IDNo", "BeginDate", "LessonName", "LessonID", "ActivityID" }
        Private m_IdentityFields As String() = {  }
        Private m_LoadFromDataRow As Boolean = False

        Private Function GetFieldValue(ByVal fieldName As String) As Object
            Select Case fieldName
                Case "IDNo"
                    Return FI_IDNo.Value
                Case "BeginDate"
                    Return FI_BeginDate.Value
                Case "LessonName"
                    Return FI_LessonName.Value
                Case "LessonID"
                    Return FI_LessonID.Value
                Case "ActivityID"
                    Return FI_ActivityID.Value
                Case "EndDate"
                    Return FI_EndDate.Value
                Case "Hours"
                    Return FI_Hours.Value
                Case "LessonUnit"
                    Return FI_LessonUnit.Value
                Case "Fee"
                    Return FI_Fee.Value
                Case "DeptName"
                    Return FI_DeptName.Value
                Case "KindName"
                    Return FI_KindName.Value
                Case "LastChgDate"
                    Return FI_LastChgDate.Value
                Case Else
                    Return Nothing
            End Select
        End Function

        Private Sub SetFieldValue(ByVal fieldName As String, ByVal value As Object)
            Select Case fieldName
                Case "IDNo"
                    FI_IDNo.SetValue(value)
                Case "BeginDate"
                    FI_BeginDate.SetValue(value)
                Case "LessonName"
                    FI_LessonName.SetValue(value)
                Case "LessonID"
                    FI_LessonID.SetValue(value)
                Case "ActivityID"
                    FI_ActivityID.SetValue(value)
                Case "EndDate"
                    FI_EndDate.SetValue(value)
                Case "Hours"
                    FI_Hours.SetValue(value)
                Case "LessonUnit"
                    FI_LessonUnit.SetValue(value)
                Case "Fee"
                    FI_Fee.SetValue(value)
                Case "DeptName"
                    FI_DeptName.SetValue(value)
                Case "KindName"
                    FI_KindName.SetValue(value)
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
                Case "IDNo"
                    return FI_IDNo.Updated
                Case "BeginDate"
                    return FI_BeginDate.Updated
                Case "LessonName"
                    return FI_LessonName.Updated
                Case "LessonID"
                    return FI_LessonID.Updated
                Case "ActivityID"
                    return FI_ActivityID.Updated
                Case "EndDate"
                    return FI_EndDate.Updated
                Case "Hours"
                    return FI_Hours.Updated
                Case "LessonUnit"
                    return FI_LessonUnit.Updated
                Case "Fee"
                    return FI_Fee.Updated
                Case "DeptName"
                    return FI_DeptName.Updated
                Case "KindName"
                    return FI_KindName.Updated
                Case "LastChgDate"
                    return FI_LastChgDate.Updated
                Case Else
                    Throw New Exception("無此欄位！")
            End Select
        End Function

        Public Function CreateUpdateSQL(ByVal fieldName As String) As Boolean
            Select Case fieldName
                Case "IDNo"
                    return FI_IDNo.CreateUpdateSQL
                Case "BeginDate"
                    return FI_BeginDate.CreateUpdateSQL
                Case "LessonName"
                    return FI_LessonName.CreateUpdateSQL
                Case "LessonID"
                    return FI_LessonID.CreateUpdateSQL
                Case "ActivityID"
                    return FI_ActivityID.CreateUpdateSQL
                Case "EndDate"
                    return FI_EndDate.CreateUpdateSQL
                Case "Hours"
                    return FI_Hours.CreateUpdateSQL
                Case "LessonUnit"
                    return FI_LessonUnit.CreateUpdateSQL
                Case "Fee"
                    return FI_Fee.CreateUpdateSQL
                Case "DeptName"
                    return FI_DeptName.CreateUpdateSQL
                Case "KindName"
                    return FI_KindName.CreateUpdateSQL
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
            FI_IDNo.SetInitValue("")
            FI_BeginDate.SetInitValue(Convert.ToDateTime("1900/01/01"))
            FI_LessonName.SetInitValue("")
            FI_LessonID.SetInitValue("")
            FI_ActivityID.SetInitValue("")
            FI_EndDate.SetInitValue(Convert.ToDateTime("1900/01/01"))
            FI_Hours.SetInitValue(0)
            FI_LessonUnit.SetInitValue("")
            FI_Fee.SetInitValue(0)
            FI_DeptName.SetInitValue("")
            FI_KindName.SetInitValue("")
            FI_LastChgDate.SetInitValue(Convert.ToDateTime("1900/01/01"))
        End Sub

        Public Sub New(ByVal dr As System.Data.DataRow)
            FI_IDNo.SetInitValue(dr("IDNo"))
            FI_BeginDate.SetInitValue(dr("BeginDate"))
            FI_LessonName.SetInitValue(dr("LessonName"))
            FI_LessonID.SetInitValue(dr("LessonID"))
            FI_ActivityID.SetInitValue(dr("ActivityID"))
            FI_EndDate.SetInitValue(dr("EndDate"))
            FI_Hours.SetInitValue(dr("Hours"))
            FI_LessonUnit.SetInitValue(dr("LessonUnit"))
            FI_Fee.SetInitValue(dr("Fee"))
            FI_DeptName.SetInitValue(dr("DeptName"))
            FI_KindName.SetInitValue(dr("KindName"))
            FI_LastChgDate.SetInitValue(dr("LastChgDate"))

            m_LoadFromDataRow = True
        End Sub

        Private Sub ClearUpdated()
            FI_IDNo.Updated = False
            FI_BeginDate.Updated = False
            FI_LessonName.Updated = False
            FI_LessonID.Updated = False
            FI_ActivityID.Updated = False
            FI_EndDate.Updated = False
            FI_Hours.Updated = False
            FI_LessonUnit.Updated = False
            FI_Fee.Updated = False
            FI_DeptName.Updated = False
            FI_KindName.Updated = False
            FI_LastChgDate.Updated = False
        End Sub

        Public ReadOnly Property IDNo As Field(Of String) 
            Get
                Return FI_IDNo
            End Get
        End Property

        Public ReadOnly Property BeginDate As Field(Of Date) 
            Get
                Return FI_BeginDate
            End Get
        End Property

        Public ReadOnly Property LessonName As Field(Of String) 
            Get
                Return FI_LessonName
            End Get
        End Property

        Public ReadOnly Property LessonID As Field(Of String) 
            Get
                Return FI_LessonID
            End Get
        End Property

        Public ReadOnly Property ActivityID As Field(Of String) 
            Get
                Return FI_ActivityID
            End Get
        End Property

        Public ReadOnly Property EndDate As Field(Of Date) 
            Get
                Return FI_EndDate
            End Get
        End Property

        Public ReadOnly Property Hours As Field(Of Decimal) 
            Get
                Return FI_Hours
            End Get
        End Property

        Public ReadOnly Property LessonUnit As Field(Of String) 
            Get
                Return FI_LessonUnit
            End Get
        End Property

        Public ReadOnly Property Fee As Field(Of Decimal) 
            Get
                Return FI_Fee
            End Get
        End Property

        Public ReadOnly Property DeptName As Field(Of String) 
            Get
                Return FI_DeptName
            End Get
        End Property

        Public ReadOnly Property KindName As Field(Of String) 
            Get
                Return FI_KindName
            End Get
        End Property

        Public ReadOnly Property LastChgDate As Field(Of Date) 
            Get
                Return FI_LastChgDate
            End Get
        End Property

    End Class

    Public Class Service
        Public Function DeleteRowByPrimaryKey(ByVal TrainingRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Delete From Training")
            strSQL.AppendLine("Where IDNo = @IDNo")
            strSQL.AppendLine("And BeginDate = @BeginDate")
            strSQL.AppendLine("And LessonName = @LessonName")
            strSQL.AppendLine("And LessonID = @LessonID")
            strSQL.AppendLine("And ActivityID = @ActivityID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@IDNo", DbType.String, TrainingRow.IDNo.Value)
            db.AddInParameter(dbcmd, "@BeginDate", DbType.Date, TrainingRow.BeginDate.Value)
            db.AddInParameter(dbcmd, "@LessonName", DbType.String, TrainingRow.LessonName.Value)
            db.AddInParameter(dbcmd, "@LessonID", DbType.String, TrainingRow.LessonID.Value)
            db.AddInParameter(dbcmd, "@ActivityID", DbType.String, TrainingRow.ActivityID.Value)

            return db.ExecuteNonQuery(dbcmd)
        End Function

        public Function DeleteRowByPrimaryKey(ByVal TrainingRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Delete From Training")
            strSQL.AppendLine("Where IDNo = @IDNo")
            strSQL.AppendLine("And BeginDate = @BeginDate")
            strSQL.AppendLine("And LessonName = @LessonName")
            strSQL.AppendLine("And LessonID = @LessonID")
            strSQL.AppendLine("And ActivityID = @ActivityID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@IDNo", DbType.String, TrainingRow.IDNo.Value)
            db.AddInParameter(dbcmd, "@BeginDate", DbType.Date, TrainingRow.BeginDate.Value)
            db.AddInParameter(dbcmd, "@LessonName", DbType.String, TrainingRow.LessonName.Value)
            db.AddInParameter(dbcmd, "@LessonID", DbType.String, TrainingRow.LessonID.Value)
            db.AddInParameter(dbcmd, "@ActivityID", DbType.String, TrainingRow.ActivityID.Value)

            return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function DeleteRowByPrimaryKey(ByVal TrainingRow As Row()) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Delete From Training")
            strSQL.AppendLine("Where IDNo = @IDNo")
            strSQL.AppendLine("And BeginDate = @BeginDate")
            strSQL.AppendLine("And LessonName = @LessonName")
            strSQL.AppendLine("And LessonID = @LessonID")
            strSQL.AppendLine("And ActivityID = @ActivityID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            using cn As DbConnection = db.CreateConnection()
                cn.Open()
                Dim tran As DbTransaction = cn.BeginTransaction()
                Dim inTrans As Boolean = True

                Try
                    For Each r As Row In TrainingRow
                        dbcmd.Parameters.Clear()
                        db.AddInParameter(dbcmd, "@IDNo", DbType.String, r.IDNo.Value)
                        db.AddInParameter(dbcmd, "@BeginDate", DbType.Date, r.BeginDate.Value)
                        db.AddInParameter(dbcmd, "@LessonName", DbType.String, r.LessonName.Value)
                        db.AddInParameter(dbcmd, "@LessonID", DbType.String, r.LessonID.Value)
                        db.AddInParameter(dbcmd, "@ActivityID", DbType.String, r.ActivityID.Value)

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

        Public Function DeleteRowByPrimaryKey(ByVal TrainingRow As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Delete From Training")
            strSQL.AppendLine("Where IDNo = @IDNo")
            strSQL.AppendLine("And BeginDate = @BeginDate")
            strSQL.AppendLine("And LessonName = @LessonName")
            strSQL.AppendLine("And LessonID = @LessonID")
            strSQL.AppendLine("And ActivityID = @ActivityID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())

            For Each r As Row In TrainingRow
                dbcmd.Parameters.Clear()
                db.AddInParameter(dbcmd, "@IDNo", DbType.String, r.IDNo.Value)
                db.AddInParameter(dbcmd, "@BeginDate", DbType.Date, r.BeginDate.Value)
                db.AddInParameter(dbcmd, "@LessonName", DbType.String, r.LessonName.Value)
                db.AddInParameter(dbcmd, "@LessonID", DbType.String, r.LessonID.Value)
                db.AddInParameter(dbcmd, "@ActivityID", DbType.String, r.ActivityID.Value)

                intRowsAffected += db.ExecuteNonQuery(dbcmd, tran)
            Next
            Return intRowsAffected
        End Function

        Public Function QueryByKey(ByVal TrainingRow As Row) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From Training")
            strSQL.AppendLine("Where IDNo = @IDNo")
            strSQL.AppendLine("And BeginDate = @BeginDate")
            strSQL.AppendLine("And LessonName = @LessonName")
            strSQL.AppendLine("And LessonID = @LessonID")
            strSQL.AppendLine("And ActivityID = @ActivityID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@IDNo", DbType.String, TrainingRow.IDNo.Value)
            db.AddInParameter(dbcmd, "@BeginDate", DbType.Date, TrainingRow.BeginDate.Value)
            db.AddInParameter(dbcmd, "@LessonName", DbType.String, TrainingRow.LessonName.Value)
            db.AddInParameter(dbcmd, "@LessonID", DbType.String, TrainingRow.LessonID.Value)
            db.AddInParameter(dbcmd, "@ActivityID", DbType.String, TrainingRow.ActivityID.Value)

            Return db.ExecuteDataSet(dbcmd)
        End Function

        Public Function QueryByKey(TrainingRow As Row, tran As DbTransaction) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From Training")
            strSQL.AppendLine("Where IDNo = @IDNo")
            strSQL.AppendLine("And BeginDate = @BeginDate")
            strSQL.AppendLine("And LessonName = @LessonName")
            strSQL.AppendLine("And LessonID = @LessonID")
            strSQL.AppendLine("And ActivityID = @ActivityID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@IDNo", DbType.String, TrainingRow.IDNo.Value)
            db.AddInParameter(dbcmd, "@BeginDate", DbType.Date, TrainingRow.BeginDate.Value)
            db.AddInParameter(dbcmd, "@LessonName", DbType.String, TrainingRow.LessonName.Value)
            db.AddInParameter(dbcmd, "@LessonID", DbType.String, TrainingRow.LessonID.Value)
            db.AddInParameter(dbcmd, "@ActivityID", DbType.String, TrainingRow.ActivityID.Value)

            Return db.ExecuteDataSet(dbcmd, tran)
        End Function

        Public Function Update(ByVal TrainingRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim strDot As String = String.Empty

            strSQL.AppendLine("Update Training Set")
            For i As Integer = 0 To TrainingRow.FieldNames.Length - 1
                If Not TrainingRow.IsIdentityField(TrainingRow.FieldNames(i)) AndAlso TrainingRow.IsUpdated(TrainingRow.FieldNames(i)) AndAlso TrainingRow.CreateUpdateSQL(TrainingRow.FieldNames(i)) Then
                    strSQL.AppendLine(string.Format("{0}{1} = @{1}", strDot, TrainingRow.FieldNames(i)))
                    strDot = ","
                End If
            Next
            If strDot = String.Empty Then Throw New Exception("未異動資料欄位！")
            strSQL.AppendLine("Where IDNo = @PKIDNo")
            strSQL.AppendLine("And BeginDate = @PKBeginDate")
            strSQL.AppendLine("And LessonName = @PKLessonName")
            strSQL.AppendLine("And LessonID = @PKLessonID")
            strSQL.AppendLine("And ActivityID = @PKActivityID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            If TrainingRow.IDNo.Updated Then db.AddInParameter(dbcmd, "@IDNo", DbType.String, TrainingRow.IDNo.Value)
            If TrainingRow.BeginDate.Updated Then db.AddInParameter(dbcmd, "@BeginDate", DbType.Date, IIf(IsDateTimeNull(TrainingRow.BeginDate.Value), Convert.ToDateTime("1900/1/1"), TrainingRow.BeginDate.Value))
            If TrainingRow.LessonName.Updated Then db.AddInParameter(dbcmd, "@LessonName", DbType.String, TrainingRow.LessonName.Value)
            If TrainingRow.LessonID.Updated Then db.AddInParameter(dbcmd, "@LessonID", DbType.String, TrainingRow.LessonID.Value)
            If TrainingRow.ActivityID.Updated Then db.AddInParameter(dbcmd, "@ActivityID", DbType.String, TrainingRow.ActivityID.Value)
            If TrainingRow.EndDate.Updated Then db.AddInParameter(dbcmd, "@EndDate", DbType.Date, IIf(IsDateTimeNull(TrainingRow.EndDate.Value), Convert.ToDateTime("1900/1/1"), TrainingRow.EndDate.Value))
            If TrainingRow.Hours.Updated Then db.AddInParameter(dbcmd, "@Hours", DbType.Decimal, TrainingRow.Hours.Value)
            If TrainingRow.LessonUnit.Updated Then db.AddInParameter(dbcmd, "@LessonUnit", DbType.String, TrainingRow.LessonUnit.Value)
            If TrainingRow.Fee.Updated Then db.AddInParameter(dbcmd, "@Fee", DbType.Decimal, TrainingRow.Fee.Value)
            If TrainingRow.DeptName.Updated Then db.AddInParameter(dbcmd, "@DeptName", DbType.String, TrainingRow.DeptName.Value)
            If TrainingRow.KindName.Updated Then db.AddInParameter(dbcmd, "@KindName", DbType.String, TrainingRow.KindName.Value)
            If TrainingRow.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(TrainingRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), TrainingRow.LastChgDate.Value))
            db.AddInParameter(dbcmd, "@PKIDNo", DbType.String, IIf(TrainingRow.LoadFromDataRow, TrainingRow.IDNo.OldValue, TrainingRow.IDNo.Value))
            db.AddInParameter(dbcmd, "@PKBeginDate", DbType.Date, IIf(TrainingRow.LoadFromDataRow, TrainingRow.BeginDate.OldValue, TrainingRow.BeginDate.Value))
            db.AddInParameter(dbcmd, "@PKLessonName", DbType.String, IIf(TrainingRow.LoadFromDataRow, TrainingRow.LessonName.OldValue, TrainingRow.LessonName.Value))
            db.AddInParameter(dbcmd, "@PKLessonID", DbType.String, IIf(TrainingRow.LoadFromDataRow, TrainingRow.LessonID.OldValue, TrainingRow.LessonID.Value))
            db.AddInParameter(dbcmd, "@PKActivityID", DbType.String, IIf(TrainingRow.LoadFromDataRow, TrainingRow.ActivityID.OldValue, TrainingRow.ActivityID.Value))

            Return db.ExecuteNonQuery(dbcmd)
        End Function

        Public Function Update(ByVal TrainingRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim strDot As String = String.Empty

            strSQL.AppendLine("Update Training Set")
            For i As Integer = 0 To TrainingRow.FieldNames.Length - 1
                If Not TrainingRow.IsIdentityField(TrainingRow.FieldNames(i)) AndAlso TrainingRow.IsUpdated(TrainingRow.FieldNames(i)) AndAlso TrainingRow.CreateUpdateSQL(TrainingRow.FieldNames(i)) Then
                    strSQL.AppendLine(string.Format("{0}{1} = @{1}", strDot, TrainingRow.FieldNames(i)))
                    strDot = ","
                End If
            Next 
            If strDot = String.Empty Then Throw New Exception("未異動資料欄位！")
            strSQL.AppendLine("Where IDNo = @PKIDNo")
            strSQL.AppendLine("And BeginDate = @PKBeginDate")
            strSQL.AppendLine("And LessonName = @PKLessonName")
            strSQL.AppendLine("And LessonID = @PKLessonID")
            strSQL.AppendLine("And ActivityID = @PKActivityID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            If TrainingRow.IDNo.Updated Then db.AddInParameter(dbcmd, "@IDNo", DbType.String, TrainingRow.IDNo.Value)
            If TrainingRow.BeginDate.Updated Then db.AddInParameter(dbcmd, "@BeginDate", DbType.Date, IIf(IsDateTimeNull(TrainingRow.BeginDate.Value), Convert.ToDateTime("1900/1/1"), TrainingRow.BeginDate.Value))
            If TrainingRow.LessonName.Updated Then db.AddInParameter(dbcmd, "@LessonName", DbType.String, TrainingRow.LessonName.Value)
            If TrainingRow.LessonID.Updated Then db.AddInParameter(dbcmd, "@LessonID", DbType.String, TrainingRow.LessonID.Value)
            If TrainingRow.ActivityID.Updated Then db.AddInParameter(dbcmd, "@ActivityID", DbType.String, TrainingRow.ActivityID.Value)
            If TrainingRow.EndDate.Updated Then db.AddInParameter(dbcmd, "@EndDate", DbType.Date, IIf(IsDateTimeNull(TrainingRow.EndDate.Value), Convert.ToDateTime("1900/1/1"), TrainingRow.EndDate.Value))
            If TrainingRow.Hours.Updated Then db.AddInParameter(dbcmd, "@Hours", DbType.Decimal, TrainingRow.Hours.Value)
            If TrainingRow.LessonUnit.Updated Then db.AddInParameter(dbcmd, "@LessonUnit", DbType.String, TrainingRow.LessonUnit.Value)
            If TrainingRow.Fee.Updated Then db.AddInParameter(dbcmd, "@Fee", DbType.Decimal, TrainingRow.Fee.Value)
            If TrainingRow.DeptName.Updated Then db.AddInParameter(dbcmd, "@DeptName", DbType.String, TrainingRow.DeptName.Value)
            If TrainingRow.KindName.Updated Then db.AddInParameter(dbcmd, "@KindName", DbType.String, TrainingRow.KindName.Value)
            If TrainingRow.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(TrainingRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), TrainingRow.LastChgDate.Value))
            db.AddInParameter(dbcmd, "@PKIDNo", DbType.String, IIf(TrainingRow.LoadFromDataRow, TrainingRow.IDNo.OldValue, TrainingRow.IDNo.Value))
            db.AddInParameter(dbcmd, "@PKBeginDate", DbType.Date, IIf(TrainingRow.LoadFromDataRow, TrainingRow.BeginDate.OldValue, TrainingRow.BeginDate.Value))
            db.AddInParameter(dbcmd, "@PKLessonName", DbType.String, IIf(TrainingRow.LoadFromDataRow, TrainingRow.LessonName.OldValue, TrainingRow.LessonName.Value))
            db.AddInParameter(dbcmd, "@PKLessonID", DbType.String, IIf(TrainingRow.LoadFromDataRow, TrainingRow.LessonID.OldValue, TrainingRow.LessonID.Value))
            db.AddInParameter(dbcmd, "@PKActivityID", DbType.String, IIf(TrainingRow.LoadFromDataRow, TrainingRow.ActivityID.OldValue, TrainingRow.ActivityID.Value))

            Return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function Update(ByVal TrainingRow As Row()) As Integer
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
                    For Each r As Row In TrainingRow
                        strDot = String.Empty
                        If strSQL.Length > 0 Then strSQL.Remove(0, strSQL.Length)
                        strSQL.AppendLine("Update Training Set")
                        For i As Integer = 0 To r.FieldNames.Length - 1
                            If Not r.IsIdentityField(r.FieldNames(i)) AndAlso r.IsUpdated(r.FieldNames(i)) AndAlso r.CreateUpdateSQL(r.FieldNames(i)) Then
                                strSQL.AppendLine(String.Format("{0}{1} = @{1}", strDot, r.FieldNames(i)))
                                strDot = ","
                            End If
                        Next
                        If strDot = String.Empty Then Continue For
                        strSQL.AppendLine("Where IDNo = @PKIDNo")
                        strSQL.AppendLine("And BeginDate = @PKBeginDate")
                        strSQL.AppendLine("And LessonName = @PKLessonName")
                        strSQL.AppendLine("And LessonID = @PKLessonID")
                        strSQL.AppendLine("And ActivityID = @PKActivityID")

                        dbcmd = db.GetSqlStringCommand(strSQL.ToString())
                        If r.IDNo.Updated Then db.AddInParameter(dbcmd, "@IDNo", DbType.String, r.IDNo.Value)
                        If r.BeginDate.Updated Then db.AddInParameter(dbcmd, "@BeginDate", DbType.Date, IIf(IsDateTimeNull(r.BeginDate.Value), Convert.ToDateTime("1900/1/1"), r.BeginDate.Value))
                        If r.LessonName.Updated Then db.AddInParameter(dbcmd, "@LessonName", DbType.String, r.LessonName.Value)
                        If r.LessonID.Updated Then db.AddInParameter(dbcmd, "@LessonID", DbType.String, r.LessonID.Value)
                        If r.ActivityID.Updated Then db.AddInParameter(dbcmd, "@ActivityID", DbType.String, r.ActivityID.Value)
                        If r.EndDate.Updated Then db.AddInParameter(dbcmd, "@EndDate", DbType.Date, IIf(IsDateTimeNull(r.EndDate.Value), Convert.ToDateTime("1900/1/1"), r.EndDate.Value))
                        If r.Hours.Updated Then db.AddInParameter(dbcmd, "@Hours", DbType.Decimal, r.Hours.Value)
                        If r.LessonUnit.Updated Then db.AddInParameter(dbcmd, "@LessonUnit", DbType.String, r.LessonUnit.Value)
                        If r.Fee.Updated Then db.AddInParameter(dbcmd, "@Fee", DbType.Decimal, r.Fee.Value)
                        If r.DeptName.Updated Then db.AddInParameter(dbcmd, "@DeptName", DbType.String, r.DeptName.Value)
                        If r.KindName.Updated Then db.AddInParameter(dbcmd, "@KindName", DbType.String, r.KindName.Value)
                        If r.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(r.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), r.LastChgDate.Value))
                        db.AddInParameter(dbcmd, "@PKIDNo", DbType.String, IIf(r.LoadFromDataRow, r.IDNo.OldValue, r.IDNo.Value))
                        db.AddInParameter(dbcmd, "@PKBeginDate", DbType.Date, IIf(r.LoadFromDataRow, r.BeginDate.OldValue, r.BeginDate.Value))
                        db.AddInParameter(dbcmd, "@PKLessonName", DbType.String, IIf(r.LoadFromDataRow, r.LessonName.OldValue, r.LessonName.Value))
                        db.AddInParameter(dbcmd, "@PKLessonID", DbType.String, IIf(r.LoadFromDataRow, r.LessonID.OldValue, r.LessonID.Value))
                        db.AddInParameter(dbcmd, "@PKActivityID", DbType.String, IIf(r.LoadFromDataRow, r.ActivityID.OldValue, r.ActivityID.Value))

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

        Public Function Update(ByVal TrainingRow As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0
            Dim strDot As String

            For Each r As Row In TrainingRow
                strDot = String.Empty
                If strSQL.Length > 0 Then strSQL.Remove(0, strSQL.Length)
                strSQL.AppendLine("Update Training Set")
                For i As Integer = 0 To r.FieldNames.Length - 1
                    If Not r.IsIdentityField(r.FieldNames(i)) AndAlso r.IsUpdated(r.FieldNames(i)) AndAlso r.CreateUpdateSQL(r.FieldNames(i))
                        strSQL.AppendLine(String.Format("{0}{1} = @{1}", strDot, r.FieldNames(i)))
                        strDot = ","
                    End If
                Next
                If strDot = String.Empty Then Continue For
                strSQL.AppendLine("Where IDNo = @PKIDNo")
                strSQL.AppendLine("And BeginDate = @PKBeginDate")
                strSQL.AppendLine("And LessonName = @PKLessonName")
                strSQL.AppendLine("And LessonID = @PKLessonID")
                strSQL.AppendLine("And ActivityID = @PKActivityID")

                dbcmd = db.GetSqlStringCommand(strSQL.ToString())
                If r.IDNo.Updated Then db.AddInParameter(dbcmd, "@IDNo", DbType.String, r.IDNo.Value)
                If r.BeginDate.Updated Then db.AddInParameter(dbcmd, "@BeginDate", DbType.Date, IIf(IsDateTimeNull(r.BeginDate.Value), Convert.ToDateTime("1900/1/1"), r.BeginDate.Value))
                If r.LessonName.Updated Then db.AddInParameter(dbcmd, "@LessonName", DbType.String, r.LessonName.Value)
                If r.LessonID.Updated Then db.AddInParameter(dbcmd, "@LessonID", DbType.String, r.LessonID.Value)
                If r.ActivityID.Updated Then db.AddInParameter(dbcmd, "@ActivityID", DbType.String, r.ActivityID.Value)
                If r.EndDate.Updated Then db.AddInParameter(dbcmd, "@EndDate", DbType.Date, IIf(IsDateTimeNull(r.EndDate.Value), Convert.ToDateTime("1900/1/1"), r.EndDate.Value))
                If r.Hours.Updated Then db.AddInParameter(dbcmd, "@Hours", DbType.Decimal, r.Hours.Value)
                If r.LessonUnit.Updated Then db.AddInParameter(dbcmd, "@LessonUnit", DbType.String, r.LessonUnit.Value)
                If r.Fee.Updated Then db.AddInParameter(dbcmd, "@Fee", DbType.Decimal, r.Fee.Value)
                If r.DeptName.Updated Then db.AddInParameter(dbcmd, "@DeptName", DbType.String, r.DeptName.Value)
                If r.KindName.Updated Then db.AddInParameter(dbcmd, "@KindName", DbType.String, r.KindName.Value)
                If r.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(r.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), r.LastChgDate.Value))
                db.AddInParameter(dbcmd, "@PKIDNo", DbType.String, IIf(r.LoadFromDataRow, r.IDNo.OldValue, r.IDNo.Value))
                db.AddInParameter(dbcmd, "@PKBeginDate", DbType.Date, IIf(r.LoadFromDataRow, r.BeginDate.OldValue, r.BeginDate.Value))
                db.AddInParameter(dbcmd, "@PKLessonName", DbType.String, IIf(r.LoadFromDataRow, r.LessonName.OldValue, r.LessonName.Value))
                db.AddInParameter(dbcmd, "@PKLessonID", DbType.String, IIf(r.LoadFromDataRow, r.LessonID.OldValue, r.LessonID.Value))
                db.AddInParameter(dbcmd, "@PKActivityID", DbType.String, IIf(r.LoadFromDataRow, r.ActivityID.OldValue, r.ActivityID.Value))

                intRowsAffected += db.ExecuteNonQuery(dbcmd, tran)
            Next
            Return intRowsAffected
        End Function

        Public Function IsDataExists(ByVal TrainingRow As Row) As Boolean
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intCount As Integer = 0

            strSQL.AppendLine("Select Count(*) Cnt From Training")
            strSQL.AppendLine("Where IDNo = @IDNo")
            strSQL.AppendLine("And BeginDate = @BeginDate")
            strSQL.AppendLine("And LessonName = @LessonName")
            strSQL.AppendLine("And LessonID = @LessonID")
            strSQL.AppendLine("And ActivityID = @ActivityID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@IDNo", DbType.String, TrainingRow.IDNo.Value)
            db.AddInParameter(dbcmd, "@BeginDate", DbType.Date, TrainingRow.BeginDate.Value)
            db.AddInParameter(dbcmd, "@LessonName", DbType.String, TrainingRow.LessonName.Value)
            db.AddInParameter(dbcmd, "@LessonID", DbType.String, TrainingRow.LessonID.Value)
            db.AddInParameter(dbcmd, "@ActivityID", DbType.String, TrainingRow.ActivityID.Value)

            intCount = Convert.ToInt32(db.ExecuteScalar(dbcmd))
            Return IIf(intCount > 0, True, False)
        End Function

        Public Function IsDataExists(ByVal TrainingRow As Row, ByVal tran As DbTransaction) As Boolean
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intCount As Integer = 0

            strSQL.AppendLine("Select Count(*) Cnt From Training")
            strSQL.AppendLine("Where IDNo = @IDNo")
            strSQL.AppendLine("And BeginDate = @BeginDate")
            strSQL.AppendLine("And LessonName = @LessonName")
            strSQL.AppendLine("And LessonID = @LessonID")
            strSQL.AppendLine("And ActivityID = @ActivityID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@IDNo", DbType.String, TrainingRow.IDNo.Value)
            db.AddInParameter(dbcmd, "@BeginDate", DbType.Date, TrainingRow.BeginDate.Value)
            db.AddInParameter(dbcmd, "@LessonName", DbType.String, TrainingRow.LessonName.Value)
            db.AddInParameter(dbcmd, "@LessonID", DbType.String, TrainingRow.LessonID.Value)
            db.AddInParameter(dbcmd, "@ActivityID", DbType.String, TrainingRow.ActivityID.Value)

            intCount = Convert.ToInt32(db.ExecuteScalar(dbcmd, tran))
            Return IIf(intCount > 0, True, False)
        End Function

        Public Function QuerybyWhere(WhereCondition As String) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From Training")
            If WhereCondition <> String.Empty Then strSQL.AppendLine(WhereCondition)

            Return db.ExecuteDataSet(CommandType.Text, strSQL.ToString())
        End Function

        Public Function Insert(ByVal TrainingRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Insert into Training")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    IDNo, BeginDate, LessonName, LessonID, ActivityID, EndDate, Hours, LessonUnit, Fee,")
            strSQL.AppendLine("    DeptName, KindName, LastChgDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @IDNo, @BeginDate, @LessonName, @LessonID, @ActivityID, @EndDate, @Hours, @LessonUnit, @Fee,")
            strSQL.AppendLine("    @DeptName, @KindName, @LastChgDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@IDNo", DbType.String, TrainingRow.IDNo.Value)
            db.AddInParameter(dbcmd, "@BeginDate", DbType.Date, IIf(IsDateTimeNull(TrainingRow.BeginDate.Value), Convert.ToDateTime("1900/1/1"), TrainingRow.BeginDate.Value))
            db.AddInParameter(dbcmd, "@LessonName", DbType.String, TrainingRow.LessonName.Value)
            db.AddInParameter(dbcmd, "@LessonID", DbType.String, TrainingRow.LessonID.Value)
            db.AddInParameter(dbcmd, "@ActivityID", DbType.String, TrainingRow.ActivityID.Value)
            db.AddInParameter(dbcmd, "@EndDate", DbType.Date, IIf(IsDateTimeNull(TrainingRow.EndDate.Value), Convert.ToDateTime("1900/1/1"), TrainingRow.EndDate.Value))
            db.AddInParameter(dbcmd, "@Hours", DbType.Decimal, TrainingRow.Hours.Value)
            db.AddInParameter(dbcmd, "@LessonUnit", DbType.String, TrainingRow.LessonUnit.Value)
            db.AddInParameter(dbcmd, "@Fee", DbType.Decimal, TrainingRow.Fee.Value)
            db.AddInParameter(dbcmd, "@DeptName", DbType.String, TrainingRow.DeptName.Value)
            db.AddInParameter(dbcmd, "@KindName", DbType.String, TrainingRow.KindName.Value)
            db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(TrainingRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), TrainingRow.LastChgDate.Value))

            Return db.ExecuteNonQuery(dbcmd)
        End Function

        Public Function Insert(ByVal TrainingRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Insert into Training")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    IDNo, BeginDate, LessonName, LessonID, ActivityID, EndDate, Hours, LessonUnit, Fee,")
            strSQL.AppendLine("    DeptName, KindName, LastChgDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @IDNo, @BeginDate, @LessonName, @LessonID, @ActivityID, @EndDate, @Hours, @LessonUnit, @Fee,")
            strSQL.AppendLine("    @DeptName, @KindName, @LastChgDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@IDNo", DbType.String, TrainingRow.IDNo.Value)
            db.AddInParameter(dbcmd, "@BeginDate", DbType.Date, IIf(IsDateTimeNull(TrainingRow.BeginDate.Value), Convert.ToDateTime("1900/1/1"), TrainingRow.BeginDate.Value))
            db.AddInParameter(dbcmd, "@LessonName", DbType.String, TrainingRow.LessonName.Value)
            db.AddInParameter(dbcmd, "@LessonID", DbType.String, TrainingRow.LessonID.Value)
            db.AddInParameter(dbcmd, "@ActivityID", DbType.String, TrainingRow.ActivityID.Value)
            db.AddInParameter(dbcmd, "@EndDate", DbType.Date, IIf(IsDateTimeNull(TrainingRow.EndDate.Value), Convert.ToDateTime("1900/1/1"), TrainingRow.EndDate.Value))
            db.AddInParameter(dbcmd, "@Hours", DbType.Decimal, TrainingRow.Hours.Value)
            db.AddInParameter(dbcmd, "@LessonUnit", DbType.String, TrainingRow.LessonUnit.Value)
            db.AddInParameter(dbcmd, "@Fee", DbType.Decimal, TrainingRow.Fee.Value)
            db.AddInParameter(dbcmd, "@DeptName", DbType.String, TrainingRow.DeptName.Value)
            db.AddInParameter(dbcmd, "@KindName", DbType.String, TrainingRow.KindName.Value)
            db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(TrainingRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), TrainingRow.LastChgDate.Value))

            Return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function Insert(ByVal TrainingRow As Row()) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Insert into Training")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    IDNo, BeginDate, LessonName, LessonID, ActivityID, EndDate, Hours, LessonUnit, Fee,")
            strSQL.AppendLine("    DeptName, KindName, LastChgDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @IDNo, @BeginDate, @LessonName, @LessonID, @ActivityID, @EndDate, @Hours, @LessonUnit, @Fee,")
            strSQL.AppendLine("    @DeptName, @KindName, @LastChgDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            Using cn As DbConnection = db.CreateConnection()
                cn.Open()
                Dim tran As DbTransaction = cn.BeginTransaction()
                Dim inTrans As Boolean = True

                Try
                    For Each r As Row In TrainingRow
                        dbcmd.Parameters.Clear()
                        db.AddInParameter(dbcmd, "@IDNo", DbType.String, r.IDNo.Value)
                        db.AddInParameter(dbcmd, "@BeginDate", DbType.Date, IIf(IsDateTimeNull(r.BeginDate.Value), Convert.ToDateTime("1900/1/1"), r.BeginDate.Value))
                        db.AddInParameter(dbcmd, "@LessonName", DbType.String, r.LessonName.Value)
                        db.AddInParameter(dbcmd, "@LessonID", DbType.String, r.LessonID.Value)
                        db.AddInParameter(dbcmd, "@ActivityID", DbType.String, r.ActivityID.Value)
                        db.AddInParameter(dbcmd, "@EndDate", DbType.Date, IIf(IsDateTimeNull(r.EndDate.Value), Convert.ToDateTime("1900/1/1"), r.EndDate.Value))
                        db.AddInParameter(dbcmd, "@Hours", DbType.Decimal, r.Hours.Value)
                        db.AddInParameter(dbcmd, "@LessonUnit", DbType.String, r.LessonUnit.Value)
                        db.AddInParameter(dbcmd, "@Fee", DbType.Decimal, r.Fee.Value)
                        db.AddInParameter(dbcmd, "@DeptName", DbType.String, r.DeptName.Value)
                        db.AddInParameter(dbcmd, "@KindName", DbType.String, r.KindName.Value)
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

        Public Function Insert(ByVal TrainingRow As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Insert into Training")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    IDNo, BeginDate, LessonName, LessonID, ActivityID, EndDate, Hours, LessonUnit, Fee,")
            strSQL.AppendLine("    DeptName, KindName, LastChgDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @IDNo, @BeginDate, @LessonName, @LessonID, @ActivityID, @EndDate, @Hours, @LessonUnit, @Fee,")
            strSQL.AppendLine("    @DeptName, @KindName, @LastChgDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())

            For Each r As Row In TrainingRow
                dbcmd.Parameters.Clear()
                db.AddInParameter(dbcmd, "@IDNo", DbType.String, r.IDNo.Value)
                db.AddInParameter(dbcmd, "@BeginDate", DbType.Date, IIf(IsDateTimeNull(r.BeginDate.Value), Convert.ToDateTime("1900/1/1"), r.BeginDate.Value))
                db.AddInParameter(dbcmd, "@LessonName", DbType.String, r.LessonName.Value)
                db.AddInParameter(dbcmd, "@LessonID", DbType.String, r.LessonID.Value)
                db.AddInParameter(dbcmd, "@ActivityID", DbType.String, r.ActivityID.Value)
                db.AddInParameter(dbcmd, "@EndDate", DbType.Date, IIf(IsDateTimeNull(r.EndDate.Value), Convert.ToDateTime("1900/1/1"), r.EndDate.Value))
                db.AddInParameter(dbcmd, "@Hours", DbType.Decimal, r.Hours.Value)
                db.AddInParameter(dbcmd, "@LessonUnit", DbType.String, r.LessonUnit.Value)
                db.AddInParameter(dbcmd, "@Fee", DbType.Decimal, r.Fee.Value)
                db.AddInParameter(dbcmd, "@DeptName", DbType.String, r.DeptName.Value)
                db.AddInParameter(dbcmd, "@KindName", DbType.String, r.KindName.Value)
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

