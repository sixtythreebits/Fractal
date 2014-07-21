<%@ Page Title="" Language="C#" MasterPageFile="../../MasterPage.master" AutoEventWireup="true" Inherits="administration_CM_quiz" Codebehind="quiz.aspx.cs"  ValidateRequest="false" %>
<%@ MasterType TypeName="administration_MasterPage" %>
<%@ Register src="~/plugins/QuizMaker/QuizMakerControl.ascx" tagname="QuizMakerControl" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script src="/Administration/scripts/validation.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:QuizMakerControl ID="QuizMakerControl1" runat="server" />
</asp:Content>