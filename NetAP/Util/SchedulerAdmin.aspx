<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SchedulerAdmin.aspx.cs" Inherits="Util_SchedulerAdmin"
    UICulture="auto" %>

<%@ Register Src="ucScheduler.ascx" TagName="ucScheduler" TagPrefix="uc1" %>
<!DOCTYPE html >
<html>
<head runat="server">
    <title>Scheduler</title>
    <style>
    .SchedulerAdminHead
    {
        margin: 2px;
        padding-left: 15px;
        font-size: 16px;
        font-weight: bold;
        background-color: #EFEFEF;
        height: 30px;
        line-height: 30px;
        vertical-align: text-bottom;
        display: block;
        cursor: pointer;
    }
    </style>
    <script type="text/javascript">
        function refreshSchedulerParent() {
            if (window.opener != null && !window.opener.closed) {
                window.opener.location.reload();
            }
        }
    </script>
</head>
<body runat="server" id="bodyScheduler" >
    <form id="form1" runat="server">
    <asp:Label ID="labCaption" runat="server" Text="" CssClass="SchedulerAdminHead" Visible="false"></asp:Label>
    <asp:Label ID="labErrMsg" runat="server" Text="" ForeColor="Red" Visible="false"></asp:Label>
    <uc1:ucScheduler ID="Scheduler1" runat="server" />
    </form>
</body>
</html>
