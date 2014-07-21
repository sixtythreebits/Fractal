$(document).ready(function () {

    $(".select").setSelectInteraction();

    $(".datepicker input").datepicker({
        dateFormat: "M dd, y",
        onSelect: function (dateText, inst) {
            SubscriptionsGrid.Refresh();
        }
    });

    $(".datepicker input").change(function (e) {
        if ($(this).val().length == 0) {
            SubscriptionsGrid.Refresh();
        }
    });

    $("#SearchTextBox").keyup(function (e) {       
        if (e.which == 13) {
            SubscriptionsGrid.Refresh();
        }
    });

    $("#SubscriptionTypesCombo").change(function (e) {
        SubscriptionsGrid.Refresh();
    });    
});