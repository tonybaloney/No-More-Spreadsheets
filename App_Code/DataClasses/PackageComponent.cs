using System;
using System.Collections.Generic;
using com.ashaw.pricing;

/// <summary>
/// Summary description for PackageComponent
/// </summary>
public class PackageComponent : DataObject
{
	public PackageComponent() 
	{
	}

    
    /// <summary>
    /// Initializes a new instance of the <see cref="Product" /> class.
    /// </summary>
    /// <param name="Id">The id.</param>
    public PackageComponent(int Id) : base(Id, "GetPackageComponent") { }

    public void AttachProduct(int ProductId)
    {
        DatabaseConnection db = new DatabaseConnection();
        db.SProc("AttachProductToPackageComponent", new KeyValuePair<string, object>("@PackageComponentId", this.Id), new KeyValuePair<string, object>("@ProductId", ProductId));
        db.Dispose();
    }

    public void ClearProducts()
    {
        DatabaseConnection db = new DatabaseConnection();
        db.SProc("ClearPackageComponentsProductsLinks", new KeyValuePair<string, object>("@PackageComponentId", this.Id));
        db.Dispose();
    }

    /// <summary>
    /// Creates this instance.
    /// </summary>
    public PackageComponent Create()
    {
        DatabaseConnection db = new DatabaseConnection();
        System.Data.SqlClient.SqlCommand com = new System.Data.SqlClient.SqlCommand(this.GetInsertSQL("PackageComponents"));
        db.RunScalarCommand(com);
        PackageComponent p = new PackageComponent(db.GetIdentity());
        db.Dispose();
        return p;
    }

    public static void Delete(int Id)
    {
        DatabaseConnection db = new DatabaseConnection();
        db.SProc("DeletePackageComponent", new KeyValuePair<string, object>("@Id", Id));
        db.Dispose();
    }

    /// <summary>
    /// Saves this instance.
    /// </summary>
    public void Save()
    {
        DatabaseConnection db = new DatabaseConnection();
        db.RunScalarCommand(new System.Data.SqlClient.SqlCommand(this.GetSaveSQL(this.Id, "PackageComponents")));
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

    [DataField("PackageId")]
    public int PackageId { get; set; } 

    /// <summary>
    /// Gets or sets the title.
    /// </summary>
    /// <value>
    /// The title.
    /// </value>
    [DataField("Title")]
    public string Title { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether [allow multiple].
    /// </summary>
    /// <value>
    ///   <c>true</c> if [allow multiple]; otherwise, <c>false</c>.
    /// </value>
    [DataField("AllowMultiple")]
    public bool AllowMultiple { get; set; }

    /// <summary>
    /// Gets or sets the product id.
    /// </summary>
    /// <value>
    /// The product id.
    /// </value>
    [DataField("Products",true)]
    public int[] Products {
        get
        {
            DatabaseConnection db = new DatabaseConnection();
            int[] me = db.SProcToIntList("GetPackageComponentsProductsLinks", new KeyValuePair<string, object>("@Id", this.Id));
            db.Dispose();
            return me;
        }
    }

    // Only used for saving
    private string productsString;
    private bool productsStringSet = false;
    [DataField("ProductsString", true)]
    public string ProductsString
    {
        get
        {
            if (productsStringSet) return productsString;
            else return String.Join(",",this.Products);
        }
        set { productsString = value; productsStringSet = true; } 
    }

}