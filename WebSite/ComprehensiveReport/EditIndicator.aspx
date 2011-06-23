<%@ Page Title="" Language="C#" MasterPageFile="~/Default.master" AutoEventWireup="true" CodeFile="EditIndicator.aspx.cs" Inherits="ComprehensiveReport_EditIndicator" %>
<%@ Register src="../UserControl/SpecialtyControl.ascx" tagname="SpecialtyControl" tagprefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" Runat="Server">
   <script type="text/javascript">
       $().ready(function () {
           $("#<%=Page.Form.UniqueID %>").validate();
       });
    </script>
    <div>
        <fieldset>
            <legend>添加</legend>
            <p>
                <label>
                    指标名称</label>
                <asp:TextBox runat="server" validate="{required:true}" ID="tbName"></asp:TextBox>
            </p>
            <p>
                <label>
                    指标单位</label>
                <asp:TextBox runat="server" validate="{required:true}"  ID="tbUnit"></asp:TextBox>
            </p>
            <p>
                <label>
                    专业</label>
                <uc2:SpecialtyControl ID="SpecialtyControl1" runat="server" />
            </p>
            <p>
                <asp:Button ID="btnEdit" runat="server" Text="编辑" OnClick="btnEdit_Click" />
                <input type="button" value="取消" onclick="window.location.href='Indicator.aspx?specialtyId=<%= Request.QueryString["specialtyId"] %>'" />
            </p>
        </fieldset>
    </div>
</asp:Content>

