<%@ Page Title="Course Keys" Language="C#" MasterPageFile="~/Administration/CM/course/MasterPage.master" AutoEventWireup="true" Inherits="administration_WebManagement_CourseKeys" Theme="DevEx" Codebehind="CourseKeys.aspx.cs" %>
<%@ MasterType VirtualPath="~/Administration/CM/course/MasterPage.master" %>
<%@ Register Assembly="DevExpress.Web.v14.1, Version=14.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"  Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v14.1, Version=14.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register src="~/UserControls/ExporterControl.ascx" tagname="ExporterControl" tagprefix="uc1" %>
<%@ Import Namespace="Core.Properties" %>
<asp:Content ID="Content2" ContentPlaceHolderID="CourseContentPlaceHolder" runat="Server">
<div class="main-title">
	<span class="left"></span>
    <a href="#" class="icon text add">დამატება</a>    
	<uc1:ExporterControl ID="Exporter" runat="server" GridID="KeysGrid" FileName="Keys" />
	<span class="right"></span>
</div>

<div class="container">
    <dx:ASPxGridView ID="KeyGroupsGrid" ClientInstanceName="KeyGroupsGrid" runat="server" DataSourceID="KeyGroupsDataSource" KeyFieldName="ID"  Width="100%" AutoGenerateColumns="False"
        OnCustomButtonCallback="KeyGroupsGrid_CustomButtonCallback">
        <ClientSideEvents CustomButtonClick="function(s,e){ e.processOnServer = confirm(ConfirmResetCourseKeyGroup); }" />
        <Columns>
            <dx:GridViewDataTextColumn FieldName="Caption" Caption="ჯგუფი" Width="300px">
            </dx:GridViewDataTextColumn>                        
            <dx:GridViewDataTextColumn FieldName="Months" Caption="თვე" Width="100px">
            </dx:GridViewDataTextColumn>                    
            <dx:GridViewDataDateColumn FieldName="CRTime" Caption="შექმნის თარიღი" Width="170px">                
            </dx:GridViewDataDateColumn>                                        
            <dx:GridViewDataSpinEditColumn FieldName="KeyCount" Caption="გასაღებები" Width="80px">
                <PropertiesSpinEdit AllowMouseWheel="false">
                    <SpinButtons ShowIncrementButtons="false"></SpinButtons>
                </PropertiesSpinEdit>
                <EditItemTemplate></EditItemTemplate>
            </dx:GridViewDataSpinEditColumn>            
            <dx:GridViewCommandColumn Caption="განულება" ButtonType="Image" Width="100px">
                <CustomButtons>
                    <dx:GridViewCommandColumnCustomButton ID="GridViewCommandColumnCustomButton1">
                        <Image Url="~/plugins/icons/img/reset.png"></Image>
                    </dx:GridViewCommandColumnCustomButton>
                </CustomButtons>
            </dx:GridViewCommandColumn>
            <dx:GridViewDataColumn Width="30px">
                <DataItemTemplate>
                    <a href="<%#Eval("ID") %>" class="icon edit"></a>
                </DataItemTemplate>
            </dx:GridViewDataColumn>
            <dx:GridViewCommandColumn  Caption=" " ButtonType="Image" Width="30px" ShowDeleteButton="true" ShowClearFilterButton="true">                
            </dx:GridViewCommandColumn>
            <dx:GridViewDataColumn>
                <EditItemTemplate></EditItemTemplate>
            </dx:GridViewDataColumn>
        </Columns>
        <SettingsDetail ShowDetailRow="true" />
        
        <Templates>
            <DetailRow>
                <dx:ASPxGridView ID="KeysGrid" runat="server" DataSourceID="CourseKeysDataSource" KeyFieldName="ID" OnInit="KeysGrid_Init" OnCustomButtonCallback="KeysGrid_CustomButtonCallback">
                    <ClientSideEvents CustomButtonClick="function(s,e){ e.processOnServer = confirm(ConfirmResetCourseKey); }" />
                    <Columns>
                        <dx:GridViewDataTextColumn FieldName="Key" Caption="გასაღები" Width="300px">
                        </dx:GridViewDataTextColumn>                                                                        
                        <dx:GridViewDataCheckColumn FieldName="IsUsed" Caption="გამოყენებულია" Width="100px">
                            <PropertiesCheckEdit DisplayTextChecked="გამოყენებული" DisplayTextUnchecked="გამოუყენებელი"></PropertiesCheckEdit>
                        </dx:GridViewDataCheckColumn>
                        <dx:GridViewDataTextColumn FieldName="UserFullName" Caption="მომხმარებელი" Width="200px">
                            <Settings AllowDragDrop="True" />
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataDateColumn FieldName="KeyUseTime" Caption="გამოყენების თარიღი"  Width="170px">
                            <PropertiesDateEdit DisplayFormatString="MMM dd, yyyy HH:mm" DisplayFormatInEditMode="true" EditFormat="Custom" EditFormatString="MMM dd, yyyy HH:mm"></PropertiesDateEdit>
                        </dx:GridViewDataDateColumn>       
                        <dx:GridViewCommandColumn Caption="განულება" ButtonType="Image" Width="100px">
                            <CustomButtons>
                                <dx:GridViewCommandColumnCustomButton ID="ResetButton">
                                    <Image Url="~/plugins/icons/img/reset.png"></Image>
                                </dx:GridViewCommandColumnCustomButton>
                            </CustomButtons>
                        </dx:GridViewCommandColumn>
                        <dx:GridViewCommandColumn  Caption=" " ButtonType="Image" Width="60px" ShowDeleteButton="true" ShowClearFilterButton="true">
                        </dx:GridViewCommandColumn>
                        <dx:GridViewDataColumn>
                            <EditItemTemplate></EditItemTemplate>
                        </dx:GridViewDataColumn>                          
                    </Columns>                    
                    <TotalSummary>
                        <dx:ASPxSummaryItem FieldName="ID" ShowInColumn="Key" SummaryType="Count" DisplayFormat="{0}" />
                    </TotalSummary>
                </dx:ASPxGridView>
            </DetailRow>
        </Templates>        
    </dx:ASPxGridView>    
    <asp:ObjectDataSource ID="KeyGroupsDataSource" runat="server" SelectMethod="ListCourseKeyGroups" TypeName="Core.CourseKeyGroup" DeleteMethod="TSP_CourseKeysGroup">
        <SelectParameters>
            <asp:Parameter Name="CourseID" Type="Int64" />
        </SelectParameters>
        <DeleteParameters>
            <asp:Parameter Name="iud" Type="Byte" DefaultValue="2" />
            <asp:Parameter Name="ID" Type="Int64" />
            <asp:Parameter Name="CourseID" Type="Int64" />
            <asp:Parameter Name="Caption" Type="String" />            
            <asp:Parameter Name="LifeTimeTypeID" Type="Int32" />
            <asp:Parameter Name="MonthLifeTime" Type="Byte" />
            <asp:Parameter Name="DayLifeTime" Type="Byte" />
            <asp:Parameter Name="ExpDate" Type="DateTime" />
        </DeleteParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="CourseKeysDataSource" runat="server" SelectMethod="ListCourseKeysByGroupID" TypeName="Core.CourseKey" DeleteMethod="TSP_CourseKeys">
        <DeleteParameters>
            <asp:Parameter Name="iud" Type="Byte" DefaultValue="2" />
            <asp:Parameter Name="ID" Type="Int64" />
            <asp:Parameter Name="GroupID" Type="Int64" />
            <asp:Parameter Name="Key" Type="String" />
        </DeleteParameters>
        <SelectParameters>
            <asp:Parameter Name="GroupID" Type="Int64" />
        </SelectParameters>
    </asp:ObjectDataSource>    
    <input type="hidden" id="HFConfirmResetCourseKeyGroup"  value="<%=Resources.ConfirmResetCourseKeyGroup %>" />
    <input type="hidden" id="HFConfirmResetCourseKey"  value="<%=Resources.ConfirmResetCourseKey %>" />
</div>    
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CourseScriptsPlaceHolder" Runat="Server">
<script type="text/javascript">
    var ConfirmResetCourseKeyGroup;
    var ConfirmResetCourseKey;
    var URL;

    $(document).ready(function () {

        ConfirmResetCourseKeyGroup = $("#HFConfirmResetCourseKeyGroup").val();
        ConfirmResetCourseKey = $("#HFConfirmResetCourseKey").val();
        URL = "/Administration/PopupPages/CreateNewCourseKeys.aspx?cid=" + gup("id") + "&title=გასაღებების შექმნა";

        $(".container").on("click", ".icon.edit", function (e) {
            FancyBox.Init({
                href: URL + "&id=" + $(this).attr("href"),
                height: 300,
                width: 650
            }).ShowPopup();

            return false;
        });

        $(".icon.add").click(function (e) {
            FancyBox.Init({
                href: URL,
                height: 400,
                width: 650
            }).ShowPopup();

            return false;
        });
    });
    
</script>
</asp:Content>
