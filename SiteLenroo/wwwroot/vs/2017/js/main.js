$(function () {
    var owl = $('.gallery__container')
        .owlCarousel({
            items: 1,
            loop: true,

            nav: true,
            navText: ['', ''],
            navClass: [
                'owl-prev gallery__button gallery__button--left',
                'owl-next gallery__button gallery__button--right'
            ],

            mouseDrag: false,

            autoplay: true,
            autoplayTimeout: 5000,
            autoplayHoverPause: true,

            animateIn: 'fadeIn',
            animateOut: 'fadeOut',

            video: true,
            videoWidth: 870,
            videoHeight: 618
        })
    ;

    $(document)
        .on(
            'click',
            '[href="#"]',
            function (event) {
                event.preventDefault();
            }
        )
        .on(
            'click',
            '.gallery__preview-link',
            function () {
                owl.trigger('to.owl.carousel', $(this).data('itemId'))
            }
        )
    ;

    $('[data-toggle="tooltip"]').tooltip();
});


var toggler = document.getElementsByClassName("caret-tree");
var i;

for (i = 0; i < toggler.length; i++) {
    toggler[i].addEventListener("click", function () {
        this.parentElement.querySelector(".nested-tree").classList.toggle("active-tree");
        this.classList.toggle("caret-down-tree");
    });
}