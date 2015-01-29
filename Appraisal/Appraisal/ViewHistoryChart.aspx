<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="ViewHistoryChart.aspx.cs" Inherits="Appraisal.ViewHistoryChart" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <table align="center" width="100%">
        <tr>
            <td>
                &nbsp;
                <asp:ImageButton ID="SearchSwapBtn" runat="server" AlternateText="Switch search filter"
                    Height="30px" ImageUrl="~/Image/swap.jpg" OnClick="SearchSwapBtn_Click" />
                <asp:Panel ID="SearchPanelSectionViaFunction" runat="server" GroupingText="View Chart By Section Group">
                    <div align="center">
                        <table>
                            <tr align="left">
                                <td>
                                    <asp:Label ID="lblSelectSection" runat="server" CssClass="label"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlSelectSection" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlSelectSection_SelectedIndexChanged"
                                        CssClass="otherstandardDropdown">
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                            <tr align="left">
                                <td>
                                    <asp:Label ID="lblSelectQuestion" runat="server" CssClass="label"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlSelectQuestion" runat="server" CssClass="otherstandardDropdown">
                                        <asp:ListItem></asp:ListItem>
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
                                    <asp:Button ID="AddBtn" runat="server" CssClass="standardButtons" Height="25px" Text="Add"
                                        OnClick="AddBtn_Click" />
                                </td>
                            </tr>
                            <tr align="left">
                                <td>
                                    <asp:Label ID="lblSelectedFunction" runat="server" CssClass="label"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="SelectTbxFunction" runat="server" CssClass="otherstandard" ReadOnly="True"
                                        Width="205px"></asp:TextBox>
                                    <asp:TextBoxWatermarkExtender ID="SelectTbxFunction_TextBoxWatermarkExtender" 
                                        runat="server" TargetControlID="SelectTbxFunction" 
                                        WatermarkText="Click on add to insert item" 
                                        WatermarkCssClass="watermarkcss">
                                    </asp:TextBoxWatermarkExtender>
                                </td>
                                <td>
                                    <asp:Button ID="SearchBtn" runat="server" CssClass="standardButtons" Text="Display"
                                        OnClick="SearchBtn_Click" />
                                </td>
                                <td>
                                    <asp:Button ID="ClearBtn" runat="server" CssClass="standardButtons" Text="Clear"
                                        OnClick="ClearBtn_Click" Visible="false" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </asp:Panel>
                <asp:Panel ID="SearchPanelFunctionViaSection" runat="server" GroupingText="View Chart By Function Group">
                    <div align="center">
                        <table>
                            <tr align="left">
                                <td>
                                    <asp:Label ID="lblSelectFunction" runat="server" CssClass="label"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlSelectFunction" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlSelectFunction_SelectedIndexChanged"
                                        CssClass="otherstandardDropdown">
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                            <tr align="left">
                                <td>
                                    <asp:Label ID="lblFilterByQuestion" runat="server" CssClass="label"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlFilterQuestion" runat="server" CssClass="otherstandardDropdown">
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                            <tr align="left">
                                <td>
                                    <asp:Label ID="lblFilterBySection" runat="server" CssClass="label"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlFilterBySection" runat="server" CssClass="otherstandardDropdown">
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    <asp:Button ID="AddSectionBtnSection" runat="server" CssClass="standardButtons" Height="25px"
                                        Text="Add" OnClick="AddSectionBtnSection_Click" />
                                </td>
                            </tr>
                            <tr align="left">
                                <td>
                                    <asp:Label ID="lblSelectedSection" runat="server" CssClass="label"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="SelectTbxSection" runat="server" CssClass="otherstandard" ReadOnly="True"
                                        Width="205px"></asp:TextBox>
                                    <asp:TextBoxWatermarkExtender ID="SelectTbxSection_TextBoxWatermarkExtender" 
                                        runat="server" TargetControlID="SelectTbxSection" 
                                        WatermarkText="Click on add to insert items" 
                                        WatermarkCssClass="watermarkcss">
                                    </asp:TextBoxWatermarkExtender>
                                </td>
                                <td>
                                    <asp:Button ID="SearchBtnSection" runat="server" CssClass="standardButtons" Text="Display"
                                        OnClick="SearchBtnSection_Click" />
                                </td>
                                <td>
                                    <asp:Button ID="ClearBtnSection" runat="server" CssClass="standardButtons" Text="Clear"
                                        OnClick="ClearBtn_Click" Visible="false" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </asp:Panel>
            </td>
        </tr>
    </table>
    <br />
    <asp:MultiView ID="MultiView1" runat="server">
        <asp:View ID="View2" runat="server">
            <br />
            <div align="center">
                <table>
                    <tr>
                        <td align="left">
                            <b>Peer Evaluation Chart</b>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Chart ID="Chart1" runat="server" AntiAliasing="Graphics" BackColor="Transparent">
                                <Series>
                                    <asp:Series Name="Series1">
                                    </asp:Series>
                                </Series>
                                <ChartAreas>
                                    <asp:ChartArea Name="ChartArea1">
                                    </asp:ChartArea>
                                </ChartAreas>
                            </asp:Chart>
                        </td>
                    </tr>
                </table>
            </div>
            <table>
                <tr>
                    <td>
                        <asp:Label ID="lbllegendHistory" runat="server" CssClass="label"></asp:Label>
                    </td>
                </tr>
            </table>
            <br />
        
            <br />
            <div align="center">
                <table>
                    <tr>
                        <td align="left">
                            <b>Peer Evaluation Chart</b>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Chart ID="Chart2" runat="server" AntiAliasing="Graphics" BackColor="Transparent">
                                <Series>
                                    <asp:Series Name="Series1">
                                    </asp:Series>
                                </Series>
                                <ChartAreas>
                                    <asp:ChartArea Name="ChartArea1">
                                    </asp:ChartArea>
                                </ChartAreas>
                            </asp:Chart>
                        </td>
                    </tr>
                </table>
            </div>
                        <br />
        </asp:View>
        <asp:View ID="View1" runat="server">
            <br />
            <div>
            <table width="100%">
                <tr>
                    <td align="center"><asp:Label ID="lbDisplay" runat="server" CssClass="label"></asp:Label></td>
                </tr>
            </table>
                
            </div>
            
            <br />
        </asp:View>
    </asp:MultiView>
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .watermarkcss
        {
            font-size: small;
            color:gray;
        }
    </style>
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
