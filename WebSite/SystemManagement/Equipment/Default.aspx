<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="SystemManagement_Equipment_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:ListView ID="ListView1" runat="server" DataSourceID="ObjectDataSource1" DataKeyNames="Id">
            <LayoutTemplate>
                <ul>
                    <asp:PlaceHolder ID="itemPlaceholder" runat="server"></asp:PlaceHolder>
                </ul>
            </LayoutTemplate>
            <ItemTemplate>
                <li>
                    <asp:LinkButton ID="edit" runat="server" CommandName="Edit" Text='<%# Eval("Name") %>'></asp:LinkButton>
                </li>
                <li>
                    <%# TSS.BLL.Specialty.Get(Eval("SpecialtyId").ToString()).Name %>
                </li>
            </ItemTemplate>
            <EditItemTemplate>
                <li>
                    <asp:TextBox ID="txt" runat="server" Text='<%# Bind("Name") %>'></asp:TextBox>
                    <asp:Button ID="update" runat="server" CommandName="Update" Text="保存" />
                </li>
                <li>
                <asp:DropDownList ID="ddl" runat="server" DataSourceId="ddlo" DataTextField="Name" DataValueField="Id" SelectedValue='<%# Bind("SpecialtyId") %>'></asp:DropDownList>
               
                </li>
            </EditItemTemplate>
        </asp:ListView>
        <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetAll"
            TypeName="TSS.BLL.Equipment" DataObjectTypeName="TSS.Models.Equipment" UpdateMethod="Update">
        </asp:ObjectDataSource>
        <asp:ObjectDataSource ID="ddlo" runat="server" SelectMethod="GetAll" TypeName="TSS.BLL.Specialty">
        </asp:ObjectDataSource>
    </div>
    </form>
</body>
</html>
