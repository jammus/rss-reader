using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;

namespace RSSReader.Models
{
    public class FeedService
    {
        IFeedRepository FeedRepository;

        public FeedService()
            : this(new SQLFeedRepository())
        {
        }

        public FeedService(IFeedRepository feedRepository)
        {
            FeedRepository = feedRepository;
        }

        public List<Feed> GetUsersFeeds(string userName)
        {
            return FeedRepository.GetUsersFeeds(userName);
        }

        public Feed GetUsersFeed(int feedId, string userName)
        {
            return FeedRepository.GetUsersFeed(feedId, userName);
        }

        public Feed CreateFeedFromXmlDoc(XmlDocument xmlDoc, string userName)
        {
            Feed feed = null;

            if (xmlDoc != null)
            {
                RSSFeedReader feedReader = new RSSFeedReader(xmlDoc);
                feed = feedReader.ReadFeedDetails();
            }

            if (feed != null)
            {
                feed.Url = xmlDoc.BaseURI;
                feed.UserName = userName;
                Save(feed);
            }

            return feed;
        }

        public Feed Save(Feed feed)
        {
            var existingFeed = GetUsersFeed(feed.FeedId, feed.UserName);
            if (existingFeed == null)
            {
                feed = Create(feed);
            }
            else
            {
                feed = Update(existingFeed, feed);
            }
            return feed;
        }

        public void Delete(Feed feed)
        {
            FeedRepository.Delete(feed);
            FeedRepository.Save();
        }

        private Feed Create(Feed feed)
        {
            FeedRepository.Add(feed);
            FeedRepository.Save();
            return feed;
        }

        private Feed Update(Feed existingFeed, Feed feed)
        {
            existingFeed.Name = feed.Name;
            existingFeed.Url = feed.Url;
            FeedRepository.Save();
            return existingFeed;
        }
    }
}
