using System;
using com.ashaw.pricing;

/// <summary>
/// Summary description for SimpleStore
/// </summary>
public class SimpleStore : Store
{
    public string[] fields { get; set; }
    public object[] data { get; set; }
}

public class SimplePackageComponentStore : Store
{
    public string[] fields { get; set; }
    public Product[] data { get; set; }
}