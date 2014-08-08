<%@ Page Title="" Language="C#" MasterPageFile="~/Administration/PopupPages/MasterPage.master" AutoEventWireup="true" Inherits="PopupPages_subscriptions" Codebehind="subscriptions.aspx.cs" %>
<%@ MasterType VirtualPath="~/Administration/PopupPages/MasterPage.master" %>
<%@ Register Assembly="DevExpress.Web.v14.1, Version=14.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxCallback" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v14.1, Version=14.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">    

<div class="pop-menu">
	<a href="#" class="icon text save">შენახვა</a>
    <a href="#" class="icon text delete-black hidden">მონიშნულის წაშლა</a>
</div>    

     <table style="margin:10px 0px 0px 20px;">
        <tr>
            <td>
                მომხმარებელი
            </td>
            <td>
                წიგნი
            </td>
        </tr>
        <tr>
            <td>
                <dx:ASPxComboBox ID="UsersCombo" ClientInstanceName="UsersCombo" runat="server" Width="300px" IncrementalFilteringMode="Contains" EnableCallbackMode="true" CallbackPageSize="30"
                                 DataSourceID="UsersDataSource" TextField="UsernameFullName" ValueField="ID">
                    <ClientSideEvents ButtonClick="function(s,e){ AddSelectedUserToListBox(); }" />
                    <Buttons>
                        <dx:EditButton>
                            <Image Url="~/plugins/icons/img/add.png"></Image>
                        </dx:EditButton>
                    </Buttons>
                </dx:ASPxComboBox>
            </td>
            <td>
                <dx:ASPxComboBox ID="CoursesCombo" ClientInstanceName="CoursesCombo" runat="server" Width="300px"  IncrementalFilteringMode="Contains" EnableCallbackMode="true" CallbackPageSize="30"
                                 DataSourceID="CoursesDataSource" TextField="Caption" ValueField="ID">
                    <ClientSideEvents ButtonClick="function(s,e){ AddSelectedCourseToListBox(); }" />
                    <Buttons>
                        <dx:EditButton>
                            <Image Url="~/plugins/icons/img/add.png"></Image>
                        </dx:EditButton>
                    </Buttons>
                </dx:ASPxComboBox>
            </td>
        </tr>
        <tr>
            <td>
                <dx:ASPxListBox ID="UsersListBox" ClientInstanceName="UsersListBox" runat="server" Width="300px" Height="180px" SelectionMode="CheckColumn">
                    <ClientSideEvents SelectedIndexChanged="function(s,e) { ProcessRemoveButton(); }" />
                </dx:ASPxListBox>
            </td>
            <td>
                <dx:ASPxListBox ID="CoursesListBox" ClientInstanceName="CoursesListBox" runat="server" Width="300px" Height="180px"  SelectionMode="CheckColumn">
                    <ClientSideEvents SelectedIndexChanged="function(s,e) { ProcessRemoveButton(); }" />
                </dx:ASPxListBox>
            </td>
        </tr>
    </table>
    
    <table style="margin:10px 0px 0px 215px;">
        <tr>
            <td width="105">Expiration Date:</td>
            <td>
                <dx:ASPxDateEdit ID="ExpirationDate" runat="server" DisplayFormatString="MMM dd, yyyy hh:mm tt" EditFormatString="MMM dd, yyyy hh:mm tt">
                </dx:ASPxDateEdit>
            </td>
        </tr>
    </table>    
    <asp:ObjectDataSource ID="UsersDataSource" runat="server" SelectMethod="ListUsers" TypeName="Core.User"></asp:ObjectDataSource>
    <asp:ObjectDataSource ID="CoursesDataSource" runat="server" SelectMethod="ListCourses" TypeName="Core.Course"></asp:ObjectDataSource>

    <dx:ASPxCallback ID="Callbacker" ClientInstanceName="Callbacker" runat="server" OnCallback="Callbacker_Callback" >
        <ClientSideEvents  BeginCallback="function(s,e){ GMLoader.open(); }" CallbackComplete="function(s,e) { Callbacker_CallbackComplete(s,e) }" EndCallback="function(s,e){ GMLoader.close(); }" />
    </dx:ASPxCallback>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptsPlaceHolder" Runat="Server">    
<script>

    $(document).ready(function () {
        $(".delete-black").click(function (e) {
            RemoveSelectedItemsFromCheckBox();
            return false;
        });

        $(".save").click(function (e) {
            Callbacker.PerformCallback();
            return false;
        });
    });


    function AddSelectedUserToListBox() {
        var item = UsersCombo.GetSelectedItem();
        if (item != null && UsersListBox.FindItemByValue(item.value) == null) {
            UsersListBox.AddItem(item.text, item.value);
        }
    }

    function AddSelectedCourseToListBox() {
        var item = CoursesCombo.GetSelectedItem();
        if (item != null && CoursesListBox.FindItemByValue(item.value) == null) {
            CoursesListBox.AddItem(item.text, item.value);
        }
    }

    function ProcessRemoveButton() {
        if (((UsersListBox.GetSelectedItems().length + CoursesListBox.GetSelectedItems().length) > 0)) {
            $(".delete-black").Show();
        }
        else {
            $(".delete-black").Hide();
        }
    }

    function RemoveSelectedItemsFromCheckBox() {

        var items1 = UsersListBox.GetSelectedItems();
        var items2 = CoursesListBox.GetSelectedItems();

        while (items1.length > 0) {
            UsersListBox.RemoveItem(items1[0].index);
            items1 = UsersListBox.GetSelectedItems();
        }

        while (items2.length > 0) {
            CoursesListBox.RemoveItem(items2[0].index);
            items2 = CoursesListBox.GetSelectedItems();
        }

        ProcessRemoveButton();
    }

    function Callbacker_CallbackComplete(s, e) {
        window.parent.SubscriptionsGrid.Refresh();
        ClosePopup(true);
    }
</script>
</asp:Content>