<%@ Page Title="Subscriptions" Language="C#" MasterPageFile="../MasterPage.master" AutoEventWireup="true" Inherits="administration_general_subscriptions"  Theme="DevEx" Codebehind="subscriptions.aspx.cs" %>
<%@ MasterType TypeName="administration_MasterPage" %>
<%@ Register Assembly="DevExpress.Web.v14.1, Version=14.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v14.1, Version=14.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register src="../../UserControls/ExporterControl.ascx" tagname="ExporterControl" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">    
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div class="main-title">
	<span class="left"></span>   
    <a href="#" class="icon text refresh">განახლება</a>
    <a href="#" class="icon text add">დამატება</a>        
    <uc1:ExporterControl ID="ExporterControl1" runat="server" Type="GridView" FileName="subscriptions"  />        
	<span class="right"></span>
</div>

<div class="container">    
    <dx:ASPxGridView ID="SubscriptionsGrid" ClientInstanceName="SubscriptionsGrid" runat="server" AutoGenerateColumns="False" DataSourceID="SubscriptionsDataSource" KeyFieldName="ID" Width="100%"
         OnRowDeleting="SubscriptionsGrid_RowDeleting">
        <ClientSideEvents BeginCallback="function(s,e) { OnSubscriptionsGridBeginCallback(s,e) }" EndCallback="function(s,e){ OnSubscriptionsGridEndCallback(s,e); }" />
        <Columns>
            <dx:GridViewCommandColumn ShowSelectCheckbox="True" Width="40px" AllowDragDrop="False">
            </dx:GridViewCommandColumn>
            <dx:GridViewDataTextColumn FieldName="UserFullName" Caption="მომხმარებელი" Width="300px" >
                <Settings AutoFilterCondition="Contains" />
                <EditCellStyle>
                    <Paddings PaddingLeft="7px" />
                </EditCellStyle>
                <EditItemTemplate>
                    <%#Eval("UserFullName") %>
                </EditItemTemplate>                
            </dx:GridViewDataTextColumn>            
            <dx:GridViewDataComboBoxColumn FieldName="CourseID" Caption="წიგნი" Width="250px">
                <PropertiesComboBox TextField="Caption" ValueField="ID" ValueType="System.Int64" DataSourceID="CoursesDataSource">
                </PropertiesComboBox>
                <EditItemTemplate></EditItemTemplate>                
            </dx:GridViewDataComboBoxColumn>                        
            <dx:GridViewDataDateColumn FieldName="CRTime" Caption="აქტივაციის თარიღი" Width="170px">
                <EditCellStyle>
                    <Paddings PaddingLeft="7px" />
                </EditCellStyle>
                <EditItemTemplate>
                    <%#((DateTime)Eval("CRTime")).ToString(FormatDateTime) %>
                </EditItemTemplate>
                <DataItemTemplate>
                    <%#((DateTime)Eval("CRTime")).ToString(FormatDateTime) %>
                </DataItemTemplate>
            </dx:GridViewDataDateColumn>            
            <dx:GridViewDataDateColumn FieldName="ExpDate" Caption="ამოწურვის თარიღი" Width="170px">                
            </dx:GridViewDataDateColumn>  
            <dx:GridViewDataColumn FieldName="CourseID" Visible="false" ShowInCustomizationForm="false"></dx:GridViewDataColumn>
            <dx:GridViewDataColumn FieldName="UserID" Visible="false" ShowInCustomizationForm="false"></dx:GridViewDataColumn>
            <dx:GridViewCommandColumn  Caption=" " ButtonType="Image" Width="60px" AllowDragDrop="False" ShowEditButton="true" ShowUpdateButton="true" ShowCancelButton="true" ShowDeleteButton="true" ShowClearFilterButton="true">                                                
            </dx:GridViewCommandColumn>
            <dx:GridViewDataColumn>
                <Settings AllowDragDrop="False" />
                <EditItemTemplate></EditItemTemplate>
            </dx:GridViewDataColumn>            
        </Columns>
        <Templates>
            <DetailRow>
                <dx:ASPxGridView ID="SubscriptionDetailsGrid" runat="server" DataSourceID="SubscriptionHistoryDataSource" KeyFieldName="ID" OnBeforePerformDataSelect="SubscriptionDetailsGrid_BeforePerformDataSelect">
                    <ClientSideEvents BeginCallback="OnSubscriptionDetailsGridBeginCallback" EndCallback="OnSubscriptionDetailsGridEndCallback" />
                    <Columns>
                        <dx:GridViewDataTextColumn FieldName="UserFullName" Caption="მომხმარებელი" Width="350px" >
                            <Settings AutoFilterCondition="Contains" />
                            <EditCellStyle>
                                <Paddings PaddingLeft="7px" />
                            </EditCellStyle>
                            <EditItemTemplate>
                                <%#Eval("UserFullName") %>
                            </EditItemTemplate>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="Course" Caption="წიგნი" Width="250px" >
                            <Settings AutoFilterCondition="Contains" />
                            <EditCellStyle>
                                <Paddings PaddingLeft="7px" />
                            </EditCellStyle>
                            <EditItemTemplate>
                                <%#Eval("Course") %>
                            </EditItemTemplate>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataDateColumn FieldName="CRTime" Caption="აქტივაციის თარიღი" Width="170px">
                            <PropertiesDateEdit DisplayFormatString="MMM dd, yyyy hh:mm tt" ></PropertiesDateEdit>
                            <EditCellStyle>
                                <Paddings PaddingLeft="7px" />
                            </EditCellStyle>
                            <EditItemTemplate>
                                <%#((DateTime)Eval("CRTime")).ToString(FormatDateTime,new System.Globalization.CultureInfo("ka-ge")) %>
                            </EditItemTemplate>
                            <DataItemTemplate>
                                <%#((DateTime)Eval("CRTime")).ToString(FormatDateTime,new System.Globalization.CultureInfo("ka-ge")) %>
                            </DataItemTemplate>
                        </dx:GridViewDataDateColumn>            
                        <dx:GridViewDataDateColumn FieldName="ExpDate" Caption="ამოწურვის თარიღი" Width="170px">                            
                        </dx:GridViewDataDateColumn>            
                        <dx:GridViewCommandColumn  Caption=" " ButtonType="Image" Width="60px" ShowEditButton="true" ShowUpdateButton="true" ShowCancelButton="true" ShowDeleteButton="true" ShowClearFilterButton="true">
                        </dx:GridViewCommandColumn>
                        <dx:GridViewDataColumn>
                            <EditItemTemplate></EditItemTemplate>
                        </dx:GridViewDataColumn>            
                    </Columns>                                        
                    <TotalSummary>
                        <dx:ASPxSummaryItem FieldName="ID" ShowInColumn="UserFullName" SummaryType="Count" DisplayFormat="{0}" />
                        <dx:ASPxSummaryItem FieldName="Price" ShowInColumn="Price" SummaryType="Sum" DisplayFormat="{0:n2}" />
                    </TotalSummary>                               
                </dx:ASPxGridView>                
            </DetailRow>            
        </Templates>        
        <SettingsBehavior EnableCustomizationWindow="true" />
        <SettingsDetail ShowDetailRow="true" />        
        <TotalSummary>
            <dx:ASPxSummaryItem FieldName="ID" ShowInColumn="UserFullName" SummaryType="Count" DisplayFormat="{0} უნიკალური აქტივაცია" />            
        </TotalSummary>           
    </dx:ASPxGridView>

    <asp:ObjectDataSource ID="SubscriptionsDataSource" runat="server" SelectMethod="ListRecentSubscriptions" TypeName="Core.Subscription"  UpdateMethod="TSP_Subscription" DeleteMethod="TX_Subscriptions"
         OnDeleting="SubscriptionsDataSource_Deleting">
        <SelectParameters>
            <asp:Parameter Name="iud" Type="Byte" />
            <asp:Parameter Name="filter" Type="Object" />
        </SelectParameters>
        <UpdateParameters>
            <asp:Parameter Name="iud" Type="Byte" DefaultValue="1" />
            <asp:Parameter Name="ID" Type="Int64" />
            <asp:Parameter Name="UserID" Type="Int64" />
            <asp:Parameter Name="CourseID" Type="Int64" />
            <asp:Parameter Name="TypeCode" Type="String" />
            <asp:Parameter Name="Type" Type="Int32" />
            <asp:Parameter Name="TariffID" Type="Int64" />
            <asp:Parameter Name="ExpDate" Type="DateTime" />
            <asp:Parameter Name="Note" Type="String" />
        </UpdateParameters>
        <DeleteParameters>
            <asp:Parameter Name="iud" Type="Byte" DefaultValue="6" />
            <asp:Parameter Name="xml" Type="String" />
        </DeleteParameters>
    </asp:ObjectDataSource>        
        
    <asp:ObjectDataSource ID="SubscriptionHistoryDataSource" runat="server" SelectMethod="ListUserCourseSubscriptionHistory" TypeName="Core.Subscription" DeleteMethod="TSP_Subscription" UpdateMethod="TSP_Subscription">
        <DeleteParameters>
            <asp:Parameter Name="iud" Type="Byte" DefaultValue="2" />
            <asp:Parameter Name="ID" Type="Int64" />
            <asp:Parameter Name="UserID" Type="Int64" />
            <asp:Parameter Name="CourseID" Type="Int64" />
            <asp:Parameter Name="TypeCode" Type="String" />
            <asp:Parameter Name="Type" Type="Int32" />
            <asp:Parameter Name="TariffID" Type="Int64" />
            <asp:Parameter Name="ExpDate" Type="DateTime" />
            <asp:Parameter Name="Note" Type="String" />
        </DeleteParameters>
        <SelectParameters>
            <asp:Parameter Name="UserID" Type="Int64" />
            <asp:Parameter Name="CourseID" Type="Int64" />
        </SelectParameters>
        <UpdateParameters>
            <asp:Parameter Name="iud" Type="Byte" DefaultValue="1" />
            <asp:Parameter Name="ID" Type="Int64" />
            <asp:Parameter Name="UserID" Type="Int64" />
            <asp:Parameter Name="CourseID" Type="Int64" />
            <asp:Parameter Name="TypeCode" Type="String" />
            <asp:Parameter Name="Type" Type="Int32" />
            <asp:Parameter Name="TariffID" Type="Int64" />
            <asp:Parameter Name="ExpDate" Type="DateTime" />
            <asp:Parameter Name="Note" Type="String" />
        </UpdateParameters>
    </asp:ObjectDataSource>
        
    <asp:ObjectDataSource ID="CoursesDataSource" runat="server" SelectMethod="ListCourses" CacheDuration="1800" TypeName="Core.Course">        
    </asp:ObjectDataSource>    
</div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptsPlaceHolder" Runat="Server">    
<script>
    $(document).ready(function () {

        $(".puzzle").click(function (e) {
            SubscriptionsGrid.ShowCustomizationWindow();
            return false;
        });

        $(".refresh").click(function (e) {
            SubscriptionsGrid.Refresh();
            return false;
        });

        $(".add").click(function (e) {
            FancyBox.Init({
                href: "/administration/popuppages/subscriptions.aspx",
                height: 430,
                width: 650
            }).ShowPopup();
            return false;
        });

        $("#ShowAllCheckBox").change(function (e) {
            if ($(this).is(':checked')) {
                SubscriptionsGrid.SelectRows();
            }
            else {
                SubscriptionsGrid.UnselectRows();
            }
            return false;
        });

    });

    var command;
    function OnSubscriptionsGridBeginCallback(s, e) {
        if (e.command == "APPLYCOLUMNFILTER") {
            command = e.command;
        }
    }

    function OnSubscriptionsGridEndCallback(s, e) {
        if (command == "APPLYCOLUMNFILTER") {
            $("#ShowAllCheckBox").removeAttr("checked");
            command = null;
            //s.UnselectRows();        
        }
    }


    var DetailCommand;
    function OnSubscriptionDetailsGridBeginCallback(s, e) {
        DetailCommand = e.command;
    }

    function OnSubscriptionDetailsGridEndCallback(s, e) {
        if (DetailCommand == "UPDATEEDIT") {
            SubscriptionsGrid.Refresh();
        }
    }
</script>
</asp:Content>