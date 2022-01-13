// Step 1: Create jQuery plugin
// ============================

$.fn.fancyMorph = function( opts ) {

  var Morphing = function( $btn, opts ) {
    var self = this;

    self.opts = $.extend({
      animationEffect : false,
      infobar    : false,
      buttons    : ['close'],
      smallBtn   : false,
      touch      : false,
      baseClass  : 'fancybox-morphing',
      afterClose : function() {
        self.close();
      }
    }, opts);

    self.init( $btn );
  };

  Morphing.prototype.init = function( $btn ) {
    var self = this;

    self.$btn = $btn.addClass('morphing-btn');

    self.$clone = $('<div class="morphing-btn-clone" />')
      .hide()
      .insertAfter( $btn );

    // Add wrapping element and set initial width used for positioning
    $btn.wrap( '<span class="morphing-btn-wrap"></span>' ).on('click', function(e) {
      e.preventDefault();

      self.start();
    });

  };

  Morphing.prototype.start = function() {
    var self = this;

    if ( self.$btn.hasClass('morphing-btn_circle') ) {
      return;
    }

    // Set initial width, because it is not possible to start CSS transition from "auto"
    self.$btn.width( self.$btn.width() ).parent().width( self.$btn.outerWidth() );

    self.$btn.off('.fm').on("transitionend.fm webkitTransitionEnd.fm oTransitionEnd.fm MSTransitionEnd.fm", function(e) {

      if ( e.originalEvent.propertyName === 'width' ) {
        self.$btn.off('.fm');

        self.animateBg();
      }

    }).addClass('morphing-btn_circle');

  };

  Morphing.prototype.animateBg = function() {
    var self = this;

    self.scaleBg();

    self.$clone.show();

    // Trigger repaint
    self.$clone[0].offsetHeight;

    self.$clone.off('.fm').on("transitionend.fm webkitTransitionEnd.fm oTransitionEnd.fm MSTransitionEnd.fm", function(e) {
      self.$clone.off('.fm');

      self.complete();

    }).addClass('morphing-btn-clone_visible');
  };

  Morphing.prototype.scaleBg = function() {
    var self = this;

    var $clone = self.$clone;
    var scale  = self.getScale();
    var $btn   = self.$btn;
    var pos    = $btn.offset();

    $clone.css({
      top       : pos.top  + $btn.outerHeight() * 0.5 - ( $btn.outerHeight() * scale * 0.5 ) - $(window).scrollTop(),
      left      : pos.left + $btn.outerWidth()  * 0.5 - ( $btn.outerWidth()  * scale * 0.5 ) - $(window).scrollLeft(),
      width     : $btn.outerWidth()  * scale,
      height    : $btn.outerHeight() * scale,
      transform : 'scale(' + ( 1 / scale ) + ')'
    });
  };

  Morphing.prototype.getScale = function() {
    var $btn    = this.$btn,
        radius  = $btn.outerWidth() * 0.5,
        left    = $btn.offset().left + radius - $(window).scrollLeft(),
        top     = $btn.offset().top  + radius - $(window).scrollTop(),
        windowW = $(window).width(),
        windowH = $(window).height();

    var maxDistHor  = ( left > windowW / 2 ) ? left : ( windowW - left ),
        maxDistVert = ( top > windowH / 2 )  ? top  : ( windowH - top );

    return Math.ceil(Math.sqrt( Math.pow( maxDistHor, 2 ) + Math.pow( maxDistVert, 2 ) ) / radius );
  };

  Morphing.prototype.complete = function() {
    var self = this;
    var $btn = self.$btn;

    $.fancybox.open({ src : $btn.data('src') || $btn.attr('href') }, self.opts);

    $(window).on('resize.fm', function() {
      //self.scaleBg();
    });
  };

  Morphing.prototype.close = function() {
    var self   = this;
    var $clone = self.$clone;

    self.scaleBg();

    $clone.one('transitionend webkitTransitionEnd oTransitionEnd MSTransitionEnd', function(e) {
      $clone.hide();

      self.$btn.removeClass('morphing-btn_circle');
    });

    $clone.removeClass('morphing-btn-clone_visible');

    $(window).off('resize.fm');
  };

  // Init
  this.each(function() {
    var $this = $(this);

    if ( !$this.data("morphing") ) {
      $this.data( "morphing", new Morphing( $this, opts ) );
    }

  });

  return this;
};
