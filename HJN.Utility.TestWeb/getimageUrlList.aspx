<%@ Page Language="C#" AutoEventWireup="true" %>

<%
    string ssss = System.IO.File.ReadAllText("c:\\11.txt", Encoding.GetEncoding("gbk"));
    string[] arr = H.GetHtmlImageUrlList(ssss);
    Response.Write(arr.Length + "<br/>");
    string result = H.ArrayJoin(arr, ',');

    string result2 = H.DESEncrypt(result, "12345678");
 //   Response.Write(result2);
     result = H.DESDecrypt(result2, "12345678");
     result = "萨里看到萨芬";
     result = H.ToUnicode(result);
     result = H.FromUnicode("\u8428\u91cc\u770b\u5230\u8428\u82ac");
    Response.Write(result);
    //Response.Write(ssss);
%>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
        </div>
    </form>
</body>
</html>
