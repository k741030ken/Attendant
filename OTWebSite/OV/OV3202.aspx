<%@ Page Language="VB" AutoEventWireup="false" CodeFile="OV3202.aspx.vb" Inherits="OV_OV3202" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link type="text/css" rel="stylesheet" href="../StyleSheet.Css" />
    <script type="text/javascript" src="../ClientFun/jquery-1.8.3.min.js"></script>
    <script type="text/javascript">
    <!--
        function EntertoSubmit() {
            if (window.event.keyCode == 13) {
                try {
                    window.parent.frames[0].document.getElementById("ucButtonPermission_btnQuery").click();
                }
                catch (ex) {
                    console.log(ex.message);
                }
            }
        }

        $(function () {
            if ($("#tvOrgan").find("table").length > 0) {
                $("#tvOrgan").find("table").each(function () {
                    var color = $(this).find("span").attr("href");
                    $(this).css("background-color", color);
                    $(this).find("td:last").css("width", "100%");
                });
            }
        });

        function OnTreeClick(evt) {
            var src = window.event != window.undefined ? window.event.srcElement : evt.target;
            var isChkBoxClick = (src.tagName.toLowerCase() == "input" && src.type == "checkbox");
            if (isChkBoxClick) {
                var parentTable = GetParentByTagName("table", src);
                var nxtSibling = parentTable.nextSibling;
                if (nxtSibling && nxtSibling.nodeType == 1)//check if nxt sibling is not null & is an element node
                {
                    if (nxtSibling.tagName.toLowerCase() == "div") //if node has children
                    {
                        //check or uncheck children at all levels
                        CheckUncheckChildren(parentTable.nextSibling, src.checked);
                    }
                }
                //check or uncheck parents at all levels
                CheckUncheckParents(src, src.checked);
            }
        }

        function CheckUncheckChildren(childContainer, check) {
            var childChkBoxes = childContainer.getElementsByTagName("input");
            var childChkBoxCount = childChkBoxes.length;
            for (var i = 0; i < childChkBoxCount; i++) {
                childChkBoxes[i].checked = check;
            }
        }

        function CheckUncheckParents(srcChild, check) {
            var parentDiv = GetParentByTagName("div", srcChild);
            var parentNodeTable = parentDiv.previousSibling;

            if (parentNodeTable) {
                var checkUncheckSwitch;

                if (check) //checkbox checked
                {
                    var isAllSiblingsChecked = AreAllSiblingsChecked(srcChild);
                    if (isAllSiblingsChecked)
                        checkUncheckSwitch = true;
                    else
                        return; //do not need to check parent if any(one or more) child not checked
                }
                else //checkbox unchecked
                {
                    checkUncheckSwitch = false;
                }

                var inpElemsInParentTable = parentNodeTable.getElementsByTagName("input");
                if (inpElemsInParentTable.length > 0) {
                    var parentNodeChkBox = inpElemsInParentTable[0];
                    parentNodeChkBox.checked = checkUncheckSwitch;
                    //do the same recursively
                    CheckUncheckParents(parentNodeChkBox, checkUncheckSwitch);
                }
            }
        }

        function AreAllSiblingsChecked(chkBox) {
            var parentDiv = GetParentByTagName("div", chkBox);
            var childCount = parentDiv.childNodes.length;
            for (var i = 0; i < childCount; i++) {
                if (parentDiv.childNodes[i].nodeType == 1) //check if the child node is an element node
                {
                    if (parentDiv.childNodes[i].tagName.toLowerCase() == "table") {
                        var prevChkBox = parentDiv.childNodes[i].getElementsByTagName("input")[0];
                        //if any of sibling nodes are not checked, return false
                        if (!prevChkBox.checked) {
                            return false;
                        }
                    }
                }
            }
            return true;
        }

        //utility function to get the container of an element by tagname
        function GetParentByTagName(parentTagName, childElementObj) {
            var parent = childElementObj.parentNode;
            while (parent.tagName.toLowerCase() != parentTagName.toLowerCase()) {
                parent = parent.parentNode;
            }
            return parent;
        }
       -->
    </script>
       <style type="text/css">
        
        .td_padding
        {
            width: 3%;
        }
         input, option, span, div, select,label,td,tr
        {
            font-family: 微軟正黑體,Calibri, 新細明體,sans-serif;
        }
    </style>
</head>
<body style="margin-top: 5px; margin-left: 5px; margin-right: 5px; margin-bottom: 0">
    <form id="frmContent" runat="server">
    <ajaxToolkit:ToolkitScriptManager runat="Server" EnableScriptGlobalization="true"
        EnableScriptLocalization="true" ID="ScriptManager1" />
    <table cellpadding="1" cellspacing="1" border="0" class="tbl_Condition" width="100%">
        <tr>
         <td class="td_padding"></td>
            <td align="left" width="10%">
                公司代碼：
            </td>
            <td align="left" width="20%">
                <asp:DropDownList ID="ddlCompID" runat="server" AutoPostBack="true" Font-Names="微軟正黑體">
                </asp:DropDownList>
                <asp:Label ID="lblCompRoleID" Font-Size="15px" runat="server" Width="200px"></asp:Label>
            </td>
            <td align="left" width="10%">
                員工編號：
            </td>
            <td align="left" width="20%">
                <asp:TextBox CssClass="InputTextStyle_Thin" ID="txtEmpID" MaxLength="6" runat="server"
                    Style="text-transform: uppercase" AutoPostBack="true"></asp:TextBox>
                <asp:Label ID="lblEmpID" Font-Size="15px" runat="server" Width="200px"></asp:Label>
                <uc:ButtonQuerySelectUserID ID="ucQueryEmp" runat="server" WindowHeight="800" WindowWidth="600"
                    ButtonText="..." />
            </td>
            <td align="left" width="10%">
                員工姓名：
            </td>
            <td align="left" width="20%">
                <asp:Label ID="lblEmpName" runat="server" Text=""></asp:Label>
            </td>
        </tr>
    </table>
    <br />
    <div style="width: 100%; float: left; height: 590px;">
    <div id="MenuBox_Organ" style="width: 49%; float: left; border: 1px solid black;
        height: 100%; overflow: auto">
        <asp:CheckBox ID="chkAllOrgan" runat="server" Text="全選" AutoPostBack="true" />
        <asp:TreeView ID="tvOrgan" runat="server" ShowLines="true" ShowCheckBoxes="All" onclick="OnTreeClick(event)">
        </asp:TreeView>
    </div>
    <div id="MenuBox_OrganFlow" style="width: 49%; float: left; border: 1px solid black;
        height: 100%; overflow: auto">
        <asp:CheckBox ID="chkAllOrganFlow" runat="server" Text="全選" AutoPostBack="true" />
        <asp:DropDownList ID="ddlBusinessType" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlBusinessType_SelectedIndexChanged"
            onclick="OnTreeClick(event)">
        </asp:DropDownList>
        <br />
        <asp:TreeView ID="tvOrganFlow" runat="server" ShowLines="true" ShowCheckBoxes="All"
            onclick="OnTreeClick(event)">
        </asp:TreeView>
    </div>
    </div>
    
    <%--<div id="Div1" style="width: 45%; float: left; height: 600px; overflow: auto">--%>
    <div id="Div1">
        <asp:TreeView ID="hidtvOrganFlow" runat="server" ShowLines="true" ShowCheckBoxes="All"
            onclick="OnTreeClick(event)" Visible="False">
        </asp:TreeView>
        <asp:HiddenField ID="isCheckAllOrgan" runat="server"></asp:HiddenField>
        <asp:HiddenField ID="isCheckAllOrganFlow" runat="server"></asp:HiddenField>
        <asp:HiddenField ID="hidEmpName" runat="server"></asp:HiddenField>
        <asp:HiddenField ID="hidEmpID" runat="server"></asp:HiddenField>
        <asp:HiddenField ID="hidOrgList" runat="server"></asp:HiddenField>
        <asp:HiddenField ID="hidOrgFlowList" runat="server"></asp:HiddenField>
        <asp:HiddenField ID="hidLasChgDate" runat="server"></asp:HiddenField>
        <asp:HiddenField ID="hidCompID" runat="server"></asp:HiddenField>
        <asp:HiddenField ID="hidchkOrganFlow" runat="server"></asp:HiddenField>
        <asp:HiddenField ID="hidSelectedBSValue" runat="server"></asp:HiddenField>
    </div>
    </form>
</body>
</html>
