using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;
using com.ashaw.pricing;

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
    [WebInvoke(Method="POST")]
    public void CloseQuote(int QuoteId)
    {
        Quote.Close(QuoteId);
        return;
    }

    [OperationContract]
    [WebInvoke(Method = "POST")]
    public void AssignQuote(int QuoteId, int newUserId)
    {
        Quote q = new Quote(QuoteId);
        q.OwnerId = newUserId;
        q.Save();
        return;
    }

    [OperationContract]
    [WebInvoke(Method = "POST")]
    public void MakeQuoteTemplate(int QuoteId)
    {
        Quote q = new Quote(QuoteId);
        q.Status = "template";
        q.Save();
        return;
    }

    [OperationContract]
    [WebInvoke(Method = "POST")]
    public void CreateProductLine(string Name, string Description, string ProductManager)
    {
        ProductLine p = new ProductLine();
        p.Name = Name;
        p.Description = Description;
        p.ProductManager = ProductManager;
        p.Create();
    }
}
