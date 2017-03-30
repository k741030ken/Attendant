'****************************************************************
' Table:SC_User_Monthly
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

Namespace beSC_User_Monthly
    Public Class Table
        Private m_Rows As Rows = New Rows()
        Private m_Fields As String() = { "YM", "UserID", "UserName", "EngName", "WorkStatus", "BusinessFlag", "CompID", "CompName", "DeptID", "OrganID" _
                                    , "BusinessID", "WorkTypeID", "WorkType", "SecWorkTypeID", "SecWorkType", "RankID", "TitleName", "Password", "PasswordErrorCount", "ExpireDate", "EMail" _
                                    , "BanMark", "LastLoginDate", "HostName", "UpdateFlag", "CreateDate", "LastChgID", "LastChgDate" }
        Private m_Types As System.Type() = { GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String) _
                                    , GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(Integer), GetType(Date), GetType(String) _
                                    , GetType(String), GetType(Date), GetType(String), GetType(String), GetType(Date), GetType(String), GetType(Date) }
        Private m_PrimaryFields As String() = { "YM", "UserID" }

        Public ReadOnly Property Rows() As beSC_User_Monthly.Rows 
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
        Public Sub Transfer2Row(SC_User_MonthlyTable As DataTable)
            For Each dr As DataRow In SC_User_MonthlyTable.Rows
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

                dr(m_Rows(i).YM.FieldName) = m_Rows(i).YM.Value
                dr(m_Rows(i).UserID.FieldName) = m_Rows(i).UserID.Value
                dr(m_Rows(i).UserName.FieldName) = m_Rows(i).UserName.Value
                dr(m_Rows(i).EngName.FieldName) = m_Rows(i).EngName.Value
                dr(m_Rows(i).WorkStatus.FieldName) = m_Rows(i).WorkStatus.Value
                dr(m_Rows(i).BusinessFlag.FieldName) = m_Rows(i).BusinessFlag.Value
                dr(m_Rows(i).CompID.FieldName) = m_Rows(i).CompID.Value
                dr(m_Rows(i).CompName.FieldName) = m_Rows(i).CompName.Value
                dr(m_Rows(i).DeptID.FieldName) = m_Rows(i).DeptID.Value
                dr(m_Rows(i).OrganID.FieldName) = m_Rows(i).OrganID.Value
                dr(m_Rows(i).BusinessID.FieldName) = m_Rows(i).BusinessID.Value
                dr(m_Rows(i).WorkTypeID.FieldName) = m_Rows(i).WorkTypeID.Value
                dr(m_Rows(i).WorkType.FieldName) = m_Rows(i).WorkType.Value
                dr(m_Rows(i).SecWorkTypeID.FieldName) = m_Rows(i).SecWorkTypeID.Value
                dr(m_Rows(i).SecWorkType.FieldName) = m_Rows(i).SecWorkType.Value
                dr(m_Rows(i).RankID.FieldName) = m_Rows(i).RankID.Value
                dr(m_Rows(i).TitleName.FieldName) = m_Rows(i).TitleName.Value
                dr(m_Rows(i).Password.FieldName) = m_Rows(i).Password.Value
                dr(m_Rows(i).PasswordErrorCount.FieldName) = m_Rows(i).PasswordErrorCount.Value
                dr(m_Rows(i).ExpireDate.FieldName) = m_Rows(i).ExpireDate.Value
                dr(m_Rows(i).EMail.FieldName) = m_Rows(i).EMail.Value
                dr(m_Rows(i).BanMark.FieldName) = m_Rows(i).BanMark.Value
                dr(m_Rows(i).LastLoginDate.FieldName) = m_Rows(i).LastLoginDate.Value
                dr(m_Rows(i).HostName.FieldName) = m_Rows(i).HostName.Value
                dr(m_Rows(i).UpdateFlag.FieldName) = m_Rows(i).UpdateFlag.Value
                dr(m_Rows(i).CreateDate.FieldName) = m_Rows(i).CreateDate.Value
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

        Public Sub Add(SC_User_MonthlyRow As Row)
            m_Rows.Add(SC_User_MonthlyRow)
        End Sub

        Public Sub Remove(SC_User_MonthlyRow As Row)
            If m_Rows.IndexOf(SC_User_MonthlyRow) >= 0 Then
                m_Rows.Remove(SC_User_MonthlyRow)
            End If
        End Sub

        Public Sub Dispose()
            m_Rows.Clear()
        End Sub

    End Class

    Public Class Row

        Private FI_YM As Field(Of String) = new Field(Of String)("YM", true)
        Private FI_UserID As Field(Of String) = new Field(Of String)("UserID", true)
        Private FI_UserName As Field(Of String) = new Field(Of String)("UserName", true)
        Private FI_EngName As Field(Of String) = new Field(Of String)("EngName", true)
        Private FI_WorkStatus As Field(Of String) = new Field(Of String)("WorkStatus", true)
        Private FI_BusinessFlag As Field(Of String) = new Field(Of String)("BusinessFlag", true)
        Private FI_CompID As Field(Of String) = new Field(Of String)("CompID", true)
        Private FI_CompName As Field(Of String) = new Field(Of String)("CompName", true)
        Private FI_DeptID As Field(Of String) = new Field(Of String)("DeptID", true)
        Private FI_OrganID As Field(Of String) = new Field(Of String)("OrganID", true)
        Private FI_BusinessID As Field(Of String) = new Field(Of String)("BusinessID", true)
        Private FI_WorkTypeID As Field(Of String) = new Field(Of String)("WorkTypeID", true)
        Private FI_WorkType As Field(Of String) = new Field(Of String)("WorkType", true)
        Private FI_SecWorkTypeID As Field(Of String) = new Field(Of String)("SecWorkTypeID", true)
        Private FI_SecWorkType As Field(Of String) = new Field(Of String)("SecWorkType", true)
        Private FI_RankID As Field(Of String) = new Field(Of String)("RankID", true)
        Private FI_TitleName As Field(Of String) = new Field(Of String)("TitleName", true)
        Private FI_Password As Field(Of String) = new Field(Of String)("Password", true)
        Private FI_PasswordErrorCount As Field(Of Integer) = new Field(Of Integer)("PasswordErrorCount", true)
        Private FI_ExpireDate As Field(Of Date) = new Field(Of Date)("ExpireDate", true)
        Private FI_EMail As Field(Of String) = new Field(Of String)("EMail", true)
        Private FI_BanMark As Field(Of String) = new Field(Of String)("BanMark", true)
        Private FI_LastLoginDate As Field(Of Date) = new Field(Of Date)("LastLoginDate", true)
        Private FI_HostName As Field(Of String) = new Field(Of String)("HostName", true)
        Private FI_UpdateFlag As Field(Of String) = new Field(Of String)("UpdateFlag", true)
        Private FI_CreateDate As Field(Of Date) = new Field(Of Date)("CreateDate", true)
        Private FI_LastChgID As Field(Of String) = new Field(Of String)("LastChgID", true)
        Private FI_LastChgDate As Field(Of Date) = new Field(Of Date)("LastChgDate", true)
        Private m_FieldNames As String() = { "YM", "UserID", "UserName", "EngName", "WorkStatus", "BusinessFlag", "CompID", "CompName", "DeptID", "OrganID" _
                                    , "BusinessID", "WorkTypeID", "WorkType", "SecWorkTypeID", "SecWorkType", "RankID", "TitleName", "Password", "PasswordErrorCount", "ExpireDate", "EMail" _
                                    , "BanMark", "LastLoginDate", "HostName", "UpdateFlag", "CreateDate", "LastChgID", "LastChgDate" }
        Private m_PrimaryFields As String() = { "YM", "UserID" }
        Private m_IdentityFields As String() = {  }
        Private m_LoadFromDataRow As Boolean = False

        Private Function GetFieldValue(ByVal fieldName As String) As Object
            Select Case fieldName
                Case "YM"
                    Return FI_YM.Value
                Case "UserID"
                    Return FI_UserID.Value
                Case "UserName"
                    Return FI_UserName.Value
                Case "EngName"
                    Return FI_EngName.Value
                Case "WorkStatus"
                    Return FI_WorkStatus.Value
                Case "BusinessFlag"
                    Return FI_BusinessFlag.Value
                Case "CompID"
                    Return FI_CompID.Value
                Case "CompName"
                    Return FI_CompName.Value
                Case "DeptID"
                    Return FI_DeptID.Value
                Case "OrganID"
                    Return FI_OrganID.Value
                Case "BusinessID"
                    Return FI_BusinessID.Value
                Case "WorkTypeID"
                    Return FI_WorkTypeID.Value
                Case "WorkType"
                    Return FI_WorkType.Value
                Case "SecWorkTypeID"
                    Return FI_SecWorkTypeID.Value
                Case "SecWorkType"
                    Return FI_SecWorkType.Value
                Case "RankID"
                    Return FI_RankID.Value
                Case "TitleName"
                    Return FI_TitleName.Value
                Case "Password"
                    Return FI_Password.Value
                Case "PasswordErrorCount"
                    Return FI_PasswordErrorCount.Value
                Case "ExpireDate"
                    Return FI_ExpireDate.Value
                Case "EMail"
                    Return FI_EMail.Value
                Case "BanMark"
                    Return FI_BanMark.Value
                Case "LastLoginDate"
                    Return FI_LastLoginDate.Value
                Case "HostName"
                    Return FI_HostName.Value
                Case "UpdateFlag"
                    Return FI_UpdateFlag.Value
                Case "CreateDate"
                    Return FI_CreateDate.Value
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
                Case "YM"
                    FI_YM.SetValue(value)
                Case "UserID"
                    FI_UserID.SetValue(value)
                Case "UserName"
                    FI_UserName.SetValue(value)
                Case "EngName"
                    FI_EngName.SetValue(value)
                Case "WorkStatus"
                    FI_WorkStatus.SetValue(value)
                Case "BusinessFlag"
                    FI_BusinessFlag.SetValue(value)
                Case "CompID"
                    FI_CompID.SetValue(value)
                Case "CompName"
                    FI_CompName.SetValue(value)
                Case "DeptID"
                    FI_DeptID.SetValue(value)
                Case "OrganID"
                    FI_OrganID.SetValue(value)
                Case "BusinessID"
                    FI_BusinessID.SetValue(value)
                Case "WorkTypeID"
                    FI_WorkTypeID.SetValue(value)
                Case "WorkType"
                    FI_WorkType.SetValue(value)
                Case "SecWorkTypeID"
                    FI_SecWorkTypeID.SetValue(value)
                Case "SecWorkType"
                    FI_SecWorkType.SetValue(value)
                Case "RankID"
                    FI_RankID.SetValue(value)
                Case "TitleName"
                    FI_TitleName.SetValue(value)
                Case "Password"
                    FI_Password.SetValue(value)
                Case "PasswordErrorCount"
                    FI_PasswordErrorCount.SetValue(value)
                Case "ExpireDate"
                    FI_ExpireDate.SetValue(value)
                Case "EMail"
                    FI_EMail.SetValue(value)
                Case "BanMark"
                    FI_BanMark.SetValue(value)
                Case "LastLoginDate"
                    FI_LastLoginDate.SetValue(value)
                Case "HostName"
                    FI_HostName.SetValue(value)
                Case "UpdateFlag"
                    FI_UpdateFlag.SetValue(value)
                Case "CreateDate"
                    FI_CreateDate.SetValue(value)
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
                Case "YM"
                    return FI_YM.Updated
                Case "UserID"
                    return FI_UserID.Updated
                Case "UserName"
                    return FI_UserName.Updated
                Case "EngName"
                    return FI_EngName.Updated
                Case "WorkStatus"
                    return FI_WorkStatus.Updated
                Case "BusinessFlag"
                    return FI_BusinessFlag.Updated
                Case "CompID"
                    return FI_CompID.Updated
                Case "CompName"
                    return FI_CompName.Updated
                Case "DeptID"
                    return FI_DeptID.Updated
                Case "OrganID"
                    return FI_OrganID.Updated
                Case "BusinessID"
                    return FI_BusinessID.Updated
                Case "WorkTypeID"
                    return FI_WorkTypeID.Updated
                Case "WorkType"
                    return FI_WorkType.Updated
                Case "SecWorkTypeID"
                    return FI_SecWorkTypeID.Updated
                Case "SecWorkType"
                    return FI_SecWorkType.Updated
                Case "RankID"
                    return FI_RankID.Updated
                Case "TitleName"
                    return FI_TitleName.Updated
                Case "Password"
                    return FI_Password.Updated
                Case "PasswordErrorCount"
                    return FI_PasswordErrorCount.Updated
                Case "ExpireDate"
                    return FI_ExpireDate.Updated
                Case "EMail"
                    return FI_EMail.Updated
                Case "BanMark"
                    return FI_BanMark.Updated
                Case "LastLoginDate"
                    return FI_LastLoginDate.Updated
                Case "HostName"
                    return FI_HostName.Updated
                Case "UpdateFlag"
                    return FI_UpdateFlag.Updated
                Case "CreateDate"
                    return FI_CreateDate.Updated
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
                Case "YM"
                    return FI_YM.CreateUpdateSQL
                Case "UserID"
                    return FI_UserID.CreateUpdateSQL
                Case "UserName"
                    return FI_UserName.CreateUpdateSQL
                Case "EngName"
                    return FI_EngName.CreateUpdateSQL
                Case "WorkStatus"
                    return FI_WorkStatus.CreateUpdateSQL
                Case "BusinessFlag"
                    return FI_BusinessFlag.CreateUpdateSQL
                Case "CompID"
                    return FI_CompID.CreateUpdateSQL
                Case "CompName"
                    return FI_CompName.CreateUpdateSQL
                Case "DeptID"
                    return FI_DeptID.CreateUpdateSQL
                Case "OrganID"
                    return FI_OrganID.CreateUpdateSQL
                Case "BusinessID"
                    return FI_BusinessID.CreateUpdateSQL
                Case "WorkTypeID"
                    return FI_WorkTypeID.CreateUpdateSQL
                Case "WorkType"
                    return FI_WorkType.CreateUpdateSQL
                Case "SecWorkTypeID"
                    return FI_SecWorkTypeID.CreateUpdateSQL
                Case "SecWorkType"
                    return FI_SecWorkType.CreateUpdateSQL
                Case "RankID"
                    return FI_RankID.CreateUpdateSQL
                Case "TitleName"
                    return FI_TitleName.CreateUpdateSQL
                Case "Password"
                    return FI_Password.CreateUpdateSQL
                Case "PasswordErrorCount"
                    return FI_PasswordErrorCount.CreateUpdateSQL
                Case "ExpireDate"
                    return FI_ExpireDate.CreateUpdateSQL
                Case "EMail"
                    return FI_EMail.CreateUpdateSQL
                Case "BanMark"
                    return FI_BanMark.CreateUpdateSQL
                Case "LastLoginDate"
                    return FI_LastLoginDate.CreateUpdateSQL
                Case "HostName"
                    return FI_HostName.CreateUpdateSQL
                Case "UpdateFlag"
                    return FI_UpdateFlag.CreateUpdateSQL
                Case "CreateDate"
                    return FI_CreateDate.CreateUpdateSQL
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
            FI_YM.SetInitValue("")
            FI_UserID.SetInitValue("")
            FI_UserName.SetInitValue("")
            FI_EngName.SetInitValue("")
            FI_WorkStatus.SetInitValue("")
            FI_BusinessFlag.SetInitValue("")
            FI_CompID.SetInitValue("")
            FI_CompName.SetInitValue("")
            FI_DeptID.SetInitValue("")
            FI_OrganID.SetInitValue("")
            FI_BusinessID.SetInitValue("")
            FI_WorkTypeID.SetInitValue("")
            FI_WorkType.SetInitValue("")
            FI_SecWorkTypeID.SetInitValue("")
            FI_SecWorkType.SetInitValue("")
            FI_RankID.SetInitValue("")
            FI_TitleName.SetInitValue("")
            FI_Password.SetInitValue("")
            FI_PasswordErrorCount.SetInitValue(0)
            FI_ExpireDate.SetInitValue(Convert.ToDateTime("1900/01/01"))
            FI_EMail.SetInitValue("")
            FI_BanMark.SetInitValue("")
            FI_LastLoginDate.SetInitValue(Convert.ToDateTime("1900/01/01"))
            FI_HostName.SetInitValue("")
            FI_UpdateFlag.SetInitValue("")
            FI_CreateDate.SetInitValue(Convert.ToDateTime("1900/01/01"))
            FI_LastChgID.SetInitValue("")
            FI_LastChgDate.SetInitValue(Convert.ToDateTime("1900/01/01"))
        End Sub

        Public Sub New(ByVal dr As System.Data.DataRow)
            FI_YM.SetInitValue(dr("YM"))
            FI_UserID.SetInitValue(dr("UserID"))
            FI_UserName.SetInitValue(dr("UserName"))
            FI_EngName.SetInitValue(dr("EngName"))
            FI_WorkStatus.SetInitValue(dr("WorkStatus"))
            FI_BusinessFlag.SetInitValue(dr("BusinessFlag"))
            FI_CompID.SetInitValue(dr("CompID"))
            FI_CompName.SetInitValue(dr("CompName"))
            FI_DeptID.SetInitValue(dr("DeptID"))
            FI_OrganID.SetInitValue(dr("OrganID"))
            FI_BusinessID.SetInitValue(dr("BusinessID"))
            FI_WorkTypeID.SetInitValue(dr("WorkTypeID"))
            FI_WorkType.SetInitValue(dr("WorkType"))
            FI_SecWorkTypeID.SetInitValue(dr("SecWorkTypeID"))
            FI_SecWorkType.SetInitValue(dr("SecWorkType"))
            FI_RankID.SetInitValue(dr("RankID"))
            FI_TitleName.SetInitValue(dr("TitleName"))
            FI_Password.SetInitValue(dr("Password"))
            FI_PasswordErrorCount.SetInitValue(dr("PasswordErrorCount"))
            FI_ExpireDate.SetInitValue(dr("ExpireDate"))
            FI_EMail.SetInitValue(dr("EMail"))
            FI_BanMark.SetInitValue(dr("BanMark"))
            FI_LastLoginDate.SetInitValue(dr("LastLoginDate"))
            FI_HostName.SetInitValue(dr("HostName"))
            FI_UpdateFlag.SetInitValue(dr("UpdateFlag"))
            FI_CreateDate.SetInitValue(dr("CreateDate"))
            FI_LastChgID.SetInitValue(dr("LastChgID"))
            FI_LastChgDate.SetInitValue(dr("LastChgDate"))

            m_LoadFromDataRow = True
        End Sub

        Private Sub ClearUpdated()
            FI_YM.Updated = False
            FI_UserID.Updated = False
            FI_UserName.Updated = False
            FI_EngName.Updated = False
            FI_WorkStatus.Updated = False
            FI_BusinessFlag.Updated = False
            FI_CompID.Updated = False
            FI_CompName.Updated = False
            FI_DeptID.Updated = False
            FI_OrganID.Updated = False
            FI_BusinessID.Updated = False
            FI_WorkTypeID.Updated = False
            FI_WorkType.Updated = False
            FI_SecWorkTypeID.Updated = False
            FI_SecWorkType.Updated = False
            FI_RankID.Updated = False
            FI_TitleName.Updated = False
            FI_Password.Updated = False
            FI_PasswordErrorCount.Updated = False
            FI_ExpireDate.Updated = False
            FI_EMail.Updated = False
            FI_BanMark.Updated = False
            FI_LastLoginDate.Updated = False
            FI_HostName.Updated = False
            FI_UpdateFlag.Updated = False
            FI_CreateDate.Updated = False
            FI_LastChgID.Updated = False
            FI_LastChgDate.Updated = False
        End Sub

        Public ReadOnly Property YM As Field(Of String) 
            Get
                Return FI_YM
            End Get
        End Property

        Public ReadOnly Property UserID As Field(Of String) 
            Get
                Return FI_UserID
            End Get
        End Property

        Public ReadOnly Property UserName As Field(Of String) 
            Get
                Return FI_UserName
            End Get
        End Property

        Public ReadOnly Property EngName As Field(Of String) 
            Get
                Return FI_EngName
            End Get
        End Property

        Public ReadOnly Property WorkStatus As Field(Of String) 
            Get
                Return FI_WorkStatus
            End Get
        End Property

        Public ReadOnly Property BusinessFlag As Field(Of String) 
            Get
                Return FI_BusinessFlag
            End Get
        End Property

        Public ReadOnly Property CompID As Field(Of String) 
            Get
                Return FI_CompID
            End Get
        End Property

        Public ReadOnly Property CompName As Field(Of String) 
            Get
                Return FI_CompName
            End Get
        End Property

        Public ReadOnly Property DeptID As Field(Of String) 
            Get
                Return FI_DeptID
            End Get
        End Property

        Public ReadOnly Property OrganID As Field(Of String) 
            Get
                Return FI_OrganID
            End Get
        End Property

        Public ReadOnly Property BusinessID As Field(Of String) 
            Get
                Return FI_BusinessID
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

        Public ReadOnly Property SecWorkTypeID As Field(Of String) 
            Get
                Return FI_SecWorkTypeID
            End Get
        End Property

        Public ReadOnly Property SecWorkType As Field(Of String) 
            Get
                Return FI_SecWorkType
            End Get
        End Property

        Public ReadOnly Property RankID As Field(Of String) 
            Get
                Return FI_RankID
            End Get
        End Property

        Public ReadOnly Property TitleName As Field(Of String) 
            Get
                Return FI_TitleName
            End Get
        End Property

        Public ReadOnly Property Password As Field(Of String) 
            Get
                Return FI_Password
            End Get
        End Property

        Public ReadOnly Property PasswordErrorCount As Field(Of Integer) 
            Get
                Return FI_PasswordErrorCount
            End Get
        End Property

        Public ReadOnly Property ExpireDate As Field(Of Date) 
            Get
                Return FI_ExpireDate
            End Get
        End Property

        Public ReadOnly Property EMail As Field(Of String) 
            Get
                Return FI_EMail
            End Get
        End Property

        Public ReadOnly Property BanMark As Field(Of String) 
            Get
                Return FI_BanMark
            End Get
        End Property

        Public ReadOnly Property LastLoginDate As Field(Of Date) 
            Get
                Return FI_LastLoginDate
            End Get
        End Property

        Public ReadOnly Property HostName As Field(Of String) 
            Get
                Return FI_HostName
            End Get
        End Property

        Public ReadOnly Property UpdateFlag As Field(Of String) 
            Get
                Return FI_UpdateFlag
            End Get
        End Property

        Public ReadOnly Property CreateDate As Field(Of Date) 
            Get
                Return FI_CreateDate
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
        Public Function DeleteRowByPrimaryKey(ByVal SC_User_MonthlyRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Delete From SC_User_Monthly")
            strSQL.AppendLine("Where YM = @YM")
            strSQL.AppendLine("And UserID = @UserID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@YM", DbType.String, SC_User_MonthlyRow.YM.Value)
            db.AddInParameter(dbcmd, "@UserID", DbType.String, SC_User_MonthlyRow.UserID.Value)

            return db.ExecuteNonQuery(dbcmd)
        End Function

        public Function DeleteRowByPrimaryKey(ByVal SC_User_MonthlyRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Delete From SC_User_Monthly")
            strSQL.AppendLine("Where YM = @YM")
            strSQL.AppendLine("And UserID = @UserID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@YM", DbType.String, SC_User_MonthlyRow.YM.Value)
            db.AddInParameter(dbcmd, "@UserID", DbType.String, SC_User_MonthlyRow.UserID.Value)

            return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function DeleteRowByPrimaryKey(ByVal SC_User_MonthlyRow As Row()) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Delete From SC_User_Monthly")
            strSQL.AppendLine("Where YM = @YM")
            strSQL.AppendLine("And UserID = @UserID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            using cn As DbConnection = db.CreateConnection()
                cn.Open()
                Dim tran As DbTransaction = cn.BeginTransaction()
                Dim inTrans As Boolean = True

                Try
                    For Each r As Row In SC_User_MonthlyRow
                        dbcmd.Parameters.Clear()
                        db.AddInParameter(dbcmd, "@YM", DbType.String, r.YM.Value)
                        db.AddInParameter(dbcmd, "@UserID", DbType.String, r.UserID.Value)

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

        Public Function DeleteRowByPrimaryKey(ByVal SC_User_MonthlyRow As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Delete From SC_User_Monthly")
            strSQL.AppendLine("Where YM = @YM")
            strSQL.AppendLine("And UserID = @UserID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())

            For Each r As Row In SC_User_MonthlyRow
                dbcmd.Parameters.Clear()
                db.AddInParameter(dbcmd, "@YM", DbType.String, r.YM.Value)
                db.AddInParameter(dbcmd, "@UserID", DbType.String, r.UserID.Value)

                intRowsAffected += db.ExecuteNonQuery(dbcmd, tran)
            Next
            Return intRowsAffected
        End Function

        Public Function QueryByKey(ByVal SC_User_MonthlyRow As Row) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From SC_User_Monthly")
            strSQL.AppendLine("Where YM = @YM")
            strSQL.AppendLine("And UserID = @UserID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@YM", DbType.String, SC_User_MonthlyRow.YM.Value)
            db.AddInParameter(dbcmd, "@UserID", DbType.String, SC_User_MonthlyRow.UserID.Value)

            Return db.ExecuteDataSet(dbcmd)
        End Function

        Public Function QueryByKey(SC_User_MonthlyRow As Row, tran As DbTransaction) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From SC_User_Monthly")
            strSQL.AppendLine("Where YM = @YM")
            strSQL.AppendLine("And UserID = @UserID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@YM", DbType.String, SC_User_MonthlyRow.YM.Value)
            db.AddInParameter(dbcmd, "@UserID", DbType.String, SC_User_MonthlyRow.UserID.Value)

            Return db.ExecuteDataSet(dbcmd, tran)
        End Function

        Public Function Update(ByVal SC_User_MonthlyRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim strDot As String = String.Empty

            strSQL.AppendLine("Update SC_User_Monthly Set")
            For i As Integer = 0 To SC_User_MonthlyRow.FieldNames.Length - 1
                If Not SC_User_MonthlyRow.IsIdentityField(SC_User_MonthlyRow.FieldNames(i)) AndAlso SC_User_MonthlyRow.IsUpdated(SC_User_MonthlyRow.FieldNames(i)) AndAlso SC_User_MonthlyRow.CreateUpdateSQL(SC_User_MonthlyRow.FieldNames(i)) Then
                    strSQL.AppendLine(string.Format("{0}{1} = @{1}", strDot, SC_User_MonthlyRow.FieldNames(i)))
                    strDot = ","
                End If
            Next
            If strDot = String.Empty Then Throw New Exception("未異動資料欄位！")
            strSQL.AppendLine("Where YM = @PKYM")
            strSQL.AppendLine("And UserID = @PKUserID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            If SC_User_MonthlyRow.YM.Updated Then db.AddInParameter(dbcmd, "@YM", DbType.String, SC_User_MonthlyRow.YM.Value)
            If SC_User_MonthlyRow.UserID.Updated Then db.AddInParameter(dbcmd, "@UserID", DbType.String, SC_User_MonthlyRow.UserID.Value)
            If SC_User_MonthlyRow.UserName.Updated Then db.AddInParameter(dbcmd, "@UserName", DbType.String, SC_User_MonthlyRow.UserName.Value)
            If SC_User_MonthlyRow.EngName.Updated Then db.AddInParameter(dbcmd, "@EngName", DbType.String, SC_User_MonthlyRow.EngName.Value)
            If SC_User_MonthlyRow.WorkStatus.Updated Then db.AddInParameter(dbcmd, "@WorkStatus", DbType.String, SC_User_MonthlyRow.WorkStatus.Value)
            If SC_User_MonthlyRow.BusinessFlag.Updated Then db.AddInParameter(dbcmd, "@BusinessFlag", DbType.String, SC_User_MonthlyRow.BusinessFlag.Value)
            If SC_User_MonthlyRow.CompID.Updated Then db.AddInParameter(dbcmd, "@CompID", DbType.String, SC_User_MonthlyRow.CompID.Value)
            If SC_User_MonthlyRow.CompName.Updated Then db.AddInParameter(dbcmd, "@CompName", DbType.String, SC_User_MonthlyRow.CompName.Value)
            If SC_User_MonthlyRow.DeptID.Updated Then db.AddInParameter(dbcmd, "@DeptID", DbType.String, SC_User_MonthlyRow.DeptID.Value)
            If SC_User_MonthlyRow.OrganID.Updated Then db.AddInParameter(dbcmd, "@OrganID", DbType.String, SC_User_MonthlyRow.OrganID.Value)
            If SC_User_MonthlyRow.BusinessID.Updated Then db.AddInParameter(dbcmd, "@BusinessID", DbType.String, SC_User_MonthlyRow.BusinessID.Value)
            If SC_User_MonthlyRow.WorkTypeID.Updated Then db.AddInParameter(dbcmd, "@WorkTypeID", DbType.String, SC_User_MonthlyRow.WorkTypeID.Value)
            If SC_User_MonthlyRow.WorkType.Updated Then db.AddInParameter(dbcmd, "@WorkType", DbType.String, SC_User_MonthlyRow.WorkType.Value)
            If SC_User_MonthlyRow.SecWorkTypeID.Updated Then db.AddInParameter(dbcmd, "@SecWorkTypeID", DbType.String, SC_User_MonthlyRow.SecWorkTypeID.Value)
            If SC_User_MonthlyRow.SecWorkType.Updated Then db.AddInParameter(dbcmd, "@SecWorkType", DbType.String, SC_User_MonthlyRow.SecWorkType.Value)
            If SC_User_MonthlyRow.RankID.Updated Then db.AddInParameter(dbcmd, "@RankID", DbType.String, SC_User_MonthlyRow.RankID.Value)
            If SC_User_MonthlyRow.TitleName.Updated Then db.AddInParameter(dbcmd, "@TitleName", DbType.String, SC_User_MonthlyRow.TitleName.Value)
            If SC_User_MonthlyRow.Password.Updated Then db.AddInParameter(dbcmd, "@Password", DbType.String, SC_User_MonthlyRow.Password.Value)
            If SC_User_MonthlyRow.PasswordErrorCount.Updated Then db.AddInParameter(dbcmd, "@PasswordErrorCount", DbType.Int32, SC_User_MonthlyRow.PasswordErrorCount.Value)
            If SC_User_MonthlyRow.ExpireDate.Updated Then db.AddInParameter(dbcmd, "@ExpireDate", DbType.Date, IIf(IsDateTimeNull(SC_User_MonthlyRow.ExpireDate.Value), Convert.ToDateTime("1900/1/1"), SC_User_MonthlyRow.ExpireDate.Value))
            If SC_User_MonthlyRow.EMail.Updated Then db.AddInParameter(dbcmd, "@EMail", DbType.String, SC_User_MonthlyRow.EMail.Value)
            If SC_User_MonthlyRow.BanMark.Updated Then db.AddInParameter(dbcmd, "@BanMark", DbType.String, SC_User_MonthlyRow.BanMark.Value)
            If SC_User_MonthlyRow.LastLoginDate.Updated Then db.AddInParameter(dbcmd, "@LastLoginDate", DbType.Date, IIf(IsDateTimeNull(SC_User_MonthlyRow.LastLoginDate.Value), Convert.ToDateTime("1900/1/1"), SC_User_MonthlyRow.LastLoginDate.Value))
            If SC_User_MonthlyRow.HostName.Updated Then db.AddInParameter(dbcmd, "@HostName", DbType.String, SC_User_MonthlyRow.HostName.Value)
            If SC_User_MonthlyRow.UpdateFlag.Updated Then db.AddInParameter(dbcmd, "@UpdateFlag", DbType.String, SC_User_MonthlyRow.UpdateFlag.Value)
            If SC_User_MonthlyRow.CreateDate.Updated Then db.AddInParameter(dbcmd, "@CreateDate", DbType.Date, IIf(IsDateTimeNull(SC_User_MonthlyRow.CreateDate.Value), Convert.ToDateTime("1900/1/1"), SC_User_MonthlyRow.CreateDate.Value))
            If SC_User_MonthlyRow.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, SC_User_MonthlyRow.LastChgID.Value)
            If SC_User_MonthlyRow.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(SC_User_MonthlyRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), SC_User_MonthlyRow.LastChgDate.Value))
            db.AddInParameter(dbcmd, "@PKYM", DbType.String, IIf(SC_User_MonthlyRow.LoadFromDataRow, SC_User_MonthlyRow.YM.OldValue, SC_User_MonthlyRow.YM.Value))
            db.AddInParameter(dbcmd, "@PKUserID", DbType.String, IIf(SC_User_MonthlyRow.LoadFromDataRow, SC_User_MonthlyRow.UserID.OldValue, SC_User_MonthlyRow.UserID.Value))

            Return db.ExecuteNonQuery(dbcmd)
        End Function

        Public Function Update(ByVal SC_User_MonthlyRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim strDot As String = String.Empty

            strSQL.AppendLine("Update SC_User_Monthly Set")
            For i As Integer = 0 To SC_User_MonthlyRow.FieldNames.Length - 1
                If Not SC_User_MonthlyRow.IsIdentityField(SC_User_MonthlyRow.FieldNames(i)) AndAlso SC_User_MonthlyRow.IsUpdated(SC_User_MonthlyRow.FieldNames(i)) AndAlso SC_User_MonthlyRow.CreateUpdateSQL(SC_User_MonthlyRow.FieldNames(i)) Then
                    strSQL.AppendLine(string.Format("{0}{1} = @{1}", strDot, SC_User_MonthlyRow.FieldNames(i)))
                    strDot = ","
                End If
            Next 
            If strDot = String.Empty Then Throw New Exception("未異動資料欄位！")
            strSQL.AppendLine("Where YM = @PKYM")
            strSQL.AppendLine("And UserID = @PKUserID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            If SC_User_MonthlyRow.YM.Updated Then db.AddInParameter(dbcmd, "@YM", DbType.String, SC_User_MonthlyRow.YM.Value)
            If SC_User_MonthlyRow.UserID.Updated Then db.AddInParameter(dbcmd, "@UserID", DbType.String, SC_User_MonthlyRow.UserID.Value)
            If SC_User_MonthlyRow.UserName.Updated Then db.AddInParameter(dbcmd, "@UserName", DbType.String, SC_User_MonthlyRow.UserName.Value)
            If SC_User_MonthlyRow.EngName.Updated Then db.AddInParameter(dbcmd, "@EngName", DbType.String, SC_User_MonthlyRow.EngName.Value)
            If SC_User_MonthlyRow.WorkStatus.Updated Then db.AddInParameter(dbcmd, "@WorkStatus", DbType.String, SC_User_MonthlyRow.WorkStatus.Value)
            If SC_User_MonthlyRow.BusinessFlag.Updated Then db.AddInParameter(dbcmd, "@BusinessFlag", DbType.String, SC_User_MonthlyRow.BusinessFlag.Value)
            If SC_User_MonthlyRow.CompID.Updated Then db.AddInParameter(dbcmd, "@CompID", DbType.String, SC_User_MonthlyRow.CompID.Value)
            If SC_User_MonthlyRow.CompName.Updated Then db.AddInParameter(dbcmd, "@CompName", DbType.String, SC_User_MonthlyRow.CompName.Value)
            If SC_User_MonthlyRow.DeptID.Updated Then db.AddInParameter(dbcmd, "@DeptID", DbType.String, SC_User_MonthlyRow.DeptID.Value)
            If SC_User_MonthlyRow.OrganID.Updated Then db.AddInParameter(dbcmd, "@OrganID", DbType.String, SC_User_MonthlyRow.OrganID.Value)
            If SC_User_MonthlyRow.BusinessID.Updated Then db.AddInParameter(dbcmd, "@BusinessID", DbType.String, SC_User_MonthlyRow.BusinessID.Value)
            If SC_User_MonthlyRow.WorkTypeID.Updated Then db.AddInParameter(dbcmd, "@WorkTypeID", DbType.String, SC_User_MonthlyRow.WorkTypeID.Value)
            If SC_User_MonthlyRow.WorkType.Updated Then db.AddInParameter(dbcmd, "@WorkType", DbType.String, SC_User_MonthlyRow.WorkType.Value)
            If SC_User_MonthlyRow.SecWorkTypeID.Updated Then db.AddInParameter(dbcmd, "@SecWorkTypeID", DbType.String, SC_User_MonthlyRow.SecWorkTypeID.Value)
            If SC_User_MonthlyRow.SecWorkType.Updated Then db.AddInParameter(dbcmd, "@SecWorkType", DbType.String, SC_User_MonthlyRow.SecWorkType.Value)
            If SC_User_MonthlyRow.RankID.Updated Then db.AddInParameter(dbcmd, "@RankID", DbType.String, SC_User_MonthlyRow.RankID.Value)
            If SC_User_MonthlyRow.TitleName.Updated Then db.AddInParameter(dbcmd, "@TitleName", DbType.String, SC_User_MonthlyRow.TitleName.Value)
            If SC_User_MonthlyRow.Password.Updated Then db.AddInParameter(dbcmd, "@Password", DbType.String, SC_User_MonthlyRow.Password.Value)
            If SC_User_MonthlyRow.PasswordErrorCount.Updated Then db.AddInParameter(dbcmd, "@PasswordErrorCount", DbType.Int32, SC_User_MonthlyRow.PasswordErrorCount.Value)
            If SC_User_MonthlyRow.ExpireDate.Updated Then db.AddInParameter(dbcmd, "@ExpireDate", DbType.Date, IIf(IsDateTimeNull(SC_User_MonthlyRow.ExpireDate.Value), Convert.ToDateTime("1900/1/1"), SC_User_MonthlyRow.ExpireDate.Value))
            If SC_User_MonthlyRow.EMail.Updated Then db.AddInParameter(dbcmd, "@EMail", DbType.String, SC_User_MonthlyRow.EMail.Value)
            If SC_User_MonthlyRow.BanMark.Updated Then db.AddInParameter(dbcmd, "@BanMark", DbType.String, SC_User_MonthlyRow.BanMark.Value)
            If SC_User_MonthlyRow.LastLoginDate.Updated Then db.AddInParameter(dbcmd, "@LastLoginDate", DbType.Date, IIf(IsDateTimeNull(SC_User_MonthlyRow.LastLoginDate.Value), Convert.ToDateTime("1900/1/1"), SC_User_MonthlyRow.LastLoginDate.Value))
            If SC_User_MonthlyRow.HostName.Updated Then db.AddInParameter(dbcmd, "@HostName", DbType.String, SC_User_MonthlyRow.HostName.Value)
            If SC_User_MonthlyRow.UpdateFlag.Updated Then db.AddInParameter(dbcmd, "@UpdateFlag", DbType.String, SC_User_MonthlyRow.UpdateFlag.Value)
            If SC_User_MonthlyRow.CreateDate.Updated Then db.AddInParameter(dbcmd, "@CreateDate", DbType.Date, IIf(IsDateTimeNull(SC_User_MonthlyRow.CreateDate.Value), Convert.ToDateTime("1900/1/1"), SC_User_MonthlyRow.CreateDate.Value))
            If SC_User_MonthlyRow.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, SC_User_MonthlyRow.LastChgID.Value)
            If SC_User_MonthlyRow.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(SC_User_MonthlyRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), SC_User_MonthlyRow.LastChgDate.Value))
            db.AddInParameter(dbcmd, "@PKYM", DbType.String, IIf(SC_User_MonthlyRow.LoadFromDataRow, SC_User_MonthlyRow.YM.OldValue, SC_User_MonthlyRow.YM.Value))
            db.AddInParameter(dbcmd, "@PKUserID", DbType.String, IIf(SC_User_MonthlyRow.LoadFromDataRow, SC_User_MonthlyRow.UserID.OldValue, SC_User_MonthlyRow.UserID.Value))

            Return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function Update(ByVal SC_User_MonthlyRow As Row()) As Integer
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
                    For Each r As Row In SC_User_MonthlyRow
                        strDot = String.Empty
                        If strSQL.Length > 0 Then strSQL.Remove(0, strSQL.Length)
                        strSQL.AppendLine("Update SC_User_Monthly Set")
                        For i As Integer = 0 To r.FieldNames.Length - 1
                            If Not r.IsIdentityField(r.FieldNames(i)) AndAlso r.IsUpdated(r.FieldNames(i)) AndAlso r.CreateUpdateSQL(r.FieldNames(i)) Then
                                strSQL.AppendLine(String.Format("{0}{1} = @{1}", strDot, r.FieldNames(i)))
                                strDot = ","
                            End If
                        Next
                        If strDot = String.Empty Then Continue For
                        strSQL.AppendLine("Where YM = @PKYM")
                        strSQL.AppendLine("And UserID = @PKUserID")

                        dbcmd = db.GetSqlStringCommand(strSQL.ToString())
                        If r.YM.Updated Then db.AddInParameter(dbcmd, "@YM", DbType.String, r.YM.Value)
                        If r.UserID.Updated Then db.AddInParameter(dbcmd, "@UserID", DbType.String, r.UserID.Value)
                        If r.UserName.Updated Then db.AddInParameter(dbcmd, "@UserName", DbType.String, r.UserName.Value)
                        If r.EngName.Updated Then db.AddInParameter(dbcmd, "@EngName", DbType.String, r.EngName.Value)
                        If r.WorkStatus.Updated Then db.AddInParameter(dbcmd, "@WorkStatus", DbType.String, r.WorkStatus.Value)
                        If r.BusinessFlag.Updated Then db.AddInParameter(dbcmd, "@BusinessFlag", DbType.String, r.BusinessFlag.Value)
                        If r.CompID.Updated Then db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                        If r.CompName.Updated Then db.AddInParameter(dbcmd, "@CompName", DbType.String, r.CompName.Value)
                        If r.DeptID.Updated Then db.AddInParameter(dbcmd, "@DeptID", DbType.String, r.DeptID.Value)
                        If r.OrganID.Updated Then db.AddInParameter(dbcmd, "@OrganID", DbType.String, r.OrganID.Value)
                        If r.BusinessID.Updated Then db.AddInParameter(dbcmd, "@BusinessID", DbType.String, r.BusinessID.Value)
                        If r.WorkTypeID.Updated Then db.AddInParameter(dbcmd, "@WorkTypeID", DbType.String, r.WorkTypeID.Value)
                        If r.WorkType.Updated Then db.AddInParameter(dbcmd, "@WorkType", DbType.String, r.WorkType.Value)
                        If r.SecWorkTypeID.Updated Then db.AddInParameter(dbcmd, "@SecWorkTypeID", DbType.String, r.SecWorkTypeID.Value)
                        If r.SecWorkType.Updated Then db.AddInParameter(dbcmd, "@SecWorkType", DbType.String, r.SecWorkType.Value)
                        If r.RankID.Updated Then db.AddInParameter(dbcmd, "@RankID", DbType.String, r.RankID.Value)
                        If r.TitleName.Updated Then db.AddInParameter(dbcmd, "@TitleName", DbType.String, r.TitleName.Value)
                        If r.Password.Updated Then db.AddInParameter(dbcmd, "@Password", DbType.String, r.Password.Value)
                        If r.PasswordErrorCount.Updated Then db.AddInParameter(dbcmd, "@PasswordErrorCount", DbType.Int32, r.PasswordErrorCount.Value)
                        If r.ExpireDate.Updated Then db.AddInParameter(dbcmd, "@ExpireDate", DbType.Date, IIf(IsDateTimeNull(r.ExpireDate.Value), Convert.ToDateTime("1900/1/1"), r.ExpireDate.Value))
                        If r.EMail.Updated Then db.AddInParameter(dbcmd, "@EMail", DbType.String, r.EMail.Value)
                        If r.BanMark.Updated Then db.AddInParameter(dbcmd, "@BanMark", DbType.String, r.BanMark.Value)
                        If r.LastLoginDate.Updated Then db.AddInParameter(dbcmd, "@LastLoginDate", DbType.Date, IIf(IsDateTimeNull(r.LastLoginDate.Value), Convert.ToDateTime("1900/1/1"), r.LastLoginDate.Value))
                        If r.HostName.Updated Then db.AddInParameter(dbcmd, "@HostName", DbType.String, r.HostName.Value)
                        If r.UpdateFlag.Updated Then db.AddInParameter(dbcmd, "@UpdateFlag", DbType.String, r.UpdateFlag.Value)
                        If r.CreateDate.Updated Then db.AddInParameter(dbcmd, "@CreateDate", DbType.Date, IIf(IsDateTimeNull(r.CreateDate.Value), Convert.ToDateTime("1900/1/1"), r.CreateDate.Value))
                        If r.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, r.LastChgID.Value)
                        If r.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(r.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), r.LastChgDate.Value))
                        db.AddInParameter(dbcmd, "@PKYM", DbType.String, IIf(r.LoadFromDataRow, r.YM.OldValue, r.YM.Value))
                        db.AddInParameter(dbcmd, "@PKUserID", DbType.String, IIf(r.LoadFromDataRow, r.UserID.OldValue, r.UserID.Value))

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

        Public Function Update(ByVal SC_User_MonthlyRow As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0
            Dim strDot As String

            For Each r As Row In SC_User_MonthlyRow
                strDot = String.Empty
                If strSQL.Length > 0 Then strSQL.Remove(0, strSQL.Length)
                strSQL.AppendLine("Update SC_User_Monthly Set")
                For i As Integer = 0 To r.FieldNames.Length - 1
                    If Not r.IsIdentityField(r.FieldNames(i)) AndAlso r.IsUpdated(r.FieldNames(i)) AndAlso r.CreateUpdateSQL(r.FieldNames(i))
                        strSQL.AppendLine(String.Format("{0}{1} = @{1}", strDot, r.FieldNames(i)))
                        strDot = ","
                    End If
                Next
                If strDot = String.Empty Then Continue For
                strSQL.AppendLine("Where YM = @PKYM")
                strSQL.AppendLine("And UserID = @PKUserID")

                dbcmd = db.GetSqlStringCommand(strSQL.ToString())
                If r.YM.Updated Then db.AddInParameter(dbcmd, "@YM", DbType.String, r.YM.Value)
                If r.UserID.Updated Then db.AddInParameter(dbcmd, "@UserID", DbType.String, r.UserID.Value)
                If r.UserName.Updated Then db.AddInParameter(dbcmd, "@UserName", DbType.String, r.UserName.Value)
                If r.EngName.Updated Then db.AddInParameter(dbcmd, "@EngName", DbType.String, r.EngName.Value)
                If r.WorkStatus.Updated Then db.AddInParameter(dbcmd, "@WorkStatus", DbType.String, r.WorkStatus.Value)
                If r.BusinessFlag.Updated Then db.AddInParameter(dbcmd, "@BusinessFlag", DbType.String, r.BusinessFlag.Value)
                If r.CompID.Updated Then db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                If r.CompName.Updated Then db.AddInParameter(dbcmd, "@CompName", DbType.String, r.CompName.Value)
                If r.DeptID.Updated Then db.AddInParameter(dbcmd, "@DeptID", DbType.String, r.DeptID.Value)
                If r.OrganID.Updated Then db.AddInParameter(dbcmd, "@OrganID", DbType.String, r.OrganID.Value)
                If r.BusinessID.Updated Then db.AddInParameter(dbcmd, "@BusinessID", DbType.String, r.BusinessID.Value)
                If r.WorkTypeID.Updated Then db.AddInParameter(dbcmd, "@WorkTypeID", DbType.String, r.WorkTypeID.Value)
                If r.WorkType.Updated Then db.AddInParameter(dbcmd, "@WorkType", DbType.String, r.WorkType.Value)
                If r.SecWorkTypeID.Updated Then db.AddInParameter(dbcmd, "@SecWorkTypeID", DbType.String, r.SecWorkTypeID.Value)
                If r.SecWorkType.Updated Then db.AddInParameter(dbcmd, "@SecWorkType", DbType.String, r.SecWorkType.Value)
                If r.RankID.Updated Then db.AddInParameter(dbcmd, "@RankID", DbType.String, r.RankID.Value)
                If r.TitleName.Updated Then db.AddInParameter(dbcmd, "@TitleName", DbType.String, r.TitleName.Value)
                If r.Password.Updated Then db.AddInParameter(dbcmd, "@Password", DbType.String, r.Password.Value)
                If r.PasswordErrorCount.Updated Then db.AddInParameter(dbcmd, "@PasswordErrorCount", DbType.Int32, r.PasswordErrorCount.Value)
                If r.ExpireDate.Updated Then db.AddInParameter(dbcmd, "@ExpireDate", DbType.Date, IIf(IsDateTimeNull(r.ExpireDate.Value), Convert.ToDateTime("1900/1/1"), r.ExpireDate.Value))
                If r.EMail.Updated Then db.AddInParameter(dbcmd, "@EMail", DbType.String, r.EMail.Value)
                If r.BanMark.Updated Then db.AddInParameter(dbcmd, "@BanMark", DbType.String, r.BanMark.Value)
                If r.LastLoginDate.Updated Then db.AddInParameter(dbcmd, "@LastLoginDate", DbType.Date, IIf(IsDateTimeNull(r.LastLoginDate.Value), Convert.ToDateTime("1900/1/1"), r.LastLoginDate.Value))
                If r.HostName.Updated Then db.AddInParameter(dbcmd, "@HostName", DbType.String, r.HostName.Value)
                If r.UpdateFlag.Updated Then db.AddInParameter(dbcmd, "@UpdateFlag", DbType.String, r.UpdateFlag.Value)
                If r.CreateDate.Updated Then db.AddInParameter(dbcmd, "@CreateDate", DbType.Date, IIf(IsDateTimeNull(r.CreateDate.Value), Convert.ToDateTime("1900/1/1"), r.CreateDate.Value))
                If r.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, r.LastChgID.Value)
                If r.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(r.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), r.LastChgDate.Value))
                db.AddInParameter(dbcmd, "@PKYM", DbType.String, IIf(r.LoadFromDataRow, r.YM.OldValue, r.YM.Value))
                db.AddInParameter(dbcmd, "@PKUserID", DbType.String, IIf(r.LoadFromDataRow, r.UserID.OldValue, r.UserID.Value))

                intRowsAffected += db.ExecuteNonQuery(dbcmd, tran)
            Next
            Return intRowsAffected
        End Function

        Public Function IsDataExists(ByVal SC_User_MonthlyRow As Row) As Boolean
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intCount As Integer = 0

            strSQL.AppendLine("Select Count(*) Cnt From SC_User_Monthly")
            strSQL.AppendLine("Where YM = @YM")
            strSQL.AppendLine("And UserID = @UserID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@YM", DbType.String, SC_User_MonthlyRow.YM.Value)
            db.AddInParameter(dbcmd, "@UserID", DbType.String, SC_User_MonthlyRow.UserID.Value)

            intCount = Convert.ToInt32(db.ExecuteScalar(dbcmd))
            Return IIf(intCount > 0, True, False)
        End Function

        Public Function IsDataExists(ByVal SC_User_MonthlyRow As Row, ByVal tran As DbTransaction) As Boolean
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intCount As Integer = 0

            strSQL.AppendLine("Select Count(*) Cnt From SC_User_Monthly")
            strSQL.AppendLine("Where YM = @YM")
            strSQL.AppendLine("And UserID = @UserID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@YM", DbType.String, SC_User_MonthlyRow.YM.Value)
            db.AddInParameter(dbcmd, "@UserID", DbType.String, SC_User_MonthlyRow.UserID.Value)

            intCount = Convert.ToInt32(db.ExecuteScalar(dbcmd, tran))
            Return IIf(intCount > 0, True, False)
        End Function

        Public Function QuerybyWhere(WhereCondition As String) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From SC_User_Monthly")
            If WhereCondition <> String.Empty Then strSQL.AppendLine(WhereCondition)

            Return db.ExecuteDataSet(CommandType.Text, strSQL.ToString())
        End Function

        Public Function Insert(ByVal SC_User_MonthlyRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Insert into SC_User_Monthly")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    YM, UserID, UserName, EngName, WorkStatus, BusinessFlag, CompID, CompName, DeptID,")
            strSQL.AppendLine("    OrganID, BusinessID, WorkTypeID, WorkType, SecWorkTypeID, SecWorkType, RankID, TitleName,")
            strSQL.AppendLine("    Password, PasswordErrorCount, ExpireDate, EMail, BanMark, LastLoginDate, HostName, UpdateFlag,")
            strSQL.AppendLine("    CreateDate, LastChgID, LastChgDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @YM, @UserID, @UserName, @EngName, @WorkStatus, @BusinessFlag, @CompID, @CompName, @DeptID,")
            strSQL.AppendLine("    @OrganID, @BusinessID, @WorkTypeID, @WorkType, @SecWorkTypeID, @SecWorkType, @RankID, @TitleName,")
            strSQL.AppendLine("    @Password, @PasswordErrorCount, @ExpireDate, @EMail, @BanMark, @LastLoginDate, @HostName, @UpdateFlag,")
            strSQL.AppendLine("    @CreateDate, @LastChgID, @LastChgDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@YM", DbType.String, SC_User_MonthlyRow.YM.Value)
            db.AddInParameter(dbcmd, "@UserID", DbType.String, SC_User_MonthlyRow.UserID.Value)
            db.AddInParameter(dbcmd, "@UserName", DbType.String, SC_User_MonthlyRow.UserName.Value)
            db.AddInParameter(dbcmd, "@EngName", DbType.String, SC_User_MonthlyRow.EngName.Value)
            db.AddInParameter(dbcmd, "@WorkStatus", DbType.String, SC_User_MonthlyRow.WorkStatus.Value)
            db.AddInParameter(dbcmd, "@BusinessFlag", DbType.String, SC_User_MonthlyRow.BusinessFlag.Value)
            db.AddInParameter(dbcmd, "@CompID", DbType.String, SC_User_MonthlyRow.CompID.Value)
            db.AddInParameter(dbcmd, "@CompName", DbType.String, SC_User_MonthlyRow.CompName.Value)
            db.AddInParameter(dbcmd, "@DeptID", DbType.String, SC_User_MonthlyRow.DeptID.Value)
            db.AddInParameter(dbcmd, "@OrganID", DbType.String, SC_User_MonthlyRow.OrganID.Value)
            db.AddInParameter(dbcmd, "@BusinessID", DbType.String, SC_User_MonthlyRow.BusinessID.Value)
            db.AddInParameter(dbcmd, "@WorkTypeID", DbType.String, SC_User_MonthlyRow.WorkTypeID.Value)
            db.AddInParameter(dbcmd, "@WorkType", DbType.String, SC_User_MonthlyRow.WorkType.Value)
            db.AddInParameter(dbcmd, "@SecWorkTypeID", DbType.String, SC_User_MonthlyRow.SecWorkTypeID.Value)
            db.AddInParameter(dbcmd, "@SecWorkType", DbType.String, SC_User_MonthlyRow.SecWorkType.Value)
            db.AddInParameter(dbcmd, "@RankID", DbType.String, SC_User_MonthlyRow.RankID.Value)
            db.AddInParameter(dbcmd, "@TitleName", DbType.String, SC_User_MonthlyRow.TitleName.Value)
            db.AddInParameter(dbcmd, "@Password", DbType.String, SC_User_MonthlyRow.Password.Value)
            db.AddInParameter(dbcmd, "@PasswordErrorCount", DbType.Int32, SC_User_MonthlyRow.PasswordErrorCount.Value)
            db.AddInParameter(dbcmd, "@ExpireDate", DbType.Date, IIf(IsDateTimeNull(SC_User_MonthlyRow.ExpireDate.Value), Convert.ToDateTime("1900/1/1"), SC_User_MonthlyRow.ExpireDate.Value))
            db.AddInParameter(dbcmd, "@EMail", DbType.String, SC_User_MonthlyRow.EMail.Value)
            db.AddInParameter(dbcmd, "@BanMark", DbType.String, SC_User_MonthlyRow.BanMark.Value)
            db.AddInParameter(dbcmd, "@LastLoginDate", DbType.Date, IIf(IsDateTimeNull(SC_User_MonthlyRow.LastLoginDate.Value), Convert.ToDateTime("1900/1/1"), SC_User_MonthlyRow.LastLoginDate.Value))
            db.AddInParameter(dbcmd, "@HostName", DbType.String, SC_User_MonthlyRow.HostName.Value)
            db.AddInParameter(dbcmd, "@UpdateFlag", DbType.String, SC_User_MonthlyRow.UpdateFlag.Value)
            db.AddInParameter(dbcmd, "@CreateDate", DbType.Date, IIf(IsDateTimeNull(SC_User_MonthlyRow.CreateDate.Value), Convert.ToDateTime("1900/1/1"), SC_User_MonthlyRow.CreateDate.Value))
            db.AddInParameter(dbcmd, "@LastChgID", DbType.String, SC_User_MonthlyRow.LastChgID.Value)
            db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(SC_User_MonthlyRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), SC_User_MonthlyRow.LastChgDate.Value))

            Return db.ExecuteNonQuery(dbcmd)
        End Function

        Public Function Insert(ByVal SC_User_MonthlyRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Insert into SC_User_Monthly")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    YM, UserID, UserName, EngName, WorkStatus, BusinessFlag, CompID, CompName, DeptID,")
            strSQL.AppendLine("    OrganID, BusinessID, WorkTypeID, WorkType, SecWorkTypeID, SecWorkType, RankID, TitleName,")
            strSQL.AppendLine("    Password, PasswordErrorCount, ExpireDate, EMail, BanMark, LastLoginDate, HostName, UpdateFlag,")
            strSQL.AppendLine("    CreateDate, LastChgID, LastChgDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @YM, @UserID, @UserName, @EngName, @WorkStatus, @BusinessFlag, @CompID, @CompName, @DeptID,")
            strSQL.AppendLine("    @OrganID, @BusinessID, @WorkTypeID, @WorkType, @SecWorkTypeID, @SecWorkType, @RankID, @TitleName,")
            strSQL.AppendLine("    @Password, @PasswordErrorCount, @ExpireDate, @EMail, @BanMark, @LastLoginDate, @HostName, @UpdateFlag,")
            strSQL.AppendLine("    @CreateDate, @LastChgID, @LastChgDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@YM", DbType.String, SC_User_MonthlyRow.YM.Value)
            db.AddInParameter(dbcmd, "@UserID", DbType.String, SC_User_MonthlyRow.UserID.Value)
            db.AddInParameter(dbcmd, "@UserName", DbType.String, SC_User_MonthlyRow.UserName.Value)
            db.AddInParameter(dbcmd, "@EngName", DbType.String, SC_User_MonthlyRow.EngName.Value)
            db.AddInParameter(dbcmd, "@WorkStatus", DbType.String, SC_User_MonthlyRow.WorkStatus.Value)
            db.AddInParameter(dbcmd, "@BusinessFlag", DbType.String, SC_User_MonthlyRow.BusinessFlag.Value)
            db.AddInParameter(dbcmd, "@CompID", DbType.String, SC_User_MonthlyRow.CompID.Value)
            db.AddInParameter(dbcmd, "@CompName", DbType.String, SC_User_MonthlyRow.CompName.Value)
            db.AddInParameter(dbcmd, "@DeptID", DbType.String, SC_User_MonthlyRow.DeptID.Value)
            db.AddInParameter(dbcmd, "@OrganID", DbType.String, SC_User_MonthlyRow.OrganID.Value)
            db.AddInParameter(dbcmd, "@BusinessID", DbType.String, SC_User_MonthlyRow.BusinessID.Value)
            db.AddInParameter(dbcmd, "@WorkTypeID", DbType.String, SC_User_MonthlyRow.WorkTypeID.Value)
            db.AddInParameter(dbcmd, "@WorkType", DbType.String, SC_User_MonthlyRow.WorkType.Value)
            db.AddInParameter(dbcmd, "@SecWorkTypeID", DbType.String, SC_User_MonthlyRow.SecWorkTypeID.Value)
            db.AddInParameter(dbcmd, "@SecWorkType", DbType.String, SC_User_MonthlyRow.SecWorkType.Value)
            db.AddInParameter(dbcmd, "@RankID", DbType.String, SC_User_MonthlyRow.RankID.Value)
            db.AddInParameter(dbcmd, "@TitleName", DbType.String, SC_User_MonthlyRow.TitleName.Value)
            db.AddInParameter(dbcmd, "@Password", DbType.String, SC_User_MonthlyRow.Password.Value)
            db.AddInParameter(dbcmd, "@PasswordErrorCount", DbType.Int32, SC_User_MonthlyRow.PasswordErrorCount.Value)
            db.AddInParameter(dbcmd, "@ExpireDate", DbType.Date, IIf(IsDateTimeNull(SC_User_MonthlyRow.ExpireDate.Value), Convert.ToDateTime("1900/1/1"), SC_User_MonthlyRow.ExpireDate.Value))
            db.AddInParameter(dbcmd, "@EMail", DbType.String, SC_User_MonthlyRow.EMail.Value)
            db.AddInParameter(dbcmd, "@BanMark", DbType.String, SC_User_MonthlyRow.BanMark.Value)
            db.AddInParameter(dbcmd, "@LastLoginDate", DbType.Date, IIf(IsDateTimeNull(SC_User_MonthlyRow.LastLoginDate.Value), Convert.ToDateTime("1900/1/1"), SC_User_MonthlyRow.LastLoginDate.Value))
            db.AddInParameter(dbcmd, "@HostName", DbType.String, SC_User_MonthlyRow.HostName.Value)
            db.AddInParameter(dbcmd, "@UpdateFlag", DbType.String, SC_User_MonthlyRow.UpdateFlag.Value)
            db.AddInParameter(dbcmd, "@CreateDate", DbType.Date, IIf(IsDateTimeNull(SC_User_MonthlyRow.CreateDate.Value), Convert.ToDateTime("1900/1/1"), SC_User_MonthlyRow.CreateDate.Value))
            db.AddInParameter(dbcmd, "@LastChgID", DbType.String, SC_User_MonthlyRow.LastChgID.Value)
            db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(SC_User_MonthlyRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), SC_User_MonthlyRow.LastChgDate.Value))

            Return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function Insert(ByVal SC_User_MonthlyRow As Row()) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Insert into SC_User_Monthly")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    YM, UserID, UserName, EngName, WorkStatus, BusinessFlag, CompID, CompName, DeptID,")
            strSQL.AppendLine("    OrganID, BusinessID, WorkTypeID, WorkType, SecWorkTypeID, SecWorkType, RankID, TitleName,")
            strSQL.AppendLine("    Password, PasswordErrorCount, ExpireDate, EMail, BanMark, LastLoginDate, HostName, UpdateFlag,")
            strSQL.AppendLine("    CreateDate, LastChgID, LastChgDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @YM, @UserID, @UserName, @EngName, @WorkStatus, @BusinessFlag, @CompID, @CompName, @DeptID,")
            strSQL.AppendLine("    @OrganID, @BusinessID, @WorkTypeID, @WorkType, @SecWorkTypeID, @SecWorkType, @RankID, @TitleName,")
            strSQL.AppendLine("    @Password, @PasswordErrorCount, @ExpireDate, @EMail, @BanMark, @LastLoginDate, @HostName, @UpdateFlag,")
            strSQL.AppendLine("    @CreateDate, @LastChgID, @LastChgDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            Using cn As DbConnection = db.CreateConnection()
                cn.Open()
                Dim tran As DbTransaction = cn.BeginTransaction()
                Dim inTrans As Boolean = True

                Try
                    For Each r As Row In SC_User_MonthlyRow
                        dbcmd.Parameters.Clear()
                        db.AddInParameter(dbcmd, "@YM", DbType.String, r.YM.Value)
                        db.AddInParameter(dbcmd, "@UserID", DbType.String, r.UserID.Value)
                        db.AddInParameter(dbcmd, "@UserName", DbType.String, r.UserName.Value)
                        db.AddInParameter(dbcmd, "@EngName", DbType.String, r.EngName.Value)
                        db.AddInParameter(dbcmd, "@WorkStatus", DbType.String, r.WorkStatus.Value)
                        db.AddInParameter(dbcmd, "@BusinessFlag", DbType.String, r.BusinessFlag.Value)
                        db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                        db.AddInParameter(dbcmd, "@CompName", DbType.String, r.CompName.Value)
                        db.AddInParameter(dbcmd, "@DeptID", DbType.String, r.DeptID.Value)
                        db.AddInParameter(dbcmd, "@OrganID", DbType.String, r.OrganID.Value)
                        db.AddInParameter(dbcmd, "@BusinessID", DbType.String, r.BusinessID.Value)
                        db.AddInParameter(dbcmd, "@WorkTypeID", DbType.String, r.WorkTypeID.Value)
                        db.AddInParameter(dbcmd, "@WorkType", DbType.String, r.WorkType.Value)
                        db.AddInParameter(dbcmd, "@SecWorkTypeID", DbType.String, r.SecWorkTypeID.Value)
                        db.AddInParameter(dbcmd, "@SecWorkType", DbType.String, r.SecWorkType.Value)
                        db.AddInParameter(dbcmd, "@RankID", DbType.String, r.RankID.Value)
                        db.AddInParameter(dbcmd, "@TitleName", DbType.String, r.TitleName.Value)
                        db.AddInParameter(dbcmd, "@Password", DbType.String, r.Password.Value)
                        db.AddInParameter(dbcmd, "@PasswordErrorCount", DbType.Int32, r.PasswordErrorCount.Value)
                        db.AddInParameter(dbcmd, "@ExpireDate", DbType.Date, IIf(IsDateTimeNull(r.ExpireDate.Value), Convert.ToDateTime("1900/1/1"), r.ExpireDate.Value))
                        db.AddInParameter(dbcmd, "@EMail", DbType.String, r.EMail.Value)
                        db.AddInParameter(dbcmd, "@BanMark", DbType.String, r.BanMark.Value)
                        db.AddInParameter(dbcmd, "@LastLoginDate", DbType.Date, IIf(IsDateTimeNull(r.LastLoginDate.Value), Convert.ToDateTime("1900/1/1"), r.LastLoginDate.Value))
                        db.AddInParameter(dbcmd, "@HostName", DbType.String, r.HostName.Value)
                        db.AddInParameter(dbcmd, "@UpdateFlag", DbType.String, r.UpdateFlag.Value)
                        db.AddInParameter(dbcmd, "@CreateDate", DbType.Date, IIf(IsDateTimeNull(r.CreateDate.Value), Convert.ToDateTime("1900/1/1"), r.CreateDate.Value))
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

        Public Function Insert(ByVal SC_User_MonthlyRow As Row(), ByVal tran As DbTransaction) As Boolean
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Insert into SC_User_Monthly")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    YM, UserID, UserName, EngName, WorkStatus, BusinessFlag, CompID, CompName, DeptID,")
            strSQL.AppendLine("    OrganID, BusinessID, WorkTypeID, WorkType, SecWorkTypeID, SecWorkType, RankID, TitleName,")
            strSQL.AppendLine("    Password, PasswordErrorCount, ExpireDate, EMail, BanMark, LastLoginDate, HostName, UpdateFlag,")
            strSQL.AppendLine("    CreateDate, LastChgID, LastChgDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @YM, @UserID, @UserName, @EngName, @WorkStatus, @BusinessFlag, @CompID, @CompName, @DeptID,")
            strSQL.AppendLine("    @OrganID, @BusinessID, @WorkTypeID, @WorkType, @SecWorkTypeID, @SecWorkType, @RankID, @TitleName,")
            strSQL.AppendLine("    @Password, @PasswordErrorCount, @ExpireDate, @EMail, @BanMark, @LastLoginDate, @HostName, @UpdateFlag,")
            strSQL.AppendLine("    @CreateDate, @LastChgID, @LastChgDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())

            For Each r As Row In SC_User_MonthlyRow
                dbcmd.Parameters.Clear()
                db.AddInParameter(dbcmd, "@YM", DbType.String, r.YM.Value)
                db.AddInParameter(dbcmd, "@UserID", DbType.String, r.UserID.Value)
                db.AddInParameter(dbcmd, "@UserName", DbType.String, r.UserName.Value)
                db.AddInParameter(dbcmd, "@EngName", DbType.String, r.EngName.Value)
                db.AddInParameter(dbcmd, "@WorkStatus", DbType.String, r.WorkStatus.Value)
                db.AddInParameter(dbcmd, "@BusinessFlag", DbType.String, r.BusinessFlag.Value)
                db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                db.AddInParameter(dbcmd, "@CompName", DbType.String, r.CompName.Value)
                db.AddInParameter(dbcmd, "@DeptID", DbType.String, r.DeptID.Value)
                db.AddInParameter(dbcmd, "@OrganID", DbType.String, r.OrganID.Value)
                db.AddInParameter(dbcmd, "@BusinessID", DbType.String, r.BusinessID.Value)
                db.AddInParameter(dbcmd, "@WorkTypeID", DbType.String, r.WorkTypeID.Value)
                db.AddInParameter(dbcmd, "@WorkType", DbType.String, r.WorkType.Value)
                db.AddInParameter(dbcmd, "@SecWorkTypeID", DbType.String, r.SecWorkTypeID.Value)
                db.AddInParameter(dbcmd, "@SecWorkType", DbType.String, r.SecWorkType.Value)
                db.AddInParameter(dbcmd, "@RankID", DbType.String, r.RankID.Value)
                db.AddInParameter(dbcmd, "@TitleName", DbType.String, r.TitleName.Value)
                db.AddInParameter(dbcmd, "@Password", DbType.String, r.Password.Value)
                db.AddInParameter(dbcmd, "@PasswordErrorCount", DbType.Int32, r.PasswordErrorCount.Value)
                db.AddInParameter(dbcmd, "@ExpireDate", DbType.Date, IIf(IsDateTimeNull(r.ExpireDate.Value), Convert.ToDateTime("1900/1/1"), r.ExpireDate.Value))
                db.AddInParameter(dbcmd, "@EMail", DbType.String, r.EMail.Value)
                db.AddInParameter(dbcmd, "@BanMark", DbType.String, r.BanMark.Value)
                db.AddInParameter(dbcmd, "@LastLoginDate", DbType.Date, IIf(IsDateTimeNull(r.LastLoginDate.Value), Convert.ToDateTime("1900/1/1"), r.LastLoginDate.Value))
                db.AddInParameter(dbcmd, "@HostName", DbType.String, r.HostName.Value)
                db.AddInParameter(dbcmd, "@UpdateFlag", DbType.String, r.UpdateFlag.Value)
                db.AddInParameter(dbcmd, "@CreateDate", DbType.Date, IIf(IsDateTimeNull(r.CreateDate.Value), Convert.ToDateTime("1900/1/1"), r.CreateDate.Value))
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

