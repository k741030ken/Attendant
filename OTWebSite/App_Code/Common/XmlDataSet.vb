Imports Microsoft.VisualBasic
Imports System.Data

Public Class XmlDataSet
    Private _DataSetName As String
    Private _TableName As String
    Private _dsData As DataSet

    Public Sub New()

    End Sub

    Public Sub New(ByVal dataSetName As String, ByVal tableName As String)
        If dataSetName <> "" Then _DataSetName = dataSetName
        If tableName <> "" Then _TableName = tableName
    End Sub

    Public Function LoadDataSet(ByVal objDS As DataSet) As Boolean
        If objDS IsNot Nothing Then
            _dsData = objDS
            _dsData.DataSetName = _DataSetName
            _dsData.Tables(0).TableName = _TableName
            Return True
        Else
            Return False
        End If
    End Function

    Public Function GetDataSet() As DataSet
        Return _dsData
    End Function

    Public Property DataSetName() As String
        Get
            Return _DataSetName
        End Get
        Set(ByVal value As String)
            If value <> "" Then _DataSetName = value
        End Set
    End Property

    Public Property TableName() As String
        Get
            Return _TableName
        End Get
        Set(ByVal value As String)
            If value <> "" Then _TableName = value
        End Set
    End Property

End Class
