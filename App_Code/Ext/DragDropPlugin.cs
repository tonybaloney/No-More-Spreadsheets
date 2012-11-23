using System;
using System.Runtime.Serialization;

/// <summary>
/// Summary description for DragDropPlugin
/// </summary>
[DataContract]
public class DragDropPlugin
{
    public DragDropPlugin()
    {
        this.ptype = "gridviewdragdrop";
        this.enableDrag = true;
        this.enableDrop = true;
    }

    [DataMember]
    public string ptype { get; set; }

    [DataMember]
    public string ddGroup { get; set; }

    [DataMember]
    public bool enableDrag { get; set; }

    [DataMember]
    public bool enableDrop { get; set; } 
}