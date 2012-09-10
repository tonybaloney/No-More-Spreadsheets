using System;
using System.Web;
using com.ashaw.pricing;

public partial class CreateQuote : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        int cloneId = -1;
        if ( !String.IsNullOrEmpty(Request.Params["cloneid"])  )
            cloneId = Convert.ToInt32 ( Request.Params["cloneid"] );
        if (!String.IsNullOrEmpty(Request.Params["quote_create"]))
        {
            // Associate owner from form.
            int newOwner = Convert.ToInt32(Request.Params["Owner"]);
            // TODO: Integrate customer lookup with internal system to get customer 'ID'
            int customerId = 0; // Un-used
            // From form
            string customerName = Request.Params["q_customer_name"];
            // From form
            int pricelistId = Convert.ToInt32(Request.Params["q_pricelist"]);
            // From form
            string quoteTitle = Request.Params["q_title"];
            // Create a new quote in the database
            Quote newQuote = Quote.CreateQuote ( newOwner, customerId, customerName, pricelistId, quoteTitle ) ;
            // If we are cloning quotes, copy the items across
            if ( cloneId != -1 ) {
                newQuote.CloneItems ( cloneId ) ;
                newQuote.UpdateTotals () ;
            }
            // Show the new quote.
            Response.Redirect("ViewQuote.aspx?QuoteId=" + newQuote.Id.ToString());
        }
    }
}