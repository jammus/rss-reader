(function($) {
    $.fn.rssChannelPanel = function(options) {
        options = $.extend({}, $.fn.rssChannelPanelDefaults, options);

        return this.each(function() {
            var $element = $(this);
            var displayPanel = $(options.displayPanel);
            $('a.newsItem', $element).live('click', function() {
                $('iframe', displayPanel).attr('src', $(this).attr('href'));
                return false;
            });
        });
    };

    $.fn.rssChannelPanelDefaults = {
        displayPanel: ''
    };
})(jQuery);