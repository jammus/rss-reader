using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RSSReader.Models
{
    public interface IFeedRepository
    {
        List<Feed> GetUsersFeeds(string userName);
        Feed GetUsersFeed(int feedId, string userName);
        void Add(Feed feed);
        void Save();
        void Delete(Feed feed);
    }
}
