using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class EWebUserHeader
{
    public string PageName { get; set; }

    public string ReferenceNo { get; set; }

    public string RemoteAddr { get; set; }

    public string RequestTime { get; set; }

    public System.DateTime? SsaTimeStamp { get; set; }

    public bool SsaTimeStampSpecified { get; set; }

    public string SsaToken { get; set; }

    public string TxCode { get; set; }

    public string UserID { get; set; }
}
