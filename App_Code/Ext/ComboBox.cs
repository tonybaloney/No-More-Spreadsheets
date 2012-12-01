using System;

/// <summary>
/// Summary description for ComboBox
/// </summary>
public class ComboBox :ExtComponent
{
    public ComboBox()
    {
        this.xtype = "combobox";
        this.queryMode = "local";
    }
    public string fieldLabel { get; set; }
    public SimpleStore store { get; set; } 
    public string queryMode { get; set; } 
    public string displayField { get; set; } 
    public string valueField {get; set ;} 
}
public class ComponentComboBox : ExtComponent
{
    public ComponentComboBox()
    {
        this.xtype = "combobox";
        this.queryMode = "local";
    }
    public string fieldLabel { get; set; }
    public string queryMode { get; set; } 
    public string displayField { get; set; } 
    public string valueField {get; set ;} 
    public SimplePackageComponentStore store { get; set; } 
}