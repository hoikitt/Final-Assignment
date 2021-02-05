<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Registration.aspx.cs" Inherits="Assignment_1.Registration" ValidateRequest="false" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">


<head runat="server">

    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery.inputmask/3.3.4/jquery.inputmask.bundle.js"></script>
 

    <title>My Registration</title>

    <script type="text/javascript">
        function validate() {
            var str = document.getElementById('<%=tb_password.ClientID%>').value;

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

        <%--$("#<%=tb_creditCard.ClientID%>").inputmask({
            mask: "9999 9999 9999 9999",
            placeholder: ""
        });--%>

    </script>      

</head>
<body>
    <form id="form1" runat="server">
        <div id="container">

            <table class="auto-style1">
                <tr>
                    <td class="auto-style2">
                        <asp:Label ID="lbl_registration" runat="server" Text="Registration"></asp:Label>
                    </td>
                    <td class="auto-style3">&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style2">&nbsp;</td>
                    <td class="auto-style3">&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>

                <tr>
                    <td class="auto-style2">
                        <asp:Label ID="lbl_adminNo" runat="server" Text="Admin No:"></asp:Label>
                    </td>
                    <td class="auto-style3">
                        <asp:TextBox ID="tb_adminNo" runat="server" Width="342px"></asp:TextBox>
                        <asp:RequiredFieldValidator id="RequiredFieldValidator_adminNo" runat="server" ControlToValidate="tb_adminNo" ErrorMessage="Admin number is required!" Forecolor="Red" SetFocusOnError="True" />
                    </td>
                </tr>

                <tr>
                    <td class="auto-style2">
                        <asp:Label ID="lbl_firstName" runat="server" Text="First Name:"></asp:Label>
                    </td>
                    <td class="auto-style3">
                        <asp:TextBox ID="tb_firstName" runat="server" Width="342px"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator_fName" runat="server" ErrorMessage="Only characters are allowed" Forecolor="Red" ControlToValidate="tb_firstName" ValidationExpression="^[A-Za-z]*$" ></asp:RegularExpressionValidator>
                        <asp:RequiredFieldValidator id="RequiredFieldValidator_firstName" runat="server" ControlToValidate="tb_firstName" ErrorMessage="First name is required!" Forecolor="Red" SetFocusOnError="True" />
                    </td>
                </tr>

                <tr>
                    <td class="auto-style2">
                        <asp:Label ID="lbl_lastName" runat="server" Text="Last Name:"></asp:Label>
                    </td>
                    <td class="auto-style3">
                        <asp:TextBox ID="tb_lastName" runat="server" Width="342px"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator_lName" runat="server" ErrorMessage="Only characters are allowed" Forecolor="Red" ControlToValidate="tb_lastName" ValidationExpression="^([a-zA-Z]{2,}\s[a-zA-Z]{1,}'?-?[a-zA-Z]{2,}\s?([a-zA-Z]{1,})?)" ></asp:RegularExpressionValidator>
                        <asp:RequiredFieldValidator id="RequiredFieldValidator_lastName" runat="server" ControlToValidate="tb_lastName" ErrorMessage="Last name is required!" Forecolor="Red" SetFocusOnError="True" />
                    </td>
                </tr>

                <tr>
                    <td class="auto-style2">
                        <asp:Label ID="lbl_creditCard" runat="server" Text="Credit Card:"></asp:Label>
                    </td>
                    <td class="auto-style3">
                        <asp:TextBox ID="tb_creditCard" runat="server" Width="342px"></asp:TextBox>
                        <asp:RequiredFieldValidator id="RequiredFieldValidator_creditCard" runat="server" ControlToValidate="tb_creditCard" ErrorMessage="Credit card number is required." Forecolor="Red" SetFocusOnError="True" />
                    </td>
                </tr>

                <tr>
                    <td class="auto-style2">
                        <asp:Label ID="lbl_emailAddress" runat="server" Text="Email Address:"></asp:Label>
                    </td>
                    <td class="auto-style3">
                        <asp:TextBox ID="tb_emailAddress" runat="server" Width="342px"></asp:TextBox>
                        <asp:RequiredFieldValidator id="RequiredFieldValidator_emailAddress" runat="server" ControlToValidate="tb_emailAddress" ErrorMessage="Email address is required." Forecolor="Red" SetFocusOnError="True" />
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator_emailAddress" runat="server" ControlToValidate="tb_emailAddress" ForeColor="Red" ValidationExpression="^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$" Display="Dynamic" ErrorMessage="Invalid email address"/>
                    </td>
                    <td class="auto-style2">
                        <asp:Label ID="lbl_xss_email" runat="server"></asp:Label>
                    </td>
                </tr>

                <tr>
                    <td class="auto-style2">
                        <asp:Label ID="lbl_password" runat="server" Text="Password:"></asp:Label>
                    </td>
                   
                    <td class="auto-style3">
                        <asp:TextBox ID="tb_password" runat="server" Width="342px" Textmode="Password" onkeyup="javascript:validate()"></asp:TextBox>
                        <asp:RequiredFieldValidator id="RequiredFieldValidator_password" runat="server" ControlToValidate="tb_password" ErrorMessage="Password is required!" Forecolor="Red" SetFocusOnError="True" Display="Dynamic"/>
                    </td>

<%--                    <td class="auto-style2">
                        <asp:Label ID="lbl_display" runat="server" Text="XSS_Test_Password:"></asp:Label>
                    </td>--%>

                    <%--<td>
                        <asp:Label ID="lbl_pwdChecker" runat="server" Text="pwdChecker"></asp:Label>
                    </td>--%>
<%--                    <td>
                        <asp:Label ID="lbl_error" runat="server" Text="Validator Warning"></asp:Label>
                    </td>--%>
                </tr>

                <tr>
                    <td>

                    </td>
                    <td>
                        <asp:Label ID="lbl_pwdChecker" runat="server" Text="pwdChecker"></asp:Label>
                    </td>

                </tr>

                <tr>
                    <td class="auto-style2">
                        <asp:Label ID="lbl_password_confirm" runat="server" Text="Confirm Password:"></asp:Label>
                    </td>
                    <td class="auto-style3">
                        <asp:TextBox ID="tb_password_confirm" runat="server" Width="342px" Textmode="Password"></asp:TextBox>
                        <asp:RequiredFieldValidator id="RequiredFieldValidator_passwordConfirmation" runat="server" ControlToValidate="tb_password_confirm" ErrorMessage="Password confirmation is required!" Forecolor="Red" SetFocusOnError="True" Display="Dynamic" />
                        <asp:CompareValidator id="CompareValidator" runat="server" ControlToCompare="tb_password" ControlToValidate="tb_password_confirm" ErrorMessage="Your passwords do not match up!" Forecolor="Red" Display="Dynamic" />
                    </td>
                </tr>

                <tr>
                    <td class="auto-style2">
                        <asp:Label ID="lbl_dateOfBirth" runat="server" Text="Date of Birth:"></asp:Label>
                    </td>
                    <td class="auto-style3">
                        <asp:TextBox ID="tb_dateOfBirth" runat="server" Width="342px"></asp:TextBox>
                        <asp:CompareValidator ErrorMessage="(mm/dd/yyyy)"  Forecolor="Red" Display="Dynamic" ID="CompareValidator1" ControlToValidate="tb_dateOfBirth" Operator="DataTypeCheck" Type="Date" runat="server"></asp:CompareValidator>  
                        <asp:RangeValidator ID="valrDate" runat="server" ControlToValidate="tb_dateOfBirth" MinimumValue="01/01/2002" MaximumValue="02/05/2020" Type="Date" text="Invalid Birth Date (mm/dd/yyyy)" Forecolor="Red" Display="Dynamic"/>                               
                        <asp:RequiredFieldValidator id="RequiredFieldValidator_dateOfBirth" runat="server" ControlToValidate="tb_dateOfBirth" ErrorMessage="Date of Birth is required! (mm/dd/yyyy)" Forecolor="Red" SetFocusOnError="True" />
                    </td>
                </tr>

                <%--<tr>
                    <td class="auto-style2">&nbsp;</td>
                    <td class="auto-style3">
                        <asp:Button ID="btn_Check" runat="server" OnClick="btn_checkPassword_Click" Text="Check Password" Width="351px" />
                    </td>
                    <td>&nbsp;</td>
                </tr>--%>

                <tr>
                    <td class="auto-style2">&nbsp;</td>
                    <td class="auto-style3">
                        &nbsp;</td>
                    <td>&nbsp;</td>
                </tr>

<%--                <tr>
                    <td>Verification Code</td>
                    <td>
                        <asp:Image ID="captcha_image" runat="server" Height="55px" ImageUrl="~/Captcha.aspx" Width="186px" />
                        <br />
                        <asp:Label runat="server" ID="lblCaptchaMessage"></asp:Label>
                    </td>
                </tr>

                <tr>
                    <td>Enter Verification Code</td>
                    <td>
                        <asp:Textbox runat="server" ID="txtVerificationCode"></asp:Textbox>
                        <asp:RequiredFieldValidator id="RequiredFieldValidator_captcha" runat="server" ControlToValidate="txtVerificationCode" ErrorMessage="This is a required field!" Forecolor="Red" SetFocusOnError="True" />
                    </td>
                </tr>--%>

                <tr>
                    <td class="auto-style2">&nbsp;</td>
                    <td class="auto-style3">
                        <asp:Button ID="btn_Check" runat="server" OnClick="btn_checkPassword_Click" Text="Submit" Width="351px" />
                    </td>
                    <td>&nbsp;</td>
                </tr>

                <tr>
                    <td class="auto-style2">&nbsp;</td>
                    <td class="auto-style3">
                        &nbsp;</td>
                    <td>&nbsp;</td>
                </tr>

                <tr>
                    <td class="auto-style2">&nbsp;</td>
                    <td class="auto-style3">
                        <asp:Label ID="lbl_exceptionError" runat="server"></asp:Label>
                    </td>
                    <td>&nbsp;</td>
                </tr>

                <%--<tr>
                    <td>

                    </td>
                    <td>
                        <asp:Button runat="server" ID="Button1" Text="Submit" OnClick="btnSubmit_Click" />
                    </td>
                </tr>--%>

                <tr>
                    <td class="auto-style2">&nbsp;</td>
                    <td class="auto-style3">
                        &nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
<%--                <tr>
                    <td class="auto-style2">&nbsp;</td>
                    <td class="auto-style3">
                        <asp:Button ID="btn_Submit" runat="server" OnClick="btn_submit_Click" Text="Submit" Width="351px" />
                    </td>
                    <td>&nbsp;</td>
                </tr>--%>
<%--                <tr>
                    <td class="auto-style2">&nbsp;</td>
                    <td class="auto-style3">
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Password Complexity - Weak" ControlToValidate="tb_password" ValidationExpression="^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[$@$!%*?&])[A-Zaz\d$@$!%*?&]{8,10}"></asp:RegularExpressionValidator>
                    </td>
                    <td>&nbsp;</td>
                </tr>--%>
            </table>
        </div>
    </form>
    <script type="text/javascript">
        $("#<%=tb_creditCard.ClientID%>").inputmask({
                mask: "9999 9999 9999 9999",
                placeholder: ""
        });
    </script>
</body>
</html>
