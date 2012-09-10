using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace com.ashaw.pricing { 
    /// <summary>
    /// Summary description for Pricelist
    /// </summary>
    public class Pricelist : DataObject
    {
        /// <summary>
        /// Creates this instance.
        /// </summary>
        public void Create()
        {
            DatabaseConnection db = new DatabaseConnection();
            System.Data.SqlClient.SqlCommand com = new System.Data.SqlClient.SqlCommand(this.GetInsertSQL("Pricelists"));
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
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        [DataField("Name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the date.
        /// </summary>
        /// <value>
        /// The date.
        /// </value>
        [DataField("Date")]
        public DateTime Date { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is default.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is default; otherwise, <c>false</c>.
        /// </value>
        [DataField("IsDefault")]
        public bool IsDefault { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is private.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is private; otherwise, <c>false</c>.
        /// </value>
        [DataField("IsPrivate")]
        public bool IsPrivate { get; set; }

        /// <summary>
        /// Gets or sets the currency.
        /// </summary>
        /// <value>
        /// The currency.
        /// </value>
        [DataField("Currency")]
        public string Currency { get; set; }

        /// <summary>
        /// Gets or sets the name of the owner.
        /// </summary>
        /// <value>
        /// The name of the owner.
        /// </value>
        [DataField("OwnerId")]
        public int OwnerId { get; set; }

        /// <summary>
        /// Gets or sets the name of the owner.
        /// </summary>
        /// <value>
        /// The name of the owner.
        /// </value>
        [DataField("OwnerName", true)]
        public string OwnerName { get; set; }
    }
}