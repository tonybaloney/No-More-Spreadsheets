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
        /// Initializes a new instance of the <see cref="Product" /> class.
        /// </summary>
        /// <param name="Id">The id.</param>
        public Product(int Id) : base(Id, "GetProduct") { }

        /// <summary>
        /// Creates this instance.
        /// </summary>
        public Product Create()
        {
            DatabaseConnection db = new DatabaseConnection();
            System.Data.SqlClient.SqlCommand com = new System.Data.SqlClient.SqlCommand(this.GetInsertSQL("Products"));
            db.RunScalarCommand(com);
            Product p = new Product(db.GetIdentity());
            db.Dispose();
            return p;
        }

        /// <summary>
        /// Deletes the specified product id.
        /// </summary>
        /// <param name="ProductId">The product id.</param>
        public static void Delete(int ProductId)
        {
            DatabaseConnection db = new DatabaseConnection();
            db.SProc("DeleteProduct", new KeyValuePair<string, object>("@Id", ProductId));
            db.Dispose();
        }

        /// <summary>
        /// Deletes this instance.
        /// </summary>
        public void Delete()
        {
            Product.Delete(this.Id);
        }

        /// <summary>
        /// Attaches the product line to this pricelist
        /// </summary>
        /// <param name="ProductLineId">The product line id.</param>
        public void AttachProductLine(int ProductLineId)
        {
            DatabaseConnection db = new DatabaseConnection();
            db.SProc("AttachProductLineToProduct", new KeyValuePair<string, object>("@ProductId", this.Id), new KeyValuePair<string, object>("@ProductLineId", ProductLineId));
            db.Dispose();
        }

        /// <summary>
        /// Deletes all product line links for this product
        /// </summary>
        public void ClearProductLines()
        {
            DatabaseConnection db = new DatabaseConnection();
            db.SProc("ClearProductsProductLinesLinks", new KeyValuePair<string, object>("@ProductId", this.Id));
            db.Dispose();
        }

        /// <summary>
        /// Saves this instance.
        /// </summary>
        public void Save()
        {
            DatabaseConnection db = new DatabaseConnection();
            db.RunScalarCommand(new System.Data.SqlClient.SqlCommand(this.GetSaveSQL(this.Id, "Products")));
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
        /// Gets or sets the manufacturer.
        /// </summary>
        /// <value>
        /// The manufacturer.
        /// </value>
        [DataField("Manufacturer")]
        public string Manufacturer { get; set; }

        /// <summary>
        /// Gets or sets the product lines.
        /// </summary>
        /// <value>
        /// The product lines.
        /// </value>
        [DataField("ProductLines", true)]
        public int[] ProductLines { 
            get {
                DatabaseConnection db = new DatabaseConnection();
                int[] me = db.SProcToIntList("GetProductProductLinesLinks", new KeyValuePair<string, object>("@Id", this.Id));
                db.Dispose();
                return me;
            }
        } 
    }
}