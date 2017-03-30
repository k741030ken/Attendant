<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucScheduler.ascx.cs" Inherits="Util_ucScheduler" %>
<div id="mini_here" runat="server">
</div>
<div id="scheduler_here" class="dhx_cal_container" runat="server">
    <div class="dhx_cal_navline" id="dhx_cal_navline" runat="server">
        <div class="dhx_cal_prev_button">
            &nbsp;
        </div>
        <div class="dhx_cal_next_button">
            &nbsp;
        </div>
        <div class="dhx_cal_today_button" style="width: 50px;">
        </div>
        <div class="dhx_cal_date">
        </div>
        <div class="dhx_minical_icon" id="dhx_minical_icon" runat="server" onclick="show_minical()">
        </div>
        <%--  <div class="dhx_cal_tab" name="year_tab" id="dhx_year_tab" runat="server" ></div> --%>
        <div class="dhx_cal_tab" name="day_tab">
        </div>
        <div class="dhx_cal_tab" name="week_tab">
        </div>
        <div class="dhx_cal_tab" name="month_tab">
        </div>
        <div class="dhx_cal_tab" id="yearView" style="display: none" name="year_tab">
        </div>
    </div>
    <div class="dhx_cal_header">
    </div>
    <div class="dhx_cal_data">
    </div>
</div>
