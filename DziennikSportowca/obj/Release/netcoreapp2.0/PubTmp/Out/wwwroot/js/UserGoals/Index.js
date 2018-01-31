$(window).load(function () {
    setTimeout(function () {
        $('.circle-loader').toggleClass('load-complete');
        $('.checkmark').toggle();

        $('.circle-loader-2').toggleClass('load-complete-2');
        $('.close-icon-line-1').toggle();
        $('.close-icon-line-2').toggle();

    }, 1000)
  
});