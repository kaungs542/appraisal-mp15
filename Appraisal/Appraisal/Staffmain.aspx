<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="Staffmain.aspx.cs"
    Inherits="Appraisal.Staffmain" %>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
<%--    <div align="right">
        <asp:Label ID="WelcomeLbl" runat="server" Text="Welcome: " CssClass="label"></asp:Label>
        <asp:Label ID="staffName" runat="server" ForeColor="#009933" CssClass="label"></asp:Label>
    </div>--%>
    <div align="center">
        <asp:Panel ID="DefaultPanel" align="left" CssClass="defaultPanel" runat="server"
            GroupingText="Peer Evaluation">
            <div align="center">
                <table>
                    <tr>
                        <td>
                            <asp:Label ID="SubmitAppraisalLbl" runat="server" CssClass="label"></asp:Label>
                            <%--<asp:LinkButton ID="SubmitAppraisalLink" runat="server" CssClass="hereLink" OnClick="SubmitAppraisalLink_Click">here</asp:LinkButton>--%>
                            <%--<asp:HyperLink ID="SubmitLink" runat="server" CssClass="hereLink" OnClick="SubmitAppraisalLink_Click">here</asp:HyperLink>--%>
                            <asp:LinkButton ID="SubmitLink" runat="server" CssClass="hereLink" OnClick="SubmitAppraisalLink_Click">here</asp:LinkButton>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="ViewAppraisalLbl" runat="server" CssClass="label"></asp:Label>
                            <asp:LinkButton ID="ViewAppraisalLink" runat="server" CssClass="hereLink" OnClick="ViewAppraisalLink_Click">here</asp:LinkButton>
                        </td>
                    </tr>
                </table>
            </div>
        </asp:Panel>
        <asp:Panel ID="ManageAppraisalPanel" align="left" CssClass="defaultPanel" runat="server"
            GroupingText="View Evaluation Status">
            <div align="center">
                <table>
                    <tr>
                        <td>
                            <asp:Label ID="ViewIndividualAllLbl" runat="server" CssClass="label"></asp:Label>
                            <asp:LinkButton ID="ViewIndividualAllLink" runat="server" CssClass="hereLink" OnClick="ViewIndividualAllLink_Click">here</asp:LinkButton>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="ViewAppraisalChart" runat="server" CssClass="label"></asp:Label>
                            <asp:LinkButton ID="ViewAppraisalChartLink" runat="server" CssClass="hereLink" OnClick="ViewAppraisalChartLink_Click">here</asp:LinkButton>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="ViewGraph" runat="server" CssClass="label"></asp:Label>
                            <asp:LinkButton ID="ViewGraphLink" runat="server" CssClass="hereLink" OnClick="ViewGraphLink_Click">here</asp:LinkButton>
                        </td>
                    </tr>
                </table>
            </div>
        </asp:Panel>
        <br />
        <br />
    </div>
    &nbsp;&nbsp;&nbsp; *Best viewed by IE 7 and above
</asp:Content>
