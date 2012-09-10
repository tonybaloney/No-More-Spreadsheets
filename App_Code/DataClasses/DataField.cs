using System;

namespace com.ashaw.pricing
{
    /// <summary>
    /// DataField Attribute, for storing information on Json Serialisation
    /// </summary>
    public class DataField : Attribute
    {
        /// <summary>
        /// Gets or sets the name of the SQL field.
        /// </summary>
        /// <value>
        /// The name of the SQL field.
        /// </value>
        public string sqlFieldName { get; set; }

        /// <summary>
        /// Gets or sets the name of the json field.
        /// </summary>
        /// <value>
        /// The name of the json field.
        /// </value>
        public string jsonFieldName { get; set; }

        /// <summary>
        /// Gets or sets the type of the sort.
        /// </summary>
        /// <value>
        /// The type of the sort.
        /// </value>
        public string sortType { get; set; }

        /// <summary>
        /// Gets or sets the type of the field.
        /// </summary>
        /// <value>
        /// The type of the field.
        /// </value>
        public Type FieldType { get; set; }

        /// <summary>
        /// Gets or sets the name of the field.
        /// </summary>
        /// <value>
        /// The name of the field.
        /// </value>
        public string FieldName { get; set; } 

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="DataField" /> is readonlyfield.
        /// </summary>
        /// <value>
        ///   <c>true</c> if readonlyfield; otherwise, <c>false</c>.
        /// </value>
        public bool readonlyfield {get; set;}

        /// <summary>
        /// Initializes a new instance of the <see cref="DataField" /> class.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="name">The name.</param>
        public DataField(string name, bool readonlyfield = false) : this(name, name, "", readonlyfield) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="DataField" /> class.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="name">The name.</param>
        /// <param name="sortType">Type of the sort.</param>
        public DataField(string name, string sortType, bool readonlyfield = false) : this(name, name, sortType, readonlyfield) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="DataField" /> class.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="sqlFieldName">Name of the SQL field.</param>
        /// <param name="jsonFieldName">Name of the json field.</param>
        /// <param name="sortType">Type of the sort.</param>
        public DataField(string sqlFieldName, string jsonFieldName, string sortType, bool readonlyfield=false)
        {
            this.sortType = sortType;
            this.sqlFieldName = sqlFieldName;
            this.jsonFieldName = jsonFieldName;
            this.readonlyfield = readonlyfield;
        }
    }
}