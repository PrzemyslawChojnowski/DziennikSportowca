$(document).ready(function () {
    $('#datepicker').datepicker({
        autoclose: true,
        calendarWeeks: true,
        clearBtn: true,
        format: "dd/mm/yyyy",
        language: "pl",
        todayBtn: "linked",
        todayHighlight: true
    });

    var userGender;
    var userWeight;
    var userWaistCircumference;

    $('#Weight').on("change paste keyup", function () {
        userWeight = $(this).val();
        calculateBodyFat(userWeight, userWaistCircumference, userGender);
    });

    $('#WaistCircumference').on("change paste keyup", function () {
        userWaistCircumference = $(this).val();
        calculateBodyFat(userWeight, userWaistCircumference, userGender);
    });

    $.ajax({
        url: '../getUserGender',
        type: 'GET',
        async: true,
        success: function (obj) {
            userGender = obj;
        },
        error: function (obj) {
            swal("Niepowodzenie", "Napotkano nieoczekiwany błąd. Spróbuj ponownie.", "error");
        }
    });    
});

function calculateBodyFat(userWeight, userWaistCircumference, userGender) {
    if (userWeight && userWaistCircumference) {
        var a = 4.15 * userWaistCircumference;
        var b = a / 2.54;
        var c = 0.082 * userWeight * 2.2;
        var d;
        if (userGender === 'Man') {
            d = b - c - 98.42;
        }
        else if (userGender === 'Woman') {
            d = b - c - 76.76;
        }
        var e = userWeight * 2.2;
        var result = d / e * 100;
        if (result < 0)
            result = 0;
        else if (result > 100)
            result = 100;
        result = result.toFixed(2).toString().replace('.', ',');
        $('#BodyFat').val(result);
    }
}