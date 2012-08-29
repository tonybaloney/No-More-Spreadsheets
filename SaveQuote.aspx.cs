using System;
using System.Collections.Generic;
using System.Web;
using System.Configuration;
using com.ashaw.pricing;
using System.Web.Script.Serialization;

public partial class SaveQuote : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        int quoteId = Convert.ToInt32(Request.Params["quote_id"]);

        Quote quote = new Quote(quoteId);

        // Remove the old items from this quote
        quote.ClearItems();

        // Get the new quote items, will be a JSON array in the POST field
        string quoteItemsString = Request.Params["quote_items"];
        if ( String.IsNullOrEmpty(Request.Params["quote_items"]) || String.IsNullOrEmpty(Request.Params["quote_id"])){
            Response.StatusCode = 500;
            Response.End();
        }
        // Deserialise the JSON into QuoteItem objects and create each one (insert into DB)
        JavaScriptSerializer jsl = new JavaScriptSerializer();
        List<QuoteItem> quoteItems =(List<QuoteItem>)jsl.Deserialize(quoteItemsString, typeof(List<QuoteItem>));
        foreach (QuoteItem item in quoteItems)
            item.Create();

        // Get the quote and update the fields from the form on the ViewQuote page.
        quote.Revision++;
        quote.DiscountPercent = Convert.ToDouble(Request.Params["discount_percent"]);
        quote.DiscountPercent24 = Convert.ToDouble(Request.Params["discount_percent_24"]);
        quote.DiscountPercent36 = Convert.ToDouble(Request.Params["discount_percent_36"]);
        quote.DiscountPercentSetup = Convert.ToDouble(Request.Params["discount_percent_setup"]);
        quote.DiscountWritein = Convert.ToDouble(Request.Params["discount_writein"]);
        quote.Title = Request.Params["title"];
        quote.LastChange = DateTime.Now;
        quote.Save();
    }
}