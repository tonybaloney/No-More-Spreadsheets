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
        [DataField("Id", "pl_id", "")]
        public int Id { get; set; }

        [DataField("Name", "pl_name", "")]
        public string Name { get; set; }

        [DataField("Date", "pl_date", "")]
        public DateTime Date { get; set; }

        [DataField("IsDefault", "pl_isdefault", "")]
        public bool IsDefault { get; set; }

        [DataField("IsPrivate", "pl_isprivate", "")]
        public bool IsPrivate { get; set; }

        [DataField("Currency", "pl_currency", "")]
        public string Currency { get; set; }

        [DataField("OwnerName", "pl_owner_name", "")]
        public string OwnerName { get; set; }
    }
}