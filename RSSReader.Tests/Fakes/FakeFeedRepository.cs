using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RSSReader.Models;

namespace RSSReader.Tests.Fakes
{
    class FakeFeedRepository : IFeedRepository
    {
        private List<Feed> db;

        public FakeFeedRepository() : this(FakeFeedData.CreateFakeFeedData())
        {
        }

        public FakeFeedRepository(List<Feed> data)
        {
            db = data;
        }

        public List<Feed> GetUsersFeeds(string userName)
        {
            return db.Where(f => f.UserName == userName).ToList();
        }

        public Feed GetUsersFeed(int feedId, string userName)
        {
            return db.Where(f => f.UserName == userName && f.FeedId == feedId).SingleOrDefault();
        }

        public void Add(Feed feed)
        {
            feed.FeedId = db.Max(f => f.FeedId) + 1;
            db.Add(feed);
        }

        public void Delete(Feed feed)
        {
            db.Remove(feed);
        }

        public void Save()
        {
        }
    }
}
