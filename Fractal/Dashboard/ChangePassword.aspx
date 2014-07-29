<%@ Page Title="" Language="C#" MasterPageFile="~/Dashboard/DashboardMaster.master" AutoEventWireup="true" CodeBehind="ChangePassword.aspx.cs" Inherits="Fractal.Dashboard.ChangePassword" %>
<asp:Content ID="Content1" ContentPlaceHolderID="DashboardHeadPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="DashboardContentPlaceHolder" runat="server">

    <h2 class="form">პაროლის შეცვლა</h2>

    <div class="form">
        <div class="left">
            <h4>&nbsp;</h4>
            <div class="validation-current-password">
                <label>პაროლი</label>
                <input name="ctl00$ctl00$ContentPlaceHolder1$DashboardContentPlaceHolder$CurrentPasswordTextBox" type="password" maxlength="50" id="CurrentPasswordTextBox" />
            </div>
            <div class="validation-password">
                <label>ახალი პაროლი</label>
                <input name="ctl00$ctl00$ContentPlaceHolder1$DashboardContentPlaceHolder$PasswordTextBox" type="password" maxlength="50" id="PasswordTextBox" />
            </div>
            <div class="validation-repassword">
                <label>გაიმეორეთ პაროლი</label>
                <input name="ctl00$ctl00$ContentPlaceHolder1$DashboardContentPlaceHolder$PasswordRetypeTextBox" type="password" maxlength="50" id="PasswordRetypeTextBox" />
            </div>
            <input type="submit" name="ctl00$ctl00$ContentPlaceHolder1$DashboardContentPlaceHolder$ChangePasswordButton" value="დამახსოვრება" id="ChangePasswordButton" class="btn big" />
        </div>
        <div class="right">
            <p>
                <span class="validation-current-password">ჩაწერეთ ძველი პაროლი</span>
                <span class="validation-current-password error hidden"></span>
                <span class="validation-password">At least 6 characters long</span>
                <span class="validation-password error hidden"></span>
                <span class="validation-repassword">Verification</span>
                <span class="validation-repassword error hidden"></span>
            </p>
        </div>
        <input type="hidden" name="ctl00$ctl00$ContentPlaceHolder1$DashboardContentPlaceHolder$HFRequiredCurrentPassword" id="HFRequiredCurrentPassword" value="Current password is required." />
        <input type="hidden" name="ctl00$ctl00$ContentPlaceHolder1$DashboardContentPlaceHolder$HFWrongPassword" id="HFWrongPassword" value="Password is incorrect." />
        <input type="hidden" name="ctl00$ctl00$ContentPlaceHolder1$DashboardContentPlaceHolder$HFRequiredPassword" id="HFRequiredPassword" value="Password is required." />
        <input type="hidden" name="ctl00$ctl00$ContentPlaceHolder1$DashboardContentPlaceHolder$HFPasswordMinimumcharacters" id="HFPasswordMinimumcharacters" value="At least 6 characters." />
        <input type="hidden" name="ctl00$ctl00$ContentPlaceHolder1$DashboardContentPlaceHolder$HFRequiredRetypePassword" id="HFRequiredRetypePassword" value="Password fields doesn&#39;t match." />
    </div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="DashboardScriptsPlaceHolder" runat="server">
</asp:Content>
