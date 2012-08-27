using System;

namespace com.ashaw.pricing
{
    /// <summary>
    /// Summary description for QuoteItem
    /// </summary>
    public class QuoteItem
    {
        public QuoteItem() {}

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        /// <value>
        /// The id.
        /// </value>
        [DataField("Id", "qi_id", "")]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the quote id.
        /// </summary>
        /// <value>
        /// The quote id.
        /// </value>
        [DataField("QuoteId", "quote_id", "")]
        public int QuoteId { get; set; }

        /// <summary>
        /// Gets or sets the pricing item id.
        /// </summary>
        /// <value>
        /// The pricing item id.
        /// </value>
        [DataField("ProductId", "pi_id", "")]
        public int ProductId { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        [DataField("Description", "qi_description", "")]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the quantity.
        /// </summary>
        /// <value>
        /// The quantity.
        /// </value>
        [DataField("Quantity", "qi_quantity", "")]
        public int Quantity { get; set; }

        /// <summary>
        /// Gets or sets the recurring price.
        /// </summary>
        /// <value>
        /// The recurring price.
        /// </value>
        [DataField("RecurringPrice", "qi_recurringprice", "")]
        public double RecurringPrice { get; set; }

        /// <summary>
        /// Gets or sets the setup price.
        /// </summary>
        /// <value>
        /// The setup price.
        /// </value>
        [DataField("SetupPrice", "qi_setupprice", "")]
        public double SetupPrice { get; set; }

        /// <summary>
        /// Gets or sets the setup cost.
        /// </summary>
        /// <value>
        /// The setup cost.
        /// </value>
        [DataField("SetupCost", "qi_setupcost", "")]
        public double SetupCost { get; set; }

        /// <summary>
        /// Gets or sets the recurring cost.
        /// </summary>
        /// <value>
        /// The recurring cost.
        /// </value>
        [DataField("RecurringCost", "qi_recurringcost", "")]
        public double RecurringCost { get; set; }

        /// <summary>
        /// Gets or sets the total setup price.
        /// </summary>
        /// <value>
        /// The total setup price.
        /// </value>
        [DataField("TotalSetupPrice", "qi_totalsetupprice", "")]
        public double TotalSetupPrice { get; set; }

        /// <summary>
        /// Gets or sets the total recurring price.
        /// </summary>
        /// <value>
        /// The total recurring price.
        /// </value>
        [DataField("TotalRecurringPrice", "qi_totalrecurringprice", "")]
        public double TotalRecurringPrice { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is bundle.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is bundle; otherwise, <c>false</c>.
        /// </value>
        [DataField("IsBundle", "qi_isbundle", "")]
        public bool IsBundle { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is part.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is part; otherwise, <c>false</c>.
        /// </value>
        [DataField("IsPart", "qi_ispart", "")]
        public bool IsPart { get; set; }

        /// <summary>
        /// Gets or sets the bundle id.
        /// </summary>
        /// <value>
        /// The bundle id.
        /// </value>
        [DataField("BundleId", "qi_bundleid", "")]
        public int BundleId { get; set; }

        /// <summary>
        /// Gets or sets the name of the group.
        /// </summary>
        /// <value>
        /// The name of the group.
        /// </value>
        [DataField("GroupName", "qi_groupname", "")]
        public string GroupName { get; set; }

        /// <summary>
        /// Gets or sets the sub group.
        /// </summary>
        /// <value>
        /// The sub group.
        /// </value>
        [DataField("SubGroup", "qi_subgroup", "")]
        public string SubGroup { get; set; }

        /// <summary>
        /// Gets or sets the index.
        /// </summary>
        /// <value>
        /// The index.
        /// </value>
        [DataField("Index", "qi_index", "")]
        public int Index { get; set; }

        /// <summary>
        /// Gets or sets the partcode.
        /// </summary>
        /// <value>
        /// The partcode.
        /// </value>
        [DataField("Partcode", "qi_partcode", "")]
        public string Partcode { get; set; }

        /// <summary>
        /// Gets or sets the notes.
        /// </summary>
        /// <value>
        /// The notes.
        /// </value>
        [DataField("Notes", "qi_notes", "")]
        public string Notes { get; set; }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>
        /// The title.
        /// </value>
        [DataField("Title", "qi_title", "")]
        public string Title { get; set; }
    }
}