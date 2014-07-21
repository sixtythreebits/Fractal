<%@ Page Title="" Language="C#" MasterPageFile="..//MasterPage.master" AutoEventWireup="true" Inherits="administration_CM_course_quiz_quiz" Codebehind="quiz.aspx.cs" %>
<%@ MasterType TypeName="administration_course_MasterPage" %>
<%@ Reference Control="../../../../administration/MasterPage.master" %>
<%@ Register src="../../../../plugins/QuizMaker/QuizMakerControl.ascx" tagname="QuizMakerControl" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CourseHeadPlaceHolder" Runat="Server">
    <script src="/Administration/scripts/validation.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CourseContentPlaceHolder" Runat="Server">
    <uc1:QuizMakerControl ID="QuizMakerControl1" runat="server" />
</asp:Content>

