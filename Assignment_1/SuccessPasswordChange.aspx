<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SuccessPasswordChange.aspx.cs" Inherits="Assignment_1.SuccessPasswordChange" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <p>Password is changed successfully!</p>
            <br />
            <asp:Button runat="server" ID="btn_Login" Text="Login" OnClick="btnLogin_Click" />
        </div>
    </form>
</body>
</html>
