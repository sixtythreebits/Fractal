<%@ Page Title="" Language="C#" MasterPageFile="../MasterPage.master" AutoEventWireup="true" Inherits="administration_CM_course_quizes" Codebehind="quizes.aspx.cs" %>
<%@ MasterType TypeName="administration_course_MasterPage" %>
<%@ Reference Control="~/administration/MasterPage.master" %>
<%@ Import Namespace="Core.Properties" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CourseHeadPlaceHolder" Runat="Server">    
    <script type="text/javascript" src="/administration/scripts/CM/course/quizes.aspx.js"></script>    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CourseContentPlaceHolder" Runat="Server">

<div id="quizes" class="static">
	<div class="main-title">
		<span class="left"></span>        
		<a href="#" class="icon text add hidden">ტესტის დამატება</a>
		<input  class="search" type="text"/>
		<span class="right"></span>
	</div>
	<ul class="type1">
        <asp:Repeater ID="QuizesRepeater" runat="server" EnableViewState="false" ItemType="Core.CourseQuiz">
            <ItemTemplate>
                <li id="<%# Item.ID %>">
		            <span class="top"></span>
		            <div class="content">
			            <h2>დასახელება და გამოქვეყნების თარიღები</h2>
			            <h1><%# Item.Caption %></h1>
			            <span><%# Item.StartDate.Value.ToString(Resources.FormatDateTime) %> - <%# Item.EndDate.Value.ToString(Resources.FormatDateTime) %></span>
			            <div>				            
				            <div class="n2">
					            <h2>სტატუსი</h2>
                                <%# Item.IsPublished ?"<span class=\"icon text circle-green\">გამოქვეყნებული</span>":"<span class=\"icon text  circle-red\">დამალული</span>" %>
                                <%# Item.IsPractice ? "<span class=\"icon text edit\">Practice</span>" : ""%>
                                <%# Item.IsPractice || Item.GradeReleaseDate <= DateTime.Now ?"<br /><span class=\"icon text success\">Show Answers</span>":"" %>					            
				            </div>
                            <asp:PlaceHolder ID="SectionPlaceHolder" runat="server" EnableViewState="false" Visible='<%# !string.IsNullOrEmpty(Item.Section) %>'>
                            <div class="n3"><h2>attached to</h2><p> <%# Item.Section %></p></div>
                            </asp:PlaceHolder>
                            
				            
				            <div class="n4">
					            <h2>ქმედებები</h2>
                                <asp:PlaceHolder ID="EditButtonPlaceHolder" runat="server" EnableViewState="false" Visible='<%#!Item.IsPractice %>'>
					            <a href="quiz.aspx?id=<%=Master.CourseObject.ID %>&qid=<%# Item.ID %>" class="icon edit" title="რედაქტირება"></a>
                                </asp:PlaceHolder>
					            <a href="#" class="icon delete" title="წაშლა"></a>
				            </div>
			            </div>
		            </div>
		            <span class="bottom"></span>
		        </li>
            </ItemTemplate>
        </asp:Repeater>		
	</ul>					
</div>
<input id="HFNewURL" type="hidden" value="<%=string.Format("{0}PopupPages/CM/course/QuizToCourse.aspx?cid={1}&title=Add Quiz",Master.Master.Root, Master.CourseObject.ID) %>" />
<input id="HFQC" type="hidden" value="<%=QuizCount %>" />
</asp:Content>

