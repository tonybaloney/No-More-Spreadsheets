using System;
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