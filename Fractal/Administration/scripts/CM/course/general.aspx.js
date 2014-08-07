var RequiredName;
var WrongFileType;
var InvalidSlug;

var NameError = null;
var SlugError = null;
var IconError = null;

var Uploader;

$(document).ready(function () {

    RequiredName = $("#HFRequiredCourseCaption").val();    
    WrongFileType = $("#HFWrongUploadedImageType").val();
    InvalidSlug = $("#HFInvalidSlug").val();

    Uploader = $("#Uploader").clone(true);

    $("#SlugTextBox").keyup(function () {
        $(this).val($(this).ToSlug());
    });

    $("#SlugTextBox").change(function () {
        $(this).val($(this).ToSlug());       
    });

    $("#SlugTextBox").focusout(function () {
        ValidateSlug();
    });

    $('#DescriptionTextBox').tinymce({
        width: "100%",
        height: 2100,
        theme: "advanced",
        theme_advanced_buttons1: "bold,italic,underline,forecolor",
        theme_advanced_buttons2: "",
        theme_advanced_buttons3: "",

        forced_root_block: false,
        force_p_newlines: 'false',
        remove_linebreaks: false,
        force_br_newlines: true,
        remove_trailing_nbsp: false,
        verify_html: false
    });

    SetTogglers();

   

    $(".toggler").change(function () {
        var part = $(this).parent().attr("class").split(" ");
        var state = $(this).attr("class").split(" ").length > 1 ? "true" : "";

        switch (part[1]) {
            case "IsPublished":
                {
                    $("#HFIsPublished").val(state);
                    break;
                }
        }
    });


    $("#CaptionTextBox").focusout(function (e) {
        ValidateCourseName();
    });

    $("#YearTextBox").focusout(function (e) {
        ValidateYear();
    });            
    
    $(document).on('change', '#Uploader', function () {
        ValidateIcon();
        $("#ClearImageButton").text("Clear Upload Field");
    });

    $("#SaveButton").click(function (e) {

        if (!IsFormValid()) {
            return false;
        }
        else {
            setTimeout("DisableSaveButton();", 1000);
        }
    });


    $("#ClearImageButton").click(function (e) {
        if ($("#Uploader").val().length > 0) {
            e.preventDefault();
            $("#ClearImageButton").text("Clear Image");

            $("#Uploader").replaceWith(Uploader);
            ClearIndicatorFields("course-icon");
            IconError = false;
        }
    });
});

function ValidateCourseName() {
    var val = $("#CaptionTextBox").val();
    if (val.length == 0) {
        SetInputFieldError("course-name", RequiredName, "NameError");
    }
    else {
        SetInputFieldSuccess("course-name", "NameError");
    }
}

function ValidateSlug() {
    var val = $("#SlugTextBox").val();
    if (val.length > 0) {
        $.ajax({
            url: document.URL,
            type: "POST",
            data: { action: "validate_slug", slug: val },
            dataType: "text",
            beforeSend: function () {
                SetInputFieldLoading("slug");
            },
            success: function (res) {
                if (res == "success") {
                    SetInputFieldSuccess("slug", "SlugError");
                }
                else {
                    SetInputFieldError("slug", InvalidSlug, "SlugError");
                }
            },
            error: function (res) {
                SetInputFieldError("slug", InvalidSlug, "SlugError");
            }
        });
    }
    else {
        SetInputFieldSuccess("slug", "SlugError");
    }
}

function ValidateIcon() {

    var val = $("#Uploader").val();
    if (val.length > 0) {
        var part = val.toString().split(".");
        var ext = part[part.length - 1].toString().toLowerCase();
        if (ext == "jpg" || ext == "jpeg" || ext == "gif" || ext == "png") {
            SetInputFieldSuccess("course-icon", "IconError");
        }
        else {
            SetInputFieldError("course-icon", WrongFileType, "IconError");
        }
    }
    else {
        ClearIndicatorFields("course-icon");
        IconError = false;
    }

}

function IsFormValid() {
    if (NameError == null) {
        ValidateCourseName();
    }

    if (YearError == null) {
        ValidateYear();
    }

    if (IconError == null) {
        ValidateIcon();
    }

    if (PromoImageError == null) {
        ValidatePromoImage();
    }

    return NameError == false && IconError == false && (SlugError != true);
}

function SetTogglers() {

    if ($("#HFIsPublished").val() == "true") {
        customForm.toggleIt($(".form.IsPublished").children("div.toggler"), 180);
    }   
}

function DisableSaveButton() {
    $("#SaveButton").addClass("disabled");
    $("#SaveButton").attr("disabled", "disabled");
    $("#SaveButton").css("cursor", "default");
}
