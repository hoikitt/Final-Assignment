﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Success.aspx.cs" Inherits="Assignment_1.Success" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<%--    <script>
        var start_time = 5;
        var timer = setInterval(function () {
            if (start_time <= 0) {
                clearInterval(timer);
            } else {
                document.getElementById("timer").innerHTML = start_time + " seconds remaining";
            }
            start_time -= 1;
        }, 1000);
        setTimeout(function () { window.location = "Login.aspx"; }, (start_time * 1000));
    </script>--%>
    <title></title>
    <style type="text/css">
        .auto-style1 {
            width: 100%;
        }
        .auto-style2 {
            width: 151px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table class="auto-style1">
                <tr>
                    <td class="auto-style2">
                        <asp:Label ID="lbl_user_profile" runat="server" Text="User Profile"></asp:Label>
                    </td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style2">&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style2">
                        <asp:Label ID="lbl_message" runat="server" EnableViewState="false" />
                    </td>
                </tr>
                <tr>
                    <td class="auto-style2">&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style2">
                        <asp:Label ID="lbl_user_id_display" runat="server" Text="User ID:"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lbl_user_id" runat="server" Text="lbl_userID"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style2">
                        <asp:Label ID="lbl_credit_card_display" runat="server" Text="Credit card:"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lbl_credit_card" runat="server" Text="lbl_credit_card"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style2">&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style2">
                        <asp:Button ID="btn_forgetPassword" runat="server" Text="Change password" onClick="btn_forgetPassword_Click"/>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style2">&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style2">
                        <asp:Button ID="btn_logout" runat="server" Text="Logout" onClick="LogoutMe" Visible="false" />
                    </td>             
                </tr>
                <tr>
                    <td class="auto-style2">
                        <asp:Label ID="lbl_timer" runat="server"/>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
