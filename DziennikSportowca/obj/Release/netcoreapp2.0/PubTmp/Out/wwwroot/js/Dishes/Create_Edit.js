$(document).ready(function () {   
    var dish = [];
    var totalWeight = 0;
    var totalProteins = 0;
    var totalCarbs = 0;
    var totalFat = 0;
    var totalEnergy = 0;

    $('#FoodProductType').change(function () {
        $('#FoodProduct')
            .find('option')
            .remove()
            .end()
            
        var selectedFoodType = $(this).find(':selected').text();
        var type = {};
        type.foodType = selectedFoodType;
            
        $.ajax({
            url: 'GetProducts',
            type: 'GET',
            async: false,
            data: type,
            success: function(obj){
                $.each(obj, function (key, value) {
                    $('#FoodProduct').append($("<option/>", {
                        value: key,
                        text: value
                    }));
                    $('#FoodProduct').selectpicker('refresh');
                });
            },
            error: function (obj) {
                swal("Niepowodzenie", "Napotkano nieoczekiwany błąd. Spróbuj ponownie.", "error");
            }                
        });
    });

    $('#Create').click(function () {
        if (!$('#Weight').val() || $('#FoodProduct').val() === '' || !$('#FoodProductType').val()) { 
            swal("Uwaga!", "Uzupełnij wszystkie pola.", "warning");
        }
        else {
            var data = {};
            data.foodProduct = $('#FoodProduct option:selected').text();
            var nutritionFacts;

            $.ajax({
                url: 'GetNutritionFacts',
                type: 'GET',
                async: false,
                data: data,
                success: function (obj) {
                    nutritionFacts = obj;
                },
                error: function (obj) {
                    swal("Niepowodzenie", "Napotkano nieoczekiwany błąd. Spróbuj ponownie.", "error");
                }
            });

            $.createElement(data, nutritionFacts);

        }
    });

    $.createElement = function (data, nutritionFacts) {
        var index = document.getElementById("DishesTable").rows.length - 1;
        var type = $('#FoodProductType option:selected').text();
        var name = data.foodProduct;
        var weightOfProduct = parseFloat($('#Weight').val());
        var proteins = parseFloat((nutritionFacts.protein * weightOfProduct / 100).toFixed(2));
        var carbs = parseFloat((nutritionFacts.carbohydrate * weightOfProduct / 100).toFixed(2));
        var fats = parseFloat((nutritionFacts.fat * weightOfProduct / 100).toFixed(2));
        var energy = parseFloat((nutritionFacts.energy * weightOfProduct / 100).toFixed(2));

        var tr = document.createElement("tr");

        var lp = document.createElement("td");
        lp.className = "FoodProductNo";
        lp.innerHTML = index;
        tr.appendChild(lp);

        var productType = document.createElement("td");
        productType.className = "ProductType";
        productType.innerHTML = type;
        tr.appendChild(productType);

        var productName = document.createElement("td");
        productName.className = "ProductName";
        productName.innerHTML = name;
        tr.appendChild(productName);

        var productWeight = document.createElement("td");
        productWeight.className = "ProductWeight";
        productWeight.innerHTML = weightOfProduct;
        tr.appendChild(productWeight);

        var protein = document.createElement("td");
        protein.className = "Proteins";
        protein.innerHTML = proteins;
        tr.appendChild(protein);

        var carbohydrates = document.createElement("td");
        carbohydrates.className = "Carbohydrates";
        carbohydrates.innerHTML = carbs;
        tr.appendChild(carbohydrates);

        var fat = document.createElement("td");
        fat.className = "Fat";
        fat.innerHTML = fats;
        tr.appendChild(fat);

        var kcal = document.createElement("td");
        kcal.className = "Energy";
        kcal.innerHTML = energy;
        tr.appendChild(kcal);

        var deleteButtonTD = document.createElement("td");
        var deleteButtonContainer = document.createElement("span");
        deleteButtonContainer.className = "btn btn-danger btn-sm remove";
        var deleteButton = document.createElement("span");
        deleteButton.className = "glyphicon glyphicon-trash";
        deleteButton.id = "Delete";
        deleteButtonContainer.appendChild(deleteButton);
        deleteButtonTD.appendChild(deleteButtonContainer);
        tr.appendChild(deleteButtonTD);

        $('#DishesBody').append(tr);

        var product = {};
        product["lp"] = index;
        product["foodType"] = type;;
        product["name"] = name;
        product["weight"] = weightOfProduct;
        product["protein"] = proteins;
        product["carbohydrates"] = carbs;
        product["fat"] = fats;
        product["kcal"] = energy;

        dish.push(product);
        $.fn.updateTotals(proteins, carbs, fats, energy, weightOfProduct, '+');

        $('.TotalWeight').text(totalWeight);
        $('.TotalProteins').text(totalProteins);
        $('.TotalCarbs').text(totalCarbs);
        $('.TotalFat').text(totalFat);
        $('.TotalEnergy').text(totalEnergy);
    }

    $(function () {
        $("table#DishesTable").on("click", ".remove", function () {
            var a = $(this).closest('tr').find('.FoodProductNo').html();
            var weightOfProduct = dish[a - 1].weight;
            var proteins = dish[a - 1].protein;
            var carbs = dish[a - 1].carbohydrates;
            var fats = dish[a - 1].fat;
            var energy = dish[a - 1].kcal;             

            $.fn.updateTotals(proteins, carbs, fats, energy, weightOfProduct, '-');

            dish.splice(a - 1, 1);

            $(this).closest('tr').remove();

            if ($('td', '#DishesBody tr').length === 0)
            {
                $('.TotalWeight').text('');
                $('.TotalProteins').text('');
                $('.TotalCarbs').text('');
                $('.TotalFat').text('');
                $('.TotalEnergy').text('');
            }
            else
            {
                $('.TotalWeight').text(totalWeight);
                $('.TotalProteins').text(totalProteins);
                $('.TotalCarbs').text(totalCarbs);
                $('.TotalFat').text(totalFat);
                $('.TotalEnergy').text(totalEnergy);

                var i = 1;
                $('td.FoodProductNo', '#DishesBody tr').each(function () {
                    $(this).html(i);
                    i++;
                });
            }
                
            for (i = a - 1; i < dish.length; i++) {
                dish[i].lp -= 1;
            }

        });
    });

    $(function () {
        $("#CreateDish").click(function () {
            var data = {};
            data.jsonData = JSON.stringify(dish);
            data.dishName = $('#DishName').val();

            $.ajax({
                url: './Create',
                type: 'POST',
                data: data,
                async: true,
                success: function (obj) {
                    swal({
                        title: "Powodzenie",
                        text: "Twój posiłek został zapisany pomyślnie.",
                        type: "success",
                        confirmButtonColor: "#DD6B55",
                        confirmButtonText: "OK!",
                        closeOnConfirm: true,
                        html: false
                    }, function () {
                        window.location.href = './Index'
                    });
                },
                error: function (obj) {
                    swal("Niepowodzenie", "Napotkano nieoczekiwany błąd. Spróbuj ponownie.", "error");
                }
            });
        })
    });

    $.fn.updateTotals = function (proteins, carbs, fat, energy, weight, type) {
        if (type === '+') {
            totalWeight += weight;
            totalProteins += proteins;
            totalCarbs += carbs;
            totalFat += fat;
            totalEnergy += energy;
        }
        else if (type === '-') {
            totalWeight -= weight;
            totalProteins -= proteins;
            totalCarbs -= carbs;
            totalFat -= fat;
            totalEnergy -= energy; 
        }
    }
});