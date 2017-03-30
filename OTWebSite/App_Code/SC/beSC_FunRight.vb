'****************************************************************
' Table:SC_FunRight
' Created Date: 2014.08.06
'****************************************************************/
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports Microsoft.Practices.EnterpriseLibrary.Common
Imports Microsoft.Practices.EnterpriseLibrary.Data
Imports System.Data.Common
Imports System.Data

Namespace beSC_FunRight
    Public Class Table
        Private m_Rows As Rows = New Rows()
        Private m_Fields As String() = { "SysID", "FunID", "RightID", "IsVisible", "Caption", "CaptionEng" }
        Private m_Types As System.Type() = { GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String) }
        Private m_PrimaryFields As String() = { "SysID", "FunID", "RightID" }

        Public ReadOnly Property Rows() As beSC_FunRight.Rows 
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
        Public Sub Transfer2Row(SC_FunRightTable As DataTable)
            For Each dr As DataRow In SC_FunRightTable.Rows
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

                dr(m_Rows(i).SysID.FieldName) = m_Rows(i).SysID.Value
                dr(m_Rows(i).FunID.FieldName) = m_Rows(i).FunID.Value
                dr(m_Rows(i).RightID.FieldName) = m_Rows(i).RightID.Value
                dr(m_Rows(i).IsVisible.FieldName) = m_Rows(i).IsVisible.Value
                dr(m_Rows(i).Caption.FieldName) = m_Rows(i).Caption.Value
                dr(m_Rows(i).CaptionEng.FieldName) = m_Rows(i).CaptionEng.Value

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

        Public Sub Add(SC_FunRightRow As Row)
            m_Rows.Add(SC_FunRightRow)
        End Sub

        Public Sub Remove(SC_FunRightRow As Row)
            If m_Rows.IndexOf(SC_FunRightRow) >= 0 Then
                m_Rows.Remove(SC_FunRightRow)
            End If
        End Sub

        Public Sub Dispose()
            m_Rows.Clear()
        End Sub

    End Class

    Public Class Row

        Private FI_SysID As Field(Of String) = new Field(Of String)("SysID", true)
        Private FI_FunID As Field(Of String) = new Field(Of String)("FunID", true)
        Private FI_RightID As Field(Of String) = new Field(Of String)("RightID", true)
        Private FI_IsVisible As Field(Of String) = new Field(Of String)("IsVisible", true)
        Private FI_Caption As Field(Of String) = new Field(Of String)("Caption", true)
        Private FI_CaptionEng As Field(Of String) = new Field(Of String)("CaptionEng", true)
        Private m_FieldNames As String() = { "SysID", "FunID", "RightID", "IsVisible", "Caption", "CaptionEng" }
        Private m_PrimaryFields As String() = { "SysID", "FunID", "RightID" }
        Private m_IdentityFields As String() = {  }
        Private m_LoadFromDataRow As Boolean = False

        Private Function GetFieldValue(ByVal fieldName As String) As Object
            Select Case fieldName
                Case "SysID"
                    Return FI_SysID.Value
                Case "FunID"
                    Return FI_FunID.Value
                Case "RightID"
                    Return FI_RightID.Value
                Case "IsVisible"
                    Return FI_IsVisible.Value
                Case "Caption"
                    Return FI_Caption.Value
                Case "CaptionEng"
                    Return FI_CaptionEng.Value
                Case Else
                    Return Nothing
            End Select
        End Function

        Private Sub SetFieldValue(ByVal fieldName As String, ByVal value As Object)
            Select Case fieldName
                Case "SysID"
                    FI_SysID.SetValue(value)
                Case "FunID"
                    FI_FunID.SetValue(value)
                Case "RightID"
                    FI_RightID.SetValue(value)
                Case "IsVisible"
                    FI_IsVisible.SetValue(value)
                Case "Caption"
                    FI_Caption.SetValue(value)
                Case "CaptionEng"
                    FI_CaptionEng.SetValue(value)
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
                Case "SysID"
                    return FI_SysID.Updated
                Case "FunID"
                    return FI_FunID.Updated
                Case "RightID"
                    return FI_RightID.Updated
                Case "IsVisible"
                    return FI_IsVisible.Updated
                Case "Caption"
                    return FI_Caption.Updated
                Case "CaptionEng"
                    return FI_CaptionEng.Updated
                Case Else
                    Throw New Exception("無此欄位！")
            End Select
        End Function

        Public Function CreateUpdateSQL(ByVal fieldName As String) As Boolean
            Select Case fieldName
                Case "SysID"
                    return FI_SysID.CreateUpdateSQL
                Case "FunID"
                    return FI_FunID.CreateUpdateSQL
                Case "RightID"
                    return FI_RightID.CreateUpdateSQL
                Case "IsVisible"
                    return FI_IsVisible.CreateUpdateSQL
                Case "Caption"
                    return FI_Caption.CreateUpdateSQL
                Case "CaptionEng"
                    return FI_CaptionEng.CreateUpdateSQL
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
            FI_SysID.SetInitValue("")
            FI_FunID.SetInitValue("")
            FI_RightID.SetInitValue("")
            FI_IsVisible.SetInitValue("")
            FI_Caption.SetInitValue("")
            FI_CaptionEng.SetInitValue("")
        End Sub

        Public Sub New(ByVal dr As System.Data.DataRow)
            FI_SysID.SetInitValue(dr("SysID"))
            FI_FunID.SetInitValue(dr("FunID"))
            FI_RightID.SetInitValue(dr("RightID"))
            FI_IsVisible.SetInitValue(dr("IsVisible"))
            FI_Caption.SetInitValue(dr("Caption"))
            FI_CaptionEng.SetInitValue(dr("CaptionEng"))

            m_LoadFromDataRow = True
        End Sub

        Private Sub ClearUpdated()
            FI_SysID.Updated = False
            FI_FunID.Updated = False
            FI_RightID.Updated = False
            FI_IsVisible.Updated = False
            FI_Caption.Updated = False
            FI_CaptionEng.Updated = False
        End Sub

        Public ReadOnly Property SysID As Field(Of String) 
            Get
                Return FI_SysID
            End Get
        End Property

        Public ReadOnly Property FunID As Field(Of String) 
            Get
                Return FI_FunID
            End Get
        End Property

        Public ReadOnly Property RightID As Field(Of String) 
            Get
                Return FI_RightID
            End Get
        End Property

        Public ReadOnly Property IsVisible As Field(Of String) 
            Get
                Return FI_IsVisible
            End Get
        End Property

        Public ReadOnly Property Caption As Field(Of String) 
            Get
                Return FI_Caption
            End Get
        End Property

        Public ReadOnly Property CaptionEng As Field(Of String) 
            Get
                Return FI_CaptionEng
            End Get
        End Property

    End Class

    Public Class Service
        Public Function DeleteRowByPrimaryKey(ByVal SC_FunRightRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Delete From SC_FunRight")
            strSQL.AppendLine("Where SysID = @SysID")
            strSQL.AppendLine("And FunID = @FunID")
            strSQL.AppendLine("And RightID = @RightID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@SysID", DbType.String, SC_FunRightRow.SysID.Value)
            db.AddInParameter(dbcmd, "@FunID", DbType.String, SC_FunRightRow.FunID.Value)
            db.AddInParameter(dbcmd, "@RightID", DbType.String, SC_FunRightRow.RightID.Value)

            return db.ExecuteNonQuery(dbcmd)
        End Function

        public Function DeleteRowByPrimaryKey(ByVal SC_FunRightRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Delete From SC_FunRight")
            strSQL.AppendLine("Where SysID = @SysID")
            strSQL.AppendLine("And FunID = @FunID")
            strSQL.AppendLine("And RightID = @RightID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@SysID", DbType.String, SC_FunRightRow.SysID.Value)
            db.AddInParameter(dbcmd, "@FunID", DbType.String, SC_FunRightRow.FunID.Value)
            db.AddInParameter(dbcmd, "@RightID", DbType.String, SC_FunRightRow.RightID.Value)

            return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function DeleteRowByPrimaryKey(ByVal SC_FunRightRow As Row()) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Delete From SC_FunRight")
            strSQL.AppendLine("Where SysID = @SysID")
            strSQL.AppendLine("And FunID = @FunID")
            strSQL.AppendLine("And RightID = @RightID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            using cn As DbConnection = db.CreateConnection()
                cn.Open()
                Dim tran As DbTransaction = cn.BeginTransaction()
                Dim inTrans As Boolean = True

                Try
                    For Each r As Row In SC_FunRightRow
                        dbcmd.Parameters.Clear()
                        db.AddInParameter(dbcmd, "@SysID", DbType.String, r.SysID.Value)
                        db.AddInParameter(dbcmd, "@FunID", DbType.String, r.FunID.Value)
                        db.AddInParameter(dbcmd, "@RightID", DbType.String, r.RightID.Value)

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

        Public Function DeleteRowByPrimaryKey(ByVal SC_FunRightRow As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Delete From SC_FunRight")
            strSQL.AppendLine("Where SysID = @SysID")
            strSQL.AppendLine("And FunID = @FunID")
            strSQL.AppendLine("And RightID = @RightID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())

            For Each r As Row In SC_FunRightRow
                dbcmd.Parameters.Clear()
                db.AddInParameter(dbcmd, "@SysID", DbType.String, r.SysID.Value)
                db.AddInParameter(dbcmd, "@FunID", DbType.String, r.FunID.Value)
                db.AddInParameter(dbcmd, "@RightID", DbType.String, r.RightID.Value)

                intRowsAffected += db.ExecuteNonQuery(dbcmd, tran)
            Next
            Return intRowsAffected
        End Function

        Public Function QueryByKey(ByVal SC_FunRightRow As Row) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From SC_FunRight")
            strSQL.AppendLine("Where SysID = @SysID")
            strSQL.AppendLine("And FunID = @FunID")
            strSQL.AppendLine("And RightID = @RightID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@SysID", DbType.String, SC_FunRightRow.SysID.Value)
            db.AddInParameter(dbcmd, "@FunID", DbType.String, SC_FunRightRow.FunID.Value)
            db.AddInParameter(dbcmd, "@RightID", DbType.String, SC_FunRightRow.RightID.Value)

            Return db.ExecuteDataSet(dbcmd)
        End Function

        Public Function QueryByKey(SC_FunRightRow As Row, tran As DbTransaction) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From SC_FunRight")
            strSQL.AppendLine("Where SysID = @SysID")
            strSQL.AppendLine("And FunID = @FunID")
            strSQL.AppendLine("And RightID = @RightID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@SysID", DbType.String, SC_FunRightRow.SysID.Value)
            db.AddInParameter(dbcmd, "@FunID", DbType.String, SC_FunRightRow.FunID.Value)
            db.AddInParameter(dbcmd, "@RightID", DbType.String, SC_FunRightRow.RightID.Value)

            Return db.ExecuteDataSet(dbcmd, tran)
        End Function

        Public Function Update(ByVal SC_FunRightRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim strDot As String = String.Empty

            strSQL.AppendLine("Update SC_FunRight Set")
            For i As Integer = 0 To SC_FunRightRow.FieldNames.Length - 1
                If Not SC_FunRightRow.IsIdentityField(SC_FunRightRow.FieldNames(i)) AndAlso SC_FunRightRow.IsUpdated(SC_FunRightRow.FieldNames(i)) AndAlso SC_FunRightRow.CreateUpdateSQL(SC_FunRightRow.FieldNames(i)) Then
                    strSQL.AppendLine(string.Format("{0}{1} = @{1}", strDot, SC_FunRightRow.FieldNames(i)))
                    strDot = ","
                End If
            Next
            If strDot = String.Empty Then Throw New Exception("未異動資料欄位！")
            strSQL.AppendLine("Where SysID = @PKSysID")
            strSQL.AppendLine("And FunID = @PKFunID")
            strSQL.AppendLine("And RightID = @PKRightID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            If SC_FunRightRow.SysID.Updated Then db.AddInParameter(dbcmd, "@SysID", DbType.String, SC_FunRightRow.SysID.Value)
            If SC_FunRightRow.FunID.Updated Then db.AddInParameter(dbcmd, "@FunID", DbType.String, SC_FunRightRow.FunID.Value)
            If SC_FunRightRow.RightID.Updated Then db.AddInParameter(dbcmd, "@RightID", DbType.String, SC_FunRightRow.RightID.Value)
            If SC_FunRightRow.IsVisible.Updated Then db.AddInParameter(dbcmd, "@IsVisible", DbType.String, SC_FunRightRow.IsVisible.Value)
            If SC_FunRightRow.Caption.Updated Then db.AddInParameter(dbcmd, "@Caption", DbType.String, SC_FunRightRow.Caption.Value)
            If SC_FunRightRow.CaptionEng.Updated Then db.AddInParameter(dbcmd, "@CaptionEng", DbType.String, SC_FunRightRow.CaptionEng.Value)
            db.AddInParameter(dbcmd, "@PKSysID", DbType.String, IIf(SC_FunRightRow.LoadFromDataRow, SC_FunRightRow.SysID.OldValue, SC_FunRightRow.SysID.Value))
            db.AddInParameter(dbcmd, "@PKFunID", DbType.String, IIf(SC_FunRightRow.LoadFromDataRow, SC_FunRightRow.FunID.OldValue, SC_FunRightRow.FunID.Value))
            db.AddInParameter(dbcmd, "@PKRightID", DbType.String, IIf(SC_FunRightRow.LoadFromDataRow, SC_FunRightRow.RightID.OldValue, SC_FunRightRow.RightID.Value))

            Return db.ExecuteNonQuery(dbcmd)
        End Function

        Public Function Update(ByVal SC_FunRightRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim strDot As String = String.Empty

            strSQL.AppendLine("Update SC_FunRight Set")
            For i As Integer = 0 To SC_FunRightRow.FieldNames.Length - 1
                If Not SC_FunRightRow.IsIdentityField(SC_FunRightRow.FieldNames(i)) AndAlso SC_FunRightRow.IsUpdated(SC_FunRightRow.FieldNames(i)) AndAlso SC_FunRightRow.CreateUpdateSQL(SC_FunRightRow.FieldNames(i)) Then
                    strSQL.AppendLine(string.Format("{0}{1} = @{1}", strDot, SC_FunRightRow.FieldNames(i)))
                    strDot = ","
                End If
            Next 
            If strDot = String.Empty Then Throw New Exception("未異動資料欄位！")
            strSQL.AppendLine("Where SysID = @PKSysID")
            strSQL.AppendLine("And FunID = @PKFunID")
            strSQL.AppendLine("And RightID = @PKRightID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            If SC_FunRightRow.SysID.Updated Then db.AddInParameter(dbcmd, "@SysID", DbType.String, SC_FunRightRow.SysID.Value)
            If SC_FunRightRow.FunID.Updated Then db.AddInParameter(dbcmd, "@FunID", DbType.String, SC_FunRightRow.FunID.Value)
            If SC_FunRightRow.RightID.Updated Then db.AddInParameter(dbcmd, "@RightID", DbType.String, SC_FunRightRow.RightID.Value)
            If SC_FunRightRow.IsVisible.Updated Then db.AddInParameter(dbcmd, "@IsVisible", DbType.String, SC_FunRightRow.IsVisible.Value)
            If SC_FunRightRow.Caption.Updated Then db.AddInParameter(dbcmd, "@Caption", DbType.String, SC_FunRightRow.Caption.Value)
            If SC_FunRightRow.CaptionEng.Updated Then db.AddInParameter(dbcmd, "@CaptionEng", DbType.String, SC_FunRightRow.CaptionEng.Value)
            db.AddInParameter(dbcmd, "@PKSysID", DbType.String, IIf(SC_FunRightRow.LoadFromDataRow, SC_FunRightRow.SysID.OldValue, SC_FunRightRow.SysID.Value))
            db.AddInParameter(dbcmd, "@PKFunID", DbType.String, IIf(SC_FunRightRow.LoadFromDataRow, SC_FunRightRow.FunID.OldValue, SC_FunRightRow.FunID.Value))
            db.AddInParameter(dbcmd, "@PKRightID", DbType.String, IIf(SC_FunRightRow.LoadFromDataRow, SC_FunRightRow.RightID.OldValue, SC_FunRightRow.RightID.Value))

            Return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function Update(ByVal SC_FunRightRow As Row()) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0
            Dim strDot As String

            Using cn As DbConnection = db.CreateConnection()
                cn.Open()
                Dim tran As DbTransaction = cn.BeginTransaction()
                Dim inTrans As Boolean = True

                Try
                    For Each r As Row In SC_FunRightRow
                        strDot = String.Empty
                        If strSQL.Length > 0 Then strSQL.Remove(0, strSQL.Length)
                        strSQL.AppendLine("Update SC_FunRight Set")
                        For i As Integer = 0 To r.FieldNames.Length - 1
                            If Not r.IsIdentityField(r.FieldNames(i)) AndAlso r.IsUpdated(r.FieldNames(i)) AndAlso r.CreateUpdateSQL(r.FieldNames(i)) Then
                                strSQL.AppendLine(String.Format("{0}{1} = @{1}", strDot, r.FieldNames(i)))
                                strDot = ","
                            End If
                        Next
                        If strDot = String.Empty Then Continue For
                        strSQL.AppendLine("Where SysID = @PKSysID")
                        strSQL.AppendLine("And FunID = @PKFunID")
                        strSQL.AppendLine("And RightID = @PKRightID")

                        dbcmd = db.GetSqlStringCommand(strSQL.ToString())
                        If r.SysID.Updated Then db.AddInParameter(dbcmd, "@SysID", DbType.String, r.SysID.Value)
                        If r.FunID.Updated Then db.AddInParameter(dbcmd, "@FunID", DbType.String, r.FunID.Value)
                        If r.RightID.Updated Then db.AddInParameter(dbcmd, "@RightID", DbType.String, r.RightID.Value)
                        If r.IsVisible.Updated Then db.AddInParameter(dbcmd, "@IsVisible", DbType.String, r.IsVisible.Value)
                        If r.Caption.Updated Then db.AddInParameter(dbcmd, "@Caption", DbType.String, r.Caption.Value)
                        If r.CaptionEng.Updated Then db.AddInParameter(dbcmd, "@CaptionEng", DbType.String, r.CaptionEng.Value)
                        db.AddInParameter(dbcmd, "@PKSysID", DbType.String, IIf(r.LoadFromDataRow, r.SysID.OldValue, r.SysID.Value))
                        db.AddInParameter(dbcmd, "@PKFunID", DbType.String, IIf(r.LoadFromDataRow, r.FunID.OldValue, r.FunID.Value))
                        db.AddInParameter(dbcmd, "@PKRightID", DbType.String, IIf(r.LoadFromDataRow, r.RightID.OldValue, r.RightID.Value))

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

        Public Function Update(ByVal SC_FunRightRow As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0
            Dim strDot As String

            For Each r As Row In SC_FunRightRow
                strDot = String.Empty
                If strSQL.Length > 0 Then strSQL.Remove(0, strSQL.Length)
                strSQL.AppendLine("Update SC_FunRight Set")
                For i As Integer = 0 To r.FieldNames.Length - 1
                    If Not r.IsIdentityField(r.FieldNames(i)) AndAlso r.IsUpdated(r.FieldNames(i)) AndAlso r.CreateUpdateSQL(r.FieldNames(i))
                        strSQL.AppendLine(String.Format("{0}{1} = @{1}", strDot, r.FieldNames(i)))
                        strDot = ","
                    End If
                Next
                If strDot = String.Empty Then Continue For
                strSQL.AppendLine("Where SysID = @PKSysID")
                strSQL.AppendLine("And FunID = @PKFunID")
                strSQL.AppendLine("And RightID = @PKRightID")

                dbcmd = db.GetSqlStringCommand(strSQL.ToString())
                If r.SysID.Updated Then db.AddInParameter(dbcmd, "@SysID", DbType.String, r.SysID.Value)
                If r.FunID.Updated Then db.AddInParameter(dbcmd, "@FunID", DbType.String, r.FunID.Value)
                If r.RightID.Updated Then db.AddInParameter(dbcmd, "@RightID", DbType.String, r.RightID.Value)
                If r.IsVisible.Updated Then db.AddInParameter(dbcmd, "@IsVisible", DbType.String, r.IsVisible.Value)
                If r.Caption.Updated Then db.AddInParameter(dbcmd, "@Caption", DbType.String, r.Caption.Value)
                If r.CaptionEng.Updated Then db.AddInParameter(dbcmd, "@CaptionEng", DbType.String, r.CaptionEng.Value)
                db.AddInParameter(dbcmd, "@PKSysID", DbType.String, IIf(r.LoadFromDataRow, r.SysID.OldValue, r.SysID.Value))
                db.AddInParameter(dbcmd, "@PKFunID", DbType.String, IIf(r.LoadFromDataRow, r.FunID.OldValue, r.FunID.Value))
                db.AddInParameter(dbcmd, "@PKRightID", DbType.String, IIf(r.LoadFromDataRow, r.RightID.OldValue, r.RightID.Value))

                intRowsAffected += db.ExecuteNonQuery(dbcmd, tran)
            Next
            Return intRowsAffected
        End Function

        Public Function IsDataExists(ByVal SC_FunRightRow As Row) As Boolean
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intCount As Integer = 0

            strSQL.AppendLine("Select Count(*) Cnt From SC_FunRight")
            strSQL.AppendLine("Where SysID = @SysID")
            strSQL.AppendLine("And FunID = @FunID")
            strSQL.AppendLine("And RightID = @RightID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@SysID", DbType.String, SC_FunRightRow.SysID.Value)
            db.AddInParameter(dbcmd, "@FunID", DbType.String, SC_FunRightRow.FunID.Value)
            db.AddInParameter(dbcmd, "@RightID", DbType.String, SC_FunRightRow.RightID.Value)

            intCount = Convert.ToInt32(db.ExecuteScalar(dbcmd))
            Return IIf(intCount > 0, True, False)
        End Function

        Public Function IsDataExists(ByVal SC_FunRightRow As Row, ByVal tran As DbTransaction) As Boolean
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intCount As Integer = 0

            strSQL.AppendLine("Select Count(*) Cnt From SC_FunRight")
            strSQL.AppendLine("Where SysID = @SysID")
            strSQL.AppendLine("And FunID = @FunID")
            strSQL.AppendLine("And RightID = @RightID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@SysID", DbType.String, SC_FunRightRow.SysID.Value)
            db.AddInParameter(dbcmd, "@FunID", DbType.String, SC_FunRightRow.FunID.Value)
            db.AddInParameter(dbcmd, "@RightID", DbType.String, SC_FunRightRow.RightID.Value)

            intCount = Convert.ToInt32(db.ExecuteScalar(dbcmd, tran))
            Return IIf(intCount > 0, True, False)
        End Function

        Public Function QuerybyWhere(WhereCondition As String) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From SC_FunRight")
            If WhereCondition <> String.Empty Then strSQL.AppendLine(WhereCondition)

            Return db.ExecuteDataSet(CommandType.Text, strSQL.ToString())
        End Function

        Public Function Insert(ByVal SC_FunRightRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Insert into SC_FunRight")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    SysID, FunID, RightID, IsVisible, Caption, CaptionEng")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @SysID, @FunID, @RightID, @IsVisible, @Caption, @CaptionEng")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@SysID", DbType.String, SC_FunRightRow.SysID.Value)
            db.AddInParameter(dbcmd, "@FunID", DbType.String, SC_FunRightRow.FunID.Value)
            db.AddInParameter(dbcmd, "@RightID", DbType.String, SC_FunRightRow.RightID.Value)
            db.AddInParameter(dbcmd, "@IsVisible", DbType.String, SC_FunRightRow.IsVisible.Value)
            db.AddInParameter(dbcmd, "@Caption", DbType.String, SC_FunRightRow.Caption.Value)
            db.AddInParameter(dbcmd, "@CaptionEng", DbType.String, SC_FunRightRow.CaptionEng.Value)

            Return db.ExecuteNonQuery(dbcmd)
        End Function

        Public Function Insert(ByVal SC_FunRightRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Insert into SC_FunRight")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    SysID, FunID, RightID, IsVisible, Caption, CaptionEng")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @SysID, @FunID, @RightID, @IsVisible, @Caption, @CaptionEng")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@SysID", DbType.String, SC_FunRightRow.SysID.Value)
            db.AddInParameter(dbcmd, "@FunID", DbType.String, SC_FunRightRow.FunID.Value)
            db.AddInParameter(dbcmd, "@RightID", DbType.String, SC_FunRightRow.RightID.Value)
            db.AddInParameter(dbcmd, "@IsVisible", DbType.String, SC_FunRightRow.IsVisible.Value)
            db.AddInParameter(dbcmd, "@Caption", DbType.String, SC_FunRightRow.Caption.Value)
            db.AddInParameter(dbcmd, "@CaptionEng", DbType.String, SC_FunRightRow.CaptionEng.Value)

            Return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function Insert(ByVal SC_FunRightRow As Row()) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Insert into SC_FunRight")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    SysID, FunID, RightID, IsVisible, Caption, CaptionEng")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @SysID, @FunID, @RightID, @IsVisible, @Caption, @CaptionEng")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            Using cn As DbConnection = db.CreateConnection()
                cn.Open()
                Dim tran As DbTransaction = cn.BeginTransaction()
                Dim inTrans As Boolean = True

                Try
                    For Each r As Row In SC_FunRightRow
                        dbcmd.Parameters.Clear()
                        db.AddInParameter(dbcmd, "@SysID", DbType.String, r.SysID.Value)
                        db.AddInParameter(dbcmd, "@FunID", DbType.String, r.FunID.Value)
                        db.AddInParameter(dbcmd, "@RightID", DbType.String, r.RightID.Value)
                        db.AddInParameter(dbcmd, "@IsVisible", DbType.String, r.IsVisible.Value)
                        db.AddInParameter(dbcmd, "@Caption", DbType.String, r.Caption.Value)
                        db.AddInParameter(dbcmd, "@CaptionEng", DbType.String, r.CaptionEng.Value)

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

        Public Function Insert(ByVal SC_FunRightRow As Row(), ByVal tran As DbTransaction) As Boolean
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Insert into SC_FunRight")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    SysID, FunID, RightID, IsVisible, Caption, CaptionEng")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @SysID, @FunID, @RightID, @IsVisible, @Caption, @CaptionEng")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())

            For Each r As Row In SC_FunRightRow
                dbcmd.Parameters.Clear()
                db.AddInParameter(dbcmd, "@SysID", DbType.String, r.SysID.Value)
                db.AddInParameter(dbcmd, "@FunID", DbType.String, r.FunID.Value)
                db.AddInParameter(dbcmd, "@RightID", DbType.String, r.RightID.Value)
                db.AddInParameter(dbcmd, "@IsVisible", DbType.String, r.IsVisible.Value)
                db.AddInParameter(dbcmd, "@Caption", DbType.String, r.Caption.Value)
                db.AddInParameter(dbcmd, "@CaptionEng", DbType.String, r.CaptionEng.Value)

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

