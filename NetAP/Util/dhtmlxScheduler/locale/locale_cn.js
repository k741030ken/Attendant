/*
by Andrew Sun. 2015.01.15
*/
scheduler.config.xml_date = '%Y-%m-%d %H:%i';
scheduler.config.day_date="%M/%d (%D)";
scheduler.config.default_date="%Y/%M/%d";
scheduler.config.month_date="%Y / %M";

scheduler.locale={
	date: {
		month_full: ["一月", "二月", "三月", "四月", "五月", "六月", "七月", "八月", "九月", "十月", "十一月", "十二月"],
		month_short: ["01", "02", "03", "04", "05", "06", "07", "08", "09", "10", "11", "12"],
		day_full: ["周日", "周一", "周二", "周三", "周四", "周五", "周六"],
		day_short: ["日", "一", "二", "三", "四", "五", "六"]	
	},
	labels: {
		dhx_cal_today_button: "今天",
		day_tab: "日",
		week_tab: "周",
		month_tab: "月",
		new_event: "新建日程",
		icon_save: "保存",
		icon_cancel: "取消",
		icon_details: "详细",
		icon_edit: "编辑",
		icon_delete: "删除",
		confirm_closing: "请确认是否撤销修改!", //Your changes will be lost, are your sure?
		confirm_deleting: "是否删除日程?",
		section_text: "主旨",
		section_description: "描述",
		section_time: "时间范围",
		full_day: "整天",

		confirm_recurring:"请确认是否将日程设为重复模式?",
		section_recurring:"重复周期",
		button_recurring:"禁用",
		button_recurring_open:"启用",
		button_edit_series: "编辑系列",
		button_edit_occurrence: "编辑实例",
		
		/*agenda view extension*/
		agenda_tab:"议程",
		date:"日期",
		description:"说明",
		
		/*year view extension*/
		year_tab:"今年",

		/*week agenda view extension*/
		week_agenda_tab: "议程",

		/*grid view extension*/
		grid_tab:"电网",

		/* touch tooltip*/
		drag_to_create:"拖曳新增",
		drag_to_move:"拖曳移动",

		/* dhtmlx message default buttons */
		message_ok:"确定",
		message_cancel:"取消"
	}
};

