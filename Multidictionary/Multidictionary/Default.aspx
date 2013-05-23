<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Multidictionary.Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <asp:PlaceHolder runat="server" id="hdr"></asp:PlaceHolder>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <h3>
            Enter URL and get contents of the page</h3>
        <%--<asp:textbox id="TextBoxURL" runat="server" height="20px" width="250px" text="http://www.vandale.nl/opzoeken?pattern=kat&lang=nn">--%>
        <asp:textbox id="TextBoxURL" runat="server" height="20px" width="250px" text="kat">
        </asp:textbox>
        <asp:dropdownlist runat="server" datasourceid="languagesDataSource"
            datatextfield="name" datavaluefield="id" ID="LanguageDropDown"></asp:dropdownlist>
        <asp:button id="Button1" runat="server" text="Get Contents" onclick="Button1_Click" />
        <br />
        <asp:literal runat="server" id="resultpanel"></asp:literal>
    </div>
    <asp:objectdatasource id="languagesDataSource" runat="server" 
        OnObjectCreating="languagesDataSource_ObjectCreating" selectmethod="getLanguages" typename="Multidictionary.Dictionaries.VanDale">
    </asp:objectdatasource>
    </form>
</body>
</html>
