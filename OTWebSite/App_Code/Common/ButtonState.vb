Imports Microsoft.VisualBasic
'20150715 wei modify 增加Button 新Button如下:
'A-新增，C-確認，D-刪除，I-查詢，L-下傳，P-列印，R-放行，U-修改，X-清除(改名)，B-駁回(新增)，E-執行(新增)，F-上傳(新增)，G-複製(新增)，H-保留(新增)，J-保留(新增)
Public Class ButtonState
    Public Enum emButtonType
        Add
        Update
        Delete
        Query
        Confirm
        [Exit]
        OK
        Print
        Cancel
        Release
        Download
        Reject  '20150716 wei add
        Executes    '20150716 wei add
        Upload  '20150716 wei add
        Copy    '20150716 wei add
    End Enum

    Private _Name As String
    Private _Caption As String
    Private _ActionParam As String
    Private _Hint As String
    Private _RightID As String
    Private _Visible As Boolean = True

    Public Property Name() As String
        Get
            Return _Name
        End Get
        Set(ByVal value As String)
            _Name = value
        End Set
    End Property

    Public Property Caption() As String
        Get
            Return _Caption
        End Get
        Set(ByVal value As String)
            _Caption = value
        End Set
    End Property

    Public Property ActionParam() As String
        Get
            Return _ActionParam
        End Get
        Set(ByVal value As String)
            _ActionParam = value
        End Set
    End Property

    Public Property Hint() As String
        Get
            Return _Hint
        End Get
        Set(ByVal value As String)
            _Hint = value
        End Set
    End Property

    Public Property RightID() As String
        Get
            Return _RightID
        End Get
        Set(ByVal value As String)
            _RightID = value
        End Set
    End Property

    Public Property Visible() As Boolean
        Get
            Return _Visible
        End Get
        Set(ByVal value As Boolean)
            _Visible = value
        End Set
    End Property

    Public Sub New()
        Name = ""
        Caption = ""
        Hint = ""
        RightID = ""
        ActionParam = ""
    End Sub

    Public Sub New(ByVal bType As emButtonType)
        Select Case bType
            Case emButtonType.Add
                Name = "btnAdd"
                Caption = "新增"
                RightID = "A"
                ActionParam = "btnAdd"
            Case emButtonType.Update
                Name = "btnUpdate"
                Caption = "修改"
                RightID = "U"
                ActionParam = "btnUpdate"
            Case emButtonType.Delete
                Name = "btnDelete"
                Caption = "删除"
                RightID = "D"
                ActionParam = "btnDelete"
            Case emButtonType.Query
                Name = "btnQuery"
                Caption = "查詢"
                RightID = "I"
                ActionParam = "btnQuery"
            Case emButtonType.Confirm
                Name = "btnActionC"
                Caption = "確認"
                RightID = "C"
                ActionParam = "btnActionC"
            Case emButtonType.Exit
                Name = "btnActionX"
                Caption = "清除" '20150716 wei modify
                RightID = "X"
                ActionParam = "btnActionX"
            Case emButtonType.OK
                Name = "btnOK"
                Caption = "確定"
                RightID = "O"
                ActionParam = "btnOK"
            Case emButtonType.Cancel
                Name = "btnCancel"
                Caption = "取消"
                RightID = "L"
                ActionParam = "btnCancel"
            Case emButtonType.Print
                Name = "btnPrint"
                Caption = "列印"
                RightID = "P"
                ActionParam = "btnActionP"
            Case emButtonType.Release
                Name = "btnRelease"
                Caption = "放行"
                RightID = "R"
                ActionParam = "btnRelease"
            Case emButtonType.Download
                Name = "btnDownload"
                Caption = "下傳"
                RightID = "L"
                ActionParam = "btnDownload"
            Case emButtonType.Copy  '20150716 wei add
                Name = "btnCopy"
                Caption = "複製"
                RightID = "G"
                ActionParam = "btnCopy"
            Case emButtonType.Executes  '20150716 wei add
                Name = "btnExecutes"
                Caption = "執行"
                RightID = "E"
                ActionParam = "btnExecutes"
            Case emButtonType.Reject    '20150716 wei add
                Name = "btnReject"
                Caption = "駁回"
                RightID = "B"
                ActionParam = "btnReject"
            Case emButtonType.Upload    '20150716 wei add
                Name = "btnUpload"
                Caption = "上傳"
                RightID = "F"
                ActionParam = "btnUpload"
        End Select
    End Sub

End Class
