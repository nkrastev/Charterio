(function ($) {
    'use strict';

    /*======== 1. PREELOADER ========*/    

    $(window).on('load', function () {
        $('#preloader').fadeOut(500);
    });

    /*======== 1. SMOOTH SCROLLING TO SECTION ========*/
    $('.scrolling  a[href*="#"]').on('click', function (e) {
        e.preventDefault();
        e.stopPropagation();
        var target = $(this).attr('href');
        $('.navbar-collapse').removeClass('show');
        function customVeocity(set_offset) {
            $(target).velocity('scroll', {
                duration: 800,
                offset: set_offset,
                easing: 'easeOutExpo',
                mobileHA: false
            });
        }

        if ($('#body').hasClass('up-scroll') || $('#body').hasClass('static')) {
            customVeocity(2);
        } else {
            customVeocity(-90);
        }

    });

    $(document).ready(function () {
        $('.removeNav').on('click', function () {
            $('.dropdown').hide();
        });
    });

    $(document).ready(function ($) {
        var n = function () {
            var ww = document.body.clientWidth;
            if (ww > 768) {
                $('.menuzord-menu').removeClass('remove-menuzord');
            } else if (ww <= 769) {
                $('.menuzord-menu').addClass('remove-menuzord');
            };
        };
        $(window).resize(function () {
            n();
        });
        //Fire it when the page first loads:
        n();
    });

    $(document).ready(function () {
        $('.scrollNav').on('click', function () {
            $('.remove-menuzord').hide();
        });
    });

    $(function () {
        $('.navComponents a').filter(function () { return this.href == location.href }).parent().addClass('active').siblings().removeClass('active')
        $('.navComponents a').click(function () {
            $(this).parent().addClass('active').siblings().removeClass('active')
        })
    });

    /*======== 2. NAVBAR ========*/
    $(window).on('load', function () {

        var header_area = $('.header');
        var main_area = header_area.find('.nav-menuzord');
        var zero = 0;
        var navbarSticky = $('.navbar-sticky');

        $(window).scroll(function () {
            var st = $(this).scrollTop();
            if (st > zero) {
                navbarSticky.addClass('navbar-scrollUp');
            } else {
                navbarSticky.removeClass('navbar-scrollUp');
            }
            zero = st;

            if (main_area.hasClass('navbar-sticky') && ($(this).scrollTop() <= 600 || $(this).width() <= 300)) {
                main_area.removeClass('navbar-scrollUp');
                main_area.removeClass('navbar-sticky').appendTo(header_area);
                header_area.css('height', 'auto');
            } else if (!main_area.hasClass('navbar-sticky') && $(this).width() > 300 && $(this).scrollTop() > 600) {
                header_area.css('height', header_area.height());
                main_area.addClass('navbar-scrollUp');
                main_area.css({ 'opacity': '0' }).addClass('navbar-sticky');
                main_area.appendTo($('body')).animate({ 'opacity': 1 });
            }
        });

        $(window).trigger('resize');
        $(window).trigger('scroll');
    });

    /* ======== ALL DROPDOWN ON HOVER======== */
    if ($(window).width() > 991) {
        $('.navbar-expand-lg .navbar-nav .dropdown').hover(function () {
            $(this).addClass('').find('.dropdown-menu').addClass('show');
        }, function () {
            $(this).find('.dropdown-menu').removeClass('show');
        });
    }

    if ($(window).width() > 767) {
        $('.navbar-expand-md .navbar-nav .dropdown').hover(function () {
            $(this).addClass('').find('.dropdown-menu').addClass('show');
        }, function () {
            $(this).find('.dropdown-menu').removeClass('show');
        });
    }


    /*======== DROPDOWN NOTIFY ========*/
    var dropdownToggle = $('.notify-toggler');
    var dropdownNotify = $('.dropdown-notify');

    if (dropdownToggle.length !== 0) {
        dropdownToggle.on('click', function () {
            if (!dropdownNotify.is(':visible')) {
                dropdownNotify.fadeIn(5);
            } else {
                dropdownNotify.fadeOut(5);
            }
        });

        $(document).mouseup(function (e) {
            if (!dropdownNotify.is(e.target) && dropdownNotify.has(e.target).length === 0) {
                dropdownNotify.fadeOut(5);
            }
        });
    }


    //============================== Menuzord =========================
    var menuzord = $('#menuzord');

    if (menuzord.length !== 0) {
        menuzord.menuzord({
            indicatorFirstLevel: '<i class="fas fa-chevron-down"></i>',
            indicatorSecondLevel: '<i class="fas fa-chevron-right"></i>'
        });
    }

 


    //===================== owl-carousel active data-hash =====================
    $('.owl-carousel').on('changed.owl.carousel', function (event) {
        var current = event.item.index;
        var hash = $(event.target).find('.owl-item').eq(current).find('.item').attr('data-hash');
        $('.' + hash).addClass('active');
    });

    $('.owl-carousel').on('change.owl.carousel', function (event) {
        var current = event.item.index;
        var hash = $(event.target).find('.owl-item').eq(current).find('.item').attr('data-hash');
        $('.' + hash).removeClass('active');
    });

    //============================== banner-carousel =========================
    var banner_carousel = $('#banner-carousel');

    if (banner_carousel.length !== 0) {
        banner_carousel.owlCarousel({
            margin: 0,
            loop: true,
            animateOut: 'fadeOut',
            animateIn: 'fadeIn',
            autoplay: true,
            autoplayHoverPause: true,
            autoplayTimeout: 5000,
            smartSpeed: 1200,
            dots: false,
            nav: true,
            navText: ['<i class="fa fa-angle-left"></i>', '<i class="fa fa-angle-right"></i>'],
            items: 1
        });
    }

 

    //============================== Rev-Slider =========================
    var homeMain = $('#rev_slider_12_1');
    if (homeMain.length !== 0) {
        $('#rev_slider_12_1').show().revolution({
            sliderType: 'standard',
            sliderLayout: 'fullwidth',
            dottedOverlay: 'none',
            delay: 6000,
            navigation: {
                keyboardNavigation: 'off',
                keyboard_direction: 'horizontal',
                mouseScrollNavigation: 'off',
                mouseScrollReverse: 'default',
                onHoverStop: 'off',
                arrows: {
                    style: 'metis',
                    enable: true,
                    hide_onmobile: true,
                    hide_under: 0,
                    hide_onleave: true,
                    hide_delay: 200,
                    hide_delay_mobile: 1200,
                    tmp: '',
                    left: {
                        h_align: 'left',
                        v_align: 'center',
                        h_offset: 20,
                        v_offset: 0
                    },
                    right: {
                        h_align: 'right',
                        v_align: 'center',
                        h_offset: 20,
                        v_offset: 0
                    }
                },
                bullets: {
                    enable: true,
                    hide_onmobile: false,
                    style: 'hesperiden',
                    hide_onleave: false,
                    direction: 'horizontal',
                    h_align: 'center',
                    v_align: 'bottom',
                    h_offset: 0,
                    v_offset: 20,
                    space: 5,
                    tmp: ''
                }
            },
            responsiveLevels: [1240, 1025, 778, 480],
            visibilityLevels: [1240, 1025, 778, 480],
            gridwidth: [1170, 1024, 769, 480],
            gridheight: [600, 743, 557, 557],
            lazyType: 'none',
            shadow: 0,
            spinner: 'off',
            stopLoop: 'on',
            stopAfterLoops: -1,
            stopAtSlide: -1,
            shuffle: 'off',
            autoHeight: 'off',
            disableProgressBar: 'on',
            hideThumbsOnMobile: 'off',
            hideSliderAtLimit: 0,
            hideCaptionAtLimit: 0,
            hideAllCaptionAtLilmit: 0,
            debugMode: false,
            fallbacks: {
                simplifyAll: 'off',
                nextSlideOnWindowFocus: 'off',
                disableFocusListener: false
            }
        });
    }

    //============================== Rev-Slider Home-2 =========================
    var homeCity = $('#rev_slider_18_1');
    if (homeCity.length !== 0) {
        $('#rev_slider_18_1').show().revolution({
            sliderType: 'standard',
            sliderLayout: 'fullwidth',
            dottedOverlay: 'none',
            delay: 9000,
            navigation: {
                keyboardNavigation: 'off',
                keyboard_direction: 'horizontal',
                mouseScrollNavigation: 'off',
                mouseScrollReverse: 'default',
                onHoverStop: 'off',
                arrows: {
                    style: 'metis',
                    enable: true,
                    hide_onmobile: true,
                    hide_under: 0,
                    hide_onleave: true,
                    hide_delay: 200,
                    hide_delay_mobile: 1200,
                    tmp: '',
                    left: {
                        h_align: 'left',
                        v_align: 'center',
                        h_offset: 20,
                        v_offset: 0
                    },
                    right: {
                        h_align: 'right',
                        v_align: 'center',
                        h_offset: 20,
                        v_offset: 0
                    }
                },
                bullets: {
                    enable: true,
                    hide_onmobile: false,
                    style: 'hesperiden',
                    hide_onleave: false,
                    direction: 'horizontal',
                    h_align: 'center',
                    v_align: 'bottom',
                    h_offset: 0,
                    v_offset: 20,
                    space: 5,
                    tmp: ''
                }
            },
            responsiveLevels: [1240, 1025, 778, 480],
            visibilityLevels: [1240, 1025, 778, 480],
            gridwidth: [1170, 1025, 769, 480],
            gridheight: [745, 743, 557, 557],
            lazyType: 'none',
            shadow: 0,
            spinner: 'off',
            stopLoop: 'off',
            stopAfterLoops: -1,
            stopAtSlide: -1,
            shuffle: 'off',
            autoHeight: 'off',
            disableProgressBar: 'on',
            hideThumbsOnMobile: 'off',
            hideSliderAtLimit: 0,
            hideCaptionAtLimit: 0,
            hideAllCaptionAtLilmit: 0,
            debugMode: false,
            fallbacks: {
                simplifyAll: 'off',
                nextSlideOnWindowFocus: 'off',
                disableFocusListener: false
            }
        });
    }



    //============================== daterangepicker =========================
    var daterangepicker = $('input[name="StartFlightDate"]');

    if (daterangepicker.length !== 0) {
        daterangepicker.daterangepicker({
            autoUpdateInput: false,
            singleDatePicker: true,

        });
    }

    $('input[name="StartFlightDate"]').on('apply.daterangepicker', function (ev, picker) {
        $(this).val(picker.startDate.format('MM/DD/YYYY'));
    });

    $('input[name="StartFlightDate"]').on('cancel.daterangepicker', function (ev, picker) { $(this).val(''); });



    var daterangepicker = $('input[name="EndFlightDate"]');

    if (daterangepicker.length !== 0) {
        daterangepicker.daterangepicker({
            autoUpdateInput: false,
            singleDatePicker: true,
            locale: {
                cancelLabel: 'Clear'
            }
        });
    }

    $('input[name="EndFlightDate"]').on('apply.daterangepicker', function (ev, picker) { $(this).val(picker.endDate.format('MM/DD/YYYY')); });

    $('input[name="EndFlightDate"]').on('cancel.daterangepicker', function (ev, picker) { $(this).val(''); });





    //============================== SELECT 2 =========================
    var select2_select = $(".select2-select");

    if (select2_select.length !== 0) {
        select2_select.select2();
    }

    //============================== SELECTRIC =========================
    var select_option = $(".select-option");

    if (select_option.length !== 0) {
        select_option.selectric();
    }

    /*========  syotimer ========*/
    var coming_soon = $('#coming-soon');

    if (coming_soon.length !== 0) {
        coming_soon.syotimer({
            year: 2022,
            month: 10,
            day: 10,
            hour: 20,
            minute: 30
        });
    }

    /*======== Tooltip  ========*/
    var tooltipTriggerList = [].slice.call(document.querySelectorAll('[data-bs-toggle="tooltip"]'))
    var tooltipList = tooltipTriggerList.map(function (tooltipTriggerEl) {
        return new bootstrap.Tooltip(tooltipTriggerEl)
    })



    var sliderValue = [
        document.getElementById('lower-value'), // 0
        document.getElementById('upper-value')  // 1
    ];

    // Display the slider value and how far the handle moved
    // from the left edge of the slider.
    var priceRange = document.getElementById('price-range');
    if (priceRange) {
        nonLinearStepSlider.noUiSlider.on('update', function (values, handle) {
            sliderValue[handle].innerHTML = '$' + Math.floor(values[handle]);
        });
    }

    /*======== COUNT INPUT (Quantity) ========*/
    $('.incr-btn').on('click', function (e) {
        var newVal;
        var $button = $(this);
        var oldValue = $button.parent().find('.quantity').val();
        $button.parent().find('.incr-btn[data-action=decrease]').removeClass('inactive');
        if ($button.data('action') === 'increase') {
            newVal = parseFloat(oldValue) + 1;
        } else {
            // Don't allow decrementing below 1
            if (oldValue > 1) {
                newVal = parseFloat(oldValue) - 1;
            } else {
                newVal = 0;
                $button.addClass('inactive');
            }
        }
        $button.parent().find('.quantity').val(newVal);
        e.preventDefault();
    });



  

    //============================== ICON TOGGLER ANIMATION =========================
    var searchItem = $('.search-item');
    var btnSearch = $('.btn-search');
    var searchBox = $('.search-box');
    var searchIcon = $('.search-icon');
    var closeIcon = $('.close-icon');

    if (btnSearch.length !== 0) {
        btnSearch.on('click', function () {
            if (!searchBox.is(':visible')) {
                searchBox.fadeIn(5);
                searchBox.addClass('open');
                searchItem.addClass('showItem');
                searchIcon.addClass('d-none');
                closeIcon.removeClass('d-none');
                $(this).addClass('expand');
            } else {
                searchBox.fadeOut(5);
                searchBox.removeClass('open');
                searchItem.removeClass('showItem');
                searchIcon.removeClass('d-none');
                closeIcon.addClass('d-none');
                $(this).removeClass('expand');
            }
        });

        $(document).mouseup(function (e) {
            if (!searchBox.is(e.target) && searchBox.has(e.target).length === 0) {
                searchBox.fadeOut(5);
                searchBox.removeClass('open');
                searchIcon.removeClass('d-none');
                closeIcon.addClass('d-none');
                searchItem.removeClass('showItem');
                btnSearch.removeClass('expand');
            }
        });
    }

    /* Google Maps */
    function initialize() {             
        //Custom Style
        var styles = [
            {
                "elementType": "geometry",
                "stylers": [
                    {
                        "color": "#f5f5f5"
                    }
                ]
            },
            {
                "elementType": "labels.icon",
                "stylers": [
                    {
                        "visibility": "off"
                    }
                ]
            },
            {
                "elementType": "labels.text.fill",
                "stylers": [
                    {
                        "color": "#616161"
                    }
                ]
            },
            {
                "elementType": "labels.text.stroke",
                "stylers": [
                    {
                        "color": "#f5f5f5"
                    }
                ]
            },
            {
                "featureType": "administrative.land_parcel",
                "elementType": "labels.text.fill",
                "stylers": [
                    {
                        "color": "#bdbdbd"
                    }
                ]
            },
            {
                "featureType": "poi",
                "elementType": "geometry",
                "stylers": [
                    {
                        "color": "#eeeeee"
                    }
                ]
            },
            {
                "featureType": "poi",
                "elementType": "labels.text.fill",
                "stylers": [
                    {
                        "color": "#757575"
                    }
                ]
            },
            {
                "featureType": "poi.park",
                "elementType": "geometry",
                "stylers": [
                    {
                        "color": "#e5e5e5"
                    }
                ]
            },
            {
                "featureType": "poi.park",
                "elementType": "labels.text.fill",
                "stylers": [
                    {
                        "color": "#9e9e9e"
                    }
                ]
            },
            {
                "featureType": "road",
                "elementType": "geometry",
                "stylers": [
                    {
                        "color": "#ffffff"
                    }
                ]
            },
            {
                "featureType": "road.arterial",
                "elementType": "labels.text.fill",
                "stylers": [
                    {
                        "color": "#757575"
                    }
                ]
            },
            {
                "featureType": "road.highway",
                "elementType": "geometry",
                "stylers": [
                    {
                        "color": "#dadada"
                    }
                ]
            },
            {
                "featureType": "road.highway",
                "elementType": "labels.text.fill",
                "stylers": [
                    {
                        "color": "#616161"
                    }
                ]
            },
            {
                "featureType": "road.local",
                "elementType": "labels.text.fill",
                "stylers": [
                    {
                        "color": "#9e9e9e"
                    }
                ]
            },
            {
                "featureType": "transit.line",
                "elementType": "geometry",
                "stylers": [
                    {
                        "color": "#e5e5e5"
                    }
                ]
            },
            {
                "featureType": "transit.station",
                "elementType": "geometry",
                "stylers": [
                    {
                        "color": "#eeeeee"
                    }
                ]
            },
            {
                "featureType": "water",
                "elementType": "geometry",
                "stylers": [
                    {
                        "color": "#c9c9c9"
                    }
                ]
            },
            {
                "featureType": "water",
                "elementType": "labels.text.fill",
                "stylers": [
                    {
                        "color": "#9e9e9e"
                    }
                ]
            }
        ]
        var myLatLng = { lat: 42.698334, lng: 23.319941 };

        var mapOptions = {
            zoom: 14,
            scrollwheel: false,
            center: new google.maps.LatLng(42.698334, 23.319941),
            styles: styles

        };
        var map = new google.maps.Map(document.getElementById('map'), mapOptions);

        var logoMarker = new google.maps.Marker({
            position: { lat: 42.688240, lng: 23.328331 },
            map,
            icon: "/assets/img/logo-for-map.png"
        });

        logoMarker.setMap(map);
        
    }
    var mapId = $('#map');
    if (mapId.length) {
        google.maps.event.addDomListener(window, 'load', initialize);
    }

})(jQuery);