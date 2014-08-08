<%@ Page Title="" Language="C#" MasterPageFile="../MasterPage.master" AutoEventWireup="true" Inherits="administration_CM_courses" Codebehind="courses.aspx.cs" %>

<%@ MasterType TypeName="administration_MasterPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">    
    <script type="text/javascript" src="<%=Master.Root %>scripts/layout.js"></script>
    <script type="text/javascript" src="<%=Master.Root %>scripts/CM/courses.aspx.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div class="main-title">
	<span class="left"></span>
    <a href="#" class="icon text add">ახლის დამატება</a>   
    <input  class="search" type="text" /> 
    <span class="right"></span>
</div>
<div id="course-list">
    <asp:Repeater ID="LettersRepeater" runat="server" EnableViewState="false" OnItemDataBound="LettersRepeater_ItemDataBound">
        <ItemTemplate>
        <div class="section">
	    <h2><%# Container.DataItem %></h2>
        <asp:Repeater ID="CoursesRepeater" runat="server" EnableViewState="false">
            <HeaderTemplate>
            <ul>
            </HeaderTemplate>
            <ItemTemplate>
            <li id="<%#Eval("ID") %>">                
                <div class="hover-menu">
                    <ul>
                        <li><a class="icon rename" title="rename course"></a></li>
                        <li><a class="icon delete" title="archive course"></a></li>
                    </ul>
                </div>
			    <div class="cover">
                    <a href="course/default.aspx?id=<%#Eval("ID") %>"></a>
				    <span><%#string.IsNullOrEmpty((string)Eval("Icon"))?string.Empty:"<img src=\""+ Core.Utilities.AppSettings.UploadFolderHttpPath + Eval("Icon")+"\" alt=\"Course Image\" height='160' />" %></span>
			    </div>
			    <a href="course/general.aspx?id=<%#Eval("ID") %>"><%#Eval("Caption") %></a>                
		    </li>
            </ItemTemplate>
            <FooterTemplate>
            </ul>
            </FooterTemplate>
        </asp:Repeater>
        </div>
        </ItemTemplate>
    </asp:Repeater>
    <asp:Literal ID="HFLiteral" runat="server" EnableViewState="false"></asp:Literal>
</div>
</asp:Content>

