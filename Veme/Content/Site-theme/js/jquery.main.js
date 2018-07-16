// page init
$(document).ready(function() {

	"use strict";
	
	initCustomForms();
	// initialize custom form elements
	function initCustomForms() {
		jcf.replaceAll();
	}

	initAddClass();
	// Add Class  init
	function initAddClass() {
		"use strict";
		
		jQuery('.nav-opener').on( "click", function(e){
			e.preventDefault();
			jQuery("body").toggleClass("nav-active");
		});
	}

	initSameHeight();
	// align blocks height
	function initSameHeight() {
		jQuery('.offer-holder').sameHeight({
			elements: '.col',
			flexible: true
		});
	}
	

	initSlickSlider();
	// Slick Slider init
	function initSlickSlider() {
		"use strict";

		jQuery('.testimonail-slider').slick({
			dots: false,
			speed: 800,
			infinite: true,
			slidesToShow: 3,
			slidesToScroll: 1,
			adaptiveHeight: true,
			autoplay: false,
			arrows: false,
			autoplaySpeed: 4000,
			responsive: [
			    {
			      breakpoint: 1023,
			      settings: {
			        slidesToShow: 2,
			        infinite: true
			      }
			    },
			    {
			      breakpoint: 767,
			      settings: {
			        slidesToShow: 1,
			        infinite: true
			      }
			    }
			]
		});
	}

	initCounter();
	// Counter init
	function initCounter() {
		"use strict";

		jQuery('.counter').counterUp({
			delay: 10,
			time: 2000
		});
	}

	initStyleChanger();
	// style changer
	function initStyleChanger() {
		"use strict";
		
		var element = jQuery('#style-changer');

		if(element) {
			$.ajax({
				url: element.attr('data-src'),
				type: 'get',
				dataType: 'text',
				success: function(data) {
					var newContent = jQuery('<div>', {
						html: data
					});

					newContent.appendTo(element);
				}
			});
		}
	}
	
	initbackTop();
	// backtop init
	function initbackTop() {
		"use strict";

	    var jQuerybackToTop = jQuery("#back-top");
	    jQuery(window).on('scroll', function() {
	        if (jQuery(this).scrollTop() > 100) {
	            jQuerybackToTop.addClass('active');
	        } else {
	            jQuerybackToTop.removeClass('active');
	        }
	    });
	    jQuerybackToTop.on('click', function(e) {
	        jQuery("html, body").animate({scrollTop: 0}, 900);
	    });
	}

});

jQuery(window).on('load', function() {
		"use strict";

	initIsoTop();

	// IsoTop init
	function initIsoTop() {
		"use strict";

		var isotopeHolder = jQuery('.offer-holder'),
			win = jQuery(window);
		jQuery('.filter-list a').on( "click", function(e){
			e.preventDefault();
			
			jQuery('.filter-list li').removeClass('active');
			jQuery(this).parent('li').addClass('active');
			var selector = jQuery(this).attr('data-filter');
			isotopeHolder.isotope({ filter: selector });
		});
		jQuery('.offer-holder').isotope({
			itemSelector: '.col',
			transitionDuration: '0.6s',
			masonry: {
		    	columnWidth: '.col'
			}
		});
	}

	initPreLoader()
	// PreLoader init
	function initPreLoader() {
	    "use strict";

	    jQuery('#loader').delay(1000).fadeOut();
	}
});