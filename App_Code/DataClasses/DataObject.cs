/// <summary>
/// Classes for creating object arrays that can be serialised to JSON and AJAX through ExtJS
/// </summary>

using System;
using System.Collections.Generic;
using System.Reflection;

namespace com.ashaw.pricing
{
    /// <summary>
    /// Summary description for DataObject
    /// </summary>
    abstract public class DataObject
    {
        public DataObject() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="DataObject" /> class.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="getSProc">The get S proc.</param>
        public DataObject(int id, string getSProc)
        {
            DatabaseConnection db = new DatabaseConnection();
            List<object> me = db.SProcToObjectList(this.GetType(),getSProc, new KeyValuePair<string,object>("@Id",id));
            db.Dispose();
            DataObjectSerialisers.Copy(me[0], this);
        }

        /// <summary>
        /// Gets the object from the class name.
        /// </summary>
        /// <param name="name">The name of the class.</param>
        /// <returns>
        /// Object
        /// </returns>
        /// <exception cref="System.TypeAccessException"></exception>
        static public object GetDataClass(string name)
        {
            switch (name)
            {
                case "Quotes":
                    return new Quote();
                case "Users":
                    return new User();
                case "Pricelists":
                    return new Pricelist();
                case "ProductsToQuote":
                    return new Product();
                case "Products":
                    return new Product();
                case "QuoteItems":
                    return new QuoteItem();
                case "ProductLines":
                    return new ProductLine();
                default:
                    throw new TypeAccessException("Type (" + name + ") not recognised.");
            }
        }

        static public string DateTimeToSQLDateTime(DateTime dt)
        {
            return dt.ToString("yyyy-MM-dd HH:mm:ss:fff");
        }

        /// <summary>
        /// Gets the save SQL.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="table">The table.</param>
        /// <returns></returns>
        public string GetSaveSQL(int key, string table)
        {
            // Build an update statement back to the database.
            List<DataField> fields = DataObjectSerialisers.GetFields(this);
            string updateSql = "UPDATE ["+table+"] SET " ;
            foreach (DataField field in fields){
                if (this.GetType().GetProperty(field.FieldName).GetValue(this) != null && field.sqlFieldName != "Id" && !field.readonlyfield)
                {
                    updateSql += " [" + field.sqlFieldName + "] = ";
                    if (field.FieldType == typeof(DateTime))
                        if (((DateTime)this.GetType().GetProperty(field.FieldName).GetValue(this)).Ticks == 0)
                            updateSql += " NULL ";
                        else
                            updateSql += " CAST ( '" + DateTimeToSQLDateTime((DateTime)this.GetType().GetProperty(field.FieldName).GetValue(this)) + "' AS datetime)";
                    else
                        updateSql += "'" + this.GetType().GetProperty(field.FieldName).GetValue(this).ToString() + "'";
                    if (fields[fields.Count - 1] != field) 
                        updateSql += ",";
                }
            }
            updateSql += " WHERE [Id] = "+key;
            return updateSql;
        }

        /// <summary>
        /// Gets the save SQL.
        /// </summary>
        /// <param name="table">The table.</param>
        /// <returns></returns>
        public string GetInsertSQL(string table)
        {
            // Build an update statement back to the database.
            List<DataField> fields = DataObjectSerialisers.GetFields(this);
            string updateSql = "INSERT INTO [" + table + "]  (";
            string fieldsSql = "";
            string valuesSql = "";
            foreach (DataField field in fields)
            {
                if (this.GetType().GetProperty(field.FieldName).GetValue(this) != null && field.sqlFieldName != "Id" && !field.readonlyfield)
                {
                    fieldsSql += " [" + field.sqlFieldName + "] ";
                    if (field.FieldType == typeof(DateTime))
                        if (((DateTime)this.GetType().GetProperty(field.FieldName).GetValue(this)).Ticks == 0)
                            valuesSql += " NULL ";
                        else
                            valuesSql += " CAST ( '" + DateTimeToSQLDateTime((DateTime)this.GetType().GetProperty(field.FieldName).GetValue(this)) + "' AS datetime)";
                    else
                        valuesSql += "'" + this.GetType().GetProperty(field.FieldName).GetValue(this).ToString() + "'";
                    if (fields[fields.Count - 1] != field)
                    {
                        fieldsSql += ",";
                        valuesSql += ",";
                    }
                }
            }
            updateSql += fieldsSql + ") VALUES ( " + valuesSql + " ) ";
            return updateSql;
        }
    }
}