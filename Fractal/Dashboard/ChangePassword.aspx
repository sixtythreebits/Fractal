<%@ Page Title="" Language="C#" MasterPageFile="~/Dashboard/DashboardMaster.master" AutoEventWireup="true" CodeBehind="ChangePassword.aspx.cs" Inherits="Fractal.Dashboard.ChangePassword" %>
<%@ MasterType VirtualPath="~/Dashboard/DashboardMaster.master" %>
<%@ Import Namespace="Core" %>
<%@ Register src="../UserControls/SuccessErrorControl.ascx" tagname="SuccessErrorControl" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="DashboardHeadPlaceHolder" runat="server">
    <%=Plugins.GetForms() %>
<script type="text/javascript" src="/scripts/validation-2.0.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="DashboardContentPlaceHolder" runat="server">

    <h2 class="form">პაროლის შეცვლა</h2>

    <div class="form">
        <div class="left">
            <h4>&nbsp;</h4>
            <div class="validation-current-password">
                <label>პაროლი</label>
                <asp:TextBox ID="CurrentPasswordTextBox" runat="server" ClientIDMode="Static" TextMode="Password"></asp:TextBox>
            </div>
            <div class="validation-password">
                <label>ახალი პაროლი</label>
                <asp:TextBox ID="NewPasswordTextBox" runat="server" ClientIDMode="Static" TextMode="Password"></asp:TextBox>
            </div>
            <div class="validation-repassword">
                <label>გაიმეორეთ პაროლი</label>
                <asp:TextBox ID="RePasswordTextBox" runat="server" ClientIDMode="Static" TextMode="Password"></asp:TextBox>
            </div>
            <asp:Button ID="ChangePasswordButton" runat="server" CssClass="btn big" Text="დამახსოვრება" OnClick="ChangePasswordButton_Click" />            
        </div>
        <div class="right">
            <p>
                <span class="validation-current-password">შეიტანეთ მიმდინარე პაროლი</span>
                <span class="validation-current-password error hidden"></span>
                <span class="validation-password">შეიტანეთ ახალი პაროლი</span>
                <span class="validation-password error hidden"></span>
                <span class="validation-repassword">გაიმეორეთ ახალი პაროლი</span>
                <span class="validation-repassword error hidden"></span>
            </p>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="DashboardScriptsPlaceHolder" runat="server">
    <uc1:SuccessErrorControl ID="SuccessErrorControl1" runat="server" />
<asp:Literal ID="ScriptsLiteral" runat="server" ViewStateMode="Disabled"></asp:Literal>
</asp:Content>
