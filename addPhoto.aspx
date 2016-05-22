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
        <asp:DropDownList ID="ddlCategory" runat="server" DataSourceID="SqlDataSourceGetCategories" DataTextField="catName" DataValueField="catId" AutoPostBack="True">
        

        </asp:DropDownList>
            Cinsiyet:
        <asp:DropDownList ID="ddlGender" runat="server" AutoPostBack="True" DataSourceID="SqlDataSourceGetGender" DataTextField="genderType" DataValueField="genderId">
            <asp:ListItem>Erkek</asp:ListItem>
            <asp:ListItem>Kadın</asp:ListItem>
        </asp:DropDownList>

            <asp:Button ID="btnUpload" runat="server" Text="Kaydet" OnClick="btnUpload_Click" />

            <asp:Label ID="lblSuccess" Style="color: green;" runat="server" Text="Durum: "></asp:Label>
            <br />
            <br />
            url of clothes:
        <asp:TextBox ID="tbUrl" runat="server"></asp:TextBox>
            <asp:SqlDataSource ID="SqlDataSourceGetCategories" runat="server" ConnectionString="<%$ ConnectionStrings:wiDatabaseConnectionString %>" SelectCommand="SELECT * FROM [wiCategory]"></asp:SqlDataSource>
            <asp:SqlDataSource ID="SqlDataSourceGetGender" runat="server" ConnectionString="<%$ ConnectionStrings:wiDatabaseConnectionString %>" SelectCommand="SELECT * FROM [wiGender]"></asp:SqlDataSource>
        </div>
    </form>
</body>
</html>
