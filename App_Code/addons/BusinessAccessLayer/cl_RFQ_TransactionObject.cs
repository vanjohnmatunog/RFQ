using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for cl_RFQ_TransactionObject
/// </summary>
public class cl_RFQ_TransactionObject
{
    public string RefID { get; set; }
    public string Reqname { get; set; }
    public string Dept { get; set; }
    public string Local { get; set; }
    public string Location { get; set; }
    public string Category { get; set; }
    public string NoOfReq { get; set; }
    public string Attributes1 { get; set; }
    public string Attributes2 { get; set; }
    public string Attributes3 { get; set; }
    public string CurrentUser { get; set; }
    public string CurrentUserEmpNo { get; set; }
    public Boolean isAdmin { get; set; }
}