<%@ Page Title="Roles" Language="C#" MasterPageFile="../MasterPage.master" AutoEventWireup="true" Inherits="management_UM_Roles" Theme="Office2010Silver" Codebehind="Roles.aspx.cs" %>
<%@ MasterType TypeName="administration_MasterPage" %>
<%@ Register Assembly="DevExpress.Web.v14.1, Version=14.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register assembly="DevExpress.Web.v14.1, Version=14.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<div class="main-title">
	<span class="left"></span>
	<a href="#" class="icon text add">Create New</a>    
	<span class="right"></span>
</div>
<div class="container static">
    <dx:ASPxGridView ID="RolesGrid" ClientInstanceName="RolesGrid" runat="server" KeyFieldName="ID" Width="100%" DataSourceID="RolesDataSource">
        <Columns>            
            <dx:GridViewDataTextColumn FieldName="Caption" Caption="Role" Width="200px">
                <PropertiesTextEdit MaxLength="50">
                    <ValidationSettings>
                        <RequiredField IsRequired="true" ErrorText=" " />
                    </ValidationSettings>
                </PropertiesTextEdit>
            </dx:GridViewDataTextColumn>            
            <dx:GridViewDataSpinEditColumn FieldName="Code" Caption="Code" Width="100px"> 
                <PropertiesSpinEdit>
                <SpinButtons ShowIncrementButtons="false"></SpinButtons>
                    <Style HorizontalAlign="Right"></Style>
                </PropertiesSpinEdit>
            </dx:GridViewDataSpinEditColumn>
            <dx:GridViewCommandColumn  Caption=" " ButtonType="Image" Width="90px" ShowEditButton="true" ShowUpdateButton="true" ShowCancelButton="true" ShowDeleteButton="true" ShowClearFilterButton="true" >
            </dx:GridViewCommandColumn>
            <dx:GridViewDataColumn>
                <EditItemTemplate></EditItemTemplate>
            </dx:GridViewDataColumn>
        </Columns>         
    </dx:ASPxGridView>

    <asp:ObjectDataSource ID="RolesDataSource" runat="server" DeleteMethod="TSP_Role" 
        InsertMethod="TSP_Role" SelectMethod="ListRoles" TypeName="Core.Role" UpdateMethod="TSP_Role">
        <DeleteParameters>
            <asp:Parameter Name="iud" Type="Byte" DefaultValue="2" />
            <asp:Parameter Name="ID" Type="Int32" />
            <asp:Parameter Name="Caption" Type="String" />
            <asp:Parameter Name="Code" Type="Byte" />
        </DeleteParameters>
        <InsertParameters>
            <asp:Parameter Name="iud" Type="Byte" DefaultValue="0" />
            <asp:Parameter Name="ID" Type="Int32" />
            <asp:Parameter Name="Caption" Type="String" />
            <asp:Parameter Name="Code" Type="Byte" />
        </InsertParameters>        
        <UpdateParameters>
            <asp:Parameter Name="iud" Type="Byte" DefaultValue="1" />
            <asp:Parameter Name="ID" Type="Int32" />
            <asp:Parameter Name="Caption" Type="String" />
            <asp:Parameter Name="Code" Type="Byte" />
        </UpdateParameters>
    </asp:ObjectDataSource>
</div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptsPlaceHolder" Runat="Server">
<script type="text/javascript">
    $(document).ready(function () {
        $(".icon.add").click(function (e) {            
            RolesGrid.AddNewRow();
            return false;
        });
    });
</script>
</asp:Content>