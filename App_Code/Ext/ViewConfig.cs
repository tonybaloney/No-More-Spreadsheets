using System;
using System.Runtime.Serialization;

/// <summary>
/// Summary description for ViewConfig
/// </summary>
[DataContract]
public class ViewConfig
{
    public ViewConfig(string dragGroup)
    {
        this.plugins = new DragDropPlugin();
        this.plugins.ddGroup = dragGroup;
        this.plugins.enableDrag = true;
        this.plugins.enableDrop = false;
    }

    [DataMember]
    public DragDropPlugin plugins { get; set; }
}