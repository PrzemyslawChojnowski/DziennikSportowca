$(window).load(function() {	
	//setTimeout(function(){
		$('body').addClass('loaded');
	//}, 500);	
});

$(document).ready(function () {
    var nutrients;

    $('.check-nutrients').click(function () {
        var productName = $(this).parent().parent().find('.product-name').children().text();
        var data = {};
        data.foodProduct = productName;

        $.ajax({
            url: './GetNutritionFacts',
            data: data,
            async: false,
            type: 'GET',
            success: function (obj) {
                console.log(obj);
                nutrients = obj;
                setModalData('#nutrients', obj);
            },
            error: function () {
                swal("Niepowodzenie", "Napotkano nieoczekiwany błąd. Spróbuj ponownie.", "error");
            }
        });

    });

    function setModalData(name, data){
        $(name).find('.modal-subtitle').text(data.description);
        $(name).find('.carbohydrates').text(data.carbohydrate + ' [g]');
        $(name).find('.proteins').text(data.protein + ' [g]');
        $(name).find('.fats').text(data.fat + ' [g]');
        $(name).find('.energy').text(data.energy + ' [g]');
        $(name).find('#foodProductWeight').val(100);
    }

    $('#foodProductWeight').tooltip();

    $('#foodProductWeight').on('change keypress keyup keydown', function () {
        var proteins = (nutrients.protein / 100 * $(this).val()).toFixed(2);
        var carbohydrates = (nutrients.carbohydrate / 100 * $(this).val()).toFixed(2);
        var fats = (nutrients.fat / 100 * $(this).val()).toFixed(2);
        var energy = (nutrients.energy / 100 * $(this).val()).toFixed(2);

        $('#nutrients').find('.carbohydrates').text(carbohydrates + ' [g]');
        $('#nutrients').find('.proteins').text(proteins + ' [g]');
        $('#nutrients').find('.fats').text(fats + ' [g]');
        $('#nutrients').find('.energy').text(energy + ' [g]');
    })

});
