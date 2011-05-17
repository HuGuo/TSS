<%@ Page Language="C#" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">

</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
            DataSourceID="LinqDataSource1">
            <Columns>
                <asp:BoundField DataField="NAME" HeaderText="NAME" ReadOnly="True" 
                    SortExpression="NAME" />
                <asp:BoundField DataField="SP_CODE" HeaderText="SP_CODE" ReadOnly="True" 
                    SortExpression="SP_CODE" />
            </Columns>
        </asp:GridView>
        <asp:LinqDataSource ID="LinqDataSource1" runat="server" 
            ContextTypeName="TSSModel.TSSEntities" EntityTypeName="" 
            Select="new (NAME, SP_CODE)" TableName="EQUIPMENT">
        </asp:LinqDataSource>
    
    </div>
    </form>
</body>
</html>
