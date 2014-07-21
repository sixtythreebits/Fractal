<%@ Page Title="" Language="C#" MasterPageFile="MasterPage.master" AutoEventWireup="true" Inherits="administration_CM_course_QuizResults" Theme="Office2010Silver" Codebehind="QuizResults.aspx.cs" %>
<%@ MasterType TypeName="administration_course_MasterPage" %>
<%@ Reference Control="~/administration/MasterPage.master" %>
<%@ Register Assembly="DevExpress.Web.v14.1, Version=14.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CourseHeadPlaceHolder" Runat="Server">    
    <script type="text/javascript" src="<%=Master.Master.Root %>scripts/CM/course/QuizResults.aspx.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CourseContentPlaceHolder" Runat="Server">
<div class="static">
    <div class="main-title">
	    <span class="left"></span>        
        <div class="datepicker">
            <a>Start Date</a>
            <asp:TextBox ID="D1TextBox" runat="server" class="from" ClientIDMode="Static"></asp:TextBox>
		    <p>-</p>
            <a>End Date</a>
            <asp:TextBox ID="D2TextBox" runat="server" class="to" ClientIDMode="Static"></asp:TextBox>
	    </div>
        <asp:TextBox ID="SearchTextBox" runat="server" CssClass="search" ClientIDMode="Static"></asp:TextBox>
	    <span class="right"></span>
    </div>          
    <div class="quiz-result-container">
    <dx:ASPxGridView ID="QuizResultsGrid" ClientInstanceName="QuizResultsGrid" runat="server" KeyFieldName="ID" DataSourceID="QuizResultsDataSource" Width="800px" OnBeforePerformDataSelect="QuizResultsGrid_BeforePerformDataSelect">        
        <Columns>
        <dx:GridViewDataColumn Caption="item">
            <DataItemTemplate>
                <div class="course-quiz-results">
                    <div class="div1">
                        <%#Eval("UserFullName") %>
                    </div>
                    <div class="div2">                    
                        <a href="<%= Master.Master.Root %>PopupPages/Quiz.aspx?id=<%#Eval("ID") %>&user=<%#Eval("UserID") %>&title=<%#Eval("Caption")%>"><%#Eval("Caption")%></a><br />                        
                    </div>
                    <div class="div3">
                        <span class="icon text coingold"><b><%#Eval("Score","{0:n0}") %></b>/<%#Eval("MaxScore","{0:n0}")%></span></div>
                    <div class="div4">
                        <%#Eval("CRTime","{0:"+General.Properties.Resources.FormatDateTime+"}") %>
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
    <asp:ObjectDataSource ID="QuizResultsDataSource" runat="server" SelectMethod="ListQuizAttempts" TypeName="Core.Quiz">
        <SelectParameters>
            <asp:Parameter Name="iud" Type="Byte" DefaultValue="2" />
            <asp:Parameter Name="filter" Type="Object" />
        </SelectParameters>
    </asp:ObjectDataSource>        
    </div>
</div>
</asp:Content>

