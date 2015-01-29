<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="ViewGraph.aspx.cs" Inherits="Appraisal.ViewGraph" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
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
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <table align="center" width="100%">
        <tr>
            <td>
                <asp:Panel ID="Question" runat="server" GroupingText="View Chart By Each Question">
                    <div align="center">
                        <table>
                            
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
                                    <asp:Button ID="Display" runat="server" CssClass="standardButtons" Text="Display"
                                        OnClick="Display_Click" />
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
