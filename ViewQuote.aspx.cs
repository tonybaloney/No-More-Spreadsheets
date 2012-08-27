using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using com.ashaw.pricing;

public partial class ViewQuote : System.Web.UI.Page
{
    public string html_currency_char;
    public int quoteId;
    public Quote quote;
    public bool hasFullPermissions;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!String.IsNullOrEmpty(Request.Params["QuoteId"]))
        {
            this.quoteId = Convert.ToInt32(Request.Params["QuoteId"]);
            this.quote = new Quote(this.quoteId);
            this.hasFullPermissions = false;
        }
        else
        {
            Response.Write("Quote ID not passed.");
            Response.StatusCode = 500;
            Response.End();
        }
    }
}