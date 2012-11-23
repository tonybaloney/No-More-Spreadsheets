using System;

namespace com.ashaw.pricing
{
    /// <summary>
    /// Summary description for Product
    /// </summary>
    public class PricedPackage : Package
    {
        /// <summary>
        /// Gets or sets the pricelist id.
        /// </summary>
        /// <value>
        /// The pricelist id.
        /// </value>
        [DataField("PricelistId")]
        public int PricelistId { get; set; }

        /// <summary>
        /// Gets or sets the pricing item id.
        /// </summary>
        /// <value>
        /// The pricing item id.
        /// </value>
        [DataField("PricingItemId")]
        public int PricingItemId { get; set; }

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

    }
}