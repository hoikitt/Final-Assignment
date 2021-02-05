<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EmailRequestResetPassword.aspx.cs" Inherits="Assignment_1.EmailRequestResetPassword" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Email To Reset Password</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div id="container">
            <table class="auto-style1">
                <tr>
                    <td class="auto-style2">&nbsp;</td>
                    <td class="auto-style3">&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>

                 <tr>
                    <td class="auto-style2">
                        <asp:Label ID="lbl_emailAddress" runat="server" Text="Email:"></asp:Label>
                    </td>
                    <td class="auto-style3">
                        <asp:TextBox ID="tb_emailAddress" runat="server"></asp:TextBox>
                    </td>
                </tr>   

                <tr>
                    <td>

                    </td>
                    <td>
                        <asp:Label ID="passChangeErrMsg" runat="server" CssClass="alert alert-dismissable alert-danger"></asp:Label>
                    </td>

                </tr>

                <tr>
                    <td class="auto-style2">&nbsp;</td>
                    <td class="auto-style3">&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>

                <tr>
                    <td class="auto-style2">&nbsp;</td>
                    <td class="auto-style3">
                        <asp:Button ID="btn_sendEmail" runat="server" Text="Send" Width="200px" OnClick="btn_SendEmail_Click" />
                    </td>
                </tr>
            </table>
        </div>
        </div>
    </form>
</body>
</html>
