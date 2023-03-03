﻿$(function () {
    loginValidate();
    initMenu();
    toggleAdminInfo();
    initEditor();
    initAvatarBox();
    initResponsive();
    

    if ($.isFunction('owlCarousel')) {
        $(".owl-carousel").owlCarousel({
            items: 1,
            loop: true,
            nav: true,
            dots: true,
            autoplay: true,
            autoplayTimeout: 3000,
            autoplayHoverPause: true,
            animateIn: "fadeIn",
            animateOut: "fadeOut",
            responsive: {
                0: {
                    items: 1,
                    nav: false
                },
                992: {
                    items: 2,
                    nav: true
                },
                1200: {
                    items: 3,
                    nav: true
                }
            }
        });
    }

    $(window).resize(function () {
        initResponsive();
    });

    $(".img-upload").change(function () {
        readURL(this);
    });
    
});

function loginValidate() {
    $(".background-login .submit-btn").click(function () {
        var usernameInput = $(".background-login .input-username");
        var passwordInput = $(".background-login .input-password");
        var alertBox = $(".background-login .div-alert");

        if (usernameInput.val() == "") {
            alertBox.find(".message").html("Hãy nhập username");
            alertBox.attr("class", "alert alert-danger alert-dismissible fade show div-alert");
            usernameInput.focus();
            return;
        }

        if (passwordInput.val() == "") {
            alertBox.find(".message").html("Hãy nhập mật khẩu");
            alertBox.attr("class", "alert alert-danger alert-dismissible fade show div-alert");
            passwordInput.focus();
            return;
        }

        var numberArray = "0123456789";
        var letterArray = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
        var symbolArray = "[!@#$%^&*()]";
        var passwordValue = passwordInput.val();

        var hasNumber = false;
        var hasLetter = false;
        var hasSymbol = false;

        for (var i = 0; i < numberArray.length; i++) {
            if (passwordValue.indexOf(numberArray[i]) != -1) {
                hasNumber = true;
            }
        }

        for (var i = 0; i < letterArray.length; i++) {
            if (passwordValue.indexOf(letterArray[i]) != -1) {
                hasLetter = true;
            }
        }

        for (var i = 0; i < symbolArray.length; i++) {
            if (passwordValue.indexOf(symbolArray[i]) != -1) {
                hasSymbol = true;
            }
        }

        if (hasNumber == false || hasLetter == false || hasSymbol == false) {
            alertBox.find(".message").html("Hãy nhập mật khẩu gồm: số, chữ, ký hiệu. Tối thiểu 6 ký tự.");
            alertBox.attr("class", "alert alert-danger alert-dismissible fade show div-alert");
            passwordInput.focus();
            return;
        }

        if (passwordValue.length < 6) {
            alertBox.find(".message").html("Hãy nhập mật khẩu gồm: số, chữ, ký hiệu. Tối thiểu 6 ký tự.");
            alertBox.attr("class", "alert alert-danger alert-dismissible fade show div-alert");
            passwordInput.focus();
            return;
        }

        alertBox.find(".message").html("Đăng nhập thành công!");
        alertBox.attr("class", "alert alert-info alert-dismissible fade show div-alert");

        location.href = "index.html";

        return;
    });
}

function initMenu() {
    $("#sidebar").mCustomScrollbar({
        theme: "minimal"
    });

    $('#sidebarCollapse').on('click', function () {
        $('#sidebar, #content').toggleClass('active');
        $(this).toggleClass('active');

        $('.collapse.in').toggleClass('in');
        $('a[aria-expanded=true]').attr('aria-expanded', 'false');
    });
}

function toggleAdminInfo() {
    $("#sidebar .btn-collapse").click(function (e) {
        $("#sidebar .content").slideToggle(500);
        e.preventDefault();
    });
}

function initEditor() {
    tinymce.init({
        selector: '.editor',
        menubar: false,
        statusbar: false,
        toolbar: 'undo redo | bold italic underline strikethrough | fontselect fontsizeselect formatselect | alignleft aligncenter alignright alignjustify | outdent indent |  numlist bullist | forecolor backcolor removeformat | pagebreak | charmap emoticons | preview save print | insertfile image media template link anchor codesample | ltr rtl',
        plugins: 'print preview fullpage paste importcss searchreplace autolink autosave save directionality code visualblocks visualchars fullscreen image link media template codesample table charmap hr pagebreak nonbreaking anchor toc insertdatetime advlist lists wordcount imagetools textpattern noneditable help charmap emoticons autoresize',
    });
}

function readURL(input) {
    var container = $(input).closest(".avatar-box");
    container.find('.img-preview').fadeOut();
    if (input.files && input.files[0]) {
        var reader = new FileReader();

        reader.onload = function (e) {
            container.find('.img-preview').attr('src', e.target.result);
            container.find('.img-preview').closest("a").attr('href', e.target.result);
            container.find('.img-preview').fadeIn(500);
        }
        reader.readAsDataURL(input.files[0]);
    }
}

function initAvatarBox() {
    var container = $(".avatar-container");
    var defaultUrl = "/Content/images/no-image.png";

    var linkBox = container.find(".link-box");
    var avatarBox = container.find(".avatar-box");
    avatarBox.find(".close").click(function () {
        $(this).closest(".avatar-box").find("img").attr("src", defaultUrl);
        $(this).closest(".avatar-box").find("img").closest("a").attr("href", defaultUrl);
        $(this).closest(".avatar-box").find("input").val("");
        linkBox.find("input").val("");
    });

    linkBox.find("button").click(function () {
        avatarBox.find("img").attr("src", defaultUrl);
        avatarBox.find("input").val("");
        linkBox.find("input").val("");
    });
    linkBox.find("input").on("input", function () {
        var value = $(this).val();
        avatarBox.find("img").attr("src", value);
        avatarBox.find("input").val("");
    });

    avatarBox.find("img").on("error", function () {
        $(this).attr("src", defaultUrl);
        return true;
    })
}

function initResponsive() {
    var width = $(window).width();

    if (width < 975) {
        $(".select-category-main").attr("size", "1");
    }
    else {
        $(".select-category-main").attr("size", "20");
    }
}

