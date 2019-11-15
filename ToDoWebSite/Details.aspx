<%@ Page Title="Details" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="Details.aspx.cs" Inherits="Details" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <br />
        <br />
        Description:
        <asp:TextBox ID="TextBox_Decription" runat="server" ></asp:TextBox>

        <asp:RequiredFieldValidator
            ControlToValidate="TextBox_Decription"
            Display="Static"
            ErrorMessage="Description is required."
            CssClass="required"
            ID="TextBox_Validator"
            RunAt="Server" />
        <br />
    </div>
    <br />
    <asp:CheckBox ID="CheckBox_WasDone" runat="server" Text="Was Done" />
    <br />
    <br />
    Due Date:
    <asp:Calendar ID="Calendar_DueDate" runat="server"></asp:Calendar>
    <br />
    <br />
    <asp:Button ID="Button_Save" runat="server" OnClick="Button_Save_Click" Text="Save" />
    <br />
</asp:Content>    
