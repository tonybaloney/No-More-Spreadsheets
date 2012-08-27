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
        /*
            foreach ($quote_items as $qi){
	            if(!isset($qi->qi_notes))
		            $qi->qi_notes = '';
                AddQuoteItem ( $quote_id , $qi->pi_id, $qi->qi_title, $qi->qi_description, $qi->qi_quantity, $qi->qi_setupcost, $qi->qi_recurringcost, $qi->qi_setupprice, $qi->qi_recurringprice, $qi->qi_groupname, $qi->qi_subgroup, $qi->qi_partcode,$qi->qi_notes,$qi->qi_index,$qi->qi_ispart,$qi->qi_isbundle,$qi->qi_bundleid );
            }
            LogQuoteChange($quote_id,$quote['q_totalvalue'],$_SESSION['currentuserid'], $quote['q_revision']);
         */
        string quoteItemsString = Request.Params["quote_items"];
        if ( String.IsNullOrEmpty(Request.Params["quote_items"]) || String.IsNullOrEmpty(Request.Params["quote_id"])){
            Response.StatusCode = 500;
            Response.End();
        }
        JavaScriptSerializer jsl = new JavaScriptSerializer();
        List<QuoteItem> quoteItems =(List<QuoteItem>)jsl.Deserialize(quoteItemsString, typeof(List<QuoteItem>));

        int quoteId = Convert.ToInt32(Request.Params["quote_id"]);

        Quote quote = new Quote(quoteId);
        quote.ClearItems();
        quote.DiscountPercent = Convert.ToDouble(Request.Params["discount_percent"]);
        quote.DiscountPercent24 = Convert.ToDouble(Request.Params["discount_percent_24"]);
        quote.DiscountPercent36 = Convert.ToDouble(Request.Params["discount_percent_36"]);
        quote.DiscountPercentSetup = Convert.ToDouble(Request.Params["discount_percent_setup"]);
        quote.DiscountWritein = Convert.ToDouble(Request.Params["discount_writein"]);
        quote.Title = Request.Params["title"];
        quote.LastChange = DateTime.Now;
        quote.Save();
        //quote.UpdateTotals();
    }
}