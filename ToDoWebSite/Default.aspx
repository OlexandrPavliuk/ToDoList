<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" EnableEventValidation = "false" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <br />
        <br />
        <asp:GridView ID="GridViewToDo" runat="server" CellPadding="4" ForeColor="#333333"
                GridLines="Both" AllowPaging="True" AllowSorting="True" OnRowCreated="GridViewToDo_RowCreated" 
                PageSize="5" OnPageIndexChanging="GridViewToDo_PageIndexChanging" OnRowDataBound="GridViewToDo_RowDataBound" 
                OnSelectedIndexChanged="GridViewToDo_SelectedIndexChanged" DataKeyNames="Id" 
                OnSorting="GridViewToDo_Sorting">
            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            <EditRowStyle BackColor="#999999" />
            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
            <SortedAscendingCellStyle BackColor="#E9E7E2" />
            <SortedAscendingHeaderStyle BackColor="#506C8C" />
            <SortedDescendingCellStyle BackColor="#FFFDF8" />
            <SortedDescendingHeaderStyle BackColor="#6F8DAE" />            
        </asp:GridView>
        <asp:LinkButton ID="lnkDummy" runat="server"></asp:LinkButton>
        <br />
        <br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="Button_CreateTodo" runat="server" OnClick="Button_CreateTodo_Click" Text="Create TODO" />
        <br />
        <br />
    </div>
</asp:Content>