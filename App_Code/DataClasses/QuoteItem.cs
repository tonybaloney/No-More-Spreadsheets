using System;

namespace com.ashaw.pricing
{
    /// <summary>
    /// Summary description for QuoteItem
    /// </summary>
    public class QuoteItem : DataObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="QuoteItem" /> class.
        /// </summary>
        public QuoteItem() {}

        /// <summary>
        /// Creates this instance.
        /// </summary>
        public void Create()
        {
            DatabaseConnection db = new DatabaseConnection();
            System.Data.SqlClient.SqlCommand com = new System.Data.SqlClient.SqlCommand(this.GetInsertSQL("QuoteItems"));
            db.RunScalarCommand(com);
            db.Dispose();
        }

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        /// <value>
        /// The id.
        /// </value>
        [DataField("Id")]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the quote id.
        /// </summary>
        /// <value>
        /// The quote id.
        /// </value>
        [DataField("QuoteId")]
        public int QuoteId { get; set; }

        /// <summary>
        /// Gets or sets the pricing item id.
        /// </summary>
        /// <value>
        /// The pricing item id.
        /// </value>
        [DataField("ProductId")]
        public int ProductId { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        [DataField("Description")]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the quantity.
        /// </summary>
        /// <value>
        /// The quantity.
        /// </value>
        [DataField("Quantity")]
        public int Quantity { get; set; }

        /// <summary>
        /// Gets or sets the recurring price.
        /// </summary>
        /// <value>
        /// The recurring price.
        /// </value>
        [DataField("RecurringPrice")]
        public double RecurringPrice { get; set; }

        /// <summary>
        /// Gets or sets the setup price.
        /// </summary>
        /// <value>
        /// The setup price.
        /// </value>
        [DataField("SetupPrice")]
        public double SetupPrice { get; set; }

        /// <summary>
        /// Gets or sets the setup cost.
        /// </summary>
        /// <value>
        /// The setup cost.
        /// </value>
        [DataField("SetupCost")]
        public double SetupCost { get; set; }

        /// <summary>
        /// Gets or sets the recurring cost.
        /// </summary>
        /// <value>
        /// The recurring cost.
        /// </value>
        [DataField("RecurringCost")]
        public double RecurringCost { get; set; }

        /// <summary>
        /// Gets or sets the total setup price.
        /// </summary>
        /// <value>
        /// The total setup price.
        /// </value>
        [DataField("TotalSetupPrice")]
        public double TotalSetupPrice { get; set; }

        /// <summary>
        /// Gets or sets the total recurring price.
        /// </summary>
        /// <value>
        /// The total recurring price.
        /// </value>
        [DataField("TotalRecurringPrice")]
        public double TotalRecurringPrice { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is bundle.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is bundle; otherwise, <c>false</c>.
        /// </value>
        [DataField("IsPackage")]
        public bool IsPackage { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is part.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is part; otherwise, <c>false</c>.
        /// </value>
        [DataField("PackageId")]
        public bool PackageId { get; set; }

        /// <summary>
        /// Gets or sets the bundle id.
        /// </summary>
        /// <value>
        /// The bundle id.
        /// </value>
        [DataField("PackageConfigJson")]
        public string PackageConfigJson { get; set; }

        /// <summary>
        /// Gets or sets the name of the group.
        /// </summary>
        /// <value>
        /// The name of the group.
        /// </value>
        [DataField("GroupName")]
        public string GroupName { get; set; }

        /// <summary>
        /// Gets or sets the sub group.
        /// </summary>
        /// <value>
        /// The sub group.
        /// </value>
        [DataField("SubGroupName")]
        public string SubGroupName { get; set; }

        /// <summary>
        /// Gets or sets the index.
        /// </summary>
        /// <value>
        /// The index.
        /// </value>
        [DataField("Index")]
        public int Index { get; set; }

        /// <summary>
        /// Gets or sets the partcode.
        /// </summary>
        /// <value>
        /// The partcode.
        /// </value>
        [DataField("Partcode")]
        public string Partcode { get; set; }

        /// <summary>
        /// Gets or sets the notes.
        /// </summary>
        /// <value>
        /// The notes.
        /// </value>
        [DataField("Notes")]
        public string Notes { get; set; }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>
        /// The title.
        /// </value>
        [DataField("Title")]
        public string Title { get; set; }
    }
}