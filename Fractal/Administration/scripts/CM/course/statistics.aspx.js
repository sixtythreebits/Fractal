var ChartHeight = 300;

$(function () {
    $(".datepicker input").datepicker({
        dateFormat: "M dd, y",
        changeMonth: true,
        changeYear: true,
        onSelect: function () {
            HistoryGrid.Refresh();
            RequestChartData(true);
        }
    });

    RequestChartData(false);
    $(".container.charts > div > div > div").css({ "height": ChartHeight });
    
});

function RequestChartData(ShowLoader) {
    $.ajax({
        url: document.URL,
        type: "POST",
        data: { action: "filter", StartDate: $("#D1TextBox").val(), EndDate: $("#D2TextBox").val() },
        dataType: "text",
        beforeSend: function () {
            $(".container.charts > div > div > div").ShowLoader();
        },
        success: function (res) {
            $("#HFData").val(res);
            initializeCharts();
        },
        complete: function () {
            $(".container.charts > div > div > div").HideLoader();
        }
    });
}

function initializeCharts() {
    var data = JSON.parse($("#HFData").val());

    initializeChart($("#TotalChart"), data.TotalActions, "Quantity", "spline", '#1E90FF');
    initializeChart($("#CourseVisits"), data.CourseVisits, "Quantity", "spline", 'purple');
    initializeChart($("#SectionVisits"), data.SectionVisits, "Quantity", "spline", 'green');
    initializeChart($("#FileDownloads"), data.FileDownloads, "Quantity", "spline", 'blue');
    initializeChart($("#VideoTimeSpent"), data.VideoTimeSpent, "TimeSpent", "spline", 'red');
}

function initializeChart(chartDiv, data, valueField, chartType, color) {

    chartDiv.dxChart({
        dataSource: data,
        title: {
            font: { weight: 200, size: 18 },
            text: $("#D1TextBox").datepicker("getDate").format("mmmm d, yyyy") + " - " + $("#D2TextBox").datepicker("getDate").format("mmmm d, yyyy")
        },
        commonSeriesSettings: {
            argumentField: 'ArgumentLabel',
            type: chartType,
            point: {
                visible: true
            }
        },
        size: {
            height: ChartHeight
        },
        series: [{
            name: valueField,
            valueField: valueField,
            color: color,
            label: {
                visible: true,
                connector: {
                    visible: true,
                    width: 1
                },
                font: {
                    weight: "bold"
                }
            },
        }],
        legend: {
            visible: false
        },
        argumentAxis: {
            label: {
                visible: true
            },
            grid: {
                visible: true
            }
        },
        commonPaneSettings: {
            border: {
                visible: true
            }
        },
        tooltip: {
            enabled: true
        }
    });
}