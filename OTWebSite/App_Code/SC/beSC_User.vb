'****************************************************************
' Table:SC_User
' Created Date: 2014.10.31
'****************************************************************/
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports Microsoft.Practices.EnterpriseLibrary.Common
Imports Microsoft.Practices.EnterpriseLibrary.Data
Imports System.Data.Common
Imports System.Data

Namespace beSC_User
    Public Class Table
        Private m_Rows As Rows = New Rows()
        Private m_Fields As String() = { "CompID", "UserID", "UserName", "EngName", "IDNo", "PassportName", "BirthDate", "Sex", "NationID", "IDExpireDate" _
                                    , "EduID", "Marriage", "WorkStatus", "EmpType", "GroupID", "DeptID", "OrganID", "WorkSiteID", "RankID", "HoldingRankID", "TitleID" _
                                    , "PublicTitleID", "EmpDate", "SinopacEmpDate", "QuitDate", "ProbDate", "ProbMonth", "NotEmpDay", "CheckInFlag", "IsBossFlag", "PassExamFlag", "LocalHireFlag" _
                                    , "Password", "PasswordErrorCount", "ExpireDate", "EMail", "BanMark", "BanMarkValidDate", "LastLoginTime", "HostName", "WorkTypeID", "SecWorkTypeID", "CreateDate" _
                                    , "LastChgComp", "LastChgID", "LastChgDate" }
        Private m_Types As System.Type() = { GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(Date), GetType(String), GetType(String), GetType(Date) _
                                    , GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String) _
                                    , GetType(String), GetType(Date), GetType(Date), GetType(Date), GetType(Date), GetType(Integer), GetType(Integer), GetType(String), GetType(String), GetType(String), GetType(String) _
                                    , GetType(String), GetType(Integer), GetType(Date), GetType(String), GetType(String), GetType(Date), GetType(Date), GetType(String), GetType(String), GetType(String), GetType(Date) _
                                    , GetType(String), GetType(String), GetType(Date) }
        Private m_PrimaryFields As String() = { "CompID", "UserID" }

        Public ReadOnly Property Rows() As beSC_User.Rows 
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
        Public Sub Transfer2Row(SC_UserTable As DataTable)
            For Each dr As DataRow In SC_UserTable.Rows
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
                dr(m_Rows(i).UserID.FieldName) = m_Rows(i).UserID.Value
                dr(m_Rows(i).UserName.FieldName) = m_Rows(i).UserName.Value
                dr(m_Rows(i).EngName.FieldName) = m_Rows(i).EngName.Value
                dr(m_Rows(i).IDNo.FieldName) = m_Rows(i).IDNo.Value
                dr(m_Rows(i).PassportName.FieldName) = m_Rows(i).PassportName.Value
                dr(m_Rows(i).BirthDate.FieldName) = m_Rows(i).BirthDate.Value
                dr(m_Rows(i).Sex.FieldName) = m_Rows(i).Sex.Value
                dr(m_Rows(i).NationID.FieldName) = m_Rows(i).NationID.Value
                dr(m_Rows(i).IDExpireDate.FieldName) = m_Rows(i).IDExpireDate.Value
                dr(m_Rows(i).EduID.FieldName) = m_Rows(i).EduID.Value
                dr(m_Rows(i).Marriage.FieldName) = m_Rows(i).Marriage.Value
                dr(m_Rows(i).WorkStatus.FieldName) = m_Rows(i).WorkStatus.Value
                dr(m_Rows(i).EmpType.FieldName) = m_Rows(i).EmpType.Value
                dr(m_Rows(i).GroupID.FieldName) = m_Rows(i).GroupID.Value
                dr(m_Rows(i).DeptID.FieldName) = m_Rows(i).DeptID.Value
                dr(m_Rows(i).OrganID.FieldName) = m_Rows(i).OrganID.Value
                dr(m_Rows(i).WorkSiteID.FieldName) = m_Rows(i).WorkSiteID.Value
                dr(m_Rows(i).RankID.FieldName) = m_Rows(i).RankID.Value
                dr(m_Rows(i).HoldingRankID.FieldName) = m_Rows(i).HoldingRankID.Value
                dr(m_Rows(i).TitleID.FieldName) = m_Rows(i).TitleID.Value
                dr(m_Rows(i).PublicTitleID.FieldName) = m_Rows(i).PublicTitleID.Value
                dr(m_Rows(i).EmpDate.FieldName) = m_Rows(i).EmpDate.Value
                dr(m_Rows(i).SinopacEmpDate.FieldName) = m_Rows(i).SinopacEmpDate.Value
                dr(m_Rows(i).QuitDate.FieldName) = m_Rows(i).QuitDate.Value
                dr(m_Rows(i).ProbDate.FieldName) = m_Rows(i).ProbDate.Value
                dr(m_Rows(i).ProbMonth.FieldName) = m_Rows(i).ProbMonth.Value
                dr(m_Rows(i).NotEmpDay.FieldName) = m_Rows(i).NotEmpDay.Value
                dr(m_Rows(i).CheckInFlag.FieldName) = m_Rows(i).CheckInFlag.Value
                dr(m_Rows(i).IsBossFlag.FieldName) = m_Rows(i).IsBossFlag.Value
                dr(m_Rows(i).PassExamFlag.FieldName) = m_Rows(i).PassExamFlag.Value
                dr(m_Rows(i).LocalHireFlag.FieldName) = m_Rows(i).LocalHireFlag.Value
                dr(m_Rows(i).Password.FieldName) = m_Rows(i).Password.Value
                dr(m_Rows(i).PasswordErrorCount.FieldName) = m_Rows(i).PasswordErrorCount.Value
                dr(m_Rows(i).ExpireDate.FieldName) = m_Rows(i).ExpireDate.Value
                dr(m_Rows(i).EMail.FieldName) = m_Rows(i).EMail.Value
                dr(m_Rows(i).BanMark.FieldName) = m_Rows(i).BanMark.Value
                dr(m_Rows(i).BanMarkValidDate.FieldName) = m_Rows(i).BanMarkValidDate.Value
                dr(m_Rows(i).LastLoginTime.FieldName) = m_Rows(i).LastLoginTime.Value
                dr(m_Rows(i).HostName.FieldName) = m_Rows(i).HostName.Value
                dr(m_Rows(i).WorkTypeID.FieldName) = m_Rows(i).WorkTypeID.Value
                dr(m_Rows(i).SecWorkTypeID.FieldName) = m_Rows(i).SecWorkTypeID.Value
                dr(m_Rows(i).CreateDate.FieldName) = m_Rows(i).CreateDate.Value
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

        Public Sub Add(SC_UserRow As Row)
            m_Rows.Add(SC_UserRow)
        End Sub

        Public Sub Remove(SC_UserRow As Row)
            If m_Rows.IndexOf(SC_UserRow) >= 0 Then
                m_Rows.Remove(SC_UserRow)
            End If
        End Sub

        Public Sub Dispose()
            m_Rows.Clear()
        End Sub

    End Class

    Public Class Row

        Private FI_CompID As Field(Of String) = new Field(Of String)("CompID", true)
        Private FI_UserID As Field(Of String) = new Field(Of String)("UserID", true)
        Private FI_UserName As Field(Of String) = new Field(Of String)("UserName", true)
        Private FI_EngName As Field(Of String) = new Field(Of String)("EngName", true)
        Private FI_IDNo As Field(Of String) = new Field(Of String)("IDNo", true)
        Private FI_PassportName As Field(Of String) = new Field(Of String)("PassportName", true)
        Private FI_BirthDate As Field(Of Date) = new Field(Of Date)("BirthDate", true)
        Private FI_Sex As Field(Of String) = new Field(Of String)("Sex", true)
        Private FI_NationID As Field(Of String) = new Field(Of String)("NationID", true)
        Private FI_IDExpireDate As Field(Of Date) = new Field(Of Date)("IDExpireDate", true)
        Private FI_EduID As Field(Of String) = new Field(Of String)("EduID", true)
        Private FI_Marriage As Field(Of String) = new Field(Of String)("Marriage", true)
        Private FI_WorkStatus As Field(Of String) = new Field(Of String)("WorkStatus", true)
        Private FI_EmpType As Field(Of String) = new Field(Of String)("EmpType", true)
        Private FI_GroupID As Field(Of String) = new Field(Of String)("GroupID", true)
        Private FI_DeptID As Field(Of String) = new Field(Of String)("DeptID", true)
        Private FI_OrganID As Field(Of String) = new Field(Of String)("OrganID", true)
        Private FI_WorkSiteID As Field(Of String) = new Field(Of String)("WorkSiteID", true)
        Private FI_RankID As Field(Of String) = new Field(Of String)("RankID", true)
        Private FI_HoldingRankID As Field(Of String) = new Field(Of String)("HoldingRankID", true)
        Private FI_TitleID As Field(Of String) = new Field(Of String)("TitleID", true)
        Private FI_PublicTitleID As Field(Of String) = new Field(Of String)("PublicTitleID", true)
        Private FI_EmpDate As Field(Of Date) = new Field(Of Date)("EmpDate", true)
        Private FI_SinopacEmpDate As Field(Of Date) = new Field(Of Date)("SinopacEmpDate", true)
        Private FI_QuitDate As Field(Of Date) = new Field(Of Date)("QuitDate", true)
        Private FI_ProbDate As Field(Of Date) = new Field(Of Date)("ProbDate", true)
        Private FI_ProbMonth As Field(Of Integer) = new Field(Of Integer)("ProbMonth", true)
        Private FI_NotEmpDay As Field(Of Integer) = new Field(Of Integer)("NotEmpDay", true)
        Private FI_CheckInFlag As Field(Of String) = new Field(Of String)("CheckInFlag", true)
        Private FI_IsBossFlag As Field(Of String) = new Field(Of String)("IsBossFlag", true)
        Private FI_PassExamFlag As Field(Of String) = new Field(Of String)("PassExamFlag", true)
        Private FI_LocalHireFlag As Field(Of String) = new Field(Of String)("LocalHireFlag", true)
        Private FI_Password As Field(Of String) = new Field(Of String)("Password", true)
        Private FI_PasswordErrorCount As Field(Of Integer) = new Field(Of Integer)("PasswordErrorCount", true)
        Private FI_ExpireDate As Field(Of Date) = new Field(Of Date)("ExpireDate", true)
        Private FI_EMail As Field(Of String) = new Field(Of String)("EMail", true)
        Private FI_BanMark As Field(Of String) = new Field(Of String)("BanMark", true)
        Private FI_BanMarkValidDate As Field(Of Date) = new Field(Of Date)("BanMarkValidDate", true)
        Private FI_LastLoginTime As Field(Of Date) = new Field(Of Date)("LastLoginTime", true)
        Private FI_HostName As Field(Of String) = new Field(Of String)("HostName", true)
        Private FI_WorkTypeID As Field(Of String) = new Field(Of String)("WorkTypeID", true)
        Private FI_SecWorkTypeID As Field(Of String) = new Field(Of String)("SecWorkTypeID", true)
        Private FI_CreateDate As Field(Of Date) = new Field(Of Date)("CreateDate", true)
        Private FI_LastChgComp As Field(Of String) = new Field(Of String)("LastChgComp", true)
        Private FI_LastChgID As Field(Of String) = new Field(Of String)("LastChgID", true)
        Private FI_LastChgDate As Field(Of Date) = new Field(Of Date)("LastChgDate", true)
        Private m_FieldNames As String() = { "CompID", "UserID", "UserName", "EngName", "IDNo", "PassportName", "BirthDate", "Sex", "NationID", "IDExpireDate" _
                                    , "EduID", "Marriage", "WorkStatus", "EmpType", "GroupID", "DeptID", "OrganID", "WorkSiteID", "RankID", "HoldingRankID", "TitleID" _
                                    , "PublicTitleID", "EmpDate", "SinopacEmpDate", "QuitDate", "ProbDate", "ProbMonth", "NotEmpDay", "CheckInFlag", "IsBossFlag", "PassExamFlag", "LocalHireFlag" _
                                    , "Password", "PasswordErrorCount", "ExpireDate", "EMail", "BanMark", "BanMarkValidDate", "LastLoginTime", "HostName", "WorkTypeID", "SecWorkTypeID", "CreateDate" _
                                    , "LastChgComp", "LastChgID", "LastChgDate" }
        Private m_PrimaryFields As String() = { "CompID", "UserID" }
        Private m_IdentityFields As String() = {  }
        Private m_LoadFromDataRow As Boolean = False

        Private Function GetFieldValue(ByVal fieldName As String) As Object
            Select Case fieldName
                Case "CompID"
                    Return FI_CompID.Value
                Case "UserID"
                    Return FI_UserID.Value
                Case "UserName"
                    Return FI_UserName.Value
                Case "EngName"
                    Return FI_EngName.Value
                Case "IDNo"
                    Return FI_IDNo.Value
                Case "PassportName"
                    Return FI_PassportName.Value
                Case "BirthDate"
                    Return FI_BirthDate.Value
                Case "Sex"
                    Return FI_Sex.Value
                Case "NationID"
                    Return FI_NationID.Value
                Case "IDExpireDate"
                    Return FI_IDExpireDate.Value
                Case "EduID"
                    Return FI_EduID.Value
                Case "Marriage"
                    Return FI_Marriage.Value
                Case "WorkStatus"
                    Return FI_WorkStatus.Value
                Case "EmpType"
                    Return FI_EmpType.Value
                Case "GroupID"
                    Return FI_GroupID.Value
                Case "DeptID"
                    Return FI_DeptID.Value
                Case "OrganID"
                    Return FI_OrganID.Value
                Case "WorkSiteID"
                    Return FI_WorkSiteID.Value
                Case "RankID"
                    Return FI_RankID.Value
                Case "HoldingRankID"
                    Return FI_HoldingRankID.Value
                Case "TitleID"
                    Return FI_TitleID.Value
                Case "PublicTitleID"
                    Return FI_PublicTitleID.Value
                Case "EmpDate"
                    Return FI_EmpDate.Value
                Case "SinopacEmpDate"
                    Return FI_SinopacEmpDate.Value
                Case "QuitDate"
                    Return FI_QuitDate.Value
                Case "ProbDate"
                    Return FI_ProbDate.Value
                Case "ProbMonth"
                    Return FI_ProbMonth.Value
                Case "NotEmpDay"
                    Return FI_NotEmpDay.Value
                Case "CheckInFlag"
                    Return FI_CheckInFlag.Value
                Case "IsBossFlag"
                    Return FI_IsBossFlag.Value
                Case "PassExamFlag"
                    Return FI_PassExamFlag.Value
                Case "LocalHireFlag"
                    Return FI_LocalHireFlag.Value
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
                Case "BanMarkValidDate"
                    Return FI_BanMarkValidDate.Value
                Case "LastLoginTime"
                    Return FI_LastLoginTime.Value
                Case "HostName"
                    Return FI_HostName.Value
                Case "WorkTypeID"
                    Return FI_WorkTypeID.Value
                Case "SecWorkTypeID"
                    Return FI_SecWorkTypeID.Value
                Case "CreateDate"
                    Return FI_CreateDate.Value
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
                Case "UserID"
                    FI_UserID.SetValue(value)
                Case "UserName"
                    FI_UserName.SetValue(value)
                Case "EngName"
                    FI_EngName.SetValue(value)
                Case "IDNo"
                    FI_IDNo.SetValue(value)
                Case "PassportName"
                    FI_PassportName.SetValue(value)
                Case "BirthDate"
                    FI_BirthDate.SetValue(value)
                Case "Sex"
                    FI_Sex.SetValue(value)
                Case "NationID"
                    FI_NationID.SetValue(value)
                Case "IDExpireDate"
                    FI_IDExpireDate.SetValue(value)
                Case "EduID"
                    FI_EduID.SetValue(value)
                Case "Marriage"
                    FI_Marriage.SetValue(value)
                Case "WorkStatus"
                    FI_WorkStatus.SetValue(value)
                Case "EmpType"
                    FI_EmpType.SetValue(value)
                Case "GroupID"
                    FI_GroupID.SetValue(value)
                Case "DeptID"
                    FI_DeptID.SetValue(value)
                Case "OrganID"
                    FI_OrganID.SetValue(value)
                Case "WorkSiteID"
                    FI_WorkSiteID.SetValue(value)
                Case "RankID"
                    FI_RankID.SetValue(value)
                Case "HoldingRankID"
                    FI_HoldingRankID.SetValue(value)
                Case "TitleID"
                    FI_TitleID.SetValue(value)
                Case "PublicTitleID"
                    FI_PublicTitleID.SetValue(value)
                Case "EmpDate"
                    FI_EmpDate.SetValue(value)
                Case "SinopacEmpDate"
                    FI_SinopacEmpDate.SetValue(value)
                Case "QuitDate"
                    FI_QuitDate.SetValue(value)
                Case "ProbDate"
                    FI_ProbDate.SetValue(value)
                Case "ProbMonth"
                    FI_ProbMonth.SetValue(value)
                Case "NotEmpDay"
                    FI_NotEmpDay.SetValue(value)
                Case "CheckInFlag"
                    FI_CheckInFlag.SetValue(value)
                Case "IsBossFlag"
                    FI_IsBossFlag.SetValue(value)
                Case "PassExamFlag"
                    FI_PassExamFlag.SetValue(value)
                Case "LocalHireFlag"
                    FI_LocalHireFlag.SetValue(value)
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
                Case "BanMarkValidDate"
                    FI_BanMarkValidDate.SetValue(value)
                Case "LastLoginTime"
                    FI_LastLoginTime.SetValue(value)
                Case "HostName"
                    FI_HostName.SetValue(value)
                Case "WorkTypeID"
                    FI_WorkTypeID.SetValue(value)
                Case "SecWorkTypeID"
                    FI_SecWorkTypeID.SetValue(value)
                Case "CreateDate"
                    FI_CreateDate.SetValue(value)
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
                Case "UserID"
                    return FI_UserID.Updated
                Case "UserName"
                    return FI_UserName.Updated
                Case "EngName"
                    return FI_EngName.Updated
                Case "IDNo"
                    return FI_IDNo.Updated
                Case "PassportName"
                    return FI_PassportName.Updated
                Case "BirthDate"
                    return FI_BirthDate.Updated
                Case "Sex"
                    return FI_Sex.Updated
                Case "NationID"
                    return FI_NationID.Updated
                Case "IDExpireDate"
                    return FI_IDExpireDate.Updated
                Case "EduID"
                    return FI_EduID.Updated
                Case "Marriage"
                    return FI_Marriage.Updated
                Case "WorkStatus"
                    return FI_WorkStatus.Updated
                Case "EmpType"
                    return FI_EmpType.Updated
                Case "GroupID"
                    return FI_GroupID.Updated
                Case "DeptID"
                    return FI_DeptID.Updated
                Case "OrganID"
                    return FI_OrganID.Updated
                Case "WorkSiteID"
                    return FI_WorkSiteID.Updated
                Case "RankID"
                    return FI_RankID.Updated
                Case "HoldingRankID"
                    return FI_HoldingRankID.Updated
                Case "TitleID"
                    return FI_TitleID.Updated
                Case "PublicTitleID"
                    return FI_PublicTitleID.Updated
                Case "EmpDate"
                    return FI_EmpDate.Updated
                Case "SinopacEmpDate"
                    return FI_SinopacEmpDate.Updated
                Case "QuitDate"
                    return FI_QuitDate.Updated
                Case "ProbDate"
                    return FI_ProbDate.Updated
                Case "ProbMonth"
                    return FI_ProbMonth.Updated
                Case "NotEmpDay"
                    return FI_NotEmpDay.Updated
                Case "CheckInFlag"
                    return FI_CheckInFlag.Updated
                Case "IsBossFlag"
                    return FI_IsBossFlag.Updated
                Case "PassExamFlag"
                    return FI_PassExamFlag.Updated
                Case "LocalHireFlag"
                    return FI_LocalHireFlag.Updated
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
                Case "BanMarkValidDate"
                    return FI_BanMarkValidDate.Updated
                Case "LastLoginTime"
                    return FI_LastLoginTime.Updated
                Case "HostName"
                    return FI_HostName.Updated
                Case "WorkTypeID"
                    return FI_WorkTypeID.Updated
                Case "SecWorkTypeID"
                    return FI_SecWorkTypeID.Updated
                Case "CreateDate"
                    return FI_CreateDate.Updated
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
                Case "UserID"
                    return FI_UserID.CreateUpdateSQL
                Case "UserName"
                    return FI_UserName.CreateUpdateSQL
                Case "EngName"
                    return FI_EngName.CreateUpdateSQL
                Case "IDNo"
                    return FI_IDNo.CreateUpdateSQL
                Case "PassportName"
                    return FI_PassportName.CreateUpdateSQL
                Case "BirthDate"
                    return FI_BirthDate.CreateUpdateSQL
                Case "Sex"
                    return FI_Sex.CreateUpdateSQL
                Case "NationID"
                    return FI_NationID.CreateUpdateSQL
                Case "IDExpireDate"
                    return FI_IDExpireDate.CreateUpdateSQL
                Case "EduID"
                    return FI_EduID.CreateUpdateSQL
                Case "Marriage"
                    return FI_Marriage.CreateUpdateSQL
                Case "WorkStatus"
                    return FI_WorkStatus.CreateUpdateSQL
                Case "EmpType"
                    return FI_EmpType.CreateUpdateSQL
                Case "GroupID"
                    return FI_GroupID.CreateUpdateSQL
                Case "DeptID"
                    return FI_DeptID.CreateUpdateSQL
                Case "OrganID"
                    return FI_OrganID.CreateUpdateSQL
                Case "WorkSiteID"
                    return FI_WorkSiteID.CreateUpdateSQL
                Case "RankID"
                    return FI_RankID.CreateUpdateSQL
                Case "HoldingRankID"
                    return FI_HoldingRankID.CreateUpdateSQL
                Case "TitleID"
                    return FI_TitleID.CreateUpdateSQL
                Case "PublicTitleID"
                    return FI_PublicTitleID.CreateUpdateSQL
                Case "EmpDate"
                    return FI_EmpDate.CreateUpdateSQL
                Case "SinopacEmpDate"
                    return FI_SinopacEmpDate.CreateUpdateSQL
                Case "QuitDate"
                    return FI_QuitDate.CreateUpdateSQL
                Case "ProbDate"
                    return FI_ProbDate.CreateUpdateSQL
                Case "ProbMonth"
                    return FI_ProbMonth.CreateUpdateSQL
                Case "NotEmpDay"
                    return FI_NotEmpDay.CreateUpdateSQL
                Case "CheckInFlag"
                    return FI_CheckInFlag.CreateUpdateSQL
                Case "IsBossFlag"
                    return FI_IsBossFlag.CreateUpdateSQL
                Case "PassExamFlag"
                    return FI_PassExamFlag.CreateUpdateSQL
                Case "LocalHireFlag"
                    return FI_LocalHireFlag.CreateUpdateSQL
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
                Case "BanMarkValidDate"
                    return FI_BanMarkValidDate.CreateUpdateSQL
                Case "LastLoginTime"
                    return FI_LastLoginTime.CreateUpdateSQL
                Case "HostName"
                    return FI_HostName.CreateUpdateSQL
                Case "WorkTypeID"
                    return FI_WorkTypeID.CreateUpdateSQL
                Case "SecWorkTypeID"
                    return FI_SecWorkTypeID.CreateUpdateSQL
                Case "CreateDate"
                    return FI_CreateDate.CreateUpdateSQL
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
            FI_UserID.SetInitValue("")
            FI_UserName.SetInitValue("")
            FI_EngName.SetInitValue("")
            FI_IDNo.SetInitValue("")
            FI_PassportName.SetInitValue("")
            FI_BirthDate.SetInitValue(Convert.ToDateTime("1900/01/01"))
            FI_Sex.SetInitValue("0")
            FI_NationID.SetInitValue("0")
            FI_IDExpireDate.SetInitValue(Convert.ToDateTime("1900/01/01"))
            FI_EduID.SetInitValue("0")
            FI_Marriage.SetInitValue("0")
            FI_WorkStatus.SetInitValue("")
            FI_EmpType.SetInitValue("0")
            FI_GroupID.SetInitValue("")
            FI_DeptID.SetInitValue("")
            FI_OrganID.SetInitValue("")
            FI_WorkSiteID.SetInitValue("0")
            FI_RankID.SetInitValue("")
            FI_HoldingRankID.SetInitValue("")
            FI_TitleID.SetInitValue("")
            FI_PublicTitleID.SetInitValue("")
            FI_EmpDate.SetInitValue(Convert.ToDateTime("1900/01/01"))
            FI_SinopacEmpDate.SetInitValue(Convert.ToDateTime("1900/01/01"))
            FI_QuitDate.SetInitValue(Convert.ToDateTime("1900/01/01"))
            FI_ProbDate.SetInitValue(Convert.ToDateTime("1900/01/01"))
            FI_ProbMonth.SetInitValue(0)
            FI_NotEmpDay.SetInitValue(0)
            FI_CheckInFlag.SetInitValue("0")
            FI_IsBossFlag.SetInitValue("0")
            FI_PassExamFlag.SetInitValue("0")
            FI_LocalHireFlag.SetInitValue("0")
            FI_Password.SetInitValue("NEWUSER")
            FI_PasswordErrorCount.SetInitValue(0)
            FI_ExpireDate.SetInitValue(Convert.ToDateTime("1900/01/01"))
            FI_EMail.SetInitValue("")
            FI_BanMark.SetInitValue("0")
            FI_BanMarkValidDate.SetInitValue(Convert.ToDateTime("1900/01/01"))
            FI_LastLoginTime.SetInitValue(Convert.ToDateTime("1900/01/01"))
            FI_HostName.SetInitValue("")
            FI_WorkTypeID.SetInitValue("")
            FI_SecWorkTypeID.SetInitValue("")
            FI_CreateDate.SetInitValue(DateTime.Now)
            FI_LastChgComp.SetInitValue("")
            FI_LastChgID.SetInitValue("")
            FI_LastChgDate.SetInitValue(DateTime.Now)
        End Sub

        Public Sub New(ByVal dr As System.Data.DataRow)
            FI_CompID.SetInitValue(dr("CompID"))
            FI_UserID.SetInitValue(dr("UserID"))
            FI_UserName.SetInitValue(dr("UserName"))
            FI_EngName.SetInitValue(dr("EngName"))
            FI_IDNo.SetInitValue(dr("IDNo"))
            FI_PassportName.SetInitValue(dr("PassportName"))
            FI_BirthDate.SetInitValue(dr("BirthDate"))
            FI_Sex.SetInitValue(dr("Sex"))
            FI_NationID.SetInitValue(dr("NationID"))
            FI_IDExpireDate.SetInitValue(dr("IDExpireDate"))
            FI_EduID.SetInitValue(dr("EduID"))
            FI_Marriage.SetInitValue(dr("Marriage"))
            FI_WorkStatus.SetInitValue(dr("WorkStatus"))
            FI_EmpType.SetInitValue(dr("EmpType"))
            FI_GroupID.SetInitValue(dr("GroupID"))
            FI_DeptID.SetInitValue(dr("DeptID"))
            FI_OrganID.SetInitValue(dr("OrganID"))
            FI_WorkSiteID.SetInitValue(dr("WorkSiteID"))
            FI_RankID.SetInitValue(dr("RankID"))
            FI_HoldingRankID.SetInitValue(dr("HoldingRankID"))
            FI_TitleID.SetInitValue(dr("TitleID"))
            FI_PublicTitleID.SetInitValue(dr("PublicTitleID"))
            FI_EmpDate.SetInitValue(dr("EmpDate"))
            FI_SinopacEmpDate.SetInitValue(dr("SinopacEmpDate"))
            FI_QuitDate.SetInitValue(dr("QuitDate"))
            FI_ProbDate.SetInitValue(dr("ProbDate"))
            FI_ProbMonth.SetInitValue(dr("ProbMonth"))
            FI_NotEmpDay.SetInitValue(dr("NotEmpDay"))
            FI_CheckInFlag.SetInitValue(dr("CheckInFlag"))
            FI_IsBossFlag.SetInitValue(dr("IsBossFlag"))
            FI_PassExamFlag.SetInitValue(dr("PassExamFlag"))
            FI_LocalHireFlag.SetInitValue(dr("LocalHireFlag"))
            FI_Password.SetInitValue(dr("Password"))
            FI_PasswordErrorCount.SetInitValue(dr("PasswordErrorCount"))
            FI_ExpireDate.SetInitValue(dr("ExpireDate"))
            FI_EMail.SetInitValue(dr("EMail"))
            FI_BanMark.SetInitValue(dr("BanMark"))
            FI_BanMarkValidDate.SetInitValue(dr("BanMarkValidDate"))
            FI_LastLoginTime.SetInitValue(dr("LastLoginTime"))
            FI_HostName.SetInitValue(dr("HostName"))
            FI_WorkTypeID.SetInitValue(dr("WorkTypeID"))
            FI_SecWorkTypeID.SetInitValue(dr("SecWorkTypeID"))
            FI_CreateDate.SetInitValue(dr("CreateDate"))
            FI_LastChgComp.SetInitValue(dr("LastChgComp"))
            FI_LastChgID.SetInitValue(dr("LastChgID"))
            FI_LastChgDate.SetInitValue(dr("LastChgDate"))

            m_LoadFromDataRow = True
        End Sub

        Private Sub ClearUpdated()
            FI_CompID.Updated = False
            FI_UserID.Updated = False
            FI_UserName.Updated = False
            FI_EngName.Updated = False
            FI_IDNo.Updated = False
            FI_PassportName.Updated = False
            FI_BirthDate.Updated = False
            FI_Sex.Updated = False
            FI_NationID.Updated = False
            FI_IDExpireDate.Updated = False
            FI_EduID.Updated = False
            FI_Marriage.Updated = False
            FI_WorkStatus.Updated = False
            FI_EmpType.Updated = False
            FI_GroupID.Updated = False
            FI_DeptID.Updated = False
            FI_OrganID.Updated = False
            FI_WorkSiteID.Updated = False
            FI_RankID.Updated = False
            FI_HoldingRankID.Updated = False
            FI_TitleID.Updated = False
            FI_PublicTitleID.Updated = False
            FI_EmpDate.Updated = False
            FI_SinopacEmpDate.Updated = False
            FI_QuitDate.Updated = False
            FI_ProbDate.Updated = False
            FI_ProbMonth.Updated = False
            FI_NotEmpDay.Updated = False
            FI_CheckInFlag.Updated = False
            FI_IsBossFlag.Updated = False
            FI_PassExamFlag.Updated = False
            FI_LocalHireFlag.Updated = False
            FI_Password.Updated = False
            FI_PasswordErrorCount.Updated = False
            FI_ExpireDate.Updated = False
            FI_EMail.Updated = False
            FI_BanMark.Updated = False
            FI_BanMarkValidDate.Updated = False
            FI_LastLoginTime.Updated = False
            FI_HostName.Updated = False
            FI_WorkTypeID.Updated = False
            FI_SecWorkTypeID.Updated = False
            FI_CreateDate.Updated = False
            FI_LastChgComp.Updated = False
            FI_LastChgID.Updated = False
            FI_LastChgDate.Updated = False
        End Sub

        Public ReadOnly Property CompID As Field(Of String) 
            Get
                Return FI_CompID
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

        Public ReadOnly Property IDNo As Field(Of String) 
            Get
                Return FI_IDNo
            End Get
        End Property

        Public ReadOnly Property PassportName As Field(Of String) 
            Get
                Return FI_PassportName
            End Get
        End Property

        Public ReadOnly Property BirthDate As Field(Of Date) 
            Get
                Return FI_BirthDate
            End Get
        End Property

        Public ReadOnly Property Sex As Field(Of String) 
            Get
                Return FI_Sex
            End Get
        End Property

        Public ReadOnly Property NationID As Field(Of String) 
            Get
                Return FI_NationID
            End Get
        End Property

        Public ReadOnly Property IDExpireDate As Field(Of Date) 
            Get
                Return FI_IDExpireDate
            End Get
        End Property

        Public ReadOnly Property EduID As Field(Of String) 
            Get
                Return FI_EduID
            End Get
        End Property

        Public ReadOnly Property Marriage As Field(Of String) 
            Get
                Return FI_Marriage
            End Get
        End Property

        Public ReadOnly Property WorkStatus As Field(Of String) 
            Get
                Return FI_WorkStatus
            End Get
        End Property

        Public ReadOnly Property EmpType As Field(Of String) 
            Get
                Return FI_EmpType
            End Get
        End Property

        Public ReadOnly Property GroupID As Field(Of String) 
            Get
                Return FI_GroupID
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

        Public ReadOnly Property WorkSiteID As Field(Of String) 
            Get
                Return FI_WorkSiteID
            End Get
        End Property

        Public ReadOnly Property RankID As Field(Of String) 
            Get
                Return FI_RankID
            End Get
        End Property

        Public ReadOnly Property HoldingRankID As Field(Of String) 
            Get
                Return FI_HoldingRankID
            End Get
        End Property

        Public ReadOnly Property TitleID As Field(Of String) 
            Get
                Return FI_TitleID
            End Get
        End Property

        Public ReadOnly Property PublicTitleID As Field(Of String) 
            Get
                Return FI_PublicTitleID
            End Get
        End Property

        Public ReadOnly Property EmpDate As Field(Of Date) 
            Get
                Return FI_EmpDate
            End Get
        End Property

        Public ReadOnly Property SinopacEmpDate As Field(Of Date) 
            Get
                Return FI_SinopacEmpDate
            End Get
        End Property

        Public ReadOnly Property QuitDate As Field(Of Date) 
            Get
                Return FI_QuitDate
            End Get
        End Property

        Public ReadOnly Property ProbDate As Field(Of Date) 
            Get
                Return FI_ProbDate
            End Get
        End Property

        Public ReadOnly Property ProbMonth As Field(Of Integer) 
            Get
                Return FI_ProbMonth
            End Get
        End Property

        Public ReadOnly Property NotEmpDay As Field(Of Integer) 
            Get
                Return FI_NotEmpDay
            End Get
        End Property

        Public ReadOnly Property CheckInFlag As Field(Of String) 
            Get
                Return FI_CheckInFlag
            End Get
        End Property

        Public ReadOnly Property IsBossFlag As Field(Of String) 
            Get
                Return FI_IsBossFlag
            End Get
        End Property

        Public ReadOnly Property PassExamFlag As Field(Of String) 
            Get
                Return FI_PassExamFlag
            End Get
        End Property

        Public ReadOnly Property LocalHireFlag As Field(Of String) 
            Get
                Return FI_LocalHireFlag
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

        Public ReadOnly Property BanMarkValidDate As Field(Of Date) 
            Get
                Return FI_BanMarkValidDate
            End Get
        End Property

        Public ReadOnly Property LastLoginTime As Field(Of Date) 
            Get
                Return FI_LastLoginTime
            End Get
        End Property

        Public ReadOnly Property HostName As Field(Of String) 
            Get
                Return FI_HostName
            End Get
        End Property

        Public ReadOnly Property WorkTypeID As Field(Of String) 
            Get
                Return FI_WorkTypeID
            End Get
        End Property

        Public ReadOnly Property SecWorkTypeID As Field(Of String) 
            Get
                Return FI_SecWorkTypeID
            End Get
        End Property

        Public ReadOnly Property CreateDate As Field(Of Date) 
            Get
                Return FI_CreateDate
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
        Public Function DeleteRowByPrimaryKey(ByVal SC_UserRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Delete From SC_User")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And UserID = @UserID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, SC_UserRow.CompID.Value)
            db.AddInParameter(dbcmd, "@UserID", DbType.String, SC_UserRow.UserID.Value)

            return db.ExecuteNonQuery(dbcmd)
        End Function

        public Function DeleteRowByPrimaryKey(ByVal SC_UserRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Delete From SC_User")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And UserID = @UserID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, SC_UserRow.CompID.Value)
            db.AddInParameter(dbcmd, "@UserID", DbType.String, SC_UserRow.UserID.Value)

            return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function DeleteRowByPrimaryKey(ByVal SC_UserRow As Row()) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Delete From SC_User")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And UserID = @UserID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            using cn As DbConnection = db.CreateConnection()
                cn.Open()
                Dim tran As DbTransaction = cn.BeginTransaction()
                Dim inTrans As Boolean = True

                Try
                    For Each r As Row In SC_UserRow
                        dbcmd.Parameters.Clear()
                        db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
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

        Public Function DeleteRowByPrimaryKey(ByVal SC_UserRow As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Delete From SC_User")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And UserID = @UserID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())

            For Each r As Row In SC_UserRow
                dbcmd.Parameters.Clear()
                db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                db.AddInParameter(dbcmd, "@UserID", DbType.String, r.UserID.Value)

                intRowsAffected += db.ExecuteNonQuery(dbcmd, tran)
            Next
            Return intRowsAffected
        End Function

        Public Function QueryByKey(ByVal SC_UserRow As Row) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From SC_User")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And UserID = @UserID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, SC_UserRow.CompID.Value)
            db.AddInParameter(dbcmd, "@UserID", DbType.String, SC_UserRow.UserID.Value)

            Return db.ExecuteDataSet(dbcmd)
        End Function

        Public Function QueryByKey(SC_UserRow As Row, tran As DbTransaction) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From SC_User")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And UserID = @UserID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, SC_UserRow.CompID.Value)
            db.AddInParameter(dbcmd, "@UserID", DbType.String, SC_UserRow.UserID.Value)

            Return db.ExecuteDataSet(dbcmd, tran)
        End Function

        Public Function Update(ByVal SC_UserRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim strDot As String = String.Empty

            strSQL.AppendLine("Update SC_User Set")
            For i As Integer = 0 To SC_UserRow.FieldNames.Length - 1
                If Not SC_UserRow.IsIdentityField(SC_UserRow.FieldNames(i)) AndAlso SC_UserRow.IsUpdated(SC_UserRow.FieldNames(i)) AndAlso SC_UserRow.CreateUpdateSQL(SC_UserRow.FieldNames(i)) Then
                    strSQL.AppendLine(string.Format("{0}{1} = @{1}", strDot, SC_UserRow.FieldNames(i)))
                    strDot = ","
                End If
            Next
            If strDot = String.Empty Then Throw New Exception("未異動資料欄位！")
            strSQL.AppendLine("Where CompID = @PKCompID")
            strSQL.AppendLine("And UserID = @PKUserID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            If SC_UserRow.CompID.Updated Then db.AddInParameter(dbcmd, "@CompID", DbType.String, SC_UserRow.CompID.Value)
            If SC_UserRow.UserID.Updated Then db.AddInParameter(dbcmd, "@UserID", DbType.String, SC_UserRow.UserID.Value)
            If SC_UserRow.UserName.Updated Then db.AddInParameter(dbcmd, "@UserName", DbType.String, SC_UserRow.UserName.Value)
            If SC_UserRow.EngName.Updated Then db.AddInParameter(dbcmd, "@EngName", DbType.String, SC_UserRow.EngName.Value)
            If SC_UserRow.IDNo.Updated Then db.AddInParameter(dbcmd, "@IDNo", DbType.String, SC_UserRow.IDNo.Value)
            If SC_UserRow.PassportName.Updated Then db.AddInParameter(dbcmd, "@PassportName", DbType.String, SC_UserRow.PassportName.Value)
            If SC_UserRow.BirthDate.Updated Then db.AddInParameter(dbcmd, "@BirthDate", DbType.Date, IIf(IsDateTimeNull(SC_UserRow.BirthDate.Value), Convert.ToDateTime("1900/1/1"), SC_UserRow.BirthDate.Value))
            If SC_UserRow.Sex.Updated Then db.AddInParameter(dbcmd, "@Sex", DbType.String, SC_UserRow.Sex.Value)
            If SC_UserRow.NationID.Updated Then db.AddInParameter(dbcmd, "@NationID", DbType.String, SC_UserRow.NationID.Value)
            If SC_UserRow.IDExpireDate.Updated Then db.AddInParameter(dbcmd, "@IDExpireDate", DbType.Date, IIf(IsDateTimeNull(SC_UserRow.IDExpireDate.Value), Convert.ToDateTime("1900/1/1"), SC_UserRow.IDExpireDate.Value))
            If SC_UserRow.EduID.Updated Then db.AddInParameter(dbcmd, "@EduID", DbType.String, SC_UserRow.EduID.Value)
            If SC_UserRow.Marriage.Updated Then db.AddInParameter(dbcmd, "@Marriage", DbType.String, SC_UserRow.Marriage.Value)
            If SC_UserRow.WorkStatus.Updated Then db.AddInParameter(dbcmd, "@WorkStatus", DbType.String, SC_UserRow.WorkStatus.Value)
            If SC_UserRow.EmpType.Updated Then db.AddInParameter(dbcmd, "@EmpType", DbType.String, SC_UserRow.EmpType.Value)
            If SC_UserRow.GroupID.Updated Then db.AddInParameter(dbcmd, "@GroupID", DbType.String, SC_UserRow.GroupID.Value)
            If SC_UserRow.DeptID.Updated Then db.AddInParameter(dbcmd, "@DeptID", DbType.String, SC_UserRow.DeptID.Value)
            If SC_UserRow.OrganID.Updated Then db.AddInParameter(dbcmd, "@OrganID", DbType.String, SC_UserRow.OrganID.Value)
            If SC_UserRow.WorkSiteID.Updated Then db.AddInParameter(dbcmd, "@WorkSiteID", DbType.String, SC_UserRow.WorkSiteID.Value)
            If SC_UserRow.RankID.Updated Then db.AddInParameter(dbcmd, "@RankID", DbType.String, SC_UserRow.RankID.Value)
            If SC_UserRow.HoldingRankID.Updated Then db.AddInParameter(dbcmd, "@HoldingRankID", DbType.String, SC_UserRow.HoldingRankID.Value)
            If SC_UserRow.TitleID.Updated Then db.AddInParameter(dbcmd, "@TitleID", DbType.String, SC_UserRow.TitleID.Value)
            If SC_UserRow.PublicTitleID.Updated Then db.AddInParameter(dbcmd, "@PublicTitleID", DbType.String, SC_UserRow.PublicTitleID.Value)
            If SC_UserRow.EmpDate.Updated Then db.AddInParameter(dbcmd, "@EmpDate", DbType.Date, IIf(IsDateTimeNull(SC_UserRow.EmpDate.Value), Convert.ToDateTime("1900/1/1"), SC_UserRow.EmpDate.Value))
            If SC_UserRow.SinopacEmpDate.Updated Then db.AddInParameter(dbcmd, "@SinopacEmpDate", DbType.Date, IIf(IsDateTimeNull(SC_UserRow.SinopacEmpDate.Value), Convert.ToDateTime("1900/1/1"), SC_UserRow.SinopacEmpDate.Value))
            If SC_UserRow.QuitDate.Updated Then db.AddInParameter(dbcmd, "@QuitDate", DbType.Date, IIf(IsDateTimeNull(SC_UserRow.QuitDate.Value), Convert.ToDateTime("1900/1/1"), SC_UserRow.QuitDate.Value))
            If SC_UserRow.ProbDate.Updated Then db.AddInParameter(dbcmd, "@ProbDate", DbType.Date, IIf(IsDateTimeNull(SC_UserRow.ProbDate.Value), Convert.ToDateTime("1900/1/1"), SC_UserRow.ProbDate.Value))
            If SC_UserRow.ProbMonth.Updated Then db.AddInParameter(dbcmd, "@ProbMonth", DbType.Int32, SC_UserRow.ProbMonth.Value)
            If SC_UserRow.NotEmpDay.Updated Then db.AddInParameter(dbcmd, "@NotEmpDay", DbType.Int32, SC_UserRow.NotEmpDay.Value)
            If SC_UserRow.CheckInFlag.Updated Then db.AddInParameter(dbcmd, "@CheckInFlag", DbType.String, SC_UserRow.CheckInFlag.Value)
            If SC_UserRow.IsBossFlag.Updated Then db.AddInParameter(dbcmd, "@IsBossFlag", DbType.String, SC_UserRow.IsBossFlag.Value)
            If SC_UserRow.PassExamFlag.Updated Then db.AddInParameter(dbcmd, "@PassExamFlag", DbType.String, SC_UserRow.PassExamFlag.Value)
            If SC_UserRow.LocalHireFlag.Updated Then db.AddInParameter(dbcmd, "@LocalHireFlag", DbType.String, SC_UserRow.LocalHireFlag.Value)
            If SC_UserRow.Password.Updated Then db.AddInParameter(dbcmd, "@Password", DbType.String, SC_UserRow.Password.Value)
            If SC_UserRow.PasswordErrorCount.Updated Then db.AddInParameter(dbcmd, "@PasswordErrorCount", DbType.Int32, SC_UserRow.PasswordErrorCount.Value)
            If SC_UserRow.ExpireDate.Updated Then db.AddInParameter(dbcmd, "@ExpireDate", DbType.Date, IIf(IsDateTimeNull(SC_UserRow.ExpireDate.Value), Convert.ToDateTime("1900/1/1"), SC_UserRow.ExpireDate.Value))
            If SC_UserRow.EMail.Updated Then db.AddInParameter(dbcmd, "@EMail", DbType.String, SC_UserRow.EMail.Value)
            If SC_UserRow.BanMark.Updated Then db.AddInParameter(dbcmd, "@BanMark", DbType.String, SC_UserRow.BanMark.Value)
            If SC_UserRow.BanMarkValidDate.Updated Then db.AddInParameter(dbcmd, "@BanMarkValidDate", DbType.Date, IIf(IsDateTimeNull(SC_UserRow.BanMarkValidDate.Value), Convert.ToDateTime("1900/1/1"), SC_UserRow.BanMarkValidDate.Value))
            If SC_UserRow.LastLoginTime.Updated Then db.AddInParameter(dbcmd, "@LastLoginTime", DbType.Date, IIf(IsDateTimeNull(SC_UserRow.LastLoginTime.Value), Convert.ToDateTime("1900/1/1"), SC_UserRow.LastLoginTime.Value))
            If SC_UserRow.HostName.Updated Then db.AddInParameter(dbcmd, "@HostName", DbType.String, SC_UserRow.HostName.Value)
            If SC_UserRow.WorkTypeID.Updated Then db.AddInParameter(dbcmd, "@WorkTypeID", DbType.String, SC_UserRow.WorkTypeID.Value)
            If SC_UserRow.SecWorkTypeID.Updated Then db.AddInParameter(dbcmd, "@SecWorkTypeID", DbType.String, SC_UserRow.SecWorkTypeID.Value)
            If SC_UserRow.CreateDate.Updated Then db.AddInParameter(dbcmd, "@CreateDate", DbType.Date, IIf(IsDateTimeNull(SC_UserRow.CreateDate.Value), Convert.ToDateTime("1900/1/1"), SC_UserRow.CreateDate.Value))
            If SC_UserRow.LastChgComp.Updated Then db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, SC_UserRow.LastChgComp.Value)
            If SC_UserRow.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, SC_UserRow.LastChgID.Value)
            If SC_UserRow.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(SC_UserRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), SC_UserRow.LastChgDate.Value))
            db.AddInParameter(dbcmd, "@PKCompID", DbType.String, IIf(SC_UserRow.LoadFromDataRow, SC_UserRow.CompID.OldValue, SC_UserRow.CompID.Value))
            db.AddInParameter(dbcmd, "@PKUserID", DbType.String, IIf(SC_UserRow.LoadFromDataRow, SC_UserRow.UserID.OldValue, SC_UserRow.UserID.Value))

            Return db.ExecuteNonQuery(dbcmd)
        End Function

        Public Function Update(ByVal SC_UserRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim strDot As String = String.Empty

            strSQL.AppendLine("Update SC_User Set")
            For i As Integer = 0 To SC_UserRow.FieldNames.Length - 1
                If Not SC_UserRow.IsIdentityField(SC_UserRow.FieldNames(i)) AndAlso SC_UserRow.IsUpdated(SC_UserRow.FieldNames(i)) AndAlso SC_UserRow.CreateUpdateSQL(SC_UserRow.FieldNames(i)) Then
                    strSQL.AppendLine(string.Format("{0}{1} = @{1}", strDot, SC_UserRow.FieldNames(i)))
                    strDot = ","
                End If
            Next 
            If strDot = String.Empty Then Throw New Exception("未異動資料欄位！")
            strSQL.AppendLine("Where CompID = @PKCompID")
            strSQL.AppendLine("And UserID = @PKUserID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            If SC_UserRow.CompID.Updated Then db.AddInParameter(dbcmd, "@CompID", DbType.String, SC_UserRow.CompID.Value)
            If SC_UserRow.UserID.Updated Then db.AddInParameter(dbcmd, "@UserID", DbType.String, SC_UserRow.UserID.Value)
            If SC_UserRow.UserName.Updated Then db.AddInParameter(dbcmd, "@UserName", DbType.String, SC_UserRow.UserName.Value)
            If SC_UserRow.EngName.Updated Then db.AddInParameter(dbcmd, "@EngName", DbType.String, SC_UserRow.EngName.Value)
            If SC_UserRow.IDNo.Updated Then db.AddInParameter(dbcmd, "@IDNo", DbType.String, SC_UserRow.IDNo.Value)
            If SC_UserRow.PassportName.Updated Then db.AddInParameter(dbcmd, "@PassportName", DbType.String, SC_UserRow.PassportName.Value)
            If SC_UserRow.BirthDate.Updated Then db.AddInParameter(dbcmd, "@BirthDate", DbType.Date, IIf(IsDateTimeNull(SC_UserRow.BirthDate.Value), Convert.ToDateTime("1900/1/1"), SC_UserRow.BirthDate.Value))
            If SC_UserRow.Sex.Updated Then db.AddInParameter(dbcmd, "@Sex", DbType.String, SC_UserRow.Sex.Value)
            If SC_UserRow.NationID.Updated Then db.AddInParameter(dbcmd, "@NationID", DbType.String, SC_UserRow.NationID.Value)
            If SC_UserRow.IDExpireDate.Updated Then db.AddInParameter(dbcmd, "@IDExpireDate", DbType.Date, IIf(IsDateTimeNull(SC_UserRow.IDExpireDate.Value), Convert.ToDateTime("1900/1/1"), SC_UserRow.IDExpireDate.Value))
            If SC_UserRow.EduID.Updated Then db.AddInParameter(dbcmd, "@EduID", DbType.String, SC_UserRow.EduID.Value)
            If SC_UserRow.Marriage.Updated Then db.AddInParameter(dbcmd, "@Marriage", DbType.String, SC_UserRow.Marriage.Value)
            If SC_UserRow.WorkStatus.Updated Then db.AddInParameter(dbcmd, "@WorkStatus", DbType.String, SC_UserRow.WorkStatus.Value)
            If SC_UserRow.EmpType.Updated Then db.AddInParameter(dbcmd, "@EmpType", DbType.String, SC_UserRow.EmpType.Value)
            If SC_UserRow.GroupID.Updated Then db.AddInParameter(dbcmd, "@GroupID", DbType.String, SC_UserRow.GroupID.Value)
            If SC_UserRow.DeptID.Updated Then db.AddInParameter(dbcmd, "@DeptID", DbType.String, SC_UserRow.DeptID.Value)
            If SC_UserRow.OrganID.Updated Then db.AddInParameter(dbcmd, "@OrganID", DbType.String, SC_UserRow.OrganID.Value)
            If SC_UserRow.WorkSiteID.Updated Then db.AddInParameter(dbcmd, "@WorkSiteID", DbType.String, SC_UserRow.WorkSiteID.Value)
            If SC_UserRow.RankID.Updated Then db.AddInParameter(dbcmd, "@RankID", DbType.String, SC_UserRow.RankID.Value)
            If SC_UserRow.HoldingRankID.Updated Then db.AddInParameter(dbcmd, "@HoldingRankID", DbType.String, SC_UserRow.HoldingRankID.Value)
            If SC_UserRow.TitleID.Updated Then db.AddInParameter(dbcmd, "@TitleID", DbType.String, SC_UserRow.TitleID.Value)
            If SC_UserRow.PublicTitleID.Updated Then db.AddInParameter(dbcmd, "@PublicTitleID", DbType.String, SC_UserRow.PublicTitleID.Value)
            If SC_UserRow.EmpDate.Updated Then db.AddInParameter(dbcmd, "@EmpDate", DbType.Date, IIf(IsDateTimeNull(SC_UserRow.EmpDate.Value), Convert.ToDateTime("1900/1/1"), SC_UserRow.EmpDate.Value))
            If SC_UserRow.SinopacEmpDate.Updated Then db.AddInParameter(dbcmd, "@SinopacEmpDate", DbType.Date, IIf(IsDateTimeNull(SC_UserRow.SinopacEmpDate.Value), Convert.ToDateTime("1900/1/1"), SC_UserRow.SinopacEmpDate.Value))
            If SC_UserRow.QuitDate.Updated Then db.AddInParameter(dbcmd, "@QuitDate", DbType.Date, IIf(IsDateTimeNull(SC_UserRow.QuitDate.Value), Convert.ToDateTime("1900/1/1"), SC_UserRow.QuitDate.Value))
            If SC_UserRow.ProbDate.Updated Then db.AddInParameter(dbcmd, "@ProbDate", DbType.Date, IIf(IsDateTimeNull(SC_UserRow.ProbDate.Value), Convert.ToDateTime("1900/1/1"), SC_UserRow.ProbDate.Value))
            If SC_UserRow.ProbMonth.Updated Then db.AddInParameter(dbcmd, "@ProbMonth", DbType.Int32, SC_UserRow.ProbMonth.Value)
            If SC_UserRow.NotEmpDay.Updated Then db.AddInParameter(dbcmd, "@NotEmpDay", DbType.Int32, SC_UserRow.NotEmpDay.Value)
            If SC_UserRow.CheckInFlag.Updated Then db.AddInParameter(dbcmd, "@CheckInFlag", DbType.String, SC_UserRow.CheckInFlag.Value)
            If SC_UserRow.IsBossFlag.Updated Then db.AddInParameter(dbcmd, "@IsBossFlag", DbType.String, SC_UserRow.IsBossFlag.Value)
            If SC_UserRow.PassExamFlag.Updated Then db.AddInParameter(dbcmd, "@PassExamFlag", DbType.String, SC_UserRow.PassExamFlag.Value)
            If SC_UserRow.LocalHireFlag.Updated Then db.AddInParameter(dbcmd, "@LocalHireFlag", DbType.String, SC_UserRow.LocalHireFlag.Value)
            If SC_UserRow.Password.Updated Then db.AddInParameter(dbcmd, "@Password", DbType.String, SC_UserRow.Password.Value)
            If SC_UserRow.PasswordErrorCount.Updated Then db.AddInParameter(dbcmd, "@PasswordErrorCount", DbType.Int32, SC_UserRow.PasswordErrorCount.Value)
            If SC_UserRow.ExpireDate.Updated Then db.AddInParameter(dbcmd, "@ExpireDate", DbType.Date, IIf(IsDateTimeNull(SC_UserRow.ExpireDate.Value), Convert.ToDateTime("1900/1/1"), SC_UserRow.ExpireDate.Value))
            If SC_UserRow.EMail.Updated Then db.AddInParameter(dbcmd, "@EMail", DbType.String, SC_UserRow.EMail.Value)
            If SC_UserRow.BanMark.Updated Then db.AddInParameter(dbcmd, "@BanMark", DbType.String, SC_UserRow.BanMark.Value)
            If SC_UserRow.BanMarkValidDate.Updated Then db.AddInParameter(dbcmd, "@BanMarkValidDate", DbType.Date, IIf(IsDateTimeNull(SC_UserRow.BanMarkValidDate.Value), Convert.ToDateTime("1900/1/1"), SC_UserRow.BanMarkValidDate.Value))
            If SC_UserRow.LastLoginTime.Updated Then db.AddInParameter(dbcmd, "@LastLoginTime", DbType.Date, IIf(IsDateTimeNull(SC_UserRow.LastLoginTime.Value), Convert.ToDateTime("1900/1/1"), SC_UserRow.LastLoginTime.Value))
            If SC_UserRow.HostName.Updated Then db.AddInParameter(dbcmd, "@HostName", DbType.String, SC_UserRow.HostName.Value)
            If SC_UserRow.WorkTypeID.Updated Then db.AddInParameter(dbcmd, "@WorkTypeID", DbType.String, SC_UserRow.WorkTypeID.Value)
            If SC_UserRow.SecWorkTypeID.Updated Then db.AddInParameter(dbcmd, "@SecWorkTypeID", DbType.String, SC_UserRow.SecWorkTypeID.Value)
            If SC_UserRow.CreateDate.Updated Then db.AddInParameter(dbcmd, "@CreateDate", DbType.Date, IIf(IsDateTimeNull(SC_UserRow.CreateDate.Value), Convert.ToDateTime("1900/1/1"), SC_UserRow.CreateDate.Value))
            If SC_UserRow.LastChgComp.Updated Then db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, SC_UserRow.LastChgComp.Value)
            If SC_UserRow.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, SC_UserRow.LastChgID.Value)
            If SC_UserRow.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(SC_UserRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), SC_UserRow.LastChgDate.Value))
            db.AddInParameter(dbcmd, "@PKCompID", DbType.String, IIf(SC_UserRow.LoadFromDataRow, SC_UserRow.CompID.OldValue, SC_UserRow.CompID.Value))
            db.AddInParameter(dbcmd, "@PKUserID", DbType.String, IIf(SC_UserRow.LoadFromDataRow, SC_UserRow.UserID.OldValue, SC_UserRow.UserID.Value))

            Return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function Update(ByVal SC_UserRow As Row()) As Integer
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
                    For Each r As Row In SC_UserRow
                        strDot = String.Empty
                        If strSQL.Length > 0 Then strSQL.Remove(0, strSQL.Length)
                        strSQL.AppendLine("Update SC_User Set")
                        For i As Integer = 0 To r.FieldNames.Length - 1
                            If Not r.IsIdentityField(r.FieldNames(i)) AndAlso r.IsUpdated(r.FieldNames(i)) AndAlso r.CreateUpdateSQL(r.FieldNames(i)) Then
                                strSQL.AppendLine(String.Format("{0}{1} = @{1}", strDot, r.FieldNames(i)))
                                strDot = ","
                            End If
                        Next
                        If strDot = String.Empty Then Continue For
                        strSQL.AppendLine("Where CompID = @PKCompID")
                        strSQL.AppendLine("And UserID = @PKUserID")

                        dbcmd = db.GetSqlStringCommand(strSQL.ToString())
                        If r.CompID.Updated Then db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                        If r.UserID.Updated Then db.AddInParameter(dbcmd, "@UserID", DbType.String, r.UserID.Value)
                        If r.UserName.Updated Then db.AddInParameter(dbcmd, "@UserName", DbType.String, r.UserName.Value)
                        If r.EngName.Updated Then db.AddInParameter(dbcmd, "@EngName", DbType.String, r.EngName.Value)
                        If r.IDNo.Updated Then db.AddInParameter(dbcmd, "@IDNo", DbType.String, r.IDNo.Value)
                        If r.PassportName.Updated Then db.AddInParameter(dbcmd, "@PassportName", DbType.String, r.PassportName.Value)
                        If r.BirthDate.Updated Then db.AddInParameter(dbcmd, "@BirthDate", DbType.Date, IIf(IsDateTimeNull(r.BirthDate.Value), Convert.ToDateTime("1900/1/1"), r.BirthDate.Value))
                        If r.Sex.Updated Then db.AddInParameter(dbcmd, "@Sex", DbType.String, r.Sex.Value)
                        If r.NationID.Updated Then db.AddInParameter(dbcmd, "@NationID", DbType.String, r.NationID.Value)
                        If r.IDExpireDate.Updated Then db.AddInParameter(dbcmd, "@IDExpireDate", DbType.Date, IIf(IsDateTimeNull(r.IDExpireDate.Value), Convert.ToDateTime("1900/1/1"), r.IDExpireDate.Value))
                        If r.EduID.Updated Then db.AddInParameter(dbcmd, "@EduID", DbType.String, r.EduID.Value)
                        If r.Marriage.Updated Then db.AddInParameter(dbcmd, "@Marriage", DbType.String, r.Marriage.Value)
                        If r.WorkStatus.Updated Then db.AddInParameter(dbcmd, "@WorkStatus", DbType.String, r.WorkStatus.Value)
                        If r.EmpType.Updated Then db.AddInParameter(dbcmd, "@EmpType", DbType.String, r.EmpType.Value)
                        If r.GroupID.Updated Then db.AddInParameter(dbcmd, "@GroupID", DbType.String, r.GroupID.Value)
                        If r.DeptID.Updated Then db.AddInParameter(dbcmd, "@DeptID", DbType.String, r.DeptID.Value)
                        If r.OrganID.Updated Then db.AddInParameter(dbcmd, "@OrganID", DbType.String, r.OrganID.Value)
                        If r.WorkSiteID.Updated Then db.AddInParameter(dbcmd, "@WorkSiteID", DbType.String, r.WorkSiteID.Value)
                        If r.RankID.Updated Then db.AddInParameter(dbcmd, "@RankID", DbType.String, r.RankID.Value)
                        If r.HoldingRankID.Updated Then db.AddInParameter(dbcmd, "@HoldingRankID", DbType.String, r.HoldingRankID.Value)
                        If r.TitleID.Updated Then db.AddInParameter(dbcmd, "@TitleID", DbType.String, r.TitleID.Value)
                        If r.PublicTitleID.Updated Then db.AddInParameter(dbcmd, "@PublicTitleID", DbType.String, r.PublicTitleID.Value)
                        If r.EmpDate.Updated Then db.AddInParameter(dbcmd, "@EmpDate", DbType.Date, IIf(IsDateTimeNull(r.EmpDate.Value), Convert.ToDateTime("1900/1/1"), r.EmpDate.Value))
                        If r.SinopacEmpDate.Updated Then db.AddInParameter(dbcmd, "@SinopacEmpDate", DbType.Date, IIf(IsDateTimeNull(r.SinopacEmpDate.Value), Convert.ToDateTime("1900/1/1"), r.SinopacEmpDate.Value))
                        If r.QuitDate.Updated Then db.AddInParameter(dbcmd, "@QuitDate", DbType.Date, IIf(IsDateTimeNull(r.QuitDate.Value), Convert.ToDateTime("1900/1/1"), r.QuitDate.Value))
                        If r.ProbDate.Updated Then db.AddInParameter(dbcmd, "@ProbDate", DbType.Date, IIf(IsDateTimeNull(r.ProbDate.Value), Convert.ToDateTime("1900/1/1"), r.ProbDate.Value))
                        If r.ProbMonth.Updated Then db.AddInParameter(dbcmd, "@ProbMonth", DbType.Int32, r.ProbMonth.Value)
                        If r.NotEmpDay.Updated Then db.AddInParameter(dbcmd, "@NotEmpDay", DbType.Int32, r.NotEmpDay.Value)
                        If r.CheckInFlag.Updated Then db.AddInParameter(dbcmd, "@CheckInFlag", DbType.String, r.CheckInFlag.Value)
                        If r.IsBossFlag.Updated Then db.AddInParameter(dbcmd, "@IsBossFlag", DbType.String, r.IsBossFlag.Value)
                        If r.PassExamFlag.Updated Then db.AddInParameter(dbcmd, "@PassExamFlag", DbType.String, r.PassExamFlag.Value)
                        If r.LocalHireFlag.Updated Then db.AddInParameter(dbcmd, "@LocalHireFlag", DbType.String, r.LocalHireFlag.Value)
                        If r.Password.Updated Then db.AddInParameter(dbcmd, "@Password", DbType.String, r.Password.Value)
                        If r.PasswordErrorCount.Updated Then db.AddInParameter(dbcmd, "@PasswordErrorCount", DbType.Int32, r.PasswordErrorCount.Value)
                        If r.ExpireDate.Updated Then db.AddInParameter(dbcmd, "@ExpireDate", DbType.Date, IIf(IsDateTimeNull(r.ExpireDate.Value), Convert.ToDateTime("1900/1/1"), r.ExpireDate.Value))
                        If r.EMail.Updated Then db.AddInParameter(dbcmd, "@EMail", DbType.String, r.EMail.Value)
                        If r.BanMark.Updated Then db.AddInParameter(dbcmd, "@BanMark", DbType.String, r.BanMark.Value)
                        If r.BanMarkValidDate.Updated Then db.AddInParameter(dbcmd, "@BanMarkValidDate", DbType.Date, IIf(IsDateTimeNull(r.BanMarkValidDate.Value), Convert.ToDateTime("1900/1/1"), r.BanMarkValidDate.Value))
                        If r.LastLoginTime.Updated Then db.AddInParameter(dbcmd, "@LastLoginTime", DbType.Date, IIf(IsDateTimeNull(r.LastLoginTime.Value), Convert.ToDateTime("1900/1/1"), r.LastLoginTime.Value))
                        If r.HostName.Updated Then db.AddInParameter(dbcmd, "@HostName", DbType.String, r.HostName.Value)
                        If r.WorkTypeID.Updated Then db.AddInParameter(dbcmd, "@WorkTypeID", DbType.String, r.WorkTypeID.Value)
                        If r.SecWorkTypeID.Updated Then db.AddInParameter(dbcmd, "@SecWorkTypeID", DbType.String, r.SecWorkTypeID.Value)
                        If r.CreateDate.Updated Then db.AddInParameter(dbcmd, "@CreateDate", DbType.Date, IIf(IsDateTimeNull(r.CreateDate.Value), Convert.ToDateTime("1900/1/1"), r.CreateDate.Value))
                        If r.LastChgComp.Updated Then db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, r.LastChgComp.Value)
                        If r.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, r.LastChgID.Value)
                        If r.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(r.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), r.LastChgDate.Value))
                        db.AddInParameter(dbcmd, "@PKCompID", DbType.String, IIf(r.LoadFromDataRow, r.CompID.OldValue, r.CompID.Value))
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

        Public Function Update(ByVal SC_UserRow As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0
            Dim strDot As String

            For Each r As Row In SC_UserRow
                strDot = String.Empty
                If strSQL.Length > 0 Then strSQL.Remove(0, strSQL.Length)
                strSQL.AppendLine("Update SC_User Set")
                For i As Integer = 0 To r.FieldNames.Length - 1
                    If Not r.IsIdentityField(r.FieldNames(i)) AndAlso r.IsUpdated(r.FieldNames(i)) AndAlso r.CreateUpdateSQL(r.FieldNames(i))
                        strSQL.AppendLine(String.Format("{0}{1} = @{1}", strDot, r.FieldNames(i)))
                        strDot = ","
                    End If
                Next
                If strDot = String.Empty Then Continue For
                strSQL.AppendLine("Where CompID = @PKCompID")
                strSQL.AppendLine("And UserID = @PKUserID")

                dbcmd = db.GetSqlStringCommand(strSQL.ToString())
                If r.CompID.Updated Then db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                If r.UserID.Updated Then db.AddInParameter(dbcmd, "@UserID", DbType.String, r.UserID.Value)
                If r.UserName.Updated Then db.AddInParameter(dbcmd, "@UserName", DbType.String, r.UserName.Value)
                If r.EngName.Updated Then db.AddInParameter(dbcmd, "@EngName", DbType.String, r.EngName.Value)
                If r.IDNo.Updated Then db.AddInParameter(dbcmd, "@IDNo", DbType.String, r.IDNo.Value)
                If r.PassportName.Updated Then db.AddInParameter(dbcmd, "@PassportName", DbType.String, r.PassportName.Value)
                If r.BirthDate.Updated Then db.AddInParameter(dbcmd, "@BirthDate", DbType.Date, IIf(IsDateTimeNull(r.BirthDate.Value), Convert.ToDateTime("1900/1/1"), r.BirthDate.Value))
                If r.Sex.Updated Then db.AddInParameter(dbcmd, "@Sex", DbType.String, r.Sex.Value)
                If r.NationID.Updated Then db.AddInParameter(dbcmd, "@NationID", DbType.String, r.NationID.Value)
                If r.IDExpireDate.Updated Then db.AddInParameter(dbcmd, "@IDExpireDate", DbType.Date, IIf(IsDateTimeNull(r.IDExpireDate.Value), Convert.ToDateTime("1900/1/1"), r.IDExpireDate.Value))
                If r.EduID.Updated Then db.AddInParameter(dbcmd, "@EduID", DbType.String, r.EduID.Value)
                If r.Marriage.Updated Then db.AddInParameter(dbcmd, "@Marriage", DbType.String, r.Marriage.Value)
                If r.WorkStatus.Updated Then db.AddInParameter(dbcmd, "@WorkStatus", DbType.String, r.WorkStatus.Value)
                If r.EmpType.Updated Then db.AddInParameter(dbcmd, "@EmpType", DbType.String, r.EmpType.Value)
                If r.GroupID.Updated Then db.AddInParameter(dbcmd, "@GroupID", DbType.String, r.GroupID.Value)
                If r.DeptID.Updated Then db.AddInParameter(dbcmd, "@DeptID", DbType.String, r.DeptID.Value)
                If r.OrganID.Updated Then db.AddInParameter(dbcmd, "@OrganID", DbType.String, r.OrganID.Value)
                If r.WorkSiteID.Updated Then db.AddInParameter(dbcmd, "@WorkSiteID", DbType.String, r.WorkSiteID.Value)
                If r.RankID.Updated Then db.AddInParameter(dbcmd, "@RankID", DbType.String, r.RankID.Value)
                If r.HoldingRankID.Updated Then db.AddInParameter(dbcmd, "@HoldingRankID", DbType.String, r.HoldingRankID.Value)
                If r.TitleID.Updated Then db.AddInParameter(dbcmd, "@TitleID", DbType.String, r.TitleID.Value)
                If r.PublicTitleID.Updated Then db.AddInParameter(dbcmd, "@PublicTitleID", DbType.String, r.PublicTitleID.Value)
                If r.EmpDate.Updated Then db.AddInParameter(dbcmd, "@EmpDate", DbType.Date, IIf(IsDateTimeNull(r.EmpDate.Value), Convert.ToDateTime("1900/1/1"), r.EmpDate.Value))
                If r.SinopacEmpDate.Updated Then db.AddInParameter(dbcmd, "@SinopacEmpDate", DbType.Date, IIf(IsDateTimeNull(r.SinopacEmpDate.Value), Convert.ToDateTime("1900/1/1"), r.SinopacEmpDate.Value))
                If r.QuitDate.Updated Then db.AddInParameter(dbcmd, "@QuitDate", DbType.Date, IIf(IsDateTimeNull(r.QuitDate.Value), Convert.ToDateTime("1900/1/1"), r.QuitDate.Value))
                If r.ProbDate.Updated Then db.AddInParameter(dbcmd, "@ProbDate", DbType.Date, IIf(IsDateTimeNull(r.ProbDate.Value), Convert.ToDateTime("1900/1/1"), r.ProbDate.Value))
                If r.ProbMonth.Updated Then db.AddInParameter(dbcmd, "@ProbMonth", DbType.Int32, r.ProbMonth.Value)
                If r.NotEmpDay.Updated Then db.AddInParameter(dbcmd, "@NotEmpDay", DbType.Int32, r.NotEmpDay.Value)
                If r.CheckInFlag.Updated Then db.AddInParameter(dbcmd, "@CheckInFlag", DbType.String, r.CheckInFlag.Value)
                If r.IsBossFlag.Updated Then db.AddInParameter(dbcmd, "@IsBossFlag", DbType.String, r.IsBossFlag.Value)
                If r.PassExamFlag.Updated Then db.AddInParameter(dbcmd, "@PassExamFlag", DbType.String, r.PassExamFlag.Value)
                If r.LocalHireFlag.Updated Then db.AddInParameter(dbcmd, "@LocalHireFlag", DbType.String, r.LocalHireFlag.Value)
                If r.Password.Updated Then db.AddInParameter(dbcmd, "@Password", DbType.String, r.Password.Value)
                If r.PasswordErrorCount.Updated Then db.AddInParameter(dbcmd, "@PasswordErrorCount", DbType.Int32, r.PasswordErrorCount.Value)
                If r.ExpireDate.Updated Then db.AddInParameter(dbcmd, "@ExpireDate", DbType.Date, IIf(IsDateTimeNull(r.ExpireDate.Value), Convert.ToDateTime("1900/1/1"), r.ExpireDate.Value))
                If r.EMail.Updated Then db.AddInParameter(dbcmd, "@EMail", DbType.String, r.EMail.Value)
                If r.BanMark.Updated Then db.AddInParameter(dbcmd, "@BanMark", DbType.String, r.BanMark.Value)
                If r.BanMarkValidDate.Updated Then db.AddInParameter(dbcmd, "@BanMarkValidDate", DbType.Date, IIf(IsDateTimeNull(r.BanMarkValidDate.Value), Convert.ToDateTime("1900/1/1"), r.BanMarkValidDate.Value))
                If r.LastLoginTime.Updated Then db.AddInParameter(dbcmd, "@LastLoginTime", DbType.Date, IIf(IsDateTimeNull(r.LastLoginTime.Value), Convert.ToDateTime("1900/1/1"), r.LastLoginTime.Value))
                If r.HostName.Updated Then db.AddInParameter(dbcmd, "@HostName", DbType.String, r.HostName.Value)
                If r.WorkTypeID.Updated Then db.AddInParameter(dbcmd, "@WorkTypeID", DbType.String, r.WorkTypeID.Value)
                If r.SecWorkTypeID.Updated Then db.AddInParameter(dbcmd, "@SecWorkTypeID", DbType.String, r.SecWorkTypeID.Value)
                If r.CreateDate.Updated Then db.AddInParameter(dbcmd, "@CreateDate", DbType.Date, IIf(IsDateTimeNull(r.CreateDate.Value), Convert.ToDateTime("1900/1/1"), r.CreateDate.Value))
                If r.LastChgComp.Updated Then db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, r.LastChgComp.Value)
                If r.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, r.LastChgID.Value)
                If r.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(r.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), r.LastChgDate.Value))
                db.AddInParameter(dbcmd, "@PKCompID", DbType.String, IIf(r.LoadFromDataRow, r.CompID.OldValue, r.CompID.Value))
                db.AddInParameter(dbcmd, "@PKUserID", DbType.String, IIf(r.LoadFromDataRow, r.UserID.OldValue, r.UserID.Value))

                intRowsAffected += db.ExecuteNonQuery(dbcmd, tran)
            Next
            Return intRowsAffected
        End Function

        Public Function IsDataExists(ByVal SC_UserRow As Row) As Boolean
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intCount As Integer = 0

            strSQL.AppendLine("Select Count(*) Cnt From SC_User")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And UserID = @UserID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, SC_UserRow.CompID.Value)
            db.AddInParameter(dbcmd, "@UserID", DbType.String, SC_UserRow.UserID.Value)

            intCount = Convert.ToInt32(db.ExecuteScalar(dbcmd))
            Return IIf(intCount > 0, True, False)
        End Function

        Public Function IsDataExists(ByVal SC_UserRow As Row, ByVal tran As DbTransaction) As Boolean
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intCount As Integer = 0

            strSQL.AppendLine("Select Count(*) Cnt From SC_User")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And UserID = @UserID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, SC_UserRow.CompID.Value)
            db.AddInParameter(dbcmd, "@UserID", DbType.String, SC_UserRow.UserID.Value)

            intCount = Convert.ToInt32(db.ExecuteScalar(dbcmd, tran))
            Return IIf(intCount > 0, True, False)
        End Function

        Public Function QuerybyWhere(WhereCondition As String) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From SC_User")
            If WhereCondition <> String.Empty Then strSQL.AppendLine(WhereCondition)

            Return db.ExecuteDataSet(CommandType.Text, strSQL.ToString())
        End Function

        Public Function Insert(ByVal SC_UserRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Insert into SC_User")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    CompID, UserID, UserName, EngName, IDNo, PassportName, BirthDate, Sex, NationID, IDExpireDate,")
            strSQL.AppendLine("    EduID, Marriage, WorkStatus, EmpType, GroupID, DeptID, OrganID, WorkSiteID, RankID,")
            strSQL.AppendLine("    HoldingRankID, TitleID, PublicTitleID, EmpDate, SinopacEmpDate, QuitDate, ProbDate,")
            strSQL.AppendLine("    ProbMonth, NotEmpDay, CheckInFlag, IsBossFlag, PassExamFlag, LocalHireFlag, Password,")
            strSQL.AppendLine("    PasswordErrorCount, ExpireDate, EMail, BanMark, BanMarkValidDate, LastLoginTime, HostName,")
            strSQL.AppendLine("    WorkTypeID, SecWorkTypeID, CreateDate, LastChgComp, LastChgID, LastChgDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @CompID, @UserID, @UserName, @EngName, @IDNo, @PassportName, @BirthDate, @Sex, @NationID, @IDExpireDate,")
            strSQL.AppendLine("    @EduID, @Marriage, @WorkStatus, @EmpType, @GroupID, @DeptID, @OrganID, @WorkSiteID, @RankID,")
            strSQL.AppendLine("    @HoldingRankID, @TitleID, @PublicTitleID, @EmpDate, @SinopacEmpDate, @QuitDate, @ProbDate,")
            strSQL.AppendLine("    @ProbMonth, @NotEmpDay, @CheckInFlag, @IsBossFlag, @PassExamFlag, @LocalHireFlag, @Password,")
            strSQL.AppendLine("    @PasswordErrorCount, @ExpireDate, @EMail, @BanMark, @BanMarkValidDate, @LastLoginTime, @HostName,")
            strSQL.AppendLine("    @WorkTypeID, @SecWorkTypeID, @CreateDate, @LastChgComp, @LastChgID, @LastChgDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, SC_UserRow.CompID.Value)
            db.AddInParameter(dbcmd, "@UserID", DbType.String, SC_UserRow.UserID.Value)
            db.AddInParameter(dbcmd, "@UserName", DbType.String, SC_UserRow.UserName.Value)
            db.AddInParameter(dbcmd, "@EngName", DbType.String, SC_UserRow.EngName.Value)
            db.AddInParameter(dbcmd, "@IDNo", DbType.String, SC_UserRow.IDNo.Value)
            db.AddInParameter(dbcmd, "@PassportName", DbType.String, SC_UserRow.PassportName.Value)
            db.AddInParameter(dbcmd, "@BirthDate", DbType.Date, IIf(IsDateTimeNull(SC_UserRow.BirthDate.Value), Convert.ToDateTime("1900/1/1"), SC_UserRow.BirthDate.Value))
            db.AddInParameter(dbcmd, "@Sex", DbType.String, SC_UserRow.Sex.Value)
            db.AddInParameter(dbcmd, "@NationID", DbType.String, SC_UserRow.NationID.Value)
            db.AddInParameter(dbcmd, "@IDExpireDate", DbType.Date, IIf(IsDateTimeNull(SC_UserRow.IDExpireDate.Value), Convert.ToDateTime("1900/1/1"), SC_UserRow.IDExpireDate.Value))
            db.AddInParameter(dbcmd, "@EduID", DbType.String, SC_UserRow.EduID.Value)
            db.AddInParameter(dbcmd, "@Marriage", DbType.String, SC_UserRow.Marriage.Value)
            db.AddInParameter(dbcmd, "@WorkStatus", DbType.String, SC_UserRow.WorkStatus.Value)
            db.AddInParameter(dbcmd, "@EmpType", DbType.String, SC_UserRow.EmpType.Value)
            db.AddInParameter(dbcmd, "@GroupID", DbType.String, SC_UserRow.GroupID.Value)
            db.AddInParameter(dbcmd, "@DeptID", DbType.String, SC_UserRow.DeptID.Value)
            db.AddInParameter(dbcmd, "@OrganID", DbType.String, SC_UserRow.OrganID.Value)
            db.AddInParameter(dbcmd, "@WorkSiteID", DbType.String, SC_UserRow.WorkSiteID.Value)
            db.AddInParameter(dbcmd, "@RankID", DbType.String, SC_UserRow.RankID.Value)
            db.AddInParameter(dbcmd, "@HoldingRankID", DbType.String, SC_UserRow.HoldingRankID.Value)
            db.AddInParameter(dbcmd, "@TitleID", DbType.String, SC_UserRow.TitleID.Value)
            db.AddInParameter(dbcmd, "@PublicTitleID", DbType.String, SC_UserRow.PublicTitleID.Value)
            db.AddInParameter(dbcmd, "@EmpDate", DbType.Date, IIf(IsDateTimeNull(SC_UserRow.EmpDate.Value), Convert.ToDateTime("1900/1/1"), SC_UserRow.EmpDate.Value))
            db.AddInParameter(dbcmd, "@SinopacEmpDate", DbType.Date, IIf(IsDateTimeNull(SC_UserRow.SinopacEmpDate.Value), Convert.ToDateTime("1900/1/1"), SC_UserRow.SinopacEmpDate.Value))
            db.AddInParameter(dbcmd, "@QuitDate", DbType.Date, IIf(IsDateTimeNull(SC_UserRow.QuitDate.Value), Convert.ToDateTime("1900/1/1"), SC_UserRow.QuitDate.Value))
            db.AddInParameter(dbcmd, "@ProbDate", DbType.Date, IIf(IsDateTimeNull(SC_UserRow.ProbDate.Value), Convert.ToDateTime("1900/1/1"), SC_UserRow.ProbDate.Value))
            db.AddInParameter(dbcmd, "@ProbMonth", DbType.Int32, SC_UserRow.ProbMonth.Value)
            db.AddInParameter(dbcmd, "@NotEmpDay", DbType.Int32, SC_UserRow.NotEmpDay.Value)
            db.AddInParameter(dbcmd, "@CheckInFlag", DbType.String, SC_UserRow.CheckInFlag.Value)
            db.AddInParameter(dbcmd, "@IsBossFlag", DbType.String, SC_UserRow.IsBossFlag.Value)
            db.AddInParameter(dbcmd, "@PassExamFlag", DbType.String, SC_UserRow.PassExamFlag.Value)
            db.AddInParameter(dbcmd, "@LocalHireFlag", DbType.String, SC_UserRow.LocalHireFlag.Value)
            db.AddInParameter(dbcmd, "@Password", DbType.String, SC_UserRow.Password.Value)
            db.AddInParameter(dbcmd, "@PasswordErrorCount", DbType.Int32, SC_UserRow.PasswordErrorCount.Value)
            db.AddInParameter(dbcmd, "@ExpireDate", DbType.Date, IIf(IsDateTimeNull(SC_UserRow.ExpireDate.Value), Convert.ToDateTime("1900/1/1"), SC_UserRow.ExpireDate.Value))
            db.AddInParameter(dbcmd, "@EMail", DbType.String, SC_UserRow.EMail.Value)
            db.AddInParameter(dbcmd, "@BanMark", DbType.String, SC_UserRow.BanMark.Value)
            db.AddInParameter(dbcmd, "@BanMarkValidDate", DbType.Date, IIf(IsDateTimeNull(SC_UserRow.BanMarkValidDate.Value), Convert.ToDateTime("1900/1/1"), SC_UserRow.BanMarkValidDate.Value))
            db.AddInParameter(dbcmd, "@LastLoginTime", DbType.Date, IIf(IsDateTimeNull(SC_UserRow.LastLoginTime.Value), Convert.ToDateTime("1900/1/1"), SC_UserRow.LastLoginTime.Value))
            db.AddInParameter(dbcmd, "@HostName", DbType.String, SC_UserRow.HostName.Value)
            db.AddInParameter(dbcmd, "@WorkTypeID", DbType.String, SC_UserRow.WorkTypeID.Value)
            db.AddInParameter(dbcmd, "@SecWorkTypeID", DbType.String, SC_UserRow.SecWorkTypeID.Value)
            db.AddInParameter(dbcmd, "@CreateDate", DbType.Date, IIf(IsDateTimeNull(SC_UserRow.CreateDate.Value), Convert.ToDateTime("1900/1/1"), SC_UserRow.CreateDate.Value))
            db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, SC_UserRow.LastChgComp.Value)
            db.AddInParameter(dbcmd, "@LastChgID", DbType.String, SC_UserRow.LastChgID.Value)
            db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(SC_UserRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), SC_UserRow.LastChgDate.Value))

            Return db.ExecuteNonQuery(dbcmd)
        End Function

        Public Function Insert(ByVal SC_UserRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Insert into SC_User")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    CompID, UserID, UserName, EngName, IDNo, PassportName, BirthDate, Sex, NationID, IDExpireDate,")
            strSQL.AppendLine("    EduID, Marriage, WorkStatus, EmpType, GroupID, DeptID, OrganID, WorkSiteID, RankID,")
            strSQL.AppendLine("    HoldingRankID, TitleID, PublicTitleID, EmpDate, SinopacEmpDate, QuitDate, ProbDate,")
            strSQL.AppendLine("    ProbMonth, NotEmpDay, CheckInFlag, IsBossFlag, PassExamFlag, LocalHireFlag, Password,")
            strSQL.AppendLine("    PasswordErrorCount, ExpireDate, EMail, BanMark, BanMarkValidDate, LastLoginTime, HostName,")
            strSQL.AppendLine("    WorkTypeID, SecWorkTypeID, CreateDate, LastChgComp, LastChgID, LastChgDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @CompID, @UserID, @UserName, @EngName, @IDNo, @PassportName, @BirthDate, @Sex, @NationID, @IDExpireDate,")
            strSQL.AppendLine("    @EduID, @Marriage, @WorkStatus, @EmpType, @GroupID, @DeptID, @OrganID, @WorkSiteID, @RankID,")
            strSQL.AppendLine("    @HoldingRankID, @TitleID, @PublicTitleID, @EmpDate, @SinopacEmpDate, @QuitDate, @ProbDate,")
            strSQL.AppendLine("    @ProbMonth, @NotEmpDay, @CheckInFlag, @IsBossFlag, @PassExamFlag, @LocalHireFlag, @Password,")
            strSQL.AppendLine("    @PasswordErrorCount, @ExpireDate, @EMail, @BanMark, @BanMarkValidDate, @LastLoginTime, @HostName,")
            strSQL.AppendLine("    @WorkTypeID, @SecWorkTypeID, @CreateDate, @LastChgComp, @LastChgID, @LastChgDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, SC_UserRow.CompID.Value)
            db.AddInParameter(dbcmd, "@UserID", DbType.String, SC_UserRow.UserID.Value)
            db.AddInParameter(dbcmd, "@UserName", DbType.String, SC_UserRow.UserName.Value)
            db.AddInParameter(dbcmd, "@EngName", DbType.String, SC_UserRow.EngName.Value)
            db.AddInParameter(dbcmd, "@IDNo", DbType.String, SC_UserRow.IDNo.Value)
            db.AddInParameter(dbcmd, "@PassportName", DbType.String, SC_UserRow.PassportName.Value)
            db.AddInParameter(dbcmd, "@BirthDate", DbType.Date, IIf(IsDateTimeNull(SC_UserRow.BirthDate.Value), Convert.ToDateTime("1900/1/1"), SC_UserRow.BirthDate.Value))
            db.AddInParameter(dbcmd, "@Sex", DbType.String, SC_UserRow.Sex.Value)
            db.AddInParameter(dbcmd, "@NationID", DbType.String, SC_UserRow.NationID.Value)
            db.AddInParameter(dbcmd, "@IDExpireDate", DbType.Date, IIf(IsDateTimeNull(SC_UserRow.IDExpireDate.Value), Convert.ToDateTime("1900/1/1"), SC_UserRow.IDExpireDate.Value))
            db.AddInParameter(dbcmd, "@EduID", DbType.String, SC_UserRow.EduID.Value)
            db.AddInParameter(dbcmd, "@Marriage", DbType.String, SC_UserRow.Marriage.Value)
            db.AddInParameter(dbcmd, "@WorkStatus", DbType.String, SC_UserRow.WorkStatus.Value)
            db.AddInParameter(dbcmd, "@EmpType", DbType.String, SC_UserRow.EmpType.Value)
            db.AddInParameter(dbcmd, "@GroupID", DbType.String, SC_UserRow.GroupID.Value)
            db.AddInParameter(dbcmd, "@DeptID", DbType.String, SC_UserRow.DeptID.Value)
            db.AddInParameter(dbcmd, "@OrganID", DbType.String, SC_UserRow.OrganID.Value)
            db.AddInParameter(dbcmd, "@WorkSiteID", DbType.String, SC_UserRow.WorkSiteID.Value)
            db.AddInParameter(dbcmd, "@RankID", DbType.String, SC_UserRow.RankID.Value)
            db.AddInParameter(dbcmd, "@HoldingRankID", DbType.String, SC_UserRow.HoldingRankID.Value)
            db.AddInParameter(dbcmd, "@TitleID", DbType.String, SC_UserRow.TitleID.Value)
            db.AddInParameter(dbcmd, "@PublicTitleID", DbType.String, SC_UserRow.PublicTitleID.Value)
            db.AddInParameter(dbcmd, "@EmpDate", DbType.Date, IIf(IsDateTimeNull(SC_UserRow.EmpDate.Value), Convert.ToDateTime("1900/1/1"), SC_UserRow.EmpDate.Value))
            db.AddInParameter(dbcmd, "@SinopacEmpDate", DbType.Date, IIf(IsDateTimeNull(SC_UserRow.SinopacEmpDate.Value), Convert.ToDateTime("1900/1/1"), SC_UserRow.SinopacEmpDate.Value))
            db.AddInParameter(dbcmd, "@QuitDate", DbType.Date, IIf(IsDateTimeNull(SC_UserRow.QuitDate.Value), Convert.ToDateTime("1900/1/1"), SC_UserRow.QuitDate.Value))
            db.AddInParameter(dbcmd, "@ProbDate", DbType.Date, IIf(IsDateTimeNull(SC_UserRow.ProbDate.Value), Convert.ToDateTime("1900/1/1"), SC_UserRow.ProbDate.Value))
            db.AddInParameter(dbcmd, "@ProbMonth", DbType.Int32, SC_UserRow.ProbMonth.Value)
            db.AddInParameter(dbcmd, "@NotEmpDay", DbType.Int32, SC_UserRow.NotEmpDay.Value)
            db.AddInParameter(dbcmd, "@CheckInFlag", DbType.String, SC_UserRow.CheckInFlag.Value)
            db.AddInParameter(dbcmd, "@IsBossFlag", DbType.String, SC_UserRow.IsBossFlag.Value)
            db.AddInParameter(dbcmd, "@PassExamFlag", DbType.String, SC_UserRow.PassExamFlag.Value)
            db.AddInParameter(dbcmd, "@LocalHireFlag", DbType.String, SC_UserRow.LocalHireFlag.Value)
            db.AddInParameter(dbcmd, "@Password", DbType.String, SC_UserRow.Password.Value)
            db.AddInParameter(dbcmd, "@PasswordErrorCount", DbType.Int32, SC_UserRow.PasswordErrorCount.Value)
            db.AddInParameter(dbcmd, "@ExpireDate", DbType.Date, IIf(IsDateTimeNull(SC_UserRow.ExpireDate.Value), Convert.ToDateTime("1900/1/1"), SC_UserRow.ExpireDate.Value))
            db.AddInParameter(dbcmd, "@EMail", DbType.String, SC_UserRow.EMail.Value)
            db.AddInParameter(dbcmd, "@BanMark", DbType.String, SC_UserRow.BanMark.Value)
            db.AddInParameter(dbcmd, "@BanMarkValidDate", DbType.Date, IIf(IsDateTimeNull(SC_UserRow.BanMarkValidDate.Value), Convert.ToDateTime("1900/1/1"), SC_UserRow.BanMarkValidDate.Value))
            db.AddInParameter(dbcmd, "@LastLoginTime", DbType.Date, IIf(IsDateTimeNull(SC_UserRow.LastLoginTime.Value), Convert.ToDateTime("1900/1/1"), SC_UserRow.LastLoginTime.Value))
            db.AddInParameter(dbcmd, "@HostName", DbType.String, SC_UserRow.HostName.Value)
            db.AddInParameter(dbcmd, "@WorkTypeID", DbType.String, SC_UserRow.WorkTypeID.Value)
            db.AddInParameter(dbcmd, "@SecWorkTypeID", DbType.String, SC_UserRow.SecWorkTypeID.Value)
            db.AddInParameter(dbcmd, "@CreateDate", DbType.Date, IIf(IsDateTimeNull(SC_UserRow.CreateDate.Value), Convert.ToDateTime("1900/1/1"), SC_UserRow.CreateDate.Value))
            db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, SC_UserRow.LastChgComp.Value)
            db.AddInParameter(dbcmd, "@LastChgID", DbType.String, SC_UserRow.LastChgID.Value)
            db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(SC_UserRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), SC_UserRow.LastChgDate.Value))

            Return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function Insert(ByVal SC_UserRow As Row()) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Insert into SC_User")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    CompID, UserID, UserName, EngName, IDNo, PassportName, BirthDate, Sex, NationID, IDExpireDate,")
            strSQL.AppendLine("    EduID, Marriage, WorkStatus, EmpType, GroupID, DeptID, OrganID, WorkSiteID, RankID,")
            strSQL.AppendLine("    HoldingRankID, TitleID, PublicTitleID, EmpDate, SinopacEmpDate, QuitDate, ProbDate,")
            strSQL.AppendLine("    ProbMonth, NotEmpDay, CheckInFlag, IsBossFlag, PassExamFlag, LocalHireFlag, Password,")
            strSQL.AppendLine("    PasswordErrorCount, ExpireDate, EMail, BanMark, BanMarkValidDate, LastLoginTime, HostName,")
            strSQL.AppendLine("    WorkTypeID, SecWorkTypeID, CreateDate, LastChgComp, LastChgID, LastChgDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @CompID, @UserID, @UserName, @EngName, @IDNo, @PassportName, @BirthDate, @Sex, @NationID, @IDExpireDate,")
            strSQL.AppendLine("    @EduID, @Marriage, @WorkStatus, @EmpType, @GroupID, @DeptID, @OrganID, @WorkSiteID, @RankID,")
            strSQL.AppendLine("    @HoldingRankID, @TitleID, @PublicTitleID, @EmpDate, @SinopacEmpDate, @QuitDate, @ProbDate,")
            strSQL.AppendLine("    @ProbMonth, @NotEmpDay, @CheckInFlag, @IsBossFlag, @PassExamFlag, @LocalHireFlag, @Password,")
            strSQL.AppendLine("    @PasswordErrorCount, @ExpireDate, @EMail, @BanMark, @BanMarkValidDate, @LastLoginTime, @HostName,")
            strSQL.AppendLine("    @WorkTypeID, @SecWorkTypeID, @CreateDate, @LastChgComp, @LastChgID, @LastChgDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            Using cn As DbConnection = db.CreateConnection()
                cn.Open()
                Dim tran As DbTransaction = cn.BeginTransaction()
                Dim inTrans As Boolean = True

                Try
                    For Each r As Row In SC_UserRow
                        dbcmd.Parameters.Clear()
                        db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                        db.AddInParameter(dbcmd, "@UserID", DbType.String, r.UserID.Value)
                        db.AddInParameter(dbcmd, "@UserName", DbType.String, r.UserName.Value)
                        db.AddInParameter(dbcmd, "@EngName", DbType.String, r.EngName.Value)
                        db.AddInParameter(dbcmd, "@IDNo", DbType.String, r.IDNo.Value)
                        db.AddInParameter(dbcmd, "@PassportName", DbType.String, r.PassportName.Value)
                        db.AddInParameter(dbcmd, "@BirthDate", DbType.Date, IIf(IsDateTimeNull(r.BirthDate.Value), Convert.ToDateTime("1900/1/1"), r.BirthDate.Value))
                        db.AddInParameter(dbcmd, "@Sex", DbType.String, r.Sex.Value)
                        db.AddInParameter(dbcmd, "@NationID", DbType.String, r.NationID.Value)
                        db.AddInParameter(dbcmd, "@IDExpireDate", DbType.Date, IIf(IsDateTimeNull(r.IDExpireDate.Value), Convert.ToDateTime("1900/1/1"), r.IDExpireDate.Value))
                        db.AddInParameter(dbcmd, "@EduID", DbType.String, r.EduID.Value)
                        db.AddInParameter(dbcmd, "@Marriage", DbType.String, r.Marriage.Value)
                        db.AddInParameter(dbcmd, "@WorkStatus", DbType.String, r.WorkStatus.Value)
                        db.AddInParameter(dbcmd, "@EmpType", DbType.String, r.EmpType.Value)
                        db.AddInParameter(dbcmd, "@GroupID", DbType.String, r.GroupID.Value)
                        db.AddInParameter(dbcmd, "@DeptID", DbType.String, r.DeptID.Value)
                        db.AddInParameter(dbcmd, "@OrganID", DbType.String, r.OrganID.Value)
                        db.AddInParameter(dbcmd, "@WorkSiteID", DbType.String, r.WorkSiteID.Value)
                        db.AddInParameter(dbcmd, "@RankID", DbType.String, r.RankID.Value)
                        db.AddInParameter(dbcmd, "@HoldingRankID", DbType.String, r.HoldingRankID.Value)
                        db.AddInParameter(dbcmd, "@TitleID", DbType.String, r.TitleID.Value)
                        db.AddInParameter(dbcmd, "@PublicTitleID", DbType.String, r.PublicTitleID.Value)
                        db.AddInParameter(dbcmd, "@EmpDate", DbType.Date, IIf(IsDateTimeNull(r.EmpDate.Value), Convert.ToDateTime("1900/1/1"), r.EmpDate.Value))
                        db.AddInParameter(dbcmd, "@SinopacEmpDate", DbType.Date, IIf(IsDateTimeNull(r.SinopacEmpDate.Value), Convert.ToDateTime("1900/1/1"), r.SinopacEmpDate.Value))
                        db.AddInParameter(dbcmd, "@QuitDate", DbType.Date, IIf(IsDateTimeNull(r.QuitDate.Value), Convert.ToDateTime("1900/1/1"), r.QuitDate.Value))
                        db.AddInParameter(dbcmd, "@ProbDate", DbType.Date, IIf(IsDateTimeNull(r.ProbDate.Value), Convert.ToDateTime("1900/1/1"), r.ProbDate.Value))
                        db.AddInParameter(dbcmd, "@ProbMonth", DbType.Int32, r.ProbMonth.Value)
                        db.AddInParameter(dbcmd, "@NotEmpDay", DbType.Int32, r.NotEmpDay.Value)
                        db.AddInParameter(dbcmd, "@CheckInFlag", DbType.String, r.CheckInFlag.Value)
                        db.AddInParameter(dbcmd, "@IsBossFlag", DbType.String, r.IsBossFlag.Value)
                        db.AddInParameter(dbcmd, "@PassExamFlag", DbType.String, r.PassExamFlag.Value)
                        db.AddInParameter(dbcmd, "@LocalHireFlag", DbType.String, r.LocalHireFlag.Value)
                        db.AddInParameter(dbcmd, "@Password", DbType.String, r.Password.Value)
                        db.AddInParameter(dbcmd, "@PasswordErrorCount", DbType.Int32, r.PasswordErrorCount.Value)
                        db.AddInParameter(dbcmd, "@ExpireDate", DbType.Date, IIf(IsDateTimeNull(r.ExpireDate.Value), Convert.ToDateTime("1900/1/1"), r.ExpireDate.Value))
                        db.AddInParameter(dbcmd, "@EMail", DbType.String, r.EMail.Value)
                        db.AddInParameter(dbcmd, "@BanMark", DbType.String, r.BanMark.Value)
                        db.AddInParameter(dbcmd, "@BanMarkValidDate", DbType.Date, IIf(IsDateTimeNull(r.BanMarkValidDate.Value), Convert.ToDateTime("1900/1/1"), r.BanMarkValidDate.Value))
                        db.AddInParameter(dbcmd, "@LastLoginTime", DbType.Date, IIf(IsDateTimeNull(r.LastLoginTime.Value), Convert.ToDateTime("1900/1/1"), r.LastLoginTime.Value))
                        db.AddInParameter(dbcmd, "@HostName", DbType.String, r.HostName.Value)
                        db.AddInParameter(dbcmd, "@WorkTypeID", DbType.String, r.WorkTypeID.Value)
                        db.AddInParameter(dbcmd, "@SecWorkTypeID", DbType.String, r.SecWorkTypeID.Value)
                        db.AddInParameter(dbcmd, "@CreateDate", DbType.Date, IIf(IsDateTimeNull(r.CreateDate.Value), Convert.ToDateTime("1900/1/1"), r.CreateDate.Value))
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

        Public Function Insert(ByVal SC_UserRow As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Insert into SC_User")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    CompID, UserID, UserName, EngName, IDNo, PassportName, BirthDate, Sex, NationID, IDExpireDate,")
            strSQL.AppendLine("    EduID, Marriage, WorkStatus, EmpType, GroupID, DeptID, OrganID, WorkSiteID, RankID,")
            strSQL.AppendLine("    HoldingRankID, TitleID, PublicTitleID, EmpDate, SinopacEmpDate, QuitDate, ProbDate,")
            strSQL.AppendLine("    ProbMonth, NotEmpDay, CheckInFlag, IsBossFlag, PassExamFlag, LocalHireFlag, Password,")
            strSQL.AppendLine("    PasswordErrorCount, ExpireDate, EMail, BanMark, BanMarkValidDate, LastLoginTime, HostName,")
            strSQL.AppendLine("    WorkTypeID, SecWorkTypeID, CreateDate, LastChgComp, LastChgID, LastChgDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @CompID, @UserID, @UserName, @EngName, @IDNo, @PassportName, @BirthDate, @Sex, @NationID, @IDExpireDate,")
            strSQL.AppendLine("    @EduID, @Marriage, @WorkStatus, @EmpType, @GroupID, @DeptID, @OrganID, @WorkSiteID, @RankID,")
            strSQL.AppendLine("    @HoldingRankID, @TitleID, @PublicTitleID, @EmpDate, @SinopacEmpDate, @QuitDate, @ProbDate,")
            strSQL.AppendLine("    @ProbMonth, @NotEmpDay, @CheckInFlag, @IsBossFlag, @PassExamFlag, @LocalHireFlag, @Password,")
            strSQL.AppendLine("    @PasswordErrorCount, @ExpireDate, @EMail, @BanMark, @BanMarkValidDate, @LastLoginTime, @HostName,")
            strSQL.AppendLine("    @WorkTypeID, @SecWorkTypeID, @CreateDate, @LastChgComp, @LastChgID, @LastChgDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())

            For Each r As Row In SC_UserRow
                dbcmd.Parameters.Clear()
                db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                db.AddInParameter(dbcmd, "@UserID", DbType.String, r.UserID.Value)
                db.AddInParameter(dbcmd, "@UserName", DbType.String, r.UserName.Value)
                db.AddInParameter(dbcmd, "@EngName", DbType.String, r.EngName.Value)
                db.AddInParameter(dbcmd, "@IDNo", DbType.String, r.IDNo.Value)
                db.AddInParameter(dbcmd, "@PassportName", DbType.String, r.PassportName.Value)
                db.AddInParameter(dbcmd, "@BirthDate", DbType.Date, IIf(IsDateTimeNull(r.BirthDate.Value), Convert.ToDateTime("1900/1/1"), r.BirthDate.Value))
                db.AddInParameter(dbcmd, "@Sex", DbType.String, r.Sex.Value)
                db.AddInParameter(dbcmd, "@NationID", DbType.String, r.NationID.Value)
                db.AddInParameter(dbcmd, "@IDExpireDate", DbType.Date, IIf(IsDateTimeNull(r.IDExpireDate.Value), Convert.ToDateTime("1900/1/1"), r.IDExpireDate.Value))
                db.AddInParameter(dbcmd, "@EduID", DbType.String, r.EduID.Value)
                db.AddInParameter(dbcmd, "@Marriage", DbType.String, r.Marriage.Value)
                db.AddInParameter(dbcmd, "@WorkStatus", DbType.String, r.WorkStatus.Value)
                db.AddInParameter(dbcmd, "@EmpType", DbType.String, r.EmpType.Value)
                db.AddInParameter(dbcmd, "@GroupID", DbType.String, r.GroupID.Value)
                db.AddInParameter(dbcmd, "@DeptID", DbType.String, r.DeptID.Value)
                db.AddInParameter(dbcmd, "@OrganID", DbType.String, r.OrganID.Value)
                db.AddInParameter(dbcmd, "@WorkSiteID", DbType.String, r.WorkSiteID.Value)
                db.AddInParameter(dbcmd, "@RankID", DbType.String, r.RankID.Value)
                db.AddInParameter(dbcmd, "@HoldingRankID", DbType.String, r.HoldingRankID.Value)
                db.AddInParameter(dbcmd, "@TitleID", DbType.String, r.TitleID.Value)
                db.AddInParameter(dbcmd, "@PublicTitleID", DbType.String, r.PublicTitleID.Value)
                db.AddInParameter(dbcmd, "@EmpDate", DbType.Date, IIf(IsDateTimeNull(r.EmpDate.Value), Convert.ToDateTime("1900/1/1"), r.EmpDate.Value))
                db.AddInParameter(dbcmd, "@SinopacEmpDate", DbType.Date, IIf(IsDateTimeNull(r.SinopacEmpDate.Value), Convert.ToDateTime("1900/1/1"), r.SinopacEmpDate.Value))
                db.AddInParameter(dbcmd, "@QuitDate", DbType.Date, IIf(IsDateTimeNull(r.QuitDate.Value), Convert.ToDateTime("1900/1/1"), r.QuitDate.Value))
                db.AddInParameter(dbcmd, "@ProbDate", DbType.Date, IIf(IsDateTimeNull(r.ProbDate.Value), Convert.ToDateTime("1900/1/1"), r.ProbDate.Value))
                db.AddInParameter(dbcmd, "@ProbMonth", DbType.Int32, r.ProbMonth.Value)
                db.AddInParameter(dbcmd, "@NotEmpDay", DbType.Int32, r.NotEmpDay.Value)
                db.AddInParameter(dbcmd, "@CheckInFlag", DbType.String, r.CheckInFlag.Value)
                db.AddInParameter(dbcmd, "@IsBossFlag", DbType.String, r.IsBossFlag.Value)
                db.AddInParameter(dbcmd, "@PassExamFlag", DbType.String, r.PassExamFlag.Value)
                db.AddInParameter(dbcmd, "@LocalHireFlag", DbType.String, r.LocalHireFlag.Value)
                db.AddInParameter(dbcmd, "@Password", DbType.String, r.Password.Value)
                db.AddInParameter(dbcmd, "@PasswordErrorCount", DbType.Int32, r.PasswordErrorCount.Value)
                db.AddInParameter(dbcmd, "@ExpireDate", DbType.Date, IIf(IsDateTimeNull(r.ExpireDate.Value), Convert.ToDateTime("1900/1/1"), r.ExpireDate.Value))
                db.AddInParameter(dbcmd, "@EMail", DbType.String, r.EMail.Value)
                db.AddInParameter(dbcmd, "@BanMark", DbType.String, r.BanMark.Value)
                db.AddInParameter(dbcmd, "@BanMarkValidDate", DbType.Date, IIf(IsDateTimeNull(r.BanMarkValidDate.Value), Convert.ToDateTime("1900/1/1"), r.BanMarkValidDate.Value))
                db.AddInParameter(dbcmd, "@LastLoginTime", DbType.Date, IIf(IsDateTimeNull(r.LastLoginTime.Value), Convert.ToDateTime("1900/1/1"), r.LastLoginTime.Value))
                db.AddInParameter(dbcmd, "@HostName", DbType.String, r.HostName.Value)
                db.AddInParameter(dbcmd, "@WorkTypeID", DbType.String, r.WorkTypeID.Value)
                db.AddInParameter(dbcmd, "@SecWorkTypeID", DbType.String, r.SecWorkTypeID.Value)
                db.AddInParameter(dbcmd, "@CreateDate", DbType.Date, IIf(IsDateTimeNull(r.CreateDate.Value), Convert.ToDateTime("1900/1/1"), r.CreateDate.Value))
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

