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
    });

    var tmp = $('#ExercisesTable > tbody > tr > td.ExerciseNo')
    tmp.each(function () {
        exercises.rowNo.push(this.innerHTML);
    });
    tmp = $('#ExercisesTable > tbody > tr > td.PartName')
    tmp.each(function () {
        exercises.musclePart.push(this.innerHTML);
    });
    tmp = $('#ExercisesTable > tbody > tr > td.Exercise')
    tmp.each(function () {
        exercises.exercise.push(this.innerHTML);
    });
    tmp = $('#ExercisesTable > tbody > tr > td.SeriesNo')
    tmp.each(function () {
        exercises.seriesNo.push(this.innerHTML);
    });
    tmp = $('#ExercisesTable > tbody > tr > td.RepsNo')
    tmp.each(function () {
        exercises.repsNo.push(this.innerHTML);
    });
});

var exercises = {};
exercises.rowNo = [];
exercises.musclePart = [];
exercises.exercise = [];
exercises.seriesNo = [];
exercises.repsNo = [];

function newElement() {
    var tr = document.createElement("tr");
    var td1 = document.createElement("td");
    td1.className = "ExerciseNo";
    var td2 = document.createElement("td");
    td2.className = "PartName";
    var td3 = document.createElement("td");
    td3.className = "Exercise";
    var td4 = document.createElement("td");
    td4.className = "SeriesNo";
    var td5 = document.createElement("td");
    td5.className = "RepsNo";
    var td6 = document.createElement("td");

    var deleteButtonContainer = document.createElement("span");
    deleteButtonContainer.className = "btn btn-default btn-sm remove";
    var deleteButton = document.createElement("span");
    deleteButton.className = "glyphicon glyphicon-trash";
    deleteButton.id = "Delete";
    deleteButtonContainer.appendChild(deleteButton);

    var rowsNo = document.getElementById("ExercisesTable").rows.length;
    var musclePart = $("select[id='MusclePartName'").find('option:selected').text()
    var exercise = $("select[id='Exercises'").find('option:selected').text()
    var seriesNo = $("#SeriesNo").val();
    var repsNo = $("#RepsNo").val();

    exercises.rowNo.push(rowsNo);
    exercises.musclePart.push(musclePart);
    exercises.exercise.push(exercise);
    exercises.seriesNo.push(seriesNo);
    exercises.repsNo.push(repsNo);

    td1.innerHTML = rowsNo;
    td2.innerHTML = musclePart;
    td3.innerHTML = exercise;
    td4.innerHTML = seriesNo;
    td5.innerHTML = repsNo;
    td6.appendChild(deleteButtonContainer);

    tr.appendChild(td1);
    tr.appendChild(td2);
    tr.appendChild(td3);
    tr.appendChild(td4);
    tr.appendChild(td5);
    tr.appendChild(td6);

    if (musclePart === '' || musclePart === 'Wybierz partie' || exercise === '' || exercise === 'Wybierz cwiczenie'
        || seriesNo === '' || repsNo === '') {
        swal("Uwaga!", "Uzupełnij wszystkie pola.", "warning");
    } else {
        document.getElementById("ExercisesBody").appendChild(tr);
    }

    $("select[id='MusclePartName'").val("Wybierz partie");
    $('#Exercises')
        .find('option')
        .remove()
        .end()
        .append('<option value="Wybierz cwiczenie">Wybierz cwiczenie</option>')
        .val('Wybierz cwiczenie')
        ;
    $("#SeriesNo").val('');
    $("#RepsNo").val('');
}

$(function () {
    $("table#ExercisesTable").on("click", ".remove", function () {
        var a = $(this).closest('tr').find('.ExerciseNo').html();
        exercises.exercise.splice(a - 1, 1);
        exercises.seriesNo.splice(a - 1, 1);
        exercises.repsNo.splice(a - 1, 1);
        exercises.rowNo.splice(a - 1, 1);
        exercises.musclePart.splice(a - 1, 1);
        
        $(this).closest('tr').remove();
        
        var i = 1;
        $('td.ExerciseNo', '#ExercisesBody tr').each(function () {
            $(this).html(i);
            i++;
        });
        
        for (i = a - 1; i < exercises.rowNo.length; i++)
        {
            exercises.rowNo[i] -= 1;
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