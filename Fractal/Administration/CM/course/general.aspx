<%@ Page Title="" Language="C#" MasterPageFile="MasterPage.master" AutoEventWireup="true" Inherits="administration_CM_course_general" ValidateRequest="false" Codebehind="general.aspx.cs" %>
<%@ MasterType TypeName="administration_course_MasterPage" %>
<%@ Reference Control="~/administration/MasterPage.master" %>
<%@ Register Src="~/plugins/SuccessErrorMessageAdmin/SuccessErrorControl.ascx" TagPrefix="uc1" TagName="SuccessErrorControl" %>
<%@ Import Namespace="Core.Properties" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CourseHeadPlaceHolder" runat="Server">
    <script src="<%=Master.Master.Root %>scripts/validation.js"></script>
    <script type="text/javascript" src="<%=Master.Master.Root %>scripts/CM/course/general.aspx.js"></script>
    <asp:Literal ID="HeadLiteral" runat="server" ViewStateMode="Disabled"></asp:Literal>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CourseContentPlaceHolder" runat="Server">
    <uc1:SuccessErrorControl ID="SuccessErrorControl1" runat="server" />
    <div id="general" class="static form-page">
        <div class="sub-title">
            <span class="left"></span>
            <h2>ძირითადი ინფორმაცია</h2>
            <span class="steps"></span>
            <span class="right"></span>
        </div>
        <div class="section">
            <div class="form course-name">
                <label>წიგნის დასახელება *</label>
                <asp:TextBox ID="CaptionTextBox" runat="server" CssClass="custom-form-input" ClientIDMode="Static"></asp:TextBox>
                <span>დასახელება რომელიც გამოჩნდება ვეგ გვერდზე.</span>
            </div>
            <div class="form slug">
                <label>ბრაუზერის მისამართი</label>
                <asp:TextBox ID="SlugTextBox" runat="server" CssClass="custom-form-input" ClientIDMode="Static" MaxLength="50"></asp:TextBox>
                <span>ბრაუზერის მისამართის წიგნის აღმნიშვნელი ნაწილი.</span>
            </div>
            <div class="form">
                <label>აღწერა</label>
                <div class="objectcont">
                    <asp:TextBox ID="DescriptionTextBox" runat="server" TextMode="MultiLine" ClientIDMode="Static"></asp:TextBox>
                </div>
                <span>წიგნის მოკლე აღწერა</span>
            </div>
            <div class="form IsPublished">
                <label>გამოქვეყნება</label>
                <div class="toggler">
                    <div class="pad">
                    </div>
                </div>
                <span>ეს ოფცია გამოაქვეყნებს წიგნს ვებ გვერდზე</span>
            </div>            
            <div class="form course-icon">
                <label>წიგნის ყდა</label>
                <div class="file-upload">
                    <div class="cover">
                        <a></a>
                        <span><asp:Literal ID="ImageLiteral" runat="server"></asp:Literal></span>
                    </div>
                    <asp:FileUpload ID="Uploader" runat="server" ClientIDMode="Static" />
                    <asp:LinkButton ID="ClearImageButton" runat="server" ViewStateMode="Disabled" CssClass="icon text delete-black" Text="სურათის წაშლა" Visible="false" ClientIDMode="Static" OnClick="ClearImageButton_Click"></asp:LinkButton>
                    <span>ატვირთეთ წიგნის ყდა, საჭირო ზომები 140 x 160 (სიგანე x სიმაღლე)</span>
                </div>
                <span></span>
            </div>
        </div>
                    
        <div id="fixed-save">
            <div>
                <asp:Button ID="SaveButton" runat="server" Text="შენახვა" CssClass="submit" OnClick="SaveButton_Click" ClientIDMode="Static" />
            </div>
        </div>
        
        <asp:HiddenField ID="HFIsPublished" runat="server" ClientIDMode="Static" />        
        <input type="hidden" id="HFRequiredCourseCaption" value="<%=Resources.RequiredCourseCaption %>" />        
        <input type="hidden" id="HFWrongUploadedImageType" value="<%=Resources.WrongUploadedImageType %>" />
        <input type="hidden" id="HFInvalidSlug" value="<%=Resources.UniqCourseSlug %>" />
    </div>
</asp:Content>

