<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="Adminmain.aspx.cs" Inherits="Appraisal.Adminmain" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
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
            GroupingText="Manage Appraisal">
            <div align="center">
                <table>
                    <tr>
                        <td>
                            <asp:Label ID="AppraisalDisplayDetailsLbl" runat="server" CssClass="label"></asp:Label>
                            <asp:LinkButton ID="AppraisalDisplayDetailsLink" runat="server" CssClass="hereLink"
                                OnClick="AppraisalDisplayDetailsLink_Click">here</asp:LinkButton>
                        </td>
                    </tr>
                </table>
            </div>
        </asp:Panel>
        <asp:Panel ID="ManageQuestionPanel" align="left" CssClass="defaultPanel" runat="server"
            GroupingText="Manage Question">
            <div align="center">
                <table>
                    <tr>
                        <td>
                            <asp:Label ID="ManageQuestionsLbl" runat="server" CssClass="label"></asp:Label>
                            <asp:LinkButton ID="ManageQuestionLink" runat="server" CssClass="hereLink" OnClick="ManageQuestionLink_Click">here</asp:LinkButton>
                        </td>
                    </tr>
                </table>
            </div>
        </asp:Panel>
        <asp:Panel ID="ManageSystem" align="left" CssClass="defaultPanel" runat="server"
            GroupingText="Manage System">
            <div align="center">
                <table>
                    <tr>
                        <td style="height: 22px">
                            <asp:Label ID="ManageUserLbl" runat="server" CssClass="label"></asp:Label>
                            <asp:LinkButton ID="ManageUserLink" runat="server" CssClass="hereLink" OnClick="ManageUserLink_Click">here</asp:LinkButton>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="ExportImportStaffListLbl" runat="server" CssClass="label"></asp:Label>
                            <asp:LinkButton ID="ExportImportStaffLink" runat="server" CssClass="hereLink" OnClick="ExportImportStaffLink_Click">here</asp:LinkButton>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="SetOpenCloseLbl" runat="server" CssClass="label"></asp:Label>
                            <asp:LinkButton ID="SetOpenCloseLink" runat="server" CssClass="hereLink" OnClick="SetOpenCloseLink_Click">here</asp:LinkButton>
                        </td>
                    </tr>
                </table>
            </div>
        </asp:Panel>
        <asp:Panel ID="ViewAppraisalSummary" align="left" CssClass="defaultPanel" runat="server"
            GroupingText="View Evaluation Status">
            <div align="center">
                <table>
                    <tr>
                        <td style="height: 22px">
                            <asp:Label ID="ViewNotCompletedLbl" runat="server" CssClass="label"></asp:Label>
                            <asp:LinkButton ID="ViewNotCompletedLink" runat="server" CssClass="hereLink" OnClick="ViewNotCompletedLink_Click">here</asp:LinkButton>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 22px">
                            <asp:Label ID="ViewIndividualAllLbl" runat="server" CssClass="label"></asp:Label>
                            <asp:LinkButton ID="ViewIndividualAllLink" runat="server" CssClass="hereLink" OnClick="ViewIndividualAllLink_Click">here</asp:LinkButton>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 22px">
                            <asp:Label ID="ViewAppraisalChart" runat="server" CssClass="label"></asp:Label>
                            <asp:LinkButton ID="ViewAppraisalChartLink" runat="server" CssClass="hereLink" OnClick="ViewAppraisalChartLink_Click">here</asp:LinkButton>
                        </td>
                    </tr>
                </table>
            </div>
        </asp:Panel>
        <asp:Panel ID="ManageStaffPassword" align="left" CssClass="defaultPanel" runat="server"
            GroupingText="Manage Staff Password">
            <div align="center">
                <table>
                    <tr>
                        <td>
                            <asp:Label ID="ResetPasswordLbl" runat="server" CssClass="label"></asp:Label>
                            <asp:LinkButton ID="ResetPasswordLink" runat="server" CssClass="hereLink" OnClick="ResetPasswordLink_Click">here</asp:LinkButton>
                        </td>
                    </tr>
                </table>
            </div>
        </asp:Panel>
        <asp:Panel ID="ManageAppraisalSummary" align="left" CssClass="defaultPanel" runat="server"
            GroupingText="Manage Staff Evaluation Report">
            <div align="center">
                <table>
                    <tr>
                        <td style="height: 22px">
                            <asp:Label ID="DeleteAllLbl" runat="server" CssClass="label"></asp:Label>
                            <asp:LinkButton ID="deleteAllLink" runat="server" CssClass="hereLink" OnClick="deleteAllLink_Click">here</asp:LinkButton>
                            <asp:ConfirmButtonExtender ID="deleteAllLink_ConfirmButtonExtender" runat="server"
                                ConfirmText="Are you sure to delete all evaluation records?" Enabled="True" TargetControlID="deleteAllLink">
                            </asp:ConfirmButtonExtender>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 22px">
                            <asp:Label ID="DeleteSingleUidLbl" runat="server" CssClass="label"></asp:Label>
                            <asp:DropDownList ID="ddlUid" runat="server" CssClass="otherstandard">
                            </asp:DropDownList>
                            <asp:LinkButton ID="deleteSingleLink" runat="server" CssClass="hereLink" OnClick="deleteSingleLink_Click">delete</asp:LinkButton>
                            <asp:ConfirmButtonExtender ID="deleteSingleLink_ConfirmButtonExtender" runat="server"
                                ConfirmText="Are you sure you want to delete all submitted evaluation records by this user?"
                                Enabled="True" TargetControlID="deleteSingleLink">
                            </asp:ConfirmButtonExtender>
                        </td>
                    </tr>
                </table>
            </div>
        </asp:Panel>
        <br />
    </div>
    &nbsp;&nbsp;&nbsp; *Best viewed by IE 7 and above
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
</asp:Content>
