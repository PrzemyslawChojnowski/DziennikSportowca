$(document).ready(function () {
    var userWeight = $('#weightBMI').val();
    var userGender = $('select.gender option').filter(':selected').first().text();
    var userHeight;
    var userAge;
    var userActivity;

    $('[data-toggle="tooltip"]').tooltip();

    $('#heightBMI').on("change paste keyup", function () {
        userHeight = $(this).val();
        calculateUserBMI(userHeight, userWeight);
    });

    $('#weightBMI').on("change paste keyup", function () {
        userWeight = $(this).val();
        calculateUserBMI(userHeight, userWeight);
    });

    /*-----------------------------------------------------------------------*/

    $('#heightPMC').on("change paste keyup", function () {
        userHeight = $(this).val();
        calculateUserPMC(userHeight);
    });

    /*-----------------------------------------------------------------------*/

    $('#heightPPM').on("change paste keyup", function () {
        userHeight = $(this).val();
        calculateUserPPM(userGender, userWeight, userHeight, userAge);
    });

    $('#weightPPM').on("change paste keyup", function () {
        userWeight = $(this).val();
        calculateUserPPM(userGender, userWeight, userHeight, userAge);
    });

    $('#agePPM').on("change paste keyup", function () {
        userAge = $(this).val();
        calculateUserPPM(userGender, userWeight, userHeight, userAge);
    });

    $('#genderPPM').change(function () {
        userGender = $(this).find(':selected').text();
        calculateUserPPM(userGender, userWeight, userHeight, userAge);
    });

    /*-----------------------------------------------------------------------*/

    $('#heightCPM').on("change paste keyup", function () {
        userHeight = $(this).val();
        calculateUserCPM(userGender, userWeight, userHeight, userAge, userActivity);
    });

    $('#weightCPM').on("change paste keyup", function () {
        userWeight = $(this).val();
        calculateUserCPM(userGender, userWeight, userHeight, userAge, userActivity);
    });

    $('#ageCPM').on("change paste keyup", function () {
        userAge = $(this).val();
        calculateUserCPM(userGender, userWeight, userHeight, userAge, userActivity);
    });

    $('#userActivityCPM').change(function () {
        userActivity = $(this).find(':selected').val();
        userActivity = parseFloat(userActivity);
        calculateUserCPM(userGender, userWeight, userHeight, userAge, userActivity);
    });

    $('#genderCPM').change(function () {
        userGender = $(this).find(':selected').text();
        calculateUserCPM(userGender, userWeight, userHeight, userAge, userActivity);
    });

});

function calculateUserBMI(userHeight, userWeight) {
    var bmi = userWeight / (userHeight * userHeight);
    if (bmi < 0)
        bmi = 0;
    
    bmi = bmi.toPrecision(4).toString().replace(',','.');
    $('#bmiResult').val(bmi);
};

function calculateUserPMC(userHeight) {
    var pmc = 50 + 0.75 * (userHeight - 150);
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
    ppm = ppm.toPrecision(4).toString().replace(',', '.');
    $('#ppmResult').val(ppm);
};

function calculateUserCPM(userGender, userWeight, userHeight, userAge, userActivity) {
    var cpm = 0;
    console.log(userGender);
    console.log(userWeight);
    console.log(userHeight);
    console.log(userAge);
    console.log(userActivity);
    if (userGender === "Kobieta") {
        cpm = 665.09 + 9.56 * userWeight + 1.85 * userHeight - 4.67 * userAge;
    }
    else if (userGender === "Mężczyzna") {
        cpm = 66.47 + 13.75 * userWeight + 5 * userHeight - 6.75 * userAge;
    }
    cpm = cpm * userActivity;
    cpm = cpm.toPrecision(4).toString().replace(',', '.');
    $('#cpmResult').val(cpm);
};