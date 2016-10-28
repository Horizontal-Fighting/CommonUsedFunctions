<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="Interview1.Home" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:Label ID="Label1" runat="server" Text="Username"></asp:Label>
        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
        <br>
        <asp:Label ID="Label2" runat="server" Text="Pwd"></asp:Label>
         &nbsp&nbsp&nbsp&nbsp&nbsp; &nbsp&nbsp
        <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
        <br>
        <asp:Button ID="Button1" runat="server" Text="Login" OnClick="Button1_Click" />
    
        <br />
        <asp:Label ID="Label5" runat="server" Text="shop_id:"></asp:Label>
        <asp:Label ID="Label6" runat="server" Text="Label"></asp:Label>
        <br />
        <asp:GridView ID="GridView1" runat="server">
        </asp:GridView>
        <br />
        <br />
        <br />
        <br />
        <asp:Label ID="Label3" runat="server" Text="Label" Visible="False"></asp:Label>
        <br />
        <asp:Label ID="Label4" runat="server" Text="Label" Visible="False"></asp:Label>
    
    </div>
    </form>
</body>
</html>
