using System;
using System.Collections.Generic;
using System.Web;

namespace com.ashaw.pricing
{
    /// <summary>
    /// Summary description for SPLookups
    /// </summary>
    static public class SPLookups
    {
        static public string GetDataView(string model,HttpRequest Request)
        {
            DatabaseConnection db = new DatabaseConnection();
            List<object> results = new List<object>();
            Type type;
            switch (model)
            {
                case "Pricelists":
                    type = typeof(Pricelist);
                    results = db.SProcToObjectList(type,"GetPricelistsForUser",new KeyValuePair<string,object>("@UserId",1));
                    break;
                case "Quotes":
                    type = typeof(Quote);
                    results = db.SProcToObjectList(type, "GetAllQuotesForUser", new KeyValuePair<string, object>("@UserId", 1));
                    break;
                case "Users":
                    type = typeof(User);
                    results = db.SProcToObjectList(type, "GetAllUsers");
                    break;
                case "QuoteItems":
                    type = typeof(QuoteItem);
                    int quoteid = Convert.ToInt32(Request.Params["QuoteId"]);
                    results = db.SProcToObjectList(type, "GetQuoteItems", new KeyValuePair<string, object>("@QuoteId", quoteid));
                    break;
                case "ProductsToQuote":
                    type = typeof(PricedProduct);
                    results = db.SProcToObjectList(type, "GetProductsAvailableToQuote", new KeyValuePair<string, object>("@QuoteId",Convert.ToInt32(Request.Params["QuoteId"])));
                    break;
                case "Products":
                    type = typeof(Product);
                    results = db.SProcToObjectList(type, "GetAllProducts");
                    break;
                case "ProductLines":
                    type = typeof(ProductLine);
                    results = db.SProcToObjectList(type, "GetAllProductLines");
                    break;
                case "Packages":
                    type = typeof(Package);
                    results = db.SProcToObjectList(type, "GetPackages");
                    break;
                case "PackagesInProductLine":
                    type = typeof(Package);
                    results = db.SProcToObjectList(type, "GetPackagesInProductLine", new KeyValuePair<string, object>("@ProductLineId", Convert.ToInt32(Request.Params["ProductLineId"])));
                    break;
                case "PackageComponents":
                    type = typeof(PackageComponent);
                    if (String.IsNullOrEmpty(Request.Params["PackageId"]))
                        results = new List<object>();
                    else
                        results = db.SProcToObjectList(type, "GetPackageComponentsInPackage", new KeyValuePair<string, object>("@PackageId", Convert.ToInt32(Request.Params["PackageId"])));
                    break;
            }
            db.Dispose();
            return DataObjectSerialisers.GetJson(results);
        }
    }
}