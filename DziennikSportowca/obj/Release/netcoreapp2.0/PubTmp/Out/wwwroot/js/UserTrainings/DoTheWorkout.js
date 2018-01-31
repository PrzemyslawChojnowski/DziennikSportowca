$(document).ready(function () { 
    $('#Save').click(function () {
        var trainingResults = getData();
        $.ajax({
            url: '../SaveTrainingResults',
            type: 'POST',
            data: trainingResults,
            async: true,
            success: function (obj) {
                swal({
                    title: "Powodzenie",
                    text: "Twój trening został zapisany pomyślnie.",
                    type: "success",
                    confirmButtonColor: "#DD6B55",
                    confirmButtonText: "OK!",
                    closeOnConfirm: true,
                    html: false
                }, function () {
                    window.location.href = '/UserTrainings/Index'
                });
            },
            error: function (obj) {
                swal("Niepowodzenie", "Napotkano nieoczekiwany błąd. Spróbuj ponownie.", "error");
            }

        })
    });

    $('#Edit').click(function () {
        var trainingResults = getData();
        trainingResults.trainingId = $('#TrainingId').val();
        $.ajax({
            url: '../Edit',
            type: 'POST',
            data: trainingResults,
            success: function (obj) {
                swal({
                    title: "Powodzenie",
                    text: "Twój trening został zapisany pomyślnie.",
                    type: "success",
                    confirmButtonColor: "#DD6B55",
                    confirmButtonText: "OK!",
                    closeOnConfirm: true,
                    html: false
                }, function () {
                    window.location.href = '/UserTrainings/Index'
                });
            },
            error: function (obj) {
                swal("Niepowodzenie", "Napotkano nieoczekiwany błąd. Spróbuj ponownie.", "error");
            }

        })
    });

    function getData() {
        var data = [];
        var rows = $('#WorkoutTable > tbody > tr');

        rows.each(function () {
            var item = {};
            item["Exercise"] = $(this).find('.Exercise').text();
            item["ActivityType"] = $(this).find('.ActivityType').text();
            item["ExerciseInfo"] = {};
            if ($(this).find('.ActivityType').text() === "Ćwiczenia siłowe") {
                var info = {};
                info["RepsNo"] = parseInt($(this).find('.RepsNo').text());
                info["SeriesNo"] = parseInt($(this).find('.SeriesNo').text());
                info["Weight"] = [];
                $(this).find('.Weight > input').each(function () {
                    info["Weight"].push(parseInt($(this).val()));
                });
                item["ExerciseInfo"] = info;
            } else if ($(this).find('.ActivityType').text() === "Ćwiczenia wytrzymałościowe" || $(this).find('.ActivityType').text() === "Sporty grupowe") {
                var info = {};
                info["ExerciseLength"] = parseInt($(this).find('.ExerciseLength').text());
                info["ExerciseLengthAtTraining"] = parseInt($(this).find('.Length > input').val());
                item["ExerciseInfo"] = info;
            }
            data.push(item);
        });

        var trainingResults = {};
        trainingResults.jsonString = JSON.stringify(data);
        trainingResults.id = $('#Id').val();
        trainingResults.startDate = JSON.stringify(createDate($('#startDate').find('input').val()));
        trainingResults.endDate = JSON.stringify(createDate($('#endDate').find('input').val()));

        return trainingResults;
    }

    function createDate(dateString) {
        var day = dateString.substr(0, 2);
        var month = parseInt(dateString.substr(3, 2)) - 1;
        var year = dateString.substr(6, 4);
        var hour = dateString.substr(13, 2);
        var minutes = dateString.substr(16, 2);

        return (new Date(year, month, day, hour, minutes));
    }
});