using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace com.ashaw.pricing
{
    /// <summary>
    /// Summary description for Product
    /// </summary>
    public class Product : DataObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Product" /> class.
        /// </summary>
        public Product() { }

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        /// <value>
        /// The id.
        /// </value>
        [DataField("Id")]
        public int Id { get; set; }

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

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>
        /// The title.
        /// </value>
        [DataField("Title")]
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        [DataField("Description")]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the internal notes.
        /// </summary>
        /// <value>
        /// The internal notes.
        /// </value>
        [DataField("InternalNotes")]
        public string InternalNotes { get; set; }

        /// <summary>
        /// Gets or sets the partcode.
        /// </summary>
        /// <value>
        /// The partcode.
        /// </value>
        [DataField("Partcode")]
        public string Partcode { get; set; }

        /// <summary>
        /// Gets or sets the group.
        /// </summary>
        /// <value>
        /// The group.
        /// </value>
        [DataField("Group")]
        public string Group { get; set; }

        /// <summary>
        /// Gets or sets the sub group.
        /// </summary>
        /// <value>
        /// The sub group.
        /// </value>
        [DataField("SubGroup")]
        public string SubGroup { get; set; }

        /// <summary>
        /// Gets or sets the availability.
        /// </summary>
        /// <value>
        /// The availability.
        /// </value>
        [DataField("Availability")]
        public string Availability { get; set; }

        /// <summary>
        /// Gets or sets the size U.
        /// </summary>
        /// <value>
        /// The size U.
        /// </value>
        [DataField("SizeU")]
        public int SizeU { get; set; }

        /// <summary>
        /// Gets or sets the power.
        /// </summary>
        /// <value>
        /// The power.
        /// </value>
        [DataField("Power")]
        public double Power { get; set; }

        /// <summary>
        /// Gets or sets the manufacturer.
        /// </summary>
        /// <value>
        /// The manufacturer.
        /// </value>
        [DataField("Manufacturer")]
        public string Manufacturer { get; set; }
    }
}