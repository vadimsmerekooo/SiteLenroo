'use strict';

(function ($, sr) {
    // debouncing function from John Hann
    // http://unscriptable.com/index.php/2009/03/20/debouncing-javascript-methods/
    var debounce = function (func, threshold, execAsap) {
        var timeout;
        return function debounced() {
            var obj = this, args = arguments;

            function delayed() {
                if (!execAsap) {
                    func.apply(obj, args);
                }
                timeout = null;
            }

            if (timeout) {
                clearTimeout(timeout);
            } else if (execAsap) {
                func.apply(obj, args);
            }
            timeout = setTimeout(delayed, threshold || 100);
        };
    };

    // smartResize
    jQuery.fn[sr] = function (fn) {
        return fn
            ? this.bind('resize', debounce(fn))
            : this.trigger(sr);
    };
})(jQuery, 'smartResize');

// Функция для скрытия горизонтального скроллера, если
// разрешение монитора >= текущей ширине сайта
function scrollHide() {
    $(document.body).toggleClass(
        'no-x-scroll'
        , (document.body.scrollWidth - document.body.clientWidth < 30)
    );
}

// Первый вызов
// scrollHide();
// Биндинг на событие Resize
//noinspection JSUnresolvedFunction
$(window)
    .smartResize(scrollHide)
    .trigger('resize');

function changeCaptcha(obj, state, captcha) {
    if (state === 'error') {
        obj
            .find('.error-captcha')
            .parents()
            .removeClass('hidden');
    }

    obj
        .parents('.auth__form-container')
        .find('.captcha-img')
        .prop('src', captcha).end();
}

$(function () {
    $('[href^="#cat"]').click(function (e) {
        e.preventDefault();

        var id = $(this).attr('href');
        window.scrollTo(0, $(document).find(id).offset().top);
    });

    $('[data-toggle="popover"]').popover({
        html: true,
        content: $('.auth').html()
    });

    var linkToLoginForm = '#showLogin';
    $('[href="' + linkToLoginForm + '"]').click(function () {
        $('[data-toggle="popover"]').popover('toggle');
        window.scrollTo(0, 0);
    });

    // Закрыть Bootstrap popover при клике за пределы popover
    $(document).on('click', function (e) {
        $('[data-toggle="popover"],[data-original-title]').each(function () {
            //the 'is' for buttons that trigger popups
            //the 'has' for icons within a button that triggers a popup
            if (!$(this).is(e.target)
                && $(this).has(e.target).length === 0
                && $('.popover').has(e.target).length === 0
                && !$(e.target).is('[href="' + linkToLoginForm + '"]')
            ) {
                $(this).popover('hide').data('bs.popover').inState.click = false;  // fix for BS 3.3.6
            }
        });
    });

    $(document).on('click', '.auth__links .btn-group', function (event) {
        if (event.target.nodeName.toLowerCase() === 'button') {
            $(event.target)
                .siblings()
                .removeClass('active').end()
                .addClass('active')
                .parents('.auth__container')
                .find('.' + $(event.target).data('form'))
                .siblings()
                .removeClass('show').end()
                .addClass('show');
        }
    });

    // AjaxForm  
    $(document).on('submit', 'form', function (e) {
        var $this = $(this);
        var $popover = $this.parents('.popover');

        // Форма авторизации
        if ($this.hasClass('auth__form-login')) {
            e.preventDefault();
            $.ajax({
                type: 'post',
                data: $this.serialize(),
                beforeSend: function () {
                    $this.find('input, button').removeClass('error').prop('disabled', true);
                    $this.find('.exception').removeClass('show').text();
                }
            }).done(function (result) {
                if (result.isError) {
                    $this.find('input').addClass('error').end().find('.exception').text(result.message).addClass('show');
                    var $captcha = $this.find('.auth__form-login__captcha-container');
                    if ($captcha.hasClass('hidden')) {
                        $captcha
                            .find('input')
                            .removeClass('error').end();
                    }
                    $captcha
                        .removeClass('hidden').end()
                        .parents('.auth__container')
                        .find('.captcha-img')
                        .prop('src', result.captcha).end()
                        .find('[name="captcha"]')
                        .prop('required', true)
                    ;
                } else {
                    // @TODO 'Добавить странице "личный кабинет" уник profile'
                    var content = '<a class="header__link" href="' + result.profileUrl + '">' + result.name + '</a>' +
                        '<br>' +
                        '<a class="header__link" href="' + result.logoutUrl + '">Выйти</a>';
                    $('.header__auth').html(content);
                    $popover.hide();
                }
            }).always(function (result) {
                $this.find('input, button').prop('disabled', false);
            });
        }

        // Форма регистрации
        if ($this.hasClass('auth__form-reg')) {
            e.preventDefault();
            $.ajax({
                type: 'post',
                data: $this.serialize(),
                beforeSend: function () {
                    $this.find('input, button').removeClass('error').prop('disabled', true);
                    $this.find('.error-sub').addClass('hidden');
                }
            }).done(function (result) {
                if (result.isError) {
                    // Проверка на длину логина
                    if (result.login !== undefined && !result.login.isValid) {
                        $this.find('[name="login-reg"]').addClass('error');
                        $this
                            .find('.error-login')
                            .removeClass('hidden')
                            .text(result.login.error);
                    }
                    // Проверка, что логин уникальный
                    if (result.errorLoginMoreThenOne !== undefined && result.errorLoginMoreThenOne) {
                        $this.find('[name="login-reg"]').addClass('error');
                        $this
                            .find('.error-login')
                            .removeClass('hidden')
                            .text('Данный логин уже занят');
                    }
                    // Проверка на длину имени
                    if (result.name !== undefined && !result.name.isValid) {
                        $this.find('[name="name"]').addClass('error');
                        $this
                            .find('.error-name')
                            .removeClass('hidden')
                            .text(result.name.error);
                    }
                    // Проверка на длину фамилии
                    if (result.surname !== undefined && !result.surname.isValid) {
                        $this.find('[name="surname"]').addClass('error');
                        $this
                            .find('.error-surname')
                            .removeClass('hidden')
                            .text(result.surname.error);
                    }
                    // Проверка, что эл. адрес введен корректно
                    if (result.email !== undefined && !result.email.isValid) {
                        $this.find('[name="email"]').addClass('error');
                        $this
                            .find('.error-email')
                            .removeClass('hidden')
                            .text(result.email.error);
                    }
                    // Проверка, что эл. адрес уникальный
                    if (result.errorEmail !== undefined && result.errorEmail) {
                        $this.find('[name="email"]').addClass('error');
                        $this
                            .find('.error-email')
                            .removeClass('hidden')
                            .text('Данный электронный адрес уже занят');
                    }
                    // Валидация паролей
                    if (result.password !== undefined && !result.password.isValid) {
                        $this.find('[name="password-reg"], [name="password-repeat"]').addClass('error');
                        $this
                            .find('.error-password-repeat')
                            .removeClass('hidden')
                            .text(result.password.error);
                    }
                    // Проверка, что пароли совпадают
                    if (result.errorConfirmPassword !== undefined && result.errorConfirmPassword) {
                        $this.find('[name="password-reg"], [name="password-repeat"]').addClass('error');
                        $this
                            .find('.error-password-repeat')
                            .removeClass('hidden');
                    }
                    // Сменить капчу
                    if (result.changeCaptcha !== undefined && result.changeCaptcha) {
                        changeCaptcha($this, 'change', result.newCaptcha);
                    }
                    // Проверка, что код капчи введен верно
                    if (result.errorCaptcha !== undefined && result.errorCaptcha) {
                        $this
                            .find('[name="code"]')
                            .addClass('error');
                        $this
                            .find('.error-captcha')
                            .removeClass('hidden');
                        changeCaptcha($this, 'error', result.newCaptcha);
                    }
                    // Проверка, что дано согласие
                    if (result.errorConditions !== undefined && result.errorConditions) {
                        $this.find('[name="conditions"]').addClass('error');
                        $this
                            .find('.error-conditions')
                            .removeClass('hidden').end();
                    }
                } else {
                    var content = result.register;
                    $this.html(content);
                }
            }).always(function (result) {
                $this.find('input, button').prop('disabled', false);
            });
        }
    });

    $(document).on('click', '.auth__service-container a', function (e) {
        var $this = $(this);
        $.ajax({
            type: 'post',
            data: {
                'service': $this.data('service')
            },
            success: function (result) {
                console.log(result);
            },
            error: function (result) {
                console.log(result);
            }
        });
    });

    $(document).on(
        'click',
        '.spoiler-title',
        function (e) {
            if (e.target.nodeName !== 'A') {
                $(this)
                    .children('.spoiler-toggle')
                    .toggleClass('hide-icon')
                    .toggleClass('show-icon')
                    .end();
                $(this)
                    .next()
                    .slideToggle();
            }
        }
    );

    //div.thickbox' добавлено для swiper
    $('a.gallery, a.thickbox, a.imagesFancy, div.thickbox')
        .attr('rel', 'gallery')
        .fancybox({
            closeBtn: false,
            padding: 0,
            margin: [60, 80, 60, 80],
            helpers: {
                thumbs: {
                    position: 'top',
                    width: 75,
                    height: 75
                }
            }
        })
    ;

    var fixTable = function (i, table) {
        if (893 < table.offsetWidth) {
            $(table).css('width', '100%').attr('cellpadding', 5);
            $(table).wrap('<div class="big-table-div"></div>');
            $(table).find('td').each(
                function (i, td) {
                    $(td).css('width', 'auto')
                }
            );
        }
    };

    $('.article table').not('[data-no-auto]').each(fixTable);

    $(document).on('hide.bs.collapse', '.panel', function (e) {
        if (!$(e.target).hasClass('form-control__datepicker')) {
            $(this).find('.panel-state').toggleClass('panel-state--show').text('Развернуть');
        }
    });

    $(document).on('show.bs.collapse', '.panel', function (e) {
        if (!$(e.target).hasClass('form-control__datepicker')) {
            $(this).find('.panel-state').toggleClass('panel-state--show').text('Свернуть');
        }
    });

    $('.attachment__link--viewer, .fancybox__viewer').fancybox({
        type: 'iframe',
        height: '100%',
        width: '75%',
        afterLoad: function () {
            var $fabcyboxClose = $('.fancybox-close');
            var clone = $fabcyboxClose.clone();

            $fabcyboxClose.remove();
            $('#fancybox-lock').append(clone);
        }
    });

    $('.npa__btn-show').click(function (e) {
        e.preventDefault();
        var $this = $(this);

        $this
            .text(
                $this.text() === 'Свернуть' ? 'Подробнее' : 'Свернуть'
            )
            .siblings('.npa__data')
            .toggleClass('hide');
    });

    $('[data-toggle="tooltip"]').tooltip();
});

//Для отображения главного меню в мобильной верстке
$('.header__burger-menu-link').click(function(){
    $('.submenu').removeClass('open');
    $('.header__burger-menu').toggleClass('open');
    $('.header__burger-menu-link').toggleClass('open');
    $(document.body).toggleClass('burger');
    $('#section-nav').css('display', 'block');
});

//Для отображения подразделов
$('.submenu__title').click(function(){
    $('.submenu').toggleClass('open');
    if($('.submenu .sidebar__list').length > 0) {   //есть sidebar
      if($('.submenu .sidebar__list').css('display') == "none")
        $('.submenu .sidebar__list').css('display', 'block');
      else
        $('.submenu .sidebar__list').css('display', 'none');
    }
    else {
      if($('.submenu__elems').css('display') == 'none')
        $('.submenu__elems').css('display', 'block');
      else
        $('.submenu__elems').css('display', 'none');
    }

});

//Для отображения календаря
$('.calendar__icon').click(function(){
    //$('.calendar__icon-block').toggleClass('open');

  //календарь для приоритетных проектов
  if($(".calendar__block:eq(0)").length > 0 ) {
    if($(".calendar__block:eq(0)").css("display") == "none") {
      $(".calendar__block:eq(0)").css("display", "block");
      //$(".calendar__year-pick").css("display", "inline-block");
    }
    else {
      $(".calendar__block:eq(0)").css("display", "none");
    }
    return;
  }



    //календарь для новостей по рубрикам и фото главы
    if($(".calendar__month:eq(0)").length > 0 ) {
        if($(".calendar__month:eq(0)").css("display") == "none") {
            $(".calendar__month:eq(0)").css("display", "block");
            $(".calendar__year-pick").css("display", "inline-block");
        }
        else {
            $(".calendar__month:eq(0)").css("display", "none");
        }
        return;
    }

    //календарь для всех новостей
    if( $(".submenu .calendar__container:eq(0)").length > 0 ) {
      if($(".submenu .calendar__container:eq(0)").css("display") == "none") {
        $(".submenu .calendar__container:eq(0)").css("display", "block");
        $(".submenu .calendar__year-pick").css("display", "inline-block");
        $(".submenu .calendar__month-pick").css("display", "inline-block");
      }
      else {
        $(".submenu .calendar__container:eq(0)").css("display", "none");
      }
      return;
    }

});

$('.banner__carousel').owlCarousel({
    loop: true,
    items: 1,
    nav: true,
    navClass: [
        'owl-prev banner__arrow banner__arrow--prev',
        'owl-next banner__arrow banner__arrow--next'
    ],
    navText: [
        '',
        ''
    ]
});