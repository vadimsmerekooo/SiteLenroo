if (typeof Array.prototype.forEach != 'function') {
    Array.prototype.forEach = function (callback) {
        for (var i = 0; i < this.length; i++) {
            callback.apply(this, [this[i], i, this]);
        }
    };
}

var MorphDropdown = function (element) {
    this.element = element;
    this.container = $('body > .container');
    this.mainNavigation = this.element.find('.main-menu__nav');
    this.mainNavigationItems = this.mainNavigation.find('.main-menu__nav--has_dropdown');
    this.dropdownList = this.element.find('.main-menu__dropdown-list');
    this.dropdownBg = this.dropdownList.find('.bg__layer');
    this.bindEvents();
};

MorphDropdown.prototype.bindEvents = function () {
    var self = this;
    //hover over an item in the main navigation

    var leaveElement = function () {
        setTimeout(function () {
            //if not hovering over a dropdown or a nav item -> hide dropdown
            (
                self.mainNavigation.find('.main-menu__nav--has_dropdown:hover').length == 0
                && self.element.find('.main-menu__dropdown-list:hover').length == 0
            ) && self.hideDropdown();
        }, 50);
    };

    this.mainNavigationItems
        .mouseenter(function () {
            //hover over one of the nav items -> show dropdown
            self.showDropdown($(this));
        })
        .mouseleave(leaveElement)
    ;

    //hover over the dropdown
    this.dropdownList.mouseleave(leaveElement);

    //click on an item in the main navigation -> open a dropdown on a touch device
    this.mainNavigationItems.on('touchstart', function (event) {
        var selectedDropdown = self.dropdownList.find('#' + $(this).data('content'));
        if (!self.element.hasClass('main-menu__dropdown--visible') || !selectedDropdown.hasClass('active')) {
            event.preventDefault();
            self.showDropdown($(this));
        }
    });
};

MorphDropdown.prototype.showDropdown = function (item) {
    var self = this;
    var selectedDropdown = this.dropdownList.find('#' + item.data('content')),
        selectedDropdownHeight = selectedDropdown.height(),
        selectedDropdownWidth = selectedDropdown.children('.content').outerWidth(),
        selectedDropdownLeft = item.offset().left - this.container.offset().left;

    // @TODO 'Учитывать высоту, когда меню в 2 и более строки'
    var offsetRight = (this.container.outerWidth() - this.mainNavigation.width()) / 2;
    var isDiff = (item.offset().left + selectedDropdown.width()) > (this.container.offset().left + this.container.outerWidth() - offsetRight);

    if (isDiff) {
        selectedDropdownLeft = this.container.outerWidth() - offsetRight - selectedDropdown.width();
    }

    //update dropdown position and size
    this.updateDropdown(
        parseInt(selectedDropdownHeight),
        parseInt(selectedDropdownWidth),
        parseInt(selectedDropdownLeft)
    );
    //add active class to the proper dropdown item
    this.element.find('.active').removeClass('active');
    selectedDropdown
        .addClass('active')
        .removeClass('move-left move-right')
        .prevAll()
        .addClass('move-left').end()
        .nextAll()
        .addClass('move-right')
    ;
    item.addClass('active');

    //show the dropdown wrapper if not visible yet
    if (!this.element.hasClass('main-menu__dropdown--visible')) {
        setTimeout(function () {
            self.element.addClass('main-menu__dropdown--visible');
        }, 10);
    }
};

MorphDropdown.prototype.updateDropdown = function (height, width, left) {
    this.dropdownList.css({
        '-moz-transform': 'translateX(' + left + 'px)',
        '-webkit-transform': 'translateX(' + left + 'px)',
        '-ms-transform': 'translateX(' + left + 'px)',
        '-o-transform': 'translateX(' + left + 'px)',
        'transform': 'translateX(' + left + 'px)',
        'width': width + 'px',
        'height': height + 'px'
    });

    // @TODO 'Поправить пересчет bgc при масштабе'
    this.dropdownBg.css({
        '-moz-transform': 'scaleX(' + width + ') scaleY(' + height + ')',
        '-webkit-transform': 'scaleX(' + width + ') scaleY(' + height + ')',
        '-ms-transform': 'scaleX(' + width + ') scaleY(' + height + ')',
        '-o-transform': 'scaleX(' + width + ') scaleY(' + height + ')',
        'transform': 'scaleX(' + width + ') scaleY(' + height + ')'
    });
};

MorphDropdown.prototype.hideDropdown = function () {
    this
        .element.removeClass('main-menu__dropdown--visible')
        .find('.active').removeClass('active').end()
        .find('.move-left').removeClass('move-left').end()
        .find('.move-right').removeClass('move-right')
    ;
};

MorphDropdown.prototype.resetDropdown = function () {
    this.dropdownList.removeAttr('style');
};

var morphDropdowns = [];
var $mainMenu = $('.main-menu');

if ($mainMenu.length > 0) {
    $mainMenu.each(function () {
        //create a MorphDropdown object for each .menu-dropdown
        morphDropdowns.push(new MorphDropdown($(this)));
    });

    var resizing = false;

    //on resize, reset dropdown style property
    updateDropdownPosition();
    $(window).on('resize', function () {
        if (!resizing) {
            resizing = true;
            (!window.requestAnimationFrame)
                ? setTimeout(updateDropdownPosition, 300)
                : window.requestAnimationFrame(updateDropdownPosition);
        }
    });

    function updateDropdownPosition() {
        morphDropdowns.forEach(function (element) {
            element.resetDropdown();
        });

        resizing = false;
    }
}
