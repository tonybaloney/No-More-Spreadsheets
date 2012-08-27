using System;
using System.Web;
using com.ashaw.pricing;

/// <summary>
/// Code-behind for Data collector, acts as an entry point to the DB for AJAX/JSON calls.
/// </summary>
public partial class Data : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // Which model?
        if ( !String.IsNullOrEmpty(Request.Params["model"]) ){
            string model = Request.Params["model"];
            // Which view?
            if (!String.IsNullOrEmpty(Request.Params["view"] )){
                string view = Request.Params["view"];
                switch ( view ) {
                    case "ExtModelAndStore": 
                        object mod = DataObject.GetDataClass(model);
                        string extra ="";
                        Response.Write(DataObjectSerialisers.GetExtJsonModel(mod,model));
                        if (model == "Products" || model == "QuoteItems")
                            extra = "&QuoteId="+Request.Params["QuoteId"];
                        Response.Write(DataObjectSerialisers.GetExtJsonStore(mod,model,model+"Store","Data.aspx?model="+model+"&view=Data"+extra));
                    break;
                    case "Data":
                        Response.Write(SPLookups.GetDataView(model,Request));
                    break;
                    default : 
                        Response.Write("Invalid Request");
                    break;
                }
            }
        }
    }
}