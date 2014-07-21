<%@ Page Title="" Language="C#" MasterPageFile="MasterPage.master" AutoEventWireup="true" Inherits="administration_CM_course_Default" Codebehind="Default.aspx.cs" %>
<%@ Reference Control="~/administration/MasterPage.master" %>
<%@ MasterType TypeName="administration_course_MasterPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CourseHeadPlaceHolder" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CourseContentPlaceHolder" Runat="Server">
				
<div class="view-text">
    <p>
    ეს განყოფილება საშუალებას გაძლევთ დაათვალიეროთ წიგნის სხვადასხვა მოდულები.    
    </p>
</div>
                
<div class="tab-nav clearfix" id="tab-nav">
    <asp:Repeater ID="CourseItemsRepeater" runat="server" EnableViewState="false">
        <ItemTemplate>
            <a href="<%# Master.Master.Root+Eval("AspxPagePath")+"?id="+Master.CourseObject.ID %>">
                <span class="help"></span>
                <span class="icon48 <%#Eval("icon") %>"></span>
                <span class="tt"><%#Eval("Caption") %></span>
            </a>
        </ItemTemplate>
    </asp:Repeater>        
</div>
</asp:Content>

