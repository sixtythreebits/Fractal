<%@ Page Title="" Language="C#" MasterPageFile="~/Administration/PopupPages/MasterPage.master" AutoEventWireup="true" Inherits="PopupPages_Administration_CM_CreateNewCourseKeys" Codebehind="CreateNewCourseKeys.aspx.cs" %>
<%@ MasterType VirtualPath="~/Administration/PopupPages/MasterPage.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">    
<asp:Literal ID="HeadLiteral" runat="server" EnableViewState="false"></asp:Literal>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div class="pop-menu">
    <asp:LinkButton ID="SaveButton" runat="server" CssClass="icon text save" Text="შენახვა" OnClick="SaveButton_Click"></asp:LinkButton>	
</div>
<div class="form group">
	<label>ჯგუფის დასახელება</label>
    <asp:TextBox ID="CaptionTextBox" runat="server" CssClass="custom-form-input" MaxLength="50" ClientIDMode="Static"></asp:TextBox>	
	<span>გასაღებების ჯგუფის დასახელება.</span>
</div>
<div class="form months short">	
    <label>თვე</label>
    <asp:TextBox ID="MonthsTextBox" runat="server" CssClass="custom-form-input" ClientIDMode="Static" MaxLength="2"></asp:TextBox>	
    <span>გააქტიურებიდან რამდენი თვე იყოს ვალიდური</span>		
</div>
<asp:PlaceHolder ID="KeyCountPlaceHolder" runat="server" EnableViewState="false">
<div class="form key short">
	<label>რაოდენობა</label>	
    <asp:TextBox ID="KeyCountTextBox" runat="server" CssClass="custom-form-input" MaxLength="4" ClientIDMode="Static"></asp:TextBox>	
    <span>რამდენი ცალი გასაღები დაგენერირდეს</span>		
</div>
</asp:PlaceHolder>
</asp:Content>
<asp:Content ID="Content3" runat="server" ContentPlaceHolderID="ScriptsPlaceHolder">
<script type="text/javascript">
var RequiredGroupName;
var GroupNameError = null;

$(document).ready(function () {
    RequiredGroupName = $("#HFRequiredGroupName").val();
    
    $("#MonthsTextBox").numericInput({
        allowNegative: false,
        allowFloat: false
    });

    $('#MonthsTextBox').keyup(function (e) {
        var val = parseInt($(this).val());
        if (val < 1 || isNaN(val)) {
            $(this).val('1');
        }
    });
    
    $("#KeyCountTextBox").numericInput({
        allowNegative: false,
        allowFloat: false
    });

    $("#KeyCountTextBox").keyup(function (e) {
        var val = parseInt($(this).val());        
        if (val < 1 || isNaN(val)) {
            $(this).val('1');
        }
    });
    
    $(".icon-text.save").click(function (e) {

        if (IsFormValid()) {
            GMLoader.open();
        }
        else {
            return false;
        }
    });

});
</script>
</asp:Content>