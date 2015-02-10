Pagination.NET
==============

This is a simple WebUserControl to give to you a pagination of your data.

How to use:

1° First copy the `web.config` content to your `web.config` file:
```xml
<configuration>
  <system.web>
    <pages controlRenderingCompatibilityVersion="4.0">
      <controls>
        <add tagPrefix="uc" src="~/uc_pagination.ascx" tagName="pagination" />
      </controls>
    </pages>
  </system.web>
</configuration>
```
2° Create a new web form called `something.aspx`

3° Add this code to him `<uc:pagination ID="uc"  runat="server"/>`

It maybe seems like this:
```asp
<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Something.aspx.cs" Inherits="Something" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Awesome html tables now!!</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <uc:pagination runat="server" ID="uc" />
        </div>
    </form>
</body>
</html>
```

4° In the code behind you need to insert the code reference at your classes and methods.
It maybe seems like this either:

```c#
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Something : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        uc.TotalPerPage = 20; //20 - 20, in other words, pages with 20 rows!
        uc.Total = 1000; // Total of your list!
    }
}
```

And save and start your ISS server! :D
I'll be improve the code, so help me fork this repo!
