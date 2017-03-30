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
		day_full: ["週日", "週一", "週二", "週三", "週四", "週五", "週六"],
		day_short: ["日", "一", "二", "三", "四", "五", "六"]
	},
	labels: {
		dhx_cal_today_button: "今日",
		day_tab: "日",
		week_tab: "週",
		month_tab: "月",
		new_event: "新建日程",
		icon_save: "儲存",
		icon_cancel: "取消",
		icon_details: "詳細",
		icon_edit: "編輯",
		icon_delete: "刪除",
		confirm_closing: "請確認是否取消修改!", //Your changes will be lost, are your sure?
		confirm_deleting: "是否刪除日程?",
		section_text: "主旨",
		section_description: "說明",
		section_time: "時間範圍",
		full_day: "整天",

		confirm_recurring:"請確認是否將日程設為重複模式?",
		section_recurring:"重複週期",
		button_recurring:"禁用",
		button_recurring_open:"啟用",
		button_edit_series: "編輯重複議程",
		button_edit_occurrence: "編輯單筆議程",
		
		/*agenda view extension*/
		agenda_tab:"議程",
		date:"日期",
		description:"說明",
		
		/*year view extension*/
		year_tab:"年度",

		/*week agenda view extension*/
		week_agenda_tab: "議程",

		/*grid view extension*/
		grid_tab:"網格",

		/* touch tooltip*/
		drag_to_create:"拖曳新增",
		drag_to_move:"拖曳移動",

		/* dhtmlx message default buttons */
		message_ok:"確定",
		message_cancel:"取消"
	}
};

