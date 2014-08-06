<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="signup.aspx.cs" Inherits="Fractal.signup" %>
<%@ MasterType VirtualPath="~/MasterPage.Master" %>
<%@ Register src="UserControls/SuccessErrorControl.ascx" tagname="SuccessErrorControl" tagprefix="uc1" %>
<%@ Import Namespace="Core" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%=Plugins.GetForms()%>
    <script src="/scripts/validation-2.0.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section class="cnt cl">
        <h2 class="form">რეგისტრაცია</h2>
        <div class="form cl">
            <div class="left">
                <h4>პერსონალური ინფორმაცია</h4>                
                <div class="validation-email">
                    <label>ელ-ფოსტა</label>
                    <asp:TextBox ID="EmailTextBox" runat="server" ViewStateMode="Disabled" MaxLength="100" ClientIDMode="Static"></asp:TextBox>
                </div>
                <div class="validation-email">
                    <label>პაროლი</label>
                    <asp:TextBox ID="PasswordTextBox" runat="server" TextMode="Password" ViewStateMode="Disabled" MaxLength="100" ClientIDMode="Static"></asp:TextBox>
                </div>
                <div class="validation-fname">
                    <label>სახელი</label>
                    <asp:TextBox ID="FnameTextBox" runat="server" ViewStateMode="Disabled" MaxLength="100" ClientIDMode="Static"></asp:TextBox>
                </div>
                <div class="validation-lname">
                    <label>გვარი</label>
                    <asp:TextBox ID="LnameTextBox" runat="server" ViewStateMode="Disabled" MaxLength="100" ClientIDMode="Static"></asp:TextBox>
                </div>
                <div>
                    <label>ქალაქი</label>                    
                    <div class="select">
                        <p></p>
                        <span></span>
                        <asp:DropDownList ID="CitiesCombo" runat="server" DataSourceID="CitiesDataSource" DataTextField="Name" DataValueField="ID" ClientIDMode="Static"></asp:DropDownList>
                          <asp:ObjectDataSource ID="CitiesDataSource" runat="server" SelectMethod="ListAny" CacheDuration="7200" TypeName="Core.Utilities.Any">
                            <SelectParameters>
                                <asp:Parameter Name="level" Type="Int32"  DefaultValue="1" />
                                <asp:Parameter Name="dcode" Type="Int32"  DefaultValue="5" />
                            </SelectParameters>
                        </asp:ObjectDataSource>
                    </div>
                </div>                            

                <asp:Button ID="FinishRegistrationButton" runat="server" ClientIDMode="Static" CssClass="btn big" Text="რეგისტრაცია" OnClick="FinishRegistrationButton_Click" />
            </div>
            <div class="right">
                <p>
                    <span class="validation-email">თქვენი ელ-ფოსტა</span>
                    <span class="validation-email error hidden"></span>
                    <span class="validation-password">თქვენი პაროლი</span>
                    <span class="validation-password error hidden"></span>
                    <span class="validation-fname">თქვენი სახელი</span>
                    <span class="validation-fname error"></span>
                    <span class="validation-lname">თქვენი გვარი</span>
                    <span class="validation-lname error"></span>                
                    <span>ქალაქი/სოფელი სადაც ცხოვრობთ</span>
                </p>                            
            </div>            
        </div>
    </section>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptsPlaceHolder" runat="server">
<uc1:SuccessErrorControl ID="SuccessErrorControl1" runat="server" />
<asp:Literal ID="ScriptsLiteral" runat="server" ViewStateMode="Disabled"></asp:Literal>
</asp:Content>
