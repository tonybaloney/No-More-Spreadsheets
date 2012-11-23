using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
/// <summary>
/// Summary description for Proxy
/// </summary>
[DataContract]
public class Proxy
{
    public enum ProxyType { AjaxWithJson }
    public Proxy(ProxyType type, string url)
    {
        switch (type)
        {
            case ProxyType.AjaxWithJson:
                {
                    this.type = "ajax";
                    this.url = url;
                    this.reader = new Reader();
                    this.reader.root = "items";
                    this.reader.type = "json";
                } break;
        }
    }
    [DataMember]
    public string type { get; set; }
    [DataMember]
    public string url { get; set; }
    [DataMember]
    public Reader reader { get; set; }
}