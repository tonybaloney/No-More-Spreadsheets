using System;
using com.ashaw.pricing;
using System.Runtime.Serialization;
using System.Collections.Generic;

/// <summary>
/// Summary description for SimpleStore
/// </summary>
[KnownType(typeof(SimplePackageComponentStore))]
public class SimpleStore
{
    public string[] fields { get; set; }
    public object[] data { get; set; }
}

public class SimplePackageComponentStore 
{
    public string[] fields { 
        get {
            List<string> results = new List<string>();
            List<DataField> fields = DataObjectSerialisers.GetFields(typeof(PricedProduct));
            foreach (DataField field in fields)
            {
                results.Add(field.jsonFieldName);
            }
            return results.ToArray();
        }
    }
    public PricedProduct[] data { get; set; }
}