




// JavaScript Document
$(function () {
	var ticker = $("#ticker1");
	var ticker2 = $("#ticker2");
	ticker.children().filter("ul").each(function () {

		var dt = $(this),

			container = $("<div>");
		dt.next().appendTo(container);
		dt.prependTo(container);
		container.appendTo(ticker);
	});
	ticker.css("overflow", "hidden");

	function animator(currentItem) {

		var distance = currentItem.height();
		duration = (distance + parseInt(currentItem.css("marginTop"))) / 0.025;
		currentItem.animate({
			marginTop: -distance
		}, duration, "linear", function () {
			currentItem.appendTo(currentItem.parent()).css("marginTop", 0);
			animator(currentItem.parent().children(":first"));
		});
	};
	animator(ticker.children(":first"));
	var j = 0
	$('#stop').click(function () {

		ticker.children().stop();
		j = 1
		$('#stop').hide();
		$('#play').show();

		$("#ticker1").css("overflow", "scroll");
		$("#ticker1 li").addClass("stopped");

	});


	ticker.mouseenter(function () {
		ticker.children().stop();
	});

	$('#play').click(function () {

		animator(ticker.children(":first"));
		j = 0;
		$('#stop').show();
		$('#play').hide();

		$("#ticker1").css("overflow", "hidden");
		$("#ticker1 li").removeClass('stopped');
	});

	ticker.mouseleave(function () {
		if (j == 0)
			animator(ticker.children(":first"));
	});





	ticker2.children().filter("ul").each(function () {
		var dt = $(this),
			container = $("<div>");
		dt.next().appendTo(container);
		dt.prependTo(container);
		container.appendTo(ticker2);
	});
	ticker2.css("overflow", "hidden");

	animator(ticker2.children(":first"));
	var k = 0
	$('#stop2').click(function () {
		ticker2.children().stop();
		k = 1
		$('#stop2').hide();
		$('#play2').show();

		$("#ticker2").css("overflow", "scroll");
		$("#ticker2 li").addClass("stopped");

	});


	ticker2.mouseenter(function () {
		ticker2.children().stop();
	});

	$('#play2').click(function () {
		animator(ticker2.children(":first"));
		k = 0
		$('#stop2').show();
		$('#play2').hide();

		$("#ticker2").css("overflow", "hidden");
		$("#ticker2 li").removeClass("stopped");

	});

	ticker2.mouseleave(function () {
		if (k == 0)
			animator(ticker2.children(":first"));
	});

	$(".runningCourses").prepend("<span class='lftTop'></span><span class='rgtTop'></span><span class='btLft'></span><span class='btRgt'></span>");
	$(".commingCourses").prepend("<span class='lftTop1'></span><span class='rgtTop1'></span><span class='btLft1'></span><span class='btRgt1'></span>");
	$(".edge").prepend("<span class='lftEdge'></span><span class='rgtEdge'></span>");
	$(".message").prepend("<span></span>");
	$(document).ready(function () {
		$(".sf-vertical li:odd").addClass("even");
		$(".dataTable tr:even").addClass("even");
		$("#tabs table tr:odd").addClass("eventr");
	});

});



jQuery(function($) {

  "use strict";


/*------------------------------------------------------------------------------*/
/* Preloader
/*------------------------------------------------------------------------------*/
   // makes sure the whole site is loaded
    $(window).on("load",function(){
        $(".loader-blob").fadeOut(),$("#preloader").delay(300).fadeOut("slow",function(){$(this).remove()})
    })


/*------------------------------------------------------------------------------*/
/* header_search
/*------------------------------------------------------------------------------*/

    jQuery(".header_search a").addClass('sclose');
    jQuery(".header_search a").on('click',function(){
        jQuery(".field.searchform-s").focus();
        if(jQuery('.header_search a').hasClass('sclose')){
            jQuery(".header_search a i").removeClass('icon-search-1').addClass('icon-search-1');
            jQuery(this).removeClass('sclose').addClass('open');
            jQuery(".header_search_content").addClass('on')
        }
        else{jQuery(this).removeClass('open').addClass('sclose');
            jQuery(".header_search_content").removeClass('on')
        }
    });
    jQuery(".header_search_content .close_btn").on('click',function(){
        jQuery('.header_search a').removeClass('open').addClass('sclose');
        jQuery(".header_search_content").removeClass('on')
    });


 
/*------------------------------------------------------------------------------*/
/* Fixed-header
/*------------------------------------------------------------------------------*/

    $(window).on("scroll",function(){
        if ( matchMedia( 'only screen and (min-width: 1200px)' ).matches ) 
        {
            if ($(window).scrollTop() >= 50 ) {

                $('.cmt-stickable-header').addClass('fixed-header');
            }
            else {

                $('.cmt-stickable-header').removeClass('fixed-header');
            }
        }
    });


/*------------------------------------------------------------------------------*/
/* Menu
/*------------------------------------------------------------------------------*/
        
        var menu = {
        initialize: function() {
            this.Menuhover();
        },

        Menuhover : function(){
            var getNav = $("nav.main-menu"),
                getWindow = $(window).width(),
                getHeight = $(window).height(),
                getIn = getNav.find("ul.menu").data("in"),
                getOut = getNav.find("ul.menu").data("out");
            
            if ( matchMedia( 'only screen and (max-width: 1200px)' ).matches ) {
                                                     
                // Enable click event
                $("nav.main-menu ul.menu").each(function(){
                    
                    // Dropdown Fade Toggle
                    $("a.mega-menu-link", this).on('click', function (e) {
                        e.preventDefault();
                        var t = $(this);
                        t.toggleClass('active').next('ul').toggleClass('active');
                    });   

                    // Megamenu style
                    $(".megamenu-fw", this).each(function(){
                        $(".col-menu", this).each(function(){
                            $(".title", this).off("click");
                            $(".title", this).on("click", function(){
                                $(this).closest(".col-menu").find(".content").stop().toggleClass('active');
                                $(this).closest(".col-menu").toggleClass("active");
                                return false;
                                e.preventDefault();
                                
                            });

                        });
                    });  
                    
                }); 
            }
        },
    };

    
    $('.btn-show-menu-mobile').on('click', function(e){
        $(this).toggleClass('is-active'); 
        $('.menu-mobile').toggleClass('show'); 
        return false;
        e.preventDefault();  
    });

    // Initialize
    $(document).ready(function(){
        menu.initialize();

    });


    
    var $bannerSlider = jQuery('.banner_slider');
    var $bannerFirstSlide = $('div.slide:first-child');

    $bannerSlider.on('init', function (e, slick) {
      var $firstAnimatingElements = $bannerFirstSlide.find('[data-animation]');
      slideanimate($firstAnimatingElements);
    });
    $bannerSlider.on('beforeChange', function (e, slick, currentSlide, nextSlide) {
      var $animatingElements = $('div.slick-slide[data-slick-index="' + nextSlide + '"]').find('[data-animation]');
      slideanimate($animatingElements);
    });
    $bannerSlider.slick({
      slidesToShow: 1,
      slidesToScroll: 1,
      arrows: true,
      fade: true,
      dots: false,
      swipe: true,
      adaptiveHeight: true,
      responsive: [

        {
            breakpoint: 1200,
            settings: {
                arrows: false
            }
        },
        {
            breakpoint: 767,
                settings: {
                slidesToShow: 1,
                slidesToScroll: 1,
                arrows: false,                
                autoplay: false,
                autoplaySpeed: 4000,
                swipe: true } 
            }] });

    function slideanimate(elements) {
      var animationEndEvents = 'webkitAnimationEnd mozAnimationEnd MSAnimationEnd oanimationend animationend';
      elements.each(function () {
        var $this = $(this);
        var $animationDelay = $this.data('delay');
        var $animationType = 'animated ' + $this.data('animation');
        $this.css({
          'animation-delay': $animationDelay,
          '-webkit-animation-delay': $animationDelay });

        $this.addClass($animationType).one(animationEndEvents, function () {
          $this.removeClass($animationType);
        });
      });
    }
 

     /* side-menu */
    $(".side-menu-container").each(function(){  
        $(".side-menu > a", this).on("click", function(e){
            e.preventDefault();
            $(".side-overlay").toggleClass("on");
            $("body").toggleClass("on-side");
        });
    });
    $(".side .close-side").on("click", function(e){
        e.preventDefault();
        $(".side-overlay").removeClass("on");
        $("body").removeClass("on-side");
    });  


/*------------------------------------------------------------------------------*/
/* Animation on scroll: Number rotator
/*------------------------------------------------------------------------------*/
    
    $("[data-appear-animation]").each(function() {
    var self      = $(this);
    var animation = self.data("appear-animation");
    var delay     = (self.data("appear-animation-delay") ? self.data("appear-animation-delay") : 0);
        
        if( $(window).width() > 959 ) {
            self.html('0');
            self.waypoint(function(direction) {
                if( !self.hasClass('completed') ){
                    var from     = self.data('from');
                    var to       = self.data('to');
                    var interval = self.data('interval');
                    self.numinate({
                        format: '%counter%',
                        from: from,
                        to: to,
                        runningInterval: 2000,
                        stepUnit: interval,
                        onComplete: function(elem) {
                            self.addClass('completed');
                        }
                    });
                }
            }, { offset:'85%' });
        } else {
            if( animation == 'animateWidth' ) {
                self.css('width', self.data("width"));
            }
        }
    });


   
/*------------------------------------------------------------------------------*/
/* Skillbar
/*------------------------------------------------------------------------------*/
    
    $('.cmt-progress-bar').each(function() {
        $(this).find('.progress-bar').width(0);
    });

    $('.cmt-progress-bar').each(function() {

        $(this).find('.progress-bar').animate({
            width: $(this).attr('data-percent')
        }, 2000);
    });


    // Part of the code responsible for loading percentages:

    $('.progress-bar-percent[data-percentage]').each(function () {

        var progress = $(this);
        var percentage = Math.ceil($(this).attr('data-percentage'));

            $({countNum: 0}).animate({countNum: percentage}, {
                duration: 2000,
                easing:'linear',
                step: function() {
                // What todo on every count
                    var pct = '';
                    if(percentage === "0"){
                        pct = Math.floor(this.countNum) + '%';
                    }else{
                        pct = Math.floor(this.countNum+1) + '%';
                    }
                    progress.text(pct);
                }
            });
    });


    jQuery(".cmt-circle-box").each(function () {

        var circle_box = jQuery(this);
        var fill_val = circle_box.data("fill");
        var emptyFill_val = circle_box.data("emptyfill");
        var thickness_val = circle_box.data("thickness");
        var linecap_val = circle_box.data("linecap")
        var fill_gradient = circle_box.data("gradient");
        var startangle_val = (-Math.PI / 4) * 1.5;
        if (fill_gradient != "") {
            fill_gradient = fill_gradient.split("|");
            fill_val = { gradient: [fill_gradient[0], fill_gradient[1]] };
        }
        if (typeof jQuery.fn.circleProgress == "function") {
            var digit = circle_box.data("digit");
            var before = circle_box.data("before");
            var after = circle_box.data("after");
            var digit = Number(digit);
            var short_digit = digit / 100;
            var size_val = circle_box.data("size");
            jQuery(".cmt-circle", circle_box)
                .circleProgress({ value: 0, duration: 8000, size: size_val, startAngle: startangle_val, 
                    thickness: thickness_val, linecap:linecap_val, emptyFill: emptyFill_val, fill: fill_val })
                .on("circle-animation-progress", function (event, progress, stepValue) {
                    
                    circle_box.find(".cmt-fid-number").html(before + Math.round(stepValue * 100) + after);
                });
        }
        circle_box.waypoint(
            function (direction) {

                if (!circle_box.hasClass("completed")) {
                    if (typeof jQuery.fn.circleProgress == "function") {
                        jQuery(".cmt-circle", circle_box).circleProgress({ value: short_digit });
                    }
                    circle_box.addClass("completed");
                }
            },
            { offset: "90%" }
        );
    });


    jQuery( document ).ready(function($) { aqovo_logMarginPadding_content();});
    function aqovo_logMarginPadding_content(){ 
        jQuery(".cmt-expandcontent-yes").each(function () {
            var cmt_column_div = '';
            var scrren_size = jQuery(window).width();
            var box_size = jQuery(this).parent().width();
            var extra_size = (scrren_size - box_size) / 3;

            if (jQuery(this).hasClass('cmt-right-span')) {
                cmt_column_div = ', .cmt-expandcontent_column > .cmt-expandcontent_wrapper ';
                jQuery('.cmt-expandcontent_column > div' + cmt_column_div, jQuery(this)).css('margin-right', '-' + extra_size + 'px');
            } else if (jQuery(this).hasClass('cmt-left-span')) {
                cmt_column_div = ', .cmt-expandcontent_column > .cmt-expandcontent_wrapper ';
                jQuery('.cmt-expandcontent_column > div' + cmt_column_div, jQuery(this)).css('margin-left', '-' + extra_size + 'px');
            }

        });
    }jQuery(window).resize(function() {aqovo_logMarginPadding_content(); });


/*------------------------------------------------------------------------------*/
/* Tab
/*------------------------------------------------------------------------------*/ 

    $('.cmt-tabs > .tabs').children('li').on('click', function(e) {  

        var tab = $(this).closest('.cmt-tabs > .tabs > .tab'), 

        index = $(this).closest('.cmt-tabs > .tabs > li').index();

        $(this).parents('.cmt-tabs').children('.tabs').children('li.active ').removeClass('active'); 

        $(this).addClass('active'); 
        $(this).addClass('active').parents('.cmt-tabs').children('.content-tab').find('.content-inner').not('.content-inner:eq(' + index + ')').slideUp();
        $(this).addClass('active').parents('.cmt-tabs').children('.content-tab').find('.content-inner:eq(' + index + ')').slideDown();

        e.preventDefault();
    });



/*------------------------------------------------------------------------------*/
/* Accordion
/*------------------------------------------------------------------------------*/

    var allPanels = $('.accordion > .toggle').children('.toggle-content').hide();

    $('.toggle-title').on('click',function(e) {

        e.preventDefault();
        var $this = $(this);
            $this.parent().parent().find('.toggle .toggle-title a').removeClass('active');

        if ($this.next().hasClass('show')) {

            $this.next().removeClass('show');   
            $this.next().slideUp('easeInExpo');

        } else {
            $this.parent().parent().find('.toggle .toggle-content').removeClass('show');
            $this.parent().parent().find('.toggle .toggle-content').slideUp('easeInExpo');
            $this.next().toggleClass('show');
            $this.next().removeClass('show');
            $this.next().slideToggle('easeInExpo');
           $this.next().parent().children().children().addClass('active');

        }

    });



/*------------------------------------------------------------------------------*/
/* Isotope
/*------------------------------------------------------------------------------*/

   $(function () {

        if ( $().isotope ) {           
            var $container = $('.isotope-project');
            $container.imagesLoaded(function(){
                $container.isotope({
                    itemSelector: '',
                    transitionDuration: '1s',
                });
            });

            $('.portfolio-filter li').on('click',function() {                           
                var selector = $(this).find("a").attr('data-filter');
                $('.portfolio-filter li').removeClass('active');
                $(this).addClass('active');
                $container.isotope({ filter: selector });
                return false;
            });
        };

   });


    
/*------------------------------------------------------------------------------*/
/* Prettyphoto
/*------------------------------------------------------------------------------*/
    $(function () {

        // Normal link
        jQuery('a[href*=".jpg"], a[href*=".jpeg"], a[href*=".png"], a[href*=".gif"]').each(function(){
            if( jQuery(this).attr('target')!='_blank' && !jQuery(this).hasClass('prettyphoto') ){
                var attr = $(this).attr('rel');
                if (typeof attr !== typeof undefined && attr !== false && attr!='prettyPhoto' ) {
                    jQuery(this).attr('data-rel','prettyPhoto');
                }
            }
        });    

        jQuery('a[data-rel^="prettyPhoto"]').prettyPhoto();
        jQuery('a.cmt_prettyphoto').prettyPhoto();
        jQuery('a[rel^="prettyPhoto"]').prettyPhoto();
    });
    

    $(window).on('load', function(){

    function gridMasonry(){
        var grid = $(".masonry-grid")
        if( grid.length ){
            
          grid.isotope({
            itemSelector: '.masonry-grid-item',
            percentPosition: true,
            layoutMode: 'masonry',
            masonry: {
              columnWidth: '.grid-sizer',
            },
          });
            
        }
    }
    gridMasonry();
});

/*------------------------------------------------------------------------------*/
/* Slick_slider
/*------------------------------------------------------------------------------*/
    $(".slick_slider").slick({
        speed: 1000,
        infinite: true,
        arrows: false,
        dots: false,                   
        autoplay: false,
        centerMode : false,

        responsive: [{

            breakpoint: 1360,
            settings: {
            slidesToShow: 3,
            slidesToScroll: 3
            }
        },
        {
            breakpoint: 1024,
            settings: {
            slidesToShow: 3,
            slidesToScroll: 3
            }
        },
        {
            breakpoint: 680,
            settings: {
                slidesToShow: 2,
                slidesToScroll: 2
            }
        },
        {
            breakpoint: 575,
            settings: {
                slidesToShow: 1,
                slidesToScroll: 1
            }
        }]
    });


/*------------------------------------------------------------------------------*/
/* Back to top
/*------------------------------------------------------------------------------*/
    
    // ===== Scroll to Top ==== 
    jQuery('#totop').hide();

    $(window).on("scroll",function(){
        if (jQuery(this).scrollTop() >= 500) {        // If page is scrolled more than 50px
            jQuery('#totop').fadeIn(200);    // Fade in the arrow
            jQuery('#totop').addClass('top-visible');
        } else {
            jQuery('#totop').fadeOut(200);   // Else fade out the arrow
            jQuery('#totop').removeClass('top-visible');
        }
    });

    jQuery('#totop').on("click",function() {      // When arrow is clicked
        jQuery('body,html').animate({
            scrollTop : 0                       // Scroll to top of body
        }, 500);
        return false;
    });



});