﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="sessionid.aspx.cs" Inherits="YueWen.Utility.TestWeb.sessionid" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <%=HJN.Utility.AllInOne.H.   GetPagedNumStr(50,8,null) %>
        <%="<br/>" %>
        <%=Request["q"] %>
    </div>
    </form>
</body>
</html>
