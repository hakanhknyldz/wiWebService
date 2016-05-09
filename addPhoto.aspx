<%@ Page Language="C#" AutoEventWireup="true" CodeFile="addPhoto.aspx.cs" Inherits="addPhoto" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <h3>Photo Upload Page</h3>

        <asp:FileUpload ID="FileUploadPhoto" runat="server" />
        Kategori:
        <asp:DropDownList ID="ddlCategory" runat="server" AutoPostBack="False">
            <asp:ListItem>gömlek</asp:ListItem>
            <asp:ListItem>t-shirt</asp:ListItem>
            <asp:ListItem>sweatshirt</asp:ListItem>
            <asp:ListItem>kazak</asp:ListItem>
            <asp:ListItem>mont</asp:ListItem>
            <asp:ListItem>şort</asp:ListItem>
            <asp:ListItem>elbise</asp:ListItem>
            <asp:ListItem>etek</asp:ListItem>
            <asp:ListItem>bluz</asp:ListItem>
            
        </asp:DropDownList>
        Cinsiyet:
        <asp:DropDownList ID="ddlGender" runat="server" AutoPostBack="False">
            <asp:ListItem>Erkek</asp:ListItem>
            <asp:ListItem>Kadın</asp:ListItem>
        </asp:DropDownList>

        <asp:Button ID="btnUpload" runat="server" Text="Kaydet" OnClick="btnUpload_Click" />

        <asp:Label ID="lblSuccess" style="color:green;"  runat="server" Text="Durum: "></asp:Label>
        <br />
        <br />
        url of clothes:
        <asp:TextBox ID="tbUrl" runat="server"></asp:TextBox>
    </div>
    </form>
</body>
</html>
