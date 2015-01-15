<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="ViewAppraisalAllHistory.aspx.cs" Inherits="Appraisal.ViewAppraisalAllHistory" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">
    <!--[if lt IE 9]>
	<script>
		$(function() {
		
			var el;
			
			$("select.otherstandardDropdown")
				.each(function() {
					el = $(this);
					el.data("origWidth", el.outerWidth()) // IE 8 will take padding on selects
				})
			  .mouseenter(function(){
			    $(this).css("width", "auto");
			  })
			  .bind("blur change", function(){
			  	el = $(this);
			    el.css("width", el.data("origWidth"));
			  });
		
		});
	</script>
	<![endif]-->
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Panel ID="SearchPanel" runat="server" GroupingText="Search Evaluation Report">
        <div align="center">
            <table>
                <tr align="left">
                    <td>
                        <asp:Label ID="lblFilterBySection" runat="server" CssClass="label"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlFilterSection" runat="server" CssClass="otherstandardDropdown">
                        </asp:DropDownList>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr align="left">
                    <td>
                        <asp:Label ID="lblFilterByFunction" runat="server" CssClass="label"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlFilterFunction" runat="server" CssClass="otherstandardDropdown">
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:Button ID="SearchBySectionFunction" runat="server" OnClick="SearchBySectionFunction_Click"
                            Text="Search" CssClass="standardButtons" />
                    </td>
                </tr>
                <tr align="left">
                    <td>
                        <asp:Label ID="lblSearchIndividual" runat="server" CssClass="label"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="SearchTbx" runat="server" CssClass="otherstandard" onkeypress="return check(event)"
                            onMouseDown="return DisableControlKey(event)" Width="205px"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Button ID="SearchIndividualBtn" runat="server" Text="Search" OnClick="SearchIndividualBtn_Click"
                            CssClass="standardButtons" />
                    </td>
                </tr>
            </table>
        </div>
    </asp:Panel>
    <asp:MultiView ID="MultiView1" runat="server">
        <asp:View ID="View1" runat="server">
            <table style="padding-left: 10px; width: 100%">
                <tr>
                    <td>
                        <asp:Button ID="ViewOverallBtn" runat="server" CssClass="standardManage" OnClick="ViewOverallBtn_Click"
                            Width="200px" />
                        <asp:Button ID="InvisBtn" runat="server" Style="display: none" />
                        <asp:ModalPopupExtender ID="ModalPopupExtender" runat="server" BackgroundCssClass="ModalPopupBG"
                            CancelControlID="CancelBtn" Enabled="True" PopupControlID="ViewOverAllGradePanel"
                            TargetControlID="InvisBtn">
                        </asp:ModalPopupExtender>
                        <br />
                        <br />
                        <asp:ImageButton ID="exportWord" runat="server" CssClass="csvwordpdficon" ImageUrl="~/Image/word.png"
                            OnClick="exportWord_Click" AlternateText="Export to word" />
                        <asp:ImageButton ID="exportExcel" runat="server" AlternateText="Export to excel"
                            CssClass="csvwordpdficon" ImageUrl="~/Image/excel.jpg" OnClick="exportExcel_Click" />
                        <asp:Panel ID="panel1" runat="server" Height="500px" Width="100%" ScrollBars="Vertical">
                            <asp:GridView ID="ViewAllHistory" runat="server" BackColor="Black" AutoGenerateColumns="False"
                                CssClass="otherstandard" Width="100%">
                                <RowStyle BackColor="White" />
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="ViewBtn" runat="server" OnClick="ViewBtn_Click">View</asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="UserID" HeaderText="User ID">
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Name" HeaderText="Name"></asp:BoundField>
                                    <asp:BoundField DataField="Section" HeaderText="Section">
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Function" HeaderText="Function">
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="Grade">
                                        <ItemTemplate>
                                            <asp:Panel ID="ScrollPanel" Style="width: 793px" runat="server" ScrollBars="Horizontal">
                                                <asp:GridView ID="GridView1" Style="position: static" runat="server" BackColor="Black">
                                                    <FooterStyle BackColor="#CCCCCC" />
                                                    <RowStyle BackColor="White" Wrap="true" />
                                                    <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                                                    <HeaderStyle BackColor="Black" Font-Bold="False" ForeColor="White" />
                                                </asp:GridView>
                                            </asp:Panel>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <FooterStyle BackColor="#CCCCCC" />
                                <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                                <HeaderStyle BackColor="Black" Font-Bold="False" ForeColor="White" />
                            </asp:GridView>
                        </asp:Panel>
                    </td>
                </tr>
                <tr style="height: 12px">
                    <td>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lbllegendHistory" runat="server" CssClass="label"></asp:Label>
                    </td>
                </tr>
            </table>
            <br />
        </asp:View>
        <asp:View ID="View2" runat="server">
            <div style="text-align: left">
                &nbsp; &nbsp;&nbsp;&nbsp;<asp:Label ID="bracket1" runat="server" Text="["></asp:Label>
                &nbsp;<asp:LinkButton ID="IndividualBack" runat="server" CssClass="hereLink" OnClick="IndividualBack_Click">Back</asp:LinkButton>
                &nbsp;<asp:Label ID="bracket2" runat="server" Text="]"></asp:Label>
            </div>
            <br />
            <table style="padding-left: 10px; width: 100%;">
                <tr>
                    <td style="font-size: medium;">
                        <b>View PDF (History chart type) : </b>
                    </td>
                    <td style="font-size: medium;">
                        <b>1. Choose period (Months) : </b>
                    </td>
                    <td style="font-size: medium;">
                        <b>2. Choose question : </b>
                    </td>
                    <td style="font-size: medium;">
                        <b>3. Choose report output : </b>
                    </td>
                    <td style="font-size: medium;">
                        <b>4. Generate report (Below) :</b>
                    </td>
                </tr>
                <tr>
                    <td>
                        <table width='100%'>
                            <tr>
                                <td>
                                    <img alt="Line" class="style2" src="Image/pdf.png" />
                                    <asp:LinkButton ID="historylineLink" runat="server" OnClick="historylineLink_Click"
                                        CssClass="label">(Line)</asp:LinkButton>
                                </td>
                                <td>
                                    <img alt="Bar" class="style2" src="Image/pdf.png" id="bar" />
                                    <asp:LinkButton ID="historychartbarLink" runat="server" OnClick="historychartbarLink_Click"
                                        CssClass="label">(Bar)</asp:LinkButton>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlOverDates" runat="server" AutoPostBack="True" CssClass="otherstandardDropdown"
                            OnSelectedIndexChanged="ddlOverDates_SelectedIndexChanged" Width="210px">
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlFilterQuestion" runat="server" AutoPostBack="True" CssClass="otherstandardDropdown"
                            OnSelectedIndexChanged="ddlFilterQuestion_SelectedIndexChanged" Width="210px">
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlFilterOutPut" runat="server" AutoPostBack="True" CssClass="otherstandardDropdown"
                            OnSelectedIndexChanged="ddlFilterOutPut_SelectedIndexChanged" Width="210px">
                            <asp:ListItem Selected="True">Both grade and comment</asp:ListItem>
                            <asp:ListItem>Only comment</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:ImageButton ID="wordExportIndividual" runat="server" AlternateText="Export to word"
                            CssClass="csvwordpdficon" ImageUrl="~/Image/word.png" OnClick="wordExportIndividual_Click" />
                    </td>
                </tr>
            </table>
            <br />
            <table width='100%' style="padding-left: 10px">
                <tr>
                    <td>
                        <asp:Label ID="nameofAppraisal" runat="server" CssClass="label"></asp:Label>
                        <asp:Label ID="lblUserName" runat="server" CssClass="label"></asp:Label>
                    </td>
                </tr>
            </table>
            <br />
            <asp:Label ID="lblYourStat" runat="server" CssClass="label"></asp:Label>
            <br />
            <asp:Label ID="lbllegend" runat="server" CssClass="label"></asp:Label>
        </asp:View>
    </asp:MultiView>
    <br />
    <br />
    <asp:Panel ID="ViewOverAllGradePanel" runat="server" BackColor="White" GroupingText="Overall Average School's Grade Panel"
        Style="background-image: url(./Image/bg1.jpg); display: none">
        <br />
        <asp:GridView ID="ViewOverAllSchoolGrid" runat="server" BackColor="Black" AutoGenerateColumns="False"
            CssClass="otherstandard">
            <RowStyle BackColor="White" />
            <Columns>
                <asp:BoundField DataField="Name" HeaderText="Name" />
                <asp:TemplateField HeaderText="Grade">
                    <ItemTemplate>
                        <asp:Panel ID="ScrollPanel" Style="width: 793px" runat="server" ScrollBars="Horizontal">
                            <asp:GridView ID="InsideGridOverAll" Style="position: static" runat="server" BackColor="Black">
                                <FooterStyle BackColor="#CCCCCC" />
                                <RowStyle BackColor="White" Wrap="true" />
                                <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                                <HeaderStyle BackColor="Black" Font-Bold="False" ForeColor="White" />
                            </asp:GridView>
                        </asp:Panel>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <FooterStyle BackColor="#CCCCCC" />
            <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="Black" Font-Bold="False" ForeColor="White" />
        </asp:GridView>
        <br />
        <table>
            <tr>
                <asp:Button ID="CancelBtn" CssClass="standardButtons" runat="server" Text="Cancel"
                    Width="76px" /></tr>
        </table>
    </asp:Panel>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
</asp:Content>
