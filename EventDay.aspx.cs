using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class EventDay : System.Web.UI.Page
{
    public string Date;
    protected void Page_Load(object sender, EventArgs e)
    {
        string sendedDate = Request.QueryString["SendingDate"];
        lblID.Text = sendedDate.ToString();
        Date = sendedDate;
    }
}