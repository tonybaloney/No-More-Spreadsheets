using System;
using System.Collections.Generic;
using System.Reflection;

namespace com.ashaw.pricing
{
    /// <summary>
    /// Serialising DataObject classes, static helper class
    /// </summary>
    static public class DataObjectSerialisers
    {
        /// <summary>
        /// Gets the ext json model for ExtJS 4 Javascript
        /// </summary>
        /// <param name="target">The target class.</param>
        /// <param name="modelName">Name of the model.</param>
        /// <returns>Javascript</returns>
        static public string GetExtJsonModel(object target, string modelName)
        {
            string javascript = "";
            javascript += "Ext.define('" + modelName + "', {extend: 'Ext.data.Model',fields:[";
            List<DataField> fields = DataObjectSerialisers.GetFields(target);
            // Get a list of fields in the class and turn them into a list of fields that ExtJs will understand.
            foreach (DataField df in fields)
            {
                javascript += "{ name : '" + df.jsonFieldName + "', type:'" + DataObjectSerialisers.ExtJsEquivalentType(df.FieldType) + "' } ";
                if (fields[fields.Count - 1] != df) javascript += ","; // If this is not the last, add a comma
            }
            javascript += "]});";
            return javascript;
        }

        /// <summary>
        /// Gets the json.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        static public string GetJson(List<object> data)
        {
            string javascript = "[";
            foreach (object o in data)
            {
                List<DataField> fields = DataObjectSerialisers.GetFields(o);
                javascript += "{";
                // Get a list of fields in the class and turn them into a list of fields that ExtJs will understand.
                foreach (DataField df in fields)
                {
                    object val = o.GetType().GetProperty(df.FieldName).GetValue(o);
                    javascript += df.jsonFieldName + ":'"+ (val == null?"null":val.ToString()) + "'";
                    if (fields[fields.Count - 1] != df) javascript += ","; // If this is not the last, add a comma
                }
                javascript += "}";
                if (data[data.Count - 1] != o) javascript += ",";
            }
            javascript += "]";
            return javascript;
        }

        /// <summary>
        /// Gets the ext json store for ExtJS 4 Javascript
        /// </summary>
        /// <param name="target">The target class.</param>
        /// <param name="modelName">Name of the model.</param>
        /// <param name="storeName">Name of the store.</param>
        /// <param name="ajaxUrl">The ajax URL.</param>
        /// <returns>Javascript</returns>
        static public string GetExtJsonStore(object target, string modelName, string storeName, string ajaxUrl)
        {
            return "new Ext.data.Store({model: '" + modelName + "',storeId: '" + storeName + "',proxy : { type: 'ajax', url: '" + ajaxUrl + "', reader: { type: 'json' } },autoLoad:true});";
        }

        /// <summary>
        /// What is the ExtJS equivalent type to a DotNet class type?
        /// </summary>
        /// <param name="dotNetType">Type of property.</param>
        /// <returns>ExtJS type name</returns>
        static public string ExtJsEquivalentType(Type dotNetType)
        {
            string t;
            string type_t = dotNetType.Name;
            switch (type_t)
            {
                case "String": t = "string";
                    break;
                case "Int32": t = "int";
                    break;
                case "float": t = "float";
                    break;
                case "Double": t = "float";
                    break;
                case "Boolean": t = "bool";
                    break;
                case "DateTime": t = "date";
                    break;
                default:
                    t = "string";
                    break;
            }
            return t;
        }

        /// <summary>
        /// Gets the properties in a (target) object that are of type DataField
        /// </summary>
        /// <param name="target">The target object.</param>
        /// <returns>List of DataField attributes</returns>
        static public List<DataField> GetFields(object target)
        {
            Type type = target.GetType();
            return DataObjectSerialisers.GetFields(type);
        }

        /// <summary>
        /// Gets the properties in a (target) object that are of type DataField
        /// </summary>
        /// <param name="target">The target type.</param>
        /// <returns>List of DataField attributes</returns>
        static public List<DataField> GetFields(Type target)
        {
            List<DataField> fieldList = new List<DataField>();
            // Iterate through all the fields of the class. 
            foreach (PropertyInfo pInfo in target.GetProperties())
            {
                // Iterate through all the Attributes for each method. 
                foreach (Attribute attr in Attribute.GetCustomAttributes(pInfo))
                {
                    if (attr.GetType() == typeof(DataField))
                    {
                        DataField df = (DataField)attr;
                        df.FieldName = pInfo.Name;
                        df.FieldType = pInfo.PropertyType;
                        fieldList.Add(df);
                    }
                }
            }
            return fieldList;
        }

        static public object TranslateKVPsToObjects(List<KeyValuePair<string, object>> row, Type targetType)
        {
            List<DataField> fields = DataObjectSerialisers.GetFields(targetType);
            // Get the target object constructor
            ConstructorInfo constructorInfo = targetType.GetConstructor(new Type[0]);
            object newObject = constructorInfo.Invoke(new object[0]);

            foreach (KeyValuePair<string, object> kvp in row)
            {
                foreach (DataField df in fields)
                {
                    if (kvp.Key == df.sqlFieldName)
                    {
                        try
                        {
                            newObject.GetType().GetProperty(df.FieldName).SetValue(newObject, kvp.Value);
                        }
                        catch (ArgumentException)
                        {
                            // ignore, prob translation error.
                        }
                    }
                }
            }
            return newObject;
        }

        /// <summary>
        /// Copies one data object to another
        /// </summary>
        /// <param name="from">From.</param>
        /// <param name="to">To.</param>
        static public void Copy(object from, object to)
        {
            Type t = to.GetType();
            foreach (DataField field in DataObjectSerialisers.GetFields(to))
            {
                // Find the equivalent from field and set the value.
                t.GetProperty(field.FieldName).SetValue(to, t.GetProperty(field.FieldName).GetValue(from));
            }
        }
    }
}