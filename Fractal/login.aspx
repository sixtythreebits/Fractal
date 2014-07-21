<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="Fractal.login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table>
            <tr>
                <td>Username:</td>
                <td><asp:TextBox ID="UsernameTextBox" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td>Password:</td>
                <td><asp:TextBox ID="PasswordTextBox" TextMode="Password" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td>
                    
                </td>
                <td><asp:Button ID="LoginButton" runat="server" Text="შესვლა" OnClick="LoginButton_Click" /></td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Label ID="ErrorLabel" runat="server" ForeColor="Red" Text="არასწორი სახელი ან პაროლი" Visible="false"></asp:Label>
                </td>
            </tr>
        </table>    
    </div>
    </form>
</body>
</html>
