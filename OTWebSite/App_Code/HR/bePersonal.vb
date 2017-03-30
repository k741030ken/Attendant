'****************************************************************
' Table:Personal
' Created Date: 2015.12.15
'****************************************************************/
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports Microsoft.Practices.EnterpriseLibrary.Common
Imports Microsoft.Practices.EnterpriseLibrary.Data
Imports System.Data.Common
Imports System.Data

Namespace bePersonal
    Public Class Table
        Private m_Rows As Rows = New Rows()
        Private m_Fields As String() = { "CompID", "EmpID", "IDNo", "Name", "NameN", "NameB", "EngName", "PassportName", "BirthDate", "Sex" _
                                    , "NationID", "IDExpireDate", "EduID", "Marriage", "WorkStatus", "EmpType", "GroupID", "DeptID", "OrganID", "WorkSiteID", "RankID" _
                                    , "RankIDMap", "HoldingRankID", "TitleID", "PublicTitleID", "EmpDate", "SinopacEmpDate", "QuitDate", "SinopacQuitDate", "ProbDate", "ProbMonth", "NotEmpDay" _
                                    , "SinopacNotEmpDay", "CheckInFlag", "IsHLBFlag", "PassExamFlag", "Password", "PwdErrCnt", "Question", "Answer", "QuestionErrCnt", "ExpireDate", "LoginTime" _
                                    , "RegStatus", "RegIP", "AOCode", "EmpIDOld", "LocalHireFlag", "RankBeginDate", "DeptBeginDate", "PositionBeginDate", "WorkTypeBeginDate", "BossBeginDate", "IDType" _
                                    , "LastChgComp", "LastChgID", "LastChgDate" }
        Private m_Types As System.Type() = { GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(Date), GetType(String) _
                                    , GetType(String), GetType(Date), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String) _
                                    , GetType(String), GetType(String), GetType(String), GetType(String), GetType(Date), GetType(Date), GetType(Date), GetType(Date), GetType(Date), GetType(Integer), GetType(Integer) _
                                    , GetType(Integer), GetType(String), GetType(String), GetType(String), GetType(String), GetType(Integer), GetType(String), GetType(String), GetType(Integer), GetType(Date), GetType(Date) _
                                    , GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(Date), GetType(Date), GetType(Date), GetType(Date), GetType(Date), GetType(String) _
                                    , GetType(String), GetType(String), GetType(Date) }
        Private m_PrimaryFields As String() = { "CompID", "EmpID" }

        Public ReadOnly Property Rows() As bePersonal.Rows 
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
        Public Sub Transfer2Row(PersonalTable As DataTable)
            For Each dr As DataRow In PersonalTable.Rows
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
                dr(m_Rows(i).IDNo.FieldName) = m_Rows(i).IDNo.Value
                dr(m_Rows(i).Name.FieldName) = m_Rows(i).Name.Value
                dr(m_Rows(i).NameN.FieldName) = m_Rows(i).NameN.Value
                dr(m_Rows(i).NameB.FieldName) = m_Rows(i).NameB.Value
                dr(m_Rows(i).EngName.FieldName) = m_Rows(i).EngName.Value
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
                dr(m_Rows(i).RankIDMap.FieldName) = m_Rows(i).RankIDMap.Value
                dr(m_Rows(i).HoldingRankID.FieldName) = m_Rows(i).HoldingRankID.Value
                dr(m_Rows(i).TitleID.FieldName) = m_Rows(i).TitleID.Value
                dr(m_Rows(i).PublicTitleID.FieldName) = m_Rows(i).PublicTitleID.Value
                dr(m_Rows(i).EmpDate.FieldName) = m_Rows(i).EmpDate.Value
                dr(m_Rows(i).SinopacEmpDate.FieldName) = m_Rows(i).SinopacEmpDate.Value
                dr(m_Rows(i).QuitDate.FieldName) = m_Rows(i).QuitDate.Value
                dr(m_Rows(i).SinopacQuitDate.FieldName) = m_Rows(i).SinopacQuitDate.Value
                dr(m_Rows(i).ProbDate.FieldName) = m_Rows(i).ProbDate.Value
                dr(m_Rows(i).ProbMonth.FieldName) = m_Rows(i).ProbMonth.Value
                dr(m_Rows(i).NotEmpDay.FieldName) = m_Rows(i).NotEmpDay.Value
                dr(m_Rows(i).SinopacNotEmpDay.FieldName) = m_Rows(i).SinopacNotEmpDay.Value
                dr(m_Rows(i).CheckInFlag.FieldName) = m_Rows(i).CheckInFlag.Value
                dr(m_Rows(i).IsHLBFlag.FieldName) = m_Rows(i).IsHLBFlag.Value
                dr(m_Rows(i).PassExamFlag.FieldName) = m_Rows(i).PassExamFlag.Value
                dr(m_Rows(i).Password.FieldName) = m_Rows(i).Password.Value
                dr(m_Rows(i).PwdErrCnt.FieldName) = m_Rows(i).PwdErrCnt.Value
                dr(m_Rows(i).Question.FieldName) = m_Rows(i).Question.Value
                dr(m_Rows(i).Answer.FieldName) = m_Rows(i).Answer.Value
                dr(m_Rows(i).QuestionErrCnt.FieldName) = m_Rows(i).QuestionErrCnt.Value
                dr(m_Rows(i).ExpireDate.FieldName) = m_Rows(i).ExpireDate.Value
                dr(m_Rows(i).LoginTime.FieldName) = m_Rows(i).LoginTime.Value
                dr(m_Rows(i).RegStatus.FieldName) = m_Rows(i).RegStatus.Value
                dr(m_Rows(i).RegIP.FieldName) = m_Rows(i).RegIP.Value
                dr(m_Rows(i).AOCode.FieldName) = m_Rows(i).AOCode.Value
                dr(m_Rows(i).EmpIDOld.FieldName) = m_Rows(i).EmpIDOld.Value
                dr(m_Rows(i).LocalHireFlag.FieldName) = m_Rows(i).LocalHireFlag.Value
                dr(m_Rows(i).RankBeginDate.FieldName) = m_Rows(i).RankBeginDate.Value
                dr(m_Rows(i).DeptBeginDate.FieldName) = m_Rows(i).DeptBeginDate.Value
                dr(m_Rows(i).PositionBeginDate.FieldName) = m_Rows(i).PositionBeginDate.Value
                dr(m_Rows(i).WorkTypeBeginDate.FieldName) = m_Rows(i).WorkTypeBeginDate.Value
                dr(m_Rows(i).BossBeginDate.FieldName) = m_Rows(i).BossBeginDate.Value
                dr(m_Rows(i).IDType.FieldName) = m_Rows(i).IDType.Value
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

        Public Sub Add(PersonalRow As Row)
            m_Rows.Add(PersonalRow)
        End Sub

        Public Sub Remove(PersonalRow As Row)
            If m_Rows.IndexOf(PersonalRow) >= 0 Then
                m_Rows.Remove(PersonalRow)
            End If
        End Sub

        Public Sub Dispose()
            m_Rows.Clear()
        End Sub

    End Class

    Public Class Row

        Private FI_CompID As Field(Of String) = new Field(Of String)("CompID", true)
        Private FI_EmpID As Field(Of String) = new Field(Of String)("EmpID", true)
        Private FI_IDNo As Field(Of String) = new Field(Of String)("IDNo", true)
        Private FI_Name As Field(Of String) = new Field(Of String)("Name", true)
        Private FI_NameN As Field(Of String) = new Field(Of String)("NameN", true)
        Private FI_NameB As Field(Of String) = new Field(Of String)("NameB", true)
        Private FI_EngName As Field(Of String) = new Field(Of String)("EngName", true)
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
        Private FI_RankIDMap As Field(Of String) = new Field(Of String)("RankIDMap", true)
        Private FI_HoldingRankID As Field(Of String) = new Field(Of String)("HoldingRankID", true)
        Private FI_TitleID As Field(Of String) = new Field(Of String)("TitleID", true)
        Private FI_PublicTitleID As Field(Of String) = new Field(Of String)("PublicTitleID", true)
        Private FI_EmpDate As Field(Of Date) = new Field(Of Date)("EmpDate", true)
        Private FI_SinopacEmpDate As Field(Of Date) = new Field(Of Date)("SinopacEmpDate", true)
        Private FI_QuitDate As Field(Of Date) = new Field(Of Date)("QuitDate", true)
        Private FI_SinopacQuitDate As Field(Of Date) = new Field(Of Date)("SinopacQuitDate", true)
        Private FI_ProbDate As Field(Of Date) = new Field(Of Date)("ProbDate", true)
        Private FI_ProbMonth As Field(Of Integer) = new Field(Of Integer)("ProbMonth", true)
        Private FI_NotEmpDay As Field(Of Integer) = new Field(Of Integer)("NotEmpDay", true)
        Private FI_SinopacNotEmpDay As Field(Of Integer) = new Field(Of Integer)("SinopacNotEmpDay", true)
        Private FI_CheckInFlag As Field(Of String) = new Field(Of String)("CheckInFlag", true)
        Private FI_IsHLBFlag As Field(Of String) = new Field(Of String)("IsHLBFlag", true)
        Private FI_PassExamFlag As Field(Of String) = new Field(Of String)("PassExamFlag", true)
        Private FI_Password As Field(Of String) = new Field(Of String)("Password", true)
        Private FI_PwdErrCnt As Field(Of Integer) = new Field(Of Integer)("PwdErrCnt", true)
        Private FI_Question As Field(Of String) = new Field(Of String)("Question", true)
        Private FI_Answer As Field(Of String) = new Field(Of String)("Answer", true)
        Private FI_QuestionErrCnt As Field(Of Integer) = new Field(Of Integer)("QuestionErrCnt", true)
        Private FI_ExpireDate As Field(Of Date) = new Field(Of Date)("ExpireDate", true)
        Private FI_LoginTime As Field(Of Date) = new Field(Of Date)("LoginTime", true)
        Private FI_RegStatus As Field(Of String) = new Field(Of String)("RegStatus", true)
        Private FI_RegIP As Field(Of String) = new Field(Of String)("RegIP", true)
        Private FI_AOCode As Field(Of String) = new Field(Of String)("AOCode", true)
        Private FI_EmpIDOld As Field(Of String) = new Field(Of String)("EmpIDOld", true)
        Private FI_LocalHireFlag As Field(Of String) = new Field(Of String)("LocalHireFlag", true)
        Private FI_RankBeginDate As Field(Of Date) = new Field(Of Date)("RankBeginDate", true)
        Private FI_DeptBeginDate As Field(Of Date) = new Field(Of Date)("DeptBeginDate", true)
        Private FI_PositionBeginDate As Field(Of Date) = new Field(Of Date)("PositionBeginDate", true)
        Private FI_WorkTypeBeginDate As Field(Of Date) = new Field(Of Date)("WorkTypeBeginDate", true)
        Private FI_BossBeginDate As Field(Of Date) = new Field(Of Date)("BossBeginDate", true)
        Private FI_IDType As Field(Of String) = new Field(Of String)("IDType", true)
        Private FI_LastChgComp As Field(Of String) = new Field(Of String)("LastChgComp", true)
        Private FI_LastChgID As Field(Of String) = new Field(Of String)("LastChgID", true)
        Private FI_LastChgDate As Field(Of Date) = new Field(Of Date)("LastChgDate", true)
        Private m_FieldNames As String() = { "CompID", "EmpID", "IDNo", "Name", "NameN", "NameB", "EngName", "PassportName", "BirthDate", "Sex" _
                                    , "NationID", "IDExpireDate", "EduID", "Marriage", "WorkStatus", "EmpType", "GroupID", "DeptID", "OrganID", "WorkSiteID", "RankID" _
                                    , "RankIDMap", "HoldingRankID", "TitleID", "PublicTitleID", "EmpDate", "SinopacEmpDate", "QuitDate", "SinopacQuitDate", "ProbDate", "ProbMonth", "NotEmpDay" _
                                    , "SinopacNotEmpDay", "CheckInFlag", "IsHLBFlag", "PassExamFlag", "Password", "PwdErrCnt", "Question", "Answer", "QuestionErrCnt", "ExpireDate", "LoginTime" _
                                    , "RegStatus", "RegIP", "AOCode", "EmpIDOld", "LocalHireFlag", "RankBeginDate", "DeptBeginDate", "PositionBeginDate", "WorkTypeBeginDate", "BossBeginDate", "IDType" _
                                    , "LastChgComp", "LastChgID", "LastChgDate" }
        Private m_PrimaryFields As String() = { "CompID", "EmpID" }
        Private m_IdentityFields As String() = {  }
        Private m_LoadFromDataRow As Boolean = False

        Private Function GetFieldValue(ByVal fieldName As String) As Object
            Select Case fieldName
                Case "CompID"
                    Return FI_CompID.Value
                Case "EmpID"
                    Return FI_EmpID.Value
                Case "IDNo"
                    Return FI_IDNo.Value
                Case "Name"
                    Return FI_Name.Value
                Case "NameN"
                    Return FI_NameN.Value
                Case "NameB"
                    Return FI_NameB.Value
                Case "EngName"
                    Return FI_EngName.Value
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
                Case "RankIDMap"
                    Return FI_RankIDMap.Value
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
                Case "SinopacQuitDate"
                    Return FI_SinopacQuitDate.Value
                Case "ProbDate"
                    Return FI_ProbDate.Value
                Case "ProbMonth"
                    Return FI_ProbMonth.Value
                Case "NotEmpDay"
                    Return FI_NotEmpDay.Value
                Case "SinopacNotEmpDay"
                    Return FI_SinopacNotEmpDay.Value
                Case "CheckInFlag"
                    Return FI_CheckInFlag.Value
                Case "IsHLBFlag"
                    Return FI_IsHLBFlag.Value
                Case "PassExamFlag"
                    Return FI_PassExamFlag.Value
                Case "Password"
                    Return FI_Password.Value
                Case "PwdErrCnt"
                    Return FI_PwdErrCnt.Value
                Case "Question"
                    Return FI_Question.Value
                Case "Answer"
                    Return FI_Answer.Value
                Case "QuestionErrCnt"
                    Return FI_QuestionErrCnt.Value
                Case "ExpireDate"
                    Return FI_ExpireDate.Value
                Case "LoginTime"
                    Return FI_LoginTime.Value
                Case "RegStatus"
                    Return FI_RegStatus.Value
                Case "RegIP"
                    Return FI_RegIP.Value
                Case "AOCode"
                    Return FI_AOCode.Value
                Case "EmpIDOld"
                    Return FI_EmpIDOld.Value
                Case "LocalHireFlag"
                    Return FI_LocalHireFlag.Value
                Case "RankBeginDate"
                    Return FI_RankBeginDate.Value
                Case "DeptBeginDate"
                    Return FI_DeptBeginDate.Value
                Case "PositionBeginDate"
                    Return FI_PositionBeginDate.Value
                Case "WorkTypeBeginDate"
                    Return FI_WorkTypeBeginDate.Value
                Case "BossBeginDate"
                    Return FI_BossBeginDate.Value
                Case "IDType"
                    Return FI_IDType.Value
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
                Case "EmpID"
                    FI_EmpID.SetValue(value)
                Case "IDNo"
                    FI_IDNo.SetValue(value)
                Case "Name"
                    FI_Name.SetValue(value)
                Case "NameN"
                    FI_NameN.SetValue(value)
                Case "NameB"
                    FI_NameB.SetValue(value)
                Case "EngName"
                    FI_EngName.SetValue(value)
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
                Case "RankIDMap"
                    FI_RankIDMap.SetValue(value)
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
                Case "SinopacQuitDate"
                    FI_SinopacQuitDate.SetValue(value)
                Case "ProbDate"
                    FI_ProbDate.SetValue(value)
                Case "ProbMonth"
                    FI_ProbMonth.SetValue(value)
                Case "NotEmpDay"
                    FI_NotEmpDay.SetValue(value)
                Case "SinopacNotEmpDay"
                    FI_SinopacNotEmpDay.SetValue(value)
                Case "CheckInFlag"
                    FI_CheckInFlag.SetValue(value)
                Case "IsHLBFlag"
                    FI_IsHLBFlag.SetValue(value)
                Case "PassExamFlag"
                    FI_PassExamFlag.SetValue(value)
                Case "Password"
                    FI_Password.SetValue(value)
                Case "PwdErrCnt"
                    FI_PwdErrCnt.SetValue(value)
                Case "Question"
                    FI_Question.SetValue(value)
                Case "Answer"
                    FI_Answer.SetValue(value)
                Case "QuestionErrCnt"
                    FI_QuestionErrCnt.SetValue(value)
                Case "ExpireDate"
                    FI_ExpireDate.SetValue(value)
                Case "LoginTime"
                    FI_LoginTime.SetValue(value)
                Case "RegStatus"
                    FI_RegStatus.SetValue(value)
                Case "RegIP"
                    FI_RegIP.SetValue(value)
                Case "AOCode"
                    FI_AOCode.SetValue(value)
                Case "EmpIDOld"
                    FI_EmpIDOld.SetValue(value)
                Case "LocalHireFlag"
                    FI_LocalHireFlag.SetValue(value)
                Case "RankBeginDate"
                    FI_RankBeginDate.SetValue(value)
                Case "DeptBeginDate"
                    FI_DeptBeginDate.SetValue(value)
                Case "PositionBeginDate"
                    FI_PositionBeginDate.SetValue(value)
                Case "WorkTypeBeginDate"
                    FI_WorkTypeBeginDate.SetValue(value)
                Case "BossBeginDate"
                    FI_BossBeginDate.SetValue(value)
                Case "IDType"
                    FI_IDType.SetValue(value)
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
                Case "EmpID"
                    return FI_EmpID.Updated
                Case "IDNo"
                    return FI_IDNo.Updated
                Case "Name"
                    return FI_Name.Updated
                Case "NameN"
                    return FI_NameN.Updated
                Case "NameB"
                    return FI_NameB.Updated
                Case "EngName"
                    return FI_EngName.Updated
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
                Case "RankIDMap"
                    return FI_RankIDMap.Updated
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
                Case "SinopacQuitDate"
                    return FI_SinopacQuitDate.Updated
                Case "ProbDate"
                    return FI_ProbDate.Updated
                Case "ProbMonth"
                    return FI_ProbMonth.Updated
                Case "NotEmpDay"
                    return FI_NotEmpDay.Updated
                Case "SinopacNotEmpDay"
                    return FI_SinopacNotEmpDay.Updated
                Case "CheckInFlag"
                    return FI_CheckInFlag.Updated
                Case "IsHLBFlag"
                    return FI_IsHLBFlag.Updated
                Case "PassExamFlag"
                    return FI_PassExamFlag.Updated
                Case "Password"
                    return FI_Password.Updated
                Case "PwdErrCnt"
                    return FI_PwdErrCnt.Updated
                Case "Question"
                    return FI_Question.Updated
                Case "Answer"
                    return FI_Answer.Updated
                Case "QuestionErrCnt"
                    return FI_QuestionErrCnt.Updated
                Case "ExpireDate"
                    return FI_ExpireDate.Updated
                Case "LoginTime"
                    return FI_LoginTime.Updated
                Case "RegStatus"
                    return FI_RegStatus.Updated
                Case "RegIP"
                    return FI_RegIP.Updated
                Case "AOCode"
                    return FI_AOCode.Updated
                Case "EmpIDOld"
                    return FI_EmpIDOld.Updated
                Case "LocalHireFlag"
                    return FI_LocalHireFlag.Updated
                Case "RankBeginDate"
                    return FI_RankBeginDate.Updated
                Case "DeptBeginDate"
                    return FI_DeptBeginDate.Updated
                Case "PositionBeginDate"
                    return FI_PositionBeginDate.Updated
                Case "WorkTypeBeginDate"
                    return FI_WorkTypeBeginDate.Updated
                Case "BossBeginDate"
                    return FI_BossBeginDate.Updated
                Case "IDType"
                    return FI_IDType.Updated
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
                Case "EmpID"
                    return FI_EmpID.CreateUpdateSQL
                Case "IDNo"
                    return FI_IDNo.CreateUpdateSQL
                Case "Name"
                    return FI_Name.CreateUpdateSQL
                Case "NameN"
                    return FI_NameN.CreateUpdateSQL
                Case "NameB"
                    return FI_NameB.CreateUpdateSQL
                Case "EngName"
                    return FI_EngName.CreateUpdateSQL
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
                Case "RankIDMap"
                    return FI_RankIDMap.CreateUpdateSQL
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
                Case "SinopacQuitDate"
                    return FI_SinopacQuitDate.CreateUpdateSQL
                Case "ProbDate"
                    return FI_ProbDate.CreateUpdateSQL
                Case "ProbMonth"
                    return FI_ProbMonth.CreateUpdateSQL
                Case "NotEmpDay"
                    return FI_NotEmpDay.CreateUpdateSQL
                Case "SinopacNotEmpDay"
                    return FI_SinopacNotEmpDay.CreateUpdateSQL
                Case "CheckInFlag"
                    return FI_CheckInFlag.CreateUpdateSQL
                Case "IsHLBFlag"
                    return FI_IsHLBFlag.CreateUpdateSQL
                Case "PassExamFlag"
                    return FI_PassExamFlag.CreateUpdateSQL
                Case "Password"
                    return FI_Password.CreateUpdateSQL
                Case "PwdErrCnt"
                    return FI_PwdErrCnt.CreateUpdateSQL
                Case "Question"
                    return FI_Question.CreateUpdateSQL
                Case "Answer"
                    return FI_Answer.CreateUpdateSQL
                Case "QuestionErrCnt"
                    return FI_QuestionErrCnt.CreateUpdateSQL
                Case "ExpireDate"
                    return FI_ExpireDate.CreateUpdateSQL
                Case "LoginTime"
                    return FI_LoginTime.CreateUpdateSQL
                Case "RegStatus"
                    return FI_RegStatus.CreateUpdateSQL
                Case "RegIP"
                    return FI_RegIP.CreateUpdateSQL
                Case "AOCode"
                    return FI_AOCode.CreateUpdateSQL
                Case "EmpIDOld"
                    return FI_EmpIDOld.CreateUpdateSQL
                Case "LocalHireFlag"
                    return FI_LocalHireFlag.CreateUpdateSQL
                Case "RankBeginDate"
                    return FI_RankBeginDate.CreateUpdateSQL
                Case "DeptBeginDate"
                    return FI_DeptBeginDate.CreateUpdateSQL
                Case "PositionBeginDate"
                    return FI_PositionBeginDate.CreateUpdateSQL
                Case "WorkTypeBeginDate"
                    return FI_WorkTypeBeginDate.CreateUpdateSQL
                Case "BossBeginDate"
                    return FI_BossBeginDate.CreateUpdateSQL
                Case "IDType"
                    return FI_IDType.CreateUpdateSQL
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
            FI_EmpID.SetInitValue("")
            FI_IDNo.SetInitValue("")
            FI_Name.SetInitValue("")
            FI_NameN.SetInitValue("")
            FI_NameB.SetInitValue("")
            FI_EngName.SetInitValue("")
            FI_PassportName.SetInitValue("")
            FI_BirthDate.SetInitValue(Convert.ToDateTime("1900/01/01"))
            FI_Sex.SetInitValue("1")
            FI_NationID.SetInitValue("1")
            FI_IDExpireDate.SetInitValue(Convert.ToDateTime("1900/01/01"))
            FI_EduID.SetInitValue("")
            FI_Marriage.SetInitValue("1")
            FI_WorkStatus.SetInitValue("1")
            FI_EmpType.SetInitValue("2")
            FI_GroupID.SetInitValue("")
            FI_DeptID.SetInitValue("")
            FI_OrganID.SetInitValue("")
            FI_WorkSiteID.SetInitValue("")
            FI_RankID.SetInitValue("")
            FI_RankIDMap.SetInitValue("")
            FI_HoldingRankID.SetInitValue("")
            FI_TitleID.SetInitValue("")
            FI_PublicTitleID.SetInitValue("")
            FI_EmpDate.SetInitValue(Convert.ToDateTime("1900/01/01"))
            FI_SinopacEmpDate.SetInitValue(Convert.ToDateTime("1900/01/01"))
            FI_QuitDate.SetInitValue(Convert.ToDateTime("1900/01/01"))
            FI_SinopacQuitDate.SetInitValue(Convert.ToDateTime("1900/01/01"))
            FI_ProbDate.SetInitValue(Convert.ToDateTime("1900/01/01"))
            FI_ProbMonth.SetInitValue(0)
            FI_NotEmpDay.SetInitValue(0)
            FI_SinopacNotEmpDay.SetInitValue(0)
            FI_CheckInFlag.SetInitValue("1")
            FI_IsHLBFlag.SetInitValue("0")
            FI_PassExamFlag.SetInitValue("0")
            FI_Password.SetInitValue("NEWUSER")
            FI_PwdErrCnt.SetInitValue(0)
            FI_Question.SetInitValue("")
            FI_Answer.SetInitValue("")
            FI_QuestionErrCnt.SetInitValue(0)
            FI_ExpireDate.SetInitValue(Convert.ToDateTime("1900/01/01"))
            FI_LoginTime.SetInitValue(Convert.ToDateTime("1900/01/01"))
            FI_RegStatus.SetInitValue("0")
            FI_RegIP.SetInitValue("")
            FI_AOCode.SetInitValue("")
            FI_EmpIDOld.SetInitValue("")
            FI_LocalHireFlag.SetInitValue(0)
            FI_RankBeginDate.SetInitValue(Convert.ToDateTime("1900/01/01"))
            FI_DeptBeginDate.SetInitValue(Convert.ToDateTime("1900/01/01"))
            FI_PositionBeginDate.SetInitValue(Convert.ToDateTime("1900/01/01"))
            FI_WorkTypeBeginDate.SetInitValue(Convert.ToDateTime("1900/01/01"))
            FI_BossBeginDate.SetInitValue(Convert.ToDateTime("1900/01/01"))
            FI_IDType.SetInitValue("")
            FI_LastChgComp.SetInitValue("")
            FI_LastChgID.SetInitValue("")
            FI_LastChgDate.SetInitValue(Convert.ToDateTime("1900/01/01"))
        End Sub

        Public Sub New(ByVal dr As System.Data.DataRow)
            FI_CompID.SetInitValue(dr("CompID"))
            FI_EmpID.SetInitValue(dr("EmpID"))
            FI_IDNo.SetInitValue(dr("IDNo"))
            FI_Name.SetInitValue(dr("Name"))
            FI_NameN.SetInitValue(dr("NameN"))
            FI_NameB.SetInitValue(dr("NameB"))
            FI_EngName.SetInitValue(dr("EngName"))
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
            FI_RankIDMap.SetInitValue(dr("RankIDMap"))
            FI_HoldingRankID.SetInitValue(dr("HoldingRankID"))
            FI_TitleID.SetInitValue(dr("TitleID"))
            FI_PublicTitleID.SetInitValue(dr("PublicTitleID"))
            FI_EmpDate.SetInitValue(dr("EmpDate"))
            FI_SinopacEmpDate.SetInitValue(dr("SinopacEmpDate"))
            FI_QuitDate.SetInitValue(dr("QuitDate"))
            FI_SinopacQuitDate.SetInitValue(dr("SinopacQuitDate"))
            FI_ProbDate.SetInitValue(dr("ProbDate"))
            FI_ProbMonth.SetInitValue(dr("ProbMonth"))
            FI_NotEmpDay.SetInitValue(dr("NotEmpDay"))
            FI_SinopacNotEmpDay.SetInitValue(dr("SinopacNotEmpDay"))
            FI_CheckInFlag.SetInitValue(dr("CheckInFlag"))
            FI_IsHLBFlag.SetInitValue(dr("IsHLBFlag"))
            FI_PassExamFlag.SetInitValue(dr("PassExamFlag"))
            FI_Password.SetInitValue(dr("Password"))
            FI_PwdErrCnt.SetInitValue(dr("PwdErrCnt"))
            FI_Question.SetInitValue(dr("Question"))
            FI_Answer.SetInitValue(dr("Answer"))
            FI_QuestionErrCnt.SetInitValue(dr("QuestionErrCnt"))
            FI_ExpireDate.SetInitValue(dr("ExpireDate"))
            FI_LoginTime.SetInitValue(dr("LoginTime"))
            FI_RegStatus.SetInitValue(dr("RegStatus"))
            FI_RegIP.SetInitValue(dr("RegIP"))
            FI_AOCode.SetInitValue(dr("AOCode"))
            FI_EmpIDOld.SetInitValue(dr("EmpIDOld"))
            FI_LocalHireFlag.SetInitValue(dr("LocalHireFlag"))
            FI_RankBeginDate.SetInitValue(dr("RankBeginDate"))
            FI_DeptBeginDate.SetInitValue(dr("DeptBeginDate"))
            FI_PositionBeginDate.SetInitValue(dr("PositionBeginDate"))
            FI_WorkTypeBeginDate.SetInitValue(dr("WorkTypeBeginDate"))
            FI_BossBeginDate.SetInitValue(dr("BossBeginDate"))
            FI_IDType.SetInitValue(dr("IDType"))
            FI_LastChgComp.SetInitValue(dr("LastChgComp"))
            FI_LastChgID.SetInitValue(dr("LastChgID"))
            FI_LastChgDate.SetInitValue(dr("LastChgDate"))

            m_LoadFromDataRow = True
        End Sub

        Private Sub ClearUpdated()
            FI_CompID.Updated = False
            FI_EmpID.Updated = False
            FI_IDNo.Updated = False
            FI_Name.Updated = False
            FI_NameN.Updated = False
            FI_NameB.Updated = False
            FI_EngName.Updated = False
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
            FI_RankIDMap.Updated = False
            FI_HoldingRankID.Updated = False
            FI_TitleID.Updated = False
            FI_PublicTitleID.Updated = False
            FI_EmpDate.Updated = False
            FI_SinopacEmpDate.Updated = False
            FI_QuitDate.Updated = False
            FI_SinopacQuitDate.Updated = False
            FI_ProbDate.Updated = False
            FI_ProbMonth.Updated = False
            FI_NotEmpDay.Updated = False
            FI_SinopacNotEmpDay.Updated = False
            FI_CheckInFlag.Updated = False
            FI_IsHLBFlag.Updated = False
            FI_PassExamFlag.Updated = False
            FI_Password.Updated = False
            FI_PwdErrCnt.Updated = False
            FI_Question.Updated = False
            FI_Answer.Updated = False
            FI_QuestionErrCnt.Updated = False
            FI_ExpireDate.Updated = False
            FI_LoginTime.Updated = False
            FI_RegStatus.Updated = False
            FI_RegIP.Updated = False
            FI_AOCode.Updated = False
            FI_EmpIDOld.Updated = False
            FI_LocalHireFlag.Updated = False
            FI_RankBeginDate.Updated = False
            FI_DeptBeginDate.Updated = False
            FI_PositionBeginDate.Updated = False
            FI_WorkTypeBeginDate.Updated = False
            FI_BossBeginDate.Updated = False
            FI_IDType.Updated = False
            FI_LastChgComp.Updated = False
            FI_LastChgID.Updated = False
            FI_LastChgDate.Updated = False
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

        Public ReadOnly Property IDNo As Field(Of String) 
            Get
                Return FI_IDNo
            End Get
        End Property

        Public ReadOnly Property Name As Field(Of String) 
            Get
                Return FI_Name
            End Get
        End Property

        Public ReadOnly Property NameN As Field(Of String) 
            Get
                Return FI_NameN
            End Get
        End Property

        Public ReadOnly Property NameB As Field(Of String) 
            Get
                Return FI_NameB
            End Get
        End Property

        Public ReadOnly Property EngName As Field(Of String) 
            Get
                Return FI_EngName
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

        Public ReadOnly Property RankIDMap As Field(Of String) 
            Get
                Return FI_RankIDMap
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

        Public ReadOnly Property SinopacQuitDate As Field(Of Date) 
            Get
                Return FI_SinopacQuitDate
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

        Public ReadOnly Property SinopacNotEmpDay As Field(Of Integer) 
            Get
                Return FI_SinopacNotEmpDay
            End Get
        End Property

        Public ReadOnly Property CheckInFlag As Field(Of String) 
            Get
                Return FI_CheckInFlag
            End Get
        End Property

        Public ReadOnly Property IsHLBFlag As Field(Of String) 
            Get
                Return FI_IsHLBFlag
            End Get
        End Property

        Public ReadOnly Property PassExamFlag As Field(Of String) 
            Get
                Return FI_PassExamFlag
            End Get
        End Property

        Public ReadOnly Property Password As Field(Of String) 
            Get
                Return FI_Password
            End Get
        End Property

        Public ReadOnly Property PwdErrCnt As Field(Of Integer) 
            Get
                Return FI_PwdErrCnt
            End Get
        End Property

        Public ReadOnly Property Question As Field(Of String) 
            Get
                Return FI_Question
            End Get
        End Property

        Public ReadOnly Property Answer As Field(Of String) 
            Get
                Return FI_Answer
            End Get
        End Property

        Public ReadOnly Property QuestionErrCnt As Field(Of Integer) 
            Get
                Return FI_QuestionErrCnt
            End Get
        End Property

        Public ReadOnly Property ExpireDate As Field(Of Date) 
            Get
                Return FI_ExpireDate
            End Get
        End Property

        Public ReadOnly Property LoginTime As Field(Of Date) 
            Get
                Return FI_LoginTime
            End Get
        End Property

        Public ReadOnly Property RegStatus As Field(Of String) 
            Get
                Return FI_RegStatus
            End Get
        End Property

        Public ReadOnly Property RegIP As Field(Of String) 
            Get
                Return FI_RegIP
            End Get
        End Property

        Public ReadOnly Property AOCode As Field(Of String) 
            Get
                Return FI_AOCode
            End Get
        End Property

        Public ReadOnly Property EmpIDOld As Field(Of String) 
            Get
                Return FI_EmpIDOld
            End Get
        End Property

        Public ReadOnly Property LocalHireFlag As Field(Of String) 
            Get
                Return FI_LocalHireFlag
            End Get
        End Property

        Public ReadOnly Property RankBeginDate As Field(Of Date) 
            Get
                Return FI_RankBeginDate
            End Get
        End Property

        Public ReadOnly Property DeptBeginDate As Field(Of Date) 
            Get
                Return FI_DeptBeginDate
            End Get
        End Property

        Public ReadOnly Property PositionBeginDate As Field(Of Date) 
            Get
                Return FI_PositionBeginDate
            End Get
        End Property

        Public ReadOnly Property WorkTypeBeginDate As Field(Of Date) 
            Get
                Return FI_WorkTypeBeginDate
            End Get
        End Property

        Public ReadOnly Property BossBeginDate As Field(Of Date) 
            Get
                Return FI_BossBeginDate
            End Get
        End Property

        Public ReadOnly Property IDType As Field(Of String) 
            Get
                Return FI_IDType
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
        Public Function DeleteRowByPrimaryKey(ByVal PersonalRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Delete From Personal")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And EmpID = @EmpID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, PersonalRow.CompID.Value)
            db.AddInParameter(dbcmd, "@EmpID", DbType.String, PersonalRow.EmpID.Value)

            return db.ExecuteNonQuery(dbcmd)
        End Function

        public Function DeleteRowByPrimaryKey(ByVal PersonalRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Delete From Personal")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And EmpID = @EmpID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, PersonalRow.CompID.Value)
            db.AddInParameter(dbcmd, "@EmpID", DbType.String, PersonalRow.EmpID.Value)

            return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function DeleteRowByPrimaryKey(ByVal PersonalRow As Row()) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Delete From Personal")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And EmpID = @EmpID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            using cn As DbConnection = db.CreateConnection()
                cn.Open()
                Dim tran As DbTransaction = cn.BeginTransaction()
                Dim inTrans As Boolean = True

                Try
                    For Each r As Row In PersonalRow
                        dbcmd.Parameters.Clear()
                        db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                        db.AddInParameter(dbcmd, "@EmpID", DbType.String, r.EmpID.Value)

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

        Public Function DeleteRowByPrimaryKey(ByVal PersonalRow As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Delete From Personal")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And EmpID = @EmpID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())

            For Each r As Row In PersonalRow
                dbcmd.Parameters.Clear()
                db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                db.AddInParameter(dbcmd, "@EmpID", DbType.String, r.EmpID.Value)

                intRowsAffected += db.ExecuteNonQuery(dbcmd, tran)
            Next
            Return intRowsAffected
        End Function

        Public Function QueryByKey(ByVal PersonalRow As Row) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From Personal")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And EmpID = @EmpID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, PersonalRow.CompID.Value)
            db.AddInParameter(dbcmd, "@EmpID", DbType.String, PersonalRow.EmpID.Value)

            Return db.ExecuteDataSet(dbcmd)
        End Function

        Public Function QueryByKey(PersonalRow As Row, tran As DbTransaction) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From Personal")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And EmpID = @EmpID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, PersonalRow.CompID.Value)
            db.AddInParameter(dbcmd, "@EmpID", DbType.String, PersonalRow.EmpID.Value)

            Return db.ExecuteDataSet(dbcmd, tran)
        End Function

        Public Function Update(ByVal PersonalRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim strDot As String = String.Empty

            strSQL.AppendLine("Update Personal Set")
            For i As Integer = 0 To PersonalRow.FieldNames.Length - 1
                If Not PersonalRow.IsIdentityField(PersonalRow.FieldNames(i)) AndAlso PersonalRow.IsUpdated(PersonalRow.FieldNames(i)) AndAlso PersonalRow.CreateUpdateSQL(PersonalRow.FieldNames(i)) Then
                    strSQL.AppendLine(string.Format("{0}{1} = @{1}", strDot, PersonalRow.FieldNames(i)))
                    strDot = ","
                End If
            Next
            If strDot = String.Empty Then Throw New Exception("未異動資料欄位！")
            strSQL.AppendLine("Where CompID = @PKCompID")
            strSQL.AppendLine("And EmpID = @PKEmpID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            If PersonalRow.CompID.Updated Then db.AddInParameter(dbcmd, "@CompID", DbType.String, PersonalRow.CompID.Value)
            If PersonalRow.EmpID.Updated Then db.AddInParameter(dbcmd, "@EmpID", DbType.String, PersonalRow.EmpID.Value)
            If PersonalRow.IDNo.Updated Then db.AddInParameter(dbcmd, "@IDNo", DbType.String, PersonalRow.IDNo.Value)
            If PersonalRow.Name.Updated Then db.AddInParameter(dbcmd, "@Name", DbType.String, PersonalRow.Name.Value)
            If PersonalRow.NameN.Updated Then db.AddInParameter(dbcmd, "@NameN", DbType.String, PersonalRow.NameN.Value)
            If PersonalRow.NameB.Updated Then db.AddInParameter(dbcmd, "@NameB", DbType.String, PersonalRow.NameB.Value)
            If PersonalRow.EngName.Updated Then db.AddInParameter(dbcmd, "@EngName", DbType.String, PersonalRow.EngName.Value)
            If PersonalRow.PassportName.Updated Then db.AddInParameter(dbcmd, "@PassportName", DbType.String, PersonalRow.PassportName.Value)
            If PersonalRow.BirthDate.Updated Then db.AddInParameter(dbcmd, "@BirthDate", DbType.Date, IIf(IsDateTimeNull(PersonalRow.BirthDate.Value), Convert.ToDateTime("1900/1/1"), PersonalRow.BirthDate.Value))
            If PersonalRow.Sex.Updated Then db.AddInParameter(dbcmd, "@Sex", DbType.String, PersonalRow.Sex.Value)
            If PersonalRow.NationID.Updated Then db.AddInParameter(dbcmd, "@NationID", DbType.String, PersonalRow.NationID.Value)
            If PersonalRow.IDExpireDate.Updated Then db.AddInParameter(dbcmd, "@IDExpireDate", DbType.Date, IIf(IsDateTimeNull(PersonalRow.IDExpireDate.Value), Convert.ToDateTime("1900/1/1"), PersonalRow.IDExpireDate.Value))
            If PersonalRow.EduID.Updated Then db.AddInParameter(dbcmd, "@EduID", DbType.String, PersonalRow.EduID.Value)
            If PersonalRow.Marriage.Updated Then db.AddInParameter(dbcmd, "@Marriage", DbType.String, PersonalRow.Marriage.Value)
            If PersonalRow.WorkStatus.Updated Then db.AddInParameter(dbcmd, "@WorkStatus", DbType.String, PersonalRow.WorkStatus.Value)
            If PersonalRow.EmpType.Updated Then db.AddInParameter(dbcmd, "@EmpType", DbType.String, PersonalRow.EmpType.Value)
            If PersonalRow.GroupID.Updated Then db.AddInParameter(dbcmd, "@GroupID", DbType.String, PersonalRow.GroupID.Value)
            If PersonalRow.DeptID.Updated Then db.AddInParameter(dbcmd, "@DeptID", DbType.String, PersonalRow.DeptID.Value)
            If PersonalRow.OrganID.Updated Then db.AddInParameter(dbcmd, "@OrganID", DbType.String, PersonalRow.OrganID.Value)
            If PersonalRow.WorkSiteID.Updated Then db.AddInParameter(dbcmd, "@WorkSiteID", DbType.String, PersonalRow.WorkSiteID.Value)
            If PersonalRow.RankID.Updated Then db.AddInParameter(dbcmd, "@RankID", DbType.String, PersonalRow.RankID.Value)
            If PersonalRow.RankIDMap.Updated Then db.AddInParameter(dbcmd, "@RankIDMap", DbType.String, PersonalRow.RankIDMap.Value)
            If PersonalRow.HoldingRankID.Updated Then db.AddInParameter(dbcmd, "@HoldingRankID", DbType.String, PersonalRow.HoldingRankID.Value)
            If PersonalRow.TitleID.Updated Then db.AddInParameter(dbcmd, "@TitleID", DbType.String, PersonalRow.TitleID.Value)
            If PersonalRow.PublicTitleID.Updated Then db.AddInParameter(dbcmd, "@PublicTitleID", DbType.String, PersonalRow.PublicTitleID.Value)
            If PersonalRow.EmpDate.Updated Then db.AddInParameter(dbcmd, "@EmpDate", DbType.Date, IIf(IsDateTimeNull(PersonalRow.EmpDate.Value), Convert.ToDateTime("1900/1/1"), PersonalRow.EmpDate.Value))
            If PersonalRow.SinopacEmpDate.Updated Then db.AddInParameter(dbcmd, "@SinopacEmpDate", DbType.Date, IIf(IsDateTimeNull(PersonalRow.SinopacEmpDate.Value), Convert.ToDateTime("1900/1/1"), PersonalRow.SinopacEmpDate.Value))
            If PersonalRow.QuitDate.Updated Then db.AddInParameter(dbcmd, "@QuitDate", DbType.Date, IIf(IsDateTimeNull(PersonalRow.QuitDate.Value), Convert.ToDateTime("1900/1/1"), PersonalRow.QuitDate.Value))
            If PersonalRow.SinopacQuitDate.Updated Then db.AddInParameter(dbcmd, "@SinopacQuitDate", DbType.Date, IIf(IsDateTimeNull(PersonalRow.SinopacQuitDate.Value), Convert.ToDateTime("1900/1/1"), PersonalRow.SinopacQuitDate.Value))
            If PersonalRow.ProbDate.Updated Then db.AddInParameter(dbcmd, "@ProbDate", DbType.Date, IIf(IsDateTimeNull(PersonalRow.ProbDate.Value), Convert.ToDateTime("1900/1/1"), PersonalRow.ProbDate.Value))
            If PersonalRow.ProbMonth.Updated Then db.AddInParameter(dbcmd, "@ProbMonth", DbType.Int32, PersonalRow.ProbMonth.Value)
            If PersonalRow.NotEmpDay.Updated Then db.AddInParameter(dbcmd, "@NotEmpDay", DbType.Int32, PersonalRow.NotEmpDay.Value)
            If PersonalRow.SinopacNotEmpDay.Updated Then db.AddInParameter(dbcmd, "@SinopacNotEmpDay", DbType.Int32, PersonalRow.SinopacNotEmpDay.Value)
            If PersonalRow.CheckInFlag.Updated Then db.AddInParameter(dbcmd, "@CheckInFlag", DbType.String, PersonalRow.CheckInFlag.Value)
            If PersonalRow.IsHLBFlag.Updated Then db.AddInParameter(dbcmd, "@IsHLBFlag", DbType.String, PersonalRow.IsHLBFlag.Value)
            If PersonalRow.PassExamFlag.Updated Then db.AddInParameter(dbcmd, "@PassExamFlag", DbType.String, PersonalRow.PassExamFlag.Value)
            If PersonalRow.Password.Updated Then db.AddInParameter(dbcmd, "@Password", DbType.String, PersonalRow.Password.Value)
            If PersonalRow.PwdErrCnt.Updated Then db.AddInParameter(dbcmd, "@PwdErrCnt", DbType.Int32, PersonalRow.PwdErrCnt.Value)
            If PersonalRow.Question.Updated Then db.AddInParameter(dbcmd, "@Question", DbType.String, PersonalRow.Question.Value)
            If PersonalRow.Answer.Updated Then db.AddInParameter(dbcmd, "@Answer", DbType.String, PersonalRow.Answer.Value)
            If PersonalRow.QuestionErrCnt.Updated Then db.AddInParameter(dbcmd, "@QuestionErrCnt", DbType.Int32, PersonalRow.QuestionErrCnt.Value)
            If PersonalRow.ExpireDate.Updated Then db.AddInParameter(dbcmd, "@ExpireDate", DbType.Date, IIf(IsDateTimeNull(PersonalRow.ExpireDate.Value), Convert.ToDateTime("1900/1/1"), PersonalRow.ExpireDate.Value))
            If PersonalRow.LoginTime.Updated Then db.AddInParameter(dbcmd, "@LoginTime", DbType.Date, IIf(IsDateTimeNull(PersonalRow.LoginTime.Value), Convert.ToDateTime("1900/1/1"), PersonalRow.LoginTime.Value))
            If PersonalRow.RegStatus.Updated Then db.AddInParameter(dbcmd, "@RegStatus", DbType.String, PersonalRow.RegStatus.Value)
            If PersonalRow.RegIP.Updated Then db.AddInParameter(dbcmd, "@RegIP", DbType.String, PersonalRow.RegIP.Value)
            If PersonalRow.AOCode.Updated Then db.AddInParameter(dbcmd, "@AOCode", DbType.String, PersonalRow.AOCode.Value)
            If PersonalRow.EmpIDOld.Updated Then db.AddInParameter(dbcmd, "@EmpIDOld", DbType.String, PersonalRow.EmpIDOld.Value)
            If PersonalRow.LocalHireFlag.Updated Then db.AddInParameter(dbcmd, "@LocalHireFlag", DbType.String, PersonalRow.LocalHireFlag.Value)
            If PersonalRow.RankBeginDate.Updated Then db.AddInParameter(dbcmd, "@RankBeginDate", DbType.Date, IIf(IsDateTimeNull(PersonalRow.RankBeginDate.Value), Convert.ToDateTime("1900/1/1"), PersonalRow.RankBeginDate.Value))
            If PersonalRow.DeptBeginDate.Updated Then db.AddInParameter(dbcmd, "@DeptBeginDate", DbType.Date, IIf(IsDateTimeNull(PersonalRow.DeptBeginDate.Value), Convert.ToDateTime("1900/1/1"), PersonalRow.DeptBeginDate.Value))
            If PersonalRow.PositionBeginDate.Updated Then db.AddInParameter(dbcmd, "@PositionBeginDate", DbType.Date, IIf(IsDateTimeNull(PersonalRow.PositionBeginDate.Value), Convert.ToDateTime("1900/1/1"), PersonalRow.PositionBeginDate.Value))
            If PersonalRow.WorkTypeBeginDate.Updated Then db.AddInParameter(dbcmd, "@WorkTypeBeginDate", DbType.Date, IIf(IsDateTimeNull(PersonalRow.WorkTypeBeginDate.Value), Convert.ToDateTime("1900/1/1"), PersonalRow.WorkTypeBeginDate.Value))
            If PersonalRow.BossBeginDate.Updated Then db.AddInParameter(dbcmd, "@BossBeginDate", DbType.Date, IIf(IsDateTimeNull(PersonalRow.BossBeginDate.Value), Convert.ToDateTime("1900/1/1"), PersonalRow.BossBeginDate.Value))
            If PersonalRow.IDType.Updated Then db.AddInParameter(dbcmd, "@IDType", DbType.String, PersonalRow.IDType.Value)
            If PersonalRow.LastChgComp.Updated Then db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, PersonalRow.LastChgComp.Value)
            If PersonalRow.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, PersonalRow.LastChgID.Value)
            If PersonalRow.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(PersonalRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), PersonalRow.LastChgDate.Value))
            db.AddInParameter(dbcmd, "@PKCompID", DbType.String, IIf(PersonalRow.LoadFromDataRow, PersonalRow.CompID.OldValue, PersonalRow.CompID.Value))
            db.AddInParameter(dbcmd, "@PKEmpID", DbType.String, IIf(PersonalRow.LoadFromDataRow, PersonalRow.EmpID.OldValue, PersonalRow.EmpID.Value))

            Return db.ExecuteNonQuery(dbcmd)
        End Function

        Public Function Update(ByVal PersonalRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim strDot As String = String.Empty

            strSQL.AppendLine("Update Personal Set")
            For i As Integer = 0 To PersonalRow.FieldNames.Length - 1
                If Not PersonalRow.IsIdentityField(PersonalRow.FieldNames(i)) AndAlso PersonalRow.IsUpdated(PersonalRow.FieldNames(i)) AndAlso PersonalRow.CreateUpdateSQL(PersonalRow.FieldNames(i)) Then
                    strSQL.AppendLine(string.Format("{0}{1} = @{1}", strDot, PersonalRow.FieldNames(i)))
                    strDot = ","
                End If
            Next 
            If strDot = String.Empty Then Throw New Exception("未異動資料欄位！")
            strSQL.AppendLine("Where CompID = @PKCompID")
            strSQL.AppendLine("And EmpID = @PKEmpID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            If PersonalRow.CompID.Updated Then db.AddInParameter(dbcmd, "@CompID", DbType.String, PersonalRow.CompID.Value)
            If PersonalRow.EmpID.Updated Then db.AddInParameter(dbcmd, "@EmpID", DbType.String, PersonalRow.EmpID.Value)
            If PersonalRow.IDNo.Updated Then db.AddInParameter(dbcmd, "@IDNo", DbType.String, PersonalRow.IDNo.Value)
            If PersonalRow.Name.Updated Then db.AddInParameter(dbcmd, "@Name", DbType.String, PersonalRow.Name.Value)
            If PersonalRow.NameN.Updated Then db.AddInParameter(dbcmd, "@NameN", DbType.String, PersonalRow.NameN.Value)
            If PersonalRow.NameB.Updated Then db.AddInParameter(dbcmd, "@NameB", DbType.String, PersonalRow.NameB.Value)
            If PersonalRow.EngName.Updated Then db.AddInParameter(dbcmd, "@EngName", DbType.String, PersonalRow.EngName.Value)
            If PersonalRow.PassportName.Updated Then db.AddInParameter(dbcmd, "@PassportName", DbType.String, PersonalRow.PassportName.Value)
            If PersonalRow.BirthDate.Updated Then db.AddInParameter(dbcmd, "@BirthDate", DbType.Date, IIf(IsDateTimeNull(PersonalRow.BirthDate.Value), Convert.ToDateTime("1900/1/1"), PersonalRow.BirthDate.Value))
            If PersonalRow.Sex.Updated Then db.AddInParameter(dbcmd, "@Sex", DbType.String, PersonalRow.Sex.Value)
            If PersonalRow.NationID.Updated Then db.AddInParameter(dbcmd, "@NationID", DbType.String, PersonalRow.NationID.Value)
            If PersonalRow.IDExpireDate.Updated Then db.AddInParameter(dbcmd, "@IDExpireDate", DbType.Date, IIf(IsDateTimeNull(PersonalRow.IDExpireDate.Value), Convert.ToDateTime("1900/1/1"), PersonalRow.IDExpireDate.Value))
            If PersonalRow.EduID.Updated Then db.AddInParameter(dbcmd, "@EduID", DbType.String, PersonalRow.EduID.Value)
            If PersonalRow.Marriage.Updated Then db.AddInParameter(dbcmd, "@Marriage", DbType.String, PersonalRow.Marriage.Value)
            If PersonalRow.WorkStatus.Updated Then db.AddInParameter(dbcmd, "@WorkStatus", DbType.String, PersonalRow.WorkStatus.Value)
            If PersonalRow.EmpType.Updated Then db.AddInParameter(dbcmd, "@EmpType", DbType.String, PersonalRow.EmpType.Value)
            If PersonalRow.GroupID.Updated Then db.AddInParameter(dbcmd, "@GroupID", DbType.String, PersonalRow.GroupID.Value)
            If PersonalRow.DeptID.Updated Then db.AddInParameter(dbcmd, "@DeptID", DbType.String, PersonalRow.DeptID.Value)
            If PersonalRow.OrganID.Updated Then db.AddInParameter(dbcmd, "@OrganID", DbType.String, PersonalRow.OrganID.Value)
            If PersonalRow.WorkSiteID.Updated Then db.AddInParameter(dbcmd, "@WorkSiteID", DbType.String, PersonalRow.WorkSiteID.Value)
            If PersonalRow.RankID.Updated Then db.AddInParameter(dbcmd, "@RankID", DbType.String, PersonalRow.RankID.Value)
            If PersonalRow.RankIDMap.Updated Then db.AddInParameter(dbcmd, "@RankIDMap", DbType.String, PersonalRow.RankIDMap.Value)
            If PersonalRow.HoldingRankID.Updated Then db.AddInParameter(dbcmd, "@HoldingRankID", DbType.String, PersonalRow.HoldingRankID.Value)
            If PersonalRow.TitleID.Updated Then db.AddInParameter(dbcmd, "@TitleID", DbType.String, PersonalRow.TitleID.Value)
            If PersonalRow.PublicTitleID.Updated Then db.AddInParameter(dbcmd, "@PublicTitleID", DbType.String, PersonalRow.PublicTitleID.Value)
            If PersonalRow.EmpDate.Updated Then db.AddInParameter(dbcmd, "@EmpDate", DbType.Date, IIf(IsDateTimeNull(PersonalRow.EmpDate.Value), Convert.ToDateTime("1900/1/1"), PersonalRow.EmpDate.Value))
            If PersonalRow.SinopacEmpDate.Updated Then db.AddInParameter(dbcmd, "@SinopacEmpDate", DbType.Date, IIf(IsDateTimeNull(PersonalRow.SinopacEmpDate.Value), Convert.ToDateTime("1900/1/1"), PersonalRow.SinopacEmpDate.Value))
            If PersonalRow.QuitDate.Updated Then db.AddInParameter(dbcmd, "@QuitDate", DbType.Date, IIf(IsDateTimeNull(PersonalRow.QuitDate.Value), Convert.ToDateTime("1900/1/1"), PersonalRow.QuitDate.Value))
            If PersonalRow.SinopacQuitDate.Updated Then db.AddInParameter(dbcmd, "@SinopacQuitDate", DbType.Date, IIf(IsDateTimeNull(PersonalRow.SinopacQuitDate.Value), Convert.ToDateTime("1900/1/1"), PersonalRow.SinopacQuitDate.Value))
            If PersonalRow.ProbDate.Updated Then db.AddInParameter(dbcmd, "@ProbDate", DbType.Date, IIf(IsDateTimeNull(PersonalRow.ProbDate.Value), Convert.ToDateTime("1900/1/1"), PersonalRow.ProbDate.Value))
            If PersonalRow.ProbMonth.Updated Then db.AddInParameter(dbcmd, "@ProbMonth", DbType.Int32, PersonalRow.ProbMonth.Value)
            If PersonalRow.NotEmpDay.Updated Then db.AddInParameter(dbcmd, "@NotEmpDay", DbType.Int32, PersonalRow.NotEmpDay.Value)
            If PersonalRow.SinopacNotEmpDay.Updated Then db.AddInParameter(dbcmd, "@SinopacNotEmpDay", DbType.Int32, PersonalRow.SinopacNotEmpDay.Value)
            If PersonalRow.CheckInFlag.Updated Then db.AddInParameter(dbcmd, "@CheckInFlag", DbType.String, PersonalRow.CheckInFlag.Value)
            If PersonalRow.IsHLBFlag.Updated Then db.AddInParameter(dbcmd, "@IsHLBFlag", DbType.String, PersonalRow.IsHLBFlag.Value)
            If PersonalRow.PassExamFlag.Updated Then db.AddInParameter(dbcmd, "@PassExamFlag", DbType.String, PersonalRow.PassExamFlag.Value)
            If PersonalRow.Password.Updated Then db.AddInParameter(dbcmd, "@Password", DbType.String, PersonalRow.Password.Value)
            If PersonalRow.PwdErrCnt.Updated Then db.AddInParameter(dbcmd, "@PwdErrCnt", DbType.Int32, PersonalRow.PwdErrCnt.Value)
            If PersonalRow.Question.Updated Then db.AddInParameter(dbcmd, "@Question", DbType.String, PersonalRow.Question.Value)
            If PersonalRow.Answer.Updated Then db.AddInParameter(dbcmd, "@Answer", DbType.String, PersonalRow.Answer.Value)
            If PersonalRow.QuestionErrCnt.Updated Then db.AddInParameter(dbcmd, "@QuestionErrCnt", DbType.Int32, PersonalRow.QuestionErrCnt.Value)
            If PersonalRow.ExpireDate.Updated Then db.AddInParameter(dbcmd, "@ExpireDate", DbType.Date, IIf(IsDateTimeNull(PersonalRow.ExpireDate.Value), Convert.ToDateTime("1900/1/1"), PersonalRow.ExpireDate.Value))
            If PersonalRow.LoginTime.Updated Then db.AddInParameter(dbcmd, "@LoginTime", DbType.Date, IIf(IsDateTimeNull(PersonalRow.LoginTime.Value), Convert.ToDateTime("1900/1/1"), PersonalRow.LoginTime.Value))
            If PersonalRow.RegStatus.Updated Then db.AddInParameter(dbcmd, "@RegStatus", DbType.String, PersonalRow.RegStatus.Value)
            If PersonalRow.RegIP.Updated Then db.AddInParameter(dbcmd, "@RegIP", DbType.String, PersonalRow.RegIP.Value)
            If PersonalRow.AOCode.Updated Then db.AddInParameter(dbcmd, "@AOCode", DbType.String, PersonalRow.AOCode.Value)
            If PersonalRow.EmpIDOld.Updated Then db.AddInParameter(dbcmd, "@EmpIDOld", DbType.String, PersonalRow.EmpIDOld.Value)
            If PersonalRow.LocalHireFlag.Updated Then db.AddInParameter(dbcmd, "@LocalHireFlag", DbType.String, PersonalRow.LocalHireFlag.Value)
            If PersonalRow.RankBeginDate.Updated Then db.AddInParameter(dbcmd, "@RankBeginDate", DbType.Date, IIf(IsDateTimeNull(PersonalRow.RankBeginDate.Value), Convert.ToDateTime("1900/1/1"), PersonalRow.RankBeginDate.Value))
            If PersonalRow.DeptBeginDate.Updated Then db.AddInParameter(dbcmd, "@DeptBeginDate", DbType.Date, IIf(IsDateTimeNull(PersonalRow.DeptBeginDate.Value), Convert.ToDateTime("1900/1/1"), PersonalRow.DeptBeginDate.Value))
            If PersonalRow.PositionBeginDate.Updated Then db.AddInParameter(dbcmd, "@PositionBeginDate", DbType.Date, IIf(IsDateTimeNull(PersonalRow.PositionBeginDate.Value), Convert.ToDateTime("1900/1/1"), PersonalRow.PositionBeginDate.Value))
            If PersonalRow.WorkTypeBeginDate.Updated Then db.AddInParameter(dbcmd, "@WorkTypeBeginDate", DbType.Date, IIf(IsDateTimeNull(PersonalRow.WorkTypeBeginDate.Value), Convert.ToDateTime("1900/1/1"), PersonalRow.WorkTypeBeginDate.Value))
            If PersonalRow.BossBeginDate.Updated Then db.AddInParameter(dbcmd, "@BossBeginDate", DbType.Date, IIf(IsDateTimeNull(PersonalRow.BossBeginDate.Value), Convert.ToDateTime("1900/1/1"), PersonalRow.BossBeginDate.Value))
            If PersonalRow.IDType.Updated Then db.AddInParameter(dbcmd, "@IDType", DbType.String, PersonalRow.IDType.Value)
            If PersonalRow.LastChgComp.Updated Then db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, PersonalRow.LastChgComp.Value)
            If PersonalRow.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, PersonalRow.LastChgID.Value)
            If PersonalRow.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(PersonalRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), PersonalRow.LastChgDate.Value))
            db.AddInParameter(dbcmd, "@PKCompID", DbType.String, IIf(PersonalRow.LoadFromDataRow, PersonalRow.CompID.OldValue, PersonalRow.CompID.Value))
            db.AddInParameter(dbcmd, "@PKEmpID", DbType.String, IIf(PersonalRow.LoadFromDataRow, PersonalRow.EmpID.OldValue, PersonalRow.EmpID.Value))

            Return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function Update(ByVal PersonalRow As Row()) As Integer
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
                    For Each r As Row In PersonalRow
                        strDot = String.Empty
                        If strSQL.Length > 0 Then strSQL.Remove(0, strSQL.Length)
                        strSQL.AppendLine("Update Personal Set")
                        For i As Integer = 0 To r.FieldNames.Length - 1
                            If Not r.IsIdentityField(r.FieldNames(i)) AndAlso r.IsUpdated(r.FieldNames(i)) AndAlso r.CreateUpdateSQL(r.FieldNames(i)) Then
                                strSQL.AppendLine(String.Format("{0}{1} = @{1}", strDot, r.FieldNames(i)))
                                strDot = ","
                            End If
                        Next
                        If strDot = String.Empty Then Continue For
                        strSQL.AppendLine("Where CompID = @PKCompID")
                        strSQL.AppendLine("And EmpID = @PKEmpID")

                        dbcmd = db.GetSqlStringCommand(strSQL.ToString())
                        If r.CompID.Updated Then db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                        If r.EmpID.Updated Then db.AddInParameter(dbcmd, "@EmpID", DbType.String, r.EmpID.Value)
                        If r.IDNo.Updated Then db.AddInParameter(dbcmd, "@IDNo", DbType.String, r.IDNo.Value)
                        If r.Name.Updated Then db.AddInParameter(dbcmd, "@Name", DbType.String, r.Name.Value)
                        If r.NameN.Updated Then db.AddInParameter(dbcmd, "@NameN", DbType.String, r.NameN.Value)
                        If r.NameB.Updated Then db.AddInParameter(dbcmd, "@NameB", DbType.String, r.NameB.Value)
                        If r.EngName.Updated Then db.AddInParameter(dbcmd, "@EngName", DbType.String, r.EngName.Value)
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
                        If r.RankIDMap.Updated Then db.AddInParameter(dbcmd, "@RankIDMap", DbType.String, r.RankIDMap.Value)
                        If r.HoldingRankID.Updated Then db.AddInParameter(dbcmd, "@HoldingRankID", DbType.String, r.HoldingRankID.Value)
                        If r.TitleID.Updated Then db.AddInParameter(dbcmd, "@TitleID", DbType.String, r.TitleID.Value)
                        If r.PublicTitleID.Updated Then db.AddInParameter(dbcmd, "@PublicTitleID", DbType.String, r.PublicTitleID.Value)
                        If r.EmpDate.Updated Then db.AddInParameter(dbcmd, "@EmpDate", DbType.Date, IIf(IsDateTimeNull(r.EmpDate.Value), Convert.ToDateTime("1900/1/1"), r.EmpDate.Value))
                        If r.SinopacEmpDate.Updated Then db.AddInParameter(dbcmd, "@SinopacEmpDate", DbType.Date, IIf(IsDateTimeNull(r.SinopacEmpDate.Value), Convert.ToDateTime("1900/1/1"), r.SinopacEmpDate.Value))
                        If r.QuitDate.Updated Then db.AddInParameter(dbcmd, "@QuitDate", DbType.Date, IIf(IsDateTimeNull(r.QuitDate.Value), Convert.ToDateTime("1900/1/1"), r.QuitDate.Value))
                        If r.SinopacQuitDate.Updated Then db.AddInParameter(dbcmd, "@SinopacQuitDate", DbType.Date, IIf(IsDateTimeNull(r.SinopacQuitDate.Value), Convert.ToDateTime("1900/1/1"), r.SinopacQuitDate.Value))
                        If r.ProbDate.Updated Then db.AddInParameter(dbcmd, "@ProbDate", DbType.Date, IIf(IsDateTimeNull(r.ProbDate.Value), Convert.ToDateTime("1900/1/1"), r.ProbDate.Value))
                        If r.ProbMonth.Updated Then db.AddInParameter(dbcmd, "@ProbMonth", DbType.Int32, r.ProbMonth.Value)
                        If r.NotEmpDay.Updated Then db.AddInParameter(dbcmd, "@NotEmpDay", DbType.Int32, r.NotEmpDay.Value)
                        If r.SinopacNotEmpDay.Updated Then db.AddInParameter(dbcmd, "@SinopacNotEmpDay", DbType.Int32, r.SinopacNotEmpDay.Value)
                        If r.CheckInFlag.Updated Then db.AddInParameter(dbcmd, "@CheckInFlag", DbType.String, r.CheckInFlag.Value)
                        If r.IsHLBFlag.Updated Then db.AddInParameter(dbcmd, "@IsHLBFlag", DbType.String, r.IsHLBFlag.Value)
                        If r.PassExamFlag.Updated Then db.AddInParameter(dbcmd, "@PassExamFlag", DbType.String, r.PassExamFlag.Value)
                        If r.Password.Updated Then db.AddInParameter(dbcmd, "@Password", DbType.String, r.Password.Value)
                        If r.PwdErrCnt.Updated Then db.AddInParameter(dbcmd, "@PwdErrCnt", DbType.Int32, r.PwdErrCnt.Value)
                        If r.Question.Updated Then db.AddInParameter(dbcmd, "@Question", DbType.String, r.Question.Value)
                        If r.Answer.Updated Then db.AddInParameter(dbcmd, "@Answer", DbType.String, r.Answer.Value)
                        If r.QuestionErrCnt.Updated Then db.AddInParameter(dbcmd, "@QuestionErrCnt", DbType.Int32, r.QuestionErrCnt.Value)
                        If r.ExpireDate.Updated Then db.AddInParameter(dbcmd, "@ExpireDate", DbType.Date, IIf(IsDateTimeNull(r.ExpireDate.Value), Convert.ToDateTime("1900/1/1"), r.ExpireDate.Value))
                        If r.LoginTime.Updated Then db.AddInParameter(dbcmd, "@LoginTime", DbType.Date, IIf(IsDateTimeNull(r.LoginTime.Value), Convert.ToDateTime("1900/1/1"), r.LoginTime.Value))
                        If r.RegStatus.Updated Then db.AddInParameter(dbcmd, "@RegStatus", DbType.String, r.RegStatus.Value)
                        If r.RegIP.Updated Then db.AddInParameter(dbcmd, "@RegIP", DbType.String, r.RegIP.Value)
                        If r.AOCode.Updated Then db.AddInParameter(dbcmd, "@AOCode", DbType.String, r.AOCode.Value)
                        If r.EmpIDOld.Updated Then db.AddInParameter(dbcmd, "@EmpIDOld", DbType.String, r.EmpIDOld.Value)
                        If r.LocalHireFlag.Updated Then db.AddInParameter(dbcmd, "@LocalHireFlag", DbType.String, r.LocalHireFlag.Value)
                        If r.RankBeginDate.Updated Then db.AddInParameter(dbcmd, "@RankBeginDate", DbType.Date, IIf(IsDateTimeNull(r.RankBeginDate.Value), Convert.ToDateTime("1900/1/1"), r.RankBeginDate.Value))
                        If r.DeptBeginDate.Updated Then db.AddInParameter(dbcmd, "@DeptBeginDate", DbType.Date, IIf(IsDateTimeNull(r.DeptBeginDate.Value), Convert.ToDateTime("1900/1/1"), r.DeptBeginDate.Value))
                        If r.PositionBeginDate.Updated Then db.AddInParameter(dbcmd, "@PositionBeginDate", DbType.Date, IIf(IsDateTimeNull(r.PositionBeginDate.Value), Convert.ToDateTime("1900/1/1"), r.PositionBeginDate.Value))
                        If r.WorkTypeBeginDate.Updated Then db.AddInParameter(dbcmd, "@WorkTypeBeginDate", DbType.Date, IIf(IsDateTimeNull(r.WorkTypeBeginDate.Value), Convert.ToDateTime("1900/1/1"), r.WorkTypeBeginDate.Value))
                        If r.BossBeginDate.Updated Then db.AddInParameter(dbcmd, "@BossBeginDate", DbType.Date, IIf(IsDateTimeNull(r.BossBeginDate.Value), Convert.ToDateTime("1900/1/1"), r.BossBeginDate.Value))
                        If r.IDType.Updated Then db.AddInParameter(dbcmd, "@IDType", DbType.String, r.IDType.Value)
                        If r.LastChgComp.Updated Then db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, r.LastChgComp.Value)
                        If r.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, r.LastChgID.Value)
                        If r.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(r.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), r.LastChgDate.Value))
                        db.AddInParameter(dbcmd, "@PKCompID", DbType.String, IIf(r.LoadFromDataRow, r.CompID.OldValue, r.CompID.Value))
                        db.AddInParameter(dbcmd, "@PKEmpID", DbType.String, IIf(r.LoadFromDataRow, r.EmpID.OldValue, r.EmpID.Value))

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

        Public Function Update(ByVal PersonalRow As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0
            Dim strDot As String

            For Each r As Row In PersonalRow
                strDot = String.Empty
                If strSQL.Length > 0 Then strSQL.Remove(0, strSQL.Length)
                strSQL.AppendLine("Update Personal Set")
                For i As Integer = 0 To r.FieldNames.Length - 1
                    If Not r.IsIdentityField(r.FieldNames(i)) AndAlso r.IsUpdated(r.FieldNames(i)) AndAlso r.CreateUpdateSQL(r.FieldNames(i))
                        strSQL.AppendLine(String.Format("{0}{1} = @{1}", strDot, r.FieldNames(i)))
                        strDot = ","
                    End If
                Next
                If strDot = String.Empty Then Continue For
                strSQL.AppendLine("Where CompID = @PKCompID")
                strSQL.AppendLine("And EmpID = @PKEmpID")

                dbcmd = db.GetSqlStringCommand(strSQL.ToString())
                If r.CompID.Updated Then db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                If r.EmpID.Updated Then db.AddInParameter(dbcmd, "@EmpID", DbType.String, r.EmpID.Value)
                If r.IDNo.Updated Then db.AddInParameter(dbcmd, "@IDNo", DbType.String, r.IDNo.Value)
                If r.Name.Updated Then db.AddInParameter(dbcmd, "@Name", DbType.String, r.Name.Value)
                If r.NameN.Updated Then db.AddInParameter(dbcmd, "@NameN", DbType.String, r.NameN.Value)
                If r.NameB.Updated Then db.AddInParameter(dbcmd, "@NameB", DbType.String, r.NameB.Value)
                If r.EngName.Updated Then db.AddInParameter(dbcmd, "@EngName", DbType.String, r.EngName.Value)
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
                If r.RankIDMap.Updated Then db.AddInParameter(dbcmd, "@RankIDMap", DbType.String, r.RankIDMap.Value)
                If r.HoldingRankID.Updated Then db.AddInParameter(dbcmd, "@HoldingRankID", DbType.String, r.HoldingRankID.Value)
                If r.TitleID.Updated Then db.AddInParameter(dbcmd, "@TitleID", DbType.String, r.TitleID.Value)
                If r.PublicTitleID.Updated Then db.AddInParameter(dbcmd, "@PublicTitleID", DbType.String, r.PublicTitleID.Value)
                If r.EmpDate.Updated Then db.AddInParameter(dbcmd, "@EmpDate", DbType.Date, IIf(IsDateTimeNull(r.EmpDate.Value), Convert.ToDateTime("1900/1/1"), r.EmpDate.Value))
                If r.SinopacEmpDate.Updated Then db.AddInParameter(dbcmd, "@SinopacEmpDate", DbType.Date, IIf(IsDateTimeNull(r.SinopacEmpDate.Value), Convert.ToDateTime("1900/1/1"), r.SinopacEmpDate.Value))
                If r.QuitDate.Updated Then db.AddInParameter(dbcmd, "@QuitDate", DbType.Date, IIf(IsDateTimeNull(r.QuitDate.Value), Convert.ToDateTime("1900/1/1"), r.QuitDate.Value))
                If r.SinopacQuitDate.Updated Then db.AddInParameter(dbcmd, "@SinopacQuitDate", DbType.Date, IIf(IsDateTimeNull(r.SinopacQuitDate.Value), Convert.ToDateTime("1900/1/1"), r.SinopacQuitDate.Value))
                If r.ProbDate.Updated Then db.AddInParameter(dbcmd, "@ProbDate", DbType.Date, IIf(IsDateTimeNull(r.ProbDate.Value), Convert.ToDateTime("1900/1/1"), r.ProbDate.Value))
                If r.ProbMonth.Updated Then db.AddInParameter(dbcmd, "@ProbMonth", DbType.Int32, r.ProbMonth.Value)
                If r.NotEmpDay.Updated Then db.AddInParameter(dbcmd, "@NotEmpDay", DbType.Int32, r.NotEmpDay.Value)
                If r.SinopacNotEmpDay.Updated Then db.AddInParameter(dbcmd, "@SinopacNotEmpDay", DbType.Int32, r.SinopacNotEmpDay.Value)
                If r.CheckInFlag.Updated Then db.AddInParameter(dbcmd, "@CheckInFlag", DbType.String, r.CheckInFlag.Value)
                If r.IsHLBFlag.Updated Then db.AddInParameter(dbcmd, "@IsHLBFlag", DbType.String, r.IsHLBFlag.Value)
                If r.PassExamFlag.Updated Then db.AddInParameter(dbcmd, "@PassExamFlag", DbType.String, r.PassExamFlag.Value)
                If r.Password.Updated Then db.AddInParameter(dbcmd, "@Password", DbType.String, r.Password.Value)
                If r.PwdErrCnt.Updated Then db.AddInParameter(dbcmd, "@PwdErrCnt", DbType.Int32, r.PwdErrCnt.Value)
                If r.Question.Updated Then db.AddInParameter(dbcmd, "@Question", DbType.String, r.Question.Value)
                If r.Answer.Updated Then db.AddInParameter(dbcmd, "@Answer", DbType.String, r.Answer.Value)
                If r.QuestionErrCnt.Updated Then db.AddInParameter(dbcmd, "@QuestionErrCnt", DbType.Int32, r.QuestionErrCnt.Value)
                If r.ExpireDate.Updated Then db.AddInParameter(dbcmd, "@ExpireDate", DbType.Date, IIf(IsDateTimeNull(r.ExpireDate.Value), Convert.ToDateTime("1900/1/1"), r.ExpireDate.Value))
                If r.LoginTime.Updated Then db.AddInParameter(dbcmd, "@LoginTime", DbType.Date, IIf(IsDateTimeNull(r.LoginTime.Value), Convert.ToDateTime("1900/1/1"), r.LoginTime.Value))
                If r.RegStatus.Updated Then db.AddInParameter(dbcmd, "@RegStatus", DbType.String, r.RegStatus.Value)
                If r.RegIP.Updated Then db.AddInParameter(dbcmd, "@RegIP", DbType.String, r.RegIP.Value)
                If r.AOCode.Updated Then db.AddInParameter(dbcmd, "@AOCode", DbType.String, r.AOCode.Value)
                If r.EmpIDOld.Updated Then db.AddInParameter(dbcmd, "@EmpIDOld", DbType.String, r.EmpIDOld.Value)
                If r.LocalHireFlag.Updated Then db.AddInParameter(dbcmd, "@LocalHireFlag", DbType.String, r.LocalHireFlag.Value)
                If r.RankBeginDate.Updated Then db.AddInParameter(dbcmd, "@RankBeginDate", DbType.Date, IIf(IsDateTimeNull(r.RankBeginDate.Value), Convert.ToDateTime("1900/1/1"), r.RankBeginDate.Value))
                If r.DeptBeginDate.Updated Then db.AddInParameter(dbcmd, "@DeptBeginDate", DbType.Date, IIf(IsDateTimeNull(r.DeptBeginDate.Value), Convert.ToDateTime("1900/1/1"), r.DeptBeginDate.Value))
                If r.PositionBeginDate.Updated Then db.AddInParameter(dbcmd, "@PositionBeginDate", DbType.Date, IIf(IsDateTimeNull(r.PositionBeginDate.Value), Convert.ToDateTime("1900/1/1"), r.PositionBeginDate.Value))
                If r.WorkTypeBeginDate.Updated Then db.AddInParameter(dbcmd, "@WorkTypeBeginDate", DbType.Date, IIf(IsDateTimeNull(r.WorkTypeBeginDate.Value), Convert.ToDateTime("1900/1/1"), r.WorkTypeBeginDate.Value))
                If r.BossBeginDate.Updated Then db.AddInParameter(dbcmd, "@BossBeginDate", DbType.Date, IIf(IsDateTimeNull(r.BossBeginDate.Value), Convert.ToDateTime("1900/1/1"), r.BossBeginDate.Value))
                If r.IDType.Updated Then db.AddInParameter(dbcmd, "@IDType", DbType.String, r.IDType.Value)
                If r.LastChgComp.Updated Then db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, r.LastChgComp.Value)
                If r.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, r.LastChgID.Value)
                If r.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(r.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), r.LastChgDate.Value))
                db.AddInParameter(dbcmd, "@PKCompID", DbType.String, IIf(r.LoadFromDataRow, r.CompID.OldValue, r.CompID.Value))
                db.AddInParameter(dbcmd, "@PKEmpID", DbType.String, IIf(r.LoadFromDataRow, r.EmpID.OldValue, r.EmpID.Value))

                intRowsAffected += db.ExecuteNonQuery(dbcmd, tran)
            Next
            Return intRowsAffected
        End Function

        Public Function IsDataExists(ByVal PersonalRow As Row) As Boolean
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intCount As Integer = 0

            strSQL.AppendLine("Select Count(*) Cnt From Personal")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And EmpID = @EmpID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, PersonalRow.CompID.Value)
            db.AddInParameter(dbcmd, "@EmpID", DbType.String, PersonalRow.EmpID.Value)

            intCount = Convert.ToInt32(db.ExecuteScalar(dbcmd))
            Return IIf(intCount > 0, True, False)
        End Function

        Public Function IsDataExists(ByVal PersonalRow As Row, ByVal tran As DbTransaction) As Boolean
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intCount As Integer = 0

            strSQL.AppendLine("Select Count(*) Cnt From Personal")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And EmpID = @EmpID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, PersonalRow.CompID.Value)
            db.AddInParameter(dbcmd, "@EmpID", DbType.String, PersonalRow.EmpID.Value)

            intCount = Convert.ToInt32(db.ExecuteScalar(dbcmd, tran))
            Return IIf(intCount > 0, True, False)
        End Function

        Public Function QuerybyWhere(WhereCondition As String) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From Personal")
            If WhereCondition <> String.Empty Then strSQL.AppendLine(WhereCondition)

            Return db.ExecuteDataSet(CommandType.Text, strSQL.ToString())
        End Function

        Public Function Insert(ByVal PersonalRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Insert into Personal")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    CompID, EmpID, IDNo, Name, NameN, NameB, EngName, PassportName, BirthDate, Sex, NationID,")
            strSQL.AppendLine("    IDExpireDate, EduID, Marriage, WorkStatus, EmpType, GroupID, DeptID, OrganID, WorkSiteID,")
            strSQL.AppendLine("    RankID, RankIDMap, HoldingRankID, TitleID, PublicTitleID, EmpDate, SinopacEmpDate, QuitDate,")
            strSQL.AppendLine("    SinopacQuitDate, ProbDate, ProbMonth, NotEmpDay, SinopacNotEmpDay, CheckInFlag, IsHLBFlag,")
            strSQL.AppendLine("    PassExamFlag, Password, PwdErrCnt, Question, Answer, QuestionErrCnt, ExpireDate, LoginTime,")
            strSQL.AppendLine("    RegStatus, RegIP, AOCode, EmpIDOld, LocalHireFlag, RankBeginDate, DeptBeginDate, PositionBeginDate,")
            strSQL.AppendLine("    WorkTypeBeginDate, BossBeginDate, IDType, LastChgComp, LastChgID, LastChgDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @CompID, @EmpID, @IDNo, @Name, @NameN, @NameB, @EngName, @PassportName, @BirthDate, @Sex, @NationID,")
            strSQL.AppendLine("    @IDExpireDate, @EduID, @Marriage, @WorkStatus, @EmpType, @GroupID, @DeptID, @OrganID, @WorkSiteID,")
            strSQL.AppendLine("    @RankID, @RankIDMap, @HoldingRankID, @TitleID, @PublicTitleID, @EmpDate, @SinopacEmpDate, @QuitDate,")
            strSQL.AppendLine("    @SinopacQuitDate, @ProbDate, @ProbMonth, @NotEmpDay, @SinopacNotEmpDay, @CheckInFlag, @IsHLBFlag,")
            strSQL.AppendLine("    @PassExamFlag, @Password, @PwdErrCnt, @Question, @Answer, @QuestionErrCnt, @ExpireDate, @LoginTime,")
            strSQL.AppendLine("    @RegStatus, @RegIP, @AOCode, @EmpIDOld, @LocalHireFlag, @RankBeginDate, @DeptBeginDate, @PositionBeginDate,")
            strSQL.AppendLine("    @WorkTypeBeginDate, @BossBeginDate, @IDType, @LastChgComp, @LastChgID, @LastChgDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, PersonalRow.CompID.Value)
            db.AddInParameter(dbcmd, "@EmpID", DbType.String, PersonalRow.EmpID.Value)
            db.AddInParameter(dbcmd, "@IDNo", DbType.String, PersonalRow.IDNo.Value)
            db.AddInParameter(dbcmd, "@Name", DbType.String, PersonalRow.Name.Value)
            db.AddInParameter(dbcmd, "@NameN", DbType.String, PersonalRow.NameN.Value)
            db.AddInParameter(dbcmd, "@NameB", DbType.String, PersonalRow.NameB.Value)
            db.AddInParameter(dbcmd, "@EngName", DbType.String, PersonalRow.EngName.Value)
            db.AddInParameter(dbcmd, "@PassportName", DbType.String, PersonalRow.PassportName.Value)
            db.AddInParameter(dbcmd, "@BirthDate", DbType.Date, IIf(IsDateTimeNull(PersonalRow.BirthDate.Value), Convert.ToDateTime("1900/1/1"), PersonalRow.BirthDate.Value))
            db.AddInParameter(dbcmd, "@Sex", DbType.String, PersonalRow.Sex.Value)
            db.AddInParameter(dbcmd, "@NationID", DbType.String, PersonalRow.NationID.Value)
            db.AddInParameter(dbcmd, "@IDExpireDate", DbType.Date, IIf(IsDateTimeNull(PersonalRow.IDExpireDate.Value), Convert.ToDateTime("1900/1/1"), PersonalRow.IDExpireDate.Value))
            db.AddInParameter(dbcmd, "@EduID", DbType.String, PersonalRow.EduID.Value)
            db.AddInParameter(dbcmd, "@Marriage", DbType.String, PersonalRow.Marriage.Value)
            db.AddInParameter(dbcmd, "@WorkStatus", DbType.String, PersonalRow.WorkStatus.Value)
            db.AddInParameter(dbcmd, "@EmpType", DbType.String, PersonalRow.EmpType.Value)
            db.AddInParameter(dbcmd, "@GroupID", DbType.String, PersonalRow.GroupID.Value)
            db.AddInParameter(dbcmd, "@DeptID", DbType.String, PersonalRow.DeptID.Value)
            db.AddInParameter(dbcmd, "@OrganID", DbType.String, PersonalRow.OrganID.Value)
            db.AddInParameter(dbcmd, "@WorkSiteID", DbType.String, PersonalRow.WorkSiteID.Value)
            db.AddInParameter(dbcmd, "@RankID", DbType.String, PersonalRow.RankID.Value)
            db.AddInParameter(dbcmd, "@RankIDMap", DbType.String, PersonalRow.RankIDMap.Value)
            db.AddInParameter(dbcmd, "@HoldingRankID", DbType.String, PersonalRow.HoldingRankID.Value)
            db.AddInParameter(dbcmd, "@TitleID", DbType.String, PersonalRow.TitleID.Value)
            db.AddInParameter(dbcmd, "@PublicTitleID", DbType.String, PersonalRow.PublicTitleID.Value)
            db.AddInParameter(dbcmd, "@EmpDate", DbType.Date, IIf(IsDateTimeNull(PersonalRow.EmpDate.Value), Convert.ToDateTime("1900/1/1"), PersonalRow.EmpDate.Value))
            db.AddInParameter(dbcmd, "@SinopacEmpDate", DbType.Date, IIf(IsDateTimeNull(PersonalRow.SinopacEmpDate.Value), Convert.ToDateTime("1900/1/1"), PersonalRow.SinopacEmpDate.Value))
            db.AddInParameter(dbcmd, "@QuitDate", DbType.Date, IIf(IsDateTimeNull(PersonalRow.QuitDate.Value), Convert.ToDateTime("1900/1/1"), PersonalRow.QuitDate.Value))
            db.AddInParameter(dbcmd, "@SinopacQuitDate", DbType.Date, IIf(IsDateTimeNull(PersonalRow.SinopacQuitDate.Value), Convert.ToDateTime("1900/1/1"), PersonalRow.SinopacQuitDate.Value))
            db.AddInParameter(dbcmd, "@ProbDate", DbType.Date, IIf(IsDateTimeNull(PersonalRow.ProbDate.Value), Convert.ToDateTime("1900/1/1"), PersonalRow.ProbDate.Value))
            db.AddInParameter(dbcmd, "@ProbMonth", DbType.Int32, PersonalRow.ProbMonth.Value)
            db.AddInParameter(dbcmd, "@NotEmpDay", DbType.Int32, PersonalRow.NotEmpDay.Value)
            db.AddInParameter(dbcmd, "@SinopacNotEmpDay", DbType.Int32, PersonalRow.SinopacNotEmpDay.Value)
            db.AddInParameter(dbcmd, "@CheckInFlag", DbType.String, PersonalRow.CheckInFlag.Value)
            db.AddInParameter(dbcmd, "@IsHLBFlag", DbType.String, PersonalRow.IsHLBFlag.Value)
            db.AddInParameter(dbcmd, "@PassExamFlag", DbType.String, PersonalRow.PassExamFlag.Value)
            db.AddInParameter(dbcmd, "@Password", DbType.String, PersonalRow.Password.Value)
            db.AddInParameter(dbcmd, "@PwdErrCnt", DbType.Int32, PersonalRow.PwdErrCnt.Value)
            db.AddInParameter(dbcmd, "@Question", DbType.String, PersonalRow.Question.Value)
            db.AddInParameter(dbcmd, "@Answer", DbType.String, PersonalRow.Answer.Value)
            db.AddInParameter(dbcmd, "@QuestionErrCnt", DbType.Int32, PersonalRow.QuestionErrCnt.Value)
            db.AddInParameter(dbcmd, "@ExpireDate", DbType.Date, IIf(IsDateTimeNull(PersonalRow.ExpireDate.Value), Convert.ToDateTime("1900/1/1"), PersonalRow.ExpireDate.Value))
            db.AddInParameter(dbcmd, "@LoginTime", DbType.Date, IIf(IsDateTimeNull(PersonalRow.LoginTime.Value), Convert.ToDateTime("1900/1/1"), PersonalRow.LoginTime.Value))
            db.AddInParameter(dbcmd, "@RegStatus", DbType.String, PersonalRow.RegStatus.Value)
            db.AddInParameter(dbcmd, "@RegIP", DbType.String, PersonalRow.RegIP.Value)
            db.AddInParameter(dbcmd, "@AOCode", DbType.String, PersonalRow.AOCode.Value)
            db.AddInParameter(dbcmd, "@EmpIDOld", DbType.String, PersonalRow.EmpIDOld.Value)
            db.AddInParameter(dbcmd, "@LocalHireFlag", DbType.String, PersonalRow.LocalHireFlag.Value)
            db.AddInParameter(dbcmd, "@RankBeginDate", DbType.Date, IIf(IsDateTimeNull(PersonalRow.RankBeginDate.Value), Convert.ToDateTime("1900/1/1"), PersonalRow.RankBeginDate.Value))
            db.AddInParameter(dbcmd, "@DeptBeginDate", DbType.Date, IIf(IsDateTimeNull(PersonalRow.DeptBeginDate.Value), Convert.ToDateTime("1900/1/1"), PersonalRow.DeptBeginDate.Value))
            db.AddInParameter(dbcmd, "@PositionBeginDate", DbType.Date, IIf(IsDateTimeNull(PersonalRow.PositionBeginDate.Value), Convert.ToDateTime("1900/1/1"), PersonalRow.PositionBeginDate.Value))
            db.AddInParameter(dbcmd, "@WorkTypeBeginDate", DbType.Date, IIf(IsDateTimeNull(PersonalRow.WorkTypeBeginDate.Value), Convert.ToDateTime("1900/1/1"), PersonalRow.WorkTypeBeginDate.Value))
            db.AddInParameter(dbcmd, "@BossBeginDate", DbType.Date, IIf(IsDateTimeNull(PersonalRow.BossBeginDate.Value), Convert.ToDateTime("1900/1/1"), PersonalRow.BossBeginDate.Value))
            db.AddInParameter(dbcmd, "@IDType", DbType.String, PersonalRow.IDType.Value)
            db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, PersonalRow.LastChgComp.Value)
            db.AddInParameter(dbcmd, "@LastChgID", DbType.String, PersonalRow.LastChgID.Value)
            db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(PersonalRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), PersonalRow.LastChgDate.Value))

            Return db.ExecuteNonQuery(dbcmd)
        End Function

        Public Function Insert(ByVal PersonalRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Insert into Personal")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    CompID, EmpID, IDNo, Name, NameN, NameB, EngName, PassportName, BirthDate, Sex, NationID,")
            strSQL.AppendLine("    IDExpireDate, EduID, Marriage, WorkStatus, EmpType, GroupID, DeptID, OrganID, WorkSiteID,")
            strSQL.AppendLine("    RankID, RankIDMap, HoldingRankID, TitleID, PublicTitleID, EmpDate, SinopacEmpDate, QuitDate,")
            strSQL.AppendLine("    SinopacQuitDate, ProbDate, ProbMonth, NotEmpDay, SinopacNotEmpDay, CheckInFlag, IsHLBFlag,")
            strSQL.AppendLine("    PassExamFlag, Password, PwdErrCnt, Question, Answer, QuestionErrCnt, ExpireDate, LoginTime,")
            strSQL.AppendLine("    RegStatus, RegIP, AOCode, EmpIDOld, LocalHireFlag, RankBeginDate, DeptBeginDate, PositionBeginDate,")
            strSQL.AppendLine("    WorkTypeBeginDate, BossBeginDate, IDType, LastChgComp, LastChgID, LastChgDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @CompID, @EmpID, @IDNo, @Name, @NameN, @NameB, @EngName, @PassportName, @BirthDate, @Sex, @NationID,")
            strSQL.AppendLine("    @IDExpireDate, @EduID, @Marriage, @WorkStatus, @EmpType, @GroupID, @DeptID, @OrganID, @WorkSiteID,")
            strSQL.AppendLine("    @RankID, @RankIDMap, @HoldingRankID, @TitleID, @PublicTitleID, @EmpDate, @SinopacEmpDate, @QuitDate,")
            strSQL.AppendLine("    @SinopacQuitDate, @ProbDate, @ProbMonth, @NotEmpDay, @SinopacNotEmpDay, @CheckInFlag, @IsHLBFlag,")
            strSQL.AppendLine("    @PassExamFlag, @Password, @PwdErrCnt, @Question, @Answer, @QuestionErrCnt, @ExpireDate, @LoginTime,")
            strSQL.AppendLine("    @RegStatus, @RegIP, @AOCode, @EmpIDOld, @LocalHireFlag, @RankBeginDate, @DeptBeginDate, @PositionBeginDate,")
            strSQL.AppendLine("    @WorkTypeBeginDate, @BossBeginDate, @IDType, @LastChgComp, @LastChgID, @LastChgDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, PersonalRow.CompID.Value)
            db.AddInParameter(dbcmd, "@EmpID", DbType.String, PersonalRow.EmpID.Value)
            db.AddInParameter(dbcmd, "@IDNo", DbType.String, PersonalRow.IDNo.Value)
            db.AddInParameter(dbcmd, "@Name", DbType.String, PersonalRow.Name.Value)
            db.AddInParameter(dbcmd, "@NameN", DbType.String, PersonalRow.NameN.Value)
            db.AddInParameter(dbcmd, "@NameB", DbType.String, PersonalRow.NameB.Value)
            db.AddInParameter(dbcmd, "@EngName", DbType.String, PersonalRow.EngName.Value)
            db.AddInParameter(dbcmd, "@PassportName", DbType.String, PersonalRow.PassportName.Value)
            db.AddInParameter(dbcmd, "@BirthDate", DbType.Date, IIf(IsDateTimeNull(PersonalRow.BirthDate.Value), Convert.ToDateTime("1900/1/1"), PersonalRow.BirthDate.Value))
            db.AddInParameter(dbcmd, "@Sex", DbType.String, PersonalRow.Sex.Value)
            db.AddInParameter(dbcmd, "@NationID", DbType.String, PersonalRow.NationID.Value)
            db.AddInParameter(dbcmd, "@IDExpireDate", DbType.Date, IIf(IsDateTimeNull(PersonalRow.IDExpireDate.Value), Convert.ToDateTime("1900/1/1"), PersonalRow.IDExpireDate.Value))
            db.AddInParameter(dbcmd, "@EduID", DbType.String, PersonalRow.EduID.Value)
            db.AddInParameter(dbcmd, "@Marriage", DbType.String, PersonalRow.Marriage.Value)
            db.AddInParameter(dbcmd, "@WorkStatus", DbType.String, PersonalRow.WorkStatus.Value)
            db.AddInParameter(dbcmd, "@EmpType", DbType.String, PersonalRow.EmpType.Value)
            db.AddInParameter(dbcmd, "@GroupID", DbType.String, PersonalRow.GroupID.Value)
            db.AddInParameter(dbcmd, "@DeptID", DbType.String, PersonalRow.DeptID.Value)
            db.AddInParameter(dbcmd, "@OrganID", DbType.String, PersonalRow.OrganID.Value)
            db.AddInParameter(dbcmd, "@WorkSiteID", DbType.String, PersonalRow.WorkSiteID.Value)
            db.AddInParameter(dbcmd, "@RankID", DbType.String, PersonalRow.RankID.Value)
            db.AddInParameter(dbcmd, "@RankIDMap", DbType.String, PersonalRow.RankIDMap.Value)
            db.AddInParameter(dbcmd, "@HoldingRankID", DbType.String, PersonalRow.HoldingRankID.Value)
            db.AddInParameter(dbcmd, "@TitleID", DbType.String, PersonalRow.TitleID.Value)
            db.AddInParameter(dbcmd, "@PublicTitleID", DbType.String, PersonalRow.PublicTitleID.Value)
            db.AddInParameter(dbcmd, "@EmpDate", DbType.Date, IIf(IsDateTimeNull(PersonalRow.EmpDate.Value), Convert.ToDateTime("1900/1/1"), PersonalRow.EmpDate.Value))
            db.AddInParameter(dbcmd, "@SinopacEmpDate", DbType.Date, IIf(IsDateTimeNull(PersonalRow.SinopacEmpDate.Value), Convert.ToDateTime("1900/1/1"), PersonalRow.SinopacEmpDate.Value))
            db.AddInParameter(dbcmd, "@QuitDate", DbType.Date, IIf(IsDateTimeNull(PersonalRow.QuitDate.Value), Convert.ToDateTime("1900/1/1"), PersonalRow.QuitDate.Value))
            db.AddInParameter(dbcmd, "@SinopacQuitDate", DbType.Date, IIf(IsDateTimeNull(PersonalRow.SinopacQuitDate.Value), Convert.ToDateTime("1900/1/1"), PersonalRow.SinopacQuitDate.Value))
            db.AddInParameter(dbcmd, "@ProbDate", DbType.Date, IIf(IsDateTimeNull(PersonalRow.ProbDate.Value), Convert.ToDateTime("1900/1/1"), PersonalRow.ProbDate.Value))
            db.AddInParameter(dbcmd, "@ProbMonth", DbType.Int32, PersonalRow.ProbMonth.Value)
            db.AddInParameter(dbcmd, "@NotEmpDay", DbType.Int32, PersonalRow.NotEmpDay.Value)
            db.AddInParameter(dbcmd, "@SinopacNotEmpDay", DbType.Int32, PersonalRow.SinopacNotEmpDay.Value)
            db.AddInParameter(dbcmd, "@CheckInFlag", DbType.String, PersonalRow.CheckInFlag.Value)
            db.AddInParameter(dbcmd, "@IsHLBFlag", DbType.String, PersonalRow.IsHLBFlag.Value)
            db.AddInParameter(dbcmd, "@PassExamFlag", DbType.String, PersonalRow.PassExamFlag.Value)
            db.AddInParameter(dbcmd, "@Password", DbType.String, PersonalRow.Password.Value)
            db.AddInParameter(dbcmd, "@PwdErrCnt", DbType.Int32, PersonalRow.PwdErrCnt.Value)
            db.AddInParameter(dbcmd, "@Question", DbType.String, PersonalRow.Question.Value)
            db.AddInParameter(dbcmd, "@Answer", DbType.String, PersonalRow.Answer.Value)
            db.AddInParameter(dbcmd, "@QuestionErrCnt", DbType.Int32, PersonalRow.QuestionErrCnt.Value)
            db.AddInParameter(dbcmd, "@ExpireDate", DbType.Date, IIf(IsDateTimeNull(PersonalRow.ExpireDate.Value), Convert.ToDateTime("1900/1/1"), PersonalRow.ExpireDate.Value))
            db.AddInParameter(dbcmd, "@LoginTime", DbType.Date, IIf(IsDateTimeNull(PersonalRow.LoginTime.Value), Convert.ToDateTime("1900/1/1"), PersonalRow.LoginTime.Value))
            db.AddInParameter(dbcmd, "@RegStatus", DbType.String, PersonalRow.RegStatus.Value)
            db.AddInParameter(dbcmd, "@RegIP", DbType.String, PersonalRow.RegIP.Value)
            db.AddInParameter(dbcmd, "@AOCode", DbType.String, PersonalRow.AOCode.Value)
            db.AddInParameter(dbcmd, "@EmpIDOld", DbType.String, PersonalRow.EmpIDOld.Value)
            db.AddInParameter(dbcmd, "@LocalHireFlag", DbType.String, PersonalRow.LocalHireFlag.Value)
            db.AddInParameter(dbcmd, "@RankBeginDate", DbType.Date, IIf(IsDateTimeNull(PersonalRow.RankBeginDate.Value), Convert.ToDateTime("1900/1/1"), PersonalRow.RankBeginDate.Value))
            db.AddInParameter(dbcmd, "@DeptBeginDate", DbType.Date, IIf(IsDateTimeNull(PersonalRow.DeptBeginDate.Value), Convert.ToDateTime("1900/1/1"), PersonalRow.DeptBeginDate.Value))
            db.AddInParameter(dbcmd, "@PositionBeginDate", DbType.Date, IIf(IsDateTimeNull(PersonalRow.PositionBeginDate.Value), Convert.ToDateTime("1900/1/1"), PersonalRow.PositionBeginDate.Value))
            db.AddInParameter(dbcmd, "@WorkTypeBeginDate", DbType.Date, IIf(IsDateTimeNull(PersonalRow.WorkTypeBeginDate.Value), Convert.ToDateTime("1900/1/1"), PersonalRow.WorkTypeBeginDate.Value))
            db.AddInParameter(dbcmd, "@BossBeginDate", DbType.Date, IIf(IsDateTimeNull(PersonalRow.BossBeginDate.Value), Convert.ToDateTime("1900/1/1"), PersonalRow.BossBeginDate.Value))
            db.AddInParameter(dbcmd, "@IDType", DbType.String, PersonalRow.IDType.Value)
            db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, PersonalRow.LastChgComp.Value)
            db.AddInParameter(dbcmd, "@LastChgID", DbType.String, PersonalRow.LastChgID.Value)
            db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(PersonalRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), PersonalRow.LastChgDate.Value))

            Return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function Insert(ByVal PersonalRow As Row()) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Insert into Personal")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    CompID, EmpID, IDNo, Name, NameN, NameB, EngName, PassportName, BirthDate, Sex, NationID,")
            strSQL.AppendLine("    IDExpireDate, EduID, Marriage, WorkStatus, EmpType, GroupID, DeptID, OrganID, WorkSiteID,")
            strSQL.AppendLine("    RankID, RankIDMap, HoldingRankID, TitleID, PublicTitleID, EmpDate, SinopacEmpDate, QuitDate,")
            strSQL.AppendLine("    SinopacQuitDate, ProbDate, ProbMonth, NotEmpDay, SinopacNotEmpDay, CheckInFlag, IsHLBFlag,")
            strSQL.AppendLine("    PassExamFlag, Password, PwdErrCnt, Question, Answer, QuestionErrCnt, ExpireDate, LoginTime,")
            strSQL.AppendLine("    RegStatus, RegIP, AOCode, EmpIDOld, LocalHireFlag, RankBeginDate, DeptBeginDate, PositionBeginDate,")
            strSQL.AppendLine("    WorkTypeBeginDate, BossBeginDate, IDType, LastChgComp, LastChgID, LastChgDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @CompID, @EmpID, @IDNo, @Name, @NameN, @NameB, @EngName, @PassportName, @BirthDate, @Sex, @NationID,")
            strSQL.AppendLine("    @IDExpireDate, @EduID, @Marriage, @WorkStatus, @EmpType, @GroupID, @DeptID, @OrganID, @WorkSiteID,")
            strSQL.AppendLine("    @RankID, @RankIDMap, @HoldingRankID, @TitleID, @PublicTitleID, @EmpDate, @SinopacEmpDate, @QuitDate,")
            strSQL.AppendLine("    @SinopacQuitDate, @ProbDate, @ProbMonth, @NotEmpDay, @SinopacNotEmpDay, @CheckInFlag, @IsHLBFlag,")
            strSQL.AppendLine("    @PassExamFlag, @Password, @PwdErrCnt, @Question, @Answer, @QuestionErrCnt, @ExpireDate, @LoginTime,")
            strSQL.AppendLine("    @RegStatus, @RegIP, @AOCode, @EmpIDOld, @LocalHireFlag, @RankBeginDate, @DeptBeginDate, @PositionBeginDate,")
            strSQL.AppendLine("    @WorkTypeBeginDate, @BossBeginDate, @IDType, @LastChgComp, @LastChgID, @LastChgDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            Using cn As DbConnection = db.CreateConnection()
                cn.Open()
                Dim tran As DbTransaction = cn.BeginTransaction()
                Dim inTrans As Boolean = True

                Try
                    For Each r As Row In PersonalRow
                        dbcmd.Parameters.Clear()
                        db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                        db.AddInParameter(dbcmd, "@EmpID", DbType.String, r.EmpID.Value)
                        db.AddInParameter(dbcmd, "@IDNo", DbType.String, r.IDNo.Value)
                        db.AddInParameter(dbcmd, "@Name", DbType.String, r.Name.Value)
                        db.AddInParameter(dbcmd, "@NameN", DbType.String, r.NameN.Value)
                        db.AddInParameter(dbcmd, "@NameB", DbType.String, r.NameB.Value)
                        db.AddInParameter(dbcmd, "@EngName", DbType.String, r.EngName.Value)
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
                        db.AddInParameter(dbcmd, "@RankIDMap", DbType.String, r.RankIDMap.Value)
                        db.AddInParameter(dbcmd, "@HoldingRankID", DbType.String, r.HoldingRankID.Value)
                        db.AddInParameter(dbcmd, "@TitleID", DbType.String, r.TitleID.Value)
                        db.AddInParameter(dbcmd, "@PublicTitleID", DbType.String, r.PublicTitleID.Value)
                        db.AddInParameter(dbcmd, "@EmpDate", DbType.Date, IIf(IsDateTimeNull(r.EmpDate.Value), Convert.ToDateTime("1900/1/1"), r.EmpDate.Value))
                        db.AddInParameter(dbcmd, "@SinopacEmpDate", DbType.Date, IIf(IsDateTimeNull(r.SinopacEmpDate.Value), Convert.ToDateTime("1900/1/1"), r.SinopacEmpDate.Value))
                        db.AddInParameter(dbcmd, "@QuitDate", DbType.Date, IIf(IsDateTimeNull(r.QuitDate.Value), Convert.ToDateTime("1900/1/1"), r.QuitDate.Value))
                        db.AddInParameter(dbcmd, "@SinopacQuitDate", DbType.Date, IIf(IsDateTimeNull(r.SinopacQuitDate.Value), Convert.ToDateTime("1900/1/1"), r.SinopacQuitDate.Value))
                        db.AddInParameter(dbcmd, "@ProbDate", DbType.Date, IIf(IsDateTimeNull(r.ProbDate.Value), Convert.ToDateTime("1900/1/1"), r.ProbDate.Value))
                        db.AddInParameter(dbcmd, "@ProbMonth", DbType.Int32, r.ProbMonth.Value)
                        db.AddInParameter(dbcmd, "@NotEmpDay", DbType.Int32, r.NotEmpDay.Value)
                        db.AddInParameter(dbcmd, "@SinopacNotEmpDay", DbType.Int32, r.SinopacNotEmpDay.Value)
                        db.AddInParameter(dbcmd, "@CheckInFlag", DbType.String, r.CheckInFlag.Value)
                        db.AddInParameter(dbcmd, "@IsHLBFlag", DbType.String, r.IsHLBFlag.Value)
                        db.AddInParameter(dbcmd, "@PassExamFlag", DbType.String, r.PassExamFlag.Value)
                        db.AddInParameter(dbcmd, "@Password", DbType.String, r.Password.Value)
                        db.AddInParameter(dbcmd, "@PwdErrCnt", DbType.Int32, r.PwdErrCnt.Value)
                        db.AddInParameter(dbcmd, "@Question", DbType.String, r.Question.Value)
                        db.AddInParameter(dbcmd, "@Answer", DbType.String, r.Answer.Value)
                        db.AddInParameter(dbcmd, "@QuestionErrCnt", DbType.Int32, r.QuestionErrCnt.Value)
                        db.AddInParameter(dbcmd, "@ExpireDate", DbType.Date, IIf(IsDateTimeNull(r.ExpireDate.Value), Convert.ToDateTime("1900/1/1"), r.ExpireDate.Value))
                        db.AddInParameter(dbcmd, "@LoginTime", DbType.Date, IIf(IsDateTimeNull(r.LoginTime.Value), Convert.ToDateTime("1900/1/1"), r.LoginTime.Value))
                        db.AddInParameter(dbcmd, "@RegStatus", DbType.String, r.RegStatus.Value)
                        db.AddInParameter(dbcmd, "@RegIP", DbType.String, r.RegIP.Value)
                        db.AddInParameter(dbcmd, "@AOCode", DbType.String, r.AOCode.Value)
                        db.AddInParameter(dbcmd, "@EmpIDOld", DbType.String, r.EmpIDOld.Value)
                        db.AddInParameter(dbcmd, "@LocalHireFlag", DbType.String, r.LocalHireFlag.Value)
                        db.AddInParameter(dbcmd, "@RankBeginDate", DbType.Date, IIf(IsDateTimeNull(r.RankBeginDate.Value), Convert.ToDateTime("1900/1/1"), r.RankBeginDate.Value))
                        db.AddInParameter(dbcmd, "@DeptBeginDate", DbType.Date, IIf(IsDateTimeNull(r.DeptBeginDate.Value), Convert.ToDateTime("1900/1/1"), r.DeptBeginDate.Value))
                        db.AddInParameter(dbcmd, "@PositionBeginDate", DbType.Date, IIf(IsDateTimeNull(r.PositionBeginDate.Value), Convert.ToDateTime("1900/1/1"), r.PositionBeginDate.Value))
                        db.AddInParameter(dbcmd, "@WorkTypeBeginDate", DbType.Date, IIf(IsDateTimeNull(r.WorkTypeBeginDate.Value), Convert.ToDateTime("1900/1/1"), r.WorkTypeBeginDate.Value))
                        db.AddInParameter(dbcmd, "@BossBeginDate", DbType.Date, IIf(IsDateTimeNull(r.BossBeginDate.Value), Convert.ToDateTime("1900/1/1"), r.BossBeginDate.Value))
                        db.AddInParameter(dbcmd, "@IDType", DbType.String, r.IDType.Value)
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

        Public Function Insert(ByVal PersonalRow As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Insert into Personal")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    CompID, EmpID, IDNo, Name, NameN, NameB, EngName, PassportName, BirthDate, Sex, NationID,")
            strSQL.AppendLine("    IDExpireDate, EduID, Marriage, WorkStatus, EmpType, GroupID, DeptID, OrganID, WorkSiteID,")
            strSQL.AppendLine("    RankID, RankIDMap, HoldingRankID, TitleID, PublicTitleID, EmpDate, SinopacEmpDate, QuitDate,")
            strSQL.AppendLine("    SinopacQuitDate, ProbDate, ProbMonth, NotEmpDay, SinopacNotEmpDay, CheckInFlag, IsHLBFlag,")
            strSQL.AppendLine("    PassExamFlag, Password, PwdErrCnt, Question, Answer, QuestionErrCnt, ExpireDate, LoginTime,")
            strSQL.AppendLine("    RegStatus, RegIP, AOCode, EmpIDOld, LocalHireFlag, RankBeginDate, DeptBeginDate, PositionBeginDate,")
            strSQL.AppendLine("    WorkTypeBeginDate, BossBeginDate, IDType, LastChgComp, LastChgID, LastChgDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @CompID, @EmpID, @IDNo, @Name, @NameN, @NameB, @EngName, @PassportName, @BirthDate, @Sex, @NationID,")
            strSQL.AppendLine("    @IDExpireDate, @EduID, @Marriage, @WorkStatus, @EmpType, @GroupID, @DeptID, @OrganID, @WorkSiteID,")
            strSQL.AppendLine("    @RankID, @RankIDMap, @HoldingRankID, @TitleID, @PublicTitleID, @EmpDate, @SinopacEmpDate, @QuitDate,")
            strSQL.AppendLine("    @SinopacQuitDate, @ProbDate, @ProbMonth, @NotEmpDay, @SinopacNotEmpDay, @CheckInFlag, @IsHLBFlag,")
            strSQL.AppendLine("    @PassExamFlag, @Password, @PwdErrCnt, @Question, @Answer, @QuestionErrCnt, @ExpireDate, @LoginTime,")
            strSQL.AppendLine("    @RegStatus, @RegIP, @AOCode, @EmpIDOld, @LocalHireFlag, @RankBeginDate, @DeptBeginDate, @PositionBeginDate,")
            strSQL.AppendLine("    @WorkTypeBeginDate, @BossBeginDate, @IDType, @LastChgComp, @LastChgID, @LastChgDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())

            For Each r As Row In PersonalRow
                dbcmd.Parameters.Clear()
                db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                db.AddInParameter(dbcmd, "@EmpID", DbType.String, r.EmpID.Value)
                db.AddInParameter(dbcmd, "@IDNo", DbType.String, r.IDNo.Value)
                db.AddInParameter(dbcmd, "@Name", DbType.String, r.Name.Value)
                db.AddInParameter(dbcmd, "@NameN", DbType.String, r.NameN.Value)
                db.AddInParameter(dbcmd, "@NameB", DbType.String, r.NameB.Value)
                db.AddInParameter(dbcmd, "@EngName", DbType.String, r.EngName.Value)
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
                db.AddInParameter(dbcmd, "@RankIDMap", DbType.String, r.RankIDMap.Value)
                db.AddInParameter(dbcmd, "@HoldingRankID", DbType.String, r.HoldingRankID.Value)
                db.AddInParameter(dbcmd, "@TitleID", DbType.String, r.TitleID.Value)
                db.AddInParameter(dbcmd, "@PublicTitleID", DbType.String, r.PublicTitleID.Value)
                db.AddInParameter(dbcmd, "@EmpDate", DbType.Date, IIf(IsDateTimeNull(r.EmpDate.Value), Convert.ToDateTime("1900/1/1"), r.EmpDate.Value))
                db.AddInParameter(dbcmd, "@SinopacEmpDate", DbType.Date, IIf(IsDateTimeNull(r.SinopacEmpDate.Value), Convert.ToDateTime("1900/1/1"), r.SinopacEmpDate.Value))
                db.AddInParameter(dbcmd, "@QuitDate", DbType.Date, IIf(IsDateTimeNull(r.QuitDate.Value), Convert.ToDateTime("1900/1/1"), r.QuitDate.Value))
                db.AddInParameter(dbcmd, "@SinopacQuitDate", DbType.Date, IIf(IsDateTimeNull(r.SinopacQuitDate.Value), Convert.ToDateTime("1900/1/1"), r.SinopacQuitDate.Value))
                db.AddInParameter(dbcmd, "@ProbDate", DbType.Date, IIf(IsDateTimeNull(r.ProbDate.Value), Convert.ToDateTime("1900/1/1"), r.ProbDate.Value))
                db.AddInParameter(dbcmd, "@ProbMonth", DbType.Int32, r.ProbMonth.Value)
                db.AddInParameter(dbcmd, "@NotEmpDay", DbType.Int32, r.NotEmpDay.Value)
                db.AddInParameter(dbcmd, "@SinopacNotEmpDay", DbType.Int32, r.SinopacNotEmpDay.Value)
                db.AddInParameter(dbcmd, "@CheckInFlag", DbType.String, r.CheckInFlag.Value)
                db.AddInParameter(dbcmd, "@IsHLBFlag", DbType.String, r.IsHLBFlag.Value)
                db.AddInParameter(dbcmd, "@PassExamFlag", DbType.String, r.PassExamFlag.Value)
                db.AddInParameter(dbcmd, "@Password", DbType.String, r.Password.Value)
                db.AddInParameter(dbcmd, "@PwdErrCnt", DbType.Int32, r.PwdErrCnt.Value)
                db.AddInParameter(dbcmd, "@Question", DbType.String, r.Question.Value)
                db.AddInParameter(dbcmd, "@Answer", DbType.String, r.Answer.Value)
                db.AddInParameter(dbcmd, "@QuestionErrCnt", DbType.Int32, r.QuestionErrCnt.Value)
                db.AddInParameter(dbcmd, "@ExpireDate", DbType.Date, IIf(IsDateTimeNull(r.ExpireDate.Value), Convert.ToDateTime("1900/1/1"), r.ExpireDate.Value))
                db.AddInParameter(dbcmd, "@LoginTime", DbType.Date, IIf(IsDateTimeNull(r.LoginTime.Value), Convert.ToDateTime("1900/1/1"), r.LoginTime.Value))
                db.AddInParameter(dbcmd, "@RegStatus", DbType.String, r.RegStatus.Value)
                db.AddInParameter(dbcmd, "@RegIP", DbType.String, r.RegIP.Value)
                db.AddInParameter(dbcmd, "@AOCode", DbType.String, r.AOCode.Value)
                db.AddInParameter(dbcmd, "@EmpIDOld", DbType.String, r.EmpIDOld.Value)
                db.AddInParameter(dbcmd, "@LocalHireFlag", DbType.String, r.LocalHireFlag.Value)
                db.AddInParameter(dbcmd, "@RankBeginDate", DbType.Date, IIf(IsDateTimeNull(r.RankBeginDate.Value), Convert.ToDateTime("1900/1/1"), r.RankBeginDate.Value))
                db.AddInParameter(dbcmd, "@DeptBeginDate", DbType.Date, IIf(IsDateTimeNull(r.DeptBeginDate.Value), Convert.ToDateTime("1900/1/1"), r.DeptBeginDate.Value))
                db.AddInParameter(dbcmd, "@PositionBeginDate", DbType.Date, IIf(IsDateTimeNull(r.PositionBeginDate.Value), Convert.ToDateTime("1900/1/1"), r.PositionBeginDate.Value))
                db.AddInParameter(dbcmd, "@WorkTypeBeginDate", DbType.Date, IIf(IsDateTimeNull(r.WorkTypeBeginDate.Value), Convert.ToDateTime("1900/1/1"), r.WorkTypeBeginDate.Value))
                db.AddInParameter(dbcmd, "@BossBeginDate", DbType.Date, IIf(IsDateTimeNull(r.BossBeginDate.Value), Convert.ToDateTime("1900/1/1"), r.BossBeginDate.Value))
                db.AddInParameter(dbcmd, "@IDType", DbType.String, r.IDType.Value)
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

