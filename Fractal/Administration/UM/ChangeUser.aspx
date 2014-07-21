<%@ Page Title="Change Your Profile" Language="C#" MasterPageFile="../MasterPage.master" AutoEventWireup="true" Inherits="management_ChangeUser" Codebehind="ChangeUser.aspx.cs" %>

<%@ MasterType TypeName="administration_MasterPage" %>

<%@ Register Assembly="DevExpress.Web.v14.1, Version=14.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div class="main-title">
	<span class="left"></span>
	<span class="right"></span>
</div>
<div class="container">
    <dx:ASPxComboBox ID="UsersCombo" runat="server" TextField="Name" ValueField="ID" AutoPostBack="true" Width="300px"
        OnSelectedIndexChanged="UsersCombo_SelectedIndexChanged" IncrementalFilteringMode="Contains" EnableCallbackMode="true" CallbackPageSize="30">
    </dx:ASPxComboBox>    
</div>

</asp:Content>

