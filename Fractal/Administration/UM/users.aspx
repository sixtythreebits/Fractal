<%@ Page Title="Users" Language="C#" MasterPageFile="../MasterPage.master" AutoEventWireup="true" Inherits="management_UM_users" Theme="Office2010Silver" Codebehind="users.aspx.cs" %>
<%@ MasterType TypeName="administration_MasterPage" %>
<%@ Register Assembly="DevExpress.Web.v14.1, Version=14.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register assembly="DevExpress.Web.v14.1, Version=14.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div class="main-title">
	<span class="left"></span>
	<a href="#" class="icon text add">Create New</a>
    <a href="#" class="icon text puzzle">Show Customization Form</a>    
	<span class="right"></span>
</div>    
<div class="container">
    <dx:ASPxGridView ID="UsersGrid" ClientInstanceName="UsersGrid" runat="server" DataSourceID="UsersDataSource" AutoGenerateColumns="False" ClientIDMode="AutoID" KeyFieldName="ID" Width="100%"
        onrowdeleting="UsersGrid_RowDeleting" onrowinserting="UsersGrid_RowInserting" onrowupdating="UsersGrid_RowUpdating" onrowvalidating="UsersGrid_RowValidating">
        <ClientSideEvents CallbackError="OnCallbackError" />
        <Columns>                                                            
            <dx:GridViewDataTextColumn FieldName="Username" Caption="Username" Width="220px">
                <Settings AutoFilterCondition="Contains" />
                <PropertiesTextEdit MaxLength="50">
                    <ValidationSettings>
                        <RequiredField IsRequired="true" />
                    </ValidationSettings>
                </PropertiesTextEdit>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="Password" Caption="Password" Width="80px">
                <PropertiesTextEdit MaxLength="50">
                </PropertiesTextEdit>
                <DataItemTemplate>****</DataItemTemplate>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="Fname" Caption="Fname" Width="100px">
                <Settings AutoFilterCondition="Contains" />
                <PropertiesTextEdit MaxLength="20">
                </PropertiesTextEdit>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="Lname" Caption="Lname"  Width="100px">
                <Settings AutoFilterCondition="Contains" />
                <PropertiesTextEdit MaxLength="20">
                </PropertiesTextEdit>
            </dx:GridViewDataTextColumn>            
            <dx:GridViewDataTextColumn FieldName="Email" Caption="Email" Width="220px">
                <Settings AutoFilterCondition="Contains" />
                <PropertiesTextEdit MaxLength="100">
                    <ValidationSettings>
                        <RequiredField IsRequired="true" />
                        <RegularExpression ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" />
                    </ValidationSettings>
                </PropertiesTextEdit>
            </dx:GridViewDataTextColumn>                        
            <dx:GridViewDataTextColumn FieldName="College" Caption="College"  Width="150px">
                <Settings AutoFilterCondition="Contains" />                
                <PropertiesTextEdit MaxLength="20">
                </PropertiesTextEdit>
                <EditItemTemplate></EditItemTemplate>
            </dx:GridViewDataTextColumn>                                                                              
            <dx:GridViewDataCheckColumn FieldName="IsTeacher" Caption="Teacher" Width="70px">
                <PropertiesCheckEdit DisplayTextChecked="Teacher" DisplayTextUnchecked="Student">                    
                </PropertiesCheckEdit>
            </dx:GridViewDataCheckColumn>
            <dx:GridViewDataCheckColumn FieldName="IsTA" Caption="TA" Width="50px">
            </dx:GridViewDataCheckColumn>
            <dx:GridViewDataCheckColumn FieldName="IsAdmin" Caption="Admin" Width="60px">                
            </dx:GridViewDataCheckColumn>
            <dx:GridViewDataCheckColumn FieldName="IsActive" Caption="Active" Width="70px">
                <PropertiesCheckEdit DisplayTextChecked="Active" DisplayTextUnchecked="Not Active"></PropertiesCheckEdit> 
            </dx:GridViewDataCheckColumn>
            <dx:GridViewDataDateColumn FieldName="LastVisitDate" Caption="Last Visit" Width="170px" Visible="false">
                <Settings AllowDragDrop="True" />
                <PropertiesDateEdit DisplayFormatString="MMM dd, yyyy hh:mm tt"></PropertiesDateEdit>
                <EditCellStyle>
                    <Paddings PaddingLeft="7px" PaddingRight="7px" />
                </EditCellStyle>
                <EditItemTemplate>
                    <%#Eval("LastVisitDate","{0:" + General.Properties.Resources.FormatDateTime + "}") %>
                </EditItemTemplate>
            </dx:GridViewDataDateColumn>                        
            <dx:GridViewDataDateColumn FieldName="CRTime" Caption="Reg Date"  Width="170px">
                <Settings AllowDragDrop="True" />
                <PropertiesDateEdit DisplayFormatString="MMM dd, yyyy hh:mm tt"></PropertiesDateEdit>
                <EditCellStyle>
                    <Paddings PaddingLeft="7px" PaddingRight="7px" />
                </EditCellStyle>
                <EditItemTemplate>
                    <%#Eval("CRTime","{0:" + General.Properties.Resources.FormatDateTime + "}") %>                    
                </EditItemTemplate>
            </dx:GridViewDataDateColumn>
            <dx:GridViewDataColumn Caption="More" Width="50px">
                <CellStyle HorizontalAlign="Center"></CellStyle>
                <DataItemTemplate>
                <a href="user.aspx?id=<%#Eval("ID") %>"><img src="<%=Core.Utilities.AppSettings.PluginIconImagesFolderHttpPath %>info.png" /></a>
                </DataItemTemplate>
                <EditItemTemplate></EditItemTemplate>
            </dx:GridViewDataColumn>
            <dx:GridViewCommandColumn  Caption=" " ButtonType="Image" Width="60px" ShowEditButton="true" ShowUpdateButton="true" ShowCancelButton="true" ShowDeleteButton="true" ShowClearFilterButton="true" >
            </dx:GridViewCommandColumn>
            <dx:GridViewDataColumn>
                <EditItemTemplate></EditItemTemplate>
            </dx:GridViewDataColumn>
        </Columns>        
        <TotalSummary>
            <dx:ASPxSummaryItem FieldName="ID" ShowInColumn="Username" SummaryType="Count" DisplayFormat="{0}" />
        </TotalSummary>            
        <SettingsBehavior EnableCustomizationWindow="true" />
        <SettingsPopup>
            <CustomizationWindow HorizontalAlign="RightSides" VerticalAlign="TopSides" />
        </SettingsPopup>        
    </dx:ASPxGridView>

    <asp:ObjectDataSource ID="UsersDataSource" runat="server" DeleteMethod="TX_Users" InsertMethod="TX_Users" SelectMethod="ListUsers" 
        TypeName="Core.User" UpdateMethod="TX_Users" ondeleting="UsersDataSource_Deleting" oninserting="UsersDataSource_Inserting" OnObjectCreated="UsersDataSource_ObjectCreated" OnDeleted="UsersDataSource_Deleted"
        onupdating="UsersDataSource_Updating">
        <DeleteParameters>
            <asp:Parameter Name="iud" Type="Byte" DefaultValue="2" />
            <asp:Parameter Name="xml" Type="String" />
        </DeleteParameters>
        <InsertParameters>
            <asp:Parameter Name="iud" Type="Byte" DefaultValue="0" />
            <asp:Parameter Name="xml" Type="String" />
        </InsertParameters>
        <UpdateParameters>
            <asp:Parameter Name="iud" Type="Byte" DefaultValue="1" />
            <asp:Parameter Name="xml" Type="String" />
        </UpdateParameters>
    </asp:ObjectDataSource>
</div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptsPlaceHolder" Runat="Server">
<script type="text/javascript">
    $(document).ready(function () {
        $(".icon.add").click(function () {
            UsersGrid.AddNewRow();
            return false;
        });

        $(".icon.puzzle").click(function () {
            UsersGrid.ShowCustomizationWindow();
            return false;
        });
    });
</script>
</asp:Content>