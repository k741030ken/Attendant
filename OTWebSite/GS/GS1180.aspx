<%@ Page Language="VB" AutoEventWireup="false" CodeFile="GS1180.aspx.vb" Inherits="GS_GS1180" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
    <script src="../ClientFun/jquery-1.8.3.min.js" type="text/javascript"></script>
    <script src="../ClientFun/jquery.dragsort-0.5.2.js" type="text/javascript"></script>
    <link type="text/css" rel="stylesheet" href="../StyleSheet.Css" />
    <script type="text/javascript">
    <!--
        function funAction(Param) {
            switch (Param) {
                case "btnUpdate":
                    if (!confirm('將依照您調整後各考績等級之順序更新排序資料。\n考績為特優、優、甲下以下及考績與前一評核結果不同者，請填寫考績補充說明。'))
                        return false;
                    break;
            }
        }


       -->
    </script>
</head>
<body style="margin-top:5px; margin-left:5px; margin-right:5px; margin-bottom:0" >
    <form id="frmContent" runat="server">
        <ajaxToolkit:ToolkitScriptManager runat="Server" EnableScriptGlobalization="true"
            EnableScriptLocalization="true" ID="ScriptManager1" />
        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">
            <tr>
                <td style="padding-bottom:5px;width: 100%">
                    <asp:Label ID="txtDeptID" runat="server" Text="" Font-Names="微軟正黑體"></asp:Label>
                </td>
            </tr>
            <tr>     
                <td style="padding-bottom:5px;width: 100%">
                    <asp:Label ID="txtGroup" runat="server" Text="" Font-Names="微軟正黑體"></asp:Label>    
                </td>                           
            </tr>
            <tr>
                <td style="width: 100%">            
                    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%" class="tbl_Content">
                        <tr>
                            <td id="tdGrade9" runat="server" valign="top">
                                <asp:HiddenField ID="hidValue9" runat="server"></asp:HiddenField>                              
                                <asp:Label ID="labGrade9" runat="server" Text="特優" CssClass="InputTextStyle_Thin" Font-Names="微軟正黑體" Font-Bold="true" Font-Size="Large"></asp:Label>
                                <div style="border-style:inset;width:96%;background-color:White;font-family:@微軟正黑體">                                 
                                    <ol id="grade9" style="font-family:@微軟正黑體">
                                        <asp:ListView ID="Grade9" runat="server">
                                            <ItemTemplate>
                                                <li data-itemid='<%# Eval("EmpID") %>' style="font-family:@微軟正黑體"> <%--<%# Eval("EmpID") %>-<%# Eval("GradeDeptID") %>--%>
                                                    <div style="background-color:#FFD9EC;font-family:微軟正黑體;width:96%;font-size:smaller"><%# Eval("ShowData")%></div>
                                                </li>
                                            </ItemTemplate>
                                        </asp:ListView>
                                    </ol>
                                </div>
                            </td>
                            <td id="tdGrade1" runat="server" valign="top">
                                  
                                <asp:HiddenField ID="hidValue1" runat="server"></asp:HiddenField> 
                                <asp:Label ID="labGrade1" runat="server" Text="優" CssClass="InputTextStyle_Thin" Font-Names="微軟正黑體" Font-Bold="true" Font-Size="Large"></asp:Label>
                                <div style="border-style:inset;width:96%;background-color:White;font-family:@微軟正黑體">                              
                                    <ol id="grade1" style="font-family:@微軟正黑體" >
                                        <asp:ListView ID="Grade1" runat="server">                                            
                                            <ItemTemplate>
                                                <li data-itemid='<%# Eval("EmpID") %>' style="font-family:@微軟正黑體"> <%--<%# Eval("EmpID") %>-<%# Eval("GradeDeptID") %>--%>
                                                    <div style="background-color:#FFD9EC;font-family:微軟正黑體;width:96%;font-size:smaller"><%# Eval("ShowData")%></div>
                                                </li>
                                            </ItemTemplate>
                                        </asp:ListView>
                                    </ol>
                                </div>
                            </td>
                            <td id="tdGrade6" runat="server" valign="top">
                                <asp:HiddenField ID="hidValue6" runat="server"></asp:HiddenField> 
                                <asp:Label ID="lblGrade6" runat="server" Text="甲上" CssClass="InputTextStyle_Thin" Font-Names="微軟正黑體" Font-Bold="true" Font-Size="Large"></asp:Label>
                                <div style="border-style:inset;width:96%;background-color:White;font-family:@微軟正黑體">                                    
                                    <ol id="grade6" style="font-family:@微軟正黑體">
                                        <asp:ListView ID="Grade6" runat="server">
                                            <ItemTemplate>
                                                <li data-itemid='<%# Eval("EmpID") %>' style="font-family:@微軟正黑體"> <%--<%# Eval("EmpID") %>-<%# Eval("GradeDeptID") %>--%>
                                                    <div style="background-color:#FFD9EC;font-family:微軟正黑體;width:96%;font-size:smaller"><%# Eval("ShowData")%></div>
                                                </li>
                                            </ItemTemplate>
                                        </asp:ListView>
                                    </ol>
                                </div>
                            </td>
                            <td id="tdGrade2" runat="server" valign="top">
                                <asp:HiddenField ID="hidValue2" runat="server"></asp:HiddenField> 
                                <asp:Label ID="lblGrade2" runat="server" Text="甲" CssClass="InputTextStyle_Thin" Font-Names="微軟正黑體" Font-Bold="true" Font-Size="Large"></asp:Label>
                                <div style="border-style:inset;width:96%;background-color:White;font-family:@微軟正黑體">                                    
                                    <ol id="grade2" style="font-family:@微軟正黑體">
                                        <asp:ListView ID="Grade2" runat="server">
                                            <ItemTemplate>
                                                <li data-itemid='<%# Eval("EmpID") %>' style="font-family:@微軟正黑體"> <%--<%# Eval("EmpID") %>-<%# Eval("GradeDeptID") %>--%>
                                                    <div style="background-color:#FFD9EC;font-family:微軟正黑體;width:96%;font-size:smaller"><%# Eval("ShowData")%></div>
                                                </li>
                                            </ItemTemplate>
                                        </asp:ListView>
                                    </ol>
                                </div>
                            </td>
                            <td id="tdGrade7" runat="server" valign="top">
                                <asp:HiddenField ID="hidValue7" runat="server"></asp:HiddenField> 
                                <asp:Label ID="lblGrade7" runat="server" Text="甲下" CssClass="InputTextStyle_Thin" Font-Names="微軟正黑體" Font-Bold="true" Font-Size="Large"></asp:Label>
                                <div style="border-style:inset;width:96%;background-color:White;font-family:@微軟正黑體">                                    
                                    <ol id="grade7" style="font-family:@微軟正黑體">
                                        <asp:ListView ID="Grade7" runat="server">
                                            <ItemTemplate>
                                                <li data-itemid='<%# Eval("EmpID") %>' style="font-family:@微軟正黑體"> <%--<%# Eval("EmpID") %>-<%# Eval("GradeDeptID") %>--%>
                                                    <div style="background-color:#FFD9EC;font-family:微軟正黑體;width:96%;font-size:smaller"><%# Eval("ShowData")%></div>
                                                </li>
                                            </ItemTemplate>
                                        </asp:ListView>
                                    </ol>
                                </div>
                            </td>
                            <td id="tdGrade3" runat="server" valign="top">
                                <asp:HiddenField ID="hidValue3" runat="server"></asp:HiddenField> 
                                <asp:Label ID="lblGrade3" runat="server" Text="乙" CssClass="InputTextStyle_Thin" Font-Names="微軟正黑體" Font-Bold="true" Font-Size="Large"></asp:Label>
                                <div style="border-style:inset;width:96%;background-color:White;font-family:@微軟正黑體">                                    
                                    <ol id="grade3" style="font-family:@微軟正黑體">
                                        <asp:ListView ID="Grade3" runat="server">
                                            <ItemTemplate>
                                                <li data-itemid='<%# Eval("EmpID") %>' style="font-family:@微軟正黑體"> <%--<%# Eval("EmpID") %>-<%# Eval("GradeDeptID") %>--%>
                                                    <div style="background-color:#FFD9EC;font-family:微軟正黑體;width:96%;font-size:smaller"><%# Eval("ShowData")%></div>
                                                </li>
                                            </ItemTemplate>
                                        </asp:ListView>
                                    </ol>
                                </div>
                            </td>
                            <td id="tdGrade4" runat="server" valign="top">
                                <asp:HiddenField ID="hidValue4" runat="server"></asp:HiddenField> 
                                <asp:Label ID="lblGrade4" runat="server" Text="丙" CssClass="InputTextStyle_Thin" Font-Names="微軟正黑體" Font-Bold="true" Font-Size="Large"></asp:Label>
                                <div style="border-style:inset;width:96%;background-color:White;font-family:@微軟正黑體">                                    
                                    <ol id="grade4" style="font-family:@微軟正黑體">
                                        <asp:ListView ID="Grade4" runat="server">
                                            <ItemTemplate>
                                                <li data-itemid='<%# Eval("EmpID") %>' style="font-family:@微軟正黑體"> <%--<%# Eval("EmpID") %>-<%# Eval("GradeDeptID") %>--%>
                                                    <div style="background-color:#FFD9EC;font-family:微軟正黑體;width:96%;font-size:smaller"><%# Eval("ShowData")%></div>
                                                </li>
                                            </ItemTemplate>
                                        </asp:ListView>
                                    </ol>
                                </div>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <script type="text/javascript">
            
            $("#grade9").dragsort({ dragSelector: "div", dragEnd: saveOrder9, placeHolderTemplate: "<li class='placeHolder'><div></div></li>" });
            $("#grade1").dragsort({ dragSelector: "div", dragEnd: saveOrder1, placeHolderTemplate: "<li class='placeHolder'><div></div></li>" });
            $("#grade6").dragsort({ dragSelector: "div", dragEnd: saveOrder6, placeHolderTemplate: "<li class='placeHolder'><div></div></li>" });
            $("#grade2").dragsort({ dragSelector: "div", dragEnd: saveOrder2, placeHolderTemplate: "<li class='placeHolder'><div></div></li>" });
            $("#grade7").dragsort({ dragSelector: "div", dragEnd: saveOrder7, placeHolderTemplate: "<li class='placeHolder'><div></div></li>" });
            $("#grade3").dragsort({ dragSelector: "div", dragEnd: saveOrder3, placeHolderTemplate: "<li class='placeHolder'><div></div></li>" });
            $("#grade4").dragsort({ dragSelector: "div", dragEnd: saveOrder4, placeHolderTemplate: "<li class='placeHolder'><div></div></li>" });

            function saveOrder() {
                saveOrder9()
                saveOrder1()
                saveOrder6()
                saveOrder2()
                saveOrder7()
                saveOrder3()
                saveOrder4()
//                var data = $("#grade9 li").map(function () { return $(this).data("itemid"); }).get();
//                
//                $.ajax({
//                    url: "../GS/GS1180.aspx/SaveListOrder",
//                    data: '{EmpID:["' + data.join('","') + '"]}',
//                    dataType: "json",
//                    type: "POST",
//                    contentType: "application/json; charset=utf-8",
//                    success: function (result) {
//                        //callback function result(on success)
//                        //alert(result.d);
//                        //resultConfirm = confirm(result.d);//不可以在這邊直接下return,會無效
//                        //fromField = document.getElementById("lblData");
//                        //fromField.innerText = result.d
//                        fromField = document.getElementById("hidValue");
//                        fromField.value = result.d
//                        //document.write(result.d);
//                    },
//                    failure: function (response) {
//                        //callback function result(on failure)
//                        alert(response.d);
//                    }

//                });
            };
            function saveOrder9() {
                var data = $("#grade9 li").map(function () { return $(this).data("itemid"); }).get();

                $.ajax({
                    url: "../GS/GS1180.aspx/SaveListOrder",
                    data: '{EmpID:["' + data.join('","') + '"]}',
                    dataType: "json",
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    success: function (result) {
                        //callback function result(on success)
                        //alert(result.d);
                        //resultConfirm = confirm(result.d);//不可以在這邊直接下return,會無效
                        //fromField = document.getElementById("lblData");
                        //fromField.innerText = result.d
                        fromField = document.getElementById("hidValue9");
                        fromField.value = result.d
                        //document.write(result.d);
                    },
                    failure: function (response) {
                        //callback function result(on failure)
                        alert(response.d);
                    }

                });
            };
            function saveOrder1() {
                var data = $("#grade1 li").map(function () { return $(this).data("itemid"); }).get();

                $.ajax({
                    url: "../GS/GS1180.aspx/SaveListOrder",
                    data: '{EmpID:["' + data.join('","') + '"]}',
                    dataType: "json",
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    success: function (result) {
                        //callback function result(on success)
                        //alert(result.d);
                        //resultConfirm = confirm(result.d);//不可以在這邊直接下return,會無效
                        //fromField = document.getElementById("lblData");
                        //fromField.innerText = result.d
                        fromField = document.getElementById("hidValue1");
                        fromField.value = result.d
                        //document.write(result.d);
                    },
                    failure: function (response) {
                        //callback function result(on failure)
                        alert(response.d);
                    }

                });
            };
            function saveOrder6() {
                var data = $("#grade6 li").map(function () { return $(this).data("itemid"); }).get();

                $.ajax({
                    url: "../GS/GS1180.aspx/SaveListOrder",
                    data: '{EmpID:["' + data.join('","') + '"]}',
                    dataType: "json",
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    success: function (result) {
                        //callback function result(on success)
                        //alert(result.d);
                        //resultConfirm = confirm(result.d);//不可以在這邊直接下return,會無效
                        //fromField = document.getElementById("lblData");
                        //fromField.innerText = result.d
                        fromField = document.getElementById("hidValue6");
                        fromField.value = result.d
                        //document.write(result.d);
                    },
                    failure: function (response) {
                        //callback function result(on failure)
                        alert(response.d);
                    }

                });
            };
            function saveOrder2() {
                var data = $("#grade2 li").map(function () { return $(this).data("itemid"); }).get();

                $.ajax({
                    url: "../GS/GS1180.aspx/SaveListOrder",
                    data: '{EmpID:["' + data.join('","') + '"]}',
                    dataType: "json",
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    success: function (result) {
                        //callback function result(on success)
                        //alert(result.d);
                        //resultConfirm = confirm(result.d);//不可以在這邊直接下return,會無效
                        //fromField = document.getElementById("lblData");
                        //fromField.innerText = result.d
                        fromField = document.getElementById("hidValue2");
                        fromField.value = result.d
                        //document.write(result.d);
                    },
                    failure: function (response) {
                        //callback function result(on failure)
                        alert(response.d);
                    }

                });
            };
            function saveOrder7() {
                var data = $("#grade7 li").map(function () { return $(this).data("itemid"); }).get();

                $.ajax({
                    url: "../GS/GS1180.aspx/SaveListOrder",
                    data: '{EmpID:["' + data.join('","') + '"]}',
                    dataType: "json",
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    success: function (result) {
                        //callback function result(on success)
                        //alert(result.d);
                        //resultConfirm = confirm(result.d);//不可以在這邊直接下return,會無效
                        //fromField = document.getElementById("lblData");
                        //fromField.innerText = result.d
                        fromField = document.getElementById("hidValue7");
                        fromField.value = result.d
                        //document.write(result.d);
                    },
                    failure: function (response) {
                        //callback function result(on failure)
                        alert(response.d);
                    }

                });
            };
            function saveOrder3() {
                var data = $("#grade3 li").map(function () { return $(this).data("itemid"); }).get();

                $.ajax({
                    url: "../GS/GS1180.aspx/SaveListOrder",
                    data: '{EmpID:["' + data.join('","') + '"]}',
                    dataType: "json",
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    success: function (result) {
                        //callback function result(on success)
                        //alert(result.d);
                        //resultConfirm = confirm(result.d);//不可以在這邊直接下return,會無效
                        //fromField = document.getElementById("lblData");
                        //fromField.innerText = result.d
                        fromField = document.getElementById("hidValue3");
                        fromField.value = result.d
                        //document.write(result.d);
                    },
                    failure: function (response) {
                        //callback function result(on failure)
                        alert(response.d);
                    }

                });
            };
            function saveOrder4() {
                var data = $("#grade4 li").map(function () { return $(this).data("itemid"); }).get();

                $.ajax({
                    url: "../GS/GS1180.aspx/SaveListOrder",
                    data: '{EmpID:["' + data.join('","') + '"]}',
                    dataType: "json",
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    success: function (result) {
                        //callback function result(on success)
                        //alert(result.d);
                        //resultConfirm = confirm(result.d);//不可以在這邊直接下return,會無效
                        //fromField = document.getElementById("lblData");
                        //fromField.innerText = result.d
                        fromField = document.getElementById("hidValue4");
                        fromField.value = result.d
                        //document.write(result.d);
                    },
                    failure: function (response) {
                        //callback function result(on failure)
                        alert(response.d);
                    }

                });
            };
	    </script>
    </form>
</body>
</html>
