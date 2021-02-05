<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ResetPassword.aspx.cs" Inherits="Assignment_1.ResetPassword" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Reset Password</title>

    <script type="text/javascript">
        function validate() {
            var str = document.getElementById('<%=tb_new_password.ClientID%>').value;

            if (str.length < 8) {
                document.getElementById("lbl_pwdChecker").innerHTML = "Password length must be at least 8 characters";
                document.getElementById("lbl_pwdChecker").style.color = "Red";

                return ("too short");
            }

            else if (str.search(/[0-9]/) == -1) {
                document.getElementById("lbl_pwdChecker").innerHTML = "Password requires at least 1 number.";
                document.getElementById("lbl_pwdChecker").style.color = "Red";

                return ("no_number");
            }

            else if (str.search(/[A-Z]/) == -1) {
                document.getElementById("lbl_pwdChecker").innerHTML = "Password requires an uppercase character."
                document.getElementById("lbl_pwdChecker").style.color = "Red"

                return ("no_uppercase");
            }

            else if (str.search(/[a-z]/) == -1) {
                document.getElementById("lbl_pwdChecker").innerHTML = "Password requires a lowercase character."
                document.getElementById("lbl_pwdChecker").style.color = "Red"

                return ("no_lowercase");
            }

            else if (str.search(/[!@#$%^&*()_+]/) == -1) {
            //else if (str.search(/^[a-zA-Z0-9äöüÄÖÜ]*$/) == -1) {
                document.getElementById("lbl_pwdChecker").innerHTML = "Password requires a special character."
                document.getElementById("lbl_pwdChecker").style.color = "Red"

                return ("no_special_character");
            }

            else {
                document.getElementById("lbl_pwdChecker").innerHTML = "Excellent!";
                document.getElementById("lbl_pwdChecker").style.color = "Green";

            }
        }

    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div id="container">
            <table class="auto-style1">
                <tr>
                    <td class="auto-style2">&nbsp;</td>
                    <td class="auto-style3">&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>

                 <tr>
                    <td class="auto-style2">
                        <asp:Label ID="lbl_old_password" runat="server" Text="Current Password:"></asp:Label>
                    </td>
                   
                    <td class="auto-style3">
                        <asp:TextBox ID="tb_old_password" runat="server" Width="342px" Textmode="Password"></asp:TextBox>
                        <asp:RequiredFieldValidator id="RequiredFieldValidator_old_password" runat="server" ControlToValidate="tb_old_password" ErrorMessage="Old Password is required!" Forecolor="Red" SetFocusOnError="True" Display="Dynamic"/>
                    </td>
                </tr>

                <tr>
                    <td class="auto-style2">
                        <asp:Label ID="lbl_new_password" runat="server" Text="New Password:"></asp:Label>
                    </td>
                    <td class="auto-style3">
                        <asp:TextBox ID="tb_new_password" runat="server" Width="342px" Textmode="Password" onkeyup="javascript:validate()"></asp:TextBox>
                        <asp:RequiredFieldValidator id="RequiredFieldValidator_new_password" runat="server" ControlToValidate="tb_new_password" ErrorMessage="New Password is required!" Forecolor="Red" SetFocusOnError="True" Display="Dynamic" />                     
                    </td>
                </tr>

                <tr>
                    <td>

                    </td>
                    <td>
                        <asp:Label ID="lbl_pwdChecker" runat="server" Text="pwdChecker"></asp:Label>
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
                    <td class="auto-style3">
                        <asp:Button ID="btn_changePassword" runat="server" Text="Confirm" Width="200px" OnClick="btn_ChangePassword_Click" />
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
