using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RSSReader.Models;

namespace RSSReader.ViewModels
{
    public class FeedReaderViewModel
    {
        public List<Feed> Feeds { get; set; }
        public List<NewsItem> NewsItems { get; set; }
        public Feed CurrentFeed { get; set; }
        public Feed NewFeed { get; private set; }
        public NewsItem CurrentNewsItem { get; set; }

        public int CurrentFeedId
        {
            get
            {
                return CurrentFeed != null ? CurrentFeed.FeedId : 0;
            }
        }

        public FeedReaderViewModel()
        {
            Feeds = new List<Feed>();
            NewsItems = new List<NewsItem>();
            NewFeed = new Feed();
        }
    }
}
