<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Reminder.aspx.cs" Inherits="Appraisal.Reminder" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <table>
        <tr>
            <td>
                <asp:ListBox ID="ListBox1" runat="server" Height="480px" Width="200px" SelectionMode="Multiple" CssClass="standardManage"
                            onMouseDown="GetCurrentListValues(this);" onchange="FillListValues(this);"></asp:ListBox>
            </td>
            <td>
                <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Send" Width="100px" />
            </td>
        </tr>
    </table>
    </asp:Content>
    
