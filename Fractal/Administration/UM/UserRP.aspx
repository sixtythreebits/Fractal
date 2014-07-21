<%@ Page Title="User Role ↔ Permissions" Language="C#" MasterPageFile="../MasterPage.master" AutoEventWireup="true" Inherits="ManagementUmUserRp" Theme="Office2010Silver" Codebehind="UserRP.aspx.cs" %>
<%@ MasterType TypeName="administration_MasterPage" %>
<%@ Register Assembly="DevExpress.Web.v14.1, Version=14.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"  Namespace="DevExpress.Web.ASPxCallback" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v14.1, Version=14.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxTreeList.v14.1, Version=14.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxTreeList" TagPrefix="dx" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<div class="main-title">
	<span class="left"></span>
	<a href="#" class="icon text save">Save</a>	    
	<span class="right"></span>
</div>

<div class="container static">

    <table>
        <tr>
            <td colspan="2">
                <dx:ASPxGridView ID="UsersGrid" runat="server" DataSourceID="UsersDataSource" KeyFieldName="ID"   Width="100%" AutoGenerateColumns="False">
                    <ClientSideEvents FocusedRowChanged="function(s,e){ GMLoader.open(); RolesGrid.PerformCallback(); ActionsTree.PerformCallback(); }" />
                    <Columns>                            
                        <dx:GridViewDataTextColumn FieldName="FullName" Caption="User" Width="200px">  
                        <Settings AutoFilterCondition="Contains" />                              
                        </dx:GridViewDataTextColumn>   
                        <dx:GridViewDataTextColumn FieldName="Username" Caption="UserName" Width="250px">
                            <Settings AutoFilterCondition="Contains" />
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="ID" Caption="ID" Visible="false">                                
                        </dx:GridViewDataTextColumn>      
                        <dx:GridViewCommandColumn Caption=" " Width="40px" ButtonType="Image" ShowClearFilterButton="true">                            
                        </dx:GridViewCommandColumn>                      
                        <dx:GridViewDataColumn>
                        </dx:GridViewDataColumn>
                    </Columns>                                    
                    <SettingsBehavior AllowFocusedRow="True"  />
                    <Settings ShowFilterRow="True" />
                </dx:ASPxGridView>
                <asp:ObjectDataSource ID="UsersDataSource" runat="server" SelectMethod="ListUsers" TypeName="Core.User">                              
                </asp:ObjectDataSource>
            </td>
        </tr>
        <tr>
            <td valign="top">
                <dx:ASPxTreeList ID="ActionsTree" ClientInstanceName="ActionsTree"  runat="server" AutoGenerateColumns="False" KeyFieldName="ID" ParentFieldName="ParentID" Width="100%" DataSourceID="ActionsDataSource" 
                    OnSelectionChanged="ActionsTree_SelectionChanged" OnCustomCallback="ActionsTree_CustomCallback">
                    <ClientSideEvents EndCallback="function(s,e){ GMLoader.close(); }" />
                    <Columns>                                     
                        <dx:TreeListTextColumn FieldName="Caption" Caption="Permission">
                        </dx:TreeListTextColumn>                            
                        <dx:TreeListTextColumn FieldName="Code" Caption="Code">
                        </dx:TreeListTextColumn>            
                    </Columns>                                            
                    <Settings GridLines="Both" />
                    <SettingsBehavior AllowFocusedNode="True" AllowDragDrop="false" AutoExpandAllNodes="True" ProcessSelectionChangedOnServer="True" />
                    <SettingsSelection Enabled="True"  />                                            
                    <SettingsLoadingPanel Enabled="false" />
                </dx:ASPxTreeList>
                <asp:ObjectDataSource ID="ActionsDataSource" runat="server" SelectMethod="ListPermissions" TypeName="Core.Permission" >
                    <SelectParameters>
                        <asp:Parameter Name="dcode" Type="Int32" />            
                    </SelectParameters>
                </asp:ObjectDataSource>
            </td>
            <td valign="top">
                <dx:ASPxGridView ID="RolesGrid" ClientInstanceName="RolesGrid" runat="server" AutoGenerateColumns="False" KeyFieldName="ID" Width="100%"  DataSourceID="RolesDataSource" OnCustomCallback="RolesGrid_CustomCallback">  
                    <ClientSideEvents Init="function(s,e){ RolesGrid.PerformCallback(); }" EndCallback="function(s,e){ GMLoader.close(); }" />
                    <Columns>
                        <dx:GridViewCommandColumn ShowSelectCheckbox="True" Width="50px"></dx:GridViewCommandColumn>            
                        <dx:GridViewDataTextColumn FieldName="Caption" Caption="Role">
                        </dx:GridViewDataTextColumn>                                                
                    </Columns>                                                                 
                    <SettingsBehavior AllowFocusedRow="True" />
                    <SettingsLoadingPanel Mode="Disabled" />
                </dx:ASPxGridView>
   
                <asp:ObjectDataSource ID="RolesDataSource" runat="server" SelectMethod="ListRoles" TypeName="Core.Role">                    
                </asp:ObjectDataSource>
            </td>            
        </tr>
    </table>        
    <dx:ASPxCallback ID="Callbacker" ClientInstanceName="Callbacker" runat="server" OnCallback="Callbacker_Callback">
        <ClientSideEvents BeginCallback="function(s,e){ GMLoader.open(); }" EndCallback="function(s,e){ GMLoader.close(); }" />
    </dx:ASPxCallback>
</div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptsPlaceHolder" Runat="Server">
<script type="text/javascript">
    $(document).ready(function (e) {
        $(".icon.save").click(function (e) {            
            Callbacker.PerformCallback('Save');
            return false;
        });
    });
</script>    
</asp:Content>