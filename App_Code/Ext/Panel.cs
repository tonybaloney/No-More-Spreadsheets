using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

/// <summary>
/// Summary description for Panel
/// </summary>
[DataContract]
[KnownType(typeof(GridPanel))]
public class Panel : ExtComponent
{
	public Panel()
	{
        this.xtype = "panel";
	}
    [DataMember]
    public ExtComponent[] items { get; set; } 
}