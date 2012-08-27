using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace com.ashaw.pricing
{
    /// <summary>
    /// Summary description for Quote
    /// </summary>
    public class Quote : DataObject
    {
        /// <summary>
        /// Creates the quote.
        /// </summary>
        /// <param name="ownerId">The owner id.</param>
        /// <param name="customerId">The customer id.</param>
        /// <param name="customerName">Name of the customer.</param>
        /// <param name="pricelistId">The pricelist id.</param>
        /// <param name="title">The title.</param>
        /// <returns></returns>
        static public Quote CreateQuote(int ownerId, int customerId, string customerName, int pricelistId, string title)
        {
            DatabaseConnection db = new DatabaseConnection();
            object result = db.SProcToObject("CreateQuote", new KeyValuePair<string, object>("@OwnerId", ownerId), new KeyValuePair<string, object>("@CustomerId", customerId), new KeyValuePair<string, object>("@CustomerName", customerName), new KeyValuePair<string, object>("@PricelistId", pricelistId), new KeyValuePair<string, object>("@QuoteTitle", title));
            db.Dispose();
            Quote q = new Quote();
            q.Id = Convert.ToInt32(result);
            return q;
        }

        /// <summary>
        /// Clones the items from the origin quote to the target quote.
        /// </summary>
        /// <param name="targetQuoteId">The target quote id.</param>
        /// <param name="originQuoteId">The origin quote id.</param>
        static public void CloneItems(int targetQuoteId, int originQuoteId)
        {
            //	PricingDBQuery ( "INSERT INTO `quote_items` (`quote_id`, `pi_id`, `qi_title`, `qi_description`, `qi_quantity`, `qi_setupcost`, `qi_recurringcost`,`qi_setupprice`,`qi_recurringprice`, `qi_groupname`, `qi_subgroup`, `qi_partcode`, `qi_notes`,`qi_index`,`qi_ispart`,`qi_isbundle`,`qi_bundleid` ) SELECT $newquoteid, `pi_id`, `qi_title`, `qi_description`, `qi_quantity`, `qi_setupcost`, `qi_recurringcost`,`qi_setupprice`,`qi_recurringprice`, `qi_groupname`, `qi_subgroup`, `qi_partcode`,`qi_notes`,`qi_index`,`qi_ispart`,`qi_isbundle`,`qi_bundleid` FROM `quote_items` WHERE `quote_id` = $quoteid");
        }

        /// <summary>
        /// Clones the items.
        /// </summary>
        /// <param name="originQuoteId">The origin quote id.</param>
        public void CloneItems ( int originQuoteId ) { Quote.CloneItems( this.Id, originQuoteId ) ; } 

        /// <summary>
        /// Updates the totals values of the quote by adding the values of the pricing items.
        /// </summary>
        /// <param name="quoteId">The quote id.</param>
        static public void UpdateTotals(int quoteId)
        {
            DatabaseConnection db = new DatabaseConnection();
            object result = db.SProcToObject("UpdateTotals", new KeyValuePair<string, object>("@QuoteId", quoteId));
            db.Dispose();
        }

        /// <summary>
        /// Updates the totals.
        /// </summary>
        public void UpdateTotals () { Quote.UpdateTotals( this.Id ) ; }

        /// <summary>
        /// Clears the items.
        /// </summary>
        /// <param name="quoteId">The quote id.</param>
        static public void ClearItems(int quoteId)
        {
            DatabaseConnection db = new DatabaseConnection();
            object result = db.SProcToObject("ClearItems", new KeyValuePair<string, object>("@QuoteId", quoteId));
            db.Dispose();
        }

        /// <summary>
        /// Clears the items.
        /// </summary>
        public void ClearItems()
        {
            ClearItems(this.Id);
        }

        /// <summary>
        /// Saves this instance.
        /// </summary>
        public void Save()
        {
            DatabaseConnection db= new DatabaseConnection();
            db.RunScalarCommand(new System.Data.SqlClient.SqlCommand(this.GetSaveSQL(this.Id,"Quotes")));
            db.Dispose();
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="Quote" /> class.
        /// </summary>
        public Quote() {}

        /// <summary>
        /// Initializes a new instance of the <see cref="Quote" /> class.
        /// </summary>
        /// <param name="id">The id.</param>
        public Quote(int id) : base(id,"GetQuote")
        {
            
        }

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        /// <value>
        /// The id.
        /// </value>
        [DataField("Id","q_id")]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the owner.
        /// </summary>
        /// <value>
        /// The name of the owner.
        /// </value>
        [DataField("OwnerName","q_ownername")]
        public string OwnerName { get; set; }

        /// <summary>
        /// Gets or sets the customer id.
        /// </summary>
        /// <value>
        /// The customer id.
        /// </value>
        [DataField("CustomerId","q_customer_id")]
        public string CustomerId { get; set; }

        /// <summary>
        /// Gets or sets the name of the customer.
        /// </summary>
        /// <value>
        /// The name of the customer.
        /// </value>
        [DataField("CustomerName","q_customer_name","Ext.data.SortTypes.asUCString")]
        public string CustomerName { get; set; }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>
        /// The title.
        /// </value>
        [DataField("Title","q_title", "Ext.data.SortTypes.asUCString")]
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        [DataField("Status","q_status")]
        public string Status { get; set; }

        /// <summary>
        /// Gets or sets the discount percent.
        /// </summary>
        /// <value>
        /// The discount percent.
        /// </value>
        [DataField("DiscountPercent","q_discount_percent")]
        public double DiscountPercent { get; set; }

        /// <summary>
        /// Gets or sets the discount percent24.
        /// </summary>
        /// <value>
        /// The discount percent24.
        /// </value>
        [DataField("DiscountPercent24", "q_discount_percent")]
        public double DiscountPercent24 { get; set; }

        /// <summary>
        /// Gets or sets the discount percent36.
        /// </summary>
        /// <value>
        /// The discount percent36.
        /// </value>
        [DataField("DiscountPercent36", "q_discount_percent")]
        public double DiscountPercent36 { get; set; }

        /// <summary>
        /// Gets or sets the discount writein.
        /// </summary>
        /// <value>
        /// The discount writein.
        /// </value>
        [DataField("DiscountWritein","q_discount_writein")]
        public double DiscountWritein { get; set; }

        /// <summary>
        /// Gets or sets the discount percent setup.
        /// </summary>
        /// <value>
        /// The discount percent setup.
        /// </value>
        [DataField("DiscountPercentSetup", "q_discount_percent_setup")]
        public double DiscountPercentSetup { get; set; }

        /// <summary>
        /// Gets or sets the last change.
        /// </summary>
        /// <value>
        /// The last change.
        /// </value>
        [DataField("LastChanged","q_last_change")]
        public DateTime LastChange { get; set; }

        /// <summary>
        /// Gets or sets the name of the pricelist.
        /// </summary>
        /// <value>
        /// The name of the pricelist.
        /// </value>
        [DataField("PricelistName","q_pricelist_name")]
        public string PricelistName { get; set; }

        /// <summary>
        /// Gets or sets the total value.
        /// </summary>
        /// <value>
        /// The total value.
        /// </value>
        [DataField("TotalValue","q_totalvalue")]
        public double TotalValue { get; set; }

        /// <summary>
        /// Gets or sets the total setup value.
        /// </summary>
        /// <value>
        /// The total setup value.
        /// </value>
        [DataField("TotalSetupValue", "q_total_setup_value")]
        public double TotalSetupValue { get; set; }

        /// <summary>
        /// Gets or sets the closed date.
        /// </summary>
        /// <value>
        /// The closed date.
        /// </value>
        [DataField("ClosedDate", "q_closed_date")]
        public DateTime ClosedDate { get; set; }

        /// <summary>
        /// Gets or sets the revision.
        /// </summary>
        /// <value>
        /// The revision.
        /// </value>
        [DataField("Revision", "q_revision")]
        public int Revision { get; set; }

        /// <summary>
        /// Gets or sets the created date.
        /// </summary>
        /// <value>
        /// The created date.
        /// </value>
        [DataField("CreatedDate", "q_created_date")]
        public DateTime CreatedDate { get; set; }
    }
}