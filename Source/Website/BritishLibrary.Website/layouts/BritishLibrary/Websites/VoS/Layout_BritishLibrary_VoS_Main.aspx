<%@ Page language="c#" Codepage="65001" AutoEventWireup="true" %>
<%@ OutputCache Location="None" VaryByParam="none" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html lang="en" xml:lang="en" xmlns="http://www.w3.org/1999/xhtml">
  <head>
    <title><sc:Text ID="Text1" runat="server" Field="Term" /></title>
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <link href="/default.css" rel="stylesheet" />
    <sc:VisitorIdentification runat="server" />
      
  </head>
  <body>
  <form method="post" runat="server" id="mainform">
      <div>Top navigation</div>
      <sc:placeholder ID="content" key="content" runat="server" /> 
  </form>
  </body>
</html>
