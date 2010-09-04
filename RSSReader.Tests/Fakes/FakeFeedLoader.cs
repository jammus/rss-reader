using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using RSSReader.Models;

namespace RSSReader.Tests.Fakes
{
    class FakeFeedLoader : IFeedLoader
    {
        public XmlDocument LoadXML(string source)
        {
            string feedName;

            switch (source)
            {
                case "http://rss.slashdot.org/Slashdot/slashdot":
                    feedName = "slashdot";
                    break;
                case "http://www.brentozar.com/feed/":
                    feedName = "brentozar";
                    break;
                default:
                    feedName = "notfound";
                    break;
            }

            return LoadFeed(feedName);
        }

        private XmlDocument LoadFeed(string feedName)
        {
            XmlDocument xmlDoc;
            try
            {
                xmlDoc = FakeXMLFeed.GetFakeXMLFeed(feedName);
            }
            catch
            {
                // invalid feed so return null
                xmlDoc = null;
            }
            return xmlDoc;
        }
    }
}
