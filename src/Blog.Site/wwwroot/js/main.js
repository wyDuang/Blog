$('.ui.menu .ui.dropdown').dropdown({ on: 'hover' });
//$('.ui.menu a.item').on('click', function () {
//    $(this).addClass('active').siblings().removeClass('active');
//});
$(document).ready(function () {
    $('.main.menu').visibility({ type: 'fixed' });

    $('.main.menu  .ui.dropdown').dropdown({ on: 'hover' });

    //$('.image').visibility({
    //    type: 'image',
    //    transition: 'vertical flip in',
    //    duration: 500
    //});
    
});

(function back_to_top() {
    var lastBttStatus = false;
    window.addEventListener('scroll', function (event) {
        var scrollTop = window.pageYOffset || document.documentElement.scrollTop || document.body.scrollTop || 0;
        var bttStatus = scrollTop > 400
        if (bttStatus != lastBttStatus) {
            lastBttStatus = bttStatus
            if (bttStatus) {
                $('.back_to_top').fadeIn(300);
            } else {
                $('.back_to_top').fadeOut(300);
            }
        }
    });
    $('.back_to_top').click(function () {
        $('body,html').animate({ scrollTop: 0 }, 800);
    })
})()