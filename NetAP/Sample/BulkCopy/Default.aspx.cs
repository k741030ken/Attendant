using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using SinoPac.WebExpress.Common;
using SinoPac.WebExpress.DAO;

public partial class Sample_BulkCopy_Default : SecurePage
{
    string _DBName = "NetSample";
    string _AppKeyID = "BulkTest";
    string _CreateFileName = "BulkTest.xlsx";
    int _CreateDataQty = 100000;

    protected void Page_Load(object sender, EventArgs e)
    {
        
        if (!IsPostBack)
        {
            //初始變數
            btnUpload.ucIsPopNewWindow = true;
            btnUpload.ucUploadFileExtList = _CreateFileName.Split('.')[1];
            btnUpload.ucUploadFileMaxKB = 4096;
            btnUpload.ucUploadedCustMsg = "上傳完畢，請關閉視窗進行資料暫存及檢核。。。";
            btnCreate.ucProcessLightboxMsg = string.Format("[{0}]筆資料產生中。。。", _CreateDataQty);
        }

        //頁面執行步驟
        string strAction = txtAction.Value;
        switch (strAction)
        {
            //儲存暫存檔
            case "SaveQueue":
                SaveQueue();
                break;
            //等待資料檢核
            case "WaitDataVerify":
                txtAction.Value = "DataVerify";
                ucLightBox.ucLightBoxMsg = "資料檢核中。。。";
                ucLightBox.Show(true);
                break;
            //資料檢核
            case "DataVerify":
                DataVerify();
                break;
            //等待資料匯入
            case "WaitDataImport":
                txtAction.Value = "DataImport";
                ucLightBox.ucLightBoxMsg = "資料匯入中。。。";
                ucLightBox.Show(true);
                break;
            //資料匯入
            case "DataImport":
                DataImport();
                break;
            default:
                break;
        }

        //事件訂閱
        btnUpload.onClose += btnUpload_onClose;
        btnCreate.onStart += btnCreate_onStart;
    }

    /// <summary>
    /// 產生並下載測試資料
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnCreate_onStart(object sender, EventArgs e)
    {
        //產生測試資料結構
        DataTable dt = new DataTable();
        dt.Columns.Add("Field01");
        dt.Columns.Add("Field02");
        dt.Columns.Add("Field03");

        //產生指定數量測試資料
        int i;
        for (i = 0; i < _CreateDataQty; i++) 
        {
            DataRow dr = dt.NewRow();
            dr[0] = "AA" + i.ToString().PadLeft(6, '0');
            dr[1] = "BB" + i.ToString().PadLeft(6, '0');
            dr[2] = "CC" + i.ToString().PadLeft(6, '0');
            dt.Rows.Add(dr);
        }

        //將匯出資料
        byte[] oBytes = Util.getBytes(Util.getExcelOpenXml(dt));
        FileInfoObj.setFileInfoObj(_CreateFileName, oBytes, true);
        FileInfoObj.DirectDownload();
        btnCreate.Complete("匯出資料準備完成，請按[存檔]下載");
    }

    /// <summary>
    /// 上傳匯入資料
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnUpload_onClose(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(btnUpload.ucUploadedFileName) && btnUpload.ucUploadedFileBody.Length > 0)
        {
            FileInfoObj.setFileInfoObj(btnUpload.ucUploadedFileName, btnUpload.ucUploadedFileBody, true);
            txtAction.Value = "SaveQueue";
            ucLightBox.ucLightBoxMsg = "資料暫存中。。。";
            ucLightBox.Show(true);
        }
        else
        {
            ShowMsg("未收到檔案");
        }
    }

    /// <summary>
    /// 儲存暫存資料
    /// </summary>
    protected void SaveQueue()
    {
        FileInfoObj oFileInfo = FileInfoObj.getFileInfoObj();
        if (oFileInfo.FileSize > 0)
        {
            //將上傳 Excel 內容轉成DataSet
            DataSet ds = Util.getDataSetFromExcel(Util.getStream(oFileInfo.FileBody));
            FileInfoObj.Clear();

            try
            {
                //將上傳結果暫存到資料庫
                if (ds != null && !ds.Tables[0].IsNullOrEmpty())
                {
                    DataTable dt = ds.Tables[0];
                    //計算 PKey
                    var PKeys = Util.getAppKey(_DBName, _AppKeyID, dt.Rows.Count);
                    //更名及新增所需欄位
                    dt.Columns[0].ColumnName = "Col01";
                    dt.Columns[1].ColumnName = "Col02";
                    dt.Columns[2].ColumnName = "Col03";
                    dt.Columns.Add("PUser");
                    dt.Columns.Add("PKey");
                    dt.Columns.Add("UpdUser");
                    dt.Columns.Add("UpdTime");

                    string strUserID = UserInfo.getUserInfo().UserID;
                    DateTime dateNow = DateTime.Now;
                    
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        //處理自訂欄位的值
                        dt.Rows[i]["PUser"] = strUserID;        //記錄暫存資料是由那個 UserID 上傳
                        dt.Rows[i]["PKey"] = PKeys.Item1[i];
                        dt.Rows[i]["UpdUser"] = strUserID;
                        dt.Rows[i]["UpdTime"] = dateNow;
                    }

                    //批量存入 MS-SQL
                    SqlConnection conn = new SqlConnection(DbHelper.getConnectionStrings(_DBName).ConnectionString);
                    conn.Open();
                    using (SqlBulkCopy sqlBC = new SqlBulkCopy(conn))
                    {
                        //設定一批寫入筆數
                        sqlBC.BatchSize = 1000;
                        //設定逾時的秒數
                        sqlBC.BulkCopyTimeout = 60;
                        //設定要寫入的資料表
                        sqlBC.DestinationTableName = "TestBulkQueue";

                        //資料欄位對應
                        sqlBC.ColumnMappings.Add("PUser", "PUser");
                        sqlBC.ColumnMappings.Add("PKey", "PKey");
                        sqlBC.ColumnMappings.Add("Col01", "TabName");
                        sqlBC.ColumnMappings.Add("Col02", "FldName");
                        sqlBC.ColumnMappings.Add("Col03", "Value");
                        sqlBC.ColumnMappings.Add("UpdUser", "UpdUser");
                        sqlBC.ColumnMappings.Add("UpdTime", "UpdTime");

                        //** 進階應用 **
                        //設定 NotifyAfter 屬性，以便在每複製指定筆數後自動呼叫指定事件
                        //sqlBC.NotifyAfter = 10000;
                        //sqlBC.SqlRowsCopied += sqlBC_SqlRowsCopied;

                        //開始寫入
                        sqlBC.WriteToServer(dt);
                    }
                    conn.Dispose();

                    txtAction.Value = "WaitDataVerify";
                    ucLightBox.Show(true);
                }
                else
                {
                    ShowMsg("Excel 無資料");
                }
            }
            catch (Exception ex)
            {
                ShowMsg(ex.Message);
            }

        }
    }

    /// <summary>
    /// 資料檢核
    /// </summary>
    protected void DataVerify()
    {
        bool IsVerify = true;
        //暫停 10 秒，模擬執行檢核作業所需時間
        //若需檢核的資料量太大，可改成分批檢核
        System.Threading.Thread.Sleep(10 * 1000); 
        
        if (IsVerify)
        {
            //檢核成功
            txtAction.Value = "WaitDataImport";
            ucLightBox.Show(true);
        }
        else
        {
            //檢核失敗
            ShowMsg(Util.getHtmlMessage(Util.HtmlMessageKind.Error));
        }
    }

    /// <summary>
    /// 資料匯入
    /// </summary>
    protected void DataImport()
    {
        //從暫存資料區(TestBulkQueue)搬移到正式資料區(TestBulk)
        string strWhereUserID = string.Format(" Where PUser = '{0}' ", UserInfo.getUserInfo().UserID); //暫存資料過濾條件
        DbHelper db = new DbHelper(_DBName);
        int intCloneQty = int.Parse(db.ExecuteDataSet("Select Count(*) From TestBulkQueue " + strWhereUserID).Tables[0].Rows[0][0].ToString());
        if (intCloneQty <= 0)
        {
            ShowMsg(Util.getHtmlMessage(Util.HtmlMessageKind.DataNotFound));
            return;
        }
        CommandHelper sb = db.CreateCommandHelper();
        DbConnection cn = db.OpenConnection();
        DbTransaction tx = cn.BeginTransaction();
        try
        {
            sb.Reset();
            sb.AppendStatement("Insert TestBulk (PKey,TabName,FldName,Value,UpdUser,UpdTime) ");
            sb.Append(" Select PKey,TabName,FldName,Value,UpdUser,UpdTime From TestBulkQueue " + strWhereUserID);
            sb.AppendStatement("Delete From TestBulkQueue " + strWhereUserID);
            db.ExecuteNonQuery(sb.BuildCommand(), tx);
            tx.Commit();
            ShowMsg("資料匯入成功");
        }
        catch (SqlException ex)
        {
            tx.Rollback();
            ShowMsg("資料匯入失敗<br/>" + ex.Message);
        }
        finally
        {
            cn.Close();
            cn.Dispose();
            tx.Dispose();
            ucLightBox.Hide();
        }
    }

    /// <summary>
    /// 顯示作業結果
    /// </summary>
    /// <param name="strMsg"></param>
    /// <param name="IsAutoPostBack"></param>
    protected void ShowMsg(string strMsg, bool IsAutoPostBack = false)
    {
        ucLightBox.Hide();
        divMsg.Style["display"] = "none";
        divMain.Style["display"] = "";
        if (!string.IsNullOrEmpty(strMsg))
        {
            labMsg.Text = strMsg;
            divMsg.Style["display"] = "";
            divMain.Style["display"] = "none";
        }

        if (IsAutoPostBack)
        {
            labMsg.Text = Util.getHtmlMessage(Util.HtmlMessageKind.Waiting, labMsg.Text, 300);
        }
    }

}