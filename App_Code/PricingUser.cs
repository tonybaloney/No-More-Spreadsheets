using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
namespace com.ashaw.pricing
{
    /// <summary>
    /// Summary description for PricingUser
    /// </summary>
    public class PricingUser
    {
        protected int id;
	    protected string realname;
	    protected string email;
	    protected string quotepermissions;
	    protected string team;

        public PricingUser(int id)
        {
            SqlConnection conn;
            SqlCommand cmd;
            conn = new SqlConnection(ConfigurationManager.ConnectionStrings["PricingConnectionString"].ConnectionString);
            conn.Open();

            // Create SqlCommand to select pwd field from users table given supplied userName.
            cmd = new SqlCommand("Select UserPassword from Users where Id=@id", conn);
            cmd.Parameters.Add("@id", SqlDbType.VarChar);
            cmd.Parameters["@id"].Value = id;

            // Cleanup command and connection objects.
            cmd.Dispose();
            conn.Dispose();
	    }
	    public int GetId() {
		    return this.id;
	    }
	    public bool HasCustomItemPermissions () {
		    // Is this user a sol arc? Can they create custom server specs?
		    return (this.quotepermissions == "full");
	    }
	    public bool IsPricingAdministrator () {
		    // Add logic to show if this user can edit pricelists.
		    return (this.quotepermissions == "full");
	    }
	    public string GetHeader () {
		    return "<div class=\"x-panel-header x-panel-header-logo\"><a href=\"/Default.aspx\"><img src=\"/pricing/images/home.gif\" alt=\"Quotes\" title=\"Quotes\"></a> <a href=\"/Pricelists.aspx\"><img src=\"/pricing/images/pricelists.gif\" alt=\"Pricelists\" title=\"Pricelists\"></a> Logged in as "+this.realname+" - <a href=\"/Logout.aspx\">Logout</a></div>";
	    }
    }
}