Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Partial Class Component_ucGridViewChoiceAll
    Inherits System.Web.UI.UserControl
    ''' <summary>
    ''' 核取方塊的名稱
    ''' </summary>
    Private _CheckBoxName As String
    Public Property CheckBoxName() As String
        Get
            Return _CheckBoxName
        End Get
        Set(value As String)
            _CheckBoxName = value
        End Set
    End Property
    ''' <summary>
    ''' 設定Header字串
    ''' </summary>
    Public WriteOnly Property HeaderText() As String
        Set(value As String)
            chkChoice.Text = value
        End Set
    End Property
    ''' <summary>
    ''' 準備輸出的動作
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Protected Sub Page_PreRender(sender As Object, e As System.EventArgs) Handles Me.PreRender
        '找出GridView之中的CheckBox名稱
        Dim gv As GridView = DirectCast(Me.Parent.Parent.Parent.Parent, GridView)
        Dim strCheckBoxName As String = ""
        Dim i As Integer
        For i = 0 To gv.Rows.Count - 1
            Dim cbx As CheckBox = DirectCast(gv.Rows(i).FindControl(Me.CheckBoxName), CheckBox)
            strCheckBoxName += cbx.ClientID + ","
        Next

        '調整陣列值
        If strCheckBoxName <> "" Then
            strCheckBoxName = strCheckBoxName.Substring(0, strCheckBoxName.Length - 1)
        End If
        '變更陣列值
        hidCheckBox.Value = strCheckBoxName
        Dim strClickScript As String
        strClickScript = "document.getElementById('" & hidCheckBox.ClientID & "').value = '" & strCheckBoxName & "';"
        ScriptManager.RegisterStartupScript(Me, Me.[GetType](), Me.ClientID, strClickScript, True)
        chkChoice.Attributes.Add("onclick", "funChoiceAll" & Convert.ToString(Me.ClientID) & "(this);")

        '註冊陣列值
        Dim strScript As String
        strScript = "function funChoiceAll" & Convert.ToString(Me.ClientID) & "(obj){"
        strScript += " var strCheckBox = document.getElementById('" & hidCheckBox.ClientID & "').value.split(',');"
        strScript += " var obj1 = document.getElementById('__SelectedRows" & gv.ClientID & "' );"
        strScript += " var i;"
        strScript += " var PageIndex;"
        strScript += " PageIndex=" & gv.PageIndex & ";"
        strScript += " for (i=0; i<strCheckBox.length; i++){ "
        strScript += " document.getElementById(strCheckBox[i]).checked = obj.checked;}"
        strScript += " if (obj1 != undefined) {"
        strScript += " if (obj.checked) { "
        strScript += " if (obj1.value != '') obj1.value = obj1.value + ',';"
        strScript += " obj1.value = obj1.value + PageIndex + '.' + i;}"
        strScript += " else { "
        strScript += " obj1.value = obj1.value.replace(PageIndex + '.' + i + ',', '');"
        strScript += " obj1.value = Replace(obj1.value, ',' + PageIndex + '.' + i + ',');"
        strScript += " obj1.value = obj1.value.replace(',' + PageIndex + '.' + i, '');"
        strScript += " obj1.value = obj1.value.replace(PageIndex + '.' + i, '');}"
        strScript += " }}"
        ScriptManager.RegisterStartupScript(Me, Me.[GetType](), "fun" & Convert.ToString(Me.ClientID), strScript, True)
    End Sub
End Class
