<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="forms.aspx.cs" Inherits="Fractal.forms" %>
<%@ Import Namespace="Core" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<%=Plugins.GetForms() %>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section class="cnt cl">
        <h2 class="form">რეგისტრაცია</h2>
        <div class="form cl">
            <div class="left">
                <h4>პერსონალური ინფორმაცია</h4>
                <div>
                    <label>მომხმარებელი</label>
                    <input type="text" value="ios" class="readonly" readonly />
                </div>
                <div class="validation-email">
                    <label>ელ-ფოსტა</label>
                    <input name="ctl00$ctl00$ContentPlaceHolder1$DashboardContentPlaceHolder$EmailTextBox" type="text" value="ios@user.com" maxlength="60" id="EmailTextBox" />
                </div>
                <div class="validation-fname">
                    <label>სახელი</label>
                    <input name="ctl00$ctl00$ContentPlaceHolder1$DashboardContentPlaceHolder$FnameTextBox" type="text" value="Apple" maxlength="30" id="FnameTextBox" />
                </div>
                <div class="validation-lname">
                    <label>გვარი</label>
                    <input name="ctl00$ctl00$ContentPlaceHolder1$DashboardContentPlaceHolder$LnameTextBox" type="text" value="User" maxlength="30" id="LnameTextBox" />
                </div>
                <div>
                    <label>სქესი</label>
                    <div class="radio">
                        <input id="MaleRadio" type="radio" name="ctl00$ctl00$ContentPlaceHolder1$DashboardContentPlaceHolder$gender" value="MaleRadio" /><label for="MaleRadio">კაცი</label>
                        <input id="FemaleRadio" type="radio" name="ctl00$ctl00$ContentPlaceHolder1$DashboardContentPlaceHolder$gender" value="FemaleRadio" checked="checked" /><label for="FemaleRadio">ქალი</label>                    
                    </div>
                </div>
            
                <h4>College</h4>
                <div class="validation-college">
                    <label>College/University</label>
                    <input name="ctl00$ctl00$ContentPlaceHolder1$DashboardContentPlaceHolder$CollegeTextBox" type="text" value="University of Manchester" maxlength="50" id="CollegeTextBox" />                    
                </div>
            
            
                <div>
                    <label>სქესი</label>
                    <div class="select">
                        <p>asd</p>
                        <span></span>
                        <select>
                            <option>1</option>
                            <option>1</option>
                        </select>
                    </div>
                </div>

                <input type="submit" name="ctl00$ctl00$ContentPlaceHolder1$DashboardContentPlaceHolder$SaveButton" value="დამახსოვრება" id="SaveButton" class="btn big" />
            </div>
            <div class="right">
                <p>
                    <span class="validation-username">Your username</span>
                    <span class="validation-username error hidden"></span>
                    <span class="validation-email">Your email, also used as username</span>
                    <span class="validation-email error hidden"></span>
                    <span class="validation-fname">Your First Name</span>
                    <span class="validation-fname error"></span>
                    <span class="validation-lname">Your Last Name</span>
                    <span class="validation-lname error"></span>                
                    <span class="validation-gender">Your Last Name</span>
                </p>
            
                <p>
                    <span class="validation-college">Your College/University</span>
                    <span class="validation-college error hidden"></span>
                </p>
            
            
            </div>
            <input type="hidden" name="ctl00$ctl00$ContentPlaceHolder1$DashboardContentPlaceHolder$HFRequiredFname" id="HFRequiredFname" value="First name is required." />
            <input type="hidden" name="ctl00$ctl00$ContentPlaceHolder1$DashboardContentPlaceHolder$HFRequiredLname" id="HFRequiredLname" value="Last name is required." />
            <input type="hidden" name="ctl00$ctl00$ContentPlaceHolder1$DashboardContentPlaceHolder$HFRequiredEmail" id="HFRequiredEmail" value="Email is required." />
            <input type="hidden" name="ctl00$ctl00$ContentPlaceHolder1$DashboardContentPlaceHolder$HFRequiredCollege" id="HFRequiredCollege" value="You college/university is required." />
            <input type="hidden" name="ctl00$ctl00$ContentPlaceHolder1$DashboardContentPlaceHolder$HFEmailFormatValidation" id="HFEmailFormatValidation" value="Email format is incorrect." />
            <input type="hidden" name="ctl00$ctl00$ContentPlaceHolder1$DashboardContentPlaceHolder$HFUniqEmail" id="HFUniqEmail" value="Sorry, this email is already taken." />
            <input type="hidden" name="ctl00$ctl00$ContentPlaceHolder1$DashboardContentPlaceHolder$HFRegexEmail" id="HFRegexEmail" value="^([A-Za-z0-9_\-\.])+\@([A-Za-z0-9_\-\.])+\.([A-Za-z]{2,4})$" />       
        </div>
    </section>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptsPlaceHolder" runat="server">
</asp:Content>
