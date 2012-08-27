using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace com.ashaw.pricing { 
    /// <summary>
    /// Summary description for User
    /// </summary>
    public class User : DataObject
    {
        [DataField("Id", "objectid", "")]
        public int Id { get; set; }

        [DataField("RealName", "userrealname", "")]
        public string RealName { get; set; }
    }
}