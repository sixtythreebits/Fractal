<%@ Page Title="" Language="C#" MasterPageFile="~/Dashboard/DashboardMaster.master" AutoEventWireup="true" CodeBehind="Profile.aspx.cs" Inherits="Fractal.Dashboard.Profile" %>
<%@ MasterType VirtualPath="~/Dashboard/DashboardMaster.master" %>
<%@ Register src="~/UserControls/SuccessErrorControl.ascx" tagname="SuccessErrorControl" tagprefix="uc1" %>
<%@ Import Namespace="Core" %>
<asp:Content ID="Content1" ContentPlaceHolderID="DashboardHeadPlaceHolder" runat="server">
<%= Plugins.GetForms() %>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="DashboardContentPlaceHolder" runat="server">

   <h2 class="form">პროფილი</h2>
   <div class="form cl">
        <div class="left">
            <h4>პერსონალური ინფორმაცია</h4>                
            <div class="validation-email">
                <label>ელ-ფოსტა</label>
                <asp:TextBox ID="EmailTextBox" runat="server" ViewStateMode="Disabled" MaxLength="100" ClientIDMode="Static" CssClass="readonly" ReadOnly="true"></asp:TextBox>
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

            <asp:Button ID="SaveButton" runat="server" ClientIDMode="Static" CssClass="btn big" Text="დამახსოვრება" OnClick="SaveButton_Click" />
        </div>
        <div class="right">
            <p>
                <span class="validation-email">თქვენი ელ-ფოსტა</span>
                <span class="validation-email error hidden"></span>                
                <span class="validation-fname">თქვენი სახელი</span>
                <span class="validation-fname error"></span>
                <span class="validation-lname">თქვენი გვარი</span>
                <span class="validation-lname error"></span>                
                <span>ქალაქი/სოფელი სადაც ცხოვრობთ</span>
            </p>                            
        </div>            
    </div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="DashboardScriptsPlaceHolder" runat="server">
<uc1:SuccessErrorControl ID="SuccessErrorControl1" runat="server" />
<asp:Literal ID="ScriptsLiteral" runat="server" ViewStateMode="Disabled"></asp:Literal>
</asp:Content>
