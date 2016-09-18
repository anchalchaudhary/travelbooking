﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Admin.aspx.cs" Inherits="Admin" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <p>
            <asp:Label ID="lblExperience" runat="server" Text="Experience" ></asp:Label>
            <asp:TextBox ID="tbxExperience" runat="server" input type="Text" style="margin-left:5px"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidatorExperience" runat="server" controltovalidate="tbxExperience" ErrorMessage="This field is required" ForeColor="red"></asp:RequiredFieldValidator>
        </p>
        <p>
            <asp:Label ID="lblQualification" runat="server" Text="Qualification" ></asp:Label>
            <asp:TextBox ID="tbxQualification" runat="server" input type="Text" style="margin-left:5px"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidatorQualification" runat="server" controltovalidate="tbxQualification" ErrorMessage="This field is required" ForeColor="red"></asp:RequiredFieldValidator>
        </p>
        <p>
            <asp:Button ID="BtnAdminRegister" runat="server" Text="Register as Admin" OnClick="BtnAdminRegister_Click" />
        </p>
    
    </div>
    </form>
</body>
</html>