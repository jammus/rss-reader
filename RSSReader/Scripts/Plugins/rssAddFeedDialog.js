(function($) {
    $.fn.rssAddFeedDialog = function(options) {
        options = $.extend({}, $.fn.rssAddFeedDialogDefaults, options);

        return this.each(function() {
            var $element = $(this);
            $('a.cancel', $element).click(function() {
                $element.dialog('close');
                return false;
            });

            var failureNotice = $('.addFailure', $element);

            var feedList = $(options.feedList);

            var form = $('form', $element);
            form.submit(function() {
                failureNotice.hide();

                function addSuccess(data, textStatus) {
                    if (data.success) {
                        $element.dialog('close');
                        feedList.trigger('refresh', [function() { $('a.current').click(); } ]);
                    }
                    else {
                        addFailure(data, textStatus);
                    }
                }

                function addFailure(data, textStatus) {
                    failureNotice.show();
                    setTimeout(function() {
                        failureNotice.fadeOut();
                    }, 5000);
                }

                $.ajax({
                    url: form.attr('action'),
                    data: form.serialize(),
                    type: 'post',
                    dataType: 'json',
                    success: addSuccess,
                    error: addFailure
                });

                return false;
            });

            $element.dialog({
                autoOpen: false,
                modal: true,
                title: options.title,
                close: function() {
                    form.get(0).reset();
                }
            });
        });
    };

    $.fn.rssAddFeedDialogDefaults = {
        title: 'Add Feed',
        feedList: ''
    };
})(jQuery);