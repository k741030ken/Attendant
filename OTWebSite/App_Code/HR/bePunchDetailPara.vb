'****************************************************************
' Table:PunchDetailPara
' Created Date: 2017.06.19
'****************************************************************/
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports Microsoft.Practices.EnterpriseLibrary.Common
Imports Microsoft.Practices.EnterpriseLibrary.Data
Imports System.Data.Common
Imports System.Data

Namespace bePunchDetailPara
    Public Class Table
        Private m_Rows As Rows = New Rows()
        Private m_Fields As String() = { "CompID", "DutyInFlag", "DutyInBT", "DutyOutFlag", "DutyOutBT", "HoldingRankIDFlag", "HoldingRankID", "PositionFlag", "Position", "WorkTypeFlag" _
                                    , "WorkTypeID", "RotateFlag", "LastChgComp", "LastChgID", "LastChgDate" }
        Private m_Types As System.Type() = { GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String) _
                                    , GetType(String), GetType(String), GetType(String), GetType(String), GetType(Date) }
        Private m_PrimaryFields As String() = { "CompID" }

        Public ReadOnly Property Rows() As bePunchDetailPara.Rows 
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
        Public Sub Transfer2Row(PunchDetailParaTable As DataTable)
            For Each dr As DataRow In PunchDetailParaTable.Rows
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
                dr(m_Rows(i).DutyInFlag.FieldName) = m_Rows(i).DutyInFlag.Value
                dr(m_Rows(i).DutyInBT.FieldName) = m_Rows(i).DutyInBT.Value
                dr(m_Rows(i).DutyOutFlag.FieldName) = m_Rows(i).DutyOutFlag.Value
                dr(m_Rows(i).DutyOutBT.FieldName) = m_Rows(i).DutyOutBT.Value
                dr(m_Rows(i).HoldingRankIDFlag.FieldName) = m_Rows(i).HoldingRankIDFlag.Value
                dr(m_Rows(i).HoldingRankID.FieldName) = m_Rows(i).HoldingRankID.Value
                dr(m_Rows(i).PositionFlag.FieldName) = m_Rows(i).PositionFlag.Value
                dr(m_Rows(i).Position.FieldName) = m_Rows(i).Position.Value
                dr(m_Rows(i).WorkTypeFlag.FieldName) = m_Rows(i).WorkTypeFlag.Value
                dr(m_Rows(i).WorkTypeID.FieldName) = m_Rows(i).WorkTypeID.Value
                dr(m_Rows(i).RotateFlag.FieldName) = m_Rows(i).RotateFlag.Value
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

        Public Sub Add(PunchDetailParaRow As Row)
            m_Rows.Add(PunchDetailParaRow)
        End Sub

        Public Sub Remove(PunchDetailParaRow As Row)
            If m_Rows.IndexOf(PunchDetailParaRow) >= 0 Then
                m_Rows.Remove(PunchDetailParaRow)
            End If
        End Sub

        Public Sub Dispose()
            m_Rows.Clear()
        End Sub

    End Class

    Public Class Row

        Private FI_CompID As Field(Of String) = new Field(Of String)("CompID", true)
        Private FI_DutyInFlag As Field(Of String) = new Field(Of String)("DutyInFlag", true)
        Private FI_DutyInBT As Field(Of String) = new Field(Of String)("DutyInBT", true)
        Private FI_DutyOutFlag As Field(Of String) = new Field(Of String)("DutyOutFlag", true)
        Private FI_DutyOutBT As Field(Of String) = new Field(Of String)("DutyOutBT", true)
        Private FI_HoldingRankIDFlag As Field(Of String) = new Field(Of String)("HoldingRankIDFlag", true)
        Private FI_HoldingRankID As Field(Of String) = new Field(Of String)("HoldingRankID", true)
        Private FI_PositionFlag As Field(Of String) = new Field(Of String)("PositionFlag", true)
        Private FI_Position As Field(Of String) = new Field(Of String)("Position", true)
        Private FI_WorkTypeFlag As Field(Of String) = new Field(Of String)("WorkTypeFlag", true)
        Private FI_WorkTypeID As Field(Of String) = new Field(Of String)("WorkTypeID", true)
        Private FI_RotateFlag As Field(Of String) = new Field(Of String)("RotateFlag", true)
        Private FI_LastChgComp As Field(Of String) = new Field(Of String)("LastChgComp", true)
        Private FI_LastChgID As Field(Of String) = new Field(Of String)("LastChgID", true)
        Private FI_LastChgDate As Field(Of Date) = new Field(Of Date)("LastChgDate", true)
        Private m_FieldNames As String() = { "CompID", "DutyInFlag", "DutyInBT", "DutyOutFlag", "DutyOutBT", "HoldingRankIDFlag", "HoldingRankID", "PositionFlag", "Position", "WorkTypeFlag" _
                                    , "WorkTypeID", "RotateFlag", "LastChgComp", "LastChgID", "LastChgDate" }
        Private m_PrimaryFields As String() = { "CompID" }
        Private m_IdentityFields As String() = {  }
        Private m_LoadFromDataRow As Boolean = False

        Private Function GetFieldValue(ByVal fieldName As String) As Object
            Select Case fieldName
                Case "CompID"
                    Return FI_CompID.Value
                Case "DutyInFlag"
                    Return FI_DutyInFlag.Value
                Case "DutyInBT"
                    Return FI_DutyInBT.Value
                Case "DutyOutFlag"
                    Return FI_DutyOutFlag.Value
                Case "DutyOutBT"
                    Return FI_DutyOutBT.Value
                Case "HoldingRankIDFlag"
                    Return FI_HoldingRankIDFlag.Value
                Case "HoldingRankID"
                    Return FI_HoldingRankID.Value
                Case "PositionFlag"
                    Return FI_PositionFlag.Value
                Case "Position"
                    Return FI_Position.Value
                Case "WorkTypeFlag"
                    Return FI_WorkTypeFlag.Value
                Case "WorkTypeID"
                    Return FI_WorkTypeID.Value
                Case "RotateFlag"
                    Return FI_RotateFlag.Value
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
                Case "DutyInFlag"
                    FI_DutyInFlag.SetValue(value)
                Case "DutyInBT"
                    FI_DutyInBT.SetValue(value)
                Case "DutyOutFlag"
                    FI_DutyOutFlag.SetValue(value)
                Case "DutyOutBT"
                    FI_DutyOutBT.SetValue(value)
                Case "HoldingRankIDFlag"
                    FI_HoldingRankIDFlag.SetValue(value)
                Case "HoldingRankID"
                    FI_HoldingRankID.SetValue(value)
                Case "PositionFlag"
                    FI_PositionFlag.SetValue(value)
                Case "Position"
                    FI_Position.SetValue(value)
                Case "WorkTypeFlag"
                    FI_WorkTypeFlag.SetValue(value)
                Case "WorkTypeID"
                    FI_WorkTypeID.SetValue(value)
                Case "RotateFlag"
                    FI_RotateFlag.SetValue(value)
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
                Case "DutyInFlag"
                    return FI_DutyInFlag.Updated
                Case "DutyInBT"
                    return FI_DutyInBT.Updated
                Case "DutyOutFlag"
                    return FI_DutyOutFlag.Updated
                Case "DutyOutBT"
                    return FI_DutyOutBT.Updated
                Case "HoldingRankIDFlag"
                    return FI_HoldingRankIDFlag.Updated
                Case "HoldingRankID"
                    return FI_HoldingRankID.Updated
                Case "PositionFlag"
                    return FI_PositionFlag.Updated
                Case "Position"
                    return FI_Position.Updated
                Case "WorkTypeFlag"
                    return FI_WorkTypeFlag.Updated
                Case "WorkTypeID"
                    return FI_WorkTypeID.Updated
                Case "RotateFlag"
                    return FI_RotateFlag.Updated
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
                Case "DutyInFlag"
                    return FI_DutyInFlag.CreateUpdateSQL
                Case "DutyInBT"
                    return FI_DutyInBT.CreateUpdateSQL
                Case "DutyOutFlag"
                    return FI_DutyOutFlag.CreateUpdateSQL
                Case "DutyOutBT"
                    return FI_DutyOutBT.CreateUpdateSQL
                Case "HoldingRankIDFlag"
                    return FI_HoldingRankIDFlag.CreateUpdateSQL
                Case "HoldingRankID"
                    return FI_HoldingRankID.CreateUpdateSQL
                Case "PositionFlag"
                    return FI_PositionFlag.CreateUpdateSQL
                Case "Position"
                    return FI_Position.CreateUpdateSQL
                Case "WorkTypeFlag"
                    return FI_WorkTypeFlag.CreateUpdateSQL
                Case "WorkTypeID"
                    return FI_WorkTypeID.CreateUpdateSQL
                Case "RotateFlag"
                    return FI_RotateFlag.CreateUpdateSQL
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
            FI_DutyInFlag.SetInitValue("0")
            FI_DutyInBT.SetInitValue("")
            FI_DutyOutFlag.SetInitValue("0")
            FI_DutyOutBT.SetInitValue("")
            FI_HoldingRankIDFlag.SetInitValue("0")
            FI_HoldingRankID.SetInitValue("")
            FI_PositionFlag.SetInitValue("0")
            FI_Position.SetInitValue("")
            FI_WorkTypeFlag.SetInitValue("0")
            FI_WorkTypeID.SetInitValue("")
            FI_RotateFlag.SetInitValue("0")
            FI_LastChgComp.SetInitValue("")
            FI_LastChgID.SetInitValue("")
            FI_LastChgDate.SetInitValue(Convert.ToDateTime("1900/01/01"))
        End Sub

        Public Sub New(ByVal dr As System.Data.DataRow)
            FI_CompID.SetInitValue(dr("CompID"))
            FI_DutyInFlag.SetInitValue(dr("DutyInFlag"))
            FI_DutyInBT.SetInitValue(dr("DutyInBT"))
            FI_DutyOutFlag.SetInitValue(dr("DutyOutFlag"))
            FI_DutyOutBT.SetInitValue(dr("DutyOutBT"))
            FI_HoldingRankIDFlag.SetInitValue(dr("HoldingRankIDFlag"))
            FI_HoldingRankID.SetInitValue(dr("HoldingRankID"))
            FI_PositionFlag.SetInitValue(dr("PositionFlag"))
            FI_Position.SetInitValue(dr("Position"))
            FI_WorkTypeFlag.SetInitValue(dr("WorkTypeFlag"))
            FI_WorkTypeID.SetInitValue(dr("WorkTypeID"))
            FI_RotateFlag.SetInitValue(dr("RotateFlag"))
            FI_LastChgComp.SetInitValue(dr("LastChgComp"))
            FI_LastChgID.SetInitValue(dr("LastChgID"))
            FI_LastChgDate.SetInitValue(dr("LastChgDate"))

            m_LoadFromDataRow = True
        End Sub

        Private Sub ClearUpdated()
            FI_CompID.Updated = False
            FI_DutyInFlag.Updated = False
            FI_DutyInBT.Updated = False
            FI_DutyOutFlag.Updated = False
            FI_DutyOutBT.Updated = False
            FI_HoldingRankIDFlag.Updated = False
            FI_HoldingRankID.Updated = False
            FI_PositionFlag.Updated = False
            FI_Position.Updated = False
            FI_WorkTypeFlag.Updated = False
            FI_WorkTypeID.Updated = False
            FI_RotateFlag.Updated = False
            FI_LastChgComp.Updated = False
            FI_LastChgID.Updated = False
            FI_LastChgDate.Updated = False
        End Sub

        Public ReadOnly Property CompID As Field(Of String) 
            Get
                Return FI_CompID
            End Get
        End Property

        Public ReadOnly Property DutyInFlag As Field(Of String) 
            Get
                Return FI_DutyInFlag
            End Get
        End Property

        Public ReadOnly Property DutyInBT As Field(Of String) 
            Get
                Return FI_DutyInBT
            End Get
        End Property

        Public ReadOnly Property DutyOutFlag As Field(Of String) 
            Get
                Return FI_DutyOutFlag
            End Get
        End Property

        Public ReadOnly Property DutyOutBT As Field(Of String) 
            Get
                Return FI_DutyOutBT
            End Get
        End Property

        Public ReadOnly Property HoldingRankIDFlag As Field(Of String) 
            Get
                Return FI_HoldingRankIDFlag
            End Get
        End Property

        Public ReadOnly Property HoldingRankID As Field(Of String) 
            Get
                Return FI_HoldingRankID
            End Get
        End Property

        Public ReadOnly Property PositionFlag As Field(Of String) 
            Get
                Return FI_PositionFlag
            End Get
        End Property

        Public ReadOnly Property Position As Field(Of String) 
            Get
                Return FI_Position
            End Get
        End Property

        Public ReadOnly Property WorkTypeFlag As Field(Of String) 
            Get
                Return FI_WorkTypeFlag
            End Get
        End Property

        Public ReadOnly Property WorkTypeID As Field(Of String) 
            Get
                Return FI_WorkTypeID
            End Get
        End Property

        Public ReadOnly Property RotateFlag As Field(Of String) 
            Get
                Return FI_RotateFlag
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
        Public Function DeleteRowByPrimaryKey(ByVal PunchDetailParaRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("AattendantDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Delete From PunchDetailPara")
            strSQL.AppendLine("Where CompID = @CompID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, PunchDetailParaRow.CompID.Value)

            return db.ExecuteNonQuery(dbcmd)
        End Function

        public Function DeleteRowByPrimaryKey(ByVal PunchDetailParaRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("AattendantDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Delete From PunchDetailPara")
            strSQL.AppendLine("Where CompID = @CompID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, PunchDetailParaRow.CompID.Value)

            return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function DeleteRowByPrimaryKey(ByVal PunchDetailParaRow As Row()) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("AattendantDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Delete From PunchDetailPara")
            strSQL.AppendLine("Where CompID = @CompID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            using cn As DbConnection = db.CreateConnection()
                cn.Open()
                Dim tran As DbTransaction = cn.BeginTransaction()
                Dim inTrans As Boolean = True

                Try
                    For Each r As Row In PunchDetailParaRow
                        dbcmd.Parameters.Clear()
                        db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)

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

        Public Function DeleteRowByPrimaryKey(ByVal PunchDetailParaRow As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("AattendantDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Delete From PunchDetailPara")
            strSQL.AppendLine("Where CompID = @CompID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())

            For Each r As Row In PunchDetailParaRow
                dbcmd.Parameters.Clear()
                db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)

                intRowsAffected += db.ExecuteNonQuery(dbcmd, tran)
            Next
            Return intRowsAffected
        End Function

        Public Function QueryByKey(ByVal PunchDetailParaRow As Row) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase("AattendantDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From PunchDetailPara")
            strSQL.AppendLine("Where CompID = @CompID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, PunchDetailParaRow.CompID.Value)

            Return db.ExecuteDataSet(dbcmd)
        End Function

        Public Function QueryByKey(PunchDetailParaRow As Row, tran As DbTransaction) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase("AattendantDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From PunchDetailPara")
            strSQL.AppendLine("Where CompID = @CompID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, PunchDetailParaRow.CompID.Value)

            Return db.ExecuteDataSet(dbcmd, tran)
        End Function

        Public Function Update(ByVal PunchDetailParaRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("AattendantDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim strDot As String = String.Empty

            strSQL.AppendLine("Update PunchDetailPara Set")
            For i As Integer = 0 To PunchDetailParaRow.FieldNames.Length - 1
                If Not PunchDetailParaRow.IsIdentityField(PunchDetailParaRow.FieldNames(i)) AndAlso PunchDetailParaRow.IsUpdated(PunchDetailParaRow.FieldNames(i)) AndAlso PunchDetailParaRow.CreateUpdateSQL(PunchDetailParaRow.FieldNames(i)) Then
                    strSQL.AppendLine(string.Format("{0}{1} = @{1}", strDot, PunchDetailParaRow.FieldNames(i)))
                    strDot = ","
                End If
            Next
            If strDot = String.Empty Then Throw New Exception("未異動資料欄位！")
            strSQL.AppendLine("Where CompID = @PKCompID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            If PunchDetailParaRow.CompID.Updated Then db.AddInParameter(dbcmd, "@CompID", DbType.String, PunchDetailParaRow.CompID.Value)
            If PunchDetailParaRow.DutyInFlag.Updated Then db.AddInParameter(dbcmd, "@DutyInFlag", DbType.String, PunchDetailParaRow.DutyInFlag.Value)
            If PunchDetailParaRow.DutyInBT.Updated Then db.AddInParameter(dbcmd, "@DutyInBT", DbType.String, PunchDetailParaRow.DutyInBT.Value)
            If PunchDetailParaRow.DutyOutFlag.Updated Then db.AddInParameter(dbcmd, "@DutyOutFlag", DbType.String, PunchDetailParaRow.DutyOutFlag.Value)
            If PunchDetailParaRow.DutyOutBT.Updated Then db.AddInParameter(dbcmd, "@DutyOutBT", DbType.String, PunchDetailParaRow.DutyOutBT.Value)
            If PunchDetailParaRow.HoldingRankIDFlag.Updated Then db.AddInParameter(dbcmd, "@HoldingRankIDFlag", DbType.String, PunchDetailParaRow.HoldingRankIDFlag.Value)
            If PunchDetailParaRow.HoldingRankID.Updated Then db.AddInParameter(dbcmd, "@HoldingRankID", DbType.String, PunchDetailParaRow.HoldingRankID.Value)
            If PunchDetailParaRow.PositionFlag.Updated Then db.AddInParameter(dbcmd, "@PositionFlag", DbType.String, PunchDetailParaRow.PositionFlag.Value)
            If PunchDetailParaRow.Position.Updated Then db.AddInParameter(dbcmd, "@Position", DbType.String, PunchDetailParaRow.Position.Value)
            If PunchDetailParaRow.WorkTypeFlag.Updated Then db.AddInParameter(dbcmd, "@WorkTypeFlag", DbType.String, PunchDetailParaRow.WorkTypeFlag.Value)
            If PunchDetailParaRow.WorkTypeID.Updated Then db.AddInParameter(dbcmd, "@WorkTypeID", DbType.String, PunchDetailParaRow.WorkTypeID.Value)
            If PunchDetailParaRow.RotateFlag.Updated Then db.AddInParameter(dbcmd, "@RotateFlag", DbType.String, PunchDetailParaRow.RotateFlag.Value)
            If PunchDetailParaRow.LastChgComp.Updated Then db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, PunchDetailParaRow.LastChgComp.Value)
            If PunchDetailParaRow.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, PunchDetailParaRow.LastChgID.Value)
            If PunchDetailParaRow.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(PunchDetailParaRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), PunchDetailParaRow.LastChgDate.Value))
            db.AddInParameter(dbcmd, "@PKCompID", DbType.String, IIf(PunchDetailParaRow.LoadFromDataRow, PunchDetailParaRow.CompID.OldValue, PunchDetailParaRow.CompID.Value))

            Return db.ExecuteNonQuery(dbcmd)
        End Function

        Public Function Update(ByVal PunchDetailParaRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("AattendantDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim strDot As String = String.Empty

            strSQL.AppendLine("Update PunchDetailPara Set")
            For i As Integer = 0 To PunchDetailParaRow.FieldNames.Length - 1
                If Not PunchDetailParaRow.IsIdentityField(PunchDetailParaRow.FieldNames(i)) AndAlso PunchDetailParaRow.IsUpdated(PunchDetailParaRow.FieldNames(i)) AndAlso PunchDetailParaRow.CreateUpdateSQL(PunchDetailParaRow.FieldNames(i)) Then
                    strSQL.AppendLine(string.Format("{0}{1} = @{1}", strDot, PunchDetailParaRow.FieldNames(i)))
                    strDot = ","
                End If
            Next 
            If strDot = String.Empty Then Throw New Exception("未異動資料欄位！")
            strSQL.AppendLine("Where CompID = @PKCompID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            If PunchDetailParaRow.CompID.Updated Then db.AddInParameter(dbcmd, "@CompID", DbType.String, PunchDetailParaRow.CompID.Value)
            If PunchDetailParaRow.DutyInFlag.Updated Then db.AddInParameter(dbcmd, "@DutyInFlag", DbType.String, PunchDetailParaRow.DutyInFlag.Value)
            If PunchDetailParaRow.DutyInBT.Updated Then db.AddInParameter(dbcmd, "@DutyInBT", DbType.String, PunchDetailParaRow.DutyInBT.Value)
            If PunchDetailParaRow.DutyOutFlag.Updated Then db.AddInParameter(dbcmd, "@DutyOutFlag", DbType.String, PunchDetailParaRow.DutyOutFlag.Value)
            If PunchDetailParaRow.DutyOutBT.Updated Then db.AddInParameter(dbcmd, "@DutyOutBT", DbType.String, PunchDetailParaRow.DutyOutBT.Value)
            If PunchDetailParaRow.HoldingRankIDFlag.Updated Then db.AddInParameter(dbcmd, "@HoldingRankIDFlag", DbType.String, PunchDetailParaRow.HoldingRankIDFlag.Value)
            If PunchDetailParaRow.HoldingRankID.Updated Then db.AddInParameter(dbcmd, "@HoldingRankID", DbType.String, PunchDetailParaRow.HoldingRankID.Value)
            If PunchDetailParaRow.PositionFlag.Updated Then db.AddInParameter(dbcmd, "@PositionFlag", DbType.String, PunchDetailParaRow.PositionFlag.Value)
            If PunchDetailParaRow.Position.Updated Then db.AddInParameter(dbcmd, "@Position", DbType.String, PunchDetailParaRow.Position.Value)
            If PunchDetailParaRow.WorkTypeFlag.Updated Then db.AddInParameter(dbcmd, "@WorkTypeFlag", DbType.String, PunchDetailParaRow.WorkTypeFlag.Value)
            If PunchDetailParaRow.WorkTypeID.Updated Then db.AddInParameter(dbcmd, "@WorkTypeID", DbType.String, PunchDetailParaRow.WorkTypeID.Value)
            If PunchDetailParaRow.RotateFlag.Updated Then db.AddInParameter(dbcmd, "@RotateFlag", DbType.String, PunchDetailParaRow.RotateFlag.Value)
            If PunchDetailParaRow.LastChgComp.Updated Then db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, PunchDetailParaRow.LastChgComp.Value)
            If PunchDetailParaRow.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, PunchDetailParaRow.LastChgID.Value)
            If PunchDetailParaRow.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(PunchDetailParaRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), PunchDetailParaRow.LastChgDate.Value))
            db.AddInParameter(dbcmd, "@PKCompID", DbType.String, IIf(PunchDetailParaRow.LoadFromDataRow, PunchDetailParaRow.CompID.OldValue, PunchDetailParaRow.CompID.Value))

            Return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function Update(ByVal PunchDetailParaRow As Row()) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("AattendantDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0
            Dim strDot As String

            Using cn As DbConnection = db.CreateConnection()
                cn.Open()
                Dim tran As DbTransaction = cn.BeginTransaction()
                Dim inTrans As Boolean = True

                Try
                    For Each r As Row In PunchDetailParaRow
                        strDot = String.Empty
                        If strSQL.Length > 0 Then strSQL.Remove(0, strSQL.Length)
                        strSQL.AppendLine("Update PunchDetailPara Set")
                        For i As Integer = 0 To r.FieldNames.Length - 1
                            If Not r.IsIdentityField(r.FieldNames(i)) AndAlso r.IsUpdated(r.FieldNames(i)) AndAlso r.CreateUpdateSQL(r.FieldNames(i)) Then
                                strSQL.AppendLine(String.Format("{0}{1} = @{1}", strDot, r.FieldNames(i)))
                                strDot = ","
                            End If
                        Next
                        If strDot = String.Empty Then Continue For
                        strSQL.AppendLine("Where CompID = @PKCompID")

                        dbcmd = db.GetSqlStringCommand(strSQL.ToString())
                        If r.CompID.Updated Then db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                        If r.DutyInFlag.Updated Then db.AddInParameter(dbcmd, "@DutyInFlag", DbType.String, r.DutyInFlag.Value)
                        If r.DutyInBT.Updated Then db.AddInParameter(dbcmd, "@DutyInBT", DbType.String, r.DutyInBT.Value)
                        If r.DutyOutFlag.Updated Then db.AddInParameter(dbcmd, "@DutyOutFlag", DbType.String, r.DutyOutFlag.Value)
                        If r.DutyOutBT.Updated Then db.AddInParameter(dbcmd, "@DutyOutBT", DbType.String, r.DutyOutBT.Value)
                        If r.HoldingRankIDFlag.Updated Then db.AddInParameter(dbcmd, "@HoldingRankIDFlag", DbType.String, r.HoldingRankIDFlag.Value)
                        If r.HoldingRankID.Updated Then db.AddInParameter(dbcmd, "@HoldingRankID", DbType.String, r.HoldingRankID.Value)
                        If r.PositionFlag.Updated Then db.AddInParameter(dbcmd, "@PositionFlag", DbType.String, r.PositionFlag.Value)
                        If r.Position.Updated Then db.AddInParameter(dbcmd, "@Position", DbType.String, r.Position.Value)
                        If r.WorkTypeFlag.Updated Then db.AddInParameter(dbcmd, "@WorkTypeFlag", DbType.String, r.WorkTypeFlag.Value)
                        If r.WorkTypeID.Updated Then db.AddInParameter(dbcmd, "@WorkTypeID", DbType.String, r.WorkTypeID.Value)
                        If r.RotateFlag.Updated Then db.AddInParameter(dbcmd, "@RotateFlag", DbType.String, r.RotateFlag.Value)
                        If r.LastChgComp.Updated Then db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, r.LastChgComp.Value)
                        If r.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, r.LastChgID.Value)
                        If r.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(r.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), r.LastChgDate.Value))
                        db.AddInParameter(dbcmd, "@PKCompID", DbType.String, IIf(r.LoadFromDataRow, r.CompID.OldValue, r.CompID.Value))

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

        Public Function Update(ByVal PunchDetailParaRow As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("AattendantDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0
            Dim strDot As String

            For Each r As Row In PunchDetailParaRow
                strDot = String.Empty
                If strSQL.Length > 0 Then strSQL.Remove(0, strSQL.Length)
                strSQL.AppendLine("Update PunchDetailPara Set")
                For i As Integer = 0 To r.FieldNames.Length - 1
                    If Not r.IsIdentityField(r.FieldNames(i)) AndAlso r.IsUpdated(r.FieldNames(i)) AndAlso r.CreateUpdateSQL(r.FieldNames(i))
                        strSQL.AppendLine(String.Format("{0}{1} = @{1}", strDot, r.FieldNames(i)))
                        strDot = ","
                    End If
                Next
                If strDot = String.Empty Then Continue For
                strSQL.AppendLine("Where CompID = @PKCompID")

                dbcmd = db.GetSqlStringCommand(strSQL.ToString())
                If r.CompID.Updated Then db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                If r.DutyInFlag.Updated Then db.AddInParameter(dbcmd, "@DutyInFlag", DbType.String, r.DutyInFlag.Value)
                If r.DutyInBT.Updated Then db.AddInParameter(dbcmd, "@DutyInBT", DbType.String, r.DutyInBT.Value)
                If r.DutyOutFlag.Updated Then db.AddInParameter(dbcmd, "@DutyOutFlag", DbType.String, r.DutyOutFlag.Value)
                If r.DutyOutBT.Updated Then db.AddInParameter(dbcmd, "@DutyOutBT", DbType.String, r.DutyOutBT.Value)
                If r.HoldingRankIDFlag.Updated Then db.AddInParameter(dbcmd, "@HoldingRankIDFlag", DbType.String, r.HoldingRankIDFlag.Value)
                If r.HoldingRankID.Updated Then db.AddInParameter(dbcmd, "@HoldingRankID", DbType.String, r.HoldingRankID.Value)
                If r.PositionFlag.Updated Then db.AddInParameter(dbcmd, "@PositionFlag", DbType.String, r.PositionFlag.Value)
                If r.Position.Updated Then db.AddInParameter(dbcmd, "@Position", DbType.String, r.Position.Value)
                If r.WorkTypeFlag.Updated Then db.AddInParameter(dbcmd, "@WorkTypeFlag", DbType.String, r.WorkTypeFlag.Value)
                If r.WorkTypeID.Updated Then db.AddInParameter(dbcmd, "@WorkTypeID", DbType.String, r.WorkTypeID.Value)
                If r.RotateFlag.Updated Then db.AddInParameter(dbcmd, "@RotateFlag", DbType.String, r.RotateFlag.Value)
                If r.LastChgComp.Updated Then db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, r.LastChgComp.Value)
                If r.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, r.LastChgID.Value)
                If r.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(r.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), r.LastChgDate.Value))
                db.AddInParameter(dbcmd, "@PKCompID", DbType.String, IIf(r.LoadFromDataRow, r.CompID.OldValue, r.CompID.Value))

                intRowsAffected += db.ExecuteNonQuery(dbcmd, tran)
            Next
            Return intRowsAffected
        End Function

        Public Function IsDataExists(ByVal PunchDetailParaRow As Row) As Boolean
            Dim db As Database = DatabaseFactory.CreateDatabase("AattendantDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intCount As Integer = 0

            strSQL.AppendLine("Select Count(*) Cnt From PunchDetailPara")
            strSQL.AppendLine("Where CompID = @CompID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, PunchDetailParaRow.CompID.Value)

            intCount = Convert.ToInt32(db.ExecuteScalar(dbcmd))
            Return IIf(intCount > 0, True, False)
        End Function

        Public Function IsDataExists(ByVal PunchDetailParaRow As Row, ByVal tran As DbTransaction) As Boolean
            Dim db As Database = DatabaseFactory.CreateDatabase("AattendantDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intCount As Integer = 0

            strSQL.AppendLine("Select Count(*) Cnt From PunchDetailPara")
            strSQL.AppendLine("Where CompID = @CompID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, PunchDetailParaRow.CompID.Value)

            intCount = Convert.ToInt32(db.ExecuteScalar(dbcmd, tran))
            Return IIf(intCount > 0, True, False)
        End Function

        Public Function QuerybyWhere(WhereCondition As String) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase("AattendantDB")
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From PunchDetailPara")
            If WhereCondition <> String.Empty Then strSQL.AppendLine(WhereCondition)

            Return db.ExecuteDataSet(CommandType.Text, strSQL.ToString())
        End Function

        Public Function Insert(ByVal PunchDetailParaRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("AattendantDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Insert into PunchDetailPara")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    CompID, DutyInFlag, DutyInBT, DutyOutFlag, DutyOutBT, HoldingRankIDFlag, HoldingRankID,")
            strSQL.AppendLine("    PositionFlag, Position, WorkTypeFlag, WorkTypeID, RotateFlag, LastChgComp, LastChgID,")
            strSQL.AppendLine("    LastChgDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @CompID, @DutyInFlag, @DutyInBT, @DutyOutFlag, @DutyOutBT, @HoldingRankIDFlag, @HoldingRankID,")
            strSQL.AppendLine("    @PositionFlag, @Position, @WorkTypeFlag, @WorkTypeID, @RotateFlag, @LastChgComp, @LastChgID,")
            strSQL.AppendLine("    @LastChgDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, PunchDetailParaRow.CompID.Value)
            db.AddInParameter(dbcmd, "@DutyInFlag", DbType.String, PunchDetailParaRow.DutyInFlag.Value)
            db.AddInParameter(dbcmd, "@DutyInBT", DbType.String, PunchDetailParaRow.DutyInBT.Value)
            db.AddInParameter(dbcmd, "@DutyOutFlag", DbType.String, PunchDetailParaRow.DutyOutFlag.Value)
            db.AddInParameter(dbcmd, "@DutyOutBT", DbType.String, PunchDetailParaRow.DutyOutBT.Value)
            db.AddInParameter(dbcmd, "@HoldingRankIDFlag", DbType.String, PunchDetailParaRow.HoldingRankIDFlag.Value)
            db.AddInParameter(dbcmd, "@HoldingRankID", DbType.String, PunchDetailParaRow.HoldingRankID.Value)
            db.AddInParameter(dbcmd, "@PositionFlag", DbType.String, PunchDetailParaRow.PositionFlag.Value)
            db.AddInParameter(dbcmd, "@Position", DbType.String, PunchDetailParaRow.Position.Value)
            db.AddInParameter(dbcmd, "@WorkTypeFlag", DbType.String, PunchDetailParaRow.WorkTypeFlag.Value)
            db.AddInParameter(dbcmd, "@WorkTypeID", DbType.String, PunchDetailParaRow.WorkTypeID.Value)
            db.AddInParameter(dbcmd, "@RotateFlag", DbType.String, PunchDetailParaRow.RotateFlag.Value)
            db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, PunchDetailParaRow.LastChgComp.Value)
            db.AddInParameter(dbcmd, "@LastChgID", DbType.String, PunchDetailParaRow.LastChgID.Value)
            db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(PunchDetailParaRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), PunchDetailParaRow.LastChgDate.Value))

            Return db.ExecuteNonQuery(dbcmd)
        End Function

        Public Function Insert(ByVal PunchDetailParaRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("AattendantDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Insert into PunchDetailPara")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    CompID, DutyInFlag, DutyInBT, DutyOutFlag, DutyOutBT, HoldingRankIDFlag, HoldingRankID,")
            strSQL.AppendLine("    PositionFlag, Position, WorkTypeFlag, WorkTypeID, RotateFlag, LastChgComp, LastChgID,")
            strSQL.AppendLine("    LastChgDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @CompID, @DutyInFlag, @DutyInBT, @DutyOutFlag, @DutyOutBT, @HoldingRankIDFlag, @HoldingRankID,")
            strSQL.AppendLine("    @PositionFlag, @Position, @WorkTypeFlag, @WorkTypeID, @RotateFlag, @LastChgComp, @LastChgID,")
            strSQL.AppendLine("    @LastChgDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, PunchDetailParaRow.CompID.Value)
            db.AddInParameter(dbcmd, "@DutyInFlag", DbType.String, PunchDetailParaRow.DutyInFlag.Value)
            db.AddInParameter(dbcmd, "@DutyInBT", DbType.String, PunchDetailParaRow.DutyInBT.Value)
            db.AddInParameter(dbcmd, "@DutyOutFlag", DbType.String, PunchDetailParaRow.DutyOutFlag.Value)
            db.AddInParameter(dbcmd, "@DutyOutBT", DbType.String, PunchDetailParaRow.DutyOutBT.Value)
            db.AddInParameter(dbcmd, "@HoldingRankIDFlag", DbType.String, PunchDetailParaRow.HoldingRankIDFlag.Value)
            db.AddInParameter(dbcmd, "@HoldingRankID", DbType.String, PunchDetailParaRow.HoldingRankID.Value)
            db.AddInParameter(dbcmd, "@PositionFlag", DbType.String, PunchDetailParaRow.PositionFlag.Value)
            db.AddInParameter(dbcmd, "@Position", DbType.String, PunchDetailParaRow.Position.Value)
            db.AddInParameter(dbcmd, "@WorkTypeFlag", DbType.String, PunchDetailParaRow.WorkTypeFlag.Value)
            db.AddInParameter(dbcmd, "@WorkTypeID", DbType.String, PunchDetailParaRow.WorkTypeID.Value)
            db.AddInParameter(dbcmd, "@RotateFlag", DbType.String, PunchDetailParaRow.RotateFlag.Value)
            db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, PunchDetailParaRow.LastChgComp.Value)
            db.AddInParameter(dbcmd, "@LastChgID", DbType.String, PunchDetailParaRow.LastChgID.Value)
            db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(PunchDetailParaRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), PunchDetailParaRow.LastChgDate.Value))

            Return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function Insert(ByVal PunchDetailParaRow As Row()) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("AattendantDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Insert into PunchDetailPara")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    CompID, DutyInFlag, DutyInBT, DutyOutFlag, DutyOutBT, HoldingRankIDFlag, HoldingRankID,")
            strSQL.AppendLine("    PositionFlag, Position, WorkTypeFlag, WorkTypeID, RotateFlag, LastChgComp, LastChgID,")
            strSQL.AppendLine("    LastChgDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @CompID, @DutyInFlag, @DutyInBT, @DutyOutFlag, @DutyOutBT, @HoldingRankIDFlag, @HoldingRankID,")
            strSQL.AppendLine("    @PositionFlag, @Position, @WorkTypeFlag, @WorkTypeID, @RotateFlag, @LastChgComp, @LastChgID,")
            strSQL.AppendLine("    @LastChgDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            Using cn As DbConnection = db.CreateConnection()
                cn.Open()
                Dim tran As DbTransaction = cn.BeginTransaction()
                Dim inTrans As Boolean = True

                Try
                    For Each r As Row In PunchDetailParaRow
                        dbcmd.Parameters.Clear()
                        db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                        db.AddInParameter(dbcmd, "@DutyInFlag", DbType.String, r.DutyInFlag.Value)
                        db.AddInParameter(dbcmd, "@DutyInBT", DbType.String, r.DutyInBT.Value)
                        db.AddInParameter(dbcmd, "@DutyOutFlag", DbType.String, r.DutyOutFlag.Value)
                        db.AddInParameter(dbcmd, "@DutyOutBT", DbType.String, r.DutyOutBT.Value)
                        db.AddInParameter(dbcmd, "@HoldingRankIDFlag", DbType.String, r.HoldingRankIDFlag.Value)
                        db.AddInParameter(dbcmd, "@HoldingRankID", DbType.String, r.HoldingRankID.Value)
                        db.AddInParameter(dbcmd, "@PositionFlag", DbType.String, r.PositionFlag.Value)
                        db.AddInParameter(dbcmd, "@Position", DbType.String, r.Position.Value)
                        db.AddInParameter(dbcmd, "@WorkTypeFlag", DbType.String, r.WorkTypeFlag.Value)
                        db.AddInParameter(dbcmd, "@WorkTypeID", DbType.String, r.WorkTypeID.Value)
                        db.AddInParameter(dbcmd, "@RotateFlag", DbType.String, r.RotateFlag.Value)
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

        Public Function Insert(ByVal PunchDetailParaRow As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("AattendantDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Insert into PunchDetailPara")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    CompID, DutyInFlag, DutyInBT, DutyOutFlag, DutyOutBT, HoldingRankIDFlag, HoldingRankID,")
            strSQL.AppendLine("    PositionFlag, Position, WorkTypeFlag, WorkTypeID, RotateFlag, LastChgComp, LastChgID,")
            strSQL.AppendLine("    LastChgDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @CompID, @DutyInFlag, @DutyInBT, @DutyOutFlag, @DutyOutBT, @HoldingRankIDFlag, @HoldingRankID,")
            strSQL.AppendLine("    @PositionFlag, @Position, @WorkTypeFlag, @WorkTypeID, @RotateFlag, @LastChgComp, @LastChgID,")
            strSQL.AppendLine("    @LastChgDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())

            For Each r As Row In PunchDetailParaRow
                dbcmd.Parameters.Clear()
                db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                db.AddInParameter(dbcmd, "@DutyInFlag", DbType.String, r.DutyInFlag.Value)
                db.AddInParameter(dbcmd, "@DutyInBT", DbType.String, r.DutyInBT.Value)
                db.AddInParameter(dbcmd, "@DutyOutFlag", DbType.String, r.DutyOutFlag.Value)
                db.AddInParameter(dbcmd, "@DutyOutBT", DbType.String, r.DutyOutBT.Value)
                db.AddInParameter(dbcmd, "@HoldingRankIDFlag", DbType.String, r.HoldingRankIDFlag.Value)
                db.AddInParameter(dbcmd, "@HoldingRankID", DbType.String, r.HoldingRankID.Value)
                db.AddInParameter(dbcmd, "@PositionFlag", DbType.String, r.PositionFlag.Value)
                db.AddInParameter(dbcmd, "@Position", DbType.String, r.Position.Value)
                db.AddInParameter(dbcmd, "@WorkTypeFlag", DbType.String, r.WorkTypeFlag.Value)
                db.AddInParameter(dbcmd, "@WorkTypeID", DbType.String, r.WorkTypeID.Value)
                db.AddInParameter(dbcmd, "@RotateFlag", DbType.String, r.RotateFlag.Value)
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

