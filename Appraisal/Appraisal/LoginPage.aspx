<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LoginPage.aspx.cs" Inherits="Appraisal.LoginPage" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<script type="text/javascript">

    document.attachEvent("onkeydown", my_onkeydown_handler);
    function my_onkeydown_handler() {
        switch (event.keyCode) {

            case 116: // 'F5' 
                event.returnValue = false;
                event.keyCode = 0;
                window.status = "F5 disabled";
                break;
        }
    }
    function limitText(limitField, limitNum) {
        if (limitField.value.length > limitNum) {
            limitField.value = limitField.value.substring(0, limitNum);
            alert("Your password has exceeded the limit.");
        }
    }
    function check(e) {
        var keynum
        var keychar
        var numcheck
        // For Internet Explorer   
        if (window.event) {
            keynum = e.keyCode
        }
        // For Netscape/Firefox/Opera   
        else if (e.which) {
            keynum = e.which
        }
        keychar = String.fromCharCode(keynum)
        //List of special characters you want to restrict   
        if (keychar == "`" || keychar == "~" || keychar == "#" || keychar == "$" || keychar == "]"
 || keychar == "%" || keychar == "^" || keychar == "*" || keychar == "["
 || keychar == "{" || keychar == "}" || keychar == ";"
 || keychar == "+" || keychar == "=" || keychar == "<" || keychar == ">"
 || keychar == "|" || keychar == "-") {
            return false;
        }
        else {
            return true;
        }
    }

    function DisableControlKey(e) {
        // Message to display
        var message = "Right click option disabled.";
        // Condition to check mouse right click
        if (e.button == 2) {
            alert(message);
            return false;
        }
    }

</script>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1/jquery.min.js"></script>
<script src="WaterMark.min.js" type="text/javascript"></script>
<script type="text/javascript">
    $(function () {
        $("[id*=staffnumber], [id*=password]").WaterMark();
    });
</script>
<head runat="server">
    <title>Online 360° Leadership System</title>
    <style type="text/css">
        .style40
        {
            width: 152px;
        }
        .label
        {
            font-family: Arial;
            font-size: medium;
        }
        .LoginButton
        {
            height: 25px;
            width: 90px;
            font-family: Arial;
            margin-bottom: 0px;
        }
        .style41
        {
            width: 213px;
        }
    h1 {
margin-bottom: 10px;
}
h1 { font-size: 1em; }
strong {
font-weight: bold;
}
p {
margin-bottom: 10px;
}
form fieldset input[type=text] {
background-color: #e5e5e5;
border: none;
border-radius: 3px;
-moz-border-radius: 3px;
-webkit-border-radius: 3px;
color: #5a5656;
font-family: 'Open Sans', Arial, Helvetica, sans-serif;
font-size: 14px;
height: 50px;
outline: none;
padding: 0px 10px;
width: 280px;
-webkit-appearance:none;
}
input[type=password] {
background-color: #e5e5e5;
border: none;
border-radius: 3px;
-moz-border-radius: 3px;
-webkit-border-radius: 3px;
color: #5a5656;
font-family: 'Open Sans', Arial, Helvetica, sans-serif;
font-size: 14px;
height: 50px;
outline: none;
padding: 0px 10px;
width: 280px;
-webkit-appearance:none;
}
form fieldset input[type="submit"] {
background-color: #008dde;
border: none;
border-radius: 3px;
-moz-border-radius: 3px;
-webkit-border-radius: 3px;
color: #f4f4f4;
cursor: pointer;
font-family: 'Open Sans', Arial, Helvetica, sans-serif;
height: 50px;
text-transform: uppercase;
width: 300px;
-webkit-appearance:none;
}
    </style>
    <script type="text/javascript">
        window.history.forward(1); 
    </script>
</head>
<body style="background-image: url(./Image/bg1.jpg);">
    <form id="form1" runat="server">
    <div align="center">
        <div align="center">
            <table>
                <tr>
                    <td>
                        <asp:Image ID="Image1" runat="server" ImageUrl="~/Image/tplogo.gif" />
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label6" runat="server" Style="font-size: large; font-weight: 700;"
                            Text="School of Applied Science" CssClass="label"></asp:Label>
                    </td>
                </tr>
            </table>
        </div>
        <br />
        <table>
            <tr>
                <td>
                    <table>
                        <tr>
                            <td>
<div id="login">
<h1><strong>Welcome from </strong><span style="font-size:12.0pt;font-family:&quot;Times New Roman&quot;,serif">ASC Online</span><span lang="EN-GB" style="font-size:12.0pt;font-family:&quot;Times New Roman&quot;,serif;
mso-ansi-language:EN-GB"> 360</span><span class="st"><span style="font-size:13.5pt;
font-family:&quot;Times New Roman&quot;,serif;font-weight:normal;mso-bidi-font-weight:
bold">°</span><span style="font-size:13.5pt;font-family:
&quot;Times New Roman&quot;,serif;mso-ansi-language:EN-GB;font-weight:normal;mso-bidi-font-weight:
bold"> </span><span lang="EN-GB" style="font-size:12.0pt;
font-family:&quot;Times New Roman&quot;,serif;mso-ansi-language:EN-GB;mso-bidi-font-weight:
bold">Leadership</span><span lang="EN-GB" style="font-size:
13.5pt;font-family:&quot;Times New Roman&quot;,serif;mso-ansi-language:EN-GB;font-weight:
normal;mso-bidi-font-weight:bold"> </span></span><span style="font-size:12.0pt;
font-family:&quot;Times New Roman&quot;,serif">System<o:p></o:p></span></h1>
    <h1>Please login to continue.</h1>
<fieldset>
<p>
    <asp:TextBox ID="staffnumber" runat="server" ToolTip="Enter User ID" Height="50px" Width="280px"></asp:TextBox>
    </p>
    <p>
                                                            <asp:RequiredFieldValidator ID="adminstaffvalidator0" runat="server" ControlToValidate="staffnumber"
                                                                Display="Dynamic" ErrorMessage="Required" ForeColor="Red"></asp:RequiredFieldValidator>
    </p>
<p>
    <asp:TextBox ID="password" runat="server" TextMode="Password" ToolTip="Enter Password" Height="50px" Width="280px"></asp:TextBox>
    </p>
    <p>
                                                            <asp:RequiredFieldValidator ID="passwordvalidator0" runat="server" ControlToValidate="password"
                                                                Display="Dynamic" ErrorMessage="Required" ForeColor="Red"></asp:RequiredFieldValidator>
    </p>
<p>
    <asp:Button ID="LoginBtn" runat="server" Text="Login" BackColor="#33CC33" OnClick="login_button_Click" />
    </p>
    <p>
                                                            <asp:Label ID="messagelbl" runat="server" ForeColor="Red" Style="text-align: left"></asp:Label>
    </p>
</fieldset>
    </div> 
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td align="left" style="font-family: Arial; font-size: medium">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td align="left" style="font-family: Arial; font-size: medium">
                    <b>For Staff:</b><br />
                    Please use your email id for login.
                </td>
            </tr>
        </table>
        <br />
    </div>
    </form>
</body>
</html>
