$(document).ready(function () {
    /*$('.ref-kutu-renk').hover(function(){
        var a = $(this);
        console.log(a.eq(0).html());

         $('.ref-kutu-renk').qtip({
             content: a.eq(0).html(),
             position: {
                 target: 'mouse', // Use the mouse position as the position origin

             }
         });




    });*/
    var start = 0;
    //getNews(0);
    $.mCustomScrollbar.defaults.axis = "yx"; //enable 2 axis scrollbars by default
    $("#content-l").mCustomScrollbar();
    $("#content-l2").mCustomScrollbar({ theme: "light-2" });
    $('.search').keypress(function (event) {
        if (event.keyCode == 13 || event.which == 13) {
            var search = $('#searchText').val();
            search = slugify(search);
            if (search != '') {
                location.href = window.location.origin + "/arama/tr/" + search;
            }
        }
    });
    $('.fa-search').click(function () {
        var search = $('#searchText').val();
        search = slugify(search);
        if (search != '') {
            location.href = window.location.origin + "/arama/tr/" + search;
        }
    });
    $(window).scroll(function () {
        //$('#mine').text($(document).scrollTop());
        $('.menu-logo').addClass('fixedMenu animated fadeInDown');
        if ($(document).scrollTop() < 350) {
            $('.menu-logo').removeClass('fixedMenu animated fadeInDown');
        }
    });
   /* $('#form_talep').submit(function (event) {
        event.preventDefault();
        var Infos = $(this).serializeArray();
        var data = {};
        for (var i = 0; i < Infos.length; i++) {
            data[Infos[i].name] = Infos[i].value;
        }
        $.post('/Ajax/sendRequest', data, function (r) {
            var message = '';
            if (r == 1) {
                $('#form_talep').val(' ');
                message = "Talebiniz Başarılı Şekilde Gönderildi";
            }
            else {
                message = "Bir Hata Oldu";
            }
            var dialog = bootbox.dialog({
                message: message,
                closeButton: false
            });
            setTimeout(function () {
                dialog.modal('hide');
            }, 2000);
        });
    });
    $('#teknik_form').submit(function (event) {
        event.preventDefault();
        var Infos = $(this).serializeArray();
        var data = {};
        for (var i = 0; i < Infos.length; i++) {
            data[Infos[i].name] = Infos[i].value;
        }
        $.post('/Ajax/TechnicRequest', data, function (r) {
            var message = '';
            if (r == 1) {
                $('#form_talep').val(' ');
                message = "Talebiniz Başarılı Şekilde Gönderildi";
            }
            else {
                message = "Bir Hata Oldu";
            }
            var dialog = bootbox.dialog({
                message: message,
                closeButton: false
            });
            setTimeout(function () {
                dialog.modal('hide');
            }, 2000);
        });
    });
    $('#blogemail').submit(function (event) {
        event.preventDefault();
        var email = $(this).serializeArray();
        var data = {};
        data['email'] = email[0].value;
        $.post('/Ajax/addebulten', data, function (r) {
            var message = '';
            if (r == 1) {
                $('.form-control').val(' ');
                message = "Talebiniz Başarılı Şekilde Gönderildi";
            }
            else {
                message = "Bir Hata Oldu";
            }
            var dialog = bootbox.dialog({
                message: message,
                closeButton: false
            });
            dialog.css({
                'top': '30%'
            });
            setTimeout(function () {
                dialog.modal('hide');
            }, 2000);
        });
    });*/
    //$('.main-slider').owlCarousel({
    //    items: 1,
    //    autoplay: true,
    //    autoplayTimeout: 4000,
    //    autoplayHoverPause: false,
    //    loop: true,
    //    dots: true
    //});
    //$('.about-slider').owlCarousel({
    //    items: 1,
    //    autoplay: true,
    //    autoplayTimeout: 3000,
    //    autoplayHoverPause: true,
    //    loop: true,
    //    dots: true
    //});
    
    /*$('.haber-gallery-slider .news-carousel').owlCarousel({
        items: 4,
        nav: true,
        margin: 10,
        navText: ["<i class='fa fa-angle-left'></i>", "<i class='fa fa-angle-right'></i>"],
        dots: false,
        responsive: {
            0: {
                items: 1,
                nav: true
            },
            480: {
                items: 1
            },
            768: {
                items: 2
            },
            1000: {
                items: 4
            },
            1600: {
                items: 4
            }
        }
    });*/
    AOS.init({
        easing: 'ease-out-back',
        duration: 2500
    });
    $(".res_menu .menu_ac").click(function () {
        $(".res_menu .acilir_menu").fadeToggle();
    });
    var totalCount;
    //$.post('/Ajax/getActiveNewsCount', function (r) {
    //    totalCount = $.parseJSON(r);
    //    totalCount = totalCount.count;
    //});
    /*aybuke
    function ilerle() {
        var pText = $('.dot-img').attr('src');
        var currentIndex = $('.dot-img').closest('.news-dot').index();
        var pTextCenter = $('.dot-img').eq(1).attr('src');
        $('.dot-img').eq(currentIndex).attr('src', pTextCenter);
        $('.dot-img').eq(1).attr('src', pText);
        var haberText = $('.item-haber').eq(currentIndex).html();
        var haberCenterEl = $('.item-haber').eq(1);
        var haberCenter = haberCenterEl.html();
        $('.item-haber').eq(currentIndex).html(haberCenter);
        $('.item-haber').eq(1).html(haberText);
    }
    $('.dot-img').click(function () {
        $(this).fadeOut('slow');
        var pText = $(this).attr('src');
        var currentIndex = $(this).closest('.news-dot').index();
        var pTextCenter = $('.dot-img').eq(1).attr('src');
        $('.dot-img').eq(1).fadeOut('slow');
        $('.dot-img').eq(currentIndex).attr('src', pTextCenter);
        $('.dot-img').eq(1).attr('src', pText);
        $('.dot-img').eq(1).fadeIn('slow');
        var haberText = $('.item-haber').eq(currentIndex).html();
        var haberCenterEl = $('.item-haber').eq(1);
        var haberCenter = haberCenterEl.html();
        $('.item-haber').eq(currentIndex).html(haberCenter);
        $('.item-haber').eq(1).html(haberText);
        $(this).fadeIn('slow');
    });
    $('.fa-angle-left').click(function () {
        if (start === 0)
            start = totalCount;
        else
            start -= 3;
        getNews(start);
    });
    var newsControl = true;
    function getNews(i) {
        if (i == 0) {
            newsControl = true;
        }
        if (i < 0)
            i = 0;
        $('.haberler').fadeOut('slow');
        var data = {};
        data['start'] = i;
        $.post('/Ajax/getNews', data, function (r) {
            var obj = $.parseJSON(r);
            if (obj.length < 3)
                newsControl = false;
            $.each(obj, function (index, object) {
                var k = index;
                if (k == 0) {
                    k = 1;
                }
                else if (k == 1) {
                    k = 0;
                }
                else {
                    k = index;
                }
                $('.news-dot').eq(k).find('img').attr('src', object['img']);
                $('.item-haber').eq(k).find('a').attr('href', '/haber/tr/' + object['url']);
                $('.item-haber').eq(k).find('span').html(object['title']);
                $('.item-haber').eq(k).find('p').html(object['content']);
            });
        });
        $('.haberler').fadeIn('slow');
    }
    $('.fa-angle-right').click(function () {
        start += 3;
        if (start === totalCount)
            start = 0;
        getNews(start);
    });
    var inter = function () {
        if (newsControl) {
            start += 3;
            if (start === totalCount)
                start = 0;
            getNews(start);
        }
        else {
            start = 0;
            getNews(start);
        }
    };
    var intervalID = null;
    function intervalManager(flag, inter, time) {
        if (flag)
            intervalID = setInterval(inter, time);
        else
            clearInterval(intervalID);
    }
    $('.news').hover(function () {
        intervalManager(false);
    }, function () {
        intervalManager(true, inter, 3000);
    });
    intervalManager(true, inter, 3000);
    (function ($) {
        $(window).on("load", function () {
            $.mCustomScrollbar.defaults.axis = "yx"; //enable 2 axis scrollbars by default
            $("#content-l").mCustomScrollbar();
            $("#content-d").mCustomScrollbar({ theme: "dark" });
            $("#content-l2").mCustomScrollbar({ theme: "light-2" });
        });
    })(jQuery);
    $('.res-cat-menu').click(function () {
        var iEl = $(this).find('i');
        if (iEl.hasClass('fa-arrow-down')) {
            iEl.removeClass('fa-arrow-down');
            iEl.addClass('fa-arrow-up');
        }
        else {
            iEl.removeClass('fa-arrow-up');
            iEl.addClass('fa-arrow-down');
        }
        $('.urunler-bolumu').slideToggle();
    });*/
});
