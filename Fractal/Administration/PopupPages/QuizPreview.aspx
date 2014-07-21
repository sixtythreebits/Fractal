<%@ Page Title="" Language="C#" MasterPageFile="~/administration/PopupPages/MasterPage.master" AutoEventWireup="true" Inherits="administration_PopupPages_QuizPreview" Codebehind="QuizPreview.aspx.cs" %>
<%@ Register TagPrefix="uc1" TagName="QuizUserControl" Src="~/plugins/QuizUserControl/QuizUserControl.ascx" %>
<%@ MasterType TypeName="administration_PopupPages_MasterPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div id="pop-content">        
    <div class="popup-quiz-container">
    <uc1:QuizUserControl ID="QuizUserControl1" runat="server" />           
    </div>
</div>
</asp:Content>