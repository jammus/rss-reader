$(function() {
    var channelPanel = $('#channelPanel').rssChannelPanel({
        displayPanel: '#displayPanel'
    });

    var feedList = $('#feedListPanel').rssFeedListPanel({
        chanelPanel: '#channelPanel',
        displayPanel: '#displayPanel'
    });

    var addFeedDialog = $('#addFeedModal').rssAddFeedDialog({
        feedList: feedList
    });

    var deleteFeedDialog = $('#deleteFeedModal').rssDeleteFeedDialog({
        feedList: feedList
    });

    $('form#addFeed input[type=submit]').live('click', function() {
        addFeedDialog.dialog('open');
        return false;
    });

    $('form#deleteFeed input[type=submit]').live('click', function() {
        deleteFeedDialog.dialog('open');
        return false;
    });
});
