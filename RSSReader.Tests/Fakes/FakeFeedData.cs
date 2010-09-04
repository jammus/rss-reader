using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RSSReader.Models;

namespace RSSReader.Tests.Fakes
{
    class FakeFeedData
    {
        public static List<Feed> CreateFakeFeedData(){
            var feeds = new List<Feed>();

            feeds.Add(new Feed()
            {
                FeedId = 1,
                Name = "Brent Ozar - SQL Server DBA",
                UserName = "jammus",
                Url = "http://www.brentozar.com/feed/"
            });

            feeds.Add(new Feed()
            {
                FeedId = 2,
                Name = "Coding Horror",
                UserName = "jammus",
                Url = "http://feeds.feedburner.com/codinghorror"
            });

            feeds.Add(new Feed()
            {
                FeedId = 3,
                Name = "Blog - Stack Overflow",
                UserName = "jammus",
                Url = "http://blog.stackoverflow.com/feed/"
            });

            feeds.Add(new Feed()
            {
                FeedId = 4,
                Name = "Hack the Planet in Exile",
                UserName = "jammus",
                Url = "http://blog.felter.org/rss"
            });

            feeds.Add(new Feed()
            {
                FeedId = 5,
                Name = "Jeffrey Zeldman Presents The Daily Report",
                UserName = "jammus",
                Url = "http://www.zeldman.com/feed/"
            });

            feeds.Add(new Feed()
            {
                FeedId = 6,
                Name = "kung fu grippe",
                UserName = "barry",
                Url = "http://www.kungfugrippe.com/rss"
            });

            return feeds;
        }
    }
}
