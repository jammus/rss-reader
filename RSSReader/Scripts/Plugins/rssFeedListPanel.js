(function($) {
    $.fn.rssFeedListPanel = function(options) {
        options = $.extend({}, $.fn.rssFeedListPanelDefaults, options);

        return this.each(function() {
            var $element = $(this);

            $('a.feed', $element).live('click', function() {
                var $this = $(this);
                var href = $this.attr('href');
                var displayPanel = $(options.displayPanel);
                $.ajax({
                    url: href,
                    success: function(data, textStatus) {
                        $(options.chanelPanel).html(data);
                        $('a.current', $element).removeClass('current');
                        $this.addClass('current');
                        $('iframe', displayPanel).attr('src', '');
                    },
                    dataType: 'html'
                });
                return false;
            });

            $element.bind('refresh', function(e, callback) {
                $.ajax({
                    url: options.url,
                    dataType: 'html',
                    success: function(data, textStatus) {
                        $element.html(data);
                        if (typeof (callback) == 'function') {
                            callback();
                        }
                    }
                });
            });
        });
    };

    $.fn.rssFeedListPanelDefaults = {
        url: '/Feed/List',
        chanelPanel: '',
        displayPanel: ''
    };
})(jQuery);