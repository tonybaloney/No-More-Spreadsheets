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
        [DataField("Id")]
        public int Id { get; set; }

        [DataField("RealName")]
        public string RealName { get; set; }
    }
}