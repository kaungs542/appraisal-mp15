﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ManageFunction.aspx.cs" Inherits="Appraisal.ManageFunction" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:MultiView ID="MultiView1" runat="server">
        <asp:View ID="View1" runat="server">
            <div style="text-align: left">
                &nbsp; &nbsp;&nbsp;&nbsp;<asp:Label ID="Label1" runat="server" Text="["></asp:Label>
                &nbsp;<asp:LinkButton ID="BackBtn" runat="server" CssClass="hereLink" OnClick="BackBtn_Click"
                    CausesValidation="False">Back</asp:LinkButton>
                &nbsp;<asp:Label ID="Label2" runat="server" Text="]"></asp:Label>
                <br />
            </div>
            <div style="text-align: center">
                <asp:Label ID="AddUserLbl" runat="server" CssClass="label" Font-Bold="True" Text="Manage Function/Role/Section"></asp:Label>
                <br />
                <br />
            </div>
            <table>
                <tr>
                    <td style="width: 12px">
                    </td>
                    <td>
                        <asp:Label ID="lblLegend" runat="server" CssClass="label"></asp:Label>
                    </td>
                </tr>
            </table>
            <br />
            <table>
                <tr>
                    <td style="width: 12px">
                    </td>
                    <td style="width: 76px">
                        <asp:Label ID="lblName" runat="server" Text="Name" CssClass="label"></asp:Label>
                    </td>
                    <td>
                        <b>
                            <asp:TextBox ID="NameTbx" runat="server" CssClass="standardManage" onkeypress="return check(event)"
                                onMouseDown="return DisableControlKey(event)"></asp:TextBox>
                        </b>
                    </td>
                    <td>
                        <asp:RequiredFieldValidator ID="RequireValidatorName" runat="server" ControlToValidate="NameTbx"
                            Display="Dynamic" ErrorMessage="Required" ForeColor="Red" EnableTheming="True"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td style="width: 12px">
                        &nbsp;
                    </td>
                    <td style="width: 76px">
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;
                    </td>
                    <td valign="top">
                        <asp:Label ID="lblDesignation" runat="server" Text="Designation" CssClass="label"></asp:Label>
                    </td>
                    <td>
                        &nbsp;</td>
                    <td valign="top">
                        <asp:RequiredFieldValidator ID="RequiredValidatorDesignation" runat="server" ControlToValidate="designationTbx"
                            Display="Dynamic" EnableTheming="True" ErrorMessage="Required" ForeColor="Red"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;
                    </td>
                    <td style="width: 76px">
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;
                    </td>
                    <td valign="top" style="width: 70px">
                        <asp:Label ID="lblSection" runat="server" Text="Section" CssClass="label"></asp:Label>
                    </td>
                    <td>
                        <asp:ListBox ID="listSection" runat="server" SelectionMode="Multiple" CssClass="standardManage"
                            onMouseDown="GetCurrentListValues(this);" onchange="FillListValues(this);"></asp:ListBox>
                    </td>
                    <td valign="top">
                        <asp:RequiredFieldValidator ID="RequiredValidatorSection" runat="server" ControlToValidate="listSection"
                            Display="Dynamic" EnableTheming="True" ErrorMessage="Required" ForeColor="Red"></asp:RequiredFieldValidator>
                        <asp:Label ID="lblValidatorSection" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;
                    </td>
                    <td style="width: 76px">
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;
                    </td>
                    <td style="width: 76px">
                        <asp:Label ID="lblFunction" runat="server" Text="Function" CssClass="label"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlFunction" runat="server" CssClass="standardManage">
                        </asp:DropDownList>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;
                    </td>
                    <td style="width: 76px">
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;
                    </td>
                    <td style="width: 76px">
                        <asp:Label ID="lblUserId" runat="server" Text="User ID" CssClass="label"></asp:Label>
                    </td>
                    <td>
                        <b>
                            <asp:TextBox ID="UserIdTbx" runat="server" CssClass="standardManage" onkeypress="return check(event)"
                                onMouseDown="return DisableControlKey(event)"></asp:TextBox>
                        </b>
                    </td>
                    <td>
                        <asp:RequiredFieldValidator ID="RequiredValidatorUid" runat="server" ControlToValidate="UserIdTbx"
                            Display="Dynamic" EnableTheming="True" ErrorMessage="Required" ForeColor="Red"></asp:RequiredFieldValidator>
                        <asp:Label ID="lblValidatorUserId" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;
                    </td>
                    <td style="width: 76px">
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;
                    </td>
                    <td style="width: 76px">
                        <asp:Label ID="lblRole" runat="server" Text="Role" CssClass="label"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlRole" runat="server" CssClass="standardManage">
                        </asp:DropDownList>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;
                    </td>
                    <td style="width: 76px">
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;
                    </td>
                    <td style="width: 70px">
                        &nbsp;
                    </td>
                    <td>
                        <asp:Button ID="NextBtn" runat="server" Text="Next" OnClick="NextBtn_Click" CssClass="standardButtons" />
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
            </table>
        </asp:View>
        <asp:View ID="View2" runat="server">
    </asp:View>
    </asp:MultiView>
</asp:Content>
