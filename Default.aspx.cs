using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.cmdSignOut.ServerClick += new System.EventHandler(this.cmdSignOut_ServerClick);
        /*
	        case 'myquotes': include ( 'pricing/myquotes.php'); break;
	        case 'createquote': include('pricing/createquote.php'); break;
	        case 'create': include('pricing/createquote.php'); break;
	        case 'pricelists': include('pricing/pricelists.php'); break;
	        case 'pricelist': include('pricing/pricelist.php'); break;
	        case 'edit': include('pricing/editquote.php'); break;
	        case 'print': include('pricing/printquote.php'); break;
	        case 'printso': include('pricing/printso.php'); break;
	        case 'save' : include('pricing/savequote.php'); break;
	        case 'printpricelist' : include('pricing/printpricelist.php'); break;
	        case 'components' : include ('pricing/compatibility.php'); break;
	        case 'import' : include ('pricing/import.php'); break;
	        case 'upload' : include ('pricing/upload.php'); break;
	        case 'compare' : include('pricing/compare.php'); break;
	        case 'close' : include('pricing/closequote.php'); break;
         */
    }
    private void cmdSignOut_ServerClick(object sender, System.EventArgs e)
    {
        FormsAuthentication.SignOut();
        Response.Redirect("Logon.aspx", true);
    }
}