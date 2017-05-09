'****************************************************************
' Table:HRFlowTypeDefine
' Created Date: 2017.05.02
'****************************************************************/
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports Microsoft.Practices.EnterpriseLibrary.Common
Imports Microsoft.Practices.EnterpriseLibrary.Data
Imports System.Data.Common
Imports System.Data

Namespace beHRFlowTypeDefine
    Public Class Table
        Private m_Rows As Rows = New Rows()
        Private m_Fields As String() = { "SystemID", "FlowCode", "FlowType", "FlowTypeName", "FlowTypeDescription", "FlowTypeFlag", "FlowSN", "LastChgComp", "LastChgID", "LastChgDate" }
        Private m_Types As System.Type() = { GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(Date) }
        Private m_PrimaryFields As String() = { "SystemID", "FlowCode", "FlowType" }

        Public ReadOnly Property Rows() As beHRFlowTypeDefine.Rows 
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
        Public Sub Transfer2Row(HRFlowTypeDefineTable As DataTable)
            For Each dr As DataRow In HRFlowTypeDefineTable.Rows
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

                dr(m_Rows(i).SystemID.FieldName) = m_Rows(i).SystemID.Value
                dr(m_Rows(i).FlowCode.FieldName) = m_Rows(i).FlowCode.Value
                dr(m_Rows(i).FlowType.FieldName) = m_Rows(i).FlowType.Value
                dr(m_Rows(i).FlowTypeName.FieldName) = m_Rows(i).FlowTypeName.Value
                dr(m_Rows(i).FlowTypeDescription.FieldName) = m_Rows(i).FlowTypeDescription.Value
                dr(m_Rows(i).FlowTypeFlag.FieldName) = m_Rows(i).FlowTypeFlag.Value
                dr(m_Rows(i).FlowSN.FieldName) = m_Rows(i).FlowSN.Value
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

        Public Sub Add(HRFlowTypeDefineRow As Row)
            m_Rows.Add(HRFlowTypeDefineRow)
        End Sub

        Public Sub Remove(HRFlowTypeDefineRow As Row)
            If m_Rows.IndexOf(HRFlowTypeDefineRow) >= 0 Then
                m_Rows.Remove(HRFlowTypeDefineRow)
            End If
        End Sub

        Public Sub Dispose()
            m_Rows.Clear()
        End Sub

    End Class

    Public Class Row

        Private FI_SystemID As Field(Of String) = new Field(Of String)("SystemID", true)
        Private FI_FlowCode As Field(Of String) = new Field(Of String)("FlowCode", true)
        Private FI_FlowType As Field(Of String) = new Field(Of String)("FlowType", true)
        Private FI_FlowTypeName As Field(Of String) = new Field(Of String)("FlowTypeName", true)
        Private FI_FlowTypeDescription As Field(Of String) = new Field(Of String)("FlowTypeDescription", true)
        Private FI_FlowTypeFlag As Field(Of String) = new Field(Of String)("FlowTypeFlag", true)
        Private FI_FlowSN As Field(Of String) = new Field(Of String)("FlowSN", true)
        Private FI_LastChgComp As Field(Of String) = new Field(Of String)("LastChgComp", true)
        Private FI_LastChgID As Field(Of String) = new Field(Of String)("LastChgID", true)
        Private FI_LastChgDate As Field(Of Date) = new Field(Of Date)("LastChgDate", true)
        Private m_FieldNames As String() = { "SystemID", "FlowCode", "FlowType", "FlowTypeName", "FlowTypeDescription", "FlowTypeFlag", "FlowSN", "LastChgComp", "LastChgID", "LastChgDate" }
        Private m_PrimaryFields As String() = { "SystemID", "FlowCode", "FlowType" }
        Private m_IdentityFields As String() = {  }
        Private m_LoadFromDataRow As Boolean = False

        Private Function GetFieldValue(ByVal fieldName As String) As Object
            Select Case fieldName
                Case "SystemID"
                    Return FI_SystemID.Value
                Case "FlowCode"
                    Return FI_FlowCode.Value
                Case "FlowType"
                    Return FI_FlowType.Value
                Case "FlowTypeName"
                    Return FI_FlowTypeName.Value
                Case "FlowTypeDescription"
                    Return FI_FlowTypeDescription.Value
                Case "FlowTypeFlag"
                    Return FI_FlowTypeFlag.Value
                Case "FlowSN"
                    Return FI_FlowSN.Value
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
                Case "SystemID"
                    FI_SystemID.SetValue(value)
                Case "FlowCode"
                    FI_FlowCode.SetValue(value)
                Case "FlowType"
                    FI_FlowType.SetValue(value)
                Case "FlowTypeName"
                    FI_FlowTypeName.SetValue(value)
                Case "FlowTypeDescription"
                    FI_FlowTypeDescription.SetValue(value)
                Case "FlowTypeFlag"
                    FI_FlowTypeFlag.SetValue(value)
                Case "FlowSN"
                    FI_FlowSN.SetValue(value)
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
                Case "SystemID"
                    return FI_SystemID.Updated
                Case "FlowCode"
                    return FI_FlowCode.Updated
                Case "FlowType"
                    return FI_FlowType.Updated
                Case "FlowTypeName"
                    return FI_FlowTypeName.Updated
                Case "FlowTypeDescription"
                    return FI_FlowTypeDescription.Updated
                Case "FlowTypeFlag"
                    return FI_FlowTypeFlag.Updated
                Case "FlowSN"
                    return FI_FlowSN.Updated
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
                Case "SystemID"
                    return FI_SystemID.CreateUpdateSQL
                Case "FlowCode"
                    return FI_FlowCode.CreateUpdateSQL
                Case "FlowType"
                    return FI_FlowType.CreateUpdateSQL
                Case "FlowTypeName"
                    return FI_FlowTypeName.CreateUpdateSQL
                Case "FlowTypeDescription"
                    return FI_FlowTypeDescription.CreateUpdateSQL
                Case "FlowTypeFlag"
                    return FI_FlowTypeFlag.CreateUpdateSQL
                Case "FlowSN"
                    return FI_FlowSN.CreateUpdateSQL
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
            FI_SystemID.SetInitValue("")
            FI_FlowCode.SetInitValue("")
            FI_FlowType.SetInitValue("")
            FI_FlowTypeName.SetInitValue("")
            FI_FlowTypeDescription.SetInitValue("")
            FI_FlowTypeFlag.SetInitValue("0")
            FI_FlowSN.SetInitValue("")
            FI_LastChgComp.SetInitValue("")
            FI_LastChgID.SetInitValue("")
            FI_LastChgDate.SetInitValue(Convert.ToDateTime("1900/01/01"))
        End Sub

        Public Sub New(ByVal dr As System.Data.DataRow)
            FI_SystemID.SetInitValue(dr("SystemID"))
            FI_FlowCode.SetInitValue(dr("FlowCode"))
            FI_FlowType.SetInitValue(dr("FlowType"))
            FI_FlowTypeName.SetInitValue(dr("FlowTypeName"))
            FI_FlowTypeDescription.SetInitValue(dr("FlowTypeDescription"))
            FI_FlowTypeFlag.SetInitValue(dr("FlowTypeFlag"))
            FI_FlowSN.SetInitValue(dr("FlowSN"))
            FI_LastChgComp.SetInitValue(dr("LastChgComp"))
            FI_LastChgID.SetInitValue(dr("LastChgID"))
            FI_LastChgDate.SetInitValue(dr("LastChgDate"))

            m_LoadFromDataRow = True
        End Sub

        Private Sub ClearUpdated()
            FI_SystemID.Updated = False
            FI_FlowCode.Updated = False
            FI_FlowType.Updated = False
            FI_FlowTypeName.Updated = False
            FI_FlowTypeDescription.Updated = False
            FI_FlowTypeFlag.Updated = False
            FI_FlowSN.Updated = False
            FI_LastChgComp.Updated = False
            FI_LastChgID.Updated = False
            FI_LastChgDate.Updated = False
        End Sub

        Public ReadOnly Property SystemID As Field(Of String) 
            Get
                Return FI_SystemID
            End Get
        End Property

        Public ReadOnly Property FlowCode As Field(Of String) 
            Get
                Return FI_FlowCode
            End Get
        End Property

        Public ReadOnly Property FlowType As Field(Of String) 
            Get
                Return FI_FlowType
            End Get
        End Property

        Public ReadOnly Property FlowTypeName As Field(Of String) 
            Get
                Return FI_FlowTypeName
            End Get
        End Property

        Public ReadOnly Property FlowTypeDescription As Field(Of String) 
            Get
                Return FI_FlowTypeDescription
            End Get
        End Property

        Public ReadOnly Property FlowTypeFlag As Field(Of String) 
            Get
                Return FI_FlowTypeFlag
            End Get
        End Property

        Public ReadOnly Property FlowSN As Field(Of String) 
            Get
                Return FI_FlowSN
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
        Public Function DeleteRowByPrimaryKey(ByVal HRFlowTypeDefineRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("AattendantDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Delete From HRFlowTypeDefine")
            strSQL.AppendLine("Where SystemID = @SystemID")
            strSQL.AppendLine("And FlowCode = @FlowCode")
            strSQL.AppendLine("And FlowType = @FlowType")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@SystemID", DbType.String, HRFlowTypeDefineRow.SystemID.Value)
            db.AddInParameter(dbcmd, "@FlowCode", DbType.String, HRFlowTypeDefineRow.FlowCode.Value)
            db.AddInParameter(dbcmd, "@FlowType", DbType.String, HRFlowTypeDefineRow.FlowType.Value)

            Return db.ExecuteNonQuery(dbcmd)
        End Function

        Public Function DeleteRowByPrimaryKey(ByVal HRFlowTypeDefineRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("AattendantDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Delete From HRFlowTypeDefine")
            strSQL.AppendLine("Where SystemID = @SystemID")
            strSQL.AppendLine("And FlowCode = @FlowCode")
            strSQL.AppendLine("And FlowType = @FlowType")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@SystemID", DbType.String, HRFlowTypeDefineRow.SystemID.Value)
            db.AddInParameter(dbcmd, "@FlowCode", DbType.String, HRFlowTypeDefineRow.FlowCode.Value)
            db.AddInParameter(dbcmd, "@FlowType", DbType.String, HRFlowTypeDefineRow.FlowType.Value)

            Return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function DeleteRowByPrimaryKey(ByVal HRFlowTypeDefineRow As Row()) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("AattendantDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Delete From HRFlowTypeDefine")
            strSQL.AppendLine("Where SystemID = @SystemID")
            strSQL.AppendLine("And FlowCode = @FlowCode")
            strSQL.AppendLine("And FlowType = @FlowType")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            Using cn As DbConnection = db.CreateConnection()
                cn.Open()
                Dim tran As DbTransaction = cn.BeginTransaction()
                Dim inTrans As Boolean = True

                Try
                    For Each r As Row In HRFlowTypeDefineRow
                        dbcmd.Parameters.Clear()
                        db.AddInParameter(dbcmd, "@SystemID", DbType.String, r.SystemID.Value)
                        db.AddInParameter(dbcmd, "@FlowCode", DbType.String, r.FlowCode.Value)
                        db.AddInParameter(dbcmd, "@FlowType", DbType.String, r.FlowType.Value)

                        intRowsAffected += db.ExecuteNonQuery(dbcmd, tran)
                    Next
                    tran.Commit()
                    inTrans = False
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

        Public Function DeleteRowByPrimaryKey(ByVal HRFlowTypeDefineRow As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("AattendantDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Delete From HRFlowTypeDefine")
            strSQL.AppendLine("Where SystemID = @SystemID")
            strSQL.AppendLine("And FlowCode = @FlowCode")
            strSQL.AppendLine("And FlowType = @FlowType")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())

            For Each r As Row In HRFlowTypeDefineRow
                dbcmd.Parameters.Clear()
                db.AddInParameter(dbcmd, "@SystemID", DbType.String, r.SystemID.Value)
                db.AddInParameter(dbcmd, "@FlowCode", DbType.String, r.FlowCode.Value)
                db.AddInParameter(dbcmd, "@FlowType", DbType.String, r.FlowType.Value)

                intRowsAffected += db.ExecuteNonQuery(dbcmd, tran)
            Next
            Return intRowsAffected
        End Function

        Public Function QueryByKey(ByVal HRFlowTypeDefineRow As Row) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase("AattendantDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From HRFlowTypeDefine")
            strSQL.AppendLine("Where SystemID = @SystemID")
            strSQL.AppendLine("And FlowCode = @FlowCode")
            strSQL.AppendLine("And FlowType = @FlowType")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@SystemID", DbType.String, HRFlowTypeDefineRow.SystemID.Value)
            db.AddInParameter(dbcmd, "@FlowCode", DbType.String, HRFlowTypeDefineRow.FlowCode.Value)
            db.AddInParameter(dbcmd, "@FlowType", DbType.String, HRFlowTypeDefineRow.FlowType.Value)

            Return db.ExecuteDataSet(dbcmd)
        End Function

        Public Function QueryByKey(HRFlowTypeDefineRow As Row, tran As DbTransaction) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase("AattendantDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From HRFlowTypeDefine")
            strSQL.AppendLine("Where SystemID = @SystemID")
            strSQL.AppendLine("And FlowCode = @FlowCode")
            strSQL.AppendLine("And FlowType = @FlowType")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@SystemID", DbType.String, HRFlowTypeDefineRow.SystemID.Value)
            db.AddInParameter(dbcmd, "@FlowCode", DbType.String, HRFlowTypeDefineRow.FlowCode.Value)
            db.AddInParameter(dbcmd, "@FlowType", DbType.String, HRFlowTypeDefineRow.FlowType.Value)

            Return db.ExecuteDataSet(dbcmd, tran)
        End Function

        Public Function Update(ByVal HRFlowTypeDefineRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("AattendantDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim strDot As String = String.Empty

            strSQL.AppendLine("Update HRFlowTypeDefine Set")
            For i As Integer = 0 To HRFlowTypeDefineRow.FieldNames.Length - 1
                If Not HRFlowTypeDefineRow.IsIdentityField(HRFlowTypeDefineRow.FieldNames(i)) AndAlso HRFlowTypeDefineRow.IsUpdated(HRFlowTypeDefineRow.FieldNames(i)) AndAlso HRFlowTypeDefineRow.CreateUpdateSQL(HRFlowTypeDefineRow.FieldNames(i)) Then
                    strSQL.AppendLine(String.Format("{0}{1} = @{1}", strDot, HRFlowTypeDefineRow.FieldNames(i)))
                    strDot = ","
                End If
            Next
            If strDot = String.Empty Then Throw New Exception("未異動資料欄位！")
            strSQL.AppendLine("Where SystemID = @PKSystemID")
            strSQL.AppendLine("And FlowCode = @PKFlowCode")
            strSQL.AppendLine("And FlowType = @PKFlowType")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            If HRFlowTypeDefineRow.SystemID.Updated Then db.AddInParameter(dbcmd, "@SystemID", DbType.String, HRFlowTypeDefineRow.SystemID.Value)
            If HRFlowTypeDefineRow.FlowCode.Updated Then db.AddInParameter(dbcmd, "@FlowCode", DbType.String, HRFlowTypeDefineRow.FlowCode.Value)
            If HRFlowTypeDefineRow.FlowType.Updated Then db.AddInParameter(dbcmd, "@FlowType", DbType.String, HRFlowTypeDefineRow.FlowType.Value)
            If HRFlowTypeDefineRow.FlowTypeName.Updated Then db.AddInParameter(dbcmd, "@FlowTypeName", DbType.String, HRFlowTypeDefineRow.FlowTypeName.Value)
            If HRFlowTypeDefineRow.FlowTypeDescription.Updated Then db.AddInParameter(dbcmd, "@FlowTypeDescription", DbType.String, HRFlowTypeDefineRow.FlowTypeDescription.Value)
            If HRFlowTypeDefineRow.FlowTypeFlag.Updated Then db.AddInParameter(dbcmd, "@FlowTypeFlag", DbType.String, HRFlowTypeDefineRow.FlowTypeFlag.Value)
            If HRFlowTypeDefineRow.FlowSN.Updated Then db.AddInParameter(dbcmd, "@FlowSN", DbType.String, HRFlowTypeDefineRow.FlowSN.Value)
            If HRFlowTypeDefineRow.LastChgComp.Updated Then db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, HRFlowTypeDefineRow.LastChgComp.Value)
            If HRFlowTypeDefineRow.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, HRFlowTypeDefineRow.LastChgID.Value)
            If HRFlowTypeDefineRow.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(HRFlowTypeDefineRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), HRFlowTypeDefineRow.LastChgDate.Value))
            db.AddInParameter(dbcmd, "@PKSystemID", DbType.String, IIf(HRFlowTypeDefineRow.LoadFromDataRow, HRFlowTypeDefineRow.SystemID.OldValue, HRFlowTypeDefineRow.SystemID.Value))
            db.AddInParameter(dbcmd, "@PKFlowCode", DbType.String, IIf(HRFlowTypeDefineRow.LoadFromDataRow, HRFlowTypeDefineRow.FlowCode.OldValue, HRFlowTypeDefineRow.FlowCode.Value))
            db.AddInParameter(dbcmd, "@PKFlowType", DbType.String, IIf(HRFlowTypeDefineRow.LoadFromDataRow, HRFlowTypeDefineRow.FlowType.OldValue, HRFlowTypeDefineRow.FlowType.Value))

            Return db.ExecuteNonQuery(dbcmd)
        End Function

        Public Function Update(ByVal HRFlowTypeDefineRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("AattendantDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim strDot As String = String.Empty

            strSQL.AppendLine("Update HRFlowTypeDefine Set")
            For i As Integer = 0 To HRFlowTypeDefineRow.FieldNames.Length - 1
                If Not HRFlowTypeDefineRow.IsIdentityField(HRFlowTypeDefineRow.FieldNames(i)) AndAlso HRFlowTypeDefineRow.IsUpdated(HRFlowTypeDefineRow.FieldNames(i)) AndAlso HRFlowTypeDefineRow.CreateUpdateSQL(HRFlowTypeDefineRow.FieldNames(i)) Then
                    strSQL.AppendLine(String.Format("{0}{1} = @{1}", strDot, HRFlowTypeDefineRow.FieldNames(i)))
                    strDot = ","
                End If
            Next
            If strDot = String.Empty Then Throw New Exception("未異動資料欄位！")
            strSQL.AppendLine("Where SystemID = @PKSystemID")
            strSQL.AppendLine("And FlowCode = @PKFlowCode")
            strSQL.AppendLine("And FlowType = @PKFlowType")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            If HRFlowTypeDefineRow.SystemID.Updated Then db.AddInParameter(dbcmd, "@SystemID", DbType.String, HRFlowTypeDefineRow.SystemID.Value)
            If HRFlowTypeDefineRow.FlowCode.Updated Then db.AddInParameter(dbcmd, "@FlowCode", DbType.String, HRFlowTypeDefineRow.FlowCode.Value)
            If HRFlowTypeDefineRow.FlowType.Updated Then db.AddInParameter(dbcmd, "@FlowType", DbType.String, HRFlowTypeDefineRow.FlowType.Value)
            If HRFlowTypeDefineRow.FlowTypeName.Updated Then db.AddInParameter(dbcmd, "@FlowTypeName", DbType.String, HRFlowTypeDefineRow.FlowTypeName.Value)
            If HRFlowTypeDefineRow.FlowTypeDescription.Updated Then db.AddInParameter(dbcmd, "@FlowTypeDescription", DbType.String, HRFlowTypeDefineRow.FlowTypeDescription.Value)
            If HRFlowTypeDefineRow.FlowTypeFlag.Updated Then db.AddInParameter(dbcmd, "@FlowTypeFlag", DbType.String, HRFlowTypeDefineRow.FlowTypeFlag.Value)
            If HRFlowTypeDefineRow.FlowSN.Updated Then db.AddInParameter(dbcmd, "@FlowSN", DbType.String, HRFlowTypeDefineRow.FlowSN.Value)
            If HRFlowTypeDefineRow.LastChgComp.Updated Then db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, HRFlowTypeDefineRow.LastChgComp.Value)
            If HRFlowTypeDefineRow.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, HRFlowTypeDefineRow.LastChgID.Value)
            If HRFlowTypeDefineRow.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(HRFlowTypeDefineRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), HRFlowTypeDefineRow.LastChgDate.Value))
            db.AddInParameter(dbcmd, "@PKSystemID", DbType.String, IIf(HRFlowTypeDefineRow.LoadFromDataRow, HRFlowTypeDefineRow.SystemID.OldValue, HRFlowTypeDefineRow.SystemID.Value))
            db.AddInParameter(dbcmd, "@PKFlowCode", DbType.String, IIf(HRFlowTypeDefineRow.LoadFromDataRow, HRFlowTypeDefineRow.FlowCode.OldValue, HRFlowTypeDefineRow.FlowCode.Value))
            db.AddInParameter(dbcmd, "@PKFlowType", DbType.String, IIf(HRFlowTypeDefineRow.LoadFromDataRow, HRFlowTypeDefineRow.FlowType.OldValue, HRFlowTypeDefineRow.FlowType.Value))

            Return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function Update(ByVal HRFlowTypeDefineRow As Row()) As Integer
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
                    For Each r As Row In HRFlowTypeDefineRow
                        strDot = String.Empty
                        If strSQL.Length > 0 Then strSQL.Remove(0, strSQL.Length)
                        strSQL.AppendLine("Update HRFlowTypeDefine Set")
                        For i As Integer = 0 To r.FieldNames.Length - 1
                            If Not r.IsIdentityField(r.FieldNames(i)) AndAlso r.IsUpdated(r.FieldNames(i)) AndAlso r.CreateUpdateSQL(r.FieldNames(i)) Then
                                strSQL.AppendLine(String.Format("{0}{1} = @{1}", strDot, r.FieldNames(i)))
                                strDot = ","
                            End If
                        Next
                        If strDot = String.Empty Then Continue For
                        strSQL.AppendLine("Where SystemID = @PKSystemID")
                        strSQL.AppendLine("And FlowCode = @PKFlowCode")
                        strSQL.AppendLine("And FlowType = @PKFlowType")

                        dbcmd = db.GetSqlStringCommand(strSQL.ToString())
                        If r.SystemID.Updated Then db.AddInParameter(dbcmd, "@SystemID", DbType.String, r.SystemID.Value)
                        If r.FlowCode.Updated Then db.AddInParameter(dbcmd, "@FlowCode", DbType.String, r.FlowCode.Value)
                        If r.FlowType.Updated Then db.AddInParameter(dbcmd, "@FlowType", DbType.String, r.FlowType.Value)
                        If r.FlowTypeName.Updated Then db.AddInParameter(dbcmd, "@FlowTypeName", DbType.String, r.FlowTypeName.Value)
                        If r.FlowTypeDescription.Updated Then db.AddInParameter(dbcmd, "@FlowTypeDescription", DbType.String, r.FlowTypeDescription.Value)
                        If r.FlowTypeFlag.Updated Then db.AddInParameter(dbcmd, "@FlowTypeFlag", DbType.String, r.FlowTypeFlag.Value)
                        If r.FlowSN.Updated Then db.AddInParameter(dbcmd, "@FlowSN", DbType.String, r.FlowSN.Value)
                        If r.LastChgComp.Updated Then db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, r.LastChgComp.Value)
                        If r.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, r.LastChgID.Value)
                        If r.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(r.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), r.LastChgDate.Value))
                        db.AddInParameter(dbcmd, "@PKSystemID", DbType.String, IIf(r.LoadFromDataRow, r.SystemID.OldValue, r.SystemID.Value))
                        db.AddInParameter(dbcmd, "@PKFlowCode", DbType.String, IIf(r.LoadFromDataRow, r.FlowCode.OldValue, r.FlowCode.Value))
                        db.AddInParameter(dbcmd, "@PKFlowType", DbType.String, IIf(r.LoadFromDataRow, r.FlowType.OldValue, r.FlowType.Value))

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

        Public Function Update(ByVal HRFlowTypeDefineRow As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("AattendantDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0
            Dim strDot As String

            For Each r As Row In HRFlowTypeDefineRow
                strDot = String.Empty
                If strSQL.Length > 0 Then strSQL.Remove(0, strSQL.Length)
                strSQL.AppendLine("Update HRFlowTypeDefine Set")
                For i As Integer = 0 To r.FieldNames.Length - 1
                    If Not r.IsIdentityField(r.FieldNames(i)) AndAlso r.IsUpdated(r.FieldNames(i)) AndAlso r.CreateUpdateSQL(r.FieldNames(i)) Then
                        strSQL.AppendLine(String.Format("{0}{1} = @{1}", strDot, r.FieldNames(i)))
                        strDot = ","
                    End If
                Next
                If strDot = String.Empty Then Continue For
                strSQL.AppendLine("Where SystemID = @PKSystemID")
                strSQL.AppendLine("And FlowCode = @PKFlowCode")
                strSQL.AppendLine("And FlowType = @PKFlowType")

                dbcmd = db.GetSqlStringCommand(strSQL.ToString())
                If r.SystemID.Updated Then db.AddInParameter(dbcmd, "@SystemID", DbType.String, r.SystemID.Value)
                If r.FlowCode.Updated Then db.AddInParameter(dbcmd, "@FlowCode", DbType.String, r.FlowCode.Value)
                If r.FlowType.Updated Then db.AddInParameter(dbcmd, "@FlowType", DbType.String, r.FlowType.Value)
                If r.FlowTypeName.Updated Then db.AddInParameter(dbcmd, "@FlowTypeName", DbType.String, r.FlowTypeName.Value)
                If r.FlowTypeDescription.Updated Then db.AddInParameter(dbcmd, "@FlowTypeDescription", DbType.String, r.FlowTypeDescription.Value)
                If r.FlowTypeFlag.Updated Then db.AddInParameter(dbcmd, "@FlowTypeFlag", DbType.String, r.FlowTypeFlag.Value)
                If r.FlowSN.Updated Then db.AddInParameter(dbcmd, "@FlowSN", DbType.String, r.FlowSN.Value)
                If r.LastChgComp.Updated Then db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, r.LastChgComp.Value)
                If r.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, r.LastChgID.Value)
                If r.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(r.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), r.LastChgDate.Value))
                db.AddInParameter(dbcmd, "@PKSystemID", DbType.String, IIf(r.LoadFromDataRow, r.SystemID.OldValue, r.SystemID.Value))
                db.AddInParameter(dbcmd, "@PKFlowCode", DbType.String, IIf(r.LoadFromDataRow, r.FlowCode.OldValue, r.FlowCode.Value))
                db.AddInParameter(dbcmd, "@PKFlowType", DbType.String, IIf(r.LoadFromDataRow, r.FlowType.OldValue, r.FlowType.Value))

                intRowsAffected += db.ExecuteNonQuery(dbcmd, tran)
            Next
            Return intRowsAffected
        End Function

        Public Function IsDataExists(ByVal HRFlowTypeDefineRow As Row) As Boolean
            Dim db As Database = DatabaseFactory.CreateDatabase("AattendantDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intCount As Integer = 0

            strSQL.AppendLine("Select Count(*) Cnt From HRFlowTypeDefine")
            strSQL.AppendLine("Where SystemID = @SystemID")
            strSQL.AppendLine("And FlowCode = @FlowCode")
            strSQL.AppendLine("And FlowType = @FlowType")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@SystemID", DbType.String, HRFlowTypeDefineRow.SystemID.Value)
            db.AddInParameter(dbcmd, "@FlowCode", DbType.String, HRFlowTypeDefineRow.FlowCode.Value)
            db.AddInParameter(dbcmd, "@FlowType", DbType.String, HRFlowTypeDefineRow.FlowType.Value)

            intCount = Convert.ToInt32(db.ExecuteScalar(dbcmd))
            Return IIf(intCount > 0, True, False)
        End Function

        Public Function IsDataExists(ByVal HRFlowTypeDefineRow As Row, ByVal tran As DbTransaction) As Boolean
            Dim db As Database = DatabaseFactory.CreateDatabase("AattendantDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intCount As Integer = 0

            strSQL.AppendLine("Select Count(*) Cnt From HRFlowTypeDefine")
            strSQL.AppendLine("Where SystemID = @SystemID")
            strSQL.AppendLine("And FlowCode = @FlowCode")
            strSQL.AppendLine("And FlowType = @FlowType")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@SystemID", DbType.String, HRFlowTypeDefineRow.SystemID.Value)
            db.AddInParameter(dbcmd, "@FlowCode", DbType.String, HRFlowTypeDefineRow.FlowCode.Value)
            db.AddInParameter(dbcmd, "@FlowType", DbType.String, HRFlowTypeDefineRow.FlowType.Value)

            intCount = Convert.ToInt32(db.ExecuteScalar(dbcmd, tran))
            Return IIf(intCount > 0, True, False)
        End Function

        Public Function QuerybyWhere(WhereCondition As String) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase("AattendantDB")
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From HRFlowTypeDefine")
            If WhereCondition <> String.Empty Then strSQL.AppendLine(WhereCondition)

            Return db.ExecuteDataSet(CommandType.Text, strSQL.ToString())
        End Function

        Public Function Insert(ByVal HRFlowTypeDefineRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("AattendantDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Insert into HRFlowTypeDefine")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    SystemID, FlowCode, FlowType, FlowTypeName, FlowTypeDescription, FlowTypeFlag, FlowSN,")
            strSQL.AppendLine("    LastChgComp, LastChgID, LastChgDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @SystemID, @FlowCode, @FlowType, @FlowTypeName, @FlowTypeDescription, @FlowTypeFlag, @FlowSN,")
            strSQL.AppendLine("    @LastChgComp, @LastChgID, @LastChgDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@SystemID", DbType.String, HRFlowTypeDefineRow.SystemID.Value)
            db.AddInParameter(dbcmd, "@FlowCode", DbType.String, HRFlowTypeDefineRow.FlowCode.Value)
            db.AddInParameter(dbcmd, "@FlowType", DbType.String, HRFlowTypeDefineRow.FlowType.Value)
            db.AddInParameter(dbcmd, "@FlowTypeName", DbType.String, HRFlowTypeDefineRow.FlowTypeName.Value)
            db.AddInParameter(dbcmd, "@FlowTypeDescription", DbType.String, HRFlowTypeDefineRow.FlowTypeDescription.Value)
            db.AddInParameter(dbcmd, "@FlowTypeFlag", DbType.String, HRFlowTypeDefineRow.FlowTypeFlag.Value)
            db.AddInParameter(dbcmd, "@FlowSN", DbType.String, HRFlowTypeDefineRow.FlowSN.Value)
            db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, HRFlowTypeDefineRow.LastChgComp.Value)
            db.AddInParameter(dbcmd, "@LastChgID", DbType.String, HRFlowTypeDefineRow.LastChgID.Value)
            db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(HRFlowTypeDefineRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), HRFlowTypeDefineRow.LastChgDate.Value))

            Return db.ExecuteNonQuery(dbcmd)
        End Function

        Public Function Insert(ByVal HRFlowTypeDefineRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("AattendantDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Insert into HRFlowTypeDefine")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    SystemID, FlowCode, FlowType, FlowTypeName, FlowTypeDescription, FlowTypeFlag, FlowSN,")
            strSQL.AppendLine("    LastChgComp, LastChgID, LastChgDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @SystemID, @FlowCode, @FlowType, @FlowTypeName, @FlowTypeDescription, @FlowTypeFlag, @FlowSN,")
            strSQL.AppendLine("    @LastChgComp, @LastChgID, @LastChgDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@SystemID", DbType.String, HRFlowTypeDefineRow.SystemID.Value)
            db.AddInParameter(dbcmd, "@FlowCode", DbType.String, HRFlowTypeDefineRow.FlowCode.Value)
            db.AddInParameter(dbcmd, "@FlowType", DbType.String, HRFlowTypeDefineRow.FlowType.Value)
            db.AddInParameter(dbcmd, "@FlowTypeName", DbType.String, HRFlowTypeDefineRow.FlowTypeName.Value)
            db.AddInParameter(dbcmd, "@FlowTypeDescription", DbType.String, HRFlowTypeDefineRow.FlowTypeDescription.Value)
            db.AddInParameter(dbcmd, "@FlowTypeFlag", DbType.String, HRFlowTypeDefineRow.FlowTypeFlag.Value)
            db.AddInParameter(dbcmd, "@FlowSN", DbType.String, HRFlowTypeDefineRow.FlowSN.Value)
            db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, HRFlowTypeDefineRow.LastChgComp.Value)
            db.AddInParameter(dbcmd, "@LastChgID", DbType.String, HRFlowTypeDefineRow.LastChgID.Value)
            db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(HRFlowTypeDefineRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), HRFlowTypeDefineRow.LastChgDate.Value))

            Return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function Insert(ByVal HRFlowTypeDefineRow As Row()) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("AattendantDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Insert into HRFlowTypeDefine")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    SystemID, FlowCode, FlowType, FlowTypeName, FlowTypeDescription, FlowTypeFlag, FlowSN,")
            strSQL.AppendLine("    LastChgComp, LastChgID, LastChgDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @SystemID, @FlowCode, @FlowType, @FlowTypeName, @FlowTypeDescription, @FlowTypeFlag, @FlowSN,")
            strSQL.AppendLine("    @LastChgComp, @LastChgID, @LastChgDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            Using cn As DbConnection = db.CreateConnection()
                cn.Open()
                Dim tran As DbTransaction = cn.BeginTransaction()
                Dim inTrans As Boolean = True

                Try
                    For Each r As Row In HRFlowTypeDefineRow
                        dbcmd.Parameters.Clear()
                        db.AddInParameter(dbcmd, "@SystemID", DbType.String, r.SystemID.Value)
                        db.AddInParameter(dbcmd, "@FlowCode", DbType.String, r.FlowCode.Value)
                        db.AddInParameter(dbcmd, "@FlowType", DbType.String, r.FlowType.Value)
                        db.AddInParameter(dbcmd, "@FlowTypeName", DbType.String, r.FlowTypeName.Value)
                        db.AddInParameter(dbcmd, "@FlowTypeDescription", DbType.String, r.FlowTypeDescription.Value)
                        db.AddInParameter(dbcmd, "@FlowTypeFlag", DbType.String, r.FlowTypeFlag.Value)
                        db.AddInParameter(dbcmd, "@FlowSN", DbType.String, r.FlowSN.Value)
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

        Public Function Insert(ByVal HRFlowTypeDefineRow As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("AattendantDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Insert into HRFlowTypeDefine")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    SystemID, FlowCode, FlowType, FlowTypeName, FlowTypeDescription, FlowTypeFlag, FlowSN,")
            strSQL.AppendLine("    LastChgComp, LastChgID, LastChgDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @SystemID, @FlowCode, @FlowType, @FlowTypeName, @FlowTypeDescription, @FlowTypeFlag, @FlowSN,")
            strSQL.AppendLine("    @LastChgComp, @LastChgID, @LastChgDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())

            For Each r As Row In HRFlowTypeDefineRow
                dbcmd.Parameters.Clear()
                db.AddInParameter(dbcmd, "@SystemID", DbType.String, r.SystemID.Value)
                db.AddInParameter(dbcmd, "@FlowCode", DbType.String, r.FlowCode.Value)
                db.AddInParameter(dbcmd, "@FlowType", DbType.String, r.FlowType.Value)
                db.AddInParameter(dbcmd, "@FlowTypeName", DbType.String, r.FlowTypeName.Value)
                db.AddInParameter(dbcmd, "@FlowTypeDescription", DbType.String, r.FlowTypeDescription.Value)
                db.AddInParameter(dbcmd, "@FlowTypeFlag", DbType.String, r.FlowTypeFlag.Value)
                db.AddInParameter(dbcmd, "@FlowSN", DbType.String, r.FlowSN.Value)
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

