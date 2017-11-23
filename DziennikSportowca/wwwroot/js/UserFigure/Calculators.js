$(document).ready(function () {
    $('[data-toggle="tooltip"]').tooltip();

    $('#heightBMI').on("change paste keyup", function () {
        clearBMI();
        var userHeight = $(this).val();
        var userWeight = $('#weightBMI').val();
        calculateUserBMI(userHeight, userWeight);
    });

    $('#weightBMI').on("change paste keyup", function () {
        clearBMI();
        var userWeight = $(this).val();
        var userHeight = $('#heightBMI').val();
        calculateUserBMI(userHeight, userWeight);
    });

    function clearBMI() {
        if (!$('#heightBMI').val() || !$('#weightBMI').val()) {
            $('#bmiResult').val('');
        }
    }

    /*-----------------------------------------------------------------------*/

    $('#heightPMC').on("change paste keyup", function () {
        var userHeight = $(this).val();
        calculateUserPMC(userHeight);
        clearPMC();
    });

    function clearPMC() {
        if (!$('#heightPMC').val()) {
            $('#pmcResult').val('');
        }
    }

    /*-----------------------------------------------------------------------*/

    $('#heightPPM').on("change paste keyup", function () {
        var userHeight = $(this).val();
        var userWeight = $('#weightPPM').val();
        var userGender = $('#genderPPM :selected').text();
        var userAge = $('#agePPM').val();
        calculateUserPPM(userGender, userWeight, userHeight, userAge);
        clearPPM();
    });

    $('#weightPPM').on("change paste keyup", function () {
        var userHeight = $('#heightPPM').val();
        var userWeight = $(this).val();
        var userGender = $('#genderPPM :selected').text();
        var userAge = $('#agePPM').val();
        calculateUserPPM(userGender, userWeight, userHeight, userAge);
        clearPPM();
    });

    $('#agePPM').on("change paste keyup", function () {
        var userHeight = $('#heightPPM').val();
        var userWeight = $('#weightPPM').val();
        var userGender = $('#genderPPM :selected').text();
        var userAge = $(this).val();
        calculateUserPPM(userGender, userWeight, userHeight, userAge);
        clearPPM();
    });

    $('#genderPPM').change(function () {
        var userHeight = $('#heightPPM').val();
        var userWeight = $('#weightPPM').val();
        var userGender = $(this).find(':selected').text();
        var userAge = $('#agePPM').val();
        calculateUserPPM(userGender, userWeight, userHeight, userAge);
        clearPPM();
    });

    function clearPPM() {
        if (!$('#heightPPM').val() || !$('#weightPPM').val() || !$('#agePPM').val() || $('#genderPPM :selected').text() === 'Wybierz płeć') {
            $('#ppmResult').val('');
        }
    }

    /*-----------------------------------------------------------------------*/

    $('#heightCPM').on("change paste keyup", function () {
        var userHeight = $(this).val();
        var userWeight = $('#weightCPM').val();
        var userAge = $('#ageCPM').val();
        var userActivity = parseFloat($('#userActivityCPM').find(':selected').val());
        var userGender = $('#genderCPM').find(':selected').text();
        calculateUserCPM(userGender, userWeight, userHeight, userAge, userActivity);
        clearCPM();
    });

    $('#weightCPM').on("change paste keyup", function () {
        var userHeight = $('#weightCPM').val();
        var userWeight = $(this).val();
        var userAge = $('#ageCPM').val();
        var userActivity = parseFloat($('#userActivityCPM').find(':selected').val());
        var userGender = $('#genderCPM').find(':selected').text();
        calculateUserCPM(userGender, userWeight, userHeight, userAge, userActivity);
        clearCPM();
    });

    $('#ageCPM').on("change paste keyup", function () {
        var userHeight = $('#weightCPM').val();
        var userWeight = $('#weightCPM').val();
        var userAge = $(this).val();
        var userActivity = parseFloat($('#userActivityCPM').find(':selected').val());
        var userGender = $('#genderCPM').find(':selected').text();
        calculateUserCPM(userGender, userWeight, userHeight, userAge, userActivity);
        clearCPM();
    });

    $('#userActivityCPM').change(function () {
        var userHeight = $('#weightCPM').val();
        var userWeight = $('#weightCPM').val();
        var userAge = $('#ageCPM').val();
        var userActivity = parseFloat($(this).find(':selected').val());
        var userGender = $('#genderCPM').find(':selected').text();
        calculateUserCPM(userGender, userWeight, userHeight, userAge, userActivity);
        clearCPM();
    });

    $('#genderCPM').change(function () {
        var userHeight = $('#weightCPM').val();
        var userWeight = $('#weightCPM').val();
        var userAge = $('#ageCPM').val();
        var userActivity = parseFloat($('#userActivityCPM').find(':selected').val());
        var userGender = $(this).find(':selected').text();
        calculateUserCPM(userGender, userWeight, userHeight, userAge, userActivity);
        clearCPM();
    });

    function clearCPM() {
        if (!$('#heightCPM').val() || !$('#weightCPM').val() || !$('#ageCPM').val() || !$('#userActivityCPM').val() || $('#genderCPM :selected').text() === 'Wybierz płeć') {
            $('#cpmResult').val('');
        }
    }

});

function calculateUserBMI(userHeight, userWeight) {
    var bmi = userWeight / (userHeight * userHeight) * 10000;
    if (bmi < 0)
        bmi = 0;
    
    bmi = bmi.toPrecision(4).toString().replace(',','.');
    $('#bmiResult').val(bmi);
};

function calculateUserPMC(userHeight) {
    var pmc = 50 + 0.75 * (userHeight - 150);
    if (pmc < 0) {
        pmc = 0;
    }
    pmc = pmc.toPrecision(4).toString().replace(',', '.');
    $('#pmcResult').val(pmc);
};

function calculateUserPPM(userGender, userWeight, userHeight, userAge) {
    var ppm = 0;
    if (userGender === "Kobieta") {
        ppm = 665.09 + 9.56 * userWeight + 1.85 * userHeight - 4.67 * userAge;
    }
    else if (userGender === "Mężczyzna") {
        ppm = 66.47 + 13.75 * userWeight + 5 * userHeight - 6.75 * userAge;
    }
    if (ppm < 0) {
        ppm = 0;
    }
    ppm = ppm.toPrecision(4).toString().replace(',', '.');
    $('#ppmResult').val(ppm);
};

function calculateUserCPM(userGender, userWeight, userHeight, userAge, userActivity) {
    var cpm = 0;
    if (userGender === "Kobieta") {
        cpm = 665.09 + 9.56 * userWeight + 1.85 * userHeight - 4.67 * userAge;
    }
    else if (userGender === "Mężczyzna") {
        cpm = 66.47 + 13.75 * userWeight + 5 * userHeight - 6.75 * userAge;
    }
    cpm = cpm * userActivity;
    if (cpm < 0) {
        cpm = 0;
    }
    cpm = cpm.toPrecision(4).toString().replace(',', '.');
    $('#cpmResult').val(cpm);
};