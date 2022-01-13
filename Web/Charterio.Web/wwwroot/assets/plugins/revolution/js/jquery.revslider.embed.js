(function ($) {

    var options = {
        embedLoaded: true,
        assetsLocation: '',
        cssLocation: '',
        fontsLocation: '',
        jsLocation: '',
        selectorTemplate: 'div.revslider[data-alias={alias}]'
    };

    var sliders = [],
        assets = [];

    var setOptions = function(data) {
        options = $.extend(options, data);
    };

    var regReplace = function(search, replace, content, escape) {
        var regString = typeof escape != 'undefined' && ! escape ? search : search.replace(/([.*+?^$|(){}\[\]])/mg, "\\$1");
        return content.replace(new RegExp('(' + regString + ')', 'g'), replace);
    }

    var embed = function(target, slider) {

        if (typeof slider.styles != 'undefined') {
            if (options.assetsLocation.length) {
                slider.styles = regReplace('"' + slider.locations.assets, '"' + options.assetsLocation, slider.styles);
            }
            if (options.cssLocation.length) {
                slider.styles = regReplace('"' + slider.locations.css, '"' + options.cssLocation, slider.styles);
            }
            if (options.fontsLocation.length) {
                slider.styles = regReplace('"' + slider.locations.fonts, '"' + options.fontsLocation, slider.styles);
            }
            $('head').append(slider.styles);
        }
        if (typeof slider.assets != 'undefined') {

            $.each(slider.assets, function(key, asset) {

                var assetUrl = asset;

                if (options.cssLocation.length && assetUrl.indexOf(slider.locations.fonts) == -1) {
                    assetUrl = regReplace(slider.locations.css, options.cssLocation, assetUrl);
                }
                if (options.fontsLocation.length) {
                    assetUrl = regReplace(slider.locations.fonts, options.fontsLocation, assetUrl);
                }
                if (assets.indexOf(assetUrl) == -1) {

                    assets.push(assetUrl);

                    if ( ! $('link[href="' + assetUrl + '"]').length) {

                        console.log('add', assetUrl);

                        $('head').append('<link rel="stylesheet" href="' + assetUrl + '" type="text/css" />');
                    }
                }
            });
        }
        if ($(target).length && typeof slider.content != 'undefined') {
            if (options.assetsLocation.length) {
                slider.content = regReplace('"' + slider.locations.assets, '"' + options.assetsLocation, slider.content);
            }
            if (options.cssLocation.length) {
                slider.content = regReplace('"' + slider.locations.css, '"' + options.cssLocation, slider.content);
            }
            if (options.fontsLocation.length) {
                slider.content = regReplace('"' + slider.locations.fonts, '"' + options.fontsLocation, slider.content);
            }
            if (options.jsLocation.length) {
                slider.content = regReplace('"' + slider.locations.js, '"' + options.jsLocation, slider.content);
                slider.content = regReplace('jsFileLocation(?:.*),', 'jsFileLocation:"' + options.jsLocation + '",', slider.content, false);
            }
            $(target).html(slider.content);
        }
    };

    /**
     * Slider Revolution jQuery Embed Plugin
     *
     * @param {...*} var_args
     */

    $.fn.embedRevslider = function(var_args) {
        var action = '',
            data = {};
        if (arguments.length == 0) {
            action = 'init';
        } else if (arguments.length == 1) {
            action = 'init';
            data = arguments[0];
        } else {
            action = arguments[0];
            data = arguments[1];
        }
        switch (action) {
            case 'init' :
                setOptions(data);
                if (typeof punchgs != 'undefined') {
                    assets.push('jquery.themepunch.tools.min.js');
                }
                if (typeof jQuery().revolution != 'undefined') {
                    assets.push('jquery.themepunch.revolution.min.js');
                }
                if (typeof document.styleSheets != "undefined") {
                    for (var i = 0; i < document.styleSheets.length; i++) {
                        if (document.styleSheets[i].href && document.styleSheets[i].href.indexOf('settings.css') != -1) {
                            assets.push('settings.css');
                            break;
                        }
                    }
                }
                if (options.embedLoaded) {
                    $.each(sliders, function(index, data) {
                        embed(options.selectorTemplate.replace('{alias}', data.alias), data);
                    });
                }
                break;
            case 'load' :
                var slider = JSON.parse(data);
                if (typeof slider.alias != 'undefined') {
                    var added = false;
                    $.each(sliders, function(index, data) {
                        if (data.alias == slider.alias) {
                            sliders[index] = slider;
                            added = true;
                        }
                    });
                    if ( ! added) {
                        sliders.push(slider);
                    }
                }
                break;
            case 'embed' :
                embed(this, JSON.parse(data));
                break;
            case 'options' :
                setOptions(data);
                break;
        }
        return this;
    };

}(jQuery));