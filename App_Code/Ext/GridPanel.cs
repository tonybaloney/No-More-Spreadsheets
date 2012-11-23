using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
/// <summary>
/// Summary description for GridPanel
/// </summary>
[DataContract]
public class GridPanel : ExtComponent
{
	public GridPanel(string dragGroup)
	{
        this.xtype = "grid";
        this.title = "";
        this.viewConfig = new ViewConfig(dragGroup);
	}
    [DataMember]
    public Store store { get; set; }

    [DataMember]
    public Column [] columns { get; set; }

    [DataMember]
    public ViewConfig viewConfig { get; set; }

    [DataMember]
    public string modelType { get; set; } 
}