$(document).ready(function () {
    var userId = {};
    userId.id = $('#userId').val();
    $.ajax({
        url: "UserFigures/GetUserCircumferences",
        data: userId,
        type: "GET",
        async: true,
        success: function (obj) {
            drawChart(obj);
        },
        error: function () {
            swal("Niepowodzenie", "Napotkano nieoczekiwany błąd. Spróbuj ponownie.", "error");
        }
    });
});

COLORS = [
    '#FF3784',
    '#36A2EB',
    '#4BC0C0',
    '#F77825',
    '#9966FF',
    '#00A8C6',
    '#379F7A',
    '#CC2738',
    '#8B628A',
    '#8FBE00',
    '#606060',
    '#bfc4cc'
];

function drawChart(json) {
    var ctx = document.getElementById("myChart");
    var userCircumferences = JSON.parse(json);

    var datesArray = [];
    var shouldersCircumferencesArray = [];
    var chestCircumferencesArray = [];
    var waistCircumferencesArray = [];
    var bicepsCircumferencesArray = [];
    var tricepsCircumferencesArray = [];
    var thighCircumferencesArray = [];
    var hipCircumferencesArray = [];
    var weightsArray = [];
    var bodyFatArray = [];

    for (var i = 0; i < userCircumferences.length; i++) {
        shouldersCircumferencesArray.push(userCircumferences[i].ShouldersCircumference);
        chestCircumferencesArray.push(userCircumferences[i].ChestCircumference);
        waistCircumferencesArray.push(userCircumferences[i].WaistCircumference);
        bicepsCircumferencesArray.push(userCircumferences[i].BicepsCircumference);
        tricepsCircumferencesArray.push(userCircumferences[i].TricepsCircumference);
        thighCircumferencesArray.push(userCircumferences[i].ThighCircumference);
        hipCircumferencesArray.push(userCircumferences[i].HipCircumference);
        weightsArray.push(userCircumferences[i].Weight);
        bodyFatArray.push(userCircumferences[i].BodyFat);

        var date = moment(new Date(userCircumferences[i].Date)).format("DD-MM-YYYY");
        datesArray.push(date);
    }

    var lineChartData = {
        labels: datesArray,
        datasets: [{
            label: "Obwód w barkach [cm]",
            borderColor: COLORS[0],
            backgroundColor: COLORS[0],
            fill: false,
            data: shouldersCircumferencesArray,
            yAxisID: "y-axis-1",
            datalabels: {
                align: 'end',
                anchor: 'end'
            }
        }, {
            label: "Obwód klatki piersiowej [cm]",
            borderColor: COLORS[1],
            backgroundColor: COLORS[1],
            fill: false,
            data: chestCircumferencesArray,
            yAxisID: "y-axis-1",
            datalabels: {
                align: 'end',
                anchor: 'end'
            }
        }, {
            label: "Obwód talii [cm]",
            borderColor: COLORS[2],
            backgroundColor: COLORS[2],
            fill: false,
            data: waistCircumferencesArray,
            yAxisID: "y-axis-1",
            datalabels: {
                align: 'end',
                anchor: 'end'
            }
        }, {
            label: "Obwód bicepsa [cm]",
            borderColor: COLORS[3],
            backgroundColor: COLORS[3],
            fill: false,
            data: bicepsCircumferencesArray,
            yAxisID: "y-axis-1",
            datalabels: {
                align: 'end',
                anchor: 'end'
            }
        }, {
            label: "Obwód tricepsa [cm]",
            borderColor: COLORS[4],
            backgroundColor: COLORS[4],
            fill: false,
            data: tricepsCircumferencesArray,
            yAxisID: "y-axis-1",
            datalabels: {
                align: 'end',
                anchor: 'end'
            }
        }, {
            label: "Obwód uda [cm]",
            borderColor: COLORS[5],
            backgroundColor: COLORS[5],
            fill: false,
            data: thighCircumferencesArray,
            yAxisID: "y-axis-1",
            datalabels: {
                align: 'end',
                anchor: 'end'
            }
        }, {
            label: "Obwód bioder [cm]",
            borderColor: COLORS[6],
            backgroundColor: COLORS[6],
            fill: false,
            data: hipCircumferencesArray,
            yAxisID: "y-axis-1",
            datalabels: {
                align: 'end',
                anchor: 'end'
            }
        }, {
            label: "Waga [kg]",
            borderColor: COLORS[7],
            backgroundColor: COLORS[7],
            fill: false,
            data: weightsArray,
            yAxisID: "y-axis-1",
            datalabels: {
                align: 'end',
                anchor: 'end'
            }
        }, {
            label: "Poziom tkanki tłuszczowej [%]",
            borderColor: COLORS[8],
            backgroundColor: COLORS[8],
            fill: false,
            data: bodyFatArray,
            yAxisID: "y-axis-2",
            datalabels: {
                align: 'end',
                anchor: 'end'
            }
        }]
    };

    var ctx = document.getElementById("myChart").getContext("2d");
    window.myLine = Chart.Line(ctx, {
        data: lineChartData,
        options: {
            responsive: true,
            hoverMode: 'index',
            stacked: false,
            title: {
                padding: 30,
                display: true,
                text: 'Moje pomiary'
            },
            layout: {
                padding: {
                    top: 42,
                    right: 16,
                    bottom: 32,
                    left: 8
                }
            },
            scales: {
                yAxes: [{
                    type: "linear",
                    display: true,
                    position: "left",
                    id: "y-axis-1",
                    ticks: {
                        beginAtZero: true
                    }
                }, {
                    type: "linear",
                    display: true,
                    position: "right",
                    id: "y-axis-2",
                    gridLines: {
                        drawOnChartArea: true,
                        color: COLORS[11]
                    },
                    ticks: {
                        beginAtZero: true
                    },
                    
                }],
            },
            plugins: {
                datalabels: {
                    backgroundColor: function (context) {
                        return context.dataset.backgroundColor;
                    },
                    borderRadius: 6,
                    color: 'white',
                    font: {
                        weight: 'bold'
                    }
                }
            },
            legend: {
                position: 'right'
            }
        }
    });

};