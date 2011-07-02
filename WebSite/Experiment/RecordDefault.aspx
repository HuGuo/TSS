<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RecordDefault.aspx.cs" Inherits="Experiment_RecordDefault" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>试验台帐</title>
    <link href="../Styles/_base.css" rel="stylesheet" type="text/css" />
    <script src="../scripts/jquery-1.6.1.min.js" type="text/javascript"></script>
    <style type="text/css">
    li{ margin:3px 0px;  border:1px solid #e2e2e2; padding:5px 10px;}
    li a{ display:block;} 
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    </div>
    <table>
        <thead>
            <tr>
                <th>
                </th>
                <th>
                    名称
                </th>
            </tr>
        </thead>
        <asp:Repeater ID="rptlist" runat="server">
            <ItemTemplate>
                <tr>
                    <td>
                        <%#Container.ItemIndex+1 %>
                    </td>
                    <td>
                        <a href="Record.aspx?id=<%#Eval("Id") %>">
                            <%#Eval("Name") %></a>
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </table>
    </form>
</body>
</html>
