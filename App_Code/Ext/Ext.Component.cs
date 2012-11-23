using System;
using System.Runtime.Serialization;
/// <summary>
/// Summary description for Ext
/// </summary>
[DataContract]
public class ExtComponent
{
    [DataMember]
    public string xtype { get; set; }
    [DataMember]
    public string title { get; set; }
}