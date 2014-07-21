<%@ Page Title="" Language="C#" MasterPageFile="../../MasterPage.master" AutoEventWireup="true" Inherits="administration_PopupPages_CM_course_QuizToCourse" Codebehind="QuizToCourse.aspx.cs" %>
<%@ MasterType  TypeName="administration_PopupPages_MasterPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">    
<script type="text/javascript" src="/administration/scripts/validation.js"></script>
<script type="text/javascript" src="/scripts/PopupPages/CM/course/QuizToCourse.aspx.js"></script>
<asp:Literal ID="HeadLiteral" runat="server" EnableViewState="false"></asp:Literal>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div class="pop-menu">
    <asp:LinkButton ID="SaveButton" runat="server" Text="შენახვა" CssClass="icon text save" EnableViewState="false" OnClick="SaveButton_Click"></asp:LinkButton>	
</div>    
<div class="page-title">
    <h2>ყველა ველი აუცილებელია</h2>
</div>
<div class="form">
    <label>არჩევანი</label>
    <div class="objectcont radio">
        <asp:RadioButton ID="NewQuizRadioButton" runat="server" Text="ახალი ტესტი" ClientIDMode="Static" GroupName="option" Checked="true" />
        <asp:RadioButton ID="ExistringQuizRadioButton" runat="server" Text="არსებული ტესტი" ClientIDMode="Static" GroupName="option"/>
    </div>
    <span>თქვენი ვარიანტი</span>
</div>
<div class="form new-quiz">
	<label>დასახელება</label>	
    <asp:TextBox ID="QuizCaptionTextBox" runat="server" ClientIDMode="Static" CssClass="custom-form-input"></asp:TextBox>		
	<span>ტესტის დასახელება</span>	
</div>
<div class="form quizes">
	<label>ტესტები</label>
    <div class="select">
        <p></p>
        <asp:DropDownList ID="QuizesCombo" runat="server" ClientIDMode="Static" CssClass="custom-form-select" DataTextField="Caption" DataValueField="ID" DataSourceID="QuizzesDataSource" OnDataBound="QuizesCombo_DataBound">
        </asp:DropDownList>
    </div>
    <asp:ObjectDataSource ID="QuizzesDataSource" runat="server" TypeName="Core.Quiz" SelectMethod="ListTeacherQuizesNotInCourse">
        <SelectParameters>
            <asp:Parameter Name="UserID" Type="Int64" />
            <asp:Parameter Name="CourseID" Type="Int64" />            
        </SelectParameters>
    </asp:ObjectDataSource>
	<span>Choose quiz</span>	
</div>
<div class="form publish">
	<label>გამოქვეყნებული</label>
	<div class="toggler">
		<div class="pad">
		</div>
	</div>
	<span>ტესტის გამოქვეყნება</span>
</div>
<div class="form date-picker start-date">
	<label>საწყისი თარიღი</label>	
    <asp:TextBox ID="StartDateTextBox" runat="server" ClientIDMode="Static" CssClass="custom-form-input"></asp:TextBox>	
	<a href="#" class="calendar icon"></a>
	<span>ტესტის დაწყების თარიღი</span>	
</div>
<div class="form date-picker end-date">
	<label>დასრულების თარიღი</label>	
    <asp:TextBox ID="EndDateTextBox" runat="server" ClientIDMode="Static" CssClass="custom-form-input"></asp:TextBox>	
	<a href="#" class="calendar icon"></a>
	<span>ტესტი დასრულების თარიღი</span>	
</div>
<asp:HiddenField ID="IsPublishedHF" runat="server" ClientIDMode="Static" />
<input type="hidden" id="HFRequiredQuiz" value="<%=Core.Properties.Resources.RequiredQuiz %>" />
<input type="hidden" id="HFRequiredQuizCaption" value="<%=Core.Properties.Resources.RequiredQuizCaption %>" />    
<input type="hidden" id="HFRequiredStartDate" value="<%=Core.Properties.Resources.RequiredStartDate %>" />
<input type="hidden" id="HFRequiredEndDate" value="<%=Core.Properties.Resources.RequiredEndDate %>" />
<input type="hidden" id="HFStartGreaterThanEnd" value="<%=Core.Properties.Resources.StartGreaterThanEnd %>" />
<input type="hidden" id="HFIsEditMode" value="<%=(!string.IsNullOrEmpty(Request.QueryString["id"])).ToString().ToLower() %>" />
</asp:Content>
<asp:Content ID="Content3" runat="server" ContentPlaceHolderID="ScriptsPlaceHolder">
<script type="text/javascript">
var RequiredQuiz;
var RequiredQuizCaption;
var RequiredStartDate;
var RequiredEndDate;
var StartGreaterThanEnd;

var QuizError = null;
var StartDateError = null;
var EndDateError = null;

$(function (e) {

    RequiredQuiz = $("#HFRequiredQuiz").val();
    RequiredQuizCaption = $("#HFRequiredQuizCaption").val();
    RequiredStartDate = $("#HFRequiredStartDate").val();
    RequiredEndDate = $("#HFRequiredEndDate").val();
    StartGreaterThanEnd = $("#HFStartGreaterThanEnd").val();

    initTemplateByRadio();

    $(".objectcont.radio input").click(function (e) {
        initTemplateByRadio();
        QuizError = null;
    });

    $("#QuizCaptionTextBox").focusout(function () {
        ValidateQuiz();
    });
    
    $(".form.date-picker input").attr("readonly", "readonly");

    $(".form.date-picker input").datetimepicker(
    {
        dateFormat: "M d, yy",
        timeFormat: "hh:mm tt",
        changeMonth: true,
        changeYear: true
    });

    $(".form.date-picker a").click(function (e) {
        e.preventDefault();
        $(this).parent().children("input").datetimepicker("show");
    });

    $(".toggler").change(function () {
        var part = $(this).parent().attr("class").split(" ");
        var state = $(this).attr("class").split(" ").length > 1 ? "true" : "";

        switch (part[1]) {
            case "publish":
                {
                    $("#IsPublishedHF").val(state);
                    break;
                }    
        }

    });

    $("#StartDateTextBox").focusout(function () {
        setTimeout("ValidateStartDate();", 200);
    });

    $("#StartDateTextBox").change(function () {
        setTimeout("ValidateStartDate();", 200);
    });

    $("#EndDateTextBox").focusout(function () {
        setTimeout("ValidateEndDate();", 200);
    });

    $("#EndDateTextBox").change(function () {
        setTimeout("ValidateEndDate();", 200);
    });

    $(".save").click(function (e) {
        if (IsFormValid()) {
            GMLoader.open();
        }
        else {
            e.preventDefault();
        }
    });

    InitStartUp();
});

function initTemplateByRadio() {
    if ($("#NewQuizRadioButton").is(":checked")) {
        $(".form.new-quiz").Show();
        $(".form.quizes").Hide();
    }
    else {
        $(".form.new-quiz").Hide();
        $(".form.quizes").Show();
    }
}

function InitStartUp() {

    if ($("#HFIsEditMode").val() == "true") {
        $(".form.quizes").fadeTo("fast", 0.5);
    }
    
    if ($("#IsPublishedHF").val() == "true") {
        customForm.toggleIt($(".form.publish").children("div.toggler"), 180);
    }    
}

function ValidateQuiz() {
    
    if ($("#NewQuizRadioButton").is(":checked")) {
        var val = $("#QuizCaptionTextBox").val()
        if (val.length > 0) {
            SetInputFieldSuccess("new-quiz", "QuizError");
        }
        else {
            SetInputFieldError("new-quiz", RequiredQuizCaption, "QuizError");
        }
    }
    else {
        var val = $("#QuizesCombo").val()
        if (parseInt(val) > 0) {
            SetInputFieldNonVisualSuccess("quizes", "QuizError");
        }
        else {
            SetInputFieldError("quizes", RequiredQuiz, "QuizError");
        }
    }
}

function ValidateStartDate() {
    var d1 = $("#StartDateTextBox").val();
    var d2 = $("#EndDateTextBox").val();

    if (d1.length == 0) {
        SetInputFieldError("start-date", RequiredStartDate, "StartDateError");
    }
    else {
        if (d2.length > 0) {
            SetInputFieldSuccess("start-date", "StartDateError");
            ValidateEndDate();
        }
        else {
            SetInputFieldSuccess("start-date", "EndDateError");
        }
        SetInputFieldSuccess("start-date", "StartDateError");
    }
}

function ValidateEndDate() {
    var d1 = $("#StartDateTextBox").val();
    var d2 = $("#EndDateTextBox").val();

    if (d2.length == 0) {
        SetInputFieldError("end-date", RequiredEndDate, "EndDateError");
    }
    else {
        d1 = new Date(d1);
        d2 = new Date(d2);
        if (d1 > d2) {
            SetInputFieldError("end-date", StartGreaterThanEnd, "EndDateError");
        }
        else {
            SetInputFieldSuccess("end-date", "EndDateError");
        }
    }
}

function IsFormValid() {
    if (QuizError == null) {
        ValidateQuiz();
    }
    
    if (StartDateError == null) {
        ValidateStartDate();
    }

    if (EndDateError == null) {
        ValidateEndDate();
    }

    return QuizError == false && StartDateError == false && EndDateError == false;
}

</script>
</asp:Content>