using System;
using System.Web;
using SinoPac.WebExpress.Common;

public partial class ErrorPage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Util.setCSS();
        labApplicationName.Text = Util.getAppSetting("app://ApplicationName/");
        if (Server.GetLastError() != null)
        {
            string strErrType = "";
            string strErrMsg = "";

            //SqlException
            if (string.IsNullOrEmpty(strErrType))
            {
                if (Server.GetLastError().GetBaseException() is System.Data.SqlClient.SqlException)
                {
                    //SqlException
                    System.Data.SqlClient.SqlException ex = (System.Data.SqlClient.SqlException)Server.GetLastError().GetBaseException();
                    strErrType += string.Format(SinoPac.WebExpress.Common.Properties.Resources.Msg_ErrorType, ex.GetType().Name);
                    strErrType += "　" + string.Format(SinoPac.WebExpress.Common.Properties.Resources.Msg_ErrorCode, ex.ErrorCode);
                    strErrMsg += ex.Message.ToString();
                }
            }

            //HttpException
            if (string.IsNullOrEmpty(strErrType)) 
            {
                if (Server.GetLastError().GetBaseException() is HttpException)
                {
                    HttpException ex = (HttpException)Server.GetLastError().GetBaseException();
                    strErrType += string.Format(SinoPac.WebExpress.Common.Properties.Resources.Msg_ErrorType, ex.GetType().Name);
                    strErrType += "　" + string.Format(SinoPac.WebExpress.Common.Properties.Resources.Msg_ErrorCode, ex.GetHttpCode());
                    strErrMsg += ex.Message.ToString();
                }
            }

            //Exception
            if (string.IsNullOrEmpty(strErrType))
            {
                Exception ex = (Exception)Server.GetLastError().GetBaseException();
                strErrType += string.Format(SinoPac.WebExpress.Common.Properties.Resources.Msg_ErrorType, ex.GetType().Name);
                strErrMsg += ex.Message.ToString();
            }

            labErrType.Text = Util.getHtmlMessage(Util.HtmlMessageKind.Error, strErrType);
            labErrMsg.Text = string.Format("<div class='Util_Frame' style='padding:10px;background-color: #ccc;'>{0}</div>", strErrMsg);
        }
    }
}