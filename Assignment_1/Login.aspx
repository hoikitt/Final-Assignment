<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Assignment_1.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <script src="https://www.google.com/recaptcha/api.js?render=6LfOZRcaAAAAAJlUMyQv5zEw9Np98rLSTverEvCu"></script>

<%--    <script>
        grecaptcha.ready(function () {
            grecaptcha.execute('6LfOZRcaAAAAAJlUMyQv5zEw9Np98rLSTverEvCu', { action: 'homepage' }).then(function (token) {
                document.getElementById("foo").value = token;
            });
        });
    </script>--%>

    <title></title>
    <style type="text/css">
        .auto-style1 {
            width: 100%;
        }
        .auto-style2 {
            width: 112px;
        }
        .auto-style3 {
            width: 110px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <fieldset>
                <legend>Login</legend>
                <table class="auto-style1">
<%--                    <tr>
                        <td class="auto-style2">
                            <asp:Label ID="lbl_login" runat="server" Text="Login"></asp:Label>
                        </td>
                        <td class="auto-style3">&nbsp;</td>
                    </tr>--%>
                    <tr>
                        <td class="auto-style2">&nbsp;</td>
                        <td class="auto-style3">&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style2">
                            <asp:Label ID="lbl_emailAddress" runat="server" Text="Email:"></asp:Label>
                        </td>
                        <td class="auto-style3">
                            <asp:TextBox ID="tb_emailAddress" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator id="RequiredFieldValidator_emailAddress" runat="server" ControlToValidate="tb_emailAddress" ErrorMessage="Field Empty" Forecolor="Red" SetFocusOnError="True" />
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style2">
                            <asp:Label ID="lbl_password" runat="server" Text="Password:" ></asp:Label>
                        </td>
                        <td class="auto-style3">
                            <asp:TextBox ID="tb_password" runat="server" Textmode="Password"></asp:TextBox>
                           
                        </td>
                        <td>
                            <asp:Label ID="lbl_errorMsg" runat="server"></asp:Label>
                        </td>
                    </tr>
<%--                    <tr>
                        <td class="auto-style2">
                            <asp:Label ID="lbl_creditCard" runat="server" Text="Credit Card:"></asp:Label>
                        </td>
                        <td class="auto-style3">
                            <asp:TextBox ID="tb_creditCard" runat="server"></asp:TextBox>
                        </td>
                    </tr>--%>
                    <tr>
                        <td class="auto-style2">&nbsp;</td>
                        <td class="auto-style3">
                            <asp:Button ID="btn_login" runat="server" Text="Login" Width="170px" OnClick="btn_login_Click" />
                        </td>
                    </tr>

                    <tr>
                        <td class="auto-style2">&nbsp;</td>
                        <td class="auto-style3">
                            <asp:Label ID="lbl_error_message" runat="server" Text="Error message here (lbMessage)"></asp:Label>
                        </td>
                    </tr>

                    <tr>
                        <td class="auto-style2">&nbsp;</td>
                        <td class="auto-style3">
                            <asp:Button ID="btn_resetPassword" runat="server" Text="Reset Password" Width="170px" OnClick="btn_reset_password_Click" visible="false"/>
                        </td>
                    </tr>

<%--                    <tr>
                        <td class="auto-style2">&nbsp;</td>
                        <td class="auto-style3">
                            <asp:Label ID="lbl_gScore" runat="server" EnableViewState="false" Text="Score:"></asp:Label>
                        </td>
                    </tr>--%>

                </table>
                <input type="hidden" id="g-recaptcha-response" name="g-recaptcha-response" />
                <asp:Label ID="lbl_gScore" runat="server"></asp:Label>
            </fieldset>
        </div>
    </form>
    <script>
        grecaptcha.ready(function () {
            grecaptcha.execute('6LfOZRcaAAAAAJlUMyQv5zEw9Np98rLSTverEvCu', { action: 'Login' }).then(function (token) {
                document.getElementById("g-recaptcha-response").value = token;
            });
        });
    </script>
</body>
</html>
