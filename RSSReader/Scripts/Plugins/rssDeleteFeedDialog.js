(function($) {
    $.fn.rssDeleteFeedDialog = function(options) {
        options = $.extend({}, $.fn.rssDeleteFeedDialogDefaults, options);

        return this.each(function() {
            var $element = $(this);
            var feedList = $(options.feedList);

            $('a.cancel', $element).click(function() {
                $element.dialog('close');
                return false;
            });

            var form = $('form', $element);
            form.submit(function() {
                $.ajax({
                    url: form.attr('action'),
                    data: form.serialize(),
                    type: 'post',
                    dataType: 'json',
                    success: function(data, textStatus) {
                        feedList.trigger('refresh', [function() { $('a.current').click(); } ]);
                        $element.dialog('close');
                    }
                });

                return false;
            });

            $element.dialog({
                autoOpen: false,
                modal: true,
                title: options.title,
                open: function() {
                    var link = $('a.current', feedList);
                    form.attr('action', link.attr('href').replace('/View/', '/Delete/'));
                    $('span.feedName').text(link.text());
                }
            });
        });
    };

    $.fn.rssDeleteFeedDialogDefaults = {
        title: 'Delete Feed',
        feedList: ''
    };
})(jQuery);