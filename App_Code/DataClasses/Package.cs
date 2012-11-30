using System;
using System.Collections.Generic;
using com.ashaw.pricing;

/// <summary>
/// Summary description for Package
/// </summary>
public class Package : DataObject
{
	public Package()
	{
	}

    /// <summary>
    /// Initializes a new instance of the <see cref="Product" /> class.
    /// </summary>
    /// <param name="Id">The id.</param>
    public Package(int Id) : base(Id, "GetPackage") { }

    /// <summary>
    /// Attaches the product line to this pricelist
    /// </summary>
    /// <param name="ProductLineId">The product line id.</param>
    public void AttachProductLine(int ProductLineId)
    {
        DatabaseConnection db = new DatabaseConnection();
        db.SProc("AttachProductLineToPackage", new KeyValuePair<string, object>("@PackageId", this.Id), new KeyValuePair<string, object>("@ProductLineId", ProductLineId));
        db.Dispose();
    }

    /// <summary>
    /// Deletes all product line links for this product
    /// </summary>
    public void ClearProductLines()
    {
        DatabaseConnection db = new DatabaseConnection();
        db.SProc("ClearPackagesProductLinesLinks", new KeyValuePair<string, object>("@PackageId", this.Id));
        db.Dispose();
    }

    public void ClearPackageComponents()
    {
        DatabaseConnection db = new DatabaseConnection();
        db.SProc("ClearPackagePackageComponents", new KeyValuePair<string, object>("@PackageId", this.Id));
        db.Dispose();
    }

    /// <summary>
    /// Creates this instance.
    /// </summary>
    public Package Create()
    {
        DatabaseConnection db = new DatabaseConnection();
        System.Data.SqlClient.SqlCommand com = new System.Data.SqlClient.SqlCommand(this.GetInsertSQL("Packages"));
        db.RunScalarCommand(com);
        Package p = new Package(db.GetIdentity());
        db.Dispose();
        return p;
    }

    /// <summary>
    /// Deletes the specified package id.
    /// </summary>
    /// <param name="ProductId">The product id.</param>
    public static void Delete(int ProductId)
    {
        DatabaseConnection db = new DatabaseConnection();
        db.SProc("DeletePackage", new KeyValuePair<string, object>("@Id", ProductId));
        db.Dispose();
    }

    /// <summary>
    /// Saves this instance.
    /// </summary>
    public void Save()
    {
        DatabaseConnection db = new DatabaseConnection();
        db.RunScalarCommand(new System.Data.SqlClient.SqlCommand(this.GetSaveSQL(this.Id, "Packages")));
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
    /// Gets or sets a value indicating whether this <see cref="Package" /> is configurable.
    /// </summary>
    /// <value>
    ///   <c>true</c> if configurable; otherwise, <c>false</c>.
    /// </value>
    [DataField("Configurable")]
    public bool Configurable { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether [inherit price].
    /// </summary>
    /// <value>
    ///   <c>true</c> if [inherit price]; otherwise, <c>false</c>.
    /// </value>
    [DataField("InheritPrice")]
    public bool InheritPrice { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether [inherit cost].
    /// </summary>
    /// <value>
    ///   <c>true</c> if [inherit cost]; otherwise, <c>false</c>.
    /// </value>
    [DataField("InheritCost")]
    public bool InheritCost { get; set; }

    /// <summary>
    /// Gets or sets the description template.
    /// </summary>
    /// <value>
    /// The description template.
    /// </value>
    [DataField("DescriptionTemplate")]
    public string DescriptionTemplate { get; set; }

    /// <summary>
    /// Gets or sets the manufacturer.
    /// </summary>
    /// <value>
    /// The manufacturer.
    /// </value>
    [DataField("Manufacturer")]
    public string Manufacturer { get; set; }

    /// <summary>
    /// Gets or sets the partcode.
    /// </summary>
    /// <value>
    /// The partcode.
    /// </value>
    [DataField("Partcode")]
    public string Partcode { get; set; }

    [DataField("Availability")]
    public string Availability { get; set; }

    /// <summary>
    /// Gets or sets the product lines.
    /// </summary>
    /// <value>
    /// The product lines.
    /// </value>
    [DataField("ProductLines", true)]
    public int[] ProductLines
    {
        get
        {
            DatabaseConnection db = new DatabaseConnection();
            int[] me = db.SProcToIntList("GetPackageProductLinesLinks", new KeyValuePair<string, object>("@Id", this.Id));
            db.Dispose();
            return me;
        }
    }

    [DataField("Components", true)]
    public PackageComponent[] Components
    {
        get
        {
            DatabaseConnection db = new DatabaseConnection();
            Type t = typeof(PackageComponent);
            List<PackageComponent> components = new List<PackageComponent>();
            List<DataObject> results = db.SProcToObjectList(t, "GetPackageComponentsInPackage", new KeyValuePair<string, object>("@PackageId", this.Id));
            foreach (DataObject res in results)
                components.Add((PackageComponent)res);
            return components.ToArray();
        }
    }
}