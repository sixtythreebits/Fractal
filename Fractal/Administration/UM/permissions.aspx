<%@ Page Title="Permissions" Language="C#" MasterPageFile="../MasterPage.master" AutoEventWireup="true" Inherits="management_UM_permissions" Codebehind="permissions.aspx.cs" Theme="DevEx" %>
<%@ MasterType TypeName="administration_MasterPage" %>
<%@ Register Assembly="DevExpress.Web.ASPxTreeList.v14.1, Version=14.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"    Namespace="DevExpress.Web.ASPxTreeList" TagPrefix="dx" %>
<%@ Register assembly="DevExpress.Web.v14.1, Version=14.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div class="main-title">
	<span class="left"></span>
	<a href="#" class="icon text add">Create New</a>	
    <a href="#" class="icon text expand">Expand All</a>
    <a href="#" class="icon text collapse">Collapse All</a>
	<span class="right"></span>
</div>
<div class="container">
    <dx:ASPxTreeList ID="ActionsTree" ClientInstanceName="ActionsTree" runat="server" KeyFieldName="ID" ParentFieldName="ParentID" Width="100%" DataSourceID="ActionsDataSource">
        <Columns>            
            <dx:TreeListTextColumn FieldName="Caption" Caption="Permission" Width="300px">
                <PropertiesTextEdit MaxLength="100">
                    <ValidationSettings>
                        <RequiredField IsRequired="true" ErrorText=" " />
                    </ValidationSettings>
                </PropertiesTextEdit>                
            </dx:TreeListTextColumn>
            <dx:TreeListTextColumn FieldName="AspxPagePath" Caption="Page Name" Width="300px">
                <PropertiesTextEdit MaxLength="100"></PropertiesTextEdit>
            </dx:TreeListTextColumn>
            <dx:TreeListTextColumn FieldName="ControlID" Caption="Control Name" Width="300px">
                <PropertiesTextEdit MaxLength="100"></PropertiesTextEdit>
            </dx:TreeListTextColumn>
            <dx:TreeListTextColumn FieldName="Code" Caption="Code" Width="100px">
                <PropertiesTextEdit MaxLength="50"></PropertiesTextEdit>
            </dx:TreeListTextColumn>            
            <dx:TreeListTextColumn FieldName="Icon" Caption="Icon File Name" Width="150px">
                <PropertiesTextEdit MaxLength="100"></PropertiesTextEdit>
            </dx:TreeListTextColumn>
            <dx:TreeListSpinEditColumn FieldName="SortVal" Caption="Sort Val." Width="50px">
                <CellStyle HorizontalAlign="Right"></CellStyle>
                <PropertiesSpinEdit AllowMouseWheel="false">
                    <SpinButtons ShowIncrementButtons="false"></SpinButtons>
                    <Style HorizontalAlign="Right"></Style>
                </PropertiesSpinEdit>
            </dx:TreeListSpinEditColumn>            
            <dx:TreeListTextColumn FieldName="PermissionCode" Caption="Permission Code" Width="100px">
                <PropertiesTextEdit MaxLength="40"></PropertiesTextEdit>
            </dx:TreeListTextColumn>
            <dx:TreeListCommandColumn ButtonType="Image" Width="60px"  Caption=" ">
                <CellStyle HorizontalAlign="Center"></CellStyle>
                <NewButton Visible="true">
                    <Image Url="~/plugins/icons/img/add.png"></Image>
                </NewButton>
                <EditButton Visible="True">
                    <Image Url="~/plugins/icons/img/edit.png"></Image>
                </EditButton>
                <UpdateButton Visible="true">
                    <Image Url="~/plugins/icons/img/update.png"></Image>
                </UpdateButton>
                <CancelButton Visible="true">
                    <Image Url="~/plugins/icons/img/cancel.png"></Image>
                </CancelButton>
                <DeleteButton Visible="True">
                    <Image Url="~/plugins/icons/img/delete.png"></Image>
                </DeleteButton>
            </dx:TreeListCommandColumn>
            <dx:TreeListDataColumn>
                <EditCellTemplate></EditCellTemplate>
            </dx:TreeListDataColumn>
        </Columns>            
        <Settings GridLines="Both" />   
        <SettingsBehavior AllowFocusedNode="true" />
        <SettingsEditing AllowNodeDragDrop="true" />
    </dx:ASPxTreeList>

    <asp:ObjectDataSource ID="ActionsDataSource" runat="server" 
        DeleteMethod="TSP_Permission" InsertMethod="TSP_Permission" SelectMethod="ListPermissions" 
        TypeName="Core.Permission" UpdateMethod="TSP_Permission">
        <DeleteParameters>
            <asp:Parameter Name="iud" Type="Byte" DefaultValue="2" />
            <asp:Parameter Name="ID" Type="Int32" />
            <asp:Parameter Name="ParentID" Type="Int32" />
            <asp:Parameter Name="Caption" Type="String" />
            <asp:Parameter Name="AspxPagePath" Type="String" />
            <asp:Parameter Name="ControlID" Type="String" />
            <asp:Parameter Name="Code" Type="String" />
            <asp:Parameter Name="DCode" Type="Int32" />
            <asp:Parameter Name="Icon" Type="String" />
            <asp:Parameter Name="SortVal" Type="Int32" />
            <asp:Parameter Name="PermissionCode" Type="String" />
            <asp:Parameter Name="IncludesHelp" Type="Boolean" />
            <asp:Parameter Name="HelpUrl" Type="String" />
        </DeleteParameters>
        <InsertParameters>
            <asp:Parameter Name="iud" Type="Byte" DefaultValue="0" />
            <asp:Parameter Name="ID" Type="Int32" />
            <asp:Parameter Name="ParentID" Type="Int32" />
            <asp:Parameter Name="Caption" Type="String" />
            <asp:Parameter Name="AspxPagePath" Type="String" />
            <asp:Parameter Name="ControlID" Type="String" />
            <asp:Parameter Name="Code" Type="String" />
            <asp:Parameter Name="DCode" Type="Int32" />
            <asp:Parameter Name="Icon" Type="String" />
            <asp:Parameter Name="SortVal" Type="Int32" />
            <asp:Parameter Name="PermissionCode" Type="String" />
            <asp:Parameter Name="IncludesHelp" Type="Boolean" />
            <asp:Parameter Name="HelpUrl" Type="String" />
        </InsertParameters>
        <SelectParameters>
            <asp:Parameter Name="dcode" Type="Int32" />            
        </SelectParameters>
        <UpdateParameters>
            <asp:Parameter Name="iud" Type="Byte" DefaultValue="1" />
            <asp:Parameter Name="ID" Type="Int32" />
            <asp:Parameter Name="ParentID" Type="Int32" />
            <asp:Parameter Name="Caption" Type="String" />
            <asp:Parameter Name="AspxPagePath" Type="String" />
            <asp:Parameter Name="ControlID" Type="String" />
            <asp:Parameter Name="Code" Type="String" />
            <asp:Parameter Name="DCode" Type="Int32" />
            <asp:Parameter Name="Icon" Type="String" />
            <asp:Parameter Name="SortVal" Type="Int32" />
            <asp:Parameter Name="PermissionCode" Type="String" />
            <asp:Parameter Name="IncludesHelp" Type="Boolean" />
            <asp:Parameter Name="HelpUrl" Type="String" />
        </UpdateParameters>
    </asp:ObjectDataSource>

</div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptsPlaceHolder" Runat="Server">
<script type="text/javascript">
$(document).ready(function (e) {
    $(".icon.add").click(function (e) {        
        ActionsTree.StartEditNewNode();
        return false;
    });

    ActionsTree.ExpandNode(ActionsTree.GetVisibleNodeKeys()[0]);

    $(".icon.expand").click(function () {
        ActionsTree.ExpandAll();
        return false;
    });

    $(".icon.collapse").click(function () {        
        ActionsTree.CollapseAll();
        return false;
    });
});
</script>    
</asp:Content>