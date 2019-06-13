layui.use(['jquery', 'layer', 'util'], function () {
    var $ = layui.jquery,
        layer = layui.layer,
        util = layui.util;
    util.fixbar();
    //导航控制
    master.start($);
});
var slider = 0;
<<<<<<< HEAD
<<<<<<< HEAD
var pathname = window.location.pathname.replace('Read', 'articles');
=======
var pathname = window.location.pathname.replace('Read', 'Article');
>>>>>>> 1ff30bd... 增加blog首页和文章页
=======
var pathname = window.location.pathname.replace('Read', 'articles');
>>>>>>> fb48ab8... 完善前端资源压缩
var master = {};
master.start = function ($) {
    $('#nav li').hover(function () {
        $(this).addClass('current');
    }, function () {
        var href = $(this).find('a').attr("href");
<<<<<<< HEAD
<<<<<<< HEAD
            if (pathname.indexOf(href) == -1 || href == "/") {
=======
        if (pathname.indexOf(href) == -1) {
>>>>>>> 1ff30bd... 增加blog首页和文章页
=======
            if (pathname.indexOf(href) == -1 || href == "/") {
>>>>>>> fb48ab8... 完善前端资源压缩
            $(this).removeClass('current');
        }
    });
    selectNav();
    function selectNav() {
        var navobjs = $("#nav a");
        $.each(navobjs, function () {
            var href = $(this).attr("href");
<<<<<<< HEAD
<<<<<<< HEAD
            if (href == "/") return;
=======
>>>>>>> 1ff30bd... 增加blog首页和文章页
=======
            if (href == "/") return;
>>>>>>> fb48ab8... 完善前端资源压缩
            if (pathname.indexOf(href) != -1) {
                $(this).parent().addClass('current');
            }
        });
    };
    $('.phone-menu').on('click', function () {
        $('#nav').toggle(500);
    });
    $(".blog-user img").hover(function () {
        var tips = layer.tips('点击退出', '.blog-user', {
            tips: [3, '#009688'],
        });
    }, function () {
        layer.closeAll('tips');
    })
};
