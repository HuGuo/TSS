<%@ Page Language="C#" AutoEventWireup="true" EnableViewState="false" CodeFile="PreView.aspx.cs" Inherits="SystemManagement_Experiment_PreView" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>实验报告模板</title>
    <link href="../../experiment/experiment.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <h4 style="text-align:center;"><asp:Literal ID="ltTitle" runat="server"></asp:Literal></h4>    
        <asp:Literal ID="ltHTML" runat="server"></asp:Literal>
    </form>
</body>
</html>