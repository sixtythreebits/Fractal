<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="PasswordReset.aspx.cs" Inherits="Fractal.PasswordReset" %>
<%@ MasterType VirtualPath="~/MasterPage.Master" %>
<%@ Register src="UserControls/SuccessErrorControl.ascx" tagname="SuccessErrorControl" tagprefix="uc1" %>
<%@ Import Namespace="Core" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">    
<%=Plugins.GetForms() %>
<script type="text/javascript" src="/scripts/validation-2.0.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<section class="cnt cl">
    <h2 class="form">პაროლის განახლება</h2>
    <div class="form cl">
        <div class="left">                
            <h4>&nbsp;</h4>
            <div>
                <label>პაროლი</label>
                <asp:TextBox ID="PasswordTextBox" runat="server" ClientIDMode="Static" TextMode="Password"></asp:TextBox>
            </div>
            <div class="validation-email">
                <label>კიდევ ერთხელ</label>
                <asp:TextBox ID="RePasswordTextBox" runat="server" ClientIDMode="Static" TextMode="Password"></asp:TextBox>                
            </div>
            <asp:Button ID="FinishResetPasswordButton" runat="server" CssClass="btn big" Text="განახლება" OnClick="FinishResetPasswordButton_Click" />
        </div>
        <div class="right">
            <p>
                <span class="validation-password">შეიტანეთ ახალი პაროლი</span>
                <span class="validation-password error hidden"></span>
                <span class="validation-repassword">გაიმეროთ ახალი პაროლი</span>
                <span class="validation-repassword error hidden"></span>
            </p>
        </div>
    </div>
</section>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptsPlaceHolder" runat="server">
<uc1:SuccessErrorControl ID="SuccessErrorControl1" runat="server" />
<asp:Literal ID="ScriptsLiteral" runat="server" ViewStateMode="Disabled"></asp:Literal>
</asp:Content>
