$(document).ready(function () {
    $("#Exercises").selectpicker('hide');
    $("#MusclePartName").selectpicker('hide');

    $('#MusclePartName').change(function () {
        
        $("#Exercises").selectpicker('show');

        var musclePartName = $('#MusclePartName').find(":selected").text();         
        var muscle = {};                                                           
        muscle.trainingPartName = musclePartName;                                   
        $.ajax({
            url: '../getExercises',
            type: 'GET',
            data: muscle,                                                           
            async: false,
            success: function (obj) {
                $.each(obj, function (key, value) {
                    $('#Exercises').append($("<option/>", {
                        value: key,
                        text: value
                    }));
                });
                $('#Exercises').selectpicker('refresh');
            },
            error: function (obj) {
                swal("Niepowodzenie", "Napotkano nieoczekiwany błąd. Spróbuj ponownie.", "error");
            }
        });
    });

    $('#Exercises').change(function () {
        if ($("#ActivityType").val() === "Ćwiczenia siłowe") {
            if ($("#SeriesNo").hasClass('hidden')) {
                $("#SeriesNo").removeClass('hidden');
            }
            if ($("#RepsNo").hasClass('hidden')) {
                $("#RepsNo").removeClass('hidden');
            }
        }
        else if ($('#ActivityType').val() === "Ćwiczenia wytrzymałościowe" || $('#ActivityType').val() === "Sporty grupowe") {
            if ($('#ExerciseTime').hasClass('hidden')) {
                $('#ExerciseTime').removeClass('hidden');
            }
        }
    });

    var info = {};

    var rows = $('#ExercisesTable > tbody > tr')
    rows.each(function () {
        var item = {};
        item["rowNo"] = parseInt($(this).find('.ExerciseNo').text());
        item["exercise"] = $(this).find('.Exercise').text();
        item["activityType"] = $(this).find('.ActivityType').text();
        item["exerciseInfo"] = {};
        if ($(this).find('.ActivityType').text() === 'Ćwiczenia siłowe') {
            var info = {};
            info["seriesNo"] = parseInt($(this).find('.SeriesNo').text());
            info["repsNo"] = parseInt($(this).find('.RepsNo').text());
            item["exerciseInfo"] = info;
        }
        else if ($(this).find('.ActivityType').text() === 'Ćwiczenia wytrzymałościowe' || $(this).find('.ActivityType').text() === 'Sporty grupowe') {
            var info = {};
            info["exerciseLength"] = parseInt($(this).find('.ExerciseLength').text());
            item["exerciseInfo"] = info;
        }
        exercises.push(item);
    });
});

var exercises = [];

function newElement() {
    var tr = document.createElement("tr");
    var td1 = document.createElement("td");
    td1.className = "ExerciseNo";
    var td2 = document.createElement("td");
    td2.className = "Exercise";
    var td3 = document.createElement("td");
    td3.className = "ExercisesInfo";
    var td4 = document.createElement("td");
    var td5 = document.createElement("td");
    td5.className = "ActivityType";
    var span1 = document.createElement("span");
    var span2 = document.createElement("span");
    var span3 = document.createElement("span");
    var span4 = document.createElement("span");

    var deleteButtonContainer = document.createElement("span");
    deleteButtonContainer.className = "btn btn-danger btn-sm remove";
    var deleteButton = document.createElement("span");
    deleteButton.className = "glyphicon glyphicon-trash";
    deleteButton.id = "Delete";
    deleteButtonContainer.appendChild(deleteButton);

    var item = {};

    var rowsNo = document.getElementById("ExercisesTable").rows.length;
    var activityType = $('#ActivityType').find('option:selected').text();
    var exercise = $("select[id='Exercises'").find('option:selected').text();

    td1.innerHTML = rowsNo;
    td2.innerHTML = exercise;
    td4.appendChild(deleteButtonContainer);
    td5.innerHTML = activityType;

    tr.appendChild(td1);
    tr.appendChild(td2);
    tr.appendChild(td5);
    tr.appendChild(td3);

    if (activityType === 'Ćwiczenia siłowe') {  
        var info = {};
        info["seriesNo"] = $("#SeriesNo").val();
        info["repsNo"] = $("#RepsNo").val();
        item["exerciseInfo"] = info;

        span1.className = "SeriesNo";
        span1.innerHTML = $("#SeriesNo").val();

        span2.className = "RepsNo";
        span2.innerHTML = $("#RepsNo").val();

        span3.className = "text1";
        span3.innerHTML = " serie x "

        span4.className = "text2";
        span4.innerHTML = " powtórzeń";

        td3.appendChild(span1);
        td3.appendChild(span3);
        td3.appendChild(span2);
        td3.appendChild(span4);
    }
    else if (activityType === 'Ćwiczenia wytrzymałościowe') {
        var info = {};
        info["exerciseLength"] = $("#ExerciseTime").val();
        item["exerciseInfo"] = info;

        span1.className = "ExerciseLength";
        span1.innerHTML = $("#ExerciseTime").val();

        span3.className = "text1";
        span3.innerHTML = "Czas trwania: ";

        span4.className = "text2";
        span4.innerHTML = " minut";

        td3.appendChild(span3);
        td3.appendChild(span1);
        td3.appendChild(span4);
    }
    else if (activityType === 'Sporty grupowe') {
        var info = {};
        info["exerciseLength"] = $("#ExerciseTime").val();
        item["exerciseInfo"] = info;

        span1.className = "ExerciseLength";
        span1.innerHTML = $("#ExerciseTime").val();

        span3.className = "text1";
        span3.innerHTML = "Czas trwania: "

        span4.className = "text2";
        span4.innerHTML = " minut";

        td3.appendChild(span3);
        td3.appendChild(span1);
        td3.appendChild(span4);
    }

    tr.appendChild(td4);

    item["rowNo"] = rowsNo;
    item["exercise"] = exercise;
    item["activityType"] = activityType;    

    if ($('#ActivityType :selected').text() === 'Ćwiczenia siłowe' && ($('#Exercises :selected').val() === '' || $('#RepsNo').val() === '' || $('#SeriesNo').val() === '')) {
        swal("Uwaga!", "Uzupełnij wszystkie pola.", "warning");
    } 
    else if (($('#ActivityType :selected').text() === 'Ćwiczenia wytrzymałościowe' || $('#ActivityType :selected').text() === 'Sporty grupowe') &&
        (!$('#Exercises :selected').val() || !$('#ExerciseTime').val())) {
        swal("Uwaga!", "Uzupełnij wszystkie pola.", "warning");
    }
    else {
        document.getElementById("ExercisesBody").appendChild(tr);

        exercises.push(item);

        $("#ActivityType").selectpicker('val', '');
        $("#MusclePartName").selectpicker('val', '');
        $('#Exercises')
            .find('option')
            .remove()
            .end()
            .append('<option value="Wybierz cwiczenie">Wybierz cwiczenie</option>')
            .val('Wybierz cwiczenie');

        $("#SeriesNo").val('');
        $("#RepsNo").val('');
        $("#ExerciseTime").val('');

        $("#MusclePartName").selectpicker('hide');
        $('#Exercises').selectpicker('hide');

        if (!$("#SeriesNo").hasClass('hidden')) {
            $("#SeriesNo").addClass('hidden');
        }
        if (!$("#RepsNo").hasClass('hidden')) {
            $("#RepsNo").addClass('hidden');
        }
        if (!$("#ExerciseTime").hasClass('hidden')) {
            $("#ExerciseTime").addClass('hidden');
        }

        console.log(exercises);
    }       
}

$(function () {
    $("table#ExercisesTable").on("click", ".remove", function () {
        var a = $(this).closest('tr').find('.ExerciseNo').html();
        exercises.splice(a - 1, 1);
        console.log(exercises);
        
        $(this).closest('tr').remove();
        
        var i = 1;
        $('td.ExerciseNo', '#ExercisesBody tr').each(function () {
            $(this).html(i);
            i++;
        });
        
        for (i = a - 1; i < exercises.length; i++)
        {
            exercises[i]["rowNo"] -= 1;
        }
        
    });
});

$(function () {
    $("#AddExercises").click(function () {
        var trainingPlanExercises = {};
        var id = $('#Id').val();
        trainingPlanExercises.jsonData = JSON.stringify(exercises);
        trainingPlanExercises.id = id;
       
        $.ajax({            
            url: '../AddTrainingExercises',
            type: 'POST',
            data: trainingPlanExercises,
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
                    window.location.href = '/TrainingPlans/Index'
                });
            },
            error: function (obj) {
                swal("Niepowodzenie", "Napotkano nieoczekiwany błąd. Spróbuj ponownie.", "error");
            }
        });
    })
})

$(function () {
    $("#ActivityType").change(function () {
        $('#Exercises')
            .find('option')
            .remove()
            .end();

        var activityType = $('#ActivityType').find(":selected").text();
        if (activityType === "Ćwiczenia wytrzymałościowe" || activityType === "Sporty grupowe") {
            $("#MusclePartName").selectpicker('hide');
            $("#Exercises").selectpicker('show');

            if (!$("#SeriesNo").hasClass('hidden')) {
                $("#SeriesNo").addClass('hidden');
            }
            if (!$("#RepsNo").hasClass('hidden')) {
                $("#RepsNo").addClass('hidden');
            }
            
            var data = {};
            data.activityType = activityType;
            $.ajax({
                url: '../getExercises',
                type: 'GET',
                data: data,
                async: false,
                success: function (obj) {
                    $.each(obj, function (key, value) {
                        $('#Exercises').append($("<option/>", {
                            value: key,
                            text: value
                        }));
                    });
                    $('#Exercises').selectpicker('refresh');
                },
                error: function (obj) {
                    swal("Niepowodzenie", "Napotkano nieoczekiwany błąd. Spróbuj ponownie.", "error");
                }
            });
        }
        else if (activityType === "Ćwiczenia siłowe") {
            $("#MusclePartName").selectpicker('show');
            $("#Exercises").selectpicker('hide');
        }
    });
})