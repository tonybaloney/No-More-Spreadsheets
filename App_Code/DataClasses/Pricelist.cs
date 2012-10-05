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
        public Pricelist Create()
        {
            DatabaseConnection db = new DatabaseConnection();
            System.Data.SqlClient.SqlCommand com = new System.Data.SqlClient.SqlCommand(this.GetInsertSQL("Pricelists"));
            db.RunScalarCommand(com);
            Pricelist p = new Pricelist(db.GetIdentity());
            db.Dispose();
            return p;
        }

        /// <summary>
        /// Deletes the specified pricelist id.
        /// </summary>
        /// <param name="PricelistId">The pricelist id.</param>
        public static void Delete(int PricelistId)
        {
            DatabaseConnection db = new DatabaseConnection();
            db.SProc("DeletePricelist", new KeyValuePair<string, object>("@Id", PricelistId));
            db.Dispose();
        }

        /// <summary>
        /// Deletes this instance.
        /// </summary>
        public void Delete()
        {
            Pricelist.Delete(this.Id);
        }

        /// <summary>
        /// Attaches the product line to the pricelist
        /// </summary>
        /// <param name="PricelistId">The pricelist id.</param>
        /// <param name="ProductLineId">The product line id.</param>
        static public void AttachProductLine(int PricelistId, int ProductLineId)
        {
            DatabaseConnection db = new DatabaseConnection();
            db.SProc("AttachProductLineToPricelist", new KeyValuePair<string, object>("@PricelistId", PricelistId), new KeyValuePair<string, object>("@ProductLineId", ProductLineId));
            db.Dispose();
        }

        /// <summary>
        /// Attaches the product line to this pricelist
        /// </summary>
        /// <param name="ProductLineId">The product line id.</param>
        public void AttachProductLine(int ProductLineId)
        {
            Pricelist.AttachProductLine(this.Id, ProductLineId);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Pricelist" /> class.
        /// </summary>
        public Pricelist()
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Quote" /> class.
        /// </summary>
        /// <param name="id">The id.</param>
        public Pricelist(int id) : base(id,"GetPricelist")
        {
            
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