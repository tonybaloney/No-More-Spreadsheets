using System;
using System.Collections.Generic;
using com.ashaw.pricing;

/// <summary>
/// Summary description for ProductLine
/// </summary>
public class ProductLine : DataObject
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ProductLine" /> class.
    /// </summary>
	public ProductLine()
	{
	}

    /// <summary>
    /// Creates this instance.
    /// </summary>
    public void Create()
    {
        DatabaseConnection db = new DatabaseConnection();
        System.Data.SqlClient.SqlCommand com = new System.Data.SqlClient.SqlCommand(this.GetInsertSQL("ProductLines"));
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
    /// Gets or sets the description.
    /// </summary>
    /// <value>
    /// The description.
    /// </value>
    [DataField("Description")]
    public string Description { get; set; }

    /// <summary>
    /// Gets or sets the product manager.
    /// </summary>
    /// <value>
    /// The product manager.
    /// </value>
    [DataField("ProductManager")]
    public string ProductManager { get; set; } 
}