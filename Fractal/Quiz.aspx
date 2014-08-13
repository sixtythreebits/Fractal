<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Quiz.aspx.cs" Inherits="Fractal.QuizPage" %>
<%@ MasterType VirtualPath="~/MasterPage.Master" %>
<%@ Register src="plugins/QuizUserControl/QuizUserControl.ascx" tagname="QuizUserControl" tagprefix="uc1" %>
<%@ Import Namespace="Core" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<section class="cnt">
<article>            
    <h1><asp:Literal ID="QuizCaptionLiteral" runat="server"></asp:Literal></h1>
</article>
<uc1:QuizUserControl ID="QuizUserControl1" runat="server" />
</section>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptsPlaceHolder" runat="server">    
</asp:Content>
