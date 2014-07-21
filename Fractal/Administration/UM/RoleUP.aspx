<%@ Page Title="Role User ↔ Permissions" Language="C#" MasterPageFile="../MasterPage.master" AutoEventWireup="true" Inherits="management_UM_RoleUP" Theme="Office2010Silver" Codebehind="RoleUP.aspx.cs" %>
<%@ MasterType TypeName="administration_MasterPage" %>
<%@ Register Assembly="DevExpress.Web.v14.1, Version=14.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"  Namespace="DevExpress.Web.ASPxCallback" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v14.1, Version=14.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxTreeList.v14.1, Version=14.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxTreeList" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v14.1, Version=14.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"  Namespace="DevExpress.Web.ASPxClasses" tagprefix="dxw" %>  
<%@ Register assembly="DevExpress.Web.v14.1, Version=14.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div class="main-title">
	<span class="left"></span>
	<a href="#" class="icon text save">Save</a>	    
	<span class="right"></span>
</div>

<div class="container">
    <table>
        <tr>
            <td colspan="2" valign="top">
                <dx:ASPxGridView ID="RolesGrid" runat="server" AutoGenerateColumns="False" KeyFieldName="ID" Width="1148px" DataSourceID="RolesDataSource">
                    <ClientSideEvents FocusedRowChanged="function(s,e){ GMLoader.open();  ActionsTree.PerformCallback();  UsersGrid.PerformCallback(); }" />
                    <Columns>                            
                        <dx:GridViewDataTextColumn FieldName="Caption" Caption="Role">
                        </dx:GridViewDataTextColumn>
                    </Columns>                             
                    <SettingsBehavior AllowFocusedRow="True" />
                </dx:ASPxGridView>

                <asp:ObjectDataSource ID="RolesDataSource" runat="server" SelectMethod="ListRoles" TypeName="Core.Role">                    
                </asp:ObjectDataSource>
            </td>
        </tr>
        <tr>
            <td valign="top">
                <dx:ASPxTreeList ID="ActionsTree" ClientInstanceName="ActionsTree" runat="server" KeyFieldName="ID" ParentFieldName="ParentID" DataSourceID="ActionsDataSource" Width="500px"
                    OnSelectionChanged="ActionsTree_SelectionChanged" OnCustomCallback="ActionsTree_CustomCallback">
                    <ClientSideEvents EndCallback="function(s,e){ GMLoader.close(); }" />
                    <Columns>                                     
                        <dx:TreeListTextColumn FieldName="Caption" Caption="Permission">
                        </dx:TreeListTextColumn>                            
                        <dx:TreeListTextColumn FieldName="Code" Caption="Code">
                        </dx:TreeListTextColumn>            
                    </Columns>
                    <Settings GridLines="Both" />
                    <SettingsBehavior AllowFocusedNode="True" AllowDragDrop="true" 
                        AutoExpandAllNodes="True" ProcessSelectionChangedOnServer="True" />
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
                 <dx:ASPxGridView ID="UsersGrid" ClientInstanceName="UsersGrid" runat="server" DataSourceID="UsersDataSource" Width="500px" KeyFieldName="ID" OnCustomCallback="UsersGrid_CustomCallback">
                    <ClientSideEvents Init="function(s,e) { UsersGrid.PerformCallback(); }" EndCallback="function(s,e){ GMLoader.close(); }" />
                    <Columns>
                        <dx:GridViewCommandColumn ShowSelectCheckbox="true" Width="50px"></dx:GridViewCommandColumn>
                        <dx:GridViewDataTextColumn FieldName="FullName" Caption="User">                                
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="Username"   Caption="UserName">
                            <Settings AutoFilterCondition="Contains" />
                        </dx:GridViewDataTextColumn>
                    </Columns>
                    <SettingsBehavior AllowFocusedRow="True"  />
                    <SettingsLoadingPanel Mode="Disabled" />
                </dx:ASPxGridView>
                <asp:ObjectDataSource ID="UsersDataSource" runat="server" SelectMethod="ListUsers" TypeName="Core.User">                                                                                        
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