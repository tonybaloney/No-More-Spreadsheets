using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;
using com.ashaw.pricing;
using System.Web.Script.Serialization;

[ServiceContract(Namespace = "")]
[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
public class QuoteService
{
    // To use HTTP GET, add [WebGet] attribute. (Default ResponseFormat is WebMessageFormat.Json)
    // To create an operation that returns XML,
    //     add [WebGet(ResponseFormat=WebMessageFormat.Xml)],
    //     and include the following line in the operation body:
    //         WebOperationContext.Current.OutgoingResponse.ContentType = "text/xml";
    [OperationContract]
    [WebInvoke(Method = "POST")]
    public void RenewQuote(int QuoteId)
    {
        throw new NotImplementedException();
    }

    // Add more operations here and mark them with [OperationContract]
    [OperationContract]
    [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped)]
    public void CloseQuote(int QuoteId)
    {
        Quote.Close(QuoteId);
        return;
    }

    [OperationContract]
    [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped)]
    public void AssignQuote(int QuoteId, int newUserId)
    {
        Quote q = new Quote(QuoteId);
        q.OwnerId = newUserId;
        q.Save();
        return;
    }

    [OperationContract]
    [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped)]
    public void MakeQuoteTemplate(int QuoteId)
    {
        Quote q = new Quote(QuoteId);
        q.Status = "template";
        q.Save();
        return;
    }

    [OperationContract]
    [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped)]
    public void CreateProductLine(string Name, string Description, string ProductManager)
    {
        ProductLine p = new ProductLine();
        p.Name = Name;
        p.Description = Description;
        p.ProductManager = ProductManager;
        p.Create();
    }

    [OperationContract]
    [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped)]
    public void SaveProductLine(int Id, string Name, string Description, string ProductManager)
    {
        ProductLine p = new ProductLine(Id);
        p.Name = Name;
        p.Description = Description;
        p.ProductManager = ProductManager;
        p.Save();
    }

    [OperationContract]
    [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped)]
    public void CreatePricelist(string Name, int OwnerId, string ProductLines, string Currency, string IsPublic)
    {
        Pricelist p = new Pricelist();
        p.Name = Name;
        p.IsDefault = false;
        p.IsPrivate = (IsPublic != "on");
        p.OwnerId = OwnerId;
        p.Currency = Currency;
        p.Date = DateTime.Now;
        Pricelist new_p = p.Create();
        foreach (string productLineId in ProductLines.Split(','))
        {
            if (!String.IsNullOrEmpty(productLineId))
                p.AttachProductLine(Convert.ToInt32(productLineId));
        }
    }

    [OperationContract]
    [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped)]
    public void SavePricelist(int Id, string Name, int OwnerId, string ProductLines, string Currency, string IsPublic)
    {
        Pricelist p = new Pricelist(Id);
        p.Name = Name;
        p.IsDefault = false;
        p.IsPrivate = (IsPublic != "on");
        p.OwnerId = OwnerId;
        p.Currency = Currency;
        p.Date = DateTime.Now;
        p.Save();
        p.ClearProductLines();
        foreach (string productLineId in ProductLines.Split(','))
        {
            if (!String.IsNullOrEmpty(productLineId))
                p.AttachProductLine(Convert.ToInt32(productLineId));
        }
    }

    [OperationContract]
    [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped)]
    public void DeletePricelist(int Id)
    {
        Pricelist.Delete(Id);
    }

    [OperationContract]
    [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped)]
    public void DeleteProduct(int Id)
    {
        Product.Delete(Id);
    }

    [OperationContract]
    [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped)]
    public void DeleteProductLine(int Id)
    {
        ProductLine.Delete(Id);
    }

    [OperationContract]
    [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped)]
    public void CreateProduct(string Title, string Group, string SubGroup, string Partcode, string Manufacturer, string Description, string InternalNotes, string Availability, string ProductLines)
    {
        Product p = new Product();
        p.Title = Title;
        p.Group = Group;
        p.SubGroup = SubGroup;
        p.Partcode = Partcode;
        p.Manufacturer = Manufacturer;
        p.Description = Description;
        p.InternalNotes = InternalNotes;
        p.Availability = Availability;
        Product newProduct = p.Create();
        foreach (string productLineId in ProductLines.Split(','))
        {
            if (!String.IsNullOrEmpty(productLineId))
                newProduct.AttachProductLine(Convert.ToInt32(productLineId));
        }
    }
    [OperationContract]
    [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped)]
    public void SaveProduct(int Id, string Title, string Group, string SubGroup, string Partcode, string Manufacturer, string Description, string InternalNotes, string Availability, string ProductLines)
    {
        Product p = new Product(Id);
        p.Title = Title;
        p.Group = Group;
        p.SubGroup = SubGroup;
        p.Partcode = Partcode;
        p.Manufacturer = Manufacturer;
        p.Description = Description;
        p.InternalNotes = InternalNotes;
        p.Availability = Availability;
        p.Save();
        p.ClearProductLines();
        foreach (string productLineId in ProductLines.Split(','))
        {
            if (!String.IsNullOrEmpty(productLineId))
                p.AttachProductLine(Convert.ToInt32(productLineId));
        }
    }

    [OperationContract]
    [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped)]
    public void CreatePackage(string Title, string Configurable, string InheritPrice, string InheritCost, string Manufacturer, string Partcode, string DescriptionTemplate, string Availability, string ProductLines)
    {
        Package p = new Package();
        p.Title = Title;
        p.Configurable = (Configurable == "on");
        p.InheritPrice = (InheritPrice == "on");
        p.InheritCost = (InheritCost == "on");
        p.Manufacturer = Manufacturer;
        p.Partcode = Partcode;
        p.DescriptionTemplate = DescriptionTemplate;
        p.Availability = Availability;
        Package newProduct = p.Create();
        foreach (string productLineId in ProductLines.Split(','))
        {
            if (!String.IsNullOrEmpty(productLineId))
                newProduct.AttachProductLine(Convert.ToInt32(productLineId));
        }
    }
    [OperationContract]
    [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped)]
    public void SavePackage(int Id, string Title, string Configurable, string InheritPrice, string InheritCost, string Manufacturer, string Partcode, string DescriptionTemplate, string Availability, string ProductLines)
    {
        Package p = new Package(Id);
        p.Title = Title;
        p.Configurable = (Configurable == "on");
        p.InheritPrice = (InheritPrice == "on");
        p.InheritCost = (InheritCost == "on");
        p.Manufacturer = Manufacturer;
        p.Partcode = Partcode;
        p.DescriptionTemplate = DescriptionTemplate;
        p.Availability = Availability;
        p.Save();
        p.ClearProductLines();
        foreach (string productLineId in ProductLines.Split(','))
        {
            if (!String.IsNullOrEmpty(productLineId))
                p.AttachProductLine(Convert.ToInt32(productLineId));
        }
    }

    [OperationContract]
    [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped)]
    public void SavePackageComponents(int OwningPackageId, PackageComponent[] Components)
    {
        Package package = new Package(OwningPackageId);
        package.ClearPackageComponents();
        foreach (PackageComponent c in Components)
        {
            c.PackageId = OwningPackageId;
            PackageComponent newComponent = c.Create();
            // Attach the products
            foreach (string productId in c.ProductsString.Split(','))
                if (!String.IsNullOrEmpty(productId))
                    newComponent.AttachProduct(Convert.ToInt32(productId));
        }
    }

    [OperationContract]
    [WebGet(ResponseFormat = WebMessageFormat.Json)]
    public Panel[] QuotePanelComponents(int QuoteId)
    {
        // Get the quote!
        Quote q = new Quote(QuoteId);
        List<Panel> results = new List<Panel>();
        if (q != null)
        {
            Pricelist p = new Pricelist(q.PricelistId);
            if (p != null)
            {
                foreach (int productLineId in p.ProductLines)
                {
                    ProductLine pl = new ProductLine(productLineId);
                    if (pl != null)
                    {
                        Panel newC = new Panel();
                        newC.title = pl.Name;
                        newC.items = new ExtComponent[2];

                        // Product grid
                        GridPanel grid = new GridPanel("quoteItemsDDGroup");
                        grid.store = new Store("PricedProducts", new Proxy(Proxy.ProxyType.AjaxWithJson, "Data.aspx?view=Data&model=GetPricedProductsToQuote&ProductLineId=" + productLineId + "&PricelistId=" + q.PricelistId));
                        grid.columns = new Column[1];
                        grid.columns[0] = new Column("Title", "Title");
                        grid.modelType = "PricedProducts";
                        newC.items[0] = grid;

                        // Packages grid
                        GridPanel grid2 = new GridPanel("quoteItemsDDGroup");
                        grid2.store = new Store("PricedPackages", new Proxy(Proxy.ProxyType.AjaxWithJson, "Data.aspx?view=Data&model=GetPricedPackagesToQuote&ProductLineId=" + productLineId + "&PricelistId=" + q.PricelistId));
                        grid2.columns = new Column[1];
                        grid2.columns[0] = new Column("Title", "Title");
                        grid2.modelType = "PricedPackages";
                        newC.items[1] = grid2;

                        results.Add(newC);
                    }
                }
            }
        }
        return results.ToArray();
    }
}
