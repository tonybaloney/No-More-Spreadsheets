<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Logon.aspx.cs" Inherits="Logon" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <h3>Logon Page</h3>
        <table>
           <tr>
              <td>Email:</td>
              <td><input id="txtUserName" type="text" runat="server"/></td>
              <td><ASP:RequiredFieldValidator ControlToValidate="txtUserName"
                   Display="Static" ErrorMessage="*" runat="server" 
                   ID="vUserName" /></td>
           </tr>
           <tr>
              <td>Password:</td>
              <td><input id="txtUserPass" type="password" runat="server"/></td>
              <td><ASP:RequiredFieldValidator ControlToValidate="txtUserPass"
                  Display="Static" ErrorMessage="*" runat="server" 
                  ID="vUserPass" />
              </td>
           </tr>
           <tr>
              <td>Persistent Cookie:</td>
              <td><ASP:CheckBox id="chkPersistCookie" runat="server" autopostback="false" /></td>
              <td></td>
           </tr>
        </table>
        <input type="submit" value="Logon" runat="server" id="cmdLogin"/><p></p>
        <asp:Label id="lblMsg" ForeColor="red" runat="server" />					
    </form>
</body>
</html>
