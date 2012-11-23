using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
/// <summary>
/// Summary description for Column
/// </summary>
[DataContract]
public class Column 
{
	public Column()
	{
        this.flex = 1;
        this.sortable = true;
        this.hidden = false;
	}
    public Column(string index, string title) : base ( )
    {
        this.header = title;
        this.dataIndex = index;
    }

    [DataMember]
    public string header { get; set; }
    [DataMember]
    public int flex { get; set; }
    [DataMember]
    public bool sortable { get; set; }
    [DataMember]
    public string dataIndex { get; set; }
    [DataMember]
    public bool hidden { get; set; } 
}