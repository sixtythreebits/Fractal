<%@ Page Title="" Language="C#" MasterPageFile="MasterPage.master"  AutoEventWireup="true" Inherits="administration_CM_course_subscriptions" Theme="Office2010Silver" Codebehind="subscriptions.aspx.cs" %>
<%@ MasterType TypeName="administration_course_MasterPage" %>
<%@ Reference Control="~/administration/MasterPage.master" %>

<%@ Register Assembly="DevExpress.Web.v14.1, Version=14.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CourseHeadPlaceHolder" Runat="Server">
    <script type="text/javascript" src="<%=Master.Master.Root %>scripts/layout.js"></script>
    <script type="text/javascript" src="<%=Master.Master.Root %>scripts/CM/course/subscriptions.aspx.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CourseContentPlaceHolder" Runat="Server">
<div class="main-title">
	<span class="left"></span>        
    <div class="datepicker">
        <a>Subscription Start</a>
        <asp:TextBox ID="D1TextBox" runat="server" class="from" ClientIDMode="Static"></asp:TextBox>
		<p>-</p>
        <a>End</a>
        <asp:TextBox ID="D2TextBox" runat="server" class="to" ClientIDMode="Static"></asp:TextBox>
	</div>
    <div class="datepicker">
        <a>Expiration Start</a>
        <asp:TextBox ID="ExpD1TextBox" runat="server" class="from" ClientIDMode="Static"></asp:TextBox>
		<p>-</p>
        <a>End</a>
        <asp:TextBox ID="ExpD2TextBox" runat="server" class="to" ClientIDMode="Static"></asp:TextBox>
	</div>
    <asp:TextBox ID="SearchTextBox" runat="server" CssClass="search" ClientIDMode="Static"></asp:TextBox>
	<span class="right"></span>
</div>  
<div class="filter-sec">    	
    <div><label>Type:</label>
        <div class="select">
			<p>All</p>
            <asp:DropDownList ID="SubscriptionTypesCombo" runat="server" DataTextField="Name" DataValueField="ID" DataSourceID="DeviceDataSource" ClientIDMode="Static" OnDataBound="SubscriptionTypesCombo_DataBound"></asp:DropDownList>                
            <asp:ObjectDataSource ID="DeviceDataSource" runat="server" SelectMethod="ListAny" TypeName="General.Any" EnableCaching="true" CacheDuration="1800">
                <SelectParameters>
                    <asp:Parameter Name="level" Type="Int32"  DefaultValue="1" />
                    <asp:Parameter Name="dcode" Type="Int32"  DefaultValue="2" />
                </SelectParameters>
            </asp:ObjectDataSource>
		</div>
    </div>        
</div>
<div class="container">
    <dx:ASPxGridView ID="SubscriptionsGrid" ClientInstanceName="SubscriptionsGrid" runat="server" KeyFieldName="ID" DataSourceID="SubscriptionsDataSource" Width="100%" OnBeforePerformDataSelect="SubscriptionsGrid_BeforePerformDataSelect">
        <Columns>
        <dx:GridViewDataColumn Caption="item">
            <DataItemTemplate>
                <div class="course-subscriptions">
                    <div class="div1">
                        <%#Eval("UserFullName") %>
                    </div>
                    <div class="div2">
                        
                    </div>
                    <div class="div3">
                        <%#GetSubscriptionHtml((string)Eval("TypeCode"),(decimal?)Eval("Price")) %>                    
                    </div>
                    <div class="div4">
                        <%# ((DateTime)Eval("CRTime")).ToString(General.Properties.Resources.FormatDateTime) %>
                        <span>Subscription Date</span>
                    </div>
                    <div class="div5">
                        <%# ((DateTime)Eval("ExpDate")).ToString(General.Properties.Resources.FormatDateTime) %>
                        <span>Expiration Date</span>
                    </div>
                </div>
            </DataItemTemplate>
        </dx:GridViewDataColumn>
        </Columns>
        <Settings ShowColumnHeaders="false" />
        <TotalSummary>
            <dx:ASPxSummaryItem FieldName="ID" DisplayFormat="n0" SummaryType="Count" ShowInColumn="item" />
        </TotalSummary>
    </dx:ASPxGridView>
    <asp:ObjectDataSource ID="SubscriptionsDataSource" runat="server" SelectMethod="ListRecentSubscriptions" TypeName="Core.Subscription">
        <SelectParameters>
            <asp:Parameter Name="iud" Type="Byte" DefaultValue="0" />
            <asp:Parameter Name="filter" Type="Object" />
        </SelectParameters>
    </asp:ObjectDataSource>        
</div>
</asp:Content>