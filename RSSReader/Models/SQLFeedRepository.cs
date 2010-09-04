using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RSSReader.Models
{
    public class SQLFeedRepository : IFeedRepository
    {
        private RSSReaderDataContext db = new RSSReaderDataContext();

        public List<Feed> GetUsersFeeds(string userName)
        {
            return (from f in db.Feeds
                    where f.UserName == userName
                    select f).ToList();
        }

        public Feed GetUsersFeed(int feedId, string userName)
        {
            return (from f in db.Feeds
                    where f.UserName == userName && f.FeedId == feedId
                    select f).SingleOrDefault();
        }

        public void Add(Feed feed)
        {
            db.Feeds.InsertOnSubmit(feed);
        }

        public void Delete(Feed feed)
        {
            db.Feeds.DeleteOnSubmit(feed);
        }

        public void Save()
        {
            db.SubmitChanges();
        }
    }
}
