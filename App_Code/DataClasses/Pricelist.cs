using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace com.ashaw.pricing { 
    /// <summary>
    /// Summary description for Pricelist
    /// </summary>
    public class Pricelist : DataObject
    {
        [DataField("Id")]
        public int Id { get; set; }

        [DataField("Name")]
        public string Name { get; set; }

        [DataField("Date")]
        public DateTime Date { get; set; }

        [DataField("IsDefault")]
        public bool IsDefault { get; set; }

        [DataField("IsPrivate")]
        public bool IsPrivate { get; set; }

        [DataField("Currency")]
        public string Currency { get; set; }

        [DataField("OwnerName")]
        public string OwnerName { get; set; }
    }
}