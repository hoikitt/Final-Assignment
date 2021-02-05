<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SuccessRegistration.aspx.cs" Inherits="Assignment_1.SuccessRegistration" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <p>Registration Success!</p>
            <asp:Label ID="lbl_encoded_text" runat="server" Text="Encoded Email: "></asp:Label><asp:Label ID="lbl_display" runat="server" Text="Display"></asp:Label>
            <br />
            <br />
            <asp:Button runat="server" ID="btn_Login" Text="Login" OnClick="btnLogin_Click" />
        </div>
    </form>
</body>
</html>


