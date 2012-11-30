using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
/// <summary>
/// Summary description for Store
/// </summary>
[DataContract]
[KnownType(typeof(SimpleStore))]
[KnownType(typeof(SimplePackageComponentStore))]
public class Store
{
    public Store()
    {
    }
	public Store(string model, Proxy proxy)
	{
        this.autoLoad = true;
        this.model = model;
        this.proxy = proxy;
	}
    [DataMember]
    public string model{ get; set;}
    [DataMember]
    public Proxy proxy { get; set; } 
    [DataMember]
    public bool autoLoad { get; set; }
}