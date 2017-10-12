$(document).ready(function () { 
    $('#Save').click(function () {
        var trainingResults = getData();
        $.ajax({
            url: '../SaveTrainingResults',
            type: 'POST',
            data: trainingResults,
            async: true,
            success: function (obj) {
                console.log("Hello");
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
        $.ajax({
            url: '../Edit',
            type: 'POST',
            data: trainingResults,
            async: true,
            success: function (obj) {
                console.log("Hello");
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
        data.Exercise = {};
        var exercises = $('#WorkoutTable > tbody > tr > td.Exercise');
        var series = $('#WorkoutTable > tbody > tr > td.SeriesNo');
        var reps = $('#WorkoutTable > tbody > tr > td.RepsNo');
        var weights = $('#WorkoutTable > tbody > tr > td.Weight > input');
        var i = 0;
        var k = 0;
        exercises.each(function () {
            var item = {};
            item["Exercise"] = this.innerHTML;
            item["SeriesNo"] = parseInt(series[i].innerHTML);
            item["RepsNo"] = parseInt(reps[i].innerHTML);
            item["Weight"] = [];
            var tmp = parseInt(series[i].innerHTML);
            for (var j = k; j < k + tmp; j++) {
                item["Weight"].push(parseFloat(weights[j].value));
            }
            k += tmp;
            i++;
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